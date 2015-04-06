/// <reference path="../../_references.js" />

; (function (global) {

    var name = "newsFavList";
    var imageIndex = 1;

    mad.view.definePage("newsFavList", app.templateUrlDict.newsFavList);

    var module = {
        ps: 10,
        refresh: true,
        scrollerRefresh: function () {

            var _this = this;
            _this.getList(1, 0, _this.ps, function () {
                $("#newsFavList .scroller-content .newsList-body ul").empty();
            }, true);
        },
        scrollerMore: function () {

            var _this = this;
            _this.getList(1, _this.pi, _this.ps, function () {

            }, false);
        },
        getList: function (loadingType, pi, ps, successFun, isScrollToTop) {
            var _this = this;
            _this.pi = pi || _this.pi;
            _this.ps = ps || _this.ps;

            var showLoadingFun = null;
            var hideLoadingFun = null;

            switch (loadingType) {
                case 0:
                    showLoadingFun = function () {
                        showLoading($("#newsFavList section"));
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

            invokeAPIAsync(app.apiUrl.GetNewsFavList, {
                pi: pi,
                ps: ps
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
                    $("#newsFavList .scroller-content .newsList-body ul").append(html);

                    $("#newsFavList .pullDate").html("上次更新：" + util.dateFormat((new Date()), "yyyy/MM/dd HH:mm:ss"));

                    if (res.d.nl == null || res.d.nl.length < ps) {
                        $("#newsFavList .pullUp").hide();
                    }
                    else {
                        $("#newsFavList .pullUp").show();
                    }               

                    module.pi = res.d.minId;

                    app.views[name]._scroll.refresh();
                    if (!!isScrollToTop) {
                        app.views[name]._scroll.scrollTo(0, -45, 200);
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
        var $scroller = $thisPage.find("#newsList-scroller");
        var count = $thisPage.find("#thelist li").length;
        $scroller.css("width", document.body.clientWidth * count);
    }

    mad.addController(name, {
        route: "newsFavList",
        action: function () {
            
            if (!module.refresh) {
                return;
            }
            module.refresh = false;
            module.getList(0, 0, module.ps);

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