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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace com.vanderlande.wpf
{
    public class InitOnAssemblyLoad : Attribute
    {}

    /// <summary>
    /// Class to Initialize Assemblies.
    /// A class that wants to initialize something at load time has to use the [InitOnAssemblyLoad] attribute
    /// and supply the private static void InitAssembly() method.
    /// </summary>
    public class InitAssemblies
    {
        private readonly List<string> _assemblies;

        /// <summary>
        /// Process every delay loaded assembly and already loaded assemblies 
        /// </summary>
        internal InitAssemblies()
        {
            _assemblies = new List<string>();
            AppDomain.CurrentDomain.AssemblyLoad += (s, o) => InitAssembly(o.LoadedAssembly);
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                InitAssembly(assembly);
            }
        }


        public static void CopyLibraryFromResource(string dll, Assembly assem)
        {
            string fname = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (fname != null)
            {
                fname = Path.Combine(fname, dll);
            }
            if (File.Exists(fname))
            {
                return;
            }

            string name = assem.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith("." + dll));
            if (name == null)
            {
                Logger.LogError(string.Format("Error in {0}: Missing embedded resource {1} in {2} to create {3}", MethodBase.GetCurrentMethod().DeclaringType, dll, assem.FullName, fname));
                return;
            }

            using (Stream src = assem.GetManifestResourceStream(name))
            {
                if (src == null)
                {
                    Logger.LogError(string.Format("Error in {0}: Cannot read embedded resource {1} from {2}", MethodBase.GetCurrentMethod().DeclaringType, name, assem.FullName));
                    return;
                }
                using (Stream dest = new FileStream(fname, FileMode.Create))
                {
                    src.CopyTo(dest);
                }
            }
        }


        private void InitAssembly(Assembly assembly)
        {
            if (_assemblies.Contains(assembly.FullName))
            {
                return;
            }
            _assemblies.Add(assembly.FullName);
            foreach (Type type in GetInitTypes(assembly))
            {
                InitTypes(type);
            }
        }


        private static IEnumerable<Type> GetInitTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(InitOnAssemblyLoad), true).Length > 0);
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("Exception in {0}: {1}", MethodBase.GetCurrentMethod().DeclaringType, ex.Message));
            }
            return new List<Type>();
        }


        private void InitTypes(Type type)
        {
            try
            {
                MethodInfo mi = type.GetMethod("InitAssembly", BindingFlags.NonPublic | BindingFlags.Static);
                if (mi != null)
                {
                    mi.Invoke(null, null);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(string.Format("Exception in {0}: {1} on {2}", MethodBase.GetCurrentMethod().DeclaringType, ex.Message, type.FullName));
            }
        }
    
    }
}
