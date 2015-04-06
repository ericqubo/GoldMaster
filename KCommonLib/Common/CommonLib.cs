using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Drawing;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace KCommonLib.Common
{
    public class CommonLib
    {
        #region 返回当前项目路径
        /// <summary>
        /// 返回当前项目路径
        /// </summary>
        /// <returns></returns>
        public static String GetPath()
        {
            Page page = new Page();
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            return path;
        }
        #endregion

        #region 生成随机字符串
        private static char[] constant = {   
        '0','1','2','3','4','5','6','7','8','9',  
        //'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
        'A','B','C','D','E','F','G','H','J','K','L','M','N','P','Q','R','S','T','U','V','W','X','Y','Z'   
        };
        private static char[] constant2 = {     
        'A','B','C','D','E','F','G','H','J','K','L','M','N','P','Q','R','S','T','U','V','W','X','Y','Z'   
        };
        private static char[] constant3 = {   
        '0','1','2','3','4','5','6','7','8','9'  
        };

        /// <summary>
        /// 返回随机字符串（数字和所有大写字母）
        /// </summary>
        /// <param name="Length"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int Length, int seed)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Random rd = new Random(seed);
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// /// 返回随机字符串（所有大写字母）
        /// </summary>
        /// <param name="Length"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber2(int Length, int seed)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant2.Length);
            Random rd = new Random(seed);
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant2[rd.Next(constant2.Length)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// 返回随机字符串（数字）
        /// </summary>
        /// <param name="Length"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber3(int Length, int seed)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant3.Length);
            Random rd = new Random(seed);
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant3[rd.Next(constant3.Length)]);
            }
            return newRandom.ToString();
        }
        #endregion

        #region 补足指定位数int类型字符
        public static string GetFormatInt(int c, int count)
        {
            int cc = c.ToString().Length;
            int bc = count - cc;
            string r = "";
            for (int i = 0; i < bc; i++)
            {
                r += "0";
            }
            return r + c.ToString();
        }
        #endregion

    }
}
