using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace project_management_mcc.Helper
{
    public class MailHandler
    {
        public string stringHtmlMessage;

        public string destinationEmail;

        public MailHandler(string stringHtmlMessage, string destinationEmail)
        {
            this.stringHtmlMessage = stringHtmlMessage;
            this.destinationEmail = destinationEmail;
        }

        public static void Email(string stringHtmlMessage, string destinationEmail)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("justhasbi7699@gmail.com");
            message.To.Add(new MailAddress(destinationEmail));
            message.Subject = "Reset Password";
            message.IsBodyHtml = true;
            message.Body = stringHtmlMessage;
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("justhasbi7699@gmail.com", "**");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
        }
    }
}
