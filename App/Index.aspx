<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>首页-中国房地产金融</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="中国房地产金融">
    <meta name="keywords" content="中国房地产金融">
    <link rel="stylesheet" href="css/style.css" type="text/css">

    <script src="js/jquery-1.9.1.min.js"></script>
    <script src="js/dragmin.js"></script>
    <script>
        var myScroll;
        function loaded() {
            myScroll = new iScroll('wrapper', {
                snap: true,
                momentum: false,
                hScrollbar: false,
                onScrollEnd: function () {
                    document.querySelector('#indicator > li.active').className = '';
                    document.querySelector('#indicator > li:nth-child(' + (this.currPageX + 1) + ')').className = 'active';
                }
            });
        }
        document.addEventListener('DOMContentLoaded', loaded, false);
    </script>
</head>
<body>
    <div id="page">
        <header class="head">
            <h1 class="fl list"><a><i class="listIcon"></i>中国房地产金融</a></h1>
        </header>
        <!--head end of-->
        <div class="content">
            <section class="flash">
                <div class="banner">
                    <div id="wrapper" style="overflow:hidden">
                        <div id="scroller" style="width:2826px;">
                            <ul id="thelist">
                                <li><p class="thelistp">厦大经济学家判断:中国房地产</p><a href="#"><img src="images/flash/flash_1.png" style="width: 1413px;"></a></li>
                                <li><p class="thelistp">意外的恋爱时光 Love Speaks (2013)</p><a href="#"><img src="images/flash/flash_2.png" style="width: 1413px;"></a></li>
                                <li><p class="thelistp">金枝欲孽2 金枝慾孽貳 (2013)</p><a href="#"><img src="images/flash/flash_3.png" style="width: 1413px;"></a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="nav">
                        <div id="prev" onclick="myScroll.scrollToPage('prev', 0,400,3);return false">← prev</div>
                        <ul id="indicator">
                            <li class="">1</li>
                            <li class="">1</li>
                            <li class="active">2</li>
                        </ul>
                        <div id="next" onclick="myScroll.scrollToPage('next', 0);return false">next →</div>
                    </div>
                    <div class="clr"></div>
                </div>
            </section>
            <!--flash end of-->
            <section class="line"><ul><li class="redLine"></li><li class="blueLine"></li></ul> </section>
            <!--line end of-->
            <section class="homeContent">
                <ul>
                    <li onclick="location.href = 'fav.html';"><p>我的收藏</p><img src="images/homePic_1.png" alt="我的收藏" /></li>
                    <li onclick="location.href = 'daliy.html';"><p>每日精选</p><img src="images/homePic_2.png" alt="每日精选" /></li>
                    <li onclick="location.href = 'newslist.aspx';"><dl><dt>新闻中心</dt><dd>News</dd></dl><img src="images/homePic_3.png" alt="新闻中心" /></li>
                    <li onclick="location.href = 'newslist.aspx?id=45';"><dl><dt>特别报道</dt><dd>Story</dd></dl><img src="images/homePic_4.png" alt="特别报道" /></li>
                    <li onclick="location.href = 'eventList.aspx';"><dl><dt>品牌</dt><dd>Brands</dd></dl><img src="images/homePic_5.png" alt="品牌" /></li>
                    <li onclick="location.href = 'magazlist.aspx';"><dl><dt>杂志</dt><dd>Magazine</dd></dl><img src="images/homePic_6.png" alt="杂志" /></li>
                </ul>
            </section>
            <!--homeContent end of-->
        </div>
        <!--content end of-->

        <!--menu end of-->
    </div>
    <!--page end of-->
    <script>
        var count = document.getElementById("thelist").getElementsByTagName("img").length;
        for (i = 0; i < count; i++) {
            document.getElementById("thelist").getElementsByTagName("img").item(i).style.cssText = " width:" + document.body.clientWidth + "px";
        }
        document.getElementById("scroller").style.cssText = " width:" + document.body.clientWidth * count + "px";
        setInterval(function () {
            myScroll.scrollToPage('next', 0, 400, count);
        }, 3500);
        window.onresize = function () {
            for (i = 0; i < count; i++) {
                document.getElementById("thelist").getElementsByTagName("img").item(i).style.cssText = " width:" + document.body.clientWidth + "px";
            }
            document.getElementById("scroller").style.cssText = " width:" + document.body.clientWidth * count + "px";
        }
    </script>
    <script src="js/base.js"></script>
</body>
</html>
