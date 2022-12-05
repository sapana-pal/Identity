using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Identity.Models
{
    public class AppIdentityDbContext: IdentityDbContext<AppUser>
    {
        //public AppIdentityDbContext():base("AuthDbContextConnection")
        //{
        //}
        public AppIdentityDbContext() 
        {
        }
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }
        //    public AppIdentityDbContext(DbContextOptions options)
        //: base(options)
        //    {
        //    }
       
        public DbSet<Customer> customer { get; set; }
        public IEnumerable<object> AspNetUsers { get; internal set; }
    }
}
    