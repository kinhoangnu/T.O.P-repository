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
    /// When no user role has been determined, the user is a guest (lowest available role).
    /// </summary>
    internal class UserRoleLowestAvailable : UserRoleFactory
    {
        public override UserRole Create()
        {
            return new UserRole { HighestRole = UserRole.Guest, CurrentRole = UserRole.Guest };
        }
    }
}

