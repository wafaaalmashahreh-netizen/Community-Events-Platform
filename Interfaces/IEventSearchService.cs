using System;
using System.Collections.Generic;
using System.Text;
using Community_Events_Platform.Models;

namespace Community_Events_Platform.Interfaces
{
     public interface IEventSearchService

    {

        List<Event> Search(Predicate<Event> filter);
        List<Event> Search(Predicate<Event> filter , Comparison<Event> sortRule);  


    }
}
