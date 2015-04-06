/// <reference path="../_references.js" />

window.umappkey = "54298c3bfd98c5cf6a015729";
//window.onerror = function (e) {
//    alert(e);
//}
var defaultImg = _sourceDomain + "images/nopic.png";
var backNum = 1;
var readyed = false, data = {

};

var imageDomain2 = "http://cbngold.com/";

var app = {
    views: {},
    templateUrlDict: {

        "footer": "js/views/footer.ejs?v=0.01",
        "newsList": "js/views/newsList.ejs?v=0.01",
        "newsFavList": "js/views/newsFavList.ejs?v=0.01",
        "newsList_hot": "js/views/newsList_hot.ejs?v=0.01",
        "newsList_item": "js/views/newsList_item.ejs?v=0.01",
        "newsDetail": "js/views/newsDetail.ejs?v=0.03",
        "eventList": "js/views/eventList.ejs?v=0.01",
        "eventFavList": "js/views/eventFavList.ejs?v=0.01",
        "eventList_hot": "js/views/eventList_hot.ejs?v=0.01",
        "eventList_item": "js/views/eventList_item.ejs?v=0.01",
        "eventDetail": "js/views/eventDetail.ejs?v=0.01",
        "eventYuYue": "js/views/eventYuYue.ejs?v=0.01",
        "magazList": "js/views/magazList.ejs?v=0.01",
        "magazFavList": "js/views/magazFavList.ejs?v=0.01",
        "magazItem": "js/views/magazItem.ejs?v=0.01",
        "magazDetail": "js/views/magazDetail.ejs?v=0.02",
        "fav": "js/views/fav.ejs?v=0.01",
        "fav_Magaz": "js/views/fav_Magaz.ejs?v=0.01",
        "myHome": "js/views/myHome.ejs?v=0.01",
        "myfavItem": "js/views/myfavItem.ejs?v=0.01",
        "videoDetail": "js/views/videoDetail.ejs?v=0.01",
        "videoList": "js/views/videoList.ejs?v=0.01",
        "videoList_item": "js/views/videoList_item.ejs?v=0.01",

        "seach": "js/views/seach.ejs?v=0.01"
    },
    apiUrl: {
        "GetBaseData": "service/BaseData",
        "Fav": "service/Fav",

        "GetNews": "news/Get",
        //"GetHotList": "news/GetHotList",
        "GetNewsList": "news/GetList",
        "SeachNewsList": "news/SeachList",
        "GetNewsFavList": "news/GetFavList",

        "GetEventList": "event/GetList",
        "GetEventFavList": "event/GetFavList",
        "GetEvent": "event/Get",

        "ReserveEvent": "event/Reserve",
        
        "GetMagaz": "Magaz/Get",
        "GetMagazList": "Magaz/GetList",
        "GetListFav": "Magaz/GetListFav",

        //我的
        "GetTypeSum": "MyHome/GetTypeSum",

        "GetVedioList": "Vedio/GetVedioList",
        
    },
    dbContext: window.openDatabase,

    pageInit: function () {

        //is ios
        if (app.device && app.device.platform && app.device.platform.toLowerCase() == "ios") {
            var version = app.device.version.split('.')[0];
            version = parseInt(version);
            if (version >= 7) {
                app.isIos7 = true;
                mad.app.addEvent("onBeforeGotoPage", function (fromRouteName, toRouteName) {
                    var $statusbar = $("#" + toRouteName).find(".statusbar");
                    if ($statusbar.length == 0) {
                        $("#" + toRouteName).prepend('<div class="statusbar"></div>');
                    }
                });
            }
        }

        //if (_type == 2) {
        //    mad.app.addEvent("onBeforeGotoPage", function (fromRouteName, toRouteName) {
        //        var $statusbar = $("#" + toRouteName).find(".statusbar");
        //        if ($statusbar.length == 0) {
        //            $("#" + toRouteName).prepend('<div class="statusbar"></div>');
        //        }
        //    });
        //}

        app.getBaseData(function () {
            mad.app.ready();
            mad.app.gotoPage("#newsList");
            hideLoading();
        });

        readyed = true;
    }

};

for (var i in app.templateUrlDict) {
    app.templateUrlDict[i] = _sourceDomain + app.templateUrlDict[i];
}

for (var i in app.apiUrl) {
    app.apiUrl[i] = _apiDomain + app.apiUrl[i];
}

mad.view.tempCached = _isDebug ? false : true;

//#region invokeAPI

