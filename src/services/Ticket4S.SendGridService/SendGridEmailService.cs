using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using SendGrid;
using Ticket4S.Services.Email;
using Ticket4S.Services.Email.Model;

namespace Ticket4S.SendGridService
{
    public class SendGridEmailService : IEmailService
    {
        protected string ApiKey => ConfigurationManager.AppSettings["sendgrid:ApiKey"];

        public Task EnviarEmailAsync(EMailAEnviar dadosDoEmail)
        {
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.AddTo(dadosDoEmail.EMailDestinatario);

            sendGridMessage.From = new MailAddress("noreply@Ticket4S.com", "Ticket4S"); //TODO: Colocar no appSetting
            sendGridMessage.Subject = dadosDoEmail.Assunto;
            sendGridMessage.Html = sendGridMessage.Text = dadosDoEmail.Corpo;

            var transportWeb = new Web(ApiKey);
            return transportWeb.DeliverAsync(sendGridMessage);
        }
    }
}