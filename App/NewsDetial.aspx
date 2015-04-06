<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsDetial.aspx.cs" Inherits="NewsDetial" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>地产新闻-----中国房地产金融</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="中国房地产金融">
    <meta name="keywords" content="中国房地产金融">

    <link rel="stylesheet" href="css/jquery.mobile-1.3.2.min.css">
    <link rel="stylesheet" href="css/style.css" type="text/css">

    <script src="js/jquery-1.9.1.min.js"></script>
    <script src="js/jquery.mobile-1.3.2.min.js"></script>
</head>
<body>
    <header class="head">
        <h2 class="goBack" onclick="$.mobile.changePage('newslist.aspx', { transition: 'slide' });"></h2>
        <%=_nowGroup.GroupName %>
        <h3 class="noticeIcon"></h3>
    </header>
    <!--head end of-->
    <section class="flashPic">

        <dl><%=_news.Title %></dl>
        <img src="<%=Biz.GetImageSrc(_news.ImgUrl) %>" />

        <div class="line">
            <ul>
                <li class="redLine"></li>
                <li class="blueLine"></li>
            </ul>
        </div>
    </section>
    <!--flashPic end of-->
    <section class="newsCotnet">
        <%=_news.Content %>
    </section>
    <!--newCotnet end of-->
    <footer class="newsFoot">
        <a href="javascript:;">
            <dl>
                <dt class="newfootIcon_1"></dt>
                <dd>收藏</dd>
            </dl>
        </a>
        <a href="javascript:;">
            <dl>
                <dt class="newfootIcon_2"></dt>
                <dd>分享</dd>
            </dl>
        </a>
        <a href="javascript:;" id="newfootIcon_3">
            <dl>
                <dt class="newfootIcon_3"></dt>
                <dd>放大</dd>
            </dl>
        </a>
        <a href="javascript:;">
            <dl>
                <dt class="newfootIcon_4"></dt>
                <dd>缩小</dd>
            </dl>
        </a>
        <a href="javascript:;" class="newsFootBg">
            <dl>
                <dd class="searchIcon"></dd>
            </dl>
        </a>
    </footer>
    <script>
        //newfootIcon_3
        $(".newfootIcon_3").click(function () {
            var s = parseInt($(".newsCotnet > p").css("font-size"));
            //alert(s);
            if (s < 26)
                $(".newsCotnet > p").css("font-size", (s + 2) + "px");
        })
        $(".newfootIcon_4").click(function () {
            var s = parseInt($(".newsCotnet > p").css("font-size"));
            if (s > 14)
                $(".newsCotnet > p").css("font-size", (s - 2) + "px");
        })
    </script>
</html>
