using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Hangfire;
using Microsoft.AspNet.Identity;
using Ticket4S.Entity;
using Ticket4S.Entity.Purchase;
using Ticket4S.Entity.Querys;
using Ticket4S.Services.Payment.Model;
using Ticket4S.Services.Purchase;
using Ticket4S.Web.ViewModels;

namespace Ticket4S.Web.Controllers
{
    public class EventController : Controller
    {
        public IMapper Mapper { get; }
        public MapperConfiguration MapperConfig { get; }
        private Ticket4SDbContext Db { get; }
        public PurchaseService PurchaseService { get; }

        public EventController(Ticket4SDbContext db, MapperConfiguration mapperConfig, IMapper mapper, PurchaseService purchaseService)
        {
            Mapper = mapper;
            PurchaseService = purchaseService;
            MapperConfig = mapperConfig;
            Db = db;
        }

        public async Task<ActionResult> Ingresso(Guid? id = null)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            var evento = await Db.EventTicketTypes.FindAsync(id);
            var viewModel = Mapper.Map<EventTicketTypeWithEventViewModel>(evento);
            return View("Ingresso", viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Comprar(OrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                var evento = await Db.EventTicketTypes.FindAsync(order.BoughtTicketId);
                var viewModel = Mapper.Map<EventTicketTypeWithEventViewModel>(evento);
                return View("Ingresso", viewModel);
            }

            var ticketType = await Db.EventTicketTypes.FindAsync(order.BoughtTicketId);
            if (ticketType == null)
                return HttpNotFound();

            var pOrder = new PurchaseOrder
            {
                Id = Guid.NewGuid(),
                UserRequestToSaveCreditCard = order.UserRequestToSaveCreditCard,
                OrderDateTime = DateTimeOffset.Now,
                BuyerUserId = User.Identity.GetUserId(),
                BoughtTicketId = ticketType.Id,
                BoughtEventId = ticketType.EventId,
                BilledValue = ticketType.Value,
                Condition = Condition.WaitingProcessing,
                ChangedAt = DateTimeOffset.Now
            };
            pOrder = Mapper.Map(pOrder, Db.PurchaseOrders.Create());

            Db.PurchaseOrders.Add(pOrder);
            await Db.SaveChangesAsync();

            var billing = new BillingWithNewCreditCard()
            {
                Id = pOrder.Id,
                Value = pOrder.BilledValue,
                CreditCard = Mapper.Map<CreditCardInfo>(order)
            };

            /*
            await PurchaseService.PurchaseTicketAsync(pOrder.Id, billing);
            */
            //BackgroundJob.Enqueue<BillingWithNewCreditCard>(()=> PurchaseService.PurchaseTicket(pOrder.Id, billing));
            BackgroundJob.Enqueue(() => PurchaseService.PurchaseTicket(pOrder.Id, billing));

            return RedirectToAction("Thanks");
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}