using System;
using System.Collections.Generic;
using System.Text;

namespace Community_Events_Platform.Models
{
    public  class Event
    {
       
            public int EventId { get; }
            public string Title { get; private set; }
            public int UserId { get; }
            public DateTime StartDate { get; private set; }
            public DateTime EndDate { get; private set; }
            public string Description { get; private set; }
            public int Capacity { get; private set; }

            public string ? RegesterInfo { get; private set; }
            public string Location { get; private set; }
            public EventStatus Status { get; private set; }
            public EventCategory Category { get; private set; }


            public Event(int eventid, string title, int userid, DateTime startdate, DateTime enddate, string description, int capacity, string regesterinfo, string location, EventCategory category)
                
            {
                EventId = eventid;
                Title = title;
                UserId = userid;
                StartDate = startdate;
                EndDate = enddate;
                Description = description;
                Capacity = capacity;
                RegesterInfo = regesterinfo;
                Location = location;
                Status = EventStatus.Proposed;
                Category =category;
            }
        public void Confirm () => Status = EventStatus.Confirmed;

        public void Cancel () => Status = EventStatus.Cancelled;

        public void UpdateDetails(string title, string description, string location,
                          EventCategory category, DateTime startDate, DateTime endDate,
                          int capacity, string? regesterInfo)
        {
            Title = title;
            Description = description;
            Location = location;
            Category = category;
            StartDate = startDate;
            EndDate = endDate;
            Capacity = capacity;
            RegesterInfo = regesterInfo;
        }

    }
    }
