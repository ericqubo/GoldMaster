/// <reference path="../../_references.js" />


; (function () {

    var name = "magazFavList";
    mad.view.definePage("magazFavList", app.templateUrlDict.magazFavList);

    var module = {
        myScoll: null,
        refresh: true,
      
        magazType:0, //0:magazList,1:magazFav
        pi: 0,//页码
        ps: 6,//页大小
        magazList: new Array(),
        //获取杂志列表
        getList: function (pi, ps, type, loadingType) {
            var _this = this;
            var url = app.apiUrl.GetListFav;
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
                        showLoading($("#magazFavList .scrollerSection"));
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
                            _this.magazList[res.d.l[i].ID] = res.d.l[i];
                        }
                        if (type == 1) {
                            $("#magazFavList .magazList").empty();
                        }
                        $("#magazFavList .pullDate").html("上次更新：" + util.dateFormat(new Date(), "yyyy-MM-dd HH:mm:ss"));
                        //生成list页面
                        html = new EJS({ url: app.templateUrlDict.magazItem, cache: false }).render({ magazListData: res.d.l });
                        $("#magazFavList .magazList").append(html);

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

                        module.pi = res.minId;
                        if (res.minId == 0)
                        {
                            $("#magazFavList .pullUp").addClass("none");
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
            _this.getList(_this.pi, _this.ps, 1, 1, module.magazType);
            $("#magazFavList .pullUp").removeClass("none");
        },
        //上拉
        scrollerMore: function () {
            var _this = this;
            _this.getList(_this.pi, _this.ps, 2, 1, module.magazType);
        },
      
    };

    var pageResize = function () {

    }

    mad.addController(name, {
        route: "magazFavList",
        action: function (type) {

            module.pi = 0;
            module.ps = 6;
            $("#magazFavList .pullUp").removeClass("none");
            app.scrollerInit();
            var $thisPage = $("#" + name);
            if (module.myScoll == null)
            {
                module.myScoll = new iScroll('magazFavList', {
                    snap: true,
                    momentum: false,
                    hScrollbar: false,
                    vScroll: false,
                    onScrollEnd: function () {
                        
                    }
                });
                pageResize();
            }
            if (type == 1) {
                module.magazType = 1;
                module.getList(module.pi, module.ps, 1, 0, 1);
            }
            else {
                module.magazType = 0;
                module.getList(module.pi, module.ps, 1, 0, 0);
            }
        },
        init: function () {

        },
        resize: function () {
            pageResize();
           
        },
        module: module
    });
})();