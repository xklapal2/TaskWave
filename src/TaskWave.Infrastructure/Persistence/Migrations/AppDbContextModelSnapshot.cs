﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskWave.Infrastructure.Persistence;

#nullable disable

namespace TaskWave.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TaskWave.Domain.Entities.Groups.Group", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Groups_Name");

                    b.ToTable("Groups", (string)null);
                });

            modelBuilder.Entity("TaskWave.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("TaskWave.Domain.Entities.Groups.Group", b =>
                {
                    b.OwnsMany("TaskWave.Domain.Entities.Groups.GroupMember", "Members", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("GroupId")
                                .IsRequired()
                                .HasColumnType("varchar(255)");

                            b1.Property<DateTime>("JoinedDate")
                                .HasColumnType("datetime(6)");

                            b1.HasKey("UserId");

                            b1.HasIndex("GroupId");

                            b1.HasIndex("UserId")
                                .HasDatabaseName("IX_GroupMembers_UserId");

                            b1.ToTable("GroupMembers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("GroupId");
                        });

                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
