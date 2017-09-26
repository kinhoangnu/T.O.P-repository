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
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace com.vanderlande.wpf
{
    public class LanguageSettingsViewModel : SettingsPageViewModel
    {
        public List<CultureInfo> Values { get; private set; }

        private CultureInfo _original;
        private CultureInfo _selected;
        public CultureInfo Selected
        {
            get { return _selected; }
            set
            {
                if (ChangeProperty(ref _selected, value) == true)
                {
                    ViApplication.Instance.Language = value.Name;
                }
            } 
        }

        public LanguageSettingsViewModel()
        {
            Values = new List<CultureInfo>();
            ObtainAvailableLanguages();
        }

        public override bool Read(ISettingsPersistency sp)
        {
            using (new SettingsPersistencyGroup(sp, "Localization"))
            {
                string name;
                if (sp.Read("Language", out name, ViApplication.Instance.Language) == true)
                {
                    ViApplication.Instance.Language = name;
                }
                return true;
            }
        }

        public override bool Write(ISettingsPersistency sp)
        {
            using (new SettingsPersistencyGroup(sp, "Localization"))
            {
                return sp.Write("Language", ViApplication.Instance.Language);
            }
        }

        public override void LoadSettings()
        {
            if (_original == null)
            {
                _original = CultureInfo.GetCultureInfo(ViApplication.Instance.Language);
                UndoSettings();
            }
        }

        public override void ApplySettings()
        {
            _original = Selected;
        }

        public override void UndoSettings()
        {
            Selected = _original;
        }

        public override bool HaveSettingsChanged()
        {
            return (_original.Name != Selected.Name);
        }


        private void ObtainAvailableLanguages()
        {
            string exeDir = (new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location)).DirectoryName;
            string dir = exeDir + "\\Resources\\Languages";
            foreach (string name in Directory.GetFiles(dir, "*.xaml").Select(Path.GetFileNameWithoutExtension))
            {
                try
                {
                    CultureInfo ci = CultureInfo.GetCultureInfo(name);
                    Values.Add(ci);
                }
                catch (Exception)
                {
                    Logger.LogError(string.Format("Language {0} not known to the .Net environment", name));                    
                }
            }
        }

    }
}
