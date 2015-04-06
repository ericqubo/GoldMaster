using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsDetial : System.Web.UI.Page
{
    protected Model.News _news;
    protected Model.NewsGroup _nowGroup, _fGroup;
    protected string groupName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["id"]) && KCommonLib.Common.DataValidation.isNum(Request["id"]))
        {
            Model.News limit = new Model.News();
            limit.ID = Convert.ToInt32(Request["id"]);
            int sumCount = 0;
            _news = Model.News.GetInfoByLimit(null, null, limit, 0, 1, out sumCount)[0];

            List<Model.NewsGroup> ngList = Model.NewsGroup.Get(null, null);
            _nowGroup = ngList.Where(t => t.ID == _news.GroupId).Single();
        }
        else
        {
            Response.Redirect("newslist.aspx");
        }
    }

    //protected string FormatNewsContent(string content)
    //{
    //    KCommonLib.Common.DataValidation.FormatHtml(_news.Content);

    //}
}