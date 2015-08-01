using System.Web;
using System.Web.Optimization;
namespace Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Assets/BootStrap/css").Include("~/Assets/BootStrap/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Assets/FlatUI/css").Include("~/Assets/FlatUI/css/flat-ui.min.css"));
            bundles.Add(new ScriptBundle("~/Assets/JQuery").Include("~/Assets/js/jquery-1.11.3.min.js"));
            bundles.Add(new ScriptBundle("~/Assets/BootStrap/js").Include("~/Assets/BootStrap/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/Assets/BootStrap/js").Include("~/Assets/FlatUI/js/flat-ui.min.js"));

            bundles.Add(new StyleBundle("~/Assets/css").Include(
                "~/Assets/BootStrap/css/bootstrap.min.css",
                "~/Assets/FlatUI/css/flat-ui.min.css"));
            bundles.Add(new ScriptBundle("~/Assets/js").Include(
                "~/Assets/js/jquery-1.11.3.min.js",
                "~/Assets/BootStrap/js/bootstrap.min.js",
                "~/Assets/FlatUI/js/flat-ui.min.js"));
        }
    }
}