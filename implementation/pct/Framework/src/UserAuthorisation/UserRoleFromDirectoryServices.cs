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
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Determine the highest role using the Directory Services.
    /// </summary>
    internal class UserRoleFromDirectoryServices : UserRoleFactory
    {
        public override UserRole Create()
        {
            List<string> groups = FindAllGroups();
            UserRole retval = new UserRole { HighestRole = UserRole.Developer };
            foreach (string role in retval.AvailableRoles)
            {
                if (role == UserRole.Developer)                     // Do not check for a Project/Developer group.
                {
                    continue;
                }
                string grp = FindGroup(groups, role.ToString().ToUpper());
                if (grp == null)
                {
                    continue;
                }
                retval.HighestRole = role;
                Logger.LogLine(string.Format("User role {0} selected from directory service.", grp));
                return retval;
            }
            return base.Create();
        }

        /// <summary>
        /// Find all groups the user belongs to.
        /// </summary>
        /// <returns></returns>
        private List<string> FindAllGroups()
        {
            List<string> result = new List<string>();
            try
            {
                // establish domain context
                PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

                // find your user
                UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, User.Current.Name);

                // if found - grab its groups
                if (user == null)
                {
                    Logger.LogLine(string.Format("Could not obtain UserPrinciple for {0}", User.Current.Name));
                }
                else
                {
                    PrincipalSearchResult<Principal> groups = user.GetGroups();
                    foreach (GroupPrincipal p in groups.OfType<GroupPrincipal>())
                    {
                        result.Add(p.Name.ToUpper());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogLine(ex.Message);
            }
            return result;
        }


        /// <summary>
        /// Find the group that contains the project name and a user role name.
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private static string FindGroup(IEnumerable<string> groups, string role)
        {
            string project = ViApplication.Instance.Project.ToUpper();
            return groups.FirstOrDefault(grp => (grp.Contains(project)) && (grp.Contains(role)));
        }
    }
}


