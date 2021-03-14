using Microsoft.EntityFrameworkCore;
using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        DataBaseContext context;
        public IQueryable<EventModel> events
        {
            get
            {
                return context.Event.Include(events => events.Member);
            }
        
        }

        public void AddEvent(EventModel events)
        {
            events.EventTime = DateTime.Now;
           
            context.Event.Add(events);
            context.SaveChanges();
        }

        public void DeleteEvent(EventModel events)
        {
            context.Event.Remove(events);
            context.SaveChanges();
        }

        public EventModel GetEventById(int EventId)
        {
            var events = (from e in context.Event
                         where e.EventID == EventId
                          select e).FirstOrDefault<EventModel>();           
            return events;
        }

        public void UpdateEvent(EventModel events)
        {
            context.Event.Update(events);
            context.SaveChanges();
        }
    }
}
