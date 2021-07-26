using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bookStory.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookStory.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");

            builder.HasKey(x => x.Id); // khóa chính
            //builder.HasKey(t => new {t.Id, t.IdUser, t.IdTranslation });
            builder.Property(x => x.Id).UseIdentityColumn();

            //builder.Property(x => x.IdUser).IsRequired(); //int
            //Property(x => x.IdTranslation).IsRequired(); //int
            builder.Property(x => x.Vote).IsRequired(); //int

            //khóa ngoại
            builder.HasOne(x => x.AppUser).WithMany(x => x.Ratings).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Translation).WithMany(x => x.Ratings).HasForeignKey(x => x.IdTranslation);
        }
    }
}