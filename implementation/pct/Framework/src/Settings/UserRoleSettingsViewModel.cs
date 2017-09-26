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
using System.Linq;

namespace com.vanderlande.wpf
{
    public class UserRoleSettingsViewModel : SettingsPageViewModel
    {
        public List<string> Values { get; private set; }

        private string _selected;
        public string Selected
        {
            get { return _selected; }
            set { ChangeProperty(ref _selected, value); } 
        }


        public UserRoleSettingsViewModel()
        {
            Values = User.Current.Role.AvailableRoles.ToList();
            Selected = User.Current.Role.CurrentRole;
        }


        public override bool Read(ISettingsPersistency sp)
        {
            using (new SettingsPersistencyGroup(sp, "User"))
            {
                string role;
                sp.Read("CurrentUserRole", out role, "Guest");
                User.Current.Role.CurrentRole = role;
            }
            return true;
        }


        public override bool Write(ISettingsPersistency sp)
        {
            using (new SettingsPersistencyGroup(sp, "User"))
            {
                return sp.Write("CurrentUserRole", User.Current.Role.CurrentRole);
            }
        }


        public override void LoadSettings()
        {
            UndoSettings();
        }


        public override void ApplySettings()
        {
            User.Current.Role.CurrentRole = Selected;
        }


        public override void UndoSettings()
        {
            Selected = User.Current.Role.CurrentRole;
        }


        public override bool HaveSettingsChanged()
        {
            return (User.Current.Role.CurrentRole != Selected);
        }


    }
}
