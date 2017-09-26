/*
*  Copyright (c) 2015 Vanderlande Industries
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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace com.vanderlande.wpf
{
    public class ScreenSaver : InActivityProcess
    {
        public static TimeSpan Timeout = new TimeSpan(0, 0, 15, 0);     // Start screensaver after 15 minutes
        public static TimeSpan Duration = new TimeSpan(0, 0, 0, 15);    // Show each page 15 seconds.

        private MainWindowViewModel _mainWindowVm;
        private DispatcherTimer _timer;
        private static readonly ObservableCollection<Type> _pageList = new ObservableCollection<Type>();
        private int _currentPageIndex;
        private FrameworkElement _currentPage;


        public static void AddPage(Type type)
        {
            _pageList.Add(type);
        }


        public ScreenSaver() :
            base(Timeout)
        { }


        public override bool OnStart()
        {
            if (_pageList.Count == 0)
            {
                return false;
            }
            _mainWindowVm = ViApplication.Instance.MainWindowViewModel;
            _currentPageIndex = 0;
            _currentPage = null;
            OpenPage();
            if (_pageList.Count > 1)
            {
                StartTimer();
            }
            return true;
        }


        public override void OnStop()
        {
            StopTimer();
            ClosePage();
        }


        private void OpenPage()
        {
            Debug.Assert(_currentPage == null);
            ContentViewModel vm = Activator.CreateInstance(_pageList[_currentPageIndex]) as ContentViewModel;
            Debug.Assert(vm != null);
            _currentPage = ViewLocator.CreateView(vm);
            vm.OnCreated();
            _mainWindowVm.PushModalDialog(_currentPage);
        }


        private void ClosePage()
        {
            Debug.Assert(_currentPage != null);
            _mainWindowVm.PopModalDialog(_currentPage);
            _currentPage = null;
        }


        private void StartTimer()
        {
            if (_pageList.Count == 1)
            {
                return;
            }
            _timer = new DispatcherTimer();
            _timer.Interval = Duration;
            _timer.Tick += NextPage;
            _timer.Start();
        }


        private void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }
        }


        private void NextPage(object obj, EventArgs args)
        {
            ClosePage();
            ++_currentPageIndex;
            if (_currentPageIndex == _pageList.Count)
            {
                _currentPageIndex = 0;
            }
            OpenPage();
        }
    }
}
