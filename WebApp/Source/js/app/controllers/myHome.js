/// <reference path="../_references.js" />


; (function () {

    var name = "myHome";
    mad.view.definePage("myHome", app.templateUrlDict.myHome);

    var module = {
        myScoll: null,
        refresh: true,
        scrollerRefresh: function () {
            var _this = this;
            _this.pi = 1;
            _this.getTypeSum(1, 0);
        },
        scrollerMore: function () {

            setTimeout(function () {
                app.views[name]._scroll.refresh();
            }, 500);
        },

        getTypeSum: function (uid, loadingType) {
            var _this = this;
            var url = app.apiUrl.GetTypeSum;
            var data = { uid: uid };
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
                        showLoading($("#myHome .scrollerSection"));
                    };
                    hideLoadingFun = function () {
                        hideLoading();
                    };

                    break;
            }
            //异步调用服务器接口
            invokeAPIAsync(
                url,
                data,
                function (res) {
                    if (res.m == "1") {

                        $("#myHome .pullDate").html("上次更新：" + util.dateFormat(new Date(), "yyyy-MM-dd HH:mm:ss"));
                        //生成list页面
                        html = new EJS({ url: app.templateUrlDict.myfavItem, cache: false }).render({ favList: res.d });
                        $("#myHome .listview").empty();
                        $("#myHome .listview").append(html);

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
    };
    var pageResize = function () {

    }

    mad.addController(name, {
        route: name,
        refresh: true,
        action: function () {

            if (!module.refresh) {
                return;
            }

            module.refresh = false;
            $("#myHome .pullUp").addClass("none");
            app.scrollerInit();
            var $thisPage = $("#" + name);
            //if (module.myScoll == null) {
            //    module.myScoll = new iScroll('myHome', {
            //        snap: true,
            //        momentum: false,
            //        hScrollbar: false,
            //        vScroll: false,
            //        onScrollEnd: function () {

            //        }
            //    });
            //    pageResize();
            //}

            module.getTypeSum(1, 0);
        },
        init: function () {

        },
        resize: function () {
            pageResize();
        },
        module: module
    });
})();