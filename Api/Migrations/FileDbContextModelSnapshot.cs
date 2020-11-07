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
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("Api.Model.File", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AddTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ext")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("MapPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sys")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Files");
                });
#pragma warning restore 612, 618
        }
    }
}