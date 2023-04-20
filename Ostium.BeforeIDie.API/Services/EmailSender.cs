
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ostium.BeforeIDie.API.Model.Contracts.Services;
using Ostium.BeforeIDie.API.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Ostium.BeforeIDie.API.Services
{
    public class EmailSender : IEmailService
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("contato@trilhasonhos.com.br", "Password Recovery"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            var response = await client.SendEmailAsync(msg);
        }

        public async Task<bool> SolicitarResetDeSenha(string nome, string idSonhador, string email)
        {
            try
            {
                string subject = "Redefinir senha";
                string message = $"Olá {nome}, redefina sua senha <a href='https://www.trilhasonhos.com.br/reset-password/{idSonhador}'>clicando aqui</a>.";

                await this.SendEmailAsync(email, subject, message);

                return await Task.FromResult(true);
               
            }
            catch (System.Exception)
            {
                return await Task.FromResult(false); ;
            }
        }
    }
}
