﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YardControlSystem.Data;

namespace YardControlSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220522124602_UpdateOrderModel")]
    partial class UpdateOrderModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.16");

            modelBuilder.Entity("YardControlSystem.Models.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("YardControlSystem.Models.Order", b =>
                {
                    b.Property<int>("OrderNr")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("TEXT");

                    b.Property<int>("DriverId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OperationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RampId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TrailerLicensePlate")
                        .HasColumnType("TEXT");

                    b.HasKey("OrderNr");

                    b.HasIndex("DriverId");

                    b.HasIndex("OperationId");

                    b.HasIndex("RampId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("YardControlSystem.Models.Ramp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Working")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Ramps");
                });

            modelBuilder.Entity("YardControlSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("YardControlSystem.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("YardControlSystem.Models.Order", b =>
                {
                    b.HasOne("YardControlSystem.Models.User", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YardControlSystem.Models.Operation", "Operation")
                        .WithMany()
                        .HasForeignKey("OperationId");

                    b.HasOne("YardControlSystem.Models.Ramp", "Ramp")
                        .WithMany()
                        .HasForeignKey("RampId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Operation");

                    b.Navigation("Ramp");
                });

            modelBuilder.Entity("YardControlSystem.Models.Ramp", b =>
                {
                    b.HasOne("YardControlSystem.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });
#pragma warning restore 612, 618
        }
    }
}
