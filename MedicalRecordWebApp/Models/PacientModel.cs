using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRecordWebApp.Models
{
    public class PacientModel
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(250)")]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Age { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Diagnos { get; set; }
    }
}
