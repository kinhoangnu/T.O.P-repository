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
using System.Collections.Generic;
using System.Diagnostics;

namespace com.vanderlande.wpf
{
    internal class PageGroupManager
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly Stack<PageGroup> _stack = new Stack<PageGroup>();


        internal PageGroup CurrentPageGroup
        {
            get { return _stack.Peek();  }
        }


        internal int NumberOfGroups
        {
            get { return _stack.Count; }
        }


        internal PageGroupManager(MainWindowViewModel vm)
        {
            _mainWindowViewModel = vm;
        }


        internal void Open(PageGroup grp)
        {
            if (NumberOfGroups > 0)
            {
                CurrentPageGroup.Hide(_mainWindowViewModel);
                CurrentPageGroup.IsCurrent = false;
            }
            _stack.Push(grp);
            CurrentPageGroup.IsActive = true;
            CurrentPageGroup.IsCurrent = true;
            if (NumberOfGroups == 1)
            {
                return;
            }
            grp.Open(_mainWindowViewModel);
        }


        internal void Close(PageGroup grp)
        {
            Debug.Assert(_stack.Peek() == grp);
            Debug.Assert(NumberOfGroups > 1);

            CurrentPageGroup.IsCurrent = false;
            CurrentPageGroup.IsActive = false;

            grp.Close(_mainWindowViewModel);
            _stack.Pop();
            _stack.Peek().Open(_mainWindowViewModel);

            Debug.Assert(CurrentPageGroup.IsActive == true);
            CurrentPageGroup.IsCurrent = true;
        }

    }
}
