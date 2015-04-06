using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Biz 的摘要说明
/// </summary>
public class Biz
{
    public Biz()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region 返回推荐新闻
    // 编辑推荐
    public static string GetReCommandNewsStr(int strType, int index, int count)
    {
        Model.News limit = new Model.News();
        limit.IsRecommend = 1;
        int smc = 0;
        List<Model.News> list = Model.News.GetInfoByLimit(null, null, limit, index, count, out smc);
        if (list != null && list.Count > 0)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var news in list)
            {
                switch (strType)
                {
                    default:
                    case 1:
                        sb.AppendLine("<li><a href=\"newsdetial.aspx?id=" + news.ID + "\">");
                        sb.AppendLine("<img width=\"185\" height=\"150\" src=\"" + Biz.GetImageSrc(news.ImgUrl) + "\" alt=\" \" /></a>");
                        sb.AppendLine("<p><a href=\"newsdetial.aspx?id=" + news.ID + "\">" + news.Title + "</a></p>");
                        sb.AppendLine("</li>");
                        break;
                    case 2:
                        sb.AppendLine("<li><a href=\"newsdetial.aspx?id=" + news.ID + "\">" + news.Title + "</a></li>");
                        break;
                }
            }

            return sb.ToString();
        }
        return "";
    }
    public static string GetIsHotNewsStr(int count)
    {
        Model.News limit = new Model.News();
        limit.IsHot = 1;
        int smc = 0;
        List<Model.News> list = Model.News.GetInfoByLimit(null, null, limit, 0, count, out smc);
        if (list != null && list.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                sb.AppendLine("<li><em>" + (i + 1) + "</em><a href=\"newsdetial.aspx?id=" + list[i].ID + "\">" + list[i].Title + "</a></li>");
            }
            return sb.ToString();
        }
        return "";
    }

    #endregion

    #region 友情链接
    public static string GetFriendLinkListStr(int count)
    {
        Model.FriendLink limitItem = new Model.FriendLink();
        int sumCount = 0;
        List<Model.FriendLink> newsList = null;
        try { newsList = Model.FriendLink.GetInfoByLimit("ID", "DESC", limitItem, 0, count, out sumCount); }
        catch { }
        if (newsList != null && newsList.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var news in newsList)
            {
                sb.AppendLine("<li><a href=\"" + news.LinkSrc + "\">");
                sb.AppendLine("<img width=\"122\" height=\"38\" src=\"" + Biz.GetImageSrc(news.LinkImageUrl) + "\" alt=\"" + news.LinkName + "\" /></a></li>");
            }
            return sb.ToString();
        }
        else
        {
            return string.Empty;
        }
    }
    #endregion

    #region 页面显示
    public static string GetTopBar()
    {
        StringBuilder sb = new StringBuilder();
        //sb.AppendLine("<div class=\"topCon\"><span class=\"fr\"><a href=\"javascript:void(0);\">登陆</a>|<a href=\"javascript:void(0);\">注册</a>|<a href=\"#\">联系我们</a>|<a href=\"#\">微博互动</a>|<a href=\"#\">微信扫一扫</a></span>主管单位：第一财经·中国房地产金融&nbsp;&nbsp;&nbsp;&nbsp;主办单位：第一财经·中国房地产金融 </div>");
        sb.AppendLine("<div class=\"topCon\"><span class=\"fr\"><a href=\"#\">联系我们</a>|<a target=\"_blank\" href=\"http://weibo.com/u/2784285922\">微博互动</a>|<a href=\"#\">微信扫一扫</a></span>指导单位：中国房地产业协会&nbsp;&nbsp;&nbsp;&nbsp;主管单位：第一财经&nbsp;&nbsp;&nbsp;&nbsp;主办单位：中国房地产金融</div>");
        return sb.ToString();
    }
    public static string GetMainCon(int now)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div class=\"tplft\">");
        sb.AppendLine("<ul>");
        sb.AppendLine("<li><a href=\"\"><em class=\"aicon\"></em></a></li>");
        sb.AppendLine("<li><a href=\"\"><em class=\"bicon\"></em></a></li>");
        sb.AppendLine("<li><a href=\"\"><em class=\"cicon\"></em></a></li>");
        sb.AppendLine("</ul>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"menu\">");
        sb.AppendLine("<ul>");
        sb.AppendLine("<li"+(now==1?" class=\"current\"":"")+"><a href=\"/\">首页</a></li>");
        sb.AppendLine("<li" + (now == 2 ? " class=\"current\"" : "") + " id=\"m2\">");
        sb.AppendLine("<a href=\"newslist.aspx\">新闻</a>");
        sb.AppendLine("<dl class=\"curdl\" id=\"m2d\">");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=24\">地产新闻</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=25\">金融新闻</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=26\">上市公司</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=27\">楼市评论</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li" + (now == 3 ? " class=\"current\"" : "") + " id=\"m3\"><a href=\"eventlist.aspx\">品牌</a>");
        sb.AppendLine("<dl class=\"curdl\" id=\"m3d\">");
        sb.AppendLine("<dd><a href=\"eventlist.aspx?id=5\">金融峰会</a></dd>");
        sb.AppendLine("<dd><a href=\"eventlist.aspx?id=1\">Leader Club</a></dd>");
        sb.AppendLine("<dd><a href=\"eventlist.aspx?id=2\">房地产金融理事会</a></dd>");
        sb.AppendLine("<dd><a href=\"eventlist.aspx?id=3\">金主高尔夫</a></dd>");
        sb.AppendLine("<dd><a href=\"eventlist.aspx?id=4\">臻品私享</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");

        sb.AppendLine("<li" + (now == 4 ? " class=\"current\"" : "") + "><a href=\"magazlist.aspx\">杂志</a></li>");
        sb.AppendLine("<li" + (now == 5 ? " class=\"current\"" : "") + "><a href=\"videolist.aspx\">视频</a></li>");
        sb.AppendLine("</ul>");
        sb.AppendLine("</div>");
        return sb.ToString();
    }
    public static string GetSearchStr()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div class=\"sdsearch\">");
        sb.AppendLine("<form action=\"newslist.aspx\" id=\"tosearch\">");
        sb.AppendLine("<div class=\"shtxt\">");
        sb.AppendLine("<input name=\"key\" type=\"text\" PlaceHolder=\"输入关键词进行搜索\" />");
        sb.AppendLine("<div style=\"height:35px;width:48px;float:right;margin:5px 5px 0 0;cursor:pointer;\" onclick=\"document.getElementById('tosearch').submit();\"></div>");
        sb.AppendLine("</div>");
        sb.AppendLine("</form>");
        sb.AppendLine("<p><a href=\"/newslist.aspx?key=房地产金融\">房地产金融</a><a href=\"/newslist.aspx?key=第一财经\">第一财经</a><a href=\"/newslist.aspx?key=大资管\">大资管</a><a href=\"/newslist.aspx?key=大涨\">大涨</a></p>");
        sb.AppendLine("</div>");
        return sb.ToString();
    }
    #endregion

    #region 杂志
    public static string GetRadomMagazStr(int count)
    {
        List<Model.Magaz> magazList = Model.Magaz.GetRadom(count);
        if (magazList != null && magazList.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var magaz in magazList)
            {
                sb.AppendLine("<li><a href=\"magazdetial.aspx?id=" + magaz.ID + "\">");
                sb.AppendLine("<img src=\"" + Biz.GetImageSrc(magaz.magazPages.Split('|')[0]) + "\" alt=\" \" /></a></li>");
            }
            return sb.ToString();
        }
        return "";
    }

    public static string GetMainMagazStr()
    {
        var limit = new Model.Magaz();
        limit.InMainTop = 1;
        int smc = 0;
        Model.Magaz aMagaz = null;
        try { aMagaz = Model.Magaz.GetInfoByLimit("ID", "DESC", limit, 0, 999, out smc)[0]; }
        catch { }
        if (aMagaz != null)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<a href=\"magazdetial.aspx?id=" + aMagaz.ID + "\"><img src=\"" + Biz.GetImageSrc( aMagaz.magazPages.Split('|')[0]) + "\" alt=\" \" /></a>");
                sb.AppendLine("<p>"+aMagaz.Content.Trim().Replace("\n","</p><p>")+"</p>");
                return sb.ToString();
            }
            catch { }
        }
        return "";
    }
    public static string GetMainMagazTitleStr()
    {
        var limit = new Model.Magaz();
        limit.InMainTop = 1;
        int smc = 0;
        Model.Magaz aMagaz = null;
        try { aMagaz = Model.Magaz.GetInfoByLimit("ID", "DESC", limit, 0, 999, out smc)[0]; }
        catch { }
        if (aMagaz != null)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<a href=\"magazdetial.aspx?id=" + aMagaz.ID + "\">" + aMagaz.Title + "</a>");
                return sb.ToString();
            }
            catch { }
        }
        return "";
    }
    #endregion

    public static string GetImageSrc(string src)
    {
        return KCommonLib.DBTools.DBCommon.GetConstr("ImageService") + src;
    }

    public static string GetLiCaiJingYingQuYuOption(string selQuyu)
    {
        List<string> quYuList = Model.LiCaiJinYing.GetQuYuList();
        if (quYuList != null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var quyu in quYuList)
            {
                if (selQuyu == quyu)
                    sb.Append("<option value =\"" + quyu + "\" selected>" + quyu + "</option>");
                else
                    sb.Append("<option value =\"" + quyu + "\">" + quyu + "</option>");
            }
            return sb.ToString();
        }
        return null;
    }

    public static Model.News GetOneNewsById(int id)
    {
        Model.News limitItem = new Model.News();
        limitItem.ID = id;
        int sumCount = 0;
        List<Model.News> newsList = null;
        try { newsList = Model.News.GetInfoByLimit(null, null, limitItem, 0, 1, out sumCount); }
        catch { }
        if (newsList != null && newsList.Count > 0)
        {
            return newsList[0];
        }
        else
        {
            return new Model.News();
        }
    }

    public static string GetNewsListStrByFGroupId(int groupId, int count)
    {
        List<Model.News> newsList = Model.News.GetNewsListByFGroupId(groupId, count);
        if (newsList != null && newsList.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var news in newsList)
            {
                sb.AppendLine("<li><a href=\"newslist.aspx?id=" + ngList.Where(t => t.ID == news.GroupId).Single().ID + "\">[" + ngList.Where(t => t.ID == news.GroupId).Single().GroupName + "]</a><a href=\"newsdetial.aspx?id=" + news.ID + "\">" + news.Title + "</a></li>");
            }
            return sb.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    // 预加载所有栏目信息
    static List<Model.NewsGroup> ngList = Model.NewsGroup.Get(null, null);
    // 返回新闻列表方法
    public static string GetNewsListByNewsGroupId(int GroupId, int strType, int count)
    {
        Model.News limitItem = new Model.News();
        limitItem.GroupId = GroupId;
        int sumCount = 0;
        List<Model.News> newsList = null;
        try { newsList = Model.News.GetInfoByLimit("CreateDate", "DESC", limitItem, 0, count, out sumCount); }
        catch { }
        if (newsList != null && newsList.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var news in newsList)
            {
                if (strType == 1)// 没有日期的类型
                    sb.AppendLine("<li><a href=\"newsdetial.aspx?id=" + news.ID + "\">" + news.Title + "</a></li>");
                else if (strType == 2)// 有日期的类型
                    sb.AppendLine("<li><span>" + news.CreateDate.ToString("MM-dd") + "</span><a href=\"#\">" + news.Title + "</a></li>");
                else if (strType == 3)
                    sb.AppendLine("<li><a href=\"#\">[" + ngList.Where(t => t.ID == news.GroupId).Single().GroupName + "]</a><a href=\"#\">" + news.Title + "</a></li>");
            }
            return sb.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    public static string GetBottom()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div class=\"tit\">");
        sb.AppendLine("<p class=\"nametit\">长汇财富管理有限公司</p>");
        sb.AppendLine("<p>Changhui Wealth Managerment Co.Ltd.</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<p><a href=\"/newsgd.aspx?id=22\">公司简介</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"/newsgd.aspx?id=24\">企业文化</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"/newsgd.aspx?id=25\">招贤纳士</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"Friend.aspx\">合作伙伴</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"/reserve.aspx\">客服中心</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"contact.aspx\">联系我们</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\"userlogin.aspx\">会员专区</a></p>");
        //sb.AppendLine("<p>欢迎访问长汇财富！请<a href=\"Contact.aspx\">点此</a>与我们联系 版权所有:Copyright &copy;2013-2014</p>");
        //sb.AppendLine("<div class=\"h10\"></div>");
        //sb.AppendLine("<div class=\"appCon\"><a href=\"#\">App Store</a><a href=\"#\">Google Play</a> </div>");
        return sb.ToString();
    }

    public static string GetMenuCon()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div class=\"menucon\">");
        sb.AppendLine("<ul>");
        sb.AppendLine("<li id=\"m1\"><a href=\"/\">首　页</a></li>");

        sb.AppendLine("<li style=\"font-family: 'Simsun';\">|</li>");

        sb.AppendLine("<li id=\"m2\"><a href=\"newsgd.aspx?id=21\">关于长汇</a>");
        sb.AppendLine("<dl id=\"m2d\" style=\"display: none;\">");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=21\">总裁致辞</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=22\">公司简介</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=23\">股东介绍</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=24\">企业文化</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=25\">企业理念</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=26\">长汇荣誉</a></dd>");
        sb.AppendLine("<dd><a href=\"Friend.aspx\">合作伙伴</a></dd>");
        sb.AppendLine("<dd><a href=\"Contact.aspx\">联系我们</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");

        sb.AppendLine("<li style=\"font-family: 'Simsun';\">|</li>");

        sb.AppendLine("<li id=\"m3\"><a href=\"cmap.aspx\">分支机构</a>");
        sb.AppendLine("<dl id=\"m3d\" style=\"display: none;\">");
        sb.AppendLine("<dd><a href=\"cmap.aspx\">网点分布</a></dd>");
        sb.AppendLine("<dd><a href=\"Elite.aspx\">理财精英</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");

        sb.AppendLine("<li style=\"font-family: 'Simsun';\">|</li>");

        sb.AppendLine("<li id=\"m4\"><a href=\"newslist.aspx?id=19\">长汇动态</a>");
        sb.AppendLine("<dl id=\"m4d\" style=\"display: none;\">");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=19\">公司动态</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=20\">媒体报道</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=21\">活动预约</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=22\">活动回顾</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");

        sb.AppendLine("<li style=\"font-family: 'Simsun';\">|</li>");

        sb.AppendLine("<li id=\"m5\"><a href=\"newsgd.aspx?id=29\">风控与服务</a>");
        sb.AppendLine("<dl id=\"m5d\" style=\"display: none;\">");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=29\">风控体系</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=30\">预约流程</a></dd>");

        //sb.AppendLine("<dd><a href=\"risktest.aspx\">风险测试</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?tag=2\">资产配置</a></dd>");
        //sb.AppendLine("<dd><a href=\"reserve.aspx\">预约理财</a></dd>");

        //sb.AppendLine("<dd><a href=\"newsgd.aspx?id=31\">产品开发</a></dd>");
        //sb.AppendLine("<dd><a href=\"newsgd.aspx?id=32\">产品筛选</a></dd>");
        //sb.AppendLine("<dd><a href=\"newsgd.aspx?id=33\">风控管理</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=34\">合规管理</a></dd>");
        //sb.AppendLine("<dd><a href=\"newsgd.aspx?id=35\">风险偏好</a></dd>");

        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");

        sb.AppendLine("<li style=\"font-family: 'Simsun';\">|</li>");

        sb.AppendLine("<li id=\"m6\"><a href=\"ProductMainList.aspx\">产品信息</a>");
        sb.AppendLine("<dl id=\"m6d\" style=\"display: none;\">");
        sb.AppendLine("<dd><a href=\"ProductMainList.aspx\">热销产品</a></dd>");
        sb.AppendLine("<dd><a href=\"ProductMainList.aspx\">全部产品</a></dd>");
        sb.AppendLine("<dd><a href=\"ProductManager.aspx\">产品续存管理</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");

        sb.AppendLine("<li style=\"font-family: 'Simsun';\">|</li>");

        sb.AppendLine("<li id=\"m7\"><a href=\"newslist.aspx?id=5\">资讯研究</a>");
        sb.AppendLine("<dl id=\"m7d\" style=\"display: none;\">");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=5\">长汇研究</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=6\">固定收益</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=7\">基金资讯</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=8\">宏观经济</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=9\">二级市场</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=10\">信托资讯</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=11\">理财市场</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");

        sb.AppendLine("<li style=\"font-family: 'Simsun';\">|</li>");

        //sb.AppendLine("<li id=\"m9\" onmouseover=\"changeMenu('m9');\" onmouseout=\"menuOut('m9');\"><a href=\"newslist.aspx?id=14\">理财学堂</a>");
        //sb.AppendLine("<dl id=\"m9d\" style=\"display: none;\" onmouseout=\"menuOut('m9');\">");
        //sb.AppendLine("<dd><a href=\"newslist.aspx?id=14\">信托</a></dd>");
        //sb.AppendLine("<dd><a href=\"newslist.aspx?id=15\">资管</a></dd>");
        //sb.AppendLine("<dd><a href=\"newslist.aspx?id=16\">私募基金</a></dd>");
        //sb.AppendLine("<dd><a href=\"newslist.aspx?id=17\">公募基金</a></dd>");
        //sb.AppendLine("</dl>");
        //sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m8\">");
        sb.AppendLine("<a href=\"userlogin.aspx\">会员专区</a>");
        sb.AppendLine("</li>");
        sb.AppendLine("</ul>");
        sb.AppendLine("</div>");
        return sb.ToString();
    }

    /*
    public static string GetMenuCon()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div class=\"menucon\">");
        sb.AppendLine("<ul>");
        sb.AppendLine("<li id=\"m1\" onmouseover=\"changeMenu('m1');\" onmouseout=\"menuOut('m1');\"><a href=\"/\">首　页</a></li>");
        sb.AppendLine("<li id=\"m2\" onmouseover=\"changeMenu('m2');\" onmouseout=\"menuOut('m2');\"><a href=\"newsgd.aspx?id=21\">关于长汇</a>");
        sb.AppendLine("<dl id=\"m2d\" style=\"display: none;\" onmouseout=\"menuOut('m2');\">");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=21\">总裁致辞</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=22\">公司简介</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=23\">股东介绍</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=24\">企业文化</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=25\">企业理念</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=26\">长汇荣誉</a></dd>");
        sb.AppendLine("<dd><a href=\"Friend.aspx\">合作伙伴</a></dd>");
        sb.AppendLine("<dd><a href=\"Contact.aspx\">联系我们</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m3\" onmouseover=\"changeMenu('m3');\" onmouseout=\"menuOut('m3');\"><a href=\"cmap.aspx\">分支机构</a>");
        sb.AppendLine("<dl id=\"m3d\" style=\"display: none;\" onmouseout=\"menuOut('m3');\">");
        sb.AppendLine("<dd><a href=\"cmap.aspx\">网点分布</a></dd>");
        sb.AppendLine("<dd><a href=\"Elite.aspx\">理财精英</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m4\" onmouseover=\"changeMenu('m4');\" onmouseout=\"menuOut('m4');\"><a href=\"newslist.aspx?id=19\">长汇动态</a>");
        sb.AppendLine("<dl id=\"m4d\" style=\"display: none;\" onmouseout=\"menuOut('m4');\">");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=19\">公司动态</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=20\">媒体报道</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=21\">活动预约</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=22\">活动回顾</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m5\" onmouseover=\"changeMenu('m5');\" onmouseout=\"menuOut('m5');\"><a href=\"newsgd.aspx?id=29\">服务体系</a>");
        sb.AppendLine("<dl id=\"m5d\" style=\"display: none;\" onmouseout=\"menuOut('m5');\">");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=29\">服务宗旨</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=30\">预约流程</a></dd>");
        sb.AppendLine("<dd><a href=\"risktest.aspx\">风险测试</a></dd>");
        sb.AppendLine("<dd><a href=\"assets.aspx\">资产配置</a></dd>");
        sb.AppendLine("<dd><a href=\"reserve.aspx\">预约理财</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m6\" onmouseover=\"changeMenu('m6');\" onmouseout=\"menuOut('m6');\"><a href=\"newsgd.aspx?id=31\">风控体系</a>");
        sb.AppendLine("<dl id=\"m6d\" style=\"display: none;\" onmouseout=\"menuOut('m6');\">");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=31\">产品开发</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=32\">产品筛选</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=33\">风控管理</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=34\">合规管理</a></dd>");
        sb.AppendLine("<dd><a href=\"newsgd.aspx?id=35\">风险偏好</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m7\" onmouseover=\"changeMenu('m7');\" onmouseout=\"menuOut('m7');\"><a href=\"ProductList.aspx\">产品信息</a>");
        sb.AppendLine("<dl id=\"m7d\" style=\"display: none;\" onmouseout=\"menuOut('m7');\">");
        sb.AppendLine("<dd><a href=\"ProductList.aspx\">热销产品</a></dd>");
        sb.AppendLine("<dd><a href=\"ProductList.aspx\">全部产品</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m8\" onmouseover=\"changeMenu('m8');\" onmouseout=\"menuOut('m8');\"><a href=\"newslist.aspx?id=5\">资讯研究</a>");
        sb.AppendLine("<dl id=\"m8d\" style=\"display: none;\" onmouseout=\"menuOut('m8');\">");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=5\">长汇研究</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=6\">固定收益</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=7\">基金资讯</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=8\">宏观经济</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=9\">二级市场</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=10\">信托资讯</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=11\">理财市场</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m9\" onmouseover=\"changeMenu('m9');\" onmouseout=\"menuOut('m9');\"><a href=\"newslist.aspx?id=14\">理财学堂</a>");
        sb.AppendLine("<dl id=\"m9d\" style=\"display: none;\" onmouseout=\"menuOut('m9');\">");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=14\">信托</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=15\">资管</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=16\">私募基金</a></dd>");
        sb.AppendLine("<dd><a href=\"newslist.aspx?id=17\">公募基金</a></dd>");
        sb.AppendLine("</dl>");
        sb.AppendLine("</li>");
        sb.AppendLine("<li id=\"m10\" onmouseover=\"changeMenu('m10');\" onmouseout=\"menuOut('m10');\">");
        sb.AppendLine("<a href=\"userlogin.aspx\">会员专区</a>");
        sb.AppendLine("</li>");
        sb.AppendLine("</ul>");
        sb.AppendLine("</div>");
        return sb.ToString();
    }
     */
}