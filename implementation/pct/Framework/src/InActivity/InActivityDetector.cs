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
* See http://stackoverflow.com/questions/4963135/wpf-inactivity-and-activity  
*
*/
using System;
using System.Runtime.InteropServices;

namespace com.vanderlande.wpf
{
    internal class IdleTimeInfo
    {
        internal DateTime LastInputTime { get; set; }
        internal TimeSpan IdleTime { get; set; }
        internal int SystemUptimeMilliseconds { get; set; }
    }


    internal struct LASTINPUTINFO
    {
        internal uint cbSize;
        internal uint dwTime;
    }

    internal static class InActivityDetector
    {
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        internal static IdleTimeInfo GetIdleTimeInfo()
        {
            int systemUptime = Environment.TickCount;
            int idleTicks = 0;

            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                int lastInputTicks = (int)lastInputInfo.dwTime;
                idleTicks = systemUptime - lastInputTicks;
            }

            return new IdleTimeInfo
                {
                    LastInputTime = DateTime.Now.AddMilliseconds(-1 * idleTicks),
                    IdleTime = new TimeSpan(0, 0, 0, 0, idleTicks),
                    SystemUptimeMilliseconds = systemUptime,
                };
        }
    }

}
