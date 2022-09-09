﻿// <auto-generated />
using AuthMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthMVC.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220909183603_AddFriends2")]
    partial class AddFriends2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("AuthMVC.Models.FriendShip", b =>
                {
                    b.Property<int>("PrimaryProfileId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FriendProfileId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PrimaryProfileId", "FriendProfileId");

                    b.HasIndex("FriendProfileId");

                    b.ToTable("FriendShips");
                });

            modelBuilder.Entity("AuthMVC.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("AuthMVC.Models.FriendShip", b =>
                {
                    b.HasOne("AuthMVC.Models.Profile", "FriendProfile")
                        .WithMany("FriendsOf")
                        .HasForeignKey("FriendProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthMVC.Models.Profile", "PrimaryProfile")
                        .WithMany("Friends")
                        .HasForeignKey("PrimaryProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FriendProfile");

                    b.Navigation("PrimaryProfile");
                });

            modelBuilder.Entity("AuthMVC.Models.Profile", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("FriendsOf");
                });
#pragma warning restore 612, 618
        }
    }
}