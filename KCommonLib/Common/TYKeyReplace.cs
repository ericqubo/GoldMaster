using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCommonLib.Common
{
    public class TYKeyReplace
    {
        List<string[]> keyList;
        public TYKeyReplace()
        {
            string src = KCommonLib.Common.CommonLib.GetPath() + "1.7wKey.txt";
            string value = KCommonLib.Common.MyIO.ReadSrc(src);
            if (!string.IsNullOrEmpty(value))
            {
                keyList = new List<string[]>();
                string[] v1 = value.Split('\r', '\n');
                for (int i = 0; i < v1.Length; i++)
                {
                    if (!string.IsNullOrEmpty(v1[i]))
                    {
                        keyList.Add(v1[i].Split('→'));
                    }
                }

                Console.WriteLine(DateTime.Now + " 装载词库成功，共装载同义词 " + keyList.Count + " 条。");
            }
        }

        /// <summary>
        /// 替换同义词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetReplacedStr(string str)
        {
            List<string[]> haveKey = new List<string[]>();

            foreach (var key in keyList)
            {
                if (str.Contains(key[0]))
                {
                    haveKey.Add(new string[] { key[0], key[1] });
                }
                if (str.Contains(key[1]))
                {
                    haveKey.Add(new string[] { key[1], key[0] });
                }
            }

            foreach (var c in haveKey)
            {
                str = str.Replace(c[0], c[1]);
                Console.WriteLine(DateTime.Now + " 替换内容中关键字 " + c[0] + " 为 " + c[1]);
            }

            return str;
        }
    }
}
