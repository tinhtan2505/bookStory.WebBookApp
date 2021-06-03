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
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(x => x.Id); // khóa chính
            builder.Property(x => x.Id).UseIdentityColumn();

            //builder.Property(x => x.IdUser).IsRequired(); //int
            //builder.Property(x => x.IdParagraph).IsRequired(); //int
            builder.Property(x => x.Reason).IsRequired().HasMaxLength(200);

            //khóa ngoại
            builder.HasOne(x => x.AppUser).WithMany(x => x.Reports).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Paragraph).WithMany(x => x.Reports).HasForeignKey(x => x.IdParagraph);
        }
    }
}
