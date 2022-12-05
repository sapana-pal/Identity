using Identity.Models;
using System;

namespace Identity.Controllers
{
    internal class ApplicationUser
    {
        public object Email { get; internal set; }
        public string SecurityStamp { get; internal set; }
        public string UserName { get; internal set; }

        public static implicit operator ApplicationUser(AppUser v)
        {
            throw new NotImplementedException();
        }
    }
}