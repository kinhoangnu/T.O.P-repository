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
using System;
using System.Globalization;
using System.Windows;

namespace com.vanderlande.wpf
{
    internal class ViewModelToViewConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ViewModel vm = value as ViewModel;
            if (vm == null)
            {
                return null;
            }
            FrameworkElement fe = ViewLocator.CreateView(vm);
            if (fe != null)
            {
                fe.DataContext = vm;
            }
            return fe;
        }
    }

}
