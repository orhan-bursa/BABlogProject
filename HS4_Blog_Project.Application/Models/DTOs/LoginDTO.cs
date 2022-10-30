using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.Models.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Must type User Name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Must type Password")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Password { get; set; }
    }
}
