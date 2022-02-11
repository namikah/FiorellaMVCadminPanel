using System;
using System.ComponentModel.DataAnnotations;

namespace FirstFiorellaMVC.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Cannot be empty"), MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Context Cannot be empty"), MaxLength(150)]
        public string Context { get; set; }

        [MaxLength()]
        public string Image { get; set; }

        [Required(ErrorMessage = "Date Cannot be empty"), MaxLength(50)]
        public DateTime Datetime { get; set; }
    }
}
