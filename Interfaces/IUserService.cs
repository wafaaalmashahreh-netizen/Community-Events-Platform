using System;
using System.Collections.Generic;
using System.Text;
using Community_Events_Platform.Models;



namespace Community_Events_Platform.Interfaces
{
    public interface IUserService
    {
        User? Register(string name, string email);
        User? Login(string email);
    }

}

