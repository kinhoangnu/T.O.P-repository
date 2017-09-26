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
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Behaviour of a datagrid to scroll the new selected item (by MVVM databinding) into view.
    /// 
    /// See http://www.codeproject.com/Tips/125583/ScrollIntoView-for-a-DataGrid-when-using-MVVM
    /// 
    /// </summary>
    public class ScrollIntoViewBehavior : Behavior<DataGrid>
    {

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += new SelectionChangedEventHandler(OnSelectionChanged);
        }


        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= new SelectionChangedEventHandler(OnSelectionChanged);
            base.OnDetaching();
        }


        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = (sender as DataGrid);
            if ((grid == null) || (grid.SelectedItem == null))
            {
                return;
            }
            grid.UpdateLayout();
            grid.ScrollIntoView(grid.SelectedItem, null);
        }

    
    }
}