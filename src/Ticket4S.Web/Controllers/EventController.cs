using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket4S.Entity;
using Ticket4S.Entity.Querys;

namespace Ticket4S.Web.Controllers
{
    public class EventController : Controller
    {
        private Ticket4SDbContext Db { get; }

        public EventController(Ticket4SDbContext db)
        {
            Db = db;
        }

        /*
        public ActionResult Index()
        {
            var events = Db.Event.ListAvailableEventsWithTicket();

            var viewModel = new { events };
            return View(events);
        }
        */
    }
}