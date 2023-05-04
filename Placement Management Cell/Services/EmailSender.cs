using System.Net.Mail;
using System.Net;

namespace Placement_Management_Cell.Services
{
    public static class EmailSender
    {
        public static void SendEmail(string email, string body, string subject)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("chiragchavda.tatvasoft@gmail.com", "xyivzeubzckqahvi");
                client.EnableSsl = true;

                var message = new MailMessage();
                message.From = new MailAddress("chiragchavda.tatvasoft@gmail.com");
                message.To.Add(email);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch (Exception)
            {

            }
            
        }
    }
}
