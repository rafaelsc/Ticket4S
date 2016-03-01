using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ticket4S.Entity.GatewayPayment;

namespace Ticket4S.Entity.User
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string CPF { get; set; }

        [Required]
        public virtual DateTime DateOfBirth { get; set; }

        [Required]
        public virtual Gender Gender { get; set; }

        //[Required]
        [ForeignKey(nameof(Address))]
        public virtual Guid? AddressId { get; set; }
        public virtual Address Address { get; set; }
        
        public ICollection<SavedCreditCard> SavedCreditCards { get; protected set; } = new List<SavedCreditCard>();


        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }

        ///////////////////////////////////////////////////////////////////////////
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}