using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ticket4S.Entity.User
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Usuario : IdentityUser
    {
        [Required]
        public virtual string Nome { get; set; }

        [Required]
        public virtual string CPF { get; set; }

        [Required]
        public virtual DateTime DataNascimento { get; set; }

        [Required]
        public virtual Sexo Sexo { get; set; }

        [Required]
        public virtual Endereco Endereco { get; set; }


        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }

        ///////////////////////////////////////////////////////////////////////////
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Usuario> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}