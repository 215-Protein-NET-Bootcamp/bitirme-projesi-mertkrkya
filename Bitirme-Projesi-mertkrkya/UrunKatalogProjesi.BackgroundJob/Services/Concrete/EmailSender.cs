using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UrunKatalogProjesi.BackgroundJob.Services.Abstract;
using UrunKatalogProjesi.Data.Configurations;
using UrunKatalogProjesi.Data.Entities;
using UrunKatalogProjesi.Data.Models;

namespace UrunKatalogProjesi.BackgroundJob.Services.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly UserManager<AppUser> userManager;
        private readonly EmailConfig emailConfig;
        public EmailSender(UserManager<AppUser> userManager, IOptions<EmailConfig> options)
        {
            this.userManager = userManager;
            emailConfig = options.Value;
        }
        /// <summary>
        /// Burada mailData formatı Sold ve Unoffer durumları için kullanılmıştır.
        /// Sold durumunda parametre olarak; 
        /// Unoffer durumunda ise;
        /// </summary>
        public async Task SendEmail(EmailTypes emailType, AppUser appUser, object mailData = null)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient sc = new SmtpClient();
                sc.Port = emailConfig.EmailPort;
                sc.Host = emailConfig.EmailHost;
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential(emailConfig.EmailAccount, emailConfig.EmailPassword);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailConfig.EmailAccount, emailConfig.EmailDisplayName);
                mail.To.Add(appUser.Email);
                string subject = "";
                var body = GenerateEmailTemplate(emailType, appUser.UserName, out subject);
                if(string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(body))
                {
                    throw new Exception("Send Email Error");
                }
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;
                sc.SendAsync(mail,null);
            }
            catch (Exception e)
            {
                throw new Exception("Send Email Error with Message. Message:" + e.Message);
            }

        }
        /// <summary>
        /// Listteki ilk eleman subject'i, ikinci eleman ise body'i belirtir.
        /// </summary>
        /// <param name="emailType"></param>
        /// <param name="UserName"></param>
        /// <param name="mailData"></param>
        /// <returns></returns>
        private string GenerateEmailTemplate(EmailTypes emailType, string UserName, out string subject, object mailData = null)
        {
            System.Text.StringBuilder body = new System.Text.StringBuilder();
            subject = "";
            if (emailType == EmailTypes.Welcome)
            {
                subject = "Hoşgeldiniz!";
                body.AppendFormat(@"Sayın {0}, <br> Sisteme kayıt olduğunuz için teşekkür ederiz.<br> <br>", UserName);
            }
            else if(emailType == EmailTypes.Block)
            {
                subject = "Üyelik Durumu Bilgilendirmesi";
                body.AppendFormat(@"Sayın {0}, <br> Sisteme 3 kez hatalı giriş yaptığınız için hesabınız bloke edilmiştir.<br> <br>", UserName);
            }
            else if(emailType == EmailTypes.Sold)
            {

            }
            else if(emailType == EmailTypes.UnOffer)
            {

            }
            body.AppendFormat("Saygılarımızla, <br> Urun Katalog Projesi");
            return body.ToString();
        }
    }
}
