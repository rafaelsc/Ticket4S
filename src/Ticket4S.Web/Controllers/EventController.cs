using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Ticket4S.Entity;
using Ticket4S.Entity.Querys;
using Ticket4S.Web.ViewModels;

namespace Ticket4S.Web.Controllers
{
    public class EventController : Controller
    {
        public IMapper Mapper { get; }
        public MapperConfiguration MapperConfig { get; }
        private Ticket4SDbContext Db { get; }

        public EventController(Ticket4SDbContext db, MapperConfiguration mapperConfig, IMapper mapper)
        {
            Mapper = mapper;
            MapperConfig = mapperConfig;
            Db = db;
        }

        public async Task<ActionResult> Ingresso(Guid? id = null)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            var evento = await Db.EventTicketTypes.FindAsync(id);
            var viewModel = Mapper.Map<EventTicketTypeWithEventViewModel>(evento);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Comprar()
        {
            throw new NotImplementedException();
        }

    }
}