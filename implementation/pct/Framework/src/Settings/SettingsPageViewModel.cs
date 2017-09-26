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

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace com.vanderlande.wpf
{
    public abstract class SettingsPageViewModel : ContentViewModel
    {
        public SettingsViewModel SettingsViewModel = null;

        protected override void RaisePropertyChanged([CallerMemberName] string id = null)
        {
            base.RaisePropertyChanged(id);
            if (SettingsViewModel != null)
            {
                SettingsViewModel.PageHasChanged();
            }
        }

        public abstract bool Read(ISettingsPersistency sp);
        public abstract bool Write(ISettingsPersistency sp);
        public abstract void LoadSettings();
        public abstract void ApplySettings();
        public abstract void UndoSettings();
        public abstract bool HaveSettingsChanged();

        public override string ToString()
        {
            string name = GetType().Name;       // Just return part of the class name, not the complete namespace.
            const string postfix = "ViewModel";
            Debug.Assert(name.EndsWith(postfix));
            return name.Substring(0, name.Length - postfix.Length);
        }
    }
}
