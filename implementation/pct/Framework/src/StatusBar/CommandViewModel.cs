﻿/*
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
namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for all CommandViewModels (settings, ...)
    /// </summary>
    public class CommandViewModel : ViewModel
    {
        #region Properties

        public RelayCommand Execute { get;  private set; }

        #endregion

        public CommandViewModel(RelayCommand cmd)
        {
            Execute = cmd;
        }

    }
}
