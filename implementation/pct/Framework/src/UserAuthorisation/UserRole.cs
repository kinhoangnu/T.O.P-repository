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

namespace com.vanderlande.wpf
{
    /// <summary>
    /// User role.
    /// </summary>
    public class UserRole
    {
        private static readonly List<string> _roles = new List<string>();

        public const string Developer = "Developer";    // Highest available user role.
        public const string Guest = "Guest";            // Lowest available user role.

        public string[] AvailableRoles
        {
            get
            {
                AddGuest();
                return _roles.GetRange(_highest, _roles.Count - _highest).ToArray();
            }
        }


        private int _highest = 0;
        public string HighestRole
        {
            get
            {
                AddGuest();
                return _roles[_highest];
            }
            set
            {
                AddGuest();
                int idx = _roles.IndexOf(value);
                if (idx != -1)
                {
                    _highest = idx;
                }
            }
        }


        private int _current = 0;
        public string CurrentRole
        {
            get
            {
                AddGuest();
                return _roles[_current];
            }
            set
            {
                AddGuest();
                int idx = _roles.IndexOf(value);
                if (idx == -1)
                {
                    Logger.LogError(string.Format("Unknown user role [{0}] is ignored.", value));
                }
                if (idx < _highest)
                {
                    Logger.LogError(string.Format("User role [{0}] higher than allowed [{1}], fall back to [{1}].", value, HighestRole));
                    idx = _highest;
                }
                _current = idx;
            }
        }


        static UserRole()
        {
            UserRole.AddRole(UserRole.Developer);
        }


        public UserRole()
        {
        }

        /// <summary>
        /// Add a domain user role, other than Developer or Guest
        /// </summary>
        public static void AddRole(string role)
        {
            _roles.Add(role);
        }

        /// <summary>
        /// Compare two user roles.
        /// </summary>
        /// <returns>
        /// -1 if role 1 has a lower priority than role 2
        /// 0 if roles are equal or one of them is unknown.
        /// 1 if role 1 has a higher priority than role 2.
        /// </returns>
        public int Compare(string role1, string role2)
        {
            int index1 = _roles.IndexOf(role1);
            int index2 = _roles.IndexOf(role2);
            if ((index2 == -1) || (index2 == -1))
            {
                return 0;
            }
            if (index1 < index2)
            {
                return 1;
            }
            if (index1 > index2)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// If the lowest user role (Guest) has not yet been added, add it now.
        /// </summary>
        private void AddGuest()
        {
            if (!_roles.Contains(Guest))
            {
                AddRole(Guest);
            }
        }
    }
}
