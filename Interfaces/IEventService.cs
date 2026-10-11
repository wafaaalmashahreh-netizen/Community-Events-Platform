using System;
using System.Collections.Generic;
using System.Text;


using Community_Events_Platform.Models; // to be able to use the models 
namespace Community_Events_Platform.Interfaces
{
      public  interface IEventService
    {
        Event CreateEvent(int userId, string title, string description,
                  string location, EventCategory category,
                  DateTime startDate, DateTime endDate,
                  int capacity, string regesterinfo);
        bool UpdateEvent(int eventId,int userId, string title, string description,
                  string location, EventCategory category,
                  DateTime startDate, DateTime endDate,
                  int capacity, string regesterinfo);// bool , True if did, False if did not 

        bool ConfirmEvent( int eventId,int userId);

        bool CancelEvent(int eventId,int userId);
        Event? GetEventById(int eventId);
            List <Event> GetAllEvents();
    }
}
