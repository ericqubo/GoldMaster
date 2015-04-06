/// <reference path="../../_references.js" />


; (function (global) {

    var name = "eventList";
    var imageIndex = 1;

    mad.view.definePage("eventList", app.templateUrlDict.eventList);

    var module = {
        tid: null,
        ps: 10,
        myScroll: null,
        refresh: true,
        scrollerRefresh: function () {

            var _this = this;
            _this.getList(1, _this.tid, 0, _this.ps, function () {
                $("#eventList .scroller-content .eventList-body ul").empty();
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
                        showLoading($("#eventList section"));
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

            invokeAPIAsync(app.apiUrl.GetEventList, {
                tid: tid,
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
                    $("#eventList .scroller-content .eventList-body ul").append(html);

                    $("#eventList .pullDate").html("上次更新：" + util.dateFormat((new Date()), "yyyy/MM/dd HH:mm:ss"));

                    if (res.d.nl == null || res.d.nl.length < ps) {
                        $("#eventList .pullUp").hide();
                    }
                    else {
                        $("#eventList .pullUp").show();
                    }

                    if (pi == 0) {

                        $("#eventList #thelist").empty();
                        $("#eventList #indicator").empty();
                    }
                    //hotNews
                    if (res.d.hnl) {
                        var htmlStr = mad.view.render({ url: app.templateUrlDict.eventList_hot }, { list: res.d.hnl });
                        $("#eventList #thelist").append(htmlStr);

                        pageResize();
                        module.myScroll.refresh();

                        var count = $("#" + name).find("#thelist li").length;
                        clearInterval(module.interval);
                        if (count > 0) {

                            for (var j = 0; j < count; j++) {
                                $("#eventList #indicator").append('<li class="">1</li>');
                            }
                            $("#eventList #indicator li").eq(0).addClass("active");

                            module.interval = setInterval(function () {
                                //console.log(imageIndex);
                                if (imageIndex >= count) imageIndex = 0;
                                module.myScroll.scrollToPage(imageIndex++, 0, 400);
                            }, 3500);
                        }
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
        },
        initActityType: function () {

            $("#eventList #subNav li").remove();
            if (global.data.atList == null)
                return;

            for (var i = 0; i < global.data.atList.length; i++) {
                var str = '<li data-id="' + global.data.atList[i].ID + '">' +
                                                '<span>' +
                                                  '<i class="subIcon_' + i + '"></i>' + global.data.atList[i].Name +
                                                '</span>' +
                                             ' </li>';
                $("#eventList #subNav").append(str);
            }

            $("#eventList #subNav li").last().addClass("noLine");

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
        route: "eventList",
        action: function () {
            
            var _init = false;
            var $thisPage = $("#" + name);
            if (module.myScroll == null) {

                if (app.isIos7) {
                    var top = parseInt($("#eventList #subNav").css("top"));
                    $("#eventList #subNav").css("top", top + 20);
                }

                var newsListWrapper = $("#" + name + " #eventList-wrapper");
                module.myScroll = new iScroll(newsListWrapper[0], {
                    snap: true,
                    momentum: false,
                    hScrollbar: false,
                    vScroll: false,
                    onScrollEnd: function () {
                        $thisPage.find('#indicator > li.active').removeClass("active");
                        imageIndex = this.currPageX + 1;
                        $thisPage.find('#indicator > li:nth-child(' + (this.currPageX + 1) + ')').addClass('active');
                    }
                });

                module.initActityType();
                if (global.data != null) {
                    module.tid = global.data.atList[0].ID;
                    $("#eventList #eventName").html(global.data.atList[0].Name);
                }
                else
                    module.tid = 0;
                module.getList(0, module.tid, 0, module.ps);

                _init = true;
            }
            else {
                module.myScroll.refresh();

                var count = $("#" + name).find("#thelist li").length;
                module.interval = setInterval(function () {
                    //console.log(imageIndex);
                    if (imageIndex >= count) imageIndex = 0;
                    module.myScroll.scrollToPage(imageIndex++, 0, 400);
                }, 3500);
            }
            
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
            module.myScroll.refresh();
        },
        destroy: function () {
            clearInterval(module.interval);
            $("#" + name).find("#subNav").hide();
        },
        module: module
    });
})(this);