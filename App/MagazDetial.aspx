﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MagazDetial.aspx.cs" Inherits="MagazDetial" %>

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
    <link rel="stylesheet" href="css/style.css" type="text/css">
</head>
<body>
    <header class="head">
        <h2 class="goBack"><a href="javascript:return_prepage();"></a></h2>
        <a href="#" class="collect" id="collect" onclick="collect()"></a>
        <a href="#" class="share"></a>
    </header>
    <!--head end of-->
    <section class="flashPic">
        <a href="#"><img src="images/mainphoto.png" alt="" /></a>
    </section>
    <!--flashPic end of-->
    <script src="js/jquery-1.9.1.min.js"></script>
    <script>
        var isShow = true;
        $("#collect").click(function () {
            if (isShow) {
                $(".collect").removeClass('collect').addClass('collectHead');
                isShow = false;
                return false;
            } else {
                $(".collect").removeClass('collectHead').addClass('collect');
                isShow = true;
                return false;
            }
        })
        function return_prepage() {
            if (window.document.referrer == "" || window.document.referrer == window.location.href) {
                window.location.href = "{dede:type}[field:typelink /]{/dede:type}";
            } else {
                window.location.href = window.document.referrer;
            }
        }
    </script>
</html>
