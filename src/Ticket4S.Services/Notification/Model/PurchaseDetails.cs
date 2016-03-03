using System;
using System.ComponentModel.DataAnnotations;
using Ticket4S.Entity.Event;

namespace Ticket4S.Services.Notification.Model
{
    public class PurchaseDetails
    {
        public virtual Guid PurchaseId { get; set; }

        [Required]
        public virtual Event BoughtEvent { get; set; }

        [Required]
        public virtual EventTicketType BoughtTicket { get; set; }

    }
}