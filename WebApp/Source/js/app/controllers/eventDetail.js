/// <reference path="../../_references.js" />


; (function (global) {

    var name = "eventDetail";

    mad.view.definePage("eventDetail", app.templateUrlDict.eventDetail);

    var module = {
        id: 0,
        get: function (id) {
            var _this = this;

            _this.id = id;
            var showLoadingFun = null;
            var hideLoadingFun = null;
            showLoadingFun = function () {
                showLoading($("#eventDetail section"));
            };
            hideLoadingFun = function () {
                hideLoading();
            };

            invokeAPIAsync(app.apiUrl.GetEvent, {
                id: id,
            }, function (res) {
                if (res.m == 1) {

                    for (var i = 0; i < global.data.atList.length; i++) {
                        if (global.data.atList[i].ID == res.d.TypeID) {
                            res.d.TypeName = global.data.atList[i].Name;
                            break;
                        }
                    }

                    var $thisPage = $("#" + name);
                    $thisPage.find("#TypeName").html(res.d.TypeName);
                    $thisPage.find("#Image").attr("src", res.d.ImageUrl);

                    var $info = $(res.d.ThisActivityInfo);
                    $info.find("img").each(function () {

                        $(this)[0].onload = function () {
                            app.views.eventDetail._scroll.refresh();
                        };

                        var src = $(this).attr("src");
                        if (src.indexOf("http://") < 0) {
                            src = imageDomain2 + src;
                            $(this).attr("src", src);
                        }
                        $(this).css("max-width", 245).css("width", "").css("height","");
                    });

                    $thisPage.find("#ThisActivityInfo").html("");
                    $thisPage.find("#ThisActivityInfo").append($info);
                    $thisPage.find("#newfootIcon_8").attr("data-forhash", "#eventYuYue/" + module.id);

                    if (res.d.isFav) {
                        $thisPage.find("#newfootIcon_1").addClass("newsFootBg");
                    }

                    app.views[name]._scroll.refresh();
                    app.views[name]._scroll.scrollTo(0, 0, 200);
                }
                else {
                    if (res.e)
                        alertBox(res.e);
                    else
                        alertBox(errTips.global);
                }
            }, null, showLoadingFun, hideLoadingFun);
        }
    };

    mad.addController(name, {
        route: "eventDetail/{id}",
        beforeAction: function (id) {

            if (module.id != id) {
                var $thisPage = $("#" + name);
                $thisPage.find("#TypeName").html("");
                $thisPage.find("#Image").attr("src", "");
                $thisPage.find("#ThisActivityInfo").html("");
                $thisPage.find("#btnToYuYue").attr("data-forhash", "");
                $thisPage.find("#newfootIcon_1").removeClass("newsFootBg");
            }
        },
        action: function (id) {

            if (module.id == id) {
                return;
            }
            module.get(id);

            app.scrollerInit();
        },
        init: function () {

            $("#eventDetail #newfootIcon_1").globalTapLive(function () {
                if ($(this).attr("data-flag") == "1")
                    return;

                var action = $(this).hasClass("newsFootBg") ? 2 : 1;

                $(this).attr("data-flag", 1);
                invokeAPIAsync(app.apiUrl.Fav, {
                    action: action,
                    type: 3,
                    id: mad.controllers["eventDetail"].module.id
                }, function (res) {
                    $("#eventDetail #newfootIcon_1").attr("data-flag", 0);
                    if (res.m == 1) {
                        if (action == 1) {
                            $("#eventDetail #newfootIcon_1").addClass("newsFootBg");
                            showTip("添加收藏成功");
                        }
                        else {
                            $("#eventDetail #newfootIcon_1").removeClass("newsFootBg");
                            showTip("取消收藏成功");
                        }
                    }
                    else {

                        if (res.e)
                            alertBox(res.e);
                        else
                            alertBox(errTips.global);
                    }
                }, function (err) {
                    $("#eventDetail #newfootIcon_1").attr("data-flag", 0);
                    //showTip(err);
                    showTip("操作失败");
                }, function () { }, function () { });

            });

            $("#eventDetail #newfootIcon_3").globalTapLive(function () {
                var s = parseInt($("#eventDetail #ThisActivityInfo > p").css("font-size").replace("px", ""));
                //alert(s);
                if (s < 26)
                    $("#eventDetail #ThisActivityInfo > p").css("font-size", (s + 2) + "px");

                $("#eventDetail #ThisActivityInfo [style]").each(function () {
                    var styleStr = $(this).attr("style");
                    if (styleStr.indexOf("font-size") > -1) {
                        s = parseInt($(this).css("font-size").replace("px", ""));
                        if (s < 26) {
                            $(this).css("font-size", (s + 2) + "px");
                        }
                    }
                });

                app.views[name]._scroll.refresh();
            })
            $("#eventDetail #newfootIcon_4").globalTapLive(function () {
                var s = parseInt($("#eventDetail #ThisActivityInfo > p").css("font-size").replace("px", ""));
                if (s > 14)
                    $("#eventDetail #ThisActivityInfo > p").css("font-size", (s - 2) + "px");

                $("#eventDetail #ThisActivityInfo [style]").each(function () {
                    var styleStr = $(this).attr("style");
                    if (styleStr.indexOf("font-size") > -1) {
                        s = parseInt($(this).css("font-size").replace("px", ""));
                        if (s > 14) {
                            $(this).css("font-size", (s - 2) + "px");
                        }
                    }
                });

                app.views[name]._scroll.refresh();
            })
        },
        resize: function () {

        },
        module: module
    });
})(this);