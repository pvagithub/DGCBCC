using System.Web.Mvc;
using System.Web.Routing;

namespace CBCC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DanhGia", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "CBCC.Controllers" }

            );
            routes.MapRoute(
                name: "GopY",
                url: "{controller}/{action}/{sbn}",
                defaults: new { controller = "GopY", action = "Index", sbn = UrlParameter.Optional },
              namespaces: new[] { "CBCC.Controllers" }

            );
        }
    }
}
