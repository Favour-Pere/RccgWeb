﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RccgWeb.Data;

#nullable disable

namespace RccgWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250210154031_removedSeed")]
    partial class removedSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
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

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
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

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RccgWeb.Models.Area", b =>
                {
                    b.Property<Guid>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AreaPastor")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ChurchId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("ZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AreaId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Areas");

                    b.HasData(
                        new
                        {
                            AreaId = new Guid("81b5f0a1-4e78-49f8-8c4d-76466ed3f102"),
                            AreaName = "Area X",
                            AreaPastor = "Pastor Smith",
                            ChurchId = "TBD",
                            DateCreated = new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Location X",
                            ZoneId = new Guid("d3f23a29-2ad4-4b3d-9df1-cf8c3bb23df1")
                        });
                });

            modelBuilder.Entity("RccgWeb.Models.Parish", b =>
                {
                    b.Property<Guid>("ParishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AreaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChurchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParishName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParishPastor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParishId");

                    b.HasIndex("AreaId");

                    b.ToTable("Parishes");
                });

            modelBuilder.Entity("RccgWeb.Models.ProgramActivity", b =>
                {
                    b.Property<Guid>("ProgramActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActiveWorkers")
                        .HasColumnType("int");

                    b.Property<string>("ActivityDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("AreaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attendance")
                        .HasColumnType("int");

                    b.Property<string>("ChurchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DateCreated")
                        .HasColumnType("date");

                    b.Property<DateTime>("DateTimeSubmitted")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Offering")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("ParishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PastorInCharge")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Tithe")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("ZoneId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProgramActivityId");

                    b.HasIndex("AreaId");

                    b.HasIndex("ParishId");

                    b.HasIndex("ZoneId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("RccgWeb.Models.Zone", b =>
                {
                    b.Property<Guid>("ZoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChurchId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZoneName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZonePastor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ZoneId");

                    b.ToTable("Zones");

                    b.HasData(
                        new
                        {
                            ZoneId = new Guid("d3f23a29-2ad4-4b3d-9df1-cf8c3bb23df1"),
                            ChurchId = "TBD",
                            DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Location A",
                            ZoneName = "Zone A",
                            ZonePastor = "Pastor John"
                        },
                        new
                        {
                            ZoneId = new Guid("36a0d3a7-2b5f-4bdc-a8d6-4fe7f3a0139f"),
                            ChurchId = "TBD",
                            DateCreated = new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Location = "Location B",
                            ZoneName = "Zone B",
                            ZonePastor = "Pastor Jane"
                        });
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RccgWeb.Models.Area", b =>
                {
                    b.HasOne("RccgWeb.Models.Zone", "Zone")
                        .WithMany("Areas")
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("RccgWeb.Models.Parish", b =>
                {
                    b.HasOne("RccgWeb.Models.Area", "Area")
                        .WithMany("Parishes")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("RccgWeb.Models.ProgramActivity", b =>
                {
                    b.HasOne("RccgWeb.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RccgWeb.Models.Parish", "Parish")
                        .WithMany("ProgramActivities")
                        .HasForeignKey("ParishId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RccgWeb.Models.Zone", "Zone")
                        .WithMany()
                        .HasForeignKey("ZoneId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Area");

                    b.Navigation("Parish");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("RccgWeb.Models.Area", b =>
                {
                    b.Navigation("Parishes");
                });

            modelBuilder.Entity("RccgWeb.Models.Parish", b =>
                {
                    b.Navigation("ProgramActivities");
                });

            modelBuilder.Entity("RccgWeb.Models.Zone", b =>
                {
                    b.Navigation("Areas");
                });
#pragma warning restore 612, 618
        }
    }
}
