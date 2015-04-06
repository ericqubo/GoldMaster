//subNav
$(function () {
    $('nav#menu').mmenu();
});
$(function () {
    $(".listArrow").click(function () {
        $(".subNav").show();
    })
    $(".subNav a").click(function () {
        $(".subNav").hide();
    })
});
//listArrow
var listArrow = document.getElementById('listArrow');
var subNav = document.getElementById('subNav');

listArrow.onclick = function (event) {
    subNav.style.display = "block";
    (event || window.event).cancelBubble = true;
};

document.onclick = function () {
    subNav.style.display = "none";
};

/*function browserRedirect() {  
	var sUserAgent = navigator.userAgent.toLowerCase();  
	var bIsIpad = sUserAgent.match(/ipad/i) == "ipad";  
	var bIsIphoneOs = sUserAgent.match(/iphone os/i) == "iphone os";  
	var bIsMidp = sUserAgent.match(/midp/i) == "midp";  
	var bIsUc7 = sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4";  
	var bIsUc = sUserAgent.match(/ucweb/i) == "ucweb";  
	var bIsAndroid = sUserAgent.match(/android/i) == "android";  
	var bIsCE = sUserAgent.match(/windows ce/i) == "windows ce";  
	var bIsWM = sUserAgent.match(/windows mobile/i) == "windows mobile";  
	if (!(bIsIpad || bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM) ){  
	    alert("不能用pc访问，另存不了")
		window.location.href="http://wwww.baidu.com/";
	}else{
		return false;
		window.location.href="http://jinjiang-group.com.cn/web7/newsList.html";
		
	}
}  
browserRedirect();  */