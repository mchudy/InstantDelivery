using System.Web.Optimization;

namespace InstantDelivery.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/yeti.bootstrap.css",
                      "~/Content/site.css", "~/Content/font-awesome.css", "~/Content/timeline.css", "~/Content/angular-chart.css"));

            bundles.Add(new ScriptBundle("~/bundles/portal")
                .Include("~/Scripts/angular.js", "~/Scripts/angular-route.js", "~/Scripts/angular-messages.js",
                        "~/Scripts/angular-local-storage.js", "~/Scripts/angular-animate.js", "~/Scripts/loading-bar.js",
                        "~/Scripts/dirPagination.js", "~/Scripts/Chart.js", "~/Scripts/angular-chart.js")
                .Include("~/App/app.js")
                .IncludeDirectory("~/App/services", "*.js", true)
                .IncludeDirectory("~/App/directives", "*.js", true)
                .IncludeDirectory("~/App/controllers", "*.js", true));
        }
    }
}
