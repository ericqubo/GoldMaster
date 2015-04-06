cordova.define('cordova/plugin_list', function (require, exports, module) {
    module.exports = [
        {
            "file": "plugins/com.phonegap.plugins.sqllite/www/sqllite.js",
            "id": "com.phonegap.plugins.sqllite.SqlLite",
            "clobbers": [
                "cordova.plugins.sqlLite"
            ]
        },
        {
            "file": "plugins/org.apache.cordova.device/www/device.js",
            "id": "org.apache.cordova.device.device",
            "clobbers": [
                "device"
            ]
        }
    ]
});