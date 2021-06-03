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
    public class ParagraphConfiguration : IEntityTypeConfiguration<Paragraph>
    {
        public void Configure(EntityTypeBuilder<Paragraph> builder)
        {
            builder.ToTable("Paragraphs");

            builder.HasKey(x => x.Id); // khóa chính
            builder.Property(x => x.Id).UseIdentityColumn();

            //builder.Property(x => x.IdBook).IsRequired(); //int
            builder.Property(x => x.Order).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Type).IsRequired().HasMaxLength(200);

            //khóa ngoại
            builder.HasOne(x => x.Book).WithMany(x => x.Paragraphs).HasForeignKey(x => x.IdBook);
        }
    }
}
