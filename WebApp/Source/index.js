var appJson;
$.ajax({
    url: _apiDomain + "Service/AppJson",
    type: "get",
    data: {
        //t: _type
    },
    success: function (res) {
        if (res) {
            appJson = res;
            
            if ("css" in appJson) {
                for (var i = 0 ; i < appJson.css.length; i++) {
                    if (appJson.css[i].path) {
                        var style = document.createElement("link");
                        style.setAttribute("rel", "stylesheet");
                        style.setAttribute("type", "text/css");
                        style.setAttribute("href", appJson.css[i].path);
                        $("head").append(style);
                    }
                }
            }
            if ("js" in appJson) {

                for (var i = 0 ; i < appJson.js.length; i++) {
                    if (appJson.js[i].path) {
                        var script = document.createElement("script");
                        script.setAttribute("type", "text/javascript");
                        script.setAttribute("src", appJson.js[i].path);
                        $("body").append(script);
                    }
                }
            }           

        }
    },
    error: function (err) {
        alert(JSON.stringify(err));
        alert("网络异常，加载失败");
    }
});