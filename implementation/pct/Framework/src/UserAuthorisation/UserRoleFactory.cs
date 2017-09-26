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
    /// User role factory: singleton chain of responsibilities.
    /// </summary>
    public class UserRoleFactory
    {
        private static UserRoleFactory _head = null;
        private readonly UserRoleFactory _previous = null;

        internal static UserRoleFactory Instance
        {
            get { return _head; }    
        }

        /// <summary>
        /// Add standard supplied user role factories
        /// </summary>
        static UserRoleFactory()
        {
            new UserRoleLowestAvailable();
            new UserRoleFromRoleBasedSecurity();
            new UserRoleFromDirectoryServices();
            new UserRoleFromDevelopersFile();
        }

        protected UserRoleFactory()
        {
            _previous = _head;
            _head = this;
        }

        public virtual UserRole Create()
        {
            return (_previous == null) ? null : _previous.Create();
        }
    }
}
