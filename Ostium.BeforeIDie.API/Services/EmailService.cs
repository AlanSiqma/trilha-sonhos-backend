using Ostium.BeforeIDie.API.Model.Contracts.Services;
using Ostium.BeforeIDie.API.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBoxDeveloper.TemplateEmail.Package.Contracts;
using ToolBoxDeveloper.TemplateEmail.Package.Dto;

namespace Ostium.BeforeIDie.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailProxy _emailProxy;
        private readonly EmailTrilhaSettings _emailTrilhaSettings;
        public EmailService(IEmailProxy emailProxy,
                                  EmailTrilhaSettings emailTrilhaSettings)
        {
            this._emailProxy = emailProxy;
            this._emailTrilhaSettings = emailTrilhaSettings;
        }
        public async Task<bool> SolicitarResetDeSenha(string nome, string idSonhador, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(nome))
                    throw new ArgumentNullException("Nome é um parametro obrigatorio");

                if (string.IsNullOrEmpty(nome))
                    throw new ArgumentNullException("idSonhador é um parametro obrigatorio");

                if (string.IsNullOrEmpty(nome))
                    throw new ArgumentNullException("Email é um parametro obrigatorio");

                Dictionary<string, string> payload = new Dictionary<string, string>();
                payload.Add("USUARIO", nome);
                payload.Add("URL", $"{ this._emailTrilhaSettings.UrlTrilhaSonhos}/reset-password/{idSonhador}");

                SendEmailDto dtoSendEmail = new SendEmailDto(
                                            sender: this._emailTrilhaSettings.Sender,
                                            destination: email,
                                            subject: "RESET DE SENHA",
                                            payload: payload,
                                            idTemplate: "60a901543b318194fd678b7c",
                                            user: this._emailTrilhaSettings.Sender);

                return await this._emailProxy.SendEmailFromTemplate(dtoSendEmail);
            }
            catch (System.Exception)
            {

                return false;
            }
        }
    }

}
