/*
*  Copyright (c) 2017 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*/
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace com.vanderlande.wpf
{

    /// <summary>
    /// Class to create a View, based on an existing ViewModel.
    /// The view class name is identical to the ViewModel name, where only the endings differ.
    /// It uses user roles to determine special views (descending privileges) by prefixing the View ending by the role name.
    /// </summary>
    public class ViewLocator
    {
        #region Fields

        private static string[] _viewModelEndings =
        {
            "ViewModel"
        };

        private static bool _viewFound;


        #endregion
        
        /// <summary>
        /// Check if a view exists for this viewmodel.
        /// </summary>
        /// <param name="vm">The viewmodel</param>
        /// <param name="assembly">Optionally a different assembly where to search in.</param>
        /// <returns>True when a view exists</returns>
        public static bool DoesViewExist(ViewModel vm, Assembly assembly = null)
        {
            return (FindView(vm.GetType(), assembly) != null);
        }


        public static bool DoesViewExist(Type vmType, Assembly assembly = null)
        {
            return (FindView(vmType, assembly) != null);
        }


        /// <summary>
        /// Create a view (FrameworkElement) for this viewmodel.
        /// The datacontext is set to the supplied viewmodel.
        public static FrameworkElement CreateView(ContentViewModel cvm, Assembly assembly = null)
        {
            FrameworkElement fe = CreateView(cvm as ViewModel, assembly);
            if (fe != null)
            {
                cvm.Attach(fe);
            }
            return fe;
        }

        /// <summary>
        /// Create a view (FrameworkElement) for this viewmodel.
        /// The datacontext is set to the supplied viewmodel.
        /// </summary>
        /// <param name="cvm">The viewmodel</param>
        /// <param name="assembly">Optionally a different assembly where to search in.</param>
        /// <returns>The frameworkelement (the view)</returns>
        public static FrameworkElement CreateView(ViewModel cvm, Assembly assembly = null)
        {
            Type vmType = cvm.GetType();
            Type viewType = FindView(vmType, assembly);
            if (viewType == null)
            {
                string msg = string.Format("None of the assemblies contain a view for {0}", vmType.FullName);
                Logger.Assert(false, msg, "VI_WPF.ViewLocator.FindView");
                throw new Exception(msg);
            }

            FrameworkElement result = Activator.CreateInstance(viewType) as FrameworkElement;
            Debug.Assert(result != null);
            return result;
        }


        internal static string GetViewModelBaseName(string typeName)
        {
            foreach (string s in _viewModelEndings)
            {
                if (typeName.EndsWith(s, true, null))       // Case insensitive compare.
                {
                    return typeName.Substring(0, typeName.Length - s.Length);
                }
                int idx = typeName.IndexOf(s + "`", StringComparison.CurrentCulture);
                if (idx > 0)
                {
                    return typeName.Substring(0, idx);
                }
            }
            return null;
        }


        #region Private methods

        private static Type FindView(Type vmType, Assembly assembly)
        {
            _viewFound = false;
            Type viewType = null;
            for (Type t = vmType; (viewType == null) && (t != typeof(ViewModel)); t = t.BaseType)
            {
                string basename = GetViewModelBaseName(t.FullName);
                // First try the supplied/same assembly.
                viewType = GetViewType(basename, assembly ?? t.Assembly);
                if (viewType == null)               // If not found, search in all assemblies.       
                {
                    foreach (Assembly assem in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        viewType = GetViewType(basename, assem);
                        if (viewType != null)
                        {
                            break;
                        }
                    }
                }
            }
            return (_viewFound == false) ? null : viewType;
        }


        private static Type GetViewType(string basename, Assembly assembly)
        {
            string name;
            bool allowed = false;
            Type type = null;
            foreach (string role in User.Current.Role.AvailableRoles)
            {                                                           // Loop through all available user roles
                allowed |= (User.Current.Role.CurrentRole == role);
                name = basename + role + "View";                        // Role and 'View' ending is case sensitive.
                type = assembly.GetType(name);
                if (type == null)
                {
                    continue;
                }
                _viewFound = true;
                if (allowed)
                {
                    return type;
                }
            }
            Trace.Assert(allowed == true);                              // At least one role must have been found
            name = basename + "View";                                   // By default, ignore the role and search for the view
            type = assembly.GetType(name);
            _viewFound |= (type != null);
            return type;
        }

        #endregion

    }

}
