using System.ComponentModel.DataAnnotations;

namespace Ticket4S.Services.Email.Model
{
    public class EMailToSend
    {
        [DataType(DataType.EmailAddress)]
        [Required, EmailAddress]
        public string DestinationEMail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }

        public override string ToString() => $"DestinationEMail: {DestinationEMail}, Subject: {Subject}";
    }
}
