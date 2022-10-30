using HS4_Blog_Project.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Infra.EntityTypeConfig
{
    //Identity AppUser'ı configure ediyor, biz sadece eklediğimiz propertyleri config edebiliriz.
    class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //Migration yaptığımızda Identity'nin configlerini kontrol etmeyi unutmayalım.
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(30);

            builder.Property(x => x.ImagePath).IsRequired(false);

            base.Configure(builder);
        }
    }
}
