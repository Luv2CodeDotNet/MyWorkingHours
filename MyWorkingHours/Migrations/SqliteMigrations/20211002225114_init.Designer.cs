// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MyWorkingHours.Data.DataAccess;

namespace MyWorkingHours.Migrations.SqliteMigrations
{
    [DbContext(typeof(SqliteDbContext))]
    [Migration("20211002225114_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("MyWorkingHours.Data.StatusTimeStamp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("ScreenLocked")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StatusTimeStamps");
                });
#pragma warning restore 612, 618
        }
    }
}
