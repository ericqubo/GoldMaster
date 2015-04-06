using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace KCommonLib.WebTools
{
    /// <summary>
    /// json格式互相转换
    /// </summary>
    public class ObjectToJson
    {
        #region Json字符串转换为List
        /// <summary>
        /// Json字符串转换为List
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static JArray GetListByJsonSrc<T>(string content)
        {
            if (string.IsNullOrEmpty(content))
                return null;
            JArray list = new JArray();
            try
            {
                JObject obj = JObject.Parse(content);
                list = (JArray)obj["rows"];
            }
            catch
            {

            }
            return list;
        }
        #endregion

        #region 从文件或URL地址获取json格式文件后格式化
        /// <summary>
        /// 从文件或URL地址获取json格式文件后格式化
        /// 调用示例
        /// JArray result = ObjectToJson.GetListByJsonSrc(src);
        /// for (int i = 0; i  result.Count; i++)
        /// result[i]["ENDDATE"]
        /// </summary>
        /// <param name="path">文件路径或URL地址</param>
        /// <returns>JArray对象，可像DataRow方式访问</returns>
        public static JArray GetListByJsonSrc(string src, string name)
        {
            JArray list = new JArray();
            try
            {
                string content = string.Empty;

                if (src.Substring(0, 7).ToUpper() == "http://".ToUpper())
                    KCommonLib.Common.MyIO.LoadNetPage(src, out content);
                else
                    KCommonLib.Common.MyIO.ReadSrc(src);

                if (!string.IsNullOrEmpty(content))
                {
                    JObject obj = JObject.Parse(content);
                    list = (JArray)obj[name];
                }
            }
            catch
            {

            }
            return list;
        }

        /// <summary>
        /// 从文件或URL地址获取json格式文件后格式化
        /// 调用示例
        /// JArray result = ObjectToJson.GetListByJsonSrc(src);
        /// for (int i = 0; i = result.Count; i++)
        /// result[i]["ENDDATE"]
        /// </summary>
        /// <param name="path">文件路径或URL地址</param>
        /// <returns>JArray对象，可像DataRow方式访问</returns>
        public static JArray GetListByJsonSrc(string src)
        {
            JArray list = new JArray();
            try
            {
                string content = string.Empty;

                if (src.Substring(0, 7).ToUpper() == "http://".ToUpper())
                    KCommonLib.Common.MyIO.LoadNetPage(src, out content);
                else
                    KCommonLib.Common.MyIO.ReadSrc(src);

                if (!string.IsNullOrEmpty(content))
                {
                    JObject obj = JObject.Parse(content);
                    list = (JArray)obj["rows"];
                }
            }
            catch
            {

            }
            return list;
        }
        /*调用示列
        JArray result = F10Result.getF10result(ref par);
        if (result != null && result.Count > 0)
        {
            V_BS_ZLDA_LIST[] v_BS_ZLDA_LIST = new V_BS_ZLDA_LIST[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                v_BS_ZLDA_LIST[i] = new V_BS_ZLDA_LIST();
                v_BS_ZLDA_LIST[i].enddate = Convert.ToDateTime(FormatField(result[i]["ENDDATE"],3, 0, false, "yyyy-MM-dd"));
                v_BS_ZLDA_LIST[i].stockcode = FormatField(result[i]["STOCKCODE"], 1, 0, false, "");
                v_BS_ZLDA_LIST[i].stockname = FormatField(result[i]["STOCKNAME"], 1, 0, false, "");
                v_BS_ZLDA_LIST[i].OrgName = FormatField(result[i]["ORGNAME"], 1, 0, false, "");
                v_BS_ZLDA_LIST[i].holdnum = Convert.ToDecimal(FormatField(result[i]["HOLDNUM"], 1, 0, false, ""));
                v_BS_ZLDA_LIST[i].HoldRate = Convert.ToDecimal(FormatField(result[i]["HOLDRATE"], 1, 0, false, ""));
            }
            return v_BS_ZLDA_LIST;
        }
        else
        {
            return null;
        }*/
        #endregion

        #region List转成json
        /// <summary>
        /// List转成json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = list[0].GetType().Name;
            Json.Append("{\"" + jsonName + "\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        Type type = pi[j].GetValue(list[i], null).GetType();
                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>
        /// List转成json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list, string jsonName, string format)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = list[0].GetType().Name;
            Json.Append("{" + format + "\"" + jsonName + "\":[" + format + "");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{" + format);
                    for (int j = 0; j < pi.Length; j++)
                    {
                        Type type = pi[j].GetValue(list[i], null).GetType();
                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

                        if (j < pi.Length - 1)
                        {
                            Json.Append("," + format);
                        }
                    }
                    Json.Append(format + "}");
                    if (i < list.Count - 1)
                    {
                        Json.Append("," + format);
                    }
                }
            }
            Json.Append(format + "]}");
            return Json.ToString();
        }

        /// <summary>
        /// List转成json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list)
        {
            object obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name);
        }

        #endregion

        #region 对象转换为Json字符串
        /// <summary> 
        /// 对象转换为Json字符串 
        /// </summary> 
        /// <param name="jsonObject">对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "}";
        }

        /// <summary> 
        /// 对象集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += ToJson(item) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }

        #endregion

        #region 普通集合转换Json
        /// <summary> 
        /// 普通集合转换Json 
        /// </summary> 
        /// <param name="array">集合对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToArrayString(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString = ToJson(item.ToString()) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }
        #endregion

        #region Datatable转换为Json
        /// <summary> 
        /// Datatable转换为Json 
        /// </summary> 
        /// <param name="table">Datatable对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        #endregion

        #region DataTable转成Json
        /// <summary>
        /// DataTable转成Json 
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(DataTable dt, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        #endregion

        #region DataReader转换为Json
        /// <summary> 
        /// DataReader转换为Json 
        /// </summary> 
        /// <param name="dataReader">DataReader对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DbDataReader dataReader)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            while (dataReader.Read())
            {
                jsonString.Append("{");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    Type type = dataReader.GetFieldType(i);
                    string strKey = dataReader.GetName(i);
                    string strValue = dataReader[i].ToString();
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (i < dataReader.FieldCount - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            dataReader.Close();
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        #endregion

        #region DataSet转换为Json
        /// <summary> 
        /// DataSet转换为Json 
        /// </summary> 
        /// <param name="dataSet">DataSet对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }
        #endregion

        #region 过滤特殊字符
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 格式化字符型、日期型、布尔型
        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            return str;
        }
        #endregion

    }
}
