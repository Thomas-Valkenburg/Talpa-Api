﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Talpa_Api.Contexts;

#nullable disable

namespace Talpa_Api.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240614134519_Add Dates to Vote")]
    partial class AddDatestoVote
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PollSuggestion", b =>
                {
                    b.Property<int>("PollsId")
                        .HasColumnType("int");

                    b.Property<int>("SuggestionsId")
                        .HasColumnType("int");

                    b.HasKey("PollsId", "SuggestionsId");

                    b.HasIndex("SuggestionsId");

                    b.ToTable("PollSuggestion");
                });

            modelBuilder.Entity("SuggestionTag", b =>
                {
                    b.Property<int>("SuggestionsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("SuggestionsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("SuggestionTag");
                });

            modelBuilder.Entity("Talpa_Api.Models.Poll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("HasPointsAssigned")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TeamId")
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("TeamId")
                        .IsUnique();

                    b.ToTable("Poll");
                });

            modelBuilder.Entity("Talpa_Api.Models.PollDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PollId")
                        .HasColumnType("int");

                    b.Property<int?>("VoteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.HasIndex("VoteId");

                    b.ToTable("PollDates");
                });

            modelBuilder.Entity("Talpa_Api.Models.Suggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatorId")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Suggestion");
                });

            modelBuilder.Entity("Talpa_Api.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Restrictive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Talpa_Api.Models.Team", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Talpa_Api.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Talpa_Api.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<int>("PollId")
                        .HasColumnType("int");

                    b.Property<int>("SuggestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PollId");

                    b.HasIndex("SuggestionId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("PollSuggestion", b =>
                {
                    b.HasOne("Talpa_Api.Models.Poll", null)
                        .WithMany()
                        .HasForeignKey("PollsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_Api.Models.Suggestion", null)
                        .WithMany()
                        .HasForeignKey("SuggestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuggestionTag", b =>
                {
                    b.HasOne("Talpa_Api.Models.Suggestion", null)
                        .WithMany()
                        .HasForeignKey("SuggestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_Api.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Talpa_Api.Models.Poll", b =>
                {
                    b.HasOne("Talpa_Api.Models.Team", "Team")
                        .WithOne("Poll")
                        .HasForeignKey("Talpa_Api.Models.Poll", "TeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Talpa_Api.Models.PollDate", b =>
                {
                    b.HasOne("Talpa_Api.Models.Poll", "Poll")
                        .WithMany("Dates")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_Api.Models.Vote", null)
                        .WithMany("Dates")
                        .HasForeignKey("VoteId");

                    b.Navigation("Poll");
                });

            modelBuilder.Entity("Talpa_Api.Models.Suggestion", b =>
                {
                    b.HasOne("Talpa_Api.Models.User", "Creator")
                        .WithMany("Suggestions")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Talpa_Api.Models.User", b =>
                {
                    b.HasOne("Talpa_Api.Models.Team", "Team")
                        .WithMany("Users")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Talpa_Api.Models.Vote", b =>
                {
                    b.HasOne("Talpa_Api.Models.User", "Creator")
                        .WithMany("Votes")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_Api.Models.Poll", "Poll")
                        .WithMany("Votes")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Talpa_Api.Models.Suggestion", "Suggestion")
                        .WithMany("Votes")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Poll");

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("Talpa_Api.Models.Poll", b =>
                {
                    b.Navigation("Dates");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Talpa_Api.Models.Suggestion", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Talpa_Api.Models.Team", b =>
                {
                    b.Navigation("Poll");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Talpa_Api.Models.User", b =>
                {
                    b.Navigation("Suggestions");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Talpa_Api.Models.Vote", b =>
                {
                    b.Navigation("Dates");
                });
#pragma warning restore 612, 618
        }
    }
}
