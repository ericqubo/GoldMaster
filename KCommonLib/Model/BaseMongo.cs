using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCommonLib.Model
{
    public class BaseMongo
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }
        /// <summary>
        /// 集合、表名称
        /// </summary>
        public string ColName { get; set; }
    }
}
