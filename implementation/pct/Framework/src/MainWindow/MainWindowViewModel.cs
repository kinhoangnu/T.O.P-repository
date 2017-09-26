/*
*  Copyright (c) 2017 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*  
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for all application specific main windows
    /// </summary>
    public partial class MainWindowViewModel : WindowViewModel
    {
        #region Fields

        private ContentEntry _activeContent;
        private readonly Stack<FrameworkElement> _pageStack;    // When 'modal dialogs'have to appear inside the main frame, these pages are stacked.
        private readonly List<SettingsPageViewModel> _settings = new List<SettingsPageViewModel>();
        private readonly PageGroupManager _pageGroupManager;
        private ContentEntry _lastSelectedPage;                 // Required to restore the selected page when settings are changed.

        #endregion

        #region Properties
        public string UserRole
        {
            get { return User.Current.Role.CurrentRole; }
        }


        // The current page; WPF Framework element is the object that holds the DataContext field.
        private FrameworkElement _content;
        public FrameworkElement Content
        {
            get { return _content; }
            set
            {
                if (ChangeProperty(ref _content, value) == true)
                {
                    ClosePageCommand.Invalidate();
                    IsContentVisible = ((Dialog != null) || (Content != null));
                }
            }
        }

        // The dialog to display over the content
        private FrameworkElement _dialog;
        public FrameworkElement Dialog
        {
            get { return _dialog; }
            set
            {
                if (ChangeProperty(ref _dialog, value) == true)
                {
                    ClosePageCommand.Invalidate();
                    IsContentVisible = ((Dialog != null) || (Content != null));
                }
            }
        }

        // The complete list of available pages.
        private ObservableCollection<ContentEntry> _contentEntries;
        public ObservableCollection<ContentEntry> ContentEntries
        {
            get { return _contentEntries; }
            private set { ChangeProperty(ref _contentEntries, value); }
        }

        // The list of (previously) selected content entries; that list is displayed in the bottom bar.
        private ObservableCollection<ContentEntry> _selectedContentEntries;
        public ObservableCollection<ContentEntry> SelectedContentEntries
        {
            get { return _selectedContentEntries; }
            set { ChangeProperty(ref _selectedContentEntries, value); }
        }

        // The current selected content; that page is displayed.
        private ContentEntry _selectedContent;
        public ContentEntry SelectedContent
        {
            get { return _selectedContent; }
            set
            {
                ContentEntry prev = _selectedContent;
                if (ChangeProperty(ref _selectedContent, value) == true)
                {
                    ActivateContent(_selectedContent);
                    if ((_selectedContent != null) && (_selectedContent.Element == null))
                    {
                        SelectedContent = prev;
                    }
                }
            }
        }

        // Can the user select (additional) content (using the + button)
        private bool _canSelectContent = true;
        public bool CanSelectContent
        {
            get { return _canSelectContent; }
            set { ChangeProperty(ref _canSelectContent, value); }
        }


        // Is there content visible?
        private bool _isContentVisible = false;
        public bool IsContentVisible
        {
            get { return _isContentVisible; }
            private set { ChangeProperty(ref _isContentVisible, value); }
        }


        // Is there a "modal" dialog visible?
        private bool _isModalDialogActive = false;
        public bool IsModalDialogActive
        {
            get { return _isModalDialogActive; }
            private set
            {
                if (ChangeProperty(ref _isModalDialogActive, value) == true)
                {
                    ClosePageCommand.Invalidate();
                }
            }
        }


        // Shows the menu items or hides them.
        private bool _showSelectionMenu = false;
        public bool ShowSelectionMenu
        {
            get { return _showSelectionMenu; }
            private set { ChangeProperty(ref _showSelectionMenu, value) ; }
        }

        public static StatusBarViewModel StatusBar { get; private set; }

        public RelayCommand AddContentCommand { get; private set; }
        public RelayCommand ClosePageCommand { get; private set; }

        #endregion

        #region Public methods
        public MainWindowViewModel()
        {
            _pageGroupManager = new PageGroupManager(this);

            ContentEntries = new ObservableCollection<ContentEntry>();
            SelectedContentEntries = new ObservableCollection<ContentEntry>();
            StatusBar = new StatusBarViewModel();
            AddContentCommand = new RelayCommand(OnAddContent, CanAddContent);
            ClosePageCommand = new RelayCommand(OnClosePage, CanClosePage);

            _pageStack = new Stack<FrameworkElement>();

            AddSetting(new LanguageSettingsViewModel());
            AddSetting(new UserRoleSettingsViewModel());
            AddSetting(new CopyrightSettingsViewModel());
            AddCommand(new SettingsCommandViewModel(new RelayCommand(OnSettings)));
        }


        public void AddNotification(NotificationViewModel vm)
        {
            StatusBar.Notifications.Add(vm);
        }

        public void RemoveNotification(NotificationViewModel vm)
        {
            StatusBar.Notifications.Remove(vm);
        }

        public void AddCommand(CommandViewModel vm)
        {
            StatusBar.Commands.Add(vm);
        }

        public void RemoveCommand(CommandViewModel vm)
        {
            StatusBar.Commands.Remove(vm);
        }

        public void AddIcon(IconViewModel vm)
        {
            StatusBar.Icons.Add(vm);
        }

        public void RemoveIcon(IconViewModel vm)
        {
            StatusBar.Icons.Remove(vm);
        }

        /// <summary>
        /// Register the viewmodel types that can be viewmodel/content of the main window.
        /// </summary>
        /// <param name="type"></param>
        public void RegisterContent(Type type)
        {
            ContentEntry cvm = new ContentEntry(type);
            ContentEntries.Add(cvm);
            cvm.EntryNumber = ContentEntries.Count;
            AddContentCommand.Invalidate();
        }

        /// <summary>
        /// Adds the content to the menu bar and registers the entry if needed.
        /// </summary>
        /// <param name="type"></param>
        public void AddContentToMenuBar(Type type)
        {
            ContentEntry cvm = ContentEntries.FirstOrDefault(x => x.Type == type);
            if (cvm == null)
            {
                RegisterContent(type);
                cvm = ContentEntries.FirstOrDefault(x => x.Type == type);
            }
            AddToSelectedContent(cvm);
        }

        /// <summary>
        /// Unregister the viewmodel/content type
        /// </summary>
        /// <param name="type"></param>
        public void UnregisterContent(Type type)
        {
            ContentEntry cvm = ContentEntries.FirstOrDefault(x => x.Type == type);
            if (cvm != null)
            {
                ContentEntries.Remove(cvm);
                AddContentCommand.Invalidate();
            }
        }


        public override void OnCreated()
        {
            base.OnCreated();
            LoadSettings();
            if ((SelectedContent == null) && (SelectedContentEntries.Count > 0))       // If there is at least one visible, select it.
            {
                SelectedContent = SelectedContentEntries[0];
            }

            if ((ContentEntries.Count == 1) && (SelectedContentEntries.Count == 1))
            {
                ShowSelectionMenu = false; // If there is only one content window, and it is visible, do not show it in the page list.
                CanSelectContent = false;
            }
        }


        public override void OnLoaded()
        {
            base.OnLoaded();
            AttachMouseEvents();
            PageGroup grp = new PageGroup(false);
            foreach (ContentEntry ce in ContentEntries)
            {
                grp.AddPage(ce.Type, ce.Active);
            }
            _pageGroupManager.Open(grp);
        }


        /// <summary>
        /// Is the content, identified by its type active?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsContentActive(Type type)
        {
            ContentEntry entry = ContentEntries.FirstOrDefault(x => x.Type == type);
            return ((entry != null) && (entry.Active == true));
        }

        /// <summary>
        /// Has the content, identified by its type been selected (previously)?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsContentSelected(Type type)
        {
            ContentEntry entry = ContentEntries.FirstOrDefault(x => x.Type == type);
            return ((entry != null) && (entry.Selected == true));
        }


        public void Open(PageGroup grp)
        {
            _pageGroupManager.Open(grp);
        }


        public void Close(PageGroup grp)
        {
            _pageGroupManager.Close(grp);
        }

        /// <summary>
        /// Activate a specific viewmodel/content.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ActivateContent(Type type)
        {
            ContentEntry entry = ContentEntries.FirstOrDefault(x => x.Type == type);
            if (entry == null)
            {
                RegisterContent(type);
                entry = _contentEntries.Last();
            }
            SelectedContent = entry;
            return entry != null;
        }

        public bool HideContent(Type type)
        {
            ContentEntry entry = ContentEntries.FirstOrDefault(x => x.Type == type);
            if (entry != null)
            {
                if (entry.Element != null)
                {
                    entry.Element.Visibility = Visibility.Collapsed;
                }

                if (SelectedContent == entry)
                {
                    SelectedContent = null;
                }
                if (SelectedContentEntries.Any(x => x.Type == type))
                {
                    SelectedContentEntries.Remove(entry);
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// Can the content be unloaded. 
        /// </summary>
        /// <param name="closing">True when the application is closing.</param>
        /// <returns>True if the content can be unloaded.</returns>
        public bool CanUnloadContent(bool closing)
        {
            if (Content == null)
            {
                return true;
            }
            ContentViewModel cvm = Content.DataContext as ContentViewModel;
            if (cvm == null)
            {
                return true;
            }
            return cvm.CanUnload(closing);
        }


        /// <summary>
        /// Can the window be closed?
        /// </summary>
        /// <returns></returns>
        public override bool CanClose()
        {
            return CanUnloadContent(true);
        }


        public void AddSetting(SettingsPageViewModel vm)
        {
            _settings.Add(vm);
            if (Window == null)
            {
                return;
            }
            using (ISettingsPersistency sp = new SettingsPersistencyXML())
            {
                vm.Read(sp);
            }
        }


        // Show a 'modal' dialog over the content, inside the dialog area of the application.
        public void PushModalDialog(FrameworkElement dlg)
        {
            if (IsModalDialogActive)
            {
                _pageStack.Push(Dialog);
            }
            else
            {
                _pageStack.Push(Content);

                if (Content != null)
                {
                    // Content can be null in case all tabs are closed and the user wants to add new content, 
                    // which is done in a dialog.
                    Content.IsEnabled = false;
                }
            }

            Dialog = dlg;
            IsModalDialogActive = true;
        }

        public void PopModalDialog(FrameworkElement dlg)
        {
            if (_pageStack.Count == 1)
            {
                _pageStack.Pop();

                if (Content != null)
                {
                    Content.IsEnabled = true;
                }
                Dialog = null;
            }
            else
            {
                Dialog = _pageStack.Pop();
            }

            IsModalDialogActive = (_pageStack.Count > 0);
        }


        #endregion

        #region Protected Methods

        /// <summary>
        /// When closing the window, destroy all visible/selected content.
        /// </summary>
        protected override void OnClosed()
        {
            while (_pageGroupManager.NumberOfGroups > 1)
            {
                _pageGroupManager.Close(_pageGroupManager.CurrentPageGroup);
            }

            SaveActivePages();
            if ((_selectedContentEntries.Count == 0) && (SelectedContent != null))
            {                           // There is only on content available, and it is active.
                OnClosed(SelectedContent);
            }
                                        // Use a list so derived OnDestroy can close additional pages.
            foreach (ContentEntry ce in _selectedContentEntries.ToList())
            {
                OnClosed(ce);
            }
            DetachMouseEvents();
            CloseAllWindows();
            base.OnClosed();
        }


        /// <summary>
        /// Close all floating windows.
        /// </summary>
        private void CloseAllWindows()
        {
            for (int i = Application.Current.Windows.Count - 1; i >= 0; --i)
            {
                Application.Current.Windows[i].Close();
            }
        }

        // Update the properties in the content entries (e.g. language has changed).
        protected override void RefreshProperties()
        {
            base.RefreshProperties();
            foreach (ContentEntry ce in ContentEntries)
            {
                RefreshProperties(ce.Element);
            }
            if (IsModalDialogActive)
            {
                RefreshProperties(Content);
                foreach (FrameworkElement fe in _pageStack)
                {                                   // Page stack could contain other "modal" dialogs, not
                    RefreshProperties(fe);          // only ContentEntries.
                }
            }
            SelectedContent = _lastSelectedPage;    // Restore the last selected page when properties have been refreshed.
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// The user wants to add new content.
        /// </summary>
        /// <param name="parameter"></param>
        private void OnAddContent(object parameter = null)
        {
            SelectPageViewModel vm = new SelectPageViewModel(ContentEntries, ActivateContent);
            ViewLocator.CreateView(vm);
            PushModalDialog(vm.Element);
        }


        /// <summary>
        /// Can the user select/add new content
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanAddContent(object parameter = null)
        {
            if (CanUnloadContent(false) == false) // Can not unload the current child viewmodel.
            {
                return false;
            }
            return ContentEntries.Any(cvm => cvm.Selected == false);
        }


        /// <summary>
        /// Activate the Settings window.
        /// </summary>
        /// <param name="parameter"></param>
        private void OnSettings(object parameter = null)
        {
                                                        // When the settings are activated, the language is set/changed.
                                                        // This results in the FrameworkElement to become null temporarily (see ViewModel class).
                                                        // Due to the filtering of pages, the current selected content becomes null
            _lastSelectedPage = SelectedContent;        // Therefor, save the current selected contect and restore it later.

            SettingsViewModel svm = new SettingsViewModel();

            // TODO 20150128 nlcgeb Let the list of settings be configurable, depending on the application.
            foreach (SettingsPageViewModel vm in _settings)
            {
                svm.Add(vm);                
            }

            ViewLocator.CreateView(svm);
            if (svm.Window != null)
            {
                svm.Window.ShowDialog();
            }
            else
            {
                PushModalDialog(svm.Element);
            }
        }


        /// <summary>
        /// Activate a specific content. If unloading the current content is not possible, it will fail and return false.
        /// </summary>
        /// <param name="ce"></param>
        /// <returns></returns>
        private bool ActivateContent(ContentEntry ce)
        {
            if ((ce != null) && (ce.Active == true))
            {
                return true;
            }
            if (CanUnloadContent(false) == false)                // Can not unload the current child viewmodel.
            {
                return false;
            }
            ChangeContent(_activeContent, ce);
            _activeContent = ce;
            return true;
        }


        /// <summary>
        /// Replace the previous content by the new content.
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="next"></param>
        private void ChangeContent(ContentEntry prev, ContentEntry next)
        {
            if (prev != null)
            {
                prev.Active = false;
                if (prev.Element != null)
                {
                    prev.Element.Visibility = Visibility.Collapsed;
                }
            }
            if (next == null)
            {
                Content = null;
                StatusBar.Text = string.Empty;
                return;
            }
            if (next.Element == null)
            {
                object obj = Activator.CreateInstance(next.Type);
                ContentViewModel cvm = obj as ContentViewModel;
                Debug.Assert(cvm != null);
                ViewLocator.CreateView(cvm);
                cvm.OnCreated();
                next.Element = cvm.Element;
                next.ViewModel = cvm;
                AddToSelectedContent(next);
            }
            Content = next.Element;
            next.Active = true;
            if (next.Element != null)
            {
                next.Element.Visibility = Visibility.Visible;
            }
            next.SequenceNumber = -1;
            foreach (ContentEntry ce in SelectedContentEntries)
            {
                ce.SequenceNumber++;
            }
        }


        /// <summary>
        /// Add a new content entry to the list of visible/selected entries.
        /// </summary>
        /// <param name="cvm"></param>
        private void AddToSelectedContent(ContentEntry cvm)
        {
            if (SelectedContentEntries.Any(x => x == cvm))  // Item already belongs to the selected content list
            {
                return;
            }
            int idx = 0;
            for (int i = 0; i < SelectedContentEntries.Count; ++i)
            {
                if (SelectedContentEntries[i].EntryNumber > cvm.EntryNumber)
                {
                    break;
                }
                ++idx;
            }
            SelectedContentEntries.Insert(idx, cvm);

            AddContentCommand.Invalidate();

            ShowSelectionMenu = true;

        }


        private void OnClosed(ContentEntry ce)
        {
            ContentViewModel cvm = ce.GetViewModel();
            if (cvm == null)
            {
                return;
            }
            cvm.OnDestroy();
        }


        // Find the previous visible page, if there is one. Otherwise return null.
        private ContentEntry GetPreviousPage()
        {
            ContentEntry prev = SelectedContent;                         
            foreach (ContentEntry entry in SelectedContentEntries)
            {
                if (entry == SelectedContent)
                {
                    continue;
                }
                if ((prev == SelectedContent) || (entry.SequenceNumber < prev.SequenceNumber))
                {
                    prev = entry;
                }
            }
            return prev == SelectedContent ? null : prev;
        }   


        private void OnClosePage(object obj)
        {
            PageGroup grp = _pageGroupManager.CurrentPageGroup;
            if (grp.CloseOneCloseAll == true)
            {
                _pageGroupManager.Close(grp);
                return;
            }
            
            ContentEntry ce = SelectedContent;
            ContentViewModel vm = ce.GetViewModel();
            if (vm == null)
            {
                return;
            }

            ContentEntry prev = GetPreviousPage();

            vm.OnUnloaded();                        // Trigger it here, since WPF does not trigger the OnUnloadedEvent in this callsequence.
            SelectedContent = prev;                 // Activate that page.
            OnClosed(ce);
            SelectedContentEntries.Remove(ce);
            ce.Reset();
        }



        private bool CanClosePage(object obj)
        {
            if (IsModalDialogActive)
            {
                return false;
            }

            ContentViewModel vm = null;
            ContentEntry ce = SelectedContent;
            if ((ce != null) && (ce.Element != null))
            {
                vm = ce.GetViewModel();
            }
            return (vm != null) && (vm.CanClosePage());
        }

        #endregion
    }
}
