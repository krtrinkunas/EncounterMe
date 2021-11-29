﻿// <auto-generated />
using Api.ModelContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("EncounterMeApp.Models.MyLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NAME")
                        .HasColumnType("TEXT");

                    b.Property<string>("owner")
                        .HasColumnType("TEXT");

                    b.Property<int>("points")
                        .HasColumnType("INTEGER");

                    b.Property<double>("positionX")
                        .HasColumnType("REAL");

                    b.Property<double>("positionY")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("EncounterMeApp.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .HasColumnType("TEXT");

                    b.Property<int>("LocationsOwned")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationsVisited")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NickName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
