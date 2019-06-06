using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedWebApp.Models
{
    public class Pacient
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="необходимо ввести имя")]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Возраст")]
        public string Age { get; set; }
        [DisplayName("Диагноз")]
        public string Diagnos { get; set; }
    }
}
