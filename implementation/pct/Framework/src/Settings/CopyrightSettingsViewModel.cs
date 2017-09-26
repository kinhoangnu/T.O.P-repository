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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace com.vanderlande.wpf
{
    internal class CopyrightSettingsViewModel : SettingsPageViewModel
    {
        public static List<CopyrightInfo> Entries { get; private set; }

        static CopyrightSettingsViewModel()
        {
            Entries = new List<CopyrightInfo>
                {
                                     // Insert this VI_WPF and VI_STYLING modules next in the list of entries.
                    new CopyrightInfo(Assembly.GetAssembly(typeof (CopyrightSettingsViewModel))),
                    new CopyrightInfo(Assembly.GetAssembly(typeof (BaseConverter)))
                };
        }

        internal CopyrightSettingsViewModel()
        {
                                    // Insert the application as the first in the list of entries.
                                    // Cannot be added in the static ctor since dynamic resources are used.
            ViApplication appl = ViApplication.Instance;
            string name = appl.Project + " " + appl.Name.ToResourceString("Application");
            if (Entries.Any(ci => ci.Product == name) == false)
            {
                Entries.Insert(0, new CopyrightInfo(name, Assembly.GetEntryAssembly()));
            }
        }

        internal static void Add(CopyrightInfo info)
        {
            if (Entries.Any(ci => ci.Product == info.Product) == false)
            {
                Entries.Add(info);
            }
        }

        public override bool Read(ISettingsPersistency sp)
        {
            return true;
        }

        public override bool Write(ISettingsPersistency sp)
        {
            return true;
        }

        public override void LoadSettings()
        {}

        public override void ApplySettings()
        {}

        public override void UndoSettings()
        {}

        public override bool HaveSettingsChanged()
        {
            return false;
        }

    }
}
