using System;
using System.Collections.Generic;

namespace Ticket4S.Web.ViewModels
{
    public class EventViewModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }

        public virtual IList<EventTicketTypeViewModel> TicketsTypes { get; set; } = new List<EventTicketTypeViewModel>();
    }
}