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
using System.Collections.ObjectModel;
using System.Linq;

namespace com.vanderlande.wpf
{
    public class CollectionViewModel : ContentViewModel
    {
        public ObservableCollection<ContentViewModel> Collection { get; private set; }


        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (ChangeProperty(ref _selectedIndex, value))
                {
                    SelectionChanged();
                }
            }
        }


        public ContentViewModel Selected
        {
            get
            {
                return (SelectedIndex == -1) ? null : Collection[SelectedIndex];
            }
            set
            {
                SelectedIndex = Collection.IndexOf(value);
            }
        }


        public CollectionViewModel()
        {
            Collection = new ObservableCollection<ContentViewModel>();
        }


        public void Add(ContentViewModel vm, bool select = false)
        {
            Insert(vm, Collection.Count, select);
        }


        public void Insert(ContentViewModel vm, int atIndex, bool select = false)
        {
            if (Element != null)
            {
                ViewLocator.CreateView(vm);
                vm.OnCreated();
            }
            Collection.Insert(atIndex, vm);
            if ((select) || (Collection.Count == 1))
            {
                SelectedIndex = atIndex;
            }
        }


        public void Remove(ContentViewModel vm)
        {
            if (Element != null)
            {
                if (Selected == vm)
                {
                    vm.OnUnloaded();
                }
                vm.OnDestroy();
            }
            Collection.Remove(vm);
            if (Selected == vm)
            {
                Selected = Collection.Any() ? Collection[0] : null;
            }
        }


        public override void OnCreated()
        {
            base.OnCreated();
            foreach (ContentViewModel vm in Collection)
            {
                ViewLocator.CreateView(vm);
                vm.OnCreated();
            }
        }

       
        public override void OnUnloaded()
        {
            if (Selected != null)
            {
                Selected.OnUnloaded();
            }
            base.OnUnloaded();
        }


        public override void OnDestroy()
        {
            foreach (ContentViewModel vm in Collection)
            {
                vm.OnDestroy();
            }
            base.OnDestroy();
        }


        private void SelectionChanged()
        {
            Selected = (SelectedIndex < 0) ? null : Collection[SelectedIndex];
        }

    }
}
