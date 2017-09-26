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
using System.Diagnostics;
using System.Windows;


namespace com.vanderlande.wpf
{
    /// <summary>
    /// Event handling for all Content ViewModels.
    /// </summary>
    public partial class ContentViewModel : ValidationViewModel
    {
        /// <summary>
        /// Called when this viewmodel has been created by the main window logic.
        /// </summary>
        public virtual void OnCreated()
        {
            ValidateAll();
            if (Parent == null)                 // Childrens layout is loaded in LoadPreviousSession
            {
                LoadLayout();
            }
            foreach (ContentViewModel cv in _childViewModels)
            {
                cv.OnCreated();
            }
        }


        /// <summary>
        /// Called when this viewmodel is loaded in the (tree of) visible views.
        /// </summary>
        public virtual void OnLoaded()
        {
            IsVisible = true;
            foreach (ContentViewModel cv in _childViewModels)
            {
                cv.OnLoaded();
            }
        }

        /// <summary>
        /// Called when this viewmodel is unloaded from the (tree of) visible views.
        /// </summary>
        public virtual void OnUnloaded()
        {
            foreach (ContentViewModel cv in _childViewModels)
            {
                cv.OnUnloaded();
            }
            IsVisible = false;
        }


        /// <summary>
        /// Called when this viewmodel has (about to be) destroyed by the main window logic.
        /// </summary>
        public virtual void OnDestroy()
        {
            foreach (ContentViewModel cv in _childViewModels)
            {
                cv.OnDestroy();
            }
            if (Parent == null)                 // Childrens layout is saved in SaveCurrentSession
            {
                SaveLayout();
            }
            Attach(null);
        }


        public void DetachEvents()
        {
            DetachEventHandlers();
        }

        #region Private methods

        private void AttachEventHandlers()
        {
            if (Element == null)
            {
                return;
            }
            Element.Loaded += OnLoadedEvent;
            Element.Unloaded += OnUnloadedEvent;
        }


        private void DetachEventHandlers()
        {
            if (Element == null)
            {
                return;
            }
            Element.Loaded -= OnLoadedEvent;
            Element.Unloaded -= OnUnloadedEvent;
        }


        private void OnLoadedEvent(object sender, RoutedEventArgs e)
        {
            OnLoaded();
        }


        private void OnUnloadedEvent(object sender, RoutedEventArgs e)
        {
            OnUnloaded();
        }


        #endregion


    }
}
