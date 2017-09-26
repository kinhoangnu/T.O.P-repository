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
namespace com.vanderlande.wpf
{
    /// <summary>
    /// Determine if the current user is a developer as stated in the list.
    /// </summary>
    internal class UserRoleFromDevelopersFile : UserRoleFactory
    {
        public override UserRole Create()
        {
            Developers devs = new Developers();
            if ((!devs.Load()) ||
                (!devs.ContainsCurrentUser))
            {
                return base.Create();
            }
            Logger.LogLine("User role selected from the Developers file.");
            UserRole retval = new UserRole { HighestRole = UserRole.Developer };
            return retval;
        }
    }
}
