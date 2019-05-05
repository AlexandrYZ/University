using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models
{
    public class ProfessorModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CathedraId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public string WorkPosition { get; set; }
        public string AcademicDegree { get; set; }
        public string Other { get; set; }
    }
}
