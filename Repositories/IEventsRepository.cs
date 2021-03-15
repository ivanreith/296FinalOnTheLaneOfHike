using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Repositories
{
    public interface IEventsRepository
    {
        IQueryable<EventModel> Events { get; }
        void AddEvent(EventModel events);  // create
        EventModel GetEventById(int EventId); //Retrieve a story by topic
        void UpdateEvent(EventModel events);
        void DeleteEvent(EventModel events);
    }
}
