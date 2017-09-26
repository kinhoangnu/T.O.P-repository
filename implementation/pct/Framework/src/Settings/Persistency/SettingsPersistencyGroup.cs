﻿/*
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
    public class SettingsPersistencyGroup : IDisposable
    {
        private ISettingsPersistency _sp;

        public SettingsPersistencyGroup(ISettingsPersistency sp, string name)
        {
            _sp = sp;
            _sp.PushGroup(name);
        }


        public void Dispose()
        {
            _sp.PopGroup();
        }
    }
}

