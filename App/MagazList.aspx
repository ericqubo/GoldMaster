<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MagazList.aspx.cs" Inherits="MagazList" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>杂志-----中国房地产金融</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="中国房地产金融">
    <meta name="keywords" content="中国房地产金融">


    <link rel="stylesheet" href="css/style.css" type="text/css">

    <script src="js/jquery-1.9.1.min.js"></script>


</head>
<body>
    <div id="page">
        <header class="head">
            <h1 class="fl list"><a><i class="listIcon"></i>杂志</a></h1>
        </header>
        <!--head end of-->
        <div class="content">
            <section class="line">
                <ul>
                    <li class="redLine"></li>
                    <li class="blueLine"></li>
                </ul>
            </section>
            <!--line end of-->
            <section class="magazList">
                <ul>
                    <dl>
                        <dt><a href="#">
                            <img src="images/magazList_1.png" alt="独家专访任志强独家专" /></a></dt>
                        <dd>独家专访任志强独家专</dd>
                    </dl>
                    <dl>
                        <dt><a href="#">
                            <img src="images/magazList_2.png" alt="独家专访任志强独家专" /></a></dt>
                        <dd>独家专访任志强独家专</dd>
                    </dl>
                    <dl>
                        <dt><a href="#">
                            <img src="images/magazList_3.png" alt="独家专访任志强独家专" /></a></dt>
                        <dd>独家专访任志强独家专</dd>
                    </dl>
                    <dl>
                        <dt><a href="#">
                            <img src="images/magazList_1.png" alt="独家专访任志强独家专" /></a></dt>
                        <dd>独家专访任志强独家专</dd>
                    </dl>
                </ul>
            </section>
            <!--homeContent end of-->
        </div>
        <!--content end of-->

        <!--menu end of-->
    </div>
    <!--page end of-->
    <footer class="newsFoot">
        <a href="newslist.aspx">
            <dl>
                <dt class="newfootIcon_1"></dt>
                <dd>新闻</dd>
            </dl>
        </a>
        <a href="EventList.aspx">
            <dl>
                <dt class="newfootIcon_2"></dt>
                <dd>品牌</dd>
            </dl>
        </a>
        <a href="magazlist.aspx">
            <dl>
                <dt class="newfootIcon_3"></dt>
                <dd>杂志</dd>
            </dl>
        </a>
        <a href="videolist.aspx">
            <dl>
                <dt class="newfootIcon_4"></dt>
                <dd>视频</dd>
            </dl>
        </a>
        <a href="my.aspx">
            <dl>
                <dt class="newfootIcon_4"></dt>
                <dd>我的</dd>
            </dl>
        </a>
    </footer>
    <script src="js/base.js"></script>
</body>
</html>
