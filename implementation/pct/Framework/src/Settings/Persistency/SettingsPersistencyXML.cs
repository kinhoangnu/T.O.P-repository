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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace com.vanderlande.wpf
{
    public class SettingsPersistencyXML : ISettingsPersistency
    {
        private static readonly bool _encryption;       // Save the settings encrypted?
        private static readonly string _fileName;       // Where to save the settings.           
        private XDocument _xml = null;
        private XElement _node = null;

        static SettingsPersistencyXML()
        {
            _encryption = true;
            _fileName = GetFileName();
        }


        public SettingsPersistencyXML()
        {
            try
            {
                if (File.Exists(_fileName) == true)
                {
                    Load();
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
            
            if (_xml == null)               // No file or could not read it.
            {
                _xml = XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?><Settings></Settings>");
            }
            _node = _xml.Element("Settings");
        }


        // Saving the data could fail when multiple instances of the same application are closed
        // as a group. Then they all try to write the same file.
        // Just retry the save several times with a short delay.
        // If it still fails, something else is wrong and an exception is generated.
        public void Dispose()
        {
            const int retry = 5;
            for (int i = 1; i <= retry; ++i)
            {
                try
                {
                    Save();
                    break;
                }
                catch (Exception)
                {
                    if (i == retry)
                    {
                        throw;
                    }
                    Thread.Sleep(100);
                }
            }
        }


        public void PushGroup(string name)
        {
            if ((_node.HasElements == false) || (_node.Elements(name).Any() == false))
            {
                _node.Add(new XElement(name));
            }
            _node = _node.Element(name);
        }


        public void PopGroup()
        {
            _node = _node.Parent;
        }


        public bool Read(SettingsPageViewModel vm)
        {
            vm.Read(this);
            return true;
        }


        public bool Write(SettingsPageViewModel vm)
        {
            vm.Write(this);
            return true;
        }


        public bool Read<T>(string key, out T value, T defaultValue = default(T))
        {
            value = defaultValue;
            string tmp;
            if (ReadValue(key, out tmp) == false)
            {
                return false;
            }
            if (typeof(T).IsEnum == true)
            {
                value = (T)Enum.Parse(typeof(T), tmp, true);
            }
            else
            {
                value = (T)Convert.ChangeType(tmp, typeof(T));
            }
            return true;
        }


        public bool Write<T>(string key, T value)
        {
            return WriteValue(key, value.ToString());
        }


        public bool ReadList<T>(string key, out List<T> value)
        {
            value = new List<T>();
            if ((_node.HasElements == false) || (_node.Elements(key).Any() == false))
            {
                return false;
            }
            foreach (XElement elem in _node.Elements(key))
            {
                if (typeof(T).IsEnum == true)
                {
                    value.Add((T)Enum.Parse(typeof(T), elem.Value, true));
                }
                else
                {
                    value.Add((T)Convert.ChangeType(elem.Value, typeof(T)));
                }
            }
            return true;
        }


        public bool WriteList<T>(string key, IEnumerable<T> list)
        {
            if (_node.HasElements == true) 
            {
                _node.Elements(key).Remove();
            }
            foreach (T value in list)
            {
                _node.Add(new XElement(key, value));
            }
            return true;
        }


        private bool ReadValue(string key, out string value, string defaultValue = null)
        {
            value = defaultValue ?? string.Empty;
            if ((_node.HasElements == false) || (_node.Elements(key).Any() == false))
            {
                return false;
            }
            XElement elem = _node.Elements(key).FirstOrDefault();
            if (elem == null)
            {
                return false;
            }
            value = elem.Value;
            return true;
        }


        private bool WriteValue(string key, string value)
        {
            if ((_node.HasElements == false) || (_node.Elements(key).Any() == false))
            {
                _node.Add(new XElement(key, value));
            }
            else
            {
                _node.Element(key).Value = value;
            }
            return true;
        }
        

        private static string GetFileName()
        {
            string folder = ViApplication.Instance.ApplicationDataFolder;
            return Path.Combine(folder, ViApplication.Instance.Name + "_" + Environment.UserName + "_Settings.xml");
        }


        private void Load()
        {
            if (_encryption == true)
            {
                LoadEncrypted();
            }
            else
            {
                _xml = XDocument.Load(_fileName);
            }
        }


        private void Save()
        {
            if (_encryption == true)
            {
                SaveEncrypted();
            }
            else
            {
                _xml.Save(_fileName);
            }
        }


        private void LoadEncrypted()
        {
            using (Rijndael RijndaelAlg = Rijndael.Create())
            {
                using (FileStream readStream = File.OpenRead(_fileName))
                {
                    byte[] key = GetKey("Settings Persistency XML");
                    byte[] iv = GetKey(Environment.UserName);
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


        private void SaveEncrypted()
        {
            using (Rijndael RijndaelAlg = Rijndael.Create())
            {
                using (FileStream writeStream = File.Open(_fileName, FileMode.Create))
                {
                    byte[] key = GetKey("Settings Persistency XML");
                    byte[] iv = GetKey(Environment.UserName);
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


        // Poor encryption, but it serves the main purpose; make the settings file unreadable to prevent manual modification.
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

