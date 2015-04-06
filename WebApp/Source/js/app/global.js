
var errTips = {
    global: "亲，您的请求失败了，请重试看看~"
};

var newsDateTitle = null;

var weekDict = {
    0: "星期日",
    1: "星期一",
    2: "星期二",
    3: "星期三",
    4: "星期四",
    5: "星期五",
    6: "星期六"
};

//#region alertBox mask

//弹出框
function alertBox(context, title, isOk, isCancel, okCallback, cancelCallback, okBtnCon, cancelBtnCon, isShowMask) {

    isShowMask = isShowMask == null || isShowMask ? true : false;
    title = title || "提示";
    isOk = isOk == null || isOk ? true : false;
    isCancel = isCancel || false;
    okBtnCon = okBtnCon || "确认";
    cancelBtnCon = cancelBtnCon || "取消";
    $(".alertBox .alertTitle").html(title);
    $(".alertBox .alertText").html(context);
    $(".alertBox .btnOk").html(okBtnCon);
    $(".alertBox .btnCancel").html(cancelBtnCon);
    if (isOk) {
        $(".alertBox .btnOk").show();
    } else {
        $(".alertBox .btnOk").hide();
    }
    if (isCancel) {
        $(".alertBox .btnCancel").show();
    } else {
        $(".alertBox .btnCancel").hide();
    }

    $(".alertBox .btnOk").die();
    okCallback = okCallback || function () {
        $(".alertBox").hide();
        hideMask();
    }
    $(".alertBox .btnOk").globalTapLive(okCallback)

    $(".alertBox .btnCancel").die();
    cancelCallback = cancelCallback || function () {
        $(".alertBox").hide();
        hideMask();
    }
    $(".alertBox .btnCancel").globalTapLive(cancelCallback)

    //$(".alertBox").show();
    if ($(".alertBox").css("display") == "none") {
        $(".alertBox").css("opacity", 0).show().animate({ "opacity": 1 }, 200, "ease-out");
    }

    if (isShowMask) {
        showMask();
    }
}

function hideBox() {
    $(".alertBox").hide();
}

function showTip(content, time, icon, callBack) {

    time = time || 2;
    icon = icon || 0; //默认出现 提示&报错icon 
    callBack = callBack || null;

    $(".tip .alertText").html(content);
    $(".tip").show();
    setTimeout(function () {
        $(".tip").hide();
        if (typeof (callBack) == "function") {
            callBack();
        }
    }, time * 1000);
}

function showLoading(parentEle, content) {
    showMask(parentEle);

    content = content || "努力加载中，请稍候...";
    $(".ajaxLoading .loading-content").html(content);
    //$(".ajaxLoading").css("opacity", 0);
    $(".ajaxLoading").show();
    //$(".ajaxLoading").animate({ "opacity": 0.8 }, 200, "ease-out");
}

function hideLoading() {
    hideMask();
    $(".ajaxLoading").hide();
    $(".ajaxLoadingHighlight").hide();

    //$(".ajaxLoading").animate({ "opacity": 0 }, 100, "ease-out", function () {
    //    $(".ajaxLoading").hide();
    //});
}

function showLoadingHighlight(parentEle, content) {

    var $ajaxLoadingHighlight = $(".ajaxLoadingHighlight");

    if ($(parentEle)[0] != null) {
        if ($(parentEle).children(".ajaxLoadingHighlight")[0] == null) {
            $(parentEle).append($ajaxLoadingHighlight);
        }
    }
    else {
        if ($("body").children(".ajaxLoadingHighlight")[0] == null) {
            $("body").append($ajaxLoadingHighlight);
        }
    }

    content = content || "努力加载中，请稍候...";
    $(".ajaxLoadingHighlight .loading-content").html(content);
    $(".ajaxLoadingHighlight").show();
}

var $mask = $(".mask");
function showMask(parentEle) {

    if ($(parentEle)[0] != null) {
        if ($(parentEle).children(".mask")[0] == null) {
            $(parentEle).append($mask);
        }
    }
    else {
        if ($("body").children(".mask")[0] == null) {
            $("body").append($mask);
        }
    }

    $mask.show();

    //$(".mask").css("opacity", 0);
    //$(".mask").show();
    //$(".mask").animate({ "opacity": 0.3 }, 200, "ease-out");
}

function hideMask() {

    $mask.hide();

    //$(".mask").animate({ "opacity": 0 }, 100, "ease-out", function () {
    //    $(".mask").hide();
    //    $("body").prepend($(".mask"));
    //});
}

//#endregion
