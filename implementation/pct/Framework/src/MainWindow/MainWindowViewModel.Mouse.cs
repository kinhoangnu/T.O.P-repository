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
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Mouse handling (breaking pure MVVM pattern quite a bit...)
    /// </summary>
    public partial class MainWindowViewModel : WindowViewModel
    {
        private void AttachMouseEvents()
        {
            Window.MouseDown += OnScrollPage;
            VanderlandeLogo region = FindInVisualTreeDown(Window, typeof(VanderlandeLogo)) as VanderlandeLogo;
            if (region != null)
            {
                region.MouseDown += OnFloatPage;
            }
        }


        private void DetachMouseEvents()
        {
            Window.MouseDown -= OnScrollPage;
            VanderlandeLogo region = FindInVisualTreeDown(Window, typeof(VanderlandeLogo)) as VanderlandeLogo;
            if (region != null)
            {
                region.MouseDown -= OnFloatPage;
            }
        }


        private static DependencyObject FindInVisualTreeDown(DependencyObject parent, Type type)
        {
            if ((parent == null) || (parent.GetType() == type))
            {
                return parent;
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = FindInVisualTreeDown(VisualTreeHelper.GetChild(parent, i), type);
                if (child != null)
                {
                    return child;
                }
            }
            return null;
        }


        private void OnScrollPage(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 1)
            {
                return;
            }
            switch (e.ChangedButton)
            {
                case MouseButton.XButton1:
                    SelectedContent = GetAdjecentPage(-1);
                    break;
                case MouseButton.XButton2:
                    SelectedContent = GetAdjecentPage(1);
                    break;
            }
        }


        /// <summary>
        /// Scroll to the next/previous page.
        /// </summary>
        private ContentEntry GetAdjecentPage(int step)
        {
            int idx = GetCurrentPageIndex();
            if (idx < 0)
            {
                return SelectedContent;
            }
            for (int i = 0; i < SelectedContentEntries.Count; ++i)
            {
                idx += step;
                if (idx < 0)
                {
                    idx = SelectedContentEntries.Count - 1;
                }
                if (idx == SelectedContentEntries.Count)
                {
                    idx = 0;
                }
                if (SelectedContentEntries[idx].Element != null)
                {
                    return SelectedContentEntries[idx];
                }
            }
            return SelectedContent;
        }

        
        private int GetCurrentPageIndex()
        {
            for (int i = 0; i < SelectedContentEntries.Count; ++i)
            {
                if (SelectedContentEntries[i] == SelectedContent)
                {
                    return i;
                }
            }
            return -1;
        }


        private void OnFloatPage(object sender, MouseButtonEventArgs e)
        {
            if ((e.ChangedButton == MouseButton.Left) &&
                (e.ClickCount == 1) &&
                ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) == KeyStates.Down) &&
                ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) == KeyStates.Down))
            {
                FloatCurrentPage();
            }
        }


        private void FloatCurrentPage()
        {
            if (Content == null)
            {
                return;
            }
            ContentViewModel vm = SelectedContent.GetViewModel();
            if ((vm == null) || (!vm.CanClosePage()) || (_pageGroupManager.NumberOfGroups != 1))
            {
                return;
            }
            FloatCurrentPage(vm);
        }


        private void FloatCurrentPage(ContentViewModel vm)
        {
            vm.DetachEvents();
            RemoveFloatingPageFromCollection();
            FloatPageViewModel fwvm = new FloatPageViewModel();
            ViewLocator.CreateView(fwvm);
            if (fwvm.Window == null)
            {
                Trace.WriteLine("Could not create floating page for {0}", vm.GetType().Name);
                return;
            }

            fwvm.Window.Show();
            fwvm.Content = vm.Element;
        }


        private void RemoveFloatingPageFromCollection()
        {
            ContentEntry ce = SelectedContent;
            ContentEntry prev = GetPreviousPage();
            ce.Reset();
            SelectedContentEntries.Remove(ce);
            SelectedContent = prev;                 // Activate that previous page, if here is one.
        }

    }
}
