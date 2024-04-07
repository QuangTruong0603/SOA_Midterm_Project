using System.Net.Mail;
using System.Net;

namespace do_an_ck.Helper
{
    public class SendMail
    {
        public static bool SendEMail(string to, string subject, string body, string attachFile)
        {
            try
            {
                MailMessage message = new MailMessage(ConstantHelper.emailSender, to, subject, body);
                using (var client = new SmtpClient(ConstantHelper.hostEmail, ConstantHelper.portEmail))
                {
                    client.EnableSsl = true;
                    if (!string.IsNullOrEmpty(attachFile))
                    {
                        Attachment attachment = new Attachment(attachFile);
                        message.Attachments.Add(attachment);
                    }
                    NetworkCredential networkCredential = new NetworkCredential(ConstantHelper.emailSender, ConstantHelper.passwordSender);
                    client.UseDefaultCredentials = false;
                    client.Credentials = networkCredential;
                    client.Send(message);

                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
