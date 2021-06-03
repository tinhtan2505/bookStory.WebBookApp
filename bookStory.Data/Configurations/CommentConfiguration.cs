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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments"); // tên bảng

            builder.HasKey(x => x.Id); // khóa chính
            builder.Property(x => x.Id).UseIdentityColumn();
            //builder.HasKey(t => new { t.IdUser, t.IdTranslation });

            //builder.Property(x => x.IdUser).IsRequired(); //int
            builder.Property(x => x.IdTranslation).IsRequired();
            builder.Property(x => x.Message).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DateComment);

            //khóa ngoài
            builder.HasOne(x => x.AppUser).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Translation).WithMany(x => x.Comments).HasForeignKey(x => x.IdTranslation);
        }
    }
}
