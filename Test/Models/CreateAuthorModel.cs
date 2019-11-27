using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class CreateAuthorModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        public string Surname { get; set; }

        [Compare("Surname", ErrorMessage = "Укажите отчество")]
        public string Patronymic { get; set; }
    }
}
