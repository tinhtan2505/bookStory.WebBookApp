﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bookStory.Data.EF;

namespace bookStory.Data.Migrations
{
    [DbContext(typeof(bookStoryDbContext))]
    partial class bookStoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            RoleId = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("bookStory.Data.Entities.AppConfig", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("AppConfigs");

                    b.HasData(
                        new
                        {
                            Key = "HomeTitle",
                            Value = "This NQT"
                        },
                        new
                        {
                            Key = "HomeKeyWord",
                            Value = "This HomeKeyWord"
                        },
                        new
                        {
                            Key = "HomeDescription",
                            Value = "This HomeDescription"
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                            ConcurrencyStamp = "bae22d19-0445-404f-86df-29f5669a8f32",
                            Description = "Administrator role",
                            Name = "admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fa82637e-e793-4186-a1c2-ffbbe6b294eb",
                            Dob = new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "tedu.international@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Toan",
                            LastName = "Bach",
                            LockoutEnabled = false,
                            NormalizedEmail = "tedu.international@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEGbo5njssptE8Gl34PK6MpysVujBbd+eC2q7dz+WueoZ6sMOFfhgIWqLHF3M0HVZBA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Author1",
                            FileName = "FileName1",
                            Title = "Title1"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Author2",
                            FileName = "FileName2",
                            Title = "Title2"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Author3",
                            FileName = "FileName3",
                            Title = "Title3"
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.BookImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<int>("IdBook")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdBook");

                    b.ToTable("BookImages");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateComment")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTranslation")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdTranslation");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateComment = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdTranslation = 2,
                            Message = "Message1",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 2,
                            DateComment = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdTranslation = 1,
                            Message = "Message3",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 3,
                            DateComment = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdTranslation = 3,
                            Message = "Message5",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.Language", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = "vi",
                            IsDefault = true,
                            Name = "Tiếng Việt"
                        },
                        new
                        {
                            Id = "en",
                            IsDefault = false,
                            Name = "English"
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.Paragraph", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdBook")
                        .HasColumnType("int");

                    b.Property<string>("Order")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdBook");

                    b.ToTable("Paragraphs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IdBook = 2,
                            Order = "Order1",
                            Type = "Type1"
                        },
                        new
                        {
                            Id = 2,
                            IdBook = 3,
                            Order = "Order2",
                            Type = "Type2"
                        },
                        new
                        {
                            Id = 3,
                            IdBook = 1,
                            Order = "Order3",
                            Type = "Type3"
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateProject")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("IdBook")
                        .HasColumnType("int");

                    b.Property<string>("IdLanguage")
                        .HasColumnType("varchar(5)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdBook");

                    b.HasIndex("IdLanguage");

                    b.HasIndex("UserId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateProject = new DateTime(2021, 6, 3, 23, 23, 18, 997, DateTimeKind.Local).AddTicks(6931),
                            Description = "Description1",
                            IdBook = 1,
                            IdLanguage = "vi",
                            Status = 1,
                            Title = "Title1",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 2,
                            DateProject = new DateTime(2021, 6, 3, 23, 23, 18, 999, DateTimeKind.Local).AddTicks(80),
                            Description = "Description2",
                            IdBook = 1,
                            IdLanguage = "en",
                            Status = 0,
                            Title = "Title2",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 3,
                            DateProject = new DateTime(2021, 6, 3, 23, 23, 18, 999, DateTimeKind.Local).AddTicks(103),
                            Description = "Description3",
                            IdBook = 1,
                            IdLanguage = "vi",
                            Status = 1,
                            Title = "Title3",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdTranslation")
                        .HasColumnType("int");

                    b.Property<int?>("TranslationId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Vote")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TranslationId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IdTranslation = 2,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Vote = 1
                        },
                        new
                        {
                            Id = 2,
                            IdTranslation = 3,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Vote = 3
                        },
                        new
                        {
                            Id = 3,
                            IdTranslation = 1,
                            UserId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Vote = 5
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdParagraph")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdParagraph");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IdParagraph = 2,
                            Reason = "Reason1",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 2,
                            IdParagraph = 3,
                            Reason = "Reason3",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 3,
                            IdParagraph = 1,
                            Reason = "Reason5",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.Translation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdParagraph")
                        .HasColumnType("int");

                    b.Property<int>("IdProject")
                        .HasColumnType("int");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdParagraph");

                    b.HasIndex("IdProject");

                    b.HasIndex("UserId");

                    b.ToTable("Translations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2021, 6, 3, 23, 23, 18, 999, DateTimeKind.Local).AddTicks(3178),
                            IdParagraph = 3,
                            IdProject = 2,
                            Rating = "ok",
                            Text = "Text2",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2021, 6, 3, 23, 23, 18, 999, DateTimeKind.Local).AddTicks(3615),
                            IdParagraph = 3,
                            IdProject = 2,
                            Rating = "yes",
                            Text = "Text3",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2021, 6, 3, 23, 23, 18, 999, DateTimeKind.Local).AddTicks(3622),
                            IdParagraph = 3,
                            IdProject = 2,
                            Rating = "no",
                            Text = "Text4",
                            UserId = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("bookStory.Data.Entities.BookImage", b =>
                {
                    b.HasOne("bookStory.Data.Entities.Book", "Book")
                        .WithMany("BookImages")
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Comment", b =>
                {
                    b.HasOne("bookStory.Data.Entities.Translation", "Translation")
                        .WithMany("Comments")
                        .HasForeignKey("IdTranslation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bookStory.Data.Entities.AppUser", "AppUser")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Translation");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Paragraph", b =>
                {
                    b.HasOne("bookStory.Data.Entities.Book", "Book")
                        .WithMany("Paragraphs")
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Project", b =>
                {
                    b.HasOne("bookStory.Data.Entities.Book", "Book")
                        .WithMany("Projects")
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bookStory.Data.Entities.Language", "Language")
                        .WithMany("Projects")
                        .HasForeignKey("IdLanguage");

                    b.HasOne("bookStory.Data.Entities.AppUser", "AppUser")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Book");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Rating", b =>
                {
                    b.HasOne("bookStory.Data.Entities.Translation", "Translation")
                        .WithMany()
                        .HasForeignKey("TranslationId");

                    b.HasOne("bookStory.Data.Entities.AppUser", "AppUser")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Translation");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Report", b =>
                {
                    b.HasOne("bookStory.Data.Entities.Paragraph", "Paragraph")
                        .WithMany("Reports")
                        .HasForeignKey("IdParagraph")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bookStory.Data.Entities.AppUser", "AppUser")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Paragraph");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Translation", b =>
                {
                    b.HasOne("bookStory.Data.Entities.Paragraph", "Paragraph")
                        .WithMany("Translations")
                        .HasForeignKey("IdParagraph")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bookStory.Data.Entities.Project", "Project")
                        .WithMany("Translations")
                        .HasForeignKey("IdProject")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bookStory.Data.Entities.AppUser", "AppUser")
                        .WithMany("Translations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Paragraph");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("bookStory.Data.Entities.AppUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Projects");

                    b.Navigation("Ratings");

                    b.Navigation("Reports");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Book", b =>
                {
                    b.Navigation("BookImages");

                    b.Navigation("Paragraphs");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Language", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Paragraph", b =>
                {
                    b.Navigation("Reports");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Project", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("bookStory.Data.Entities.Translation", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
