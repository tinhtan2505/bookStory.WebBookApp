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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(x => x.Id); // khóa chính
            builder.Property(x => x.Id).UseIdentityColumn();

            //builder.Property(x => x.IdUser).IsRequired(); //int
            //builder.Property(x => x.IdBook).IsRequired(); //int
            //builder.Property(x => x.IdLanguage).IsRequired(); //int
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired().HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.DateProject);
            builder.Property(x => x.UserId).IsRequired(); //int
            builder.Property(x => x.Status).IsRequired(); //int

            //khóa ngoại
            builder.HasOne(x => x.AppUser).WithMany(x => x.Projects).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Book).WithMany(x => x.Projects).HasForeignKey(x => x.IdBook);
            builder.HasOne(x => x.Language).WithMany(x => x.Projects).HasForeignKey(x => x.IdLanguage);
        }
    }
}