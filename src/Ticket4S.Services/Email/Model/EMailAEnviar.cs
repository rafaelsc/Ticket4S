using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket4S.Services.Email.Model
{
    public class EMailAEnviar
    {
        [DataType(DataType.EmailAddress)]
        [Required, EmailAddress]
        public string EMailDestinatario { get; set; }
        [Required]
        public string Assunto { get; set; }
        [Required]
        public string Corpo { get; set; }


        public override string ToString() => $"EMailDestinatario: {EMailDestinatario}, Assunto: {Assunto}";
    }
}
