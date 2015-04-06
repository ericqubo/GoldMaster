using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// WebBiz 的摘要说明
/// </summary>
public class WebBiz
{
	public WebBiz()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string GetMenuStr(string now)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<li"+(now=="index"?" class=\"Selected\"":"")+"><a href=\"index.aspx\"><i class=\"navIcon_1\"></i>首页</a></li>");
        sb.AppendLine("<li" + (now == "fav" ? " class=\"Selected\"" : "") + "><a href=\"fav.html\"><i class=\"navIcon_1\"></i>我的收藏</a></li>");
        sb.AppendLine("<li" + (now == "daliy" ? " class=\"Selected\"" : "") + "><a href=\"daliy.html\"><i class=\"navIcon_2\"></i>每日精选</a></li>");
        sb.AppendLine("<li" + (now == "newslist45" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=45\"><i class=\"navIcon_3\"></i>特别报道</a></li>");
        sb.AppendLine("<li" + (now == "newslist24" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=24\"><i class=\"navIcon_3\"></i>地产新闻</a></li>");
        sb.AppendLine("<li" + (now == "newslist25" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=25\"><i class=\"navIcon_4\"></i>金融新闻</a></li>");
        sb.AppendLine("<li" + (now == "newslist26" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=26\"><i class=\"navIcon_5\"></i>上市公司</a></li>");
        sb.AppendLine("<li" + (now == "newslist27" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=27\"><i class=\"navIcon_6\"></i>楼市评论</a></li>");
        sb.AppendLine("<li" + (now == "event5" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=5\"><i class=\"navIcon_7\"></i>金融峰会</a></li>");
        sb.AppendLine("<li" + (now == "event1" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=1\"><i class=\"navIcon_8\"></i>Leader Club</a></li>");
        sb.AppendLine("<li" + (now == "event2" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=2\"><i class=\"navIcon_9\"></i>房地产金融理事会</a></li>");
        sb.AppendLine("<li" + (now == "event3" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=3\"><i class=\"navIcon_10\"></i>金主高尔夫</a></li>");
        sb.AppendLine("<li" + (now == "event4" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=4\"><i class=\"navIcon_11\"></i>臻品私享</a></li>");
        sb.AppendLine("<li" + (now == "magaz" ? " class=\"Selected\"" : "") + "><a href=\"MagazList.aspx\"><i class=\"navIcon_12\"></i>杂志</a></li>");
        return sb.ToString();
    }
    public static string GetMenuStr()
    {
        string now = "index";
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<li" + (now == "index" ? " class=\"Selected\"" : "") + "><a href=\"index.aspx\"><i class=\"navIcon_1\"></i>首页</a></li>");
        sb.AppendLine("<li" + (now == "fav" ? " class=\"Selected\"" : "") + "><a href=\"fav.html\"><i class=\"navIcon_1\"></i>我的收藏</a></li>");
        sb.AppendLine("<li" + (now == "daliy" ? " class=\"Selected\"" : "") + "><a href=\"daliy.html\"><i class=\"navIcon_2\"></i>每日精选</a></li>");
        sb.AppendLine("<li" + (now == "newslist45" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=45\"><i class=\"navIcon_3\"></i>特别报道</a></li>");
        sb.AppendLine("<li" + (now == "newslist24" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=24\"><i class=\"navIcon_3\"></i>地产新闻</a></li>");
        sb.AppendLine("<li" + (now == "newslist25" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=25\"><i class=\"navIcon_4\"></i>金融新闻</a></li>");
        sb.AppendLine("<li" + (now == "newslist26" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=26\"><i class=\"navIcon_5\"></i>上市公司</a></li>");
        sb.AppendLine("<li" + (now == "newslist27" ? " class=\"Selected\"" : "") + "><a href=\"newslist.aspx?id=27\"><i class=\"navIcon_6\"></i>楼市评论</a></li>");
        sb.AppendLine("<li" + (now == "event5" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=5\"><i class=\"navIcon_7\"></i>金融峰会</a></li>");
        sb.AppendLine("<li" + (now == "event1" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=1\"><i class=\"navIcon_8\"></i>Leader Club</a></li>");
        sb.AppendLine("<li" + (now == "event2" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=2\"><i class=\"navIcon_9\"></i>房地产金融理事会</a></li>");
        sb.AppendLine("<li" + (now == "event3" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=3\"><i class=\"navIcon_10\"></i>金主高尔夫</a></li>");
        sb.AppendLine("<li" + (now == "event4" ? " class=\"Selected\"" : "") + "><a href=\"eventlist.aspx?id=4\"><i class=\"navIcon_11\"></i>臻品私享</a></li>");
        sb.AppendLine("<li" + (now == "magaz" ? " class=\"Selected\"" : "") + "><a href=\"MagazList.aspx\"><i class=\"navIcon_12\"></i>杂志</a></li>");
        return sb.ToString();
    }
}