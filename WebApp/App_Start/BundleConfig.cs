using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib_b").Include(
                        "~/Source/js/libs/zepto.js",
                        "~/Source/js/libs/iscroll-4.2.5.js",
                        "~/Source/js/libs/ejs.js",
                        "~/Source/js/libs/json2.js"
                        //"~/Source/js/libs/video.js"
                        ));
            
            bundles.Add(new ScriptBundle("~/bundles/app").Include(

                        "~/Source/js/libs/html2canvas.js",
                        //"~/Source/js/libs/social.umeng.js",
                        "~/Source/js/libs/mad.js",
                        "~/Source/js/app/global.js",
                        "~/Source/js/app/app.js",
                        "~/Source/js/app/controllers/*.js",
                        "~/Source/js/app/start.js"
                        ));

            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new StyleBundle("~/source/bundles/style").Include(
                        "~/Source/css/mad.css",
                        "~/Source/css/style.css"
                        //"~/Source/css/video-js.css"
                        ));
            bundles.Add(new StyleBundle("~/source/css/social.umeng/css/style").Include(
                        "~/Source/css/social.umeng/css/social.umeng.css"
                        ));

        }
    }
}