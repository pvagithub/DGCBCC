using System.Web.Optimization;

namespace CBCC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/hightcharts").Include(
                        "~/Scripts/jquery-{version}.js",
                         "~/Scripts/highcharts.js",
                        "~/Scripts/highcharts-3d.js",
                        "~/Scripts/exporting.js",
                        "~/Scripts/bootstrap-datepicker.js"



                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.dotdotdot.js",
                        "~/Scripts/drilldown.js",
                        "~/Scripts/jquery.metisMenu.js",
                        "~/Scripts/app_scripts.js",
                        "~/Scripts/jquery.dataTables.js",
                        "~/Scripts/dataTables.bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/SPA/scripts/angular.min.js",
                        "~/SPA/scripts/angular-ui/angular-ui-router.js",
                        "~/SPA/scripts/angular-route.min.js",
                        "~/SPA/scripts/angular-resource.min.js",
                       "~/SPA/scripts/angular-ui/ui-bootstrap-tpls.min.js",
                       "~/SPA/js/app.js",
                       "~/SPA/js/helperService.js",
                       "~/SPA/js/quizCtrl.js",
                       "~/Scripts/perfect-scrollbar/js/perfect-scrollbar.min.js",
                         "~/Scripts/vki/keyboard.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/dropdown-menu.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/dataTables.bootstrap.css",
                      "~/Content/datepicker.css",
                      "~/Scripts/perfect-scrollbar/css/perfect-scrollbar.css",
                      "~/Scripts/vki/keyboard.css"

                      ));

            //BundleTable.EnableOptimizations = true;
        }
    }
}