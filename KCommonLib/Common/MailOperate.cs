using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace KCommonLib.Common
{
    /// <summary>
    /// 邮件操作方法类
    /// </summary>
    public class MailOperate
    {
        static string _body, _subject, _addressee;

        /// <summary>
        /// 开始发送邮件
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static bool StartSendMail(string body, string subject,string addressee)
        {
            _body = body;
            _subject = subject;
            _addressee = addressee;
            System.Threading.Thread _thread = new System.Threading.Thread(new System.Threading.ThreadStart(SendMail));
            _thread.Start();
            return true;
        }

        static void SendMail()
        {
            string address = "";
            string displayName = "";
            string addressee = _addressee;// "13901913220@139.com";//收件人 ,分割
            string formAddressee = "kriswj@sina.com";//发件人地址
            string formName = "kriswang";//发件人姓名

            MailAddress from = new MailAddress(formAddressee, formName); //邮件的发件人
            MailMessage newMailMessage = new MailMessage();

            string[] mailNames = (addressee).Split(',');
            {
                try
                {
                    foreach (string name in mailNames)
                    {
                        if (name != string.Empty)
                        {
                            if (name.IndexOf('<') > 0)
                            {
                                displayName = name.Substring(0, name.IndexOf('<'));
                                address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');

                            }
                            else
                            {
                                displayName = string.Empty;
                                address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                            }

                        }
                        newMailMessage.From = from;

                        newMailMessage.To.Add(new MailAddress(address, displayName));

                        newMailMessage.Body = _body;
                        newMailMessage.Subject = _subject;

                        //设置SMTP服务器地址 
                        SmtpClient newclient = new SmtpClient("smtp.sina.com");   //以新浪为例（126的用不了，也不晓得为啥）
                        newclient.UseDefaultCredentials = false;
                        //此处设置发件人邮箱的用户名和密码 
                        newclient.Credentials = new System.Net.NetworkCredential("kriswj", "wj840728"); //发件人的账号和密码
                        newclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //newMailMessage.Attachments.Add(new Attachment(txt附件.Text));          // 发送附件
                        newMailMessage.Priority = MailPriority.High;  //设置发送级别
                        //发送邮件 
                        newclient.Send(newMailMessage);

                    }
                    //MessageBox.Show("邮件发送完毕！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch 
                {
                    //MessageBox.Show("邮件发送发生错误：" + exp.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
