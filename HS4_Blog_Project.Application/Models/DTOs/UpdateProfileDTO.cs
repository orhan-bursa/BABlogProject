using HS4_Blog_Project.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.Models.DTOs
{
    public class UpdateProfileDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Must type User Name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Must type Password")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must type Password")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Must type Password")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }

    }
}
