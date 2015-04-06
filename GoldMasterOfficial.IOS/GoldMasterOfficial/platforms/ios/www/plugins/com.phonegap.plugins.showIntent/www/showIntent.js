cordova.define("com.phonegap.plugins.showIntent.ShowIntent", function (require, exports, module) {
    var exec = require("cordova/exec");

    function ShowIntent() {

    }

    ShowIntent.prototype.settings = {
        WirelessSettings: "WirelessSettings",
        LocationSettings: "LocationSettings"
    };


    ShowIntent.prototype.ShowSystemPage = function (successCallback, errCallback, options) {
        if (errCallback == null) {
            errCallback = function () {
            };
        }
        if (typeof errCallback != "function") {

            return;
        }

        if (typeof successCallback != "function") {

            return;
        }
        exec(successCallback, errCallback, 'ShowIntent', 'ShowSystemPage', options);
    };
    var showIntent = new ShowIntent();
    module.exports = showIntent;
});