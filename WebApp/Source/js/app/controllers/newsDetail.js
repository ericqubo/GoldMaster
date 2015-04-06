/// <reference path="../../_references.js" />

; (function (global) {

    var name = "newsDetail";

    mad.view.definePage("newsDetail", app.templateUrlDict.newsDetail);

    var module = {
        id: 0,
        get: function (id) {
            var _this = this;

            _this.id = id;
            var showLoadingFun = null;
            var hideLoadingFun = null;
            showLoadingFun = function () {
                showLoading($("#newsDetail section"));
            };
            hideLoadingFun = function () {
                hideLoading();
            };

            invokeAPIAsync(app.apiUrl.GetNews, {
                id: id,
            }, function (res) {
                if (res.m == 1) {

                    for (var i = 0; i < global.data.ngList.length; i++) {
                        if (global.data.ngList[i].ID == res.d.GroupId) {
                            res.d.GroupName = global.data.ngList[i].GroupName;
                            break;
                        }
                    }

                    var $thisPage = $("#" + name);
                    $thisPage.find("#GroupName").html(res.d.GroupName);
                    $thisPage.find("#Title").html(res.d.Title);
                    if (!util.isNullOrEmpty(res.d.ImgUrl)) {
                        $thisPage.find("#Image").attr("src", res.d.ImgUrl).show();
                    }

                    var $content = $(res.d.Content);
                    $content.find("img").each(function () {

                        $(this)[0].onload = function () {
                            app.views.newsDetail._scroll.refresh();
                        };

                        var src = $(this).attr("src");
                        if (src.indexOf("http://") < 0) {
                            src = imageDomain2 + src;
                            $(this).attr("src", src);
                        }
                        $(this).css("max-width", 245).css("width", "").css("height", "");
                    });


                    $thisPage.find("#Content").html("");
                    $thisPage.find("#Content").html($content);
                    if (res.d.isFav) {
                        $thisPage.find("#newfootIcon_1").addClass("newsFootBg");
                    }
                    $thisPage.find("#newsSource").html(util.isNullOrEmpty(res.d.Source) ? "" : "来源：" + res.d.Source);
                    $thisPage.find("#newsAuthor").html(util.isNullOrEmpty(res.d.Author) ? "" : "作者：" + res.d.Author);

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

    var pageResize = function () {

    }

    mad.addController(name, {
        route: "newsDetail/{id}",
        beforeAction: function (id) {

            if (module.id != id) {
                var $thisPage = $("#" + name);
                $thisPage.find("#GroupName").html("");
                $thisPage.find("#Title").html("");
                $thisPage.find("#Image").attr("src", "").hide();
                $thisPage.find("#Content").html("");
                $thisPage.find("#newfootIcon_1").removeClass("newsFootBg");
                $thisPage.find("#newsSource").html("");
                $thisPage.find("#newsAuthor").html("");

            }
        },
        action: function (id) {

            if (module.id == id) {
                return;
            }
            module.get(id);
            module.id = id;

            app.scrollerInit();
        },
        init: function () {

            $("#newsDetail #newfootIcon_1").globalTapLive(function () {
                if ($(this).attr("data-flag") == "1")
                    return;

                var action = $(this).hasClass("newsFootBg") ? 2 : 1;

                $(this).attr("data-flag", 1);
                invokeAPIAsync(app.apiUrl.Fav, {
                    action: action,
                    type:1,
                    id: mad.controllers["newsDetail"].module.id
                }, function (res) {
                    $("#newsDetail #newfootIcon_1").attr("data-flag", 0);
                    if (res.m == 1) {
                        if (action == 1) {
                            $("#newsDetail #newfootIcon_1").addClass("newsFootBg");
                            showTip("添加收藏成功");
                        }
                        else {
                            $("#newsDetail #newfootIcon_1").removeClass("newsFootBg");
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
                    $("#newsDetail #newfootIcon_1").attr("data-flag", 0);
                    //showTip(err);
                    showTip("操作失败");
                }, function () { }, function () { });

            });

            //分享
            $("#newsDetail #newfootIcon_2").globalTapLive(function () {
                var plat = 'renren';    //平台名
                var opt = {
                    'data': {
                        'content': {
                            'text': '1111111' //要分享的文字
                        }
                    }
                }

                try {
                    $.fn.umshare.share(plat, opt);
                }
                catch (e) {
                    //alert(e);
                }
            });

            $("#newsDetail #newfootIcon_3").globalTapLive(function () {
                var s = parseInt($("#newsDetail .newsCotnet > p").css("font-size").replace("px",""));
                //alert(s);
                if (s < 26)
                    $("#newsDetail .newsCotnet > p").css("font-size", (s + 2) + "px");

                $("#newsDetail .newsCotnet [style]").each(function () {
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
            $("#newsDetail #newfootIcon_4").globalTapLive(function () {
                var s = parseInt($("#newsDetail .newsCotnet > p").css("font-size").replace("px", ""));
                if (s > 14)
                    $("#newsDetail .newsCotnet > p").css("font-size", (s - 2) + "px");

                $("#newsDetail .newsCotnet [style]").each(function () {
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