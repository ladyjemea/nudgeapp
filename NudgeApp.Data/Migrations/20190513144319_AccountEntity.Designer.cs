﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NudgeApp.Data;

namespace NudgeApp.Data.Migrations
{
    [DbContext(typeof(NudgeDbContext))]
    [Migration("20190513144319_AccountEntity")]
    partial class AccountEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NudgeApp.Data.Entities.AccountEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Google");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.ActualPreferencesEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("MaxTemperature");

                    b.Property<int>("MinTemperature");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<bool>("RainyTrip");

                    b.Property<bool>("SnowyTrip");

                    b.Property<int>("TransportationType");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ActualPreferences");
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.NotificationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<Guid>("NudgeId");

                    b.Property<int>("Status");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("NudgeId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.NudgeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CloudCoveragePercent");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("Distance");

                    b.Property<int>("Duration");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("NudgeResult");

                    b.Property<int>("PrecipitationProbability");

                    b.Property<int>("Probability");

                    b.Property<float>("ReafFeelTemperature");

                    b.Property<int>("RoadCondition");

                    b.Property<int>("SkyCoverage");

                    b.Property<float>("Temperature");

                    b.Property<DateTime>("Time");

                    b.Property<int>("TransportationType");

                    b.Property<int>("Type");

                    b.Property<Guid>("UserId");

                    b.Property<float>("Wind");

                    b.Property<int>("WindCondition");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Nudges");
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.OracleNudgeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActualTransportationType");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PrecipitationProbability");

                    b.Property<int>("Result");

                    b.Property<int>("RoadCondition");

                    b.Property<int>("SkyCoverage");

                    b.Property<float>("Temperature");

                    b.Property<Guid>("UserId");

                    b.Property<int>("UserPreferedTransportationType");

                    b.Property<float>("Wind");

                    b.HasKey("Id");

                    b.ToTable("OracleNudgeEntity");
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.PreferencesEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("MaxTemperature");

                    b.Property<int>("MinTemperature");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<bool>("RainyTrip");

                    b.Property<bool>("SnowyTrip");

                    b.Property<int>("TransportationType");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.PushNotificationSubscriptionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Auth");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Endpoint");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("P256DH");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("PushNotificationSubscriptions");
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountId");

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.ActualPreferencesEntity", b =>
                {
                    b.HasOne("NudgeApp.Data.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.NotificationEntity", b =>
                {
                    b.HasOne("NudgeApp.Data.Entities.NudgeEntity", "Nudge")
                        .WithMany()
                        .HasForeignKey("NudgeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.NudgeEntity", b =>
                {
                    b.HasOne("NudgeApp.Data.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.PreferencesEntity", b =>
                {
                    b.HasOne("NudgeApp.Data.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NudgeApp.Data.Entities.UserEntity", b =>
                {
                    b.HasOne("NudgeApp.Data.Entities.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
