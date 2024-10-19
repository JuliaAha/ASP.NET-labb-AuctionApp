﻿// <auto-generated />
using System;
using AuctionApplication.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuctionApplication.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    [Migration("20241019165741_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AuctionApplication.Persistence.AuctionDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AuctionOwner")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("StartingPrice")
                        .HasColumnType("double");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("AuctionDbs");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            AuctionOwner = "julg@kth.se",
                            Description = "En gul kofta",
                            EndDate = new DateTime(2024, 10, 22, 18, 57, 39, 651, DateTimeKind.Local).AddTicks(2820),
                            StartingPrice = 200.0,
                            Title = "Kofta"
                        });
                });

            modelBuilder.Entity("AuctionApplication.Persistence.BidDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<int>("AuctionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BidDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.ToTable("BidDbs");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Amount = 250.0,
                            AuctionId = -1,
                            BidDate = new DateTime(2024, 10, 19, 18, 57, 39, 651, DateTimeKind.Local).AddTicks(3210),
                            UserName = "emma@kth.se"
                        },
                        new
                        {
                            Id = -2,
                            Amount = 300.0,
                            AuctionId = -1,
                            BidDate = new DateTime(2024, 10, 19, 18, 57, 39, 651, DateTimeKind.Local).AddTicks(3220),
                            UserName = "emma@kth.se"
                        });
                });

            modelBuilder.Entity("AuctionApplication.Persistence.BidDb", b =>
                {
                    b.HasOne("AuctionApplication.Persistence.AuctionDb", "AuctionDb")
                        .WithMany("BidDbs")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuctionDb");
                });

            modelBuilder.Entity("AuctionApplication.Persistence.AuctionDb", b =>
                {
                    b.Navigation("BidDbs");
                });
#pragma warning restore 612, 618
        }
    }
}
