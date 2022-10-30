using HS4_Blog_Project.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Domain.Entities
{
    public class Like :IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        //Navigation
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
