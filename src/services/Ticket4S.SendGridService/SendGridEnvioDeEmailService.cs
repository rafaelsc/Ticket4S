using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;
using Serilog;
using Ticket4S.Services.Email;
using Ticket4S.Services.Email.Model;
using Ticket4S.Utils;

namespace Ticket4S.SendGridService
{
    public class SendGridEnvioDeEmailService : IEnvioDeEmailService
    {
        protected ILogger Log { get; }
        protected string ApiKey => ConfigurationManager.AppSettings["sendgrid:ApiKey"];

        public SendGridEnvioDeEmailService(ILogger log)
        {
            Log = log.ForContext<SendGridEnvioDeEmailService>();
        }

        public Task EnviarAsync(EMailAEnviar dadosDoEmail)
        {
            Contract.Requires(dadosDoEmail != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationErro(nameof(dadosDoEmail), dadosDoEmail);

            Log.Information("Enviando de Email Pelo SendGrid");

            try
            {
                var sendGridMessage = new SendGridMessage();
                sendGridMessage.AddTo(dadosDoEmail.EMailDestinatario);

                sendGridMessage.From = new MailAddress("noreply@Ticket4S.com", "Ticket4S"); //TODO: Colocar no appSetting
                sendGridMessage.Subject = dadosDoEmail.Assunto;
                sendGridMessage.Html = sendGridMessage.Text = dadosDoEmail.Corpo;

                Log.Debug("Dados de Envido do e-mail. {dadosDoEmail}", dadosDoEmail);

                var transportWeb = new Web(ApiKey);
                return transportWeb.DeliverAsync(sendGridMessage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro GRAVE INTERNO durante o Envio de E-mail. {Exception}", ex);
                throw;
            }
            finally
            {
                Log.Information("E-mail enviado com Sucesso.");
            }
        }
    }
}