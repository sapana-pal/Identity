using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Models
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }

        public static implicit operator AppUser(IdentityRole v)
        {
            throw new NotImplementedException();
        }
    }
}