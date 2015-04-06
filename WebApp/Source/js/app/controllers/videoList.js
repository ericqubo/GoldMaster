/// <reference path="../_references.js" />


; (function () {

    var name = "videoList";
    mad.view.definePage("videoList", app.templateUrlDict.videoList);

    var module = {
        refresh: true,
        pi: 0,//页码
        ps: 6,//页大小
        videoList: new Array(),
        //获取杂志列表
        getList: function (pi, ps, type, loadingType) {
            var _this = this;
            var url = app.apiUrl.GetVedioList;
            var data = { pi: pi, ps: ps };

            var showLoadingFun = null;
            var hideLoadingFun = null;

            switch (loadingType) {
                //下拉刷新时
                case 1:
                    showLoadingFun = function () { };
                    hideLoadingFun = function () { };

                    break;
                case 0:
                    showLoadingFun = function () {
                        showLoading($("#videoList .scrollerSection"));
                    };
                    hideLoadingFun = function () {
                        hideLoading();
                    };

                    break;
            }
            //异步调用服务器接口
            //function InvokeAPIAsync(url, data, successCallback, errorCallback)
            invokeAPIAsync(
                url,
                data,
                function (res) {
                    if (res.m == "1") {
                        //缓存数据
                        for (var i = 0; i < res.d.l.length; i++) {
                            _this.videoList[res.d.l[i].ID] = res.d.l[i];
                        }
                        if (type == 1) {
                            $("#videoList .videoList").empty();
                        }
                        $("#videoList .pullDate").html("上次更新：" + util.dateFormat(new Date(), "yyyy-MM-dd HH:mm:ss"));
                        //生成list页面
                        html = new EJS({ url: app.templateUrlDict.videoList_item, cache: false }).render({ videoListData: res.d.l });
                        $("#videoList .videoList").append(html);

                        //var pageCount = res.d.p;
                        ////判断下拉加载是否出现
                        //if (pi < pageCount) {
                        //    //出现
                        //    $("#magazList .pullUp").removeClass("none");
                        //}
                        //else {
                        //    //隐藏
                        //    $("#magazList .pullUp").addClass("none");
                        //}

                        module.pi = res.d.minId;
                        if (module.pi == 0)
                        {
                            $("#videoList .pullUp").addClass("none");
                        }

                        app.views[name]._scroll.refresh();
                        _this.isResh = false;
                    }
                    else {
                        if (res.e)
                            alertBox(res.e);
                        else
                            alertBox(errTips.global);
                    }
                },
                 function (err) {
                     alertBox(errTips.global);
                 }, function () { showLoadingFun(); }, function () { hideLoadingFun(); }
            );
        },
        //下拉
        scrollerRefresh: function (isScrollToTop) {
            var _this = this;
            _this.pi = 0;
            _this.getList(_this.pi, _this.ps, 1, 1);
            $("#videoList .pullUp").removeClass("none");
        },
        //上拉
        scrollerMore: function () {
            var _this = this;
            _this.getList(_this.pi, _this.ps, 2, 1);
        },
      
    };

    var pageResize = function () {

    }

    mad.addController(name, {
        route: "videoList",
        action: function () {

            if (!module.refresh) {
                return;
            }
            module.refresh = false;

            module.pi = 0;
            module.ps = 6;
            $("#videoList .pullUp").removeClass("none");
            app.scrollerInit();
            var $thisPage = $("#" + name);
            if (module.myScoll == null)
            {
                module.myScoll = new iScroll('videoList', {
                    snap: true,
                    momentum: false,
                    hScrollbar: false,
                    vScroll: false,
                    onScrollEnd: function () {
                        
                    }
                });
                pageResize();
            }
            module.getList(module.pi, module.ps, 1, 0);
            
        },
        init: function () {

        },
        resize: function () {
            pageResize();
           
        },
        module: module
    });
})();