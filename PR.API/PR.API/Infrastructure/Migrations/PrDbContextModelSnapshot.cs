﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PR.Infrastructure;

#nullable disable

namespace PR.API.Infrastructure.Migrations
{
    [DbContext(typeof(PrDbContext))]
    partial class PrDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("friendrequestseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("personseq")
                .IncrementsBy(10);

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendRequestAggregate.FriendRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "friendrequestseq");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Modifier")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverPersonId")
                        .HasColumnType("int");

                    b.Property<int>("SenderPersonId")
                        .HasColumnType("int");

                    b.Property<int>("_friendRequestStatusId")
                        .HasColumnType("int")
                        .HasColumnName("FriendRequestStatusId");

                    b.HasKey("Id");

                    b.HasIndex("_friendRequestStatusId");

                    b.ToTable("FriendRequests", (string)null);
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

                    b.ToTable("friendRequestStatus", (string)null);
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendshipAggregate.Friendship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("friendships", (string)null);
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.PersonAggregate.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "personseq");

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

                    b.ToTable("persons", (string)null);
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendRequestAggregate.FriendRequest", b =>
                {
                    b.HasOne("PR.Domain.AggregatesModel.FriendRequestAggregate.FriendRequestStatus", "FriendRequestStatus")
                        .WithMany()
                        .HasForeignKey("_friendRequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FriendRequestStatus");
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.FriendshipAggregate.Friendship", b =>
                {
                    b.HasOne("PR.Domain.AggregatesModel.PersonAggregate.Person", "Receiver")
                        .WithMany("FriendshipsReceived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PR.Domain.AggregatesModel.PersonAggregate.Person", "Sender")
                        .WithMany("FriendshipsSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("PR.Domain.AggregatesModel.PersonAggregate.Person", b =>
                {
                    b.Navigation("FriendshipsReceived");

                    b.Navigation("FriendshipsSent");
                });
#pragma warning restore 612, 618
        }
    }
}
