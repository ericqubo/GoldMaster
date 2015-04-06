using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCommonLib.Common
{
    public class LogManager
    {
        public static void WriteLog(string msg)
        {
            string filePath = string.Format(KCommonLib.Common.CommonLib.GetPath() + "\\log\\log{0}.htm", DateTime.Now.ToString("yyyyMMdd"));
            KCommonLib.Common.MyIO.WriteLog(msg, filePath, false);
            Console.WriteLine(msg);
        }
    }
}
