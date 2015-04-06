/// <reference path="../_references.js" />


; (function () {

    var name = "videoDetail";

    mad.view.definePage("videoDetail", app.templateUrlDict.videoDetail);

    var module = {
        id: 0,
        myScroll: null,
    };

    var pageResize = function () {
        var $thisPage = $("#" + name);
        var height = $thisPage.find("section").height() - $thisPage.find(".flashPic").height();
        $thisPage.find("#scroller-warp").height(height);
    }

    mad.addController(name, {
        route: "videoDetail/{id}",
        beforeAction: function (id) {

            if (module.id != id) {
                var $thisPage = $("#" + name);
                $thisPage.find("#GroupName").html("");
                $thisPage.find("#Title").html("");
                //$thisPage.find("#sourcemp4").attr("src", "");
                //$thisPage.find("#sourcewebm").attr("src", "");
                //$thisPage.find("#sourceogg").attr("src", "");
                //$thisPage.find("#sourceflv").attr("src", "");
                $thisPage.find("#Content").html("");

                if (module.play) {
                    module.play.src("");
                }
                $thisPage.find("#Image video").attr("poster", "");
            }
        },
        action: function (id) {

            if (module.myScroll == null) {
                var magazDetailWrapper = $("#" + name + " #scroller-warp");
                module.myScroll = new iScroll(magazDetailWrapper[0], {
                    useTransition: false,
                    fadeScrollbar: true
                });

            }

            if (module.id == id) {
                return;
            }
            var vedio = mad.controllers["videoList"].module.videoList[id];
            $("#videoDetail .pullUp").removeClass("none");
            //app.scrollerInit();
            var $thisPage = $("#" + name);

            if (vedio) {
                var w = $(window).width();
                var $thisPage = $("#" + name);
                $thisPage.find("#GroupName").html("");
                $thisPage.find("#Title").html(vedio.Title);
                //$thisPage.find("#sourcemp4").attr("src", vedio.Src);
                //$thisPage.find("#sourcewebm").attr("src", vedio.Src);
                //$thisPage.find("#sourceogg").attr("src", vedio.Src);
                //$thisPage.find("#sourceflv").attr("src", vedio.Src);
                $thisPage.find("#Image").attr("width", w);
                $thisPage.find("#Content").html(vedio.Content);

                if (!module.play) {
                    module.play = videojs("Image", {}, function () {
                        // Player (this) is initialized and ready.
                    });
                }

                //module.play.poster(vedio.IntroImage);
                module.play.src(vedio.Src);
                $thisPage.find("#Image video").attr("poster", vedio.IntroImage);
            }

            module.id = id;

            pageResize();
            module.myScroll.refresh();
        },
        init: function () {

        },
        destroy: function () {
            var $thisPage = $("#" + name);

            var video = $thisPage.find("video")[0];
            if (video && video.pause)
                video.pause();

        },
        resize: function () {
            pageResize();
        },
        module: module
    });
})();