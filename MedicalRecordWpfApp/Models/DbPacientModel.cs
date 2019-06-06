using MedicalRecordWpfApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordWpfApp.Models
{
    public class DbPacientModel : IPacient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Diagnos { get; set; }
        public string FirstViewFile { get; set; }
        public string EpicrisisFile { get; set; }

    }
}
