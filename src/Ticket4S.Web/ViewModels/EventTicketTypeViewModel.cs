using System;
using Ticket4S.Entity.Event;

namespace Ticket4S.Web.ViewModels
{
    public class EventTicketTypeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class EventTicketTypeWithEventViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }

        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public string EventShortName { get; set; }

        public EventPlaceViewModel EventPlace { get; set; }
    }

    public class EventPlaceViewModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }

        public virtual string StateName { get; set; }
        public virtual string CityName { get; set; }
        public virtual string DistrictName { get; set; }

        public override string ToString()
        {
            return $"Local: {Name}, Estado: {StateName}, Cidade: {CityName}, Bairro: {DistrictName}";
        }
    }
}