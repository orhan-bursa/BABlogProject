using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.Extensions
{
    public class PictureFileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower(); // JPEG -> jpeg

                string[] extensions = { "jpg", "jpeg", "png" };

                bool result = extensions.Any(x => x.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("Valid formats are 'jpg', 'jpeg', 'png'");
                }

            }
                return ValidationResult.Success;
        }
    }
}
