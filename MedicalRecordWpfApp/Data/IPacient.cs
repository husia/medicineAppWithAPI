using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordWpfApp.Data
{
    interface IPacient
    {
         string Name { get; set; }
         string Age { get; set; }
         string Diagnos { get; set; }
    }
}
