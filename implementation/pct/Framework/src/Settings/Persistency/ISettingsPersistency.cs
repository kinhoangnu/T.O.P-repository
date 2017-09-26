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

namespace com.vanderlande.wpf
{
    public interface ISettingsPersistency : IDisposable
    {
        void PushGroup(string name);
        void PopGroup();

        bool Read(SettingsPageViewModel vm);
        bool Write(SettingsPageViewModel vm);

        bool Read<T>(string key, out T value, T defaultValue = default(T));
        bool Write<T>(string key, T value);

        bool ReadList<T>(string key, out List<T> value);
        bool WriteList<T>(string key, IEnumerable<T> value);
    }
}

