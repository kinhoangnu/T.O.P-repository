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

namespace com.vanderlande.wpf
{
    public class User
    {
        public static User Current { get; private set; }

        public string Name { get; private set; }

        public UserRole Role { get; private set; }

        static User()
        {
            Current = new User();
        }


        private User()
        {
            Name = Environment.UserName;
            Role = UserRoleFactory.Instance.Create();
            Role.CurrentRole = Role.HighestRole;
        }

    }
}


