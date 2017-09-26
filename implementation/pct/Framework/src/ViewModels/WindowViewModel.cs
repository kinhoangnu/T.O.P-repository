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
using System.Windows;

namespace com.vanderlande.wpf
{
    public class WindowViewModel : ContentViewModel
    {
        #region Properties

        // In the extreme rare situations where the window is required, it can be obtained from the WindowViewModel
        // An example could be where the 3D camera position needs to be adjusted, based on (non) visual objects.
        public Window Window { get; private set; }

        public string Project
        {
            get { return ViApplication.Instance.Project; }          // Always use the application project as the project title for this window.
        }


        private string _title = ViApplication.Instance.Name;        // By default, use the application name as the title for this window.
        public string Title
        {
            get { return _title; }
            set { ChangeProperty(ref _title, value); }
        }

        #endregion

        #region Public methods

        public override void Attach(FrameworkElement element)
        {
            DetachEventHandlers();
            Window = element as Window;
            AttachEventHandlers();
            base.Attach(element);
        }

        /// <summary>
        /// This is not a page, so it can not be closed.
        /// </summary>
        /// <returns></returns>
        public override bool CanClosePage()
        {
            return false;
        }

        /// <summary>
        /// Simulate as if the user presses the close page button.
        /// There is no check if the window can be closed.
        /// </summary>
        public override void Close()
        {
            if (Window != null)
            {
                Window.Close();
            }
        }

        #endregion


        #region Protected methods
        public virtual bool CanClose()
        {
            return true;
        }

        protected virtual void OnClosed()
        {
            DetachEventHandlers();
        }

        protected virtual void OnActivated()
        {
        }

        protected virtual void OnDeactivated()
        {
        }


        protected override void SaveCurrentSession(ISettingsPersistency sp)
        {
            base.SaveCurrentSession(sp);
            SaveSizeAndPosition(sp);
        }

        protected override void LoadPreviousSession(ISettingsPersistency sp)
        {
            base.LoadPreviousSession(sp);
            LoadSizeAndPosition(sp);
        }

        #endregion


        #region Private methods


        private void AttachEventHandlers()
        {
            if (Window == null)
            {
                return;
            }
            Window.Closing += OnClosingEvent;
            Window.Closed += OnClosedEvent;
            Window.Activated += OnActivatedEvent;
            Window.Deactivated += OnDeactivatedEvent;
            OnCreated();
        }


        private void DetachEventHandlers()
        {
            if (Window == null)
            {
                return;
            }
            Window.Closing -= OnClosingEvent;
            Window.Closed -= OnClosedEvent;
            Window.Activated -= OnActivatedEvent;
            Window.Deactivated -= OnDeactivatedEvent;
        }


        private void OnClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = (CanClose() != true);
        }


        // Trigger the virtual methods in the correct order when a (main) window is closed.
        private void OnClosedEvent(object sender, System.EventArgs e)
        {
            OnUnloaded();
            OnClosed();
            OnDestroy();
        }

        private void OnActivatedEvent(object sender, EventArgs e)
        {
            OnActivated();
        }

        private void OnDeactivatedEvent(object sender, EventArgs e)
        {
            OnDeactivated();
        }


        private void SaveSizeAndPosition(ISettingsPersistency sp)
        {
            using (new SettingsPersistencyGroup(sp, "WindowSizeAndPosition"))
            {
                sp.Write("WindowTop", Window.Top);
                sp.Write("WindowLeft", Window.Left);
                sp.Write("WindowHeight", Window.Height);
                sp.Write("WindowWidth", Window.Width);
                sp.Write("WindowMaximized", Window.WindowState == WindowState.Maximized);
            }
        }

        
        private void LoadSizeAndPosition(ISettingsPersistency sp)
        {
            double top;
            double left;
            double height;
            double width;
            bool isMax;
            using (new SettingsPersistencyGroup(sp, "WindowSizeAndPosition"))
            {
                if ((sp.Read("WindowTop", out top) == false)            ||
                    (sp.Read("WindowLeft", out left) == false)          ||
                    (sp.Read("WindowHeight", out height) == false)      ||
                    (sp.Read("WindowWidth", out width) == false)        ||
                    (sp.Read("WindowMaximized", out isMax) == false))
                {
                    return;
                }
            }

            // Position and size the window to fit the current screen.
            // This can be altered due to a disconnected second screen or changed resolution.
            if (height > SystemParameters.VirtualScreenHeight)
            {
                height = SystemParameters.VirtualScreenHeight;
            }

            if (width > SystemParameters.VirtualScreenWidth)
            {
                width = SystemParameters.VirtualScreenWidth;
            }
            if (top + height / 2 > SystemParameters.VirtualScreenHeight)
            {
                top = SystemParameters.VirtualScreenHeight - height;
            }

            if (left + width / 2 > SystemParameters.VirtualScreenWidth)
            {
                left = SystemParameters.VirtualScreenWidth - width;
            }

            Window.Top = Math.Max(0, top);
            Window.Left = Math.Max(0, left);
            Window.Height = height;
            Window.Width = width;
            Window.WindowState = (isMax ? WindowState.Maximized : WindowState.Normal);
        }

        #endregion

    }
}
