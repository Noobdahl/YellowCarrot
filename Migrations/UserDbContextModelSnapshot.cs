﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YellowCarrot.Data;

#nullable disable

namespace YellowCarrot.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YellowCarrot.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            IsAdmin = true,
                            Name = "admin",
                            Password = "pKj8ZhaWCE7Qed6qJOrYoA=="
                        },
                        new
                        {
                            UserId = 2,
                            IsAdmin = false,
                            Name = "user",
                            Password = "pKj8ZhaWCE7Qed6qJOrYoA=="
                        },
                        new
                        {
                            UserId = 3,
                            IsAdmin = false,
                            Name = "Micke",
                            Password = "YlLIEhPWqLis4l23Lcbx3w=="
                        },
                        new
                        {
                            UserId = 4,
                            IsAdmin = false,
                            Name = "Albin",
                            Password = "S0jZbC2T5ClvvM2LddCdZQ=="
                        },
                        new
                        {
                            UserId = 5,
                            IsAdmin = false,
                            Name = "Lars",
                            Password = "6If32EwDRymYsYbnLWj5Yw=="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
