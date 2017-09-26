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
using System.Collections.Generic;
using System.Linq;

namespace com.vanderlande.wpf
{
    public class PageGroup
    {
        private readonly List<PageInfo> _pages;

        public int Count { get { return _pages.Count; } }

        /// <summary>
        /// Is this pagegroup active; has this pagegroup been opened?
        /// </summary>
        public bool IsActive       
        {
            get;
            internal set;
        }


        /// <summary>
        /// Is this pagegroup the current active (topmost) pagegroup.
        /// </summary>
        public bool IsCurrent
        {
            get;
            internal set;
        }

        
        public readonly bool CloseOneCloseAll;

        public PageGroup(bool closeOnecloseAll)
        {
            _pages = new List<PageInfo>();
            CloseOneCloseAll = closeOnecloseAll;
        }

        
        public void AddPage(Type pagetype, bool initVisible = false)
        {
            _pages.Add(new PageInfo(pagetype, initVisible));
        }


        public void RemovePage(Type pagetype)
        {
            int i = 0;
            while (i < _pages.Count)
            {
                if (_pages[i].PageType == pagetype)
                {
                    _pages.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }


        public bool ContainsPage(Type pagetype)
        {
            return _pages.Any(pi => pi.PageType == pagetype);
        }


        public bool ContainsPage(ViewModel vm)
        {
            return ContainsPage(vm.GetType());
        }


        internal void Open(MainWindowViewModel mainWnd)
        {
            foreach (PageInfo pi in _pages)
            {
                if (pi.IsVisible == null)
                {
                    pi.IsVisible = pi.InitialVisible;
                }
                mainWnd.ActivateContent(pi.PageType);
                if (pi.IsVisible == true)
                {
                    mainWnd.AddContentToMenuBar(pi.PageType);
                }
                else
                {
                    mainWnd.HideContent(pi.PageType);
                }
            }
            if (_pages.Count > 0)
            {
                mainWnd.ActivateContent(_pages[0].PageType);
            }
        }


        internal void Close(MainWindowViewModel mainWnd)
        {
            UnloadAndDestroyPages(mainWnd);
            Hide(mainWnd);
            foreach (PageInfo pi in _pages)
            {
                mainWnd.UnregisterContent(pi.PageType);
                pi.IsVisible = null;
            }
        }


        internal void Hide(MainWindowViewModel mainWnd)
        {
            foreach (PageInfo pi in _pages)
            {
                pi.InitialVisible = false;
            }
            foreach (ContentEntry ce in mainWnd.SelectedContentEntries.ToList())
            {
                foreach (PageInfo pi in _pages.Where(pi => pi.PageType == ce.Type))
                {
                    pi.InitialVisible = true;
                }
                mainWnd.HideContent(ce.Type);
            }
            mainWnd.SelectedContent = null;
        }


        private void UnloadAndDestroyPages(MainWindowViewModel mainWnd)
        {
            int ignore = mainWnd.ContentEntries.Count - Count;      // Ignore the pages that do not belong to this group.
            foreach (ContentEntry ce in mainWnd.ContentEntries)
            {

                ContentViewModel vm = ce.GetViewModel();
                if ((vm == null) || (ignore-- > 0))
                {
                    continue;
                }
                if (ce.Active == true)
                {
                    vm.OnUnloaded();
                }
                vm.OnDestroy();
            }
        }
    
    
    }
}
