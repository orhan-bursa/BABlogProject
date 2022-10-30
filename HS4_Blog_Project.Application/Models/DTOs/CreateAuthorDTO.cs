using HS4_Blog_Project.Application.Extensions;
using HS4_Blog_Project.Domain.Entities;
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
    public class CreateAuthorDTO
    {
        [Required(ErrorMessage ="Must type First Name")]
        [MinLength(3,ErrorMessage ="Minimum lenght is 3")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Must type Last Name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string LastName { get; set; }
        public string ImagePath { get; set; }

        //Custom Extension yazılacak. Custom Data Annotation. jpeg,png uzantılı dosyalar sadece yüklensin
        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

    }
}
