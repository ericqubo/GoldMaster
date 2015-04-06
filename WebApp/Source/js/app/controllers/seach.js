/// <reference path="../../_references.js" />

; (function (global) {

    var name = "seach";
    var isInit = false;

    mad.view.definePage("seach", app.templateUrlDict.seach);

    var module = {
        getList: function (keyword, loadingType) {
            var _this = this;

            var showLoadingFun = null;
            var hideLoadingFun = null;
            loadingType = 1;
            switch (loadingType) {
                case 0:
                    showLoadingFun = function () {
                        showLoading($("#seach section"));
                    };
                    hideLoadingFun = function () {
                        hideLoading();
                    };

                    break;

                    //下拉刷新时
                case 1:
                    showLoadingFun = function () { };
                    hideLoadingFun = function () { };

                    break;
            }

            invokeAPIAsync(app.apiUrl.SeachNewsList, {
                keyword: keyword
            }, function (res) {
                if (res.m == 1) {

                    if (res.d == null) {

                        app.views[name]._scroll.refresh();
                        return;
                    }

                    if (typeof (successFun) == "function") {
                        successFun();
                    }

                    var html = mad.view.render({ url: app.templateUrlDict.newsList_item }, { list: res.d.nl });
                    $("#seach .newsList-body ul").empty();
                    $("#seach .newsList-body ul").append(html);

                    module.pi = res.d.minId;

                    app.views[name]._scroll.refresh();
                    if (!!isScrollToTop) {
                        app.views[name]._scroll.scrollTo(0, 0, 200);
                    }
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

        var $thisPage = $("#" + name);
        $thisPage.find("#thelist li").each(function () {
            $(this).css("width", document.body.clientWidth);
            $(this).find("img").css("width", document.body.clientWidth);
        });
        var $scroller = $thisPage.find("#seach-scroller");
        var count = $thisPage.find("#thelist li").length;
        $scroller.css("width", document.body.clientWidth * count);
    }

    mad.addController(name, {
        route: "seach",
        beforeAction: function () {
            $("#seach .newsList-body ul").empty();
        },
        action: function () {
            
            if (!isInit) {
                isInit = true;

                $("#seach .searchBtn").globalTapLive(function () {

                    var key = $("#seach .seach-text input").val();
                    console.log(key);
                    if (!util.isNullOrEmpty(key)) {
                        module.getList(key);
                    }
                });
            }

            app.scrollerInit();

            //$section = $thisPage.find("section");
            //$section.css("margin-top", "-45px");
            //$section.height($thisPage.find("section").height() + 45);

            //$thisPage.find("[data-role='scrollerSection']")
            //    .height($section.height() - $thisPage.find(".flash").height() - $thisPage.find(".line").height());


        },
        reset: function () {
            module.refresh = true;
        },
        init: function () {

        },
        resize: function () {
            pageResize();

            //var $thisPage = $("#" + name);
            //$thisPage.find("section").css("margin-top", "-45px");
            //$thisPage.find("section").height($thisPage.find("section").height() + 45);
        },
        destroy: function () {
        },
        module: module
    });
})(this);
