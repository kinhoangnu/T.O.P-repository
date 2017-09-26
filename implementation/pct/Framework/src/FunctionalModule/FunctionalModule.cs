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
    /// Base class for the functional modules. 
    /// Multiple Functional Modules can be combined in a single application.
    /// </summary>
    public abstract class FunctionalModule
    {
        public enum TimerRunMode
        {
            OnlyWhenActive,
            Always,
            Never
        }

        public bool IsActive { get; protected set; }

        private readonly TimerRunMode _timerRunMode;
        private DispatcherTimer _timer;
        private bool _timerHasTicked;

        #region Internal Methods
        internal bool DoInitialize()
        {
            if (_timer != null)
            {
                _timer.Start();
            }
            try
            {
                return Initialize();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("Initialize exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
            return false;
        }


        internal bool DoCanActivate()
        {
            if (IsActive)
            {
                return false;
            }
            try
            {
                return CanActivate();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("CanActivate exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
            return false;
        }


        internal bool DoActivate()
        {
            try
            {
                IsActive = Activate();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("Activate exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
            return IsActive;
        }


        internal bool DoCanDeActivate()
        {
            if (IsActive == false)
            {
                return false;
            }
            try
            {
                return CanDeActivate();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("CanDeActivate exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
            return false;
        }


        internal bool DoDeActivate()
        {
            try
            {
                IsActive = ! DeActivate();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("DeActivate exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
            return ! IsActive;
        }


        internal bool DoCanDispose()
        {
            if (IsActive)
            {
                return false;
            }
            try
            {
                return CanDispose();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("CanDispose exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
            return false;
        }


        internal bool DoDispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }
            try
            {
                return Dispose();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("Dispose exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
            return false;
        }


        internal void DoOnIdle()
        {
            try
            {
                OnIdle();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("OnIdle exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
        }


        #endregion

        #region Protected (overridable) methods.
        protected FunctionalModule()
        {
            IsActive = false;
            _timerRunMode = TimerRunMode.Never;
            _timer = null;
            _timerHasTicked = false;
        }


        protected FunctionalModule(TimerRunMode runMode, TimeSpan timeSpan) :
            this()
        {
            if (runMode == TimerRunMode.Never)
            {
                return;
            }
            _timerRunMode = runMode;
            _timer = new DispatcherTimer { Interval = timeSpan };
            _timer.Tick += OnTimerTick;

        }


        protected virtual bool Initialize()
        {
            return true;
        }


        protected virtual bool CanActivate()
        {
            return true;
        }


        protected virtual bool Activate()
        {
            return true;
        }


        protected virtual bool CanDeActivate()
        {
            return true;
        }


        protected virtual bool DeActivate()
        {
            return true;
        }


        protected virtual bool CanDispose()
        {
            return true;
        }


        protected virtual bool Dispose()
        {
            return true;
        }


        protected virtual void OnIdle()
        { }


        protected virtual void OnTimer()
        { }

        #endregion

        #region Private Methods

        private void OnTimerTick(object sender, EventArgs e)
        {
            if ((_timerRunMode == TimerRunMode.OnlyWhenActive) && (IsActive == false))
            {
                return;
            }
            if (_timerHasTicked)
            {
                return;
            }
            _timerHasTicked = true;
            try
            {
                OnTimer();
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("OnTimer exception in functional module {0}: {1}", GetType().Name, ex.Message));
            }
            _timerHasTicked = false;
        }

        #endregion
    }

}
