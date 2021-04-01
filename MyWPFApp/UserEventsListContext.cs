using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWPFApp
{
    class UserEventsListContext : DbContext
    {
        public UserEventsListContext() : base("DBConnection")
        {

        }
        public DbSet<UserEventsList> UserEvents { get; set; }
    }
}
