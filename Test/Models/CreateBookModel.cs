using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class CreateBookModel
    {
        [Required(ErrorMessage = "Укажите название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите ID автора")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Укажите издательство")]
        public string PublishingHouse { get; set; }

        [Required(ErrorMessage = "Укажите год издания")]
        public int YearOfPublishing { get; set; }
    }
}
