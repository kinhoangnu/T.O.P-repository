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
using System.Security.Principal;
using System.Threading;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Determine highest role using Role-Based security (using the Windows Principal object)
    ///                            (code has never been tested)
    /// </summary>
    internal class UserRoleFromRoleBasedSecurity : UserRoleFactory
    {
        public override UserRole Create()
        {
            IPrincipal prin = Thread.CurrentPrincipal;
            if (prin == null)
            {
                return base.Create();
            }
            UserRole retval = new UserRole { HighestRole = UserRole.Developer };
            foreach (string role in retval.AvailableRoles)
            {
                if (role == UserRole.Developer)                     // Do not check for a Project/Developer group.
                {
                    continue;
                }

                try
                {
                    string name = ViApplication.Instance.Project + " " + role;
                    if (prin.IsInRole(name))
                    {
                        retval.HighestRole = role;
                        Logger.LogLine(string.Format("User role selected from Role Base {0}", prin.Identity.Name));
                        return retval;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogLine(ex.Message);
                }
            }
            return base.Create();
        }
    }
}

