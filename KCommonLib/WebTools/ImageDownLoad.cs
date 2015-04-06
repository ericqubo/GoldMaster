using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace KCommonLib.WebTools
{
    public class ImageDownLoad
    {
        public static void DownImage(string src,string path)
        {
            if (!String.IsNullOrEmpty(src))
            {
                string url = src;
                string filepath = path;
                WebClient mywebclient = new WebClient();
                mywebclient.DownloadFile(url, filepath);
            }

        }
    }
}
