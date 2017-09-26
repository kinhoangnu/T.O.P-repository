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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;


namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for all ViewModels
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        #region Property Notify Changed

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the event when a property has been changed.
        /// </summary>
        /// <param name="id">The name of the property (optional)</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string id = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(id));
            }
        }


        /// <summary>
        /// Change a property value, if it is different.
        /// When different, also raise the property-changed event.
        /// </summary>
        /// <typeparam name="T">the field type.</typeparam>
        /// <param name="field">the current field value</param>
        /// <param name="value">the new field value</param>
        /// <param name="id">the field name (optional)</param>
        /// <returns>True when the property is changed.</returns>
        protected bool ChangeProperty<T>(ref T field, T value, [CallerMemberName] string id = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            RaisePropertyChanged(id);
            return true;
        }

        #endregion

        #region Update properties when language has changed.
            // List of framework elements that already have been updated.
            // To avoid indirect infinite recursion.
        private static List<FrameworkElement> hasBeenRefreshed = null;

        /// <summary>
        /// This method is used when switching language to refresh the contents of all windows/views.
        /// If the derived viewmodel holds framework elements, they need to be updated through the virtual RefreshProperties method.
        /// </summary>
        public static void RefreshAllProperties()
        {
            Debug.Assert(hasBeenRefreshed == null);
            hasBeenRefreshed = new List<FrameworkElement>();
            foreach (Window wnd in Application.Current.Windows)      // Process main window AND other child windows.
            {
                RefreshProperties(wnd);
            }
            hasBeenRefreshed = null;
        }


        // Update the properties in the content entries (e.g. language has changed).
        protected static void RefreshProperties(FrameworkElement element)
        {
            if (element == null)
            {
                return;
            }
            if (hasBeenRefreshed.Contains(element))     // Use a container to prevent (indirect) infinite recursion.
            {
                return;
            }
            hasBeenRefreshed.Add(element);

            object old = element.DataContext;           // This sequence will also refresh collections and trigger static resources in the XAML.
            element.DataContext = null;
            element.DataContext = old;

            ViewModel vm = element.DataContext as ViewModel;
            if (vm != null)
            {
                vm.RefreshProperties();
            }
        }


        // Let a ViewModel derivative do addition stuff when properties have to be refreshed (e.g. language has changed).
        protected virtual void RefreshProperties()
        { }

        #endregion

    }
}
