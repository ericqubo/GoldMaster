<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Daliy.aspx.cs" Inherits="Daliy" %>

<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<title>每日精选</title>
<meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="format-detection" content="telephone=no">
<meta name="description" content="地产新闻">
<meta name="keywords" content="地产新闻">
<link rel="stylesheet" href="css/style.css" type="text/css">
</head>
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
<body>
<div id="page">
  <header class="head">
    <h1 class="fl list"><a href="#menu"><i class="listIcon"></i>每日精选</a></h1>
  </header>
  <!--head end of-->
  <div class="content">
    <section class="flash">
      <div class="banner">
          <div id="wrapper" style="overflow:hidden">
             <div id="scroller" style="width:2826px;">
                <ul id="thelist"> 
                    <li><p style="font-size:9px">厦大经济学家判断:中国房地产</p><a href="#"><img src="images/flash/flash_1.png" style="width: 1413px;"></a></li>
                    <li><p style="font-size:9px">意外的恋爱时光 Love Speaks (2013)</p><a href="#"><img src="images/flash/flash_2.png"style="width: 1413px;"></a></li>
                    <li><p style="font-size:9px">金枝欲孽2 金枝慾孽貳 (2013)</p><a href="#"><img src="images/flash/flash_3.png" style="width: 1413px;"></a></li>		
                </ul>
            </div>
          </div>
          <div id="nav">
              <div id="prev" onClick="myScroll.scrollToPage(&#39;prev&#39;, 0,400,3);return false">← prev</div>
              <ul id="indicator">
                 <li class="">1</li><li class="">1</li><li class="active">2</li>			 
              </ul>
              <div id="next" onClick="myScroll.scrollToPage(&#39;next&#39;, 0);return false">next →</div>
          </div>
          <div class="clr"></div>
      </div>
    </section>
    <!--flash end of-->
    <section class="line"><ul><li class="redLine"></li><li class="blueLine"></li></ul> </section>
    <!--line end of-->
    <section class="newsList">
      <ul>
        <li>
           <a href="#">
             <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀"/></div>
             <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
             <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
             <div class="time_box">
                <span>36</span> <code>36</code> <cite>5月16日</cite>
             </div>
           </a>
        </li>
        <li>
           <a href="#">
             <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀"/></div>
             <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
             <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
             <div class="time_box">
                <span>36</span> <code>36</code> <cite>5月16日</cite>
             </div>
           </a>
        </li>
        <li>
           <a href="#">
             <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀"/></div>
             <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
             <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
             <div class="time_box">
                <span>36</span> <code>36</code> <cite>5月16日</cite>
             </div>
           </a>
        </li>
        <li>
           <a href="#">
             <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀"/></div>
             <div class="title_box">除房企谁还在淘金地产:险成新秀</div>
             <div class="active_box">前天（5月29日），市房地产经纪业商苏州房地产经纪业自律公约》，40</div>
             <div class="time_box">
                <span>36</span> <code>36</code> <cite>5月16日</cite>
             </div>
           </a>
        </li>
        <li>
           <a href="#">
             <div class="pic_box"><img src="images/flash/newsPic.png" alt="除房企谁还在淘金地产:险成新秀"/></div>
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
