using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public static class ConfigHelper
    {

        /// <summary>
        /// 是否Debug
        /// </summary>
        public static readonly bool IsDebug = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsDebug"]);

        /// <summary>
        /// 
        /// </summary>
        public static readonly string WebDomain = System.Configuration.ConfigurationManager.AppSettings["WebDomain"];

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ImgDomain = System.Configuration.ConfigurationManager.AppSettings["ImgDomain"];

        /// <summary>
        /// 
        /// </summary>
        public static readonly string VideoDomain = System.Configuration.ConfigurationManager.AppSettings["VideoDomain"];

        /// <summary>
        /// 
        /// </summary>
        public static readonly int AppVersion = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["AppVersion"]);
    }
}