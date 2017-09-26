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

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Main Window persistency logic
    /// </summary>
    public partial class MainWindowViewModel : WindowViewModel
    {
        /// <summary>
        /// Load the settings, the active page list and others. 
        /// </summary>
        private void LoadSettings()
        {
            using (ISettingsPersistency sp = new SettingsPersistencyXML())
            {
                foreach (SettingsPageViewModel vm in _settings)
                {
                    vm.Read(sp);
                }
                LoadActivePages(sp);
            }
        }


        private void LoadActivePages(ISettingsPersistency sp)
        {
            List<string> list;
            string selected;
            using (new SettingsPersistencyGroup(sp, "ActivePageList"))
            {
                sp.ReadList("Page", out list);
                sp.Read("Selected", out selected);
            }
            foreach (string page in list)
            {
                LoadActivePage(page);
            }
            if (string.IsNullOrEmpty(selected) == false)
            {
                ActivatePage(selected);
            }
        }


        private void LoadActivePage(string type)
        {
            ContentEntry ce = ContentEntries.FirstOrDefault(x => x.Type.ToString() == type);
            if ((ce != null) && (ViewLocator.DoesViewExist(ce.Type) == true))
            {
                ActivateContent(ce);
            }
        }


        private void ActivatePage(string type)
        {
            ContentEntry ce = SelectedContentEntries.FirstOrDefault(x => x.Type.ToString() == type);
            if ((ce != null) && (ViewLocator.DoesViewExist(ce.Type) == true))
            {
                SelectedContent = null;
                SelectedContent = ce;
            }
        }


        private void SaveActivePages()
        {
            using (ISettingsPersistency sp = new SettingsPersistencyXML())
            {
                SaveActivePages(sp);
            }
        }


        private void SaveActivePages(ISettingsPersistency sp)
        {
            using (new SettingsPersistencyGroup(sp, "ActivePageList"))
            {
                sp.WriteList("Page", SelectedContentEntries.Select(ce => ce.Type.ToString()).ToList());
                if (SelectedContent != null)
                {
                    sp.Write("Selected", SelectedContent.Type);
                }
            }
        }

    }
}
