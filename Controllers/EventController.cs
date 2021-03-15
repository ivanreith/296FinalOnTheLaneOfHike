using Microsoft.AspNetCore.Authorization;
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
        IProposalRepository Repopro { get; set; }
        DataBaseContext Context { get; set; }
        UserManager<MemberModel> userManager;
        private SignInManager<MemberModel> signInManager;
        public EventController(IEventsRepository r, DataBaseContext c, UserManager<MemberModel> m,IProposalRepository rp)
        {
            Context = c;
            Repo = r;
            userManager = m;
            Repopro = rp;
        }

        public IActionResult Index()
        {
            List<EventModel> events = Repo.Events.ToList();

            return View(events);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddEvent()
        {
            var events = new EventViewModel();
          
            ViewBag.Action = "Add";
            ViewBag.Users = Context.Members.OrderBy(g => g.Name).ToList();
            return View("AddEvent", events);  
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddEvent(EventViewModel events)
        {
            if (ModelState.IsValid)
            {               
                EventModel newevent = new EventModel
                {
                    EventTitle = events.EventTitle,                   
                    EventText = events.EventText,
                    Member = userManager.GetUserAsync(User).Result,
                    EventTime = events.EventTime,
                    
                };
                Repo.AddEvent(newevent);
                return RedirectToAction("Index","Event");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Users = Context.Members.OrderBy(g => g.Name).ToList();
            var events = Repo.GetEventById(id);

            //  var story = Context.Story.Find(id);
            return View(events);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditEvent(EventModel events)
        {
            if (ModelState.IsValid)
            {

                events.Member = userManager.GetUserAsync(User).Result;
                events.Member.Name = events.Member.UserName;
                events.EventTime = events.EventTime;
                    Repo.UpdateEvent(events);
                    return RedirectToAction("Index", "Event"); 
            }
            else
            {
                ViewBag.Members = Context.Members.OrderBy(g => g.Name).ToList();
                return View(events);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteEvent(int id)
        {

            var events = Context.Event.Find(id);
            return View(events);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteEvent(EventModel events)
        {
            Repo.DeleteEvent(events);
            return RedirectToAction("Index", "Event");
        }

        [Authorize]
        public IActionResult Proposal()
        {
            List<ProposalModel> events = Repopro.proposals.ToList<ProposalModel>();

            return View(events);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddProposal()
        {
            var proposal = new ProposalViewModel();

            ViewBag.Action = "Add";
            ViewBag.Users = Context.Members.OrderBy(g => g.Name).ToList();
            return View("AddProposal", proposal);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddProposal(ProposalViewModel proposal)
        {
            if (ModelState.IsValid)
            {
                ProposalModel newpropo = new ProposalModel
                {
                    ProposalTitle = proposal.ProposalTitle,
                    ProposalText = proposal.ProposalText,
                    Member = userManager.GetUserAsync(User).Result,
                    ProposalTime = proposal.ProposalTime,

                };
                Repopro.AddProposal(newpropo);
                return RedirectToAction("Proposal","Event" );
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditProposal(int id)
        {
            ViewBag.Action = "EditProposal";
            ViewBag.Users = Context.Members.OrderBy(g => g.Name).ToList();
            var newpropo = Repopro.GetProposalById(id);

            //  var story = Context.Story.Find(id);
            return View(newpropo);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditProposal(ProposalModel proposal)
        {
            if (ModelState.IsValid)
            {

                proposal.Member = userManager.GetUserAsync(User).Result;
                proposal.Member.Name = proposal.Member.UserName;
                proposal.ProposalTime = DateTime.Now;
                Repopro.UpdateProposal(proposal);
                return RedirectToAction("Proposal", "Event");
            }
            else
            {
                ViewBag.Members = Context.Members.OrderBy(g => g.Name).ToList();
                return View(proposal);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult DeleteProposal(int id)
        {

            var proposal = Context.Proposals.Find(id);
            return View(proposal);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteProposal(ProposalModel proposal)
        {
            Repopro.DeleteProposal(proposal);
            return RedirectToAction("Proposal", "Event");
        }



    }
}
