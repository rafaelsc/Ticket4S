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
    public class SendGridSendEmailService : ISendEmailService
    {
        protected ILogger Log { get; }
        protected string ApiKey => ConfigurationManager.AppSettings["sendgrid:ApiKey"];

        public SendGridSendEmailService(ILogger log)
        {
            Log = log.ForContext<SendGridSendEmailService>();
        }

        public Task SendAsync(EMailToSend emailData)
        {
            Contract.Requires(emailData != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationErro(nameof(emailData), emailData);

            Log.Information("Enviando de Email Pelo SendGrid");

            try
            {
                var sendGridMessage = new SendGridMessage();
                sendGridMessage.AddTo(emailData.DestinationEMail);

                sendGridMessage.From = new MailAddress("noreply@Ticket4S.com", "Ticket4S"); //TODO: Colocar no appSetting
                sendGridMessage.Subject = emailData.Subject;
                sendGridMessage.Html = sendGridMessage.Text = emailData.Body;

                Log.Debug("Dados de Envido do e-mail. {dadosDoEmail}", emailData);

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