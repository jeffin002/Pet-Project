using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Mvc.Models;

namespace Web.Mvc.Helper

{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class Message
    {

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IConfiguration config)
        {
            To = new List<MailboxAddress>();
            //To.AddRange(to.Select(x => new MailboxAddress(x, )));
            To.AddRange(to.Select(x => new MailboxAddress(config.GetSection("EmailConfiguration").Get<EmailConfiguration>().From, x)));
            Subject = subject;
            Content = content;
        }
    }
    public class EmailHelper
    {
        private readonly IConfiguration _config;
        private readonly string _fromAddress;
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _testEmailAddress;

        public EmailHelper(IConfiguration config)
        {
            
            _config = config;
            _fromAddress = _config.GetSection("EmailConfiguration").Get<EmailConfiguration>().From;
            _smtpServer = _config.GetSection("EmailConfiguration").Get<EmailConfiguration>().SmtpServer;
            _port = _config.GetSection("EmailConfiguration").Get<EmailConfiguration>().Port;
            _userName = _config.GetSection("EmailConfiguration").Get<EmailConfiguration>().UserName;
            _password = _config.GetSection("EmailConfiguration").Get<EmailConfiguration>().Password;
            _testEmailAddress = _config.GetSection("MyEmailAddress").Value;
        }
        private MimeMessage CreateEmailMessage(Message message)
        {

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("PetProject", _fromAddress));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_smtpServer, _port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_userName, _password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
