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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace com.vanderlande.wpf
{
    class SelectPageViewModel : WindowViewModel
    {
        public ObservableCollection<ContentEntry> Pages { get; private set; }

        private ContentEntry _selected = null;
        public ContentEntry Selected
        {
            get { return _selected; }
            set
            {
                if (ChangeProperty(ref _selected, value) == true)
                {
                    SelectCommand.Invalidate();
                }
            }
        }

        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public delegate bool ActivateContent(Type type);
        private readonly ActivateContent _activateContent;

        public SelectPageViewModel(IEnumerable<ContentEntry> entries, ActivateContent ac)
        {
            Pages = new ObservableCollection<ContentEntry>();
            foreach (ContentEntry ce in entries.Where(ce => ce.Selected == false))
            {
                if (ce.Selected == true)
                {
                    continue;
                }
                if (ViewLocator.DoesViewExist(ce.Type) == false)
                {
                    continue;
                }
                Pages.Add(ce);
            }
            _activateContent = ac;
            SelectCommand = new RelayCommand(OnSelect, CanSelect);
            CancelCommand = new RelayCommand(OnCancel);

        }

        private void OnSelect(object obj)
        {
            Close();
            _activateContent(Selected.Type);
        }

        private bool CanSelect(object obj)
        {
            return (Selected != null);
        }

        private void OnCancel(object obj)
        {
            Close();
        }

        public override void Close()
        {
            if (Window == null)
            {
                MainWindowViewModel mwvm = Application.Current.MainWindow.DataContext as MainWindowViewModel;
                mwvm.PopModalDialog(Element);
            }
            base.Close();
        }

    }
}
