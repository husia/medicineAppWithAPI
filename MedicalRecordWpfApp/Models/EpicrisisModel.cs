using MedicalRecordWpfApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordWpfApp.Models
{
    public class EpicrisisModel : IPacientView
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string DeliveryDate { get; set; }
        public string Diagnos { get; set; }
        public string SecondDiagnos { get; set; }
        public string Anamnes { get; set; }
        public string StatusPraesens { get; set; }
        public string Researches { get; set; }
        public string Treatment { get; set; }
        public string Recomendation { get; set; }

    }
}
