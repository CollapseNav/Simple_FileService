﻿// <auto-generated />
using System;
using Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(FileDbContext))]
    [Migration("20210129141924_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10");

            modelBuilder.Entity("Api.Model.Dir", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AddTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<Guid?>("FileTypeId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MapPath")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Size")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Sys")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FileTypeId");

                    b.HasIndex("ParentId");

                    b.ToTable("Dirs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("353b4b8b-d59c-4d64-8e5c-9faedfc20cc0"),
                            AddTime = new DateTime(2021, 1, 29, 22, 19, 23, 953, DateTimeKind.Local).AddTicks(7666),
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
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<Guid?>("FileTypeId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MapPath")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Size")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Sys")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FileTypeId");

                    b.HasIndex("ParentId");

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
                    b.HasOne("Api.Model.FileType", "FileType")
                        .WithMany()
                        .HasForeignKey("FileTypeId");

                    b.HasOne("Api.Model.Dir", "Parent")
                        .WithMany("Dirs")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Api.Model.File", b =>
                {
                    b.HasOne("Api.Model.FileType", "FileType")
                        .WithMany()
                        .HasForeignKey("FileTypeId");

                    b.HasOne("Api.Model.Dir", "Parent")
                        .WithMany("Files")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}