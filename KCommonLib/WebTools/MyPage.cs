using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace KCommonLib.WebTools
{
    public class MyPage : System.Web.UI.Page
    {
        string uName;
        public MyPage()
        {
            try
            {
                uName = HttpContext.Current.Session["user"].ToString();
            }
            catch { }

            if (uName == null)
            {
                HttpCookie co = HttpContext.Current.Request.Cookies["krisweb"];
                try
                {
                    uName = co.Values["user"].ToString();
                }
                catch { }
            }

            if (uName == null)
                HttpContext.Current.Response.Write("<script>window.parent.location.href=\"login.aspx\";</script>");
                //HttpContext.Current.Response.Redirect("login.aspx");
        }
    }
}
