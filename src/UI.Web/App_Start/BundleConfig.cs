using System.Web.Optimization;

namespace UI.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Virtual path of bundle cannot point to actual folder:
            // http://stackoverflow.com/q/13673759/426840
            // http://forums.asp.net/post/5012037.aspx

            // Base Admin styles
            bundles.Add(
                new StyleBundle("~/Bundle/Themes/Default/Css")
                .Include(
                    "~/Themes/Default/Css/Libraries/Bootstrap/bootstrap.min.css",
                    "~/Themes/Default/Css/Libraries/Select2/select2.css",
                    "~/Themes/Default/Css/Libraries/FontAwesome/font-awesome.min.css",
                    "~/Themes/Default/Css/Libraries/SlickGrid/slick.grid.css",
                    "~/Themes/Default/Css/Libraries/JqGrid/ui.jqgrid.css",
                    "~/Themes/Default/Css/slick.grid.custom.css",
                    "~/Themes/Default/Css/main.css",
                    "~/Themes/Default/Css/Libraries/Bootstrap/bootstrap-responsive.min.css" 
                    // Responsive css should be after main.css, because of the padding-top on the body set in main.css
                    //http://twitter.github.io/bootstrap/components.html#navbar
                )
            );

            // Base Admin scripts
            bundles.Add(
                new ScriptBundle("~/Bundle/Themes/Default/Js")
                .Include(
                    "~/Themes/Default/Js/dummyConsole.js",
                    // http://stackoverflow.com/questions/14402741/jquery-1-9-0-and-modernizr-cannot-be-minified-with-the-asp-net-web-optimization
                    // Removed the comment from jquery-1.9.0.min.js to fix the problem
                    "~/Themes/Default/Js/Libraries/Jquery/jquery-1.9.0.min.js", 
                    "~/Themes/Default/Js/Libraries/Bootstrap/bootstrap.min.js",
                    "~/Themes/Default/Js/Libraries/Select2/select2.min.js",
                    "~/Themes/Default/Js/Libraries/Jquery.Form/jquery.form.js",
                    "~/Themes/Default/Js/Libraries/Jquery.Event.Drag/jquery.Event.Drag-2.2.js",
                    "~/Themes/Default/Js/Libraries/Jquery.Validate/jquery.validate.min.js",
                    "~/Themes/Default/Js/Libraries/Jquery.Validate/jquery.validate.unobtrusive.min.js",
                    "~/Themes/Default/Js/Libraries/SlickGrid/slick.core.js",
                    "~/Themes/Default/Js/Libraries/SlickGrid/slick.grid.js",
                    "~/Themes/Default/Js/Libraries/JqGrid/jquery-ui-1.10.1.min,js",
                    "~/Themes/Default/Js/Libraries/JqGrid/i18n/grid.locale-nl.js",
                    "~/Themes/Default/Js/Libraries/JqGrid/jquery.jqGrid.src.js",
                    "~/Themes/Default/Js/defaultAjaxForm.js",
                    "~/Themes/Default/Js/main.js",
                    "~/Themes/Default/Js/flashMessage.js",
                    "~/Themes/Default/Js/userIndexController.js",
                    "~/Themes/Default/Js/logGridController.js"
                )
            );

            // Uncomment to test minimized bundle
            //BundleTable.EnableOptimizations = true;
        }
    }
}