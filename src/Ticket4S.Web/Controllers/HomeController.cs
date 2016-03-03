using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ticket4S.Entity;
using Ticket4S.Entity.Querys;
using Ticket4S.Web.ViewModels;

namespace Ticket4S.Web.Controllers
{
    public class HomeController : Controller
    {
        public MapperConfiguration MapperConfig { get; }
        private Ticket4SDbContext Db { get; }

        public HomeController(Ticket4SDbContext db, MapperConfiguration mapperConfig)
        {
            MapperConfig = mapperConfig;
            Db = db;
        }

        public async Task<ViewResult> Index()
        {
            var events = await Db.Event.ListAvailableEventsWithTicket().ProjectTo<EventViewModel>(MapperConfig).ToListAsync();
            return View(events);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

       
    }
}