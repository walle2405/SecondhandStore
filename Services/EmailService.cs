using System.Net;
using System.Net.Mail;
using SecondhandStore.Extension;

namespace SecondhandStore.Services;

    public interface IEmailService
    {
        void SendMail(SendMailModel model);
    }

    public class EmailService : IEmailService
    {
        public void SendMail(SendMailModel model)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    Subject = model.Subject,
                    Body = model.Content,
                    IsBodyHtml = false,
                };
                mailMessage.From = new MailAddress(EmailSettingModel.Instance.FromEmailAddress, EmailSettingModel.Instance.FromDisplayName);
                mailMessage.To.Add(model.ReceiveAddress);

                var smtp = new SmtpClient()
                {
                    EnableSsl = EmailSettingModel.Instance.Smtp.EnableSsl,
                    Host = EmailSettingModel.Instance.Smtp.Host,
                    Port = EmailSettingModel.Instance.Smtp.Port,
                };
                var network = new NetworkCredential(EmailSettingModel.Instance.Smtp.EmailAddress, EmailSettingModel.Instance.Smtp.Password);
                smtp.Credentials = network;

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
