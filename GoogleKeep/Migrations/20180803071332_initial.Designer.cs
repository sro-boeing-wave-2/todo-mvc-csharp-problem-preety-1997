﻿// <auto-generated />
using System;
using GoogleKeep.Data;
using GoogleKeep.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GoogleKeep.Migrations
{
    [DbContext(typeof(NotesContext))]
    [Migration("20180803071332_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoogleKeep.Models.Checklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsChecked");

                    b.Property<int?>("NoteId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("Checklist");
                });

            modelBuilder.Entity("GoogleKeep.Models.Labels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NoteId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("GoogleKeep.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanbePinned");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("GoogleKeep.Models.Checklist", b =>
                {
                    b.HasOne("GoogleKeep.Models.Note")
                        .WithMany("ChecklistList")
                        .HasForeignKey("NoteId");
                });

            modelBuilder.Entity("GoogleKeep.Models.Labels", b =>
                {
                    b.HasOne("GoogleKeep.Models.Note")
                        .WithMany("LabelsList")
                        .HasForeignKey("NoteId");
                });
#pragma warning restore 612, 618
        }
    }
}
