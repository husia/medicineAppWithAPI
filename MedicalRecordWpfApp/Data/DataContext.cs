using MedicalRecordWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordWpfApp.Data
{
    class DataContext : DbContext
    {
        public DataContext():base("DBConnection")
        {

        }
        public DbSet<DbPacientModel> PacientsDb { get; set; }
    }
}
