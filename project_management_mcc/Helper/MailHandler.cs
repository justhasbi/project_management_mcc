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

        public string subjectMail;

        public MailHandler(string stringHtmlMessage, string destinationEmail, string subjectMail)
        {
            this.stringHtmlMessage = stringHtmlMessage;
            this.destinationEmail = destinationEmail;
            this.subjectMail = subjectMail;
        }

        public static void Email(string stringHtmlMessage, string destinationEmail, string subjectMail)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("uzumakijohn82@gmail.com");
            message.To.Add(new MailAddress(destinationEmail));
            message.Subject = subjectMail;
            message.IsBodyHtml = true;
            message.Body = stringHtmlMessage;
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("uzumakijohn82@gmail.com", "projectmm82");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
        }
    }
}
