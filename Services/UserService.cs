using Community_Events_Platform.Interfaces;
using Community_Events_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Community_Events_Platform.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> Users  =  new ();
        private int nextid = 1;
       public User Register (  string name , string Email )
        {
             var user = new User (nextid++, name, Email);
            Users.Add(user);
            return user;
        }

        public User? Login (String Email)
        {
            return Users.FirstOrDefault(u => u.Email.Equals(Email, StringComparison.OrdinalIgnoreCase)); // OrdinalIgnoreCase We used it to ignore case (uppercase and lowercase letters).
        }



    }
}
