﻿// <auto-generated />
using System;
using Api.ModelContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220321165234_newDB3")]
    partial class newDB3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("EncounterMeApp.Models.CaptureAttempt", b =>
                {
                    b.Property<int>("CaptureAttemptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasCaptured")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CaptureAttemptId");

                    b.ToTable("CaptureAttempts");
                });

            modelBuilder.Entity("EncounterMeApp.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommentText")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasCaptured")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasSpoilers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimePosted")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CommentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EncounterMeApp.Models.CommentRating", b =>
                {
                    b.Property<int>("CommentRatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CommentRatingId");

                    b.ToTable("CommentRatings");
                });

            modelBuilder.Entity("EncounterMeApp.Models.LocationRating", b =>
                {
                    b.Property<int>("LocationRatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LocationRatingId");

                    b.ToTable("LocationRatings");
                });

            modelBuilder.Entity("EncounterMeApp.Models.MyLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NAME")
                        .HasColumnType("TEXT");

                    b.Property<string>("answer")
                        .HasColumnType("TEXT");

                    b.Property<string>("owner")
                        .HasColumnType("TEXT");

                    b.Property<int>("points")
                        .HasColumnType("INTEGER");

                    b.Property<double>("positionX")
                        .HasColumnType("REAL");

                    b.Property<double>("positionY")
                        .HasColumnType("REAL");

                    b.Property<string>("question")
                        .HasColumnType("TEXT");

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
