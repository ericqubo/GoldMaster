using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Lib
{
    public static class DataDict_Error
    {
        #region News 新闻 1000
        /// <summary>
        /// 新闻 1000
        /// </summary>
        public readonly static Dictionary<string, string> NewsErrorDict = new Dictionary<string, string>()
        {
            { "1001", "新闻类别数据异常" }
        };

        #endregion


        #region UserErr 用户信息异常
        /// <summary>
        /// 基础异常
        /// </summary>
        public readonly static Dictionary<string, string> UserErrorDict = new Dictionary<string, string>()
        {
            { "2001", "uuid不能为空" },
        };

        #endregion
    }
}