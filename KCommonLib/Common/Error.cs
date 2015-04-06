using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KCommonLib.Common
{
    /// <summary>
    /// 将错误日志写入到系统事件中
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="sourceName">日志分类名称</param>
        /// <param name="message">日志信息</param>
        /// <param name="type">写入类型</param>
        public static void Log(string sourceName, string message, LogType type)
        {
            EventLog eventLog = null;
            // 确定日志是否存在
            if (!(EventLog.SourceExists(sourceName)))
            {
                EventLog.CreateEventSource(sourceName, sourceName + "Log");
            }
            if (eventLog == null)
            {
                eventLog = new EventLog(sourceName + "Log");
                eventLog.Source = sourceName;
            }
            // 记录日志信息
            switch (type)
            {
                case LogType.Error:
                    eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Error);
                    break;
                case LogType.FailureAudit:
                    eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.FailureAudit);
                    break;
                case LogType.Information:
                    eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Information);
                    break;
                case LogType.SuccessAudit:
                    eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.SuccessAudit);
                    break;
                case LogType.Warning:
                    eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Warning);
                    break;
                default: eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Information);
                    break;
            }
        }

        /// <summary>
        /// 写入类型
        /// Error 错误
        /// FailureAudit 失败审核事件
        /// Information 信息事件
        /// SuccessAudit 成功审核事件
        /// Warning 警告事件
        /// </summary>
        public enum LogType { Error, FailureAudit, Information, SuccessAudit, Warning }
    }
}

//调用方法
//try
//{
//……
//}
//catch (Exception ex)
//{
//Error.Log("Town", ex.ToString());
//return false;
//}
