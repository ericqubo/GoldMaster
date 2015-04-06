<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventYuYue.aspx.cs" Inherits="EventYuYue" %>

<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
<title>活动预约-----中国房地产金融</title>
<meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="format-detection" content="telephone=no">
<meta name="description" content="中国房地产金融">
<meta name="keywords" content="中国房地产金融">
<link rel="stylesheet" href="css/style.css" type="text/css">
</head>
<body>
<header class="head">
  <h2 class="goBack"><a href="javascript:return_prepage();"></a></h2>活动预约
</header>
<!--head end of-->
<section class="line"><ul><li class="redLine"></li><li class="blueLine"></li></ul> </section>
<!--line end of-->
<section class="doing">
  <h2>银行间市场拟试水项目收益票据 地方融资再开正门</h2>
  <ul>
   <form>
    <li><span>姓名：</span><code><input type="text" class="doingInput"></code></li>
    <li><span>电话：</span><code><input type="tel" class="doingInput"></code></li>
    <li><span>邮箱：</span><code><input type="email" class="doingInput"></code></li>
    <li><span>从事行业：</span><code><input type="text" class="doingInput"></code></li>
    <li><span>所在地区：</span><code><input type="text" class="doingInput"></code></li>
    <li style="text-align:center"><a href="#">立即预约</a></li>
   </form> 
  </ul>
</section>
<script>
    function return_prepage() {
        if (window.document.referrer == "" || window.document.referrer == window.location.href) {
            window.location.href = "{dede:type}[field:typelink /]{/dede:type}";
        } else {
            window.location.href = window.document.referrer;
        }
    }
</script>
</body>  
</html>
