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
    public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            builder.ToTable("Translations");

            builder.HasKey(x => x.Id); // khóa chính
            builder.Property(x => x.Id).UseIdentityColumn();

            //builder.Property(x => x.IdUser).IsRequired(); //int
            //builder.Property(x => x.IdProject).IsRequired(); //int
            //builder.Property(x => x.IdParagraph).IsRequired(); //int
            builder.Property(x => x.Text).IsRequired().HasColumnType("nvarchar(MAX)");
            builder.Property(x => x.Rating).IsRequired().HasMaxLength(200);
            //builder.Property(x => x.IdProject).IsRequired(); //int

            //khóa ngoại
            builder.HasOne(x => x.AppUser).WithMany(x => x.Translations).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Paragraph).WithMany(x => x.Translations).HasForeignKey(x => x.IdParagraph);
            //builder.HasOne(x => x.Project).WithMany(x => x.Translations).HasForeignKey(x => x.IdProject);
        }
    }
}