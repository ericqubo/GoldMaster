/// <reference path="../../_references.js" />


; (function (global) {

    var name = "eventFavList";
    var imageIndex = 1;

    mad.view.definePage("eventFavList", app.templateUrlDict.eventFavList);

    var module = {
        ps: 10,
        refresh: true,
        scrollerRefresh: function () {

            var _this = this;
            _this.getList(1, _this.tid, 0, _this.ps, function () {
                $("#eventFavList .scroller-content .eventList-body ul").empty();
            }, true);
        },
        scrollerMore: function () {

            var _this = this;
            _this.getList(1, _this.tid, _this.pi, _this.ps, function () {

            }, false);
        },
        getList: function (loadingType, tid, pi, ps, successFun, isScrollToTop) {
            var _this = this;
            _this.pi = pi || _this.pi;
            _this.ps = ps || _this.ps;
            _this.tid = tid || _this.tid;

            var showLoadingFun = null;
            var hideLoadingFun = null;

            switch (loadingType) {
                case 0:
                    showLoadingFun = function () {
                        showLoading($("#eventFavList section"));
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

            invokeAPIAsync(app.apiUrl.GetEventFavList, {
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

                    var html = mad.view.render({ url: app.templateUrlDict.eventList_item }, { list: res.d.nl });
                    $("#eventFavList .scroller-content .eventList-body ul").append(html);

                    $("#eventFavList .pullDate").html("上次更新：" + util.dateFormat((new Date()), "yyyy/MM/dd HH:mm:ss"));

                    if (res.d.nl == null || res.d.nl.length < ps) {
                        $("#eventFavList .pullUp").hide();
                    }
                    else {
                        $("#eventFavList .pullUp").show();
                    }

                    $("#eventFavList #eventList-body ul").empty();
                    

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
        var $scroller = $thisPage.find("#eventList-scroller");
        var count = $thisPage.find("#thelist li").length;
        $scroller.css("width", document.body.clientWidth * count);
    }

    mad.addController(name, {
        route: "eventFavList",
        action: function () {

            if (!module.refresh) {
                return;
            }
            module.refresh = false;
            module.getList(0, module.tid, 0, module.ps);

            app.scrollerInit();

        },
        reset: function () {
            module.refresh = true;
        },
        init: function () {

            $("#" + name + " #listArrow").globalTapLive(function () {
                if ($("#" + name).find("#subNav").css("display") == "none") {
                    $("#" + name).find("#subNav").show();
                }
                else
                    $("#" + name).find("#subNav").hide();
            });


            $("#" + name + " #subNav li").globalTapLive(function () {
                $("#" + name).find("#subNav").hide();

                var id = $(this).attr("data-id");
                for (var i = 0; i < global.data.atList.length; i++) {
                    if (id === global.data.atList[i].ID.toString()) {

                        module.tid = global.data.atList[i].ID;
                        $("#" + name + " #eventName").html(global.data.atList[i].Name);

                        module.getList(0, module.tid, 0, module.ps, function () {
                            $("#" + name + " .scroller-content .eventList-body ul").empty();
                        }, true);
                    }
                }
            });
        },
        resize: function () {
            pageResize();

            //var $thisPage = $("#" + name);
            //$thisPage.find("section").css("margin-top", "-45px");
            //$thisPage.find("section").height($thisPage.find("section").height() + 45);
            //module.myScroll.refresh();
        },
        destroy: function () {
            clearInterval(module.interval);
            $("#" + name).find("#subNav").hide();
        },
        module: module
    });
})(this);