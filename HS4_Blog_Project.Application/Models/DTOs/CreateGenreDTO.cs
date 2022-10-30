using HS4_Blog_Project.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.Models.DTOs
{
    public class CreateGenreDTO
    {
        [Required(ErrorMessage = "Must type Name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Name { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
