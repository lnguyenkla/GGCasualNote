﻿// <auto-generated />
using GGCasualNote.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GGCasualNote.Migrations
{
    [DbContext(typeof(GgNoteContext))]
    [Migration("20240530132830_RelationshipTest")]
    partial class RelationshipTest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.27");

            modelBuilder.Entity("GGCasualNote.Models.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

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

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ComboString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FootageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NoteContext")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ComboNoteId");

                    b.HasIndex("CharacterId");

                    b.ToTable("ComboNotes");
                });

            modelBuilder.Entity("GGCasualNote.Models.ComboNote", b =>
                {
                    b.HasOne("GGCasualNote.Models.Character", null)
                        .WithMany("ComboNotes")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GGCasualNote.Models.Character", b =>
                {
                    b.Navigation("ComboNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
