/// <reference path="../../_references.js" />

; (function (global) {

    var name = "newsList";
    var imageIndex = 1;

    mad.view.definePage("newsList", app.templateUrlDict.newsList);

    var module = {
        gid: null,
        ps: 10,
        myScroll: null,
        refresh: true,
        scrollerRefresh: function () {

            var _this = this;
            _this.getList(1, _this.gid, 0, _this.ps, function () {
                $("#newsList .scroller-content .newsList-body ul").empty();
            }, true);
        },
        scrollerMore: function () {

            var _this = this;
            _this.getList(1, _this.gid, _this.pi, _this.ps, function () {

            }, false);
        },
        getList: function (loadingType, gid, pi, ps, successFun, isScrollToTop) {
            var _this = this;
            _this.pi = pi || _this.pi;
            _this.ps = ps || _this.ps;
            _this.gid = gid || _this.gid;

            var showLoadingFun = null;
            var hideLoadingFun = null;

            switch (loadingType) {
                case 0:
                    showLoadingFun = function () {
                        showLoading($("#newsList section"));
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

            invokeAPIAsync(app.apiUrl.GetNewsList, {
                gid: gid,
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
                    $("#newsList .scroller-content .newsList-body ul").append(html);

                    $("#newsList .pullDate").html("上次更新：" + util.dateFormat((new Date()), "yyyy/MM/dd HH:mm:ss"));

                    if (res.d.nl == null || res.d.nl.length < ps) {
                        $("#newsList .pullUp").hide();
                    }
                    else {
                        $("#newsList .pullUp").show();
                    }
                    
                    if (pi == 0) {

                        $("#newsList #thelist").empty();
                        $("#newsList #indicator").empty();
                    }
                    //hotNews
                    if (res.d.hnl) {
                        var htmlStr = mad.view.render({ url: app.templateUrlDict.newsList_hot }, { list: res.d.hnl });
                        $("#newsList #thelist").append(htmlStr);

                        pageResize();
                        module.myScroll.refresh();

                        var count = $("#" + name).find("#thelist li").length;
                        clearInterval(module.interval);
                        if (count > 0) {

                            for (var j = 0; j < count; j++) {
                                $("#newsList #indicator").append('<li class="">1</li>');
                            }
                            $("#newsList #indicator li").eq(0).addClass("active");

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
            },null, showLoadingFun, hideLoadingFun);
        },
        initNewsGroup: function () {

            $("#newsList #subNav li").remove();
            if (global.data.ngList == null)
                return;

            for (var i = 0; i < global.data.ngList.length; i++) {
                var str = '<li data-id="' + global.data.ngList[i].ID + '">' +
                                                '<span>' +
                                                  '<i class="subIcon_' + i + '"></i>' + global.data.ngList[i].GroupName +
                                                '</span>' +
                                             ' </li>';
                $("#newsList #subNav").append(str);
            }

            $("#newsList #subNav li").last().addClass("noLine");

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
        route: "newsList",
        action: function () {

            var _init = false;
            var $thisPage = $("#" + name);
            if (module.myScroll == null) {

                //if (app.isIos7) {
                //    var top = parseInt($("#newsList #subNav").css("top"));
                //    $("#newsList #subNav").css("top", top + 20);
                //}

                var newsListWrapper = $("#" + name + " #newsList-wrapper");
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

                module.initNewsGroup();
                //if (global.data.ngList != null) {
                //    module.gid = global.data.ngList[0].ID;
                //    $("#newsList #groupName").html(global.data.ngList[0].GroupName);
                //}
                //else
                    module.gid = null;
                module.getList(0, module.gid, 0, module.ps);

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

            //$section = $thisPage.find("section");
            //$section.css("margin-top", "-45px");
            //$section.height($thisPage.find("section").height() + 45);

            //$thisPage.find("[data-role='scrollerSection']")
            //    .height($section.height() - $thisPage.find(".flash").height() - $thisPage.find(".line").height());


        },
        reset: function () {
            newsDateTitle = null;
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
                for (var i = 0; i < global.data.ngList.length; i++) {
                    if (id === global.data.ngList[i].ID.toString()) {

                        module.gid = global.data.ngList[i].ID;
                        $("#newsList #groupName").html(global.data.ngList[i].GroupName);

                        module.getList(0, module.gid, 0, module.ps, function () {
                            $("#newsList .scroller-content .newsList-body ul").empty();
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