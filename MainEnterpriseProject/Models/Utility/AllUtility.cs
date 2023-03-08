using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace MainEnterpriseProject.Models.Utility
{
    public class AllUtility
    {
        public void SendEmail(string Smtp, string FromEmail, string Password, string To, string Subject, string Message)
        {
            MailMessage MyMessage = new MailMessage();
            MyMessage.From = new MailAddress(FromEmail);
            MyMessage.To.Add(To);
            //MyMessage.To.Add(To);
            MyMessage.Subject = Subject;
            MyMessage.Body = Message;
            MyMessage.IsBodyHtml = true;
            MyMessage.Priority = MailPriority.High;
            SmtpClient mysmtp = new SmtpClient(Smtp);
            mysmtp.UseDefaultCredentials = false;
            mysmtp.EnableSsl = true;
            mysmtp.Credentials = new NetworkCredential(FromEmail, Password);
            mysmtp.Port = 25;
            mysmtp.Send(MyMessage);
        }

        public bool SendSms(string MobileNumber, string Message, string UserName, string Password, string Sender)
        {
            try
            {
                API_SendSMS.SendReceive ws = new API_SendSMS.SendReceive();

                string err = string.Empty;
                long[] mobileNos = new long[1];
                string[] messages = new string[1];
                mobileNos[0] = long.Parse(MobileNumber);
                messages[0] = Message;
                long[] result = ws.SendMessageWithLineNumber(UserName, Password, mobileNos, messages, Sender, System.DateTime.Now, ref err);
                if (err == "" || err == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;

            }
        }

        public string PersianToEnglish(string persianStr)
        {
            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9'
            };
            foreach (var item in persianStr)
            {
                persianStr = persianStr.Replace(item, LettersDictionary[item]);
            }
            return persianStr;
        }
    }
}