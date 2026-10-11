using Community_Events_Platform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Community_Events_Platform.Interfaces
{
   public  interface IEventRequestService


    {
        EventRequest AddRequest(int userId, string text);
        List<EventRequest> GetAllRequests();


    }
}
