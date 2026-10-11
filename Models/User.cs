using System;
using System.Collections.Generic;
using System.Text;

namespace Community_Events_Platform.Models
{
    public class User

    {
       
            public int UserId { get; }
            public string Name { get; private set; }
            public string Email { get; private set; }
            public User(int userid, string name, string email)
            {
                UserId = userid;
                Name = name;
                Email = email;

            }


        }
    }

