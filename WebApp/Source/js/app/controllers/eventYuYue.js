/// <reference path="../../_references.js" />


; (function () {

    var name = "eventYuYue";

    mad.view.definePage("eventYuYue", app.templateUrlDict.eventYuYue);

    var module = {
        id: 0
    };

    var pageResize = function () {

    }

    mad.addController(name, {
        route: "eventYuYue/{id}",
        action: function (id) {
            module.id = id;
            app.scrollerInit();
        },
        init: function () {

            //预约
            $("#" + name + " #reserve").globalTapLive(function () {
                if ($(this).attr("data-flag") == 1)
                    return;

                var $thisPage = $("#" + name);
                var _name = $thisPage.find(".event-form [name='name']").val();
                var _mobile = $thisPage.find(".event-form [name='mobile']").val();
                var _mail = $thisPage.find(".event-form [name='mail']").val();
                var _industry = $thisPage.find(".event-form [name='industry']").val();
                var _address = $thisPage.find(".event-form [name='address']").val();

                if (util.isNullOrEmpty(_name)) {
                    alertBox("请输入姓名", "提示");
                    return;
                }
                if (util.isNullOrEmpty(_mobile)) {
                    alertBox("请输入电话", "提示");
                    return;
                }

                $("#eventYuYue #reserve").attr("data-flag", 0);

                invokeAPIAsync(app.apiUrl.ReserveEvent, {
                    name: _name,
                    mobile: _mobile,
                    mail: _mail,
                    industry: _industry,
                    address: _address,
                    id: module.id
                }, function (res) {
                    $("#eventYuYue #reserve").attr("data-flag", 0);
                    if (res.m == 1) {

                        showTip("提交成功");
                        mad.app.back("slideRight", false)
                    }
                    else {
                        if (res.e)
                            alertBox(res.e);
                        else
                            alertBox(errTips.global);
                    }
                }, function (err) {

                    $(this).attr("data-flag", 0);
                    alertBox("加载失败，请重试");
                });


            });
        },
        resize: function () {

        },
        module: module
    });
})();