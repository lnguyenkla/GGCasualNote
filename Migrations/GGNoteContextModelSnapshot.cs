﻿// <auto-generated />
using System;
using GGCasualNote.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GGCasualNote.Migrations
{
    [DbContext(typeof(GgNoteContext))]
    partial class GGNoteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.27");

            modelBuilder.Entity("GGCasualNote.Models.Character", b =>
                {
                    b.Property<string>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CharacterId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("GGCasualNote.Models.ComboNote", b =>
                {
                    b.Property<int>("ComboNoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CharacterId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ComboString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FootageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LongestStreak")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NoteContext")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ComboNoteId");

                    b.HasIndex("CharacterId");

                    b.ToTable("ComboNotes");
                });

            modelBuilder.Entity("GGCasualNote.Models.MatchupNote", b =>
                {
                    b.Property<int>("MatchupNoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CharacterId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContextNote")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("VsCharacterId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MatchupNoteId");

                    b.HasIndex("CharacterId");

                    b.ToTable("MatchupNotes");
                });

            modelBuilder.Entity("GGCasualNote.Models.Move", b =>
                {
                    b.Property<int>("MoveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CharacterId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("MoveId");

                    b.HasIndex("CharacterId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("GGCasualNote.Models.MoveListTimestamp", b =>
                {
                    b.Property<int>("MoveListTimestampId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CharacterId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.HasKey("MoveListTimestampId");

                    b.HasIndex("CharacterId");

                    b.ToTable("MoveListTimestamps");
                });

            modelBuilder.Entity("GGCasualNote.Models.ComboNote", b =>
                {
                    b.HasOne("GGCasualNote.Models.Character", null)
                        .WithMany("ComboNotes")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GGCasualNote.Models.MatchupNote", b =>
                {
                    b.HasOne("GGCasualNote.Models.Character", null)
                        .WithMany("MatchupNotes")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GGCasualNote.Models.Move", b =>
                {
                    b.HasOne("GGCasualNote.Models.Character", null)
                        .WithMany("Moves")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GGCasualNote.Models.MoveListTimestamp", b =>
                {
                    b.HasOne("GGCasualNote.Models.Character", null)
                        .WithMany("MoveListTimestamps")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GGCasualNote.Models.Character", b =>
                {
                    b.Navigation("ComboNotes");

                    b.Navigation("MatchupNotes");

                    b.Navigation("MoveListTimestamps");

                    b.Navigation("Moves");
                });
#pragma warning restore 612, 618
        }
    }
}
