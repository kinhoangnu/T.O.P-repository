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
    public class DummyFunctionalModule : FunctionalModule
    {
        public static bool ReturnInitialize;
        public static bool ReturnCanActivate;
        public static bool ReturnActivate;
        public static bool ReturnCanDeActivate;
        public static bool ReturnDeActivate;
        public static bool ReturnCanDispose;
        public static bool ReturnDispose;

        public static bool ThrowInitialize;
        public static bool ThrowCanActivate;
        public static bool ThrowActivate;
        public static bool ThrowCanDeActivate;
        public static bool ThrowDeActivate;
        public static bool ThrowCanDispose;
        public static bool ThrowDispose;
        public static bool ThrowOnIdle;
        public static bool ThrowOnTimer;
            
        public static int CalledInitialize;
        public static int CalledCanActivate;
        public static int CalledActivate;
        public static int CalledCanDeActivate;
        public static int CalledDeActivate;
        public static int CalledCanDispose;
        public static int CalledDispose;
        public static int CalledOnIdle;
        public static int CalledOnTimer;

        public static void Reset()
        {
            ReturnInitialize = true;
            ReturnCanActivate = true;
            ReturnActivate = true;
            ReturnCanDeActivate = true;
            ReturnDeActivate = true;
            ReturnCanDispose = true;
            ReturnDispose = true;

            ThrowInitialize = false;
            ThrowCanActivate = false;
            ThrowActivate = false;
            ThrowCanDeActivate = false;
            ThrowDeActivate = false;
            ThrowCanDispose = false;
            ThrowDispose = false;
            ThrowOnIdle = false;
            ThrowOnTimer = false;

            CalledInitialize = 0;
            CalledCanActivate = 0;
            CalledActivate = 0;
            CalledCanDeActivate = 0;
            CalledDeActivate = 0;
            CalledCanDispose = 0;
            CalledDispose = 0;
            CalledOnIdle = 0;
            CalledOnTimer = 0; 
        }

        public DummyFunctionalModule() :
            base()
        {}


        public DummyFunctionalModule(TimerRunMode runMode, TimeSpan timeSpan) :
            base(runMode, timeSpan)
        { }

        protected override bool Initialize()
        {
            ++CalledInitialize;
            if (ThrowInitialize)
                throw new Exception("Initialize");
            return ReturnInitialize;
        }


        protected override bool CanActivate()
        {
            ++CalledCanActivate;
            if (ThrowCanActivate)
                throw new Exception("Initialize");
            return ReturnCanActivate;
        }


        protected override bool Activate()
        {
            ++CalledActivate;
            if (ThrowActivate)
                throw new Exception("Activate");
            return ReturnActivate;
        }


        protected override bool CanDeActivate()
        {
            ++CalledCanDeActivate;
            if (ThrowCanDeActivate)
                throw new Exception("CanDeActivate");
            return ReturnCanDeActivate;
        }


        protected override bool DeActivate()
        {
            ++CalledDeActivate;
            if (ThrowDeActivate)
                throw new Exception("DeActivate");
            return ReturnDeActivate;
        }


        protected override bool CanDispose()
        {
            ++CalledCanDispose;
            if (ThrowCanDispose)
                throw new Exception("CanDispose");
            return ReturnCanDispose;
        }


        protected override bool Dispose()
        {
            ++CalledDispose;
            if (ThrowDispose)
                throw new Exception("Dispose");
            return ReturnDispose;
        }


        protected override void OnIdle()
        {
            ++CalledOnIdle;
            if (ThrowOnIdle)
                throw new Exception("OnIdle");
        }


        protected override void OnTimer()
        {
            DispatcherTimer.Kick();
            ++CalledOnTimer;
            if (ThrowOnTimer)
                throw new Exception("OnTimer");
        }
        
    }
}
