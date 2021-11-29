using System.Web;
using System.Web.Optimization;

namespace KermesseApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new Bundle("~/Scripts/js").Include(
                "~/Scripts/data-table.js",
                "~/Scripts/calendar.js",
                "~/Scripts/jq.tablesort.js",
                "~/Scripts/profile-demo.js",
                "~/Scripts/paginate.js",
                "~/Scripts/form-validation.js",
                "~/Scripts/form-repeater.js",
                "~/Scripts/dashboard.js",
                "~/Scripts/off-canvas.js",
                "~/Scripts/bootstrap-table.js",
                "~/Scripts/dataTables.bootstrap4.js",
                "~/Scripts/jquery.dataTables.js",
                "~/Scripts/dataTables.select.min.js",
                "~/Scripts/template.js",
                "~/Scripts/vendor.bundle.base.js"));

            bundles.Add(new Bundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/style.css",
                      "~/Content/vendor.bundle.base.css",
                      "~/Content/feather.css"));

        }
    }
}
