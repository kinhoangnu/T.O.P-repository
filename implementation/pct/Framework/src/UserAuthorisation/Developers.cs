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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;

namespace com.vanderlande.wpf
{
    // Class that contains the Developer user names,as saved in 
    // <YourRunningApplication>/Resources/Developers.xml
    internal class Developers
    {
        private XDocument _xml;
        private readonly List<string> Names = new List<string>();
        
        internal bool ContainsCurrentUser
        {
            get
            {
                return Names.Any(name => name == Environment.UserName);
            }
        }

        internal void Clear()
        {
            Names.Clear();
        }


        internal void Add(string name)
        {
            if (Names.Any(x => x == name) == false)
            {
                Names.Add(name);
            }   
        }


        internal void Remove(string name)
        {
            if (Names.Any(x => x == name) == true)
            {
                Names.Remove(name);
            }
        }


        internal bool Import()
        {
            string fname = GetFileName();
            if (fname == null)
            {
                return false;
            }
            LoadEncrypted(fname);
            LoadFromXML();
            return true;
        }


        internal bool Export()
        {
            string fname = GetFileName();
            if (fname == null)
            {
                return false;
            }
            return Save(fname);
        }


        internal bool Save(string fname)
        {
            try
            {
                if (File.Exists(fname) == true)
                {
                    File.Delete(fname);
                }
                SaveToXML();
                SaveEncrypted(fname);
                return true;

            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
            return false;
        }


        private void LoadFromXML()
        {
            Names.Clear();
            XElement root = _xml.Element("Developers");
            if (root == null)
            {
                return;
            }
            foreach (XElement node in root.Elements("Name"))
            {
                Names.Add(node.Value);                    
            }
        }


        private void SaveToXML()
        {
            _xml = XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?><Developers></Developers>");
            XElement root = _xml.Element("Developers");
            Debug.Assert(root != null);
            foreach (string name in Names)
            {
                root.Add(new XElement("Name", name));
            }
        }


        private static string GetFileName()
        {
            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            Debug.Assert(folder != null);
            string result = Path.Combine(folder, "Resources", "Developers.xml");
            if (File.Exists(result))
            {
                return result;
            }
                        // When the file is not found in the <exe>/Resources, check the application folder.
            folder = ViApplication.Instance.ApplicationDataFolder;
            result = Path.Combine(folder, "Developers.xml");
            if (File.Exists(result))
            {
                return result;
            }

            return null;
        }


        private void LoadEncrypted(string fname)
        {
            using (Rijndael RijndaelAlg = Rijndael.Create())
            {
                using (FileStream readStream = File.OpenRead(fname))
                {
                    byte[] key = GetKey("Developers Persistency XML");
                    byte[] iv = GetKey( ViApplication.Instance.Project);
                    using (CryptoStream cStream = new CryptoStream(readStream,
                        RijndaelAlg.CreateDecryptor(key, iv),
                        CryptoStreamMode.Read))
                    {
                        using (XmlTextReader reader = new XmlTextReader(cStream))
                        {
                            _xml = XDocument.Load(reader);
                        }
                    }
                }
            }
        }


        private void SaveEncrypted(string fname)
        {
            using (Rijndael RijndaelAlg = Rijndael.Create())
            {
                using (FileStream writeStream = File.Open(fname, FileMode.Create))
                {
                    byte[] key = GetKey("Developers Persistency XML");
                    byte[] iv = GetKey(ViApplication.Instance.Project);
                    using (CryptoStream cStream = new CryptoStream(writeStream,
                        RijndaelAlg.CreateEncryptor(key, iv),
                        CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cStream))
                        {
                            _xml.Save(writer);
                        }
                    }
                }
            }
        }


        // Poor encryption, but it serves the main purpose; make the Developers file unreadable to prevent manual modification.
        public static byte[] GetKey(string key)
        {
            byte[] _salt = { 0x01, 0x02, 0x03, 0x01, 0x02, 0x03, 0x01, 0x02, 0x03, 0x01, 0x02, 0x03 };
            string key16 = key + "ABCDEFGHIJKLMNOP";
            key16 = key16.Substring(0, 16);
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(key16, _salt);
            return deriveBytes.GetBytes(16);
        }


    }
}
