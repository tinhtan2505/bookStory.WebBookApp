using bookStory.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.Configurations
{
    class BookImageConfiguration : IEntityTypeConfiguration<BookImage>
    {
        public void Configure(EntityTypeBuilder<BookImage> builder)
        {
            builder.ToTable("BookImages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);
            builder.Property(x => x.Caption).HasMaxLength(200);

            builder.HasOne(x => x.Book).WithMany(x => x.BookImages).HasForeignKey(x => x.IdBook);
        }
    }
}
