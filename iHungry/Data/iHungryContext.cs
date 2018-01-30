using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iHungry.Models;

namespace iHungry.Models
{
    public class iHungryContext : DbContext
    {
        public iHungryContext (DbContextOptions<iHungryContext> options)
            : base(options)
        {
        }

        public DbSet<iHungry.Models.Jelo> Jelo { get; set; }

        public DbSet<iHungry.Models.Košarica> Košarica { get; set; }
    }
}
