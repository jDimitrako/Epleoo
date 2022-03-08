﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PR.Infrastructure;

#nullable disable

namespace PR.API.Infrastructure.Migrations
{
    [DbContext(typeof(PrDbContext))]
    [Migration("20220308195908_Initia3")]
    partial class Initia3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("friendrequestseq", "pr.service")
                .IncrementsBy(10);

            modelBuilder.HasSequence("friendshipseq", "pr.service")
                .IncrementsBy(10);

            modelBuilder.HasSequence("personseq", "pr.service")
                .IncrementsBy(10);

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendRequestAggregate.FriendRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "friendrequestseq", "pr.service");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("FriendshipId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Modifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverIdentityGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderIdentityGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("_friendRequestStatusId")
                        .HasColumnType("int")
                        .HasColumnName("FriendRequestStatusId");

                    b.HasKey("Id");

                    b.HasIndex("FriendshipId");

                    b.HasIndex("_friendRequestStatusId");

                    b.ToTable("FriendRequests", "pr.service");
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendRequestAggregate.FriendRequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("FriendRequestStatus", "pr.service");
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendRequestAggregate.Friendship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "friendshipseq", "pr.service");

                    b.Property<string>("ReceiverIdentityGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderIdentityGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("friendships", "pr.service");
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.PersonAggregate.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "personseq", "pr.service");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityGuid")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdentityGuid")
                        .IsUnique();

                    b.ToTable("persons", "pr.service");
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendRequestAggregate.FriendRequest", b =>
                {
                    b.HasOne("PR.Domain.AggregatesModel.FriendRequestAggregate.Friendship", null)
                        .WithMany("FriendRequests")
                        .HasForeignKey("FriendshipId");

                    b.HasOne("PR.Domain.AggregatesModel.FriendRequestAggregate.FriendRequestStatus", "FriendRequestStatus")
                        .WithMany()
                        .HasForeignKey("_friendRequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FriendRequestStatus");
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendRequestAggregate.Friendship", b =>
                {
                    b.Navigation("FriendRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
