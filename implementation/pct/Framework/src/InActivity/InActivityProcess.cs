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

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for user inactivity processing.
    /// </summary>
    public abstract class InActivityProcess
    {
        public TimeSpan IdleTime { get; private set; }
        public bool IsActive { get; private set; }

        private bool _isEnabled;
        public bool IsEnabled 
        {
            get { return _isEnabled; }
            set
            {
                if ((value == false) && (IsActive == true))
                {
                    Stop();
                }
                _isEnabled = value;
            }
        }

        protected InActivityProcess(TimeSpan idle, bool enabled = true)
        {
            IdleTime = idle;
            IsActive = false;
            IsEnabled = enabled;
        }

        public abstract bool OnStart();
        public abstract void OnStop();

        internal void Start()
        {
            if ((IsEnabled == true) && (IsActive == false))
            {
                IsActive = OnStart();
            }
        }

        internal void Stop()
        {
            if ((IsEnabled == true) && (IsActive == true))
            {
                OnStop();
            }
            IsActive = false;
        }
    }

}
