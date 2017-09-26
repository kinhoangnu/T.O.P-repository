/*
*  Copyright (c) 2015 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*/
namespace com.vanderlande.wpf
{
    /// <summary>
    /// Logger class that provides logging entries for VI_WPF
    /// </summary>
    public static class Logger
    {
        #region Assertion and Basic string logging
        public delegate void LineHandler(string str);
        public static event LineHandler OnLine;

        internal static void LogLine(string str)
        {
            if (OnLine != null)
                OnLine(str);
        }

        /// <summary>
        /// When a release build is created, Debug.Assert statements are removed.
        /// Using this Assert method, some Assert logging is still available.
        /// This method is empty when a debug build is created.
        /// </summary>
        /// <param name="valid"></param>
        /// <param name="str"></param>
        /// <param name="location"></param>
        internal static void Assert(bool valid, string str = "", string location = "")
        {
#if ! DEBUG
            if (valid == true)
                return;
            LogLine(string.Format("Assertion failed {0} at {1}", str, location));
#endif
        }
        #endregion
        
        #region Command Logging
        public static bool CommandFallThrough = true;
        public delegate void CommandHandler(string cmd, string par, string from, bool valid);
        public static event CommandHandler OnCommand;

        internal static void LogCommand(string cmd, string par, string from, bool valid)
        {
            if (OnCommand != null)
                OnCommand(cmd, par, from, valid);
            else if (CommandFallThrough == true)
            {
                LogLine(string.Format("Command {0}({1}) from {2} was {3}valid.", cmd, par, from, (valid == false) ? "not " : ""));
            }
        }
        #endregion
        
        #region Error logging
        public static bool ErrorFallThrough = true;
        public delegate void ErrorHandler(string str);
        public static event ErrorHandler OnError;

        internal static void LogError(string str)
        {
            if (OnError != null)
                OnError(str);
            else if (ErrorFallThrough == true)
            {
                LogLine("Error: " + str);
            }
        }
        #endregion

    }

}
