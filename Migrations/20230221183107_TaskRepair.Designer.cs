﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoApi.Database;

#nullable disable

namespace ToDoApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230221183107_TaskRepair")]
    partial class TaskRepair
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ToDoApi.Entities.Domain.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Completed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("PublicId")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.Property<int?>("TaskListId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("TaskListId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ToDoApi.Entities.Domain.TaskList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("PublicId")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TaskLists");
                });

            modelBuilder.Entity("ToDoApi.Entities.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<Guid?>("PublicId")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ToDoApi.Entities.Domain.TaskItem", b =>
                {
                    b.HasOne("ToDoApi.Entities.Domain.TaskList", null)
                        .WithMany("TaskItems")
                        .HasForeignKey("TaskListId");
                });

            modelBuilder.Entity("ToDoApi.Entities.Domain.TaskList", b =>
                {
                    b.HasOne("ToDoApi.Entities.Domain.User", null)
                        .WithMany("TaskLists")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ToDoApi.Entities.Domain.TaskList", b =>
                {
                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("ToDoApi.Entities.Domain.User", b =>
                {
                    b.Navigation("TaskLists");
                });
#pragma warning restore 612, 618
        }
    }
}
