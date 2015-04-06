<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventList.aspx.cs" Inherits="EventList" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>品牌   金融峰会</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="地产新闻">
    <meta name="keywords" content="地产新闻">


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
            <h1 class="fl list"><a><i class="listIcon"></i>品牌  | 金融峰会</a></h1>
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
            <section class="flash">
                <div class="banner">
                    <div id="wrapper" style="overflow: hidden">
                        <div id="scroller" style="width: 2826px;">
                            <ul id="thelist">
                                <li>
                                    <p class="thelistp">厦大经济学家判断:中国房地产</p>
                                    <a href="#">
                                        <img src="images/flash/flash_1.png" style="width: 1413px;"></a></li>
                                <li>
                                    <p class="thelistp">意外的恋爱时光 Love Speaks (2013)</p>
                                    <a href="#">
                                        <img src="images/flash/flash_2.png" style="width: 1413px;"></a></li>
                                <li>
                                    <p class="thelistp">金枝欲孽2 金枝慾孽貳 (2013)</p>
                                    <a href="#">
                                        <img src="images/flash/flash_3.png" style="width: 1413px;"></a></li>
                            </ul>
                        </div>
                    </div>
                    <div id="nav">
                        <div id="prev" onclick="myScroll.scrollToPage(&#39;prev&#39;, 0,400,3);return false">← prev</div>
                        <ul id="indicator">
                            <li class="">1</li>
                            <li class="">1</li>
                            <li class="active">2</li>
                        </ul>
                        <div id="next" onclick="myScroll.scrollToPage(&#39;next&#39;, 0);return false">next →</div>
                    </div>
                    <div class="clr"></div>
                </div>
            </section>
            <!--flash end of-->
            <section class="line">
                <ul>
                    <li class="redLine"></li>
                    <li class="blueLine"></li>
                </ul>
            </section>
            <!--line end of-->
            <section class="eventList">
                <ul>
                    <li>
                        <a href="#">
                            <p>银行间市场拟试水项目收益票据 地方融资再开</p>
                            <div class="pic_box">
                                <img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" />
                            </div>
                            <div class="article_box">事实上，在17号文、再贷款传闻、央行正回购缩量、总理表示政策工具要“适时适度预调微调”等多项利好因素刺激下</div>
                            <div class="time_box"><cite>举办时间：2014-06-10 00:00</cite></div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <p>银行间市场拟试水项目收益票据 地方融资再开</p>
                            <div class="pic_box">
                                <img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" />
                            </div>
                            <div class="article_box">事实上，在17号文、再贷款传闻、央行正回购缩量、总理表示政策工具要“适时适度预调微调”等多项利好因素刺激下</div>
                            <div class="time_box"><cite>举办时间：2014-06-10 00:00</cite></div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <p>银行间市场拟试水项目收益票据 地方融资再开</p>
                            <div class="pic_box">
                                <img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" />
                            </div>
                            <div class="article_box">事实上，在17号文、再贷款传闻、央行正回购缩量、总理表示政策工具要“适时适度预调微调”等多项利好因素刺激下</div>
                            <div class="time_box"><cite>举办时间：2014-06-10 00:00</cite></div>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <p>银行间市场拟试水项目收益票据 地方融资再开</p>
                            <div class="pic_box">
                                <img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀" />
                            </div>
                            <div class="article_box">事实上，在17号文、再贷款传闻、央行正回购缩量、总理表示政策工具要“适时适度预调微调”等多项利好因素刺激下</div>
                            <div class="time_box"><cite>举办时间：2014-06-10 00:00</cite></div>
                        </a>
                    </li>
                </ul>
            </section>
            <!--eventList end of-->
        </div>
        <!--content end of-->
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
