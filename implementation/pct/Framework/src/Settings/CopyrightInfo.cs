/*
*  Copyright (c) 2016 Vanderlande Industries
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
using System.Reflection;

namespace com.vanderlande.wpf
{
    public class CopyrightInfo : ViewModel
    {
        private string _product;
        public string Product
        {
            get { return _product; }
            private set { ChangeProperty(ref _product, value); }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set { ChangeProperty(ref _version, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { ChangeProperty(ref _description, value); }
        }
        
        private string _copyright;
        public string Copyright
        {
            get { return _copyright; }
            set { ChangeProperty(ref _copyright, value); }
        }

        private string _trademarks;
        public string Trademarks
        {
            get { return _trademarks; }
            set { ChangeProperty(ref _trademarks, value); }
        }

        public CopyrightInfo(string prod)
        {
            Product = prod;
        }

        public CopyrightInfo(string prod, Assembly assembly) :
            this(prod)
        {
            FileVersionInfo vi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Copyright = vi.LegalCopyright;
            if (string.IsNullOrEmpty(Copyright))
                Copyright = vi.CompanyName;
            Version = vi.FileVersion;
            if (string.IsNullOrEmpty(Version))
                Version = vi.ProductVersion;
            Trademarks = vi.LegalTrademarks;
            Description = vi.Comments;
            if (string.IsNullOrEmpty(Description))
                Description = vi.FileDescription;
        }

        public CopyrightInfo(Assembly assembly) :
            this(FileVersionInfo.GetVersionInfo(assembly.Location).ProductName, assembly)
        {}


        public void Add()
        {
            CopyrightSettingsViewModel.Add(this);
        }
    }
}
