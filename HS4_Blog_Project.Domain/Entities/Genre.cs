using HS4_Blog_Project.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Domain.Entities
{
    public class Genre:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //IBaseEntity implementation
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Status Status { get; set; }

        //Navigation
        public List<Post> Posts { get; set; }
    }
}
