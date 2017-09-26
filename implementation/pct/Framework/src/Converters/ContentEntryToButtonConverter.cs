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
using System.Diagnostics;
using System.Globalization;
using System.Windows;

namespace com.vanderlande.wpf
{
    internal class ContentEntryToButtonConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ContentEntry ce = value as ContentEntry;
            if ((ce == null) || (ce.Element == null))
            {
                return null;
            }
            string name = ViewLocator.GetViewModelBaseName(ce.Type.FullName);

            Type type = ce.Type.Assembly.GetType(name + "PageButton");
            if (type == null)               // Static button (image + text)
            {
                FrameworkElement pb = new PageButton();
                Debug.Assert(pb != null);
                pb.DataContext = ce.Element == null ? null : ce.Element.DataContext;
                return pb;
                
            }
                                            // Active page button.
            FrameworkElement apb = Activator.CreateInstance(type) as FrameworkElement;
            Debug.Assert(apb != null);
            name = type.FullName + "ViewModel";
            Type vmType = type.Assembly.GetType(name);
            ContentViewModel vm = vmType == null ? null : Activator.CreateInstance(vmType) as ContentViewModel;
            if (vm == null)                 // Not a dedicated page button viewmodel
            {
                apb.DataContext = ce.Element == null ? null : ce.Element.DataContext;
                return apb;
            }

            vm.Attach(apb);                 // Dedicated page button viewmodel
            return apb;

        }

    }
}