//异步调用服务器接口
function invokeAPIAsync(url, data, successCallback, errorCallback, showLoadingFun, hideLoadingFun) {
    
    var _data = {};
    _data._uuid = app.device.uuid;
    if (data) {
        for (var i in data) {
            if (data[i] != undefined) {
                _data[i] = data[i];
            }
        }
    }

    //console.log(_data);
    if (typeof (showLoadingFun) == "function") {
        showLoadingFun();
    } else {
        showLoading();
    }
    $.ajax({
        url: url,
        type: "post",
        data: _data,
        //context: document.body,
        //headers: {
        //    "UserAgent": app.userAgentHeader
        //},
        success: function (res) {

            if (app.views[mad.currentRouteName]) {
                app.views[mad.currentRouteName].moreFlag = false;
            }

            if (typeof (hideLoadingFun) == "function") {
                hideLoadingFun();
            } else {
                hideLoading();
            }
            if (successCallback) {
                successCallback(res);
            }

            //else if (res.m == 1) {
            //}
            //else {
            //    alertBox(res.e || "加载失败，请重试");
            //}
        },
        error: function (err) {

            if (app.views[mad.currentRouteName]) {
                app.views[mad.currentRouteName].moreFlag = false;
            }

            if (typeof (hideLoadingFun) == "function") {
                hideLoadingFun();
            } else {
                hideLoading();
            }

            if (typeof (errorCallback) == "function") {
                errorCallback(err);
            }
            else {
                alertBox("加载失败，请重试");
            }
        }
    });
}

//同步调用服务器
function invokeAPISync(url, data, successCallback, errorCallback, showLoadingFun, hideLoadingFun) {

    var _data = {};
    _data._uuid = app.device.uuid;
    if (data) {
        for (var i in data) {
            if (data[i] != undefined) {
                _data[i] = data[i];
            }
        }
    }


    if (typeof (showLoadingFun) == "function") {
        showLoadingFun();
    } else {
        showLoading();
    }

    $.ajax({
        url: url,
        type: "post",
        async: false,
        data: _data,
        //context: document.body,
        //headers: {
        //    "UserAgent": app.userAgentHeader
        //},
        success: function (res) {

            if (app.views[mad.currentRouteName]) {
                app.views[mad.currentRouteName].moreFlag = false;
            }

            if (typeof (hideLoadingFun) == "function") {
                hideLoadingFun();
            } else {
                hideLoading();
            }
            if (successCallback) {
                successCallback(res);
            }

        },
        error: function (err) {

            if (app.views[mad.currentRouteName]) {
                app.views[mad.currentRouteName].moreFlag = false;
            }

            if (typeof (hideLoadingFun) == "function") {
                hideLoadingFun();
            } else {
                hideLoading();
            }

            if (typeof (errorCallback) == "function") {
                errorCallback(err);
            }
            else {
                alertBox(errTips.global);
            }
        }
    });
}

//#endregion
//APP信息
app.device =
{
    //操作系统
    //platform: "",
    //uuid
    uuid: "1",
    //手机系统版本
   // phoneVersion: "",
    //手机型号
    //phoneModel: "",
    //屏幕宽
    //screenWith: "",
    //屏幕高
    //screenHeight: "",
    //app类型
    //appType: ""
};
app.userAgentHeader = {
    _uuid: app.device.uuid,
};

//获取基础信息
app.getBaseData = function (callback) {

    invokeAPISync(
        app.apiUrl.GetBaseData,
        null,
        function (res) {
            if (res.m == "1") {

                if (res.d) {
                    data.ngList = res.d.ngList;
                    data.atList = res.d.atList;
                }

                if (typeof (callback) == "function") {
                    callback();
                }
            }
            else {
                console.log(res.m + res.e);
            }
        },
        function (err) {
            console.log(err);
            alertBox();
        },
        function () { showLoading(); },
        function () { }
    );
};


