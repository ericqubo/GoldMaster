using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsList : System.Web.UI.Page
{
    // 获取所有栏目信息
    protected int groupId = 0;
    protected string key;

    int _sumCount = 0, _pageRowCount = 10, _nowPage = 0, _sumPage = 0;//分页用
    protected string _FPageStr, newsListStr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["id"]) && KCommonLib.Common.DataValidation.isNum(Request["id"]))
            groupId = Convert.ToInt32(Request["id"]);
        else
            groupId = 24;

        //if (!string.IsNullOrEmpty(Request["key"]))
        //    key = KCommonLib.Common.DataValidation.FormatHtml(Request["key"].ToString()).Replace("'", "");

        if (!string.IsNullOrEmpty(Request["p"]) && KCommonLib.Common.DataValidation.isNum(Request["p"]))
            _nowPage = Convert.ToInt32(Request["p"]);

        LoadNewsListStr();
    }

    // 返回新闻列表的页面字符串
    protected void LoadNewsListStr()
    {
        Model.News limitItem = new Model.News();
        limitItem.GroupId = groupId;

        List<Model.News> nList = Model.News.GetInfoByLimit("CreateDate", "DESC", limitItem, 0, _pageRowCount, out _sumCount);
        _sumPage = _sumCount % _pageRowCount > 0 ? _sumCount / _pageRowCount + 1 : _sumCount / _pageRowCount;
        if (nList != null && nList.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < nList.Count; i++)
            {
                string content = string.Empty;
                sb.AppendLine("<li>");
                sb.AppendLine("<a onclick=\"$.mobile.changePage('newsdetial.aspx?gid="+groupId+"&id=" + nList[i].ID + "', { transition: 'slide' });\">");
                sb.AppendLine("<div class=\"pic_box\"><img width=\"96px\" height=\"80px\" src=\"" + Biz.GetImageSrc(nList[i].ImgUrl) + "\" /></div>");
                sb.AppendLine("<div class=\"title_box\">" + nList[i].Title + "</div>");
                sb.AppendLine("<div class=\"active_box\">" + nList[i].Discrption + "</div>");
                sb.AppendLine("<div class=\"time_box\">");
                sb.AppendLine("<span>0</span> <code>0</code> <cite>" + nList[i].CreateDate.ToString("MM月dd日") + "</cite>");
                sb.AppendLine("</div>");
                sb.AppendLine("</a>");
                sb.AppendLine("</li>");
            }
            newsListStr = sb.ToString();
        }
    }
}