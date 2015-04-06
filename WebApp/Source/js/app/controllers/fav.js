/// <reference path="../_references.js" />


; (function () {

    var name = "fav";

    mad.view.definePage("fav", app.templateUrlDict.newsDetial);

    var module = {
    };

    var pageResize = function () {

    }

    mad.addController(name, {
        route: "fav/{id}",
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