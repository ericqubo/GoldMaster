﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fav.aspx.cs" Inherits="Fav" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>我的收藏 </title>
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="地产新闻">
    <meta name="keywords" content="地产新闻">
    <link rel="stylesheet" href="css/style.css" type="text/css">
</head>
<body>
    <div id="page">
        <header class="head">
            <h1 class="fl list"><a href="#menu"><i class="listIcon"></i>我的收藏 | </a><span>新闻收藏</span> <span class="onSpan">杂志收藏</span></h1>
        </header>
        <!--head end of-->
        <div class="subNav" id="subNav">
            <ul>
                <code></code>
                <li><a href="#"><i class="subIcon_1"></i>地产新闻</a></li>
                <li><a href="#"><i class="subIcon_2"></i>金融地产</a></li>
                <li><a href="#"><i class="subIcon_3"></i>上市公司</a></li>
                <li class="noLine"><a href="#"><i class="subIcon_4"></i>楼市评论</a></li>
            </ul>
        </div>
        <!--subNav end of-->
        <div class="content">
            <section class="line"><ul><li class="redLine"></li><li class="blueLine"></li></ul> </section>
            <!--line end of-->
            <section class="newsList">
                <ul>
                    <li>
                        <a href="#">
                            <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" /></div>
                            <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
                            <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
                            <div class="time_box">
                                <span>36</span> <code>36</code> <cite>5月16日</cite>
                            </div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" /></div>
                            <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
                            <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
                            <div class="time_box">
                                <span>36</span> <code>36</code> <cite>5月16日</cite>
                            </div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" /></div>
                            <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
                            <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
                            <div class="time_box">
                                <span>36</span> <code>36</code> <cite>5月16日</cite>
                            </div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" /></div>
                            <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
                            <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
                            <div class="time_box">
                                <span>36</span> <code>36</code> <cite>5月16日</cite>
                            </div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" /></div>
                            <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
                            <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
                            <div class="time_box">
                                <span>36</span> <code>36</code> <cite>5月16日</cite>
                            </div>
                        </a>
                    </li>
                </ul>
            </section>
            <!--newsList end of-->
        </div>
        <!--content end of-->
        <nav id="menu">
            <ul>
                <%=WebBiz.GetMenuStr() %>
            </ul>
        </nav>
        <!--menu end of-->
    </div>
    <!--page end of-->
    <script src="js/jquery-1.9.1.min.js"></script>
    <script src="js/jquery.mmenu.min.js"></script>
    <script src="js/base.js"></script>
</body>
</html>
