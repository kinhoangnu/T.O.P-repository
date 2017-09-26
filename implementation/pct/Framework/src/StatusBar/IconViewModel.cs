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
namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for all IconViewModels (connected, disconnected, ...)
    /// The difference between a Notification and an Icon is that an Icon has no command attached.
    /// </summary>
    public class IconViewModel : ViewModel
    {
        #region Properties

        private bool _isVisible = false;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { ChangeProperty(ref _isVisible, value); }
        }

        #endregion
    }
}
