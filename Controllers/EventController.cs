using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnTheLaneOfHike.Models;
using OnTheLaneOfHike.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Controllers
{
    public class EventController : Controller
    {
        IEventsRepository Repo { get; set; }
        DataBaseContext Context { get; set; }
        UserManager<MemberModel> userManager;
        public EventController(IEventsRepository r, DataBaseContext c, UserManager<MemberModel> m)
        {
            Context = c;
            Repo = r;
            userManager = m;
        }

        public IActionResult Index()
        {
            List<EventModel> events = Repo.events.ToList<EventModel>();

            return View(events);
        }
        public IActionResult Proposal()
        {
            return View();
        }
    }
}
