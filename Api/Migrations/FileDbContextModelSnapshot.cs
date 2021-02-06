﻿// <auto-generated />
using System;
using Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(FileDbContext))]
    partial class FileDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Api.Model.Dir", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AddTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ext")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MapPath")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Size")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Sys")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("TypeId");

                    b.ToTable("Dirs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ca2ee4ba-b4db-4f06-99f6-98df27952663"),
                            AddTime = new DateTime(2021, 2, 6, 14, 25, 2, 145, DateTimeKind.Local).AddTicks(7123),
                            FileName = "root",
                            IsVisible = true,
                            MapPath = ""
                        });
                });

            modelBuilder.Entity("Api.Model.File", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AddTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ext")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MapPath")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Size")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Sys")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("TypeId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Api.Model.FileType", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AddTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ext")
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FileTypes");
                });

            modelBuilder.Entity("Api.Model.Dir", b =>
                {
                    b.HasOne("Api.Model.Dir", "Parent")
                        .WithMany("Dirs")
                        .HasForeignKey("ParentId");

                    b.HasOne("Api.Model.FileType", "FileType")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("FileType");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Api.Model.File", b =>
                {
                    b.HasOne("Api.Model.Dir", "Parent")
                        .WithMany("Files")
                        .HasForeignKey("ParentId");

                    b.HasOne("Api.Model.FileType", "FileType")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("FileType");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Api.Model.Dir", b =>
                {
                    b.Navigation("Dirs");

                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
