using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Http;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Visitors;
using System.IO;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Tags;

namespace KCommonLib.Common
{
    public class MyHtmlParser
    {
        /// <summary>
        /// 返回html页面中指定标签参数内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="enCoding"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> GetHtmlNodes(string url, string enCoding, string targetName)
        {
            Lexer lexer = new Lexer(KCommonLib.Common.MyIO.LoadNetPage(url, enCoding));
            Parser parser = new Parser(lexer);
            NodeFilter filter = new TagNameFilter(targetName);
            NodeList htmlNodes = parser.Parse(filter);

            if (htmlNodes != null && htmlNodes.Count > 0)
            {
                List<Dictionary<string, string>> _targets = new List<Dictionary<string, string>>();
                Dictionary<string, string> _target = null;

                for (int i = 0; i < htmlNodes.Count; i++)
                {
                    if (htmlNodes[i] is ITag)
                    {
                        ITag tag = (htmlNodes[i] as ITag);
                        if (!tag.IsEndTag())
                        {
                            if (tag.Attributes != null && tag.Attributes.Count > 0)
                            {
                                string[] keysArr = new string[tag.Attributes.Count];
                                tag.Attributes.Keys.CopyTo(keysArr, 0);

                                string[] valuesArr = new string[tag.Attributes.Count];
                                tag.Attributes.Values.CopyTo(valuesArr, 0);

                                _target = new Dictionary<string, string>();

                                for (int j = 0; j < tag.Attributes.Keys.Count; j++)
                                {

                                    if (valuesArr[j] != "$<NULL>$" && keysArr[j] != "$<TAGNAME>$")
                                        _target.Add(keysArr[j], valuesArr[j]);
                                }

                                _targets.Add(_target);

                            }
                        }
                    }
                }
                return _targets;
            }
            return null;
        }

        /// <summary>
        /// 返回html页面中指定标签参数内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="enCoding"></param>
        /// <param name="targetName"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> GetHtmlNodes(string html, string targetName)
        {
            Lexer lexer = new Lexer(html);
            Parser parser = new Parser(lexer);
            NodeFilter filter = new TagNameFilter(targetName);
            NodeList htmlNodes = parser.Parse(filter);

            if (htmlNodes != null && htmlNodes.Count > 0)
            {
                List<Dictionary<string, string>> _targets = new List<Dictionary<string, string>>();
                Dictionary<string, string> _target = null;

                for (int i = 0; i < htmlNodes.Count; i++)
                {
                    if (htmlNodes[i] is ITag)
                    {
                        ITag tag = (htmlNodes[i] as ITag);
                        if (!tag.IsEndTag())
                        {
                            if (tag.Attributes != null && tag.Attributes.Count > 0)
                            {
                                string[] keysArr = new string[tag.Attributes.Count];
                                tag.Attributes.Keys.CopyTo(keysArr, 0);

                                string[] valuesArr = new string[tag.Attributes.Count];
                                tag.Attributes.Values.CopyTo(valuesArr, 0);

                                _target = new Dictionary<string, string>();

                                for (int j = 0; j < tag.Attributes.Keys.Count; j++)
                                {

                                    if (valuesArr[j] != "$<NULL>$" && keysArr[j] != "$<TAGNAME>$")
                                        _target.Add(keysArr[j], valuesArr[j]);
                                }

                                _targets.Add(_target);

                            }
                        }
                    }
                }
                return _targets;
            }
            return null;
        }

        /// <summary>
        /// 根据标签参数列表，替换页面中的节点
        /// </summary>
        /// <param name="html"></param>
        /// <param name="?"></param>
        /// <param name="replaceValues"></param>
        /// <returns></returns>
        public static string ReplaceHtmlByNodes(string html, List<Dictionary<string, string>> targets,Dictionary<string,string> replaceValues,string targetName)
        {
            if (targets != null && targets.Count > 0)
            {
                foreach (Dictionary<string, string> target in targets)
                {
                    if (target.ContainsKey("ID"))
                    {
                        int index = html.IndexOf("<" + targetName + " id=\"" + target["ID"]);
                        int length = html.Substring(index).IndexOf("/>") + 2;
                        string baseValue = html.Substring(index,length);

                        html = html.Replace(baseValue, replaceValues[target["ID"]]);
                    }
                }
                return html;
            }
            return null;
        }
    }
}
