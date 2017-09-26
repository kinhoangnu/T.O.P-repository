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
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Behaviour of an items control to scroll the newly added item in the bottom, when the scrollbar is all the way down.
    /// </summary>
    public class LastElementVisibleBehavior : Behavior<ItemsControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged += OnCollectionChanged;
        }


        protected override void OnDetaching()
        {
            ((INotifyCollectionChanged) AssociatedObject.Items).CollectionChanged -= OnCollectionChanged;
            base.OnDetaching();
        }


        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)   
        {
            if (e.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }

            ScrollViewer scrollbar = GetScrollViewer();
            if (scrollbar == null)                         // No scroll bar found.
            {
                return;
            }
                            // Check if scrollbar is all the way down.
                            // If it is, scroll the added items in view.
            if (scrollbar.VerticalOffset >= scrollbar.ScrollableHeight) 
            {
                scrollbar.ScrollToEnd();
            }
        }


        /// <summary>
        /// Get the scrollviewer which might be a child of a control (like datagrid, listbox) 
        /// or the parent of the ItemsControl
        /// </summary>
        /// <returns></returns>
        private ScrollViewer GetScrollViewer()
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(AssociatedObject); ++i)       // Number of children could be 0 when page is not visible.
            {                                                                                   // In that case, the GetChild will throw an exception (invalid index).
                Decorator border = VisualTreeHelper.GetChild(AssociatedObject, i) as Decorator;
                ScrollViewer scroll = (border == null) ? null : border.Child as ScrollViewer;
                if (scroll != null)
                {
                    return scroll;
                }
            }
            return GetScrollViewerParent();
        }
            

        /// <summary>
        /// Find scrollviewer which is the parent of the itemscontrol
        /// </summary>
        /// <returns></returns>
        private ScrollViewer GetScrollViewerParent()
        {
            FrameworkElement fa = AssociatedObject;
            while (fa != null)
            {
                ScrollViewer sv = fa as ScrollViewer;
                if (sv != null)
                {
                    return sv;
                }
                fa = fa.Parent as FrameworkElement;
            }
            return null;
        }

    }
}