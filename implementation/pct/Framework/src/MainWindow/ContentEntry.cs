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
using System;
using System.Windows;

namespace com.vanderlande.wpf
{
    public class ContentEntry
    {
        public Type Type { get; private set; }
        public bool Active;
        public bool Selected 
        {
            get { return Element != null; } 
        }

        internal FrameworkElement Element;
        internal ContentViewModel ViewModel;

        internal int SequenceNumber;      // Selection sequence numbering; active selected page has the lowest number.

        internal int EntryNumber;         // Entry sequence number to sort the buttons on the bottom bar.

        internal ContentEntry(Type type)
        {
            Type = type;
            Active = false;             // Is the content active, visible?
            Reset();             
        }

        internal void Reset()
        {
            Element = null;
            ViewModel = null;
        }

        internal ContentViewModel GetViewModel()
        {
            ContentViewModel cvm = null;
            if ((Element != null) && (Element.DataContext != null))
            {
                cvm = Element.DataContext as ContentViewModel;
            }
            return cvm ?? ViewModel;
        }


        public override string ToString()
        {
            string typeName = Type.Name;
            return typeName.Substring(0, typeName.Length - 9);        // Strip off "ViewModel"
        }
    }

}
