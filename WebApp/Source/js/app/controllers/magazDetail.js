/// <reference path="../../_references.js" />


; (function (global) {

    var name = "magazDetail";
    mad.view.definePage("magazDetail", app.templateUrlDict.magazDetail);

    var module = {
        id: 0,
        myScroll: null,
        dataList: [],
        dataIndex: 0,
        refresh: true,
        getPages: function (id) {

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

            invokeAPIAsync(app.apiUrl.GetMagaz, {
                id: id,
            }, function (res) {
                if (res.m == 1) {

                    var htmlStr = "";
                    module.dataList = [];
                    module.dataIndex = 0;
                    for (var i = 0; i < res.d.pages.length; i++) {
                        //htmlStr += ' <li><img src="' + res.d.pages[i] + '" style="" /></li>';
                        module.dataList.push(res.d.pages[i]);
                    }
                    if (module.dataList.length > 0) {

                        $("#magazDetail #thelist img").attr("src", module.dataList[0]);
                        $("#magazDetail .page-num").html("1/" + module.dataList.length);
                    }
                    //var $thisPage = $("#" + name);
                    //$thisPage.find("#thelist").empty();
                    //$thisPage.find("#thelist").append(htmlStr);
                    pageResize();
                    module.myScroll.refresh();

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
            var height = $thisPage.find("section").height();
            $(this).css("height", height);
            $(this).find("img").css("max-height", height);
            $(this).find("img").css("max-width", document.body.clientWidth);
            //$(this).find("img").css("width", document.body.clientWidth);
        });
        var $scroller = $thisPage.find("#magazDetail-scroller");
        var count = $thisPage.find("#thelist li").length;
        $scroller.css("width", document.body.clientWidth * count);
    }

    mad.addController(name, {
        route: "magazDetail/{id}",
        beforeAction: function (id) {

            if (module.id != id) {
                $("#magazDetail #thelist img").attr("src", "");
                //$("#magazDetail #thelist").empty();
            }
        },
        action: function (id) {

            var $thisPage = $("#" + name);
            if (module.myScroll == null) {
                var magazDetailWrapper = $("#" + name + " #magazDetail-wrapper");
                module.myScroll = new iScroll(magazDetailWrapper[0], {
                    //snap: true,
                    //momentum: false,
                    //hScrollbar: false,
                    //vScroll: false,
                    zoom: true
                    //onScrollEnd: function () {
                    //
                    //}
                });

            }

            if (module.id == id) {
                return;
            }

            module.getPages(id);

        },
        reset: function () {
            module.refresh = true;
        },
        init: function () {
            $("#magazDetail .page-prev").globalTapLive(function () {
                if (module.dataIndex > 0) {
                    module.dataIndex--;

                    $("#magazDetail #thelist img").attr("src", module.dataList[module.dataIndex]);
                    $("#magazDetail .page-num").html((module.dataIndex + 1) + "/" + module.dataList.length);
                    module.myScroll.refresh();
                }
            });
            $("#magazDetail .page-next").globalTapLive(function () {

                if (module.dataIndex < module.dataList.length - 1) {
                    module.dataIndex++;

                    $("#magazDetail #thelist img").attr("src", module.dataList[module.dataIndex]);
                    $("#magazDetail .page-num").html((module.dataIndex + 1) + "/" + module.dataList.length);
                    module.myScroll.refresh();
                }
            });
        },
        resize: function () {
            pageResize();
        },
        module: module
    });
})(this);