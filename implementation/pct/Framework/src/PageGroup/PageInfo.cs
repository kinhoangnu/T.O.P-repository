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

namespace com.vanderlande.wpf
{
    internal class PageInfo
    {
        internal readonly Type PageType;
        internal bool InitialVisible;
        internal bool? IsVisible;

        internal PageInfo(Type pageType, bool initialVisible = true)
        {
            PageType = pageType;
            InitialVisible = initialVisible;
            IsVisible = null;
        }

    }
}
