using System.Web;
using System.Web.Optimization;

namespace GigHub
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                  "~/Scripts/app/Services/AttendanceServices.js",
                  "~/Scripts/app/Services/FollowingServices.js",
                  "~/Scripts/app/Controllers/GigDetailsController.js",
                  "~/Scripts/app/Controllers/GigsController.js",
                  "~/Scripts/app/app.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/underscore.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/bootbox.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

         
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

           

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/animate.css"
                      
                      ));
            

        }
    }
}
