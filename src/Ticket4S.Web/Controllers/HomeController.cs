using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket4S.Entity;
using Ticket4S.Entity.Querys;

namespace Ticket4S.Web.Controllers
{
    public class HomeController : Controller
    {
        private Ticket4SDbContext Db { get; }

        public HomeController(Ticket4SDbContext db)
        {
            Db = db;
        }

        public ViewResult Index()
        {
            var events = Db.Event.ListAvailableEventsWithTicket().ToList();
            return View(events);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}