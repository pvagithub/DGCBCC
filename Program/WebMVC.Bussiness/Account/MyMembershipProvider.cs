 
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using WebMVC.Framework.Utilities;
using WebMVC.Entities;
using WebMVC.Dal;

namespace WebMVC.Bussiness
{
    public class MyMembershipProvider : MembershipProvider
    {
        private MyRepository repository;

        public int MinPasswordLength
        {
            get
            {
                return 5;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return MinPasswordLength;
            }
        }

        public MyMembershipProvider()
        {
            this.repository = new MyRepository();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(password.Trim()) || string.IsNullOrEmpty(username.Trim()))
                return false;

            string hash = TextUtility.GetMd5Hash(password.Trim());

            return this.repository.GetAllUsers().Any(user => (user.Username == username.Trim()) && (user.Password == hash) && (user.IsLockedOut == false || user.IsLockedOut == null));
        }

        //public void CreateUser(string username, string fullName, string password, string email, string roleName)
        //{
        //    this.repository.CreateUser(username, fullName, password, email, roleName);
        //}

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!ValidateUser(username, oldPassword) || string.IsNullOrEmpty(newPassword.Trim()))
                return false;

            WebMVC.Entities.Membership user = repository.GetUser(username);
            string hash = TextUtility.GetMd5Hash(newPassword.Trim());

            user.Password = hash;
            repository.Save();

            return true;
        }

        #region Not Implemented MembershipProvider Methods

        #region Properties

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property is not implemented.
        /// </summary>
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        #endregion Properties

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public MembershipCreateStatus CreateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            var user = GetUserByUserName(userName);

            if (user == null)
            {
                WebMVC.Entities.Membership userObj = new WebMVC.Entities.Membership();

                userObj.Username = userName;
                userObj.Password = TextUtility.GetMd5Hash(password);

                using (var usersContext = new DataModelEntities())
                {
                    usersContext.Memberships.Add(userObj);
                    usersContext.SaveChanges();
                }

                return MembershipCreateStatus.Success;
            }
            return MembershipCreateStatus.DuplicateUserName;
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public WebMVC.Entities.Membership GetUserByUserName(string username)
        {
            var db = new DataModelEntities();
            return db.Memberships.Where(x => x.Username == username).FirstOrDefault();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function is not implemented.
        /// </summary>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not implemented.
        /// </summary>
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

       

        #endregion Not Implemented MembershipProvider Methods


        public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                base.OnAuthorization(filterContext);
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/Account/Login");
                    return;
                }

                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectResult("~/Account/AccessDenied");
                }
            }
        }
    }
}