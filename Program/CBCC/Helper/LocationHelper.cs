using System;
using System.Web.Mvc;

namespace CBCC.Helpers
{
    public class LocationHelper
    {
        /// <summary>
        /// Kiem tra view dang o controller va action nao 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="viewContext"></param>
        /// <returns></returns>
        public static bool IsCurrentControllerAndAction(string controllerName, string actionName, ViewContext viewContext)
        {
            bool result = false;
            string normalizedControllerName = controllerName.EndsWith("Controller") ? controllerName : String.Format("{0}Controller", controllerName);

            if (viewContext == null) return false;
            if (String.IsNullOrEmpty(actionName)) return false;

            if (viewContext.Controller.GetType().Name.Equals(normalizedControllerName, StringComparison.InvariantCultureIgnoreCase) &&
                viewContext.Controller.ValueProvider.GetValue("action").AttemptedValue.Equals(actionName, StringComparison.InvariantCultureIgnoreCase))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Get action name cua view hien tai
        /// </summary>
        /// <param name="viewContext"></param>
        /// <returns></returns>
        public static string GetAction(ViewContext viewContext)
        {
            string result = "";
            result = viewContext.Controller.ValueProvider.GetValue("action").AttemptedValue.ToLower();
            return result;
        }
    }
}