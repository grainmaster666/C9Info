using System;
using System.ComponentModel.DataAnnotations;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace C9info.Models
{
    public class EmailModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Message { get; set; }

    }

    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class Email
    {
        public  bool SendEmail(EmailModel emailModel, MailSettings _mailSettings)
        {
            try
            {
                MimeMessage email = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
                email.From.Add(emailFrom);


                MailboxAddress emailTo = new MailboxAddress(emailModel.Name, emailModel.Email);
                email.To.Add(emailTo);

                email.Subject = emailModel.Subject;

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = emailModel.Message;
                email.Body = emailBodyBuilder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                smtp.SendAsync(email);
                smtp.Disconnect(true);
                smtp.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    
}
