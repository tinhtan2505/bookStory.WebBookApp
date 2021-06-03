using bookStory.Data.Configurations;
using bookStory.Data.Entities;
using bookStory.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.Data.EF
{
    public class bookStoryDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public bookStoryDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookImageConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new ParagraphConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new TranslationConfiguration());
            //modelBuilder.ApplyConfiguration(new SlideConfiguration());

            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        //public DbSet<Slide> Slides { get; set; }
    }
}