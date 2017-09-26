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
    /// Base class for all Notification ViewModels (error, warning, info controls)
    /// The difference between a Notification and an Icon is that a Notification has a 
    /// command attached. When the user clicks a notification, the command is executed.
    /// </summary>
    public abstract class NotificationViewModel : ViewModel
    {
        #region Properties

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { ChangeProperty(ref _isVisible, value); }
        }

        public RelayCommand Execute { get;  private set; }

        #endregion

        protected NotificationViewModel(RelayCommand cmd)
        {
            Execute = cmd;
        }

    }
}
