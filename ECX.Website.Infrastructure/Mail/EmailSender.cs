using ECX.Website.Application.Contracts.Infrastructure;
using ECX.Website.Application.DTOs.Email;
using ECX.Website.Application.Models;
using ECX.Website.Application.Response;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace ECX.Website.Infrastructure.Mail
{

    public class EmailSender : IEmailSender
    {
        private IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public async Task<ResponseAccount> SendEmail(SendEmailDto model)
        {

            //creating the object of MailMessage
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("ecxsubscription@ecx.com"); //From Email Id
            mailMessage.Subject = "Membership Registration "; //Subject of Email
            mailMessage.Body = "Hi , " + " thank you for subscribing  !!";
            //body or message of Email

            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(model.To)); //reciver's Email Id

            SmtpClient smtp = new SmtpClient(); // creating object of smptpclient
            smtp.Host = "10.3.5.55"; //host of emailaddress for example smtp.gmail.com etc

            //network and security related credentials

            smtp.EnableSsl = false;


            smtp.Port = 25;
            NetworkCredential NetworkCred = new NetworkCredential();
            //MailAddress ma = new MailAddress(txtFrom.Text, txtName.Text);
            //Mail.From = ma;
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = "Testing12";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            try
            {
                smtp.Send(mailMessage); //sending Email
                return new ResponseAccount
                {
                    Message = "User created successfully!",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ResponseAccount
                {
                    Message = ex.Message,
                    Success = false,
                };
            }


            //var email = new MimeMessage();
            //    email.From.Add(MailboxAddress.Parse((_configuration["EmailSettings:EmailUserName"])));
            //    email.To.Add(MailboxAddress.Parse(model.To));
            //    email.Subject = model.Subject;
            //    email.Body = new TextPart(TextFormat.Html) { Text = model.Body };

            //    using var smtp = new SmtpClient();
            //    smtp.Connect((_configuration["EmailSettings:EmailHost"]), 465, SecureSocketOptions.StartTls);
            //    smtp.Authenticate((_configuration["EmailSettings:EmailUserName"]), (_configuration["EmailSettings:EmailPassword"]));
            //    var result = smtp.Send(email);
            //    smtp.Disconnect(true);





            //throw new NotImplementedException();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@authdemo.com", "JWT Auth Demo");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}

//public class EmailSender : IEmailSender
//{
//    private  EmailSetting _emailSetting { get; }

//    public EmailSender(IOptions<EmailSetting> emailSetting)
//    {
//        _emailSetting = emailSetting.Value;
//    }

//    public async Task<bool> SendEmail(Email email)
//    {
//        var client = new SendGridClient(_emailSetting.ApiKey);
//        var to = new EmailAddress(email.To);
//        var from = new EmailAddress
//        {
//            Email = _emailSetting.FromAddress,
//            Name = _emailSetting.FromName
//        };
//        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
//        var response = await client.SendEmailAsync(message);
//        return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
//    }
//}
