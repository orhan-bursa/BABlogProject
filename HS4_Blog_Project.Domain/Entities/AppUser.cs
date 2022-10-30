using HS4_Blog_Project.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Domain.Entities
{
    public class AppUser:IdentityUser,IBaseEntity//Manage ile ekledik 5.0.17
    {

        //ID IDENTİTYUSER dan gelicek.
        //public string FullName { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile UploadPath { get; set; }

        //IBaseEntity İmplemantation
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation
        //public  List<Comment> Comments { get; set; }
        //public List<Like> Likes { get; set; }



    }
}
