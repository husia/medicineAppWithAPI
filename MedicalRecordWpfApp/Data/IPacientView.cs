using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordWpfApp.Data
{
    public interface IPacientView
    {
        string Name { get; set; }
        string Age { get; set; }
        string Anamnes { get; set; }
        string StatusPraesens { get; set; }
        string Diagnos { get; set; }
        string SecondDiagnos { get; set; }
       
    }
}
