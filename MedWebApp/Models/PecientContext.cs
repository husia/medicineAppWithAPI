using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedWebApp.Models
{
    public class PecientContext : DbContext
    {
        public PecientContext(DbContextOptions<PecientContext> options): base(options)
        {

        }
        public DbSet<Pacient> Pacients { get; set; }
    }
   
}
