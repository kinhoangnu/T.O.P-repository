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
using System.Windows.Threading;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for popup windows that can disappear automatically after a certain period.
    /// The Progess en Duration properties can be used to show a progress bar.
    /// Duration can be set to 0 to keep the popup visible.
    /// Adjusting the duration afterwards could close the popup.
    /// </summary>
    public abstract class PopupViewModel : WindowViewModel
    {
        private DispatcherTimer _timer;
        private const int _stepSize = 100;

        private int _duration = 1500;        // Time the window is visible in milliseconds.
        public int Duration
        {
            get { return _duration; }
            set
            {
                if ((ChangeProperty(ref _duration, value) == true) && (_timer != null))
                {
                    CanCloseWindow();
                }
            }
        }


        private int _progress;              // When progress is larger/equal a duration, the window closes.
        public int Progress
        {
            get { return _progress; }
            private set
            {
                if (value > Duration)
                {
                    value = Duration;
                }
                ChangeProperty(ref _progress, value);
            }
        }


        public override void OnCreated()
        {
            base.OnCreated();
            Progress = 0;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(_stepSize);
            _timer.Tick += CanCloseWindow;
            _timer.Start();
        }


        private void CanCloseWindow(object sender, EventArgs e)
        {
            Progress += _stepSize;
            CanCloseWindow();
        }


        private void CanCloseWindow()
        {
            if ((Duration != 0) && (Progress >= Duration))
            {
                _timer.Stop();
                Window.Close();
            }
        }

    }
}
