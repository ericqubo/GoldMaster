/// <reference path="../_references.js" />


; (function () {

    var name = "fav_Magaz";

    mad.view.definePage("fav_Magaz", app.templateUrlDict.newsDetial);

    var module = {
    };

    var pageResize = function () {

    }

    mad.addController(name, {
        route: "fav_Magaz/{id}",
        action: function (id) {

            var $thisPage = $("#" + name);

            app.scrollerInit();
        },
        init: function () {

        },
        resize: function () {

        },
        module: module
    });
})();