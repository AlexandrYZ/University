using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class CathedraModel
    {
        [Required]
        public  int Id { get; set; }
        [Required]
        public string CathedraName { get; set; }
        [Required]
        public int FoundationYear { get; set; }
        [Required]
        public int FacultyId { get; set; }
    }
}
