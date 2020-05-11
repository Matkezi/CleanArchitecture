﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(SkipperAgencyDbContext))]
    [Migration("20200303151856_languages2")]
    partial class languages2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("OIB")
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
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AppUser");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AvailableFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AvailableTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("SkipperId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SkipperId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Boat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoathPhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CharterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MinimalRequiredLicence")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharterId");

                    b.ToTable("Boats");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BoatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BookedFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BookedTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("BookingURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CharterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CrewSize")
                        .HasColumnType("int");

                    b.Property<string>("GuestEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuestMessege")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuestName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuestNationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OnboardingLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkipperId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoatId");

                    b.HasIndex("CharterId");

                    b.HasIndex("SkipperId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.BookingHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("SkipperId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("SkipperId");

                    b.ToTable("BookingHistories");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryNameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryNameNative")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageNameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LanguageNameNative")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TwoLetterISOLanguageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Licence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<int>("LicenceType")
                        .HasColumnType("int");

                    b.Property<string>("LicenceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkipperId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SkipperId")
                        .IsUnique()
                        .HasFilter("[SkipperId] IS NOT NULL");

                    b.ToTable("Licences");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Region", b =>
                {
                    b.Property<int>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegionId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.RegionAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailabilityId")
                        .HasColumnType("int");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilityId");

                    b.HasIndex("RegionId");

                    b.ToTable("RegionAvailabilities");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Icon = "fas fa-utensils",
                            Name = "Cook"
                        },
                        new
                        {
                            Id = 1,
                            Icon = "fas fa-swimmer",
                            Name = "Diving"
                        },
                        new
                        {
                            Id = 2,
                            Icon = "fas fa-search-location",
                            Name = "Local expert"
                        },
                        new
                        {
                            Id = 4,
                            Icon = "fas fa-video",
                            Name = "Video"
                        },
                        new
                        {
                            Id = 3,
                            Icon = "fas fa-camera",
                            Name = "Photo"
                        });
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.SkipperLanguage", b =>
                {
                    b.Property<string>("SkipperId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.HasKey("SkipperId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("SkipperLanguage");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.SkipperSkill", b =>
                {
                    b.Property<string>("SkipperId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("SkipperId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("SkipperSkills");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.TrustedCharterSkipper", b =>
                {
                    b.Property<string>("SkipperID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CharterID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SkipperID", "CharterID");

                    b.HasIndex("CharterID");

                    b.ToTable("TrustedSkippers");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.UnTrustedCharterSkipper", b =>
                {
                    b.Property<string>("SkipperID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CharterID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SkipperID", "CharterID");

                    b.HasIndex("CharterID");

                    b.ToTable("UnTrustedSkippers");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Charter", b =>
                {
                    b.HasBaseType("SkipperBooking.DAL.Entities.AppUser");

                    b.Property<string>("CharterName")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Charter");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Skipper", b =>
                {
                    b.HasBaseType("SkipperBooking.DAL.Entities.AppUser");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalSummary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("UserPhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Skipper");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkipperBooking.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Availability", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.AppUser", "Skipper")
                        .WithMany()
                        .HasForeignKey("SkipperId");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Boat", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Charter", "Charter")
                        .WithMany()
                        .HasForeignKey("CharterId");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Booking", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Boat", "Boat")
                        .WithMany()
                        .HasForeignKey("BoatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkipperBooking.DAL.Entities.Charter", "Charter")
                        .WithMany("Bookings")
                        .HasForeignKey("CharterId");

                    b.HasOne("SkipperBooking.DAL.Entities.Skipper", "Skipper")
                        .WithMany("Bookings")
                        .HasForeignKey("SkipperId");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.BookingHistory", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Booking", "Booking")
                        .WithMany("BookingHistories")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkipperBooking.DAL.Entities.Skipper", "Skipper")
                        .WithMany()
                        .HasForeignKey("SkipperId");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.Licence", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Skipper", "Skipper")
                        .WithOne("Licence")
                        .HasForeignKey("SkipperBooking.DAL.Entities.Licence", "SkipperId");
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.RegionAvailability", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Availability", "Availability")
                        .WithMany()
                        .HasForeignKey("AvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkipperBooking.DAL.Entities.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.SkipperLanguage", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Language", "Language")
                        .WithMany("Skippers")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkipperBooking.DAL.Entities.Skipper", "Skipper")
                        .WithMany("ListOfLanguages")
                        .HasForeignKey("SkipperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.SkipperSkill", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Skill", "Skill")
                        .WithMany("Skippers")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkipperBooking.DAL.Entities.Skipper", "Skipper")
                        .WithMany("ListOfSkills")
                        .HasForeignKey("SkipperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.TrustedCharterSkipper", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Charter", "Charter")
                        .WithMany("TrustedSkippers")
                        .HasForeignKey("CharterID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkipperBooking.DAL.Entities.Skipper", "Skipper")
                        .WithMany("TrustedCharters")
                        .HasForeignKey("SkipperID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SkipperBooking.DAL.Entities.UnTrustedCharterSkipper", b =>
                {
                    b.HasOne("SkipperBooking.DAL.Entities.Charter", "Charter")
                        .WithMany("UnTrustedSkippers")
                        .HasForeignKey("CharterID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkipperBooking.DAL.Entities.Skipper", "Skipper")
                        .WithMany("UnTrustedCharters")
                        .HasForeignKey("SkipperID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
