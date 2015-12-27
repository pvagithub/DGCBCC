using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CBCC.Account
{
    public static class ProjectRoles
    {
        //now constants for the attribute values
        public const string Director = "Director";

        public const string Manager = "Manager";
        public const string Staff = "Staff";
        public const string Stationery = "Stationery"; //van phong pham
        public const string Asset = "Asset"; //tai san
        public const string Document = "Document"; //van ban
        public const string HCNSNS = "HCNSNS"; //nhan su
        public const string Recruitment = "Recruitment"; //Tuyen dung
        public const string News = "News"; //van hoa cong ty
        public const string DocumentType = "DocumentType"; //Loai van ban
    }

    public static class IPrincipalExtend
    {
        public static bool HasAnyRole(this IPrincipal user, params string[] roles)
        {
            return roles.Any(user.IsInRole);
        }

        public static string[] Roles(this IPrincipal user)
        {
            string[] rolesArray;
            RolePrincipal r = (RolePrincipal)user;
            rolesArray = r.GetRoles();

            return rolesArray;
        }
    }

    public class CustomAuthorize : AuthorizeAttribute
    {
        //Property to allow array instead of single string.
        private string[] _authorizedRoles;

        public string[] AuthorizedRoles
        {
            get { return _authorizedRoles ?? new string[0]; }
            set { _authorizedRoles = value; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //If its an unauthorized/timed out ajax request go to top window and redirect to logon.
            if (filterContext.Result is HttpUnauthorizedResult && filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new JavaScriptResult() { Script = "top.location = '/Account/LogOn?Expired=1'" };

            //If authorization results in HttpUnauthorizedResult, redirect to error page instead of Logon page.
            if (filterContext.Result is HttpUnauthorizedResult)
                filterContext.Result = new RedirectResult("~/Account/Login");
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            //Bypass role check if user is Admin, prevents having to add Admin role across the whole project.
            if (httpContext.User.IsInRole("Admin"))
                return true;

            //If no roles are supplied to the attribute just check that the user is logged in.
            if (AuthorizedRoles.Length == 0)
                return true;

            //Check to see if any of the authorized roles fits into any assigned roles only if roles have been supplied.
            if (AuthorizedRoles.Any(httpContext.User.IsInRole))
                return true;

            return false;
        }
    }
}