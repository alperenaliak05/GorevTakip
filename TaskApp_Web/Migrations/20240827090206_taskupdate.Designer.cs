﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskApp_Web.Data;

#nullable disable

namespace TaskAppWeb.Migrations
{
    [DbContext(typeof(TaskApp_WebContext))]
    [Migration("20240827090206_taskupdate")]
    partial class taskupdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskApp_Web.Models.Departments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TaskApp_Web.Models.TaskReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Report")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskReports");
                });

            modelBuilder.Entity("TaskApp_Web.Models.ToDoTasks", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("AssignedByUserId")
                        .HasColumnType("int");

                    b.Property<int?>("AssignedToDepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("AssignedToUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Process")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignedByUserId");

                    b.HasIndex("AssignedToUserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskApp_Web.Models.UserToDoList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Task")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserToDoLists");
                });

            modelBuilder.Entity("TaskApp_Web.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkingHours")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskApp_Web.Models.TaskReport", b =>
                {
                    b.HasOne("TaskApp_Web.Models.Users", "CreatedByUser")
                        .WithMany("TaskReports")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TaskApp_Web.Models.ToDoTasks", "Task")
                        .WithMany("TaskReports")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskApp_Web.Models.ToDoTasks", b =>
                {
                    b.HasOne("TaskApp_Web.Models.Users", "AssignedByUser")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("AssignedByUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TaskApp_Web.Models.Users", "AssignedToUser")
                        .WithMany("Tasks")
                        .HasForeignKey("AssignedToUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AssignedByUser");

                    b.Navigation("AssignedToUser");
                });

            modelBuilder.Entity("TaskApp_Web.Models.UserToDoList", b =>
                {
                    b.HasOne("TaskApp_Web.Models.Users", "User")
                        .WithMany("ToDoLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskApp_Web.Models.Users", b =>
                {
                    b.HasOne("TaskApp_Web.Models.Departments", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("TaskApp_Web.Models.Departments", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TaskApp_Web.Models.ToDoTasks", b =>
                {
                    b.Navigation("TaskReports");
                });

            modelBuilder.Entity("TaskApp_Web.Models.Users", b =>
                {
                    b.Navigation("AssignedTasks");

                    b.Navigation("TaskReports");

                    b.Navigation("Tasks");

                    b.Navigation("ToDoLists");
                });
#pragma warning restore 612, 618
        }
    }
}
