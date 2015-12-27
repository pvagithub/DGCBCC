 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using WebMVC.Entities;

namespace WebMVC.Bussiness
{
    public class MyRoleProvider : RoleProvider
    {
        #region Variables

        private MyRepository repository;

        #endregion Variables

        #region Constructors

        public MyRoleProvider()
        {
            this.repository = new MyRepository();
        }

        #endregion Constructors

        #region Implemented RoleProvider Methods

        public override bool IsUserInRole(string userName, string roleName)
        {
            WebMVC.Entities.Membership user = repository.GetUser(userName);
            Role role = repository.GetRole(roleName);

            if (!repository.UserExists(user))
                return false;
            //if (!repository.RoleExists(role))
            //    return false;

            return true;
            //Var
            //return user.UsersInRoles.Name == role.Name;
        }

        public override string[] GetRolesForUser(string userName)
        {
            if (userName != "administrator")
            {
                List<Role> role = this.repository.GetRoleForUser(userName);
                var lstRole = role.Select(m => m.RoleName);
                if (!this.repository.RoleExists(role))
                    return new string[] { string.Empty };

                return (String[])lstRole.ToArray();
            }
            else
            {
                String[] items = new string[] { "Admin" };
                return items;
            }
        }

        #endregion Implemented RoleProvider Methods

        #region Not Implemented RoleProvider Methods

        #region Properties

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion Properties

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion Not Implemented RoleProvider Methods
    }
}