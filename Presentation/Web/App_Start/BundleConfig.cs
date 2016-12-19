using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Web.App_Start
{
    public class BundleConfig
    { // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendoui").Include(
                "~/Scripts/kendo/2016.2.714/kendo.ui.core.min.js",
                "~/Scripts/kendo/2016.2.714/cultures/kendo.culture.zh-Hans.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/echarts").Include(
                "~/Scripts/echarts.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/dataTables.responsive.js",
                "~/Scripts/DataTables/dataTables.bootstrap.js",
                "~/Scripts/DataTables/dataTables.fixedColumns.js"));

            bundles.Add(new ScriptBundle("~/bundles/kunlun-extensions").Include(
                "~/Scripts/kunlun.util.js",
                "~/Scripts/kunlun.data.js",
                "~/Scripts/kunlun.modal.js",
                "~/Scripts/kunlun.modal.Selector.js",
                "~/Scripts/kunlun.extension.alertify.js",
                "~/Scripts/kunlun.extension.jquery-validator-compatibility",
                "~/Scripts/kunlun.extension.kendo.js",
                "~/Scripts/kunlun.extension.kendo.culture.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/2016.2.714/css").Include(
                "~/Content/kendo/2016.2.714/kendo.common-bootstrap.min.css",
                "~/Content/kendo/2016.2.714/kendo.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/admin-lte/AdminLTE.css",
                "~/Content/admin-lte/skins/skin-green.css",
                "~/Content/alertifyjs/alertify.css",
                "~/Content/alertifyjs/themes/bootstrap.css",
                "~/Content/DataTables/css/responsive.bootstrap.css",
                "~/Content/DataTables/css/fixedColumns.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/kl-style").Include(
                "~/Content/Site.css"));
        }

    }
}