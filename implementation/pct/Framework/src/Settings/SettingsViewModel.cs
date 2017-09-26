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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace com.vanderlande.wpf
{
    public class SettingsViewModel : WindowViewModel
    {
        private ObservableCollection<SettingsPageViewModel> _settingsList;
        public ObservableCollection<SettingsPageViewModel> SettingsList
        {
            get { return _settingsList; }
            set { ChangeProperty(ref _settingsList, value); }
        }


        private SettingsPageViewModel _selectedSetting;
        public SettingsPageViewModel SelectedSetting
        {
            get { return _selectedSetting; }
            set
            {
                if (ChangeProperty(ref _selectedSetting, value))
                {
                    OnSelectionChanged();
                }
            }
        }


        private FrameworkElement _currentPage;
        public FrameworkElement CurrentPage
        {
            get { return _currentPage; }
            set
            {
                FrameworkElement prev = CurrentPage;
                if (ChangeProperty(ref _currentPage, value))
                {
                    ContentViewModel vm = (prev == null) ? null : prev.DataContext as ContentViewModel;
                    if (vm != null)
                    {
                        vm.DetachEvents();
                    }
                }
            }
        }


        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand ApplyCommand { get; private set; }
        public RelayCommand UndoCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }


        public SettingsViewModel()
        {
            Mediator.Default.Raise(new OnSettings(OnSettings.WhatEnum.StartEdit));
            SettingsList = new ObservableCollection<SettingsPageViewModel>();
            SaveCommand = new RelayCommand(OnSave, HasChanged);
            ApplyCommand = new RelayCommand(OnApply, HasChanged);
            UndoCommand = new RelayCommand(OnUndo, HasChanged);
            CancelCommand = new RelayCommand(OnCancel);
        }


        public override bool CanClosePage()
        {
            return false;
        }


        public override void OnLoaded()
        {
            base.OnLoaded();
            foreach (SettingsPageViewModel page in SettingsList)
            {
                page.LoadSettings();
            }
            if (SettingsList.Count > 0)
            {
                SelectedSetting = SettingsList[0];
            }
        }



        public override void Close()
        {
            CurrentPage = null;
            if (Window == null)
            {
                MainWindowViewModel mwvm = Application.Current.MainWindow.DataContext as MainWindowViewModel;
                mwvm.PopModalDialog(Element);
            }
            base.Close();
            Mediator.Default.Raise(new OnSettings(OnSettings.WhatEnum.StopEdit));
        }


        internal void PageHasChanged()
        {
                        // Refresh the settings page list which might have 'changed' due to a language change.
            ObservableCollection<SettingsPageViewModel> list = SettingsList;
            SettingsPageViewModel selected = SelectedSetting;
            SettingsList = null;
            SettingsList = list;
            SelectedSetting = selected;

            SaveCommand.Invalidate();
            ApplyCommand.Invalidate();
            UndoCommand.Invalidate();
        }


        internal void Add(SettingsPageViewModel page)
        {
            SettingsList.Add(page);
            page.SettingsViewModel = this;
        }


        private void OnSave(object obj = null)
        {
            OnApply();
            Close();
        }


        private void OnApply(object obj = null)
        {
            bool hasChanged = HasChanged();
            using (ISettingsPersistency sp = new SettingsPersistencyXML())
            {
                foreach (SettingsPageViewModel page in SettingsList)
                {
                    page.ApplySettings();
                    sp.Write(page);
                }
            }
            PageHasChanged();
            if (hasChanged)
            {
                Mediator.Default.Raise(new OnSettings (OnSettings.WhatEnum.Changed));
            }
        }


        private bool HasChanged(object obj = null)
        {
            return SettingsList.Any(page => page.HaveSettingsChanged());
        }


        private void OnUndo(object obj = null)
        {
            bool hasChanged = HasChanged();
            using (ISettingsPersistency sp = new SettingsPersistencyXML())
            {
                foreach (SettingsPageViewModel page in SettingsList)
                {
                    sp.Read(page);
                    page.UndoSettings();
                }
            }
            PageHasChanged();
            if (hasChanged)
            {
                Mediator.Default.Raise(new OnSettings(OnSettings.WhatEnum.Changed));
            }
        }


        private void OnCancel(object obj = null)
        {
            OnUndo(obj);
            Close();
        }


        private void OnSelectionChanged()
        {
            if (SelectedSetting != null)
            {
                CurrentPage = ViewLocator.CreateView(SelectedSetting);
            }
        }
    }
}