app.scrollerInit = function () {

    var viewName = mad.currentRouteName;
    if (!mad.controllers[viewName] || app.views[viewName])
        return;


    var scrollerSection = $("#" + viewName).find("[data-role='scrollerSection']")[0];
    if (scrollerSection == null) { return; }

    app.views[viewName] = {
        _minScrollYs: $('#' + viewName).find(".pullDown").attr("data-minScrollY") || 0,
        _pullDownHeight: $('#' + viewName).find(".pullDown").attr("data-height") || 0
    };

    (function (viewName) {
        var pullDown = $('#' + viewName + ' .pullDown');
        if (app.views[viewName]._pullDownHeight > 0) {
            pullDown.css("height", app.views[viewName]._pullDownHeight + "px");
        }
        else if (app.views[viewName]._minScrollYs > 0) {
            pullDown.css("height", app.views[viewName]._minScrollYs + "px");
        }
        var pullUp = $('#' + viewName + ' .pullUp');
        if (pullUp[0] != null) { pullUp.addClass("noIcon"); }

        var option = {};

        if (pullDown[0] != null) {

            option = {
                useTransition: false,
                fadeScrollbar: true,
                topOffset: app.views[viewName]._minScrollYs || 0, //pullDownOffset,
                onRefresh: function () {
                    if (pullDown.hasClass('loading')) {
                        pullDown.removeClass('loading');
                        pullDown.hasClass('no');

                        pullDown.find('.pullDownLabel').html('下拉可以刷新...');
                    }
                    if (pullUp.hasClass('loading')) {
                        pullUp.removeClass('loading');
                        pullUp.addClass('noIcon');
                        pullUp.find('.pullUpLabel').html('');
                    }
                },
                //onBeforeScrollMove: function () {
                //    if (this.y > this.minScrollY + 3 && typeof mad.controllers[viewName].module.page_onBeforeScrollMove == "function") {
                //        mad.controllers[viewName].module.page_onBeforeScrollMove();
                //    }
                //},
                onScrollMove: function () {

                    if (window.hideBrowserTop) { window.hideBrowserTop(); }

                    if (this.y > 5 && !pullDown.hasClass('flip')) {
                        pullDown.addClass('flip');
                        pullDown.find('.pullDownLabel').html('松开刷新...');
                        this.minScrollY = 0;
                    } else if (this.y < 5 && pullDown.hasClass('flip')) {
                        pullDown.removeClass('flip');
                        pullDown.find('.pullDownLabel').html('下拉可以刷新...');
                        this.minScrollY = -app.views[viewName]._minScrollYs || 0;
                    }
                    else if (pullUp[0] != null && !pullUp.hasClass('loading') && pullUp.css("display") != "none" && this.y < this.maxScrollY + 35) {
                        //pullUp.removeClass('flip');
                        pullUp.addClass('loading');
                        pullUp.find('.pullUpLabel').html('加载中...');

                        if (typeof (mad.controllers[viewName].module.scrollerMore) == "function" && viewName == mad.currentRouteName) {

                            if (!app.views[viewName].moreFlag) {
                                app.views[viewName].moreFlag = true;
                                mad.controllers[viewName].module.scrollerMore();
                            }
                        }
                        //else
                        //    this.refresh();
                    }
                    //else if (this.y < (this.maxScrollY - 5) && !pullUp.hasClass('flip')) {
                    //    pullUp.addClass('flip');
                    //    pullUp.find('.pullUpLabel').html('松开加载...');
                    //    this.maxScrollY = this.maxScrollY;
                    //} else if (this.y > (this.maxScrollY + 5) && pullUp.hasClass('flip')) {
                    //    pullUp.removeClass('flip');
                    //    pullUp.find('.pullUpLabel').html('上拉加载更多...');
                    //    //this.maxScrollY = pullUp.offsetHeight;
                    //}
                },
                //onBeforeScrollEnd: function () {
                //
                //    if (typeof app.controller.routes[viewName].module.page_onScrollEnd == "function") {
                //        app.controller.routes[viewName].module.page_onScrollEnd();
                //    }
                //},
                onScrollEnd: function () {

                    if (pullDown.hasClass('flip')) {
                        pullDown.removeClass('flip');
                        pullDown.addClass('loading');

                        pullDown.find('.pullDownLabel').html('加载中...');

                        if (typeof (mad.controllers[viewName].module.scrollerRefresh) == "function") {
                            mad.controllers[viewName].module.scrollerRefresh();
                        }
                        //else
                        //    this.refresh();
                    }
                    else if (pullUp[0] != null && !pullUp.hasClass('loading') && pullUp.css("display") != "none" && this.y < this.maxScrollY + 35) {
                        //pullUp.removeClass('flip');
                        pullUp.addClass('loading');
                        pullUp.find('.pullUpLabel').html('加载中...');

                        if (typeof (mad.controllers[viewName].module.scrollerMore) == "function" && viewName == mad.currentRouteName) {
                            if (!app.views[viewName].moreFlag) {
                                app.views[viewName].moreFlag = true;
                                mad.controllers[viewName].module.scrollerMore();
                            }
                        }
                        //else
                        //    this.refresh();
                    }
                    //else this.refresh();
                }
            };

        }

        app.views[viewName]._scroll = new iScroll(scrollerSection, option);

    })(viewName);

};


var startapp = {
    initialize: function () {
        this.bindEvents();
    },
    // Bind Event Listeners
    //
    // Bind any events that are required on startup. Common events are:
    // `load`, `deviceready`, `offline`, and `online`.
    bindEvents: function () {
        //document.addEventListener('deviceready', this.onDeviceReady, false);
        this.onDeviceReady();
    },
    // deviceready Event Handler
    //
    // The scope of `this` is the event. In order to call the `receivedEvent`
    // function, we must explicity call `app.receivedEvent(...);`
    onDeviceReady: function () {
        startapp.receivedEvent();
        document.addEventListener("backbutton", startapp.eventBackButton, false); // 返回键
    },
    // 返回键
    eventBackButton: function () {
        if ("newsList|eventList|magazList|videoList|myHome".indexOf(mad.currentRouteName) > -1) {
            if (backNum < 2) {
                backNum = backNum + 1;

                showTip("再按一次退出应用", null, null, function () {
                    backNum = 1;
                });
            }
            else {
                navigator.app.exitApp();
            }
        }
        else {
            mad.app.back("slideRight", null, null);
        }
    },
    receivedEvent: function () {
        if (typeof cordova != "undefined") {
            //app.dbContext = cordova.plugins.sqlLite;
            if (device) {
                //alert(JSON.stringify(device));
                for (var i in device) {
                    if (device.hasOwnProperty(i)) {
                        app.device[i] = device[i];
                    }
                }
                                
            }
            
        }
        app.pageInit();
    }
};

