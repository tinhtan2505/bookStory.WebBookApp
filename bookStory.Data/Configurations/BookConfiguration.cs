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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books"); // tên bảng

            builder.HasKey(x => x.Id); // khóa chính
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.FileName).IsRequired().HasMaxLength(200); //định dạng string
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Author).IsRequired().HasMaxLength(200);
        }
    }
}
