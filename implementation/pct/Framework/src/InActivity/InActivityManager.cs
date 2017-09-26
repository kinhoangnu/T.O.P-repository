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
using System.Collections.Generic;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Class for managing user inactivity processing.
    /// </summary>
    public static class InActivityManager
    {
        private static readonly List<InActivityProcess> _processes = new List<InActivityProcess>();

        public static void Add(InActivityProcess iap)
        {
            _processes.Add(iap);
        }

        public static void Remove(InActivityProcess iap)
        {
            _processes.Remove(iap);
        }

        internal static void Step()
        {
            var idleTime = InActivityDetector.GetIdleTimeInfo();
            foreach (InActivityProcess iap in _processes)
            {
                if (idleTime.IdleTime < iap.IdleTime)
                {
                    iap.Stop();
                }
                else
                {
                    iap.Start();
                }
            }
        }

    }

}
