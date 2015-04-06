
//测试调用
if (_type == 0) {
    startapp.onDeviceReady();
} else {
    $("body").removeClass("bgLoadPre");
    $(".preLoadRefresh").remove();
    startapp.initialize();
}
