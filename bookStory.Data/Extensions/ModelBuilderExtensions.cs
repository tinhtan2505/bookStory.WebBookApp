using bookStory.Data.Entities;
using bookStory.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookStory.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                    new AppConfig() { Key = "HomeTitle", Value = "This NQT" },
                    new AppConfig() { Key = "HomeKeyWord", Value = "This HomeKeyWord" },
                    new AppConfig() { Key = "HomeDescription", Value = "This HomeDescription" }
                    );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false });
            modelBuilder.Entity<Book>().HasData(
               new Book() { Id = 1, FileName = "FileName1", Title = "Title1", Author = "Author1" },
               new Book() { Id = 2, FileName = "FileName2", Title = "Title2", Author = "Author2" },
               new Book() { Id = 3, FileName = "FileName3", Title = "Title3", Author = "Author3" }
               );
            modelBuilder.Entity<Paragraph>().HasData(
               new Paragraph() { Id = 1, IdBook = 2, Order = "Order1", Type = "Type1" },
               new Paragraph() { Id = 2, IdBook = 3, Order = "Order2", Type = "Type2" },
               new Paragraph() { Id = 3, IdBook = 1, Order = "Order3", Type = "Type3" }
               );
            modelBuilder.Entity<Project>().HasData(
               new Project()
               {
                   Id = 1,
                   IdLanguage = "vi",
                   IdBook = 1,
                   Title = "Title1",
                   Description = "Description1",
                   //IdUserRequested = 2,
                   Status = 1,
                   DateProject = DateTime.Now
               },
               new Project()
               {
                   Id = 2,
                   IdLanguage = "en",
                   IdBook = 1,
                   Title = "Title2",
                   Description = "Description2",
                   //IdUserRequested = 2,
                   Status = 0,
                   DateProject = DateTime.Now
               },
               new Project()
               {
                   Id = 3,
                   IdLanguage = "vi",
                   IdBook = 1,
                   Title = "Title3",
                   Description = "Description3",
                   //IdUserRequested = 2,
                   Status = 1,
                   DateProject = DateTime.Now
               }
               );
            modelBuilder.Entity<Translation>().HasData(
               new Translation()
               {
                   Id = 1,
                   IdParagraph = 3,
                   Text = "Text2",
                   Rating = "ok",
                   Date = DateTime.Now
               },
               new Translation()
               {
                   Id = 2,
                   IdParagraph = 3,
                   Text = "Text3",
                   Rating = "yes",
                   Date = DateTime.Now
               },
               new Translation()
               {
                   Id = 3,
                   IdParagraph = 3,
                   Text = "Text4",
                   Rating = "no",
                   Date = DateTime.Now
               }
               );
            modelBuilder.Entity<Rating>().HasData(
               new Rating()
               {
                   Id = 1,
                   //IdUser = 2,
                   IdTranslation = 2,
                   Vote = 1
               },
               new Rating()
               {
                   Id = 2,
                   //IdUser = 2,
                   IdTranslation = 3,
                   Vote = 3
               },
               new Rating()
               {
                   Id = 3,
                   //IdUser = 2,
                   IdTranslation = 1,
                   Vote = 5
               }
               );
            modelBuilder.Entity<Report>().HasData(
               new Report()
               {
                   Id = 1,
                   //IdUser = 2,
                   IdParagraph = 2,
                   Reason = "Reason1"
               },
               new Report()
               {
                   Id = 2,
                   //IdUser = 2,
                   IdParagraph = 3,
                   Reason = "Reason3"
               },
               new Report()
               {
                   Id = 3,
                   //IdUser = 2,
                   IdParagraph = 1,
                   Reason = "Reason5"
               }
               );
            modelBuilder.Entity<Comment>().HasData(
               new Comment()
               {
                   Id = 1,
                   IdTranslation = 2,
                   Message = "Message1"
               },
               new Comment()
               {
                   Id = 2,
                   IdTranslation = 1,
                   Message = "Message3"
               },
               new Comment()
               {
                   Id = 3,
                   IdTranslation = 3,
                   Message = "Message5"
               }
               );
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tedu.international@gmail.com",
                NormalizedEmail = "tedu.international@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Toan",
                LastName = "Bach",
                Dob = new DateTime(2020, 01, 31)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}