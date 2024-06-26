﻿// <auto-generated />
using System;
using Borkfolio.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Borkfolio.Persistence.Migrations
{
    [DbContext(typeof(BorkfolioDbContext))]
    partial class BorkfolioDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Borkfolio.Domain.Entities.BoardGame", b =>
                {
                    b.Property<Guid>("BoardGameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BoardGameGeekId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("BoardGameId");

                    b.HasIndex("BoardGameGeekId")
                        .IsUnique();

                    b.ToTable("BoardGames");
                });

            modelBuilder.Entity("Borkfolio.Domain.Entities.Suggestion", b =>
                {
                    b.Property<Guid>("SuggestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BoardGameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SuggestionId");

                    b.HasIndex("BoardGameId");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("Borkfolio.Domain.Entities.Suggestion", b =>
                {
                    b.HasOne("Borkfolio.Domain.Entities.BoardGame", "BoardGame")
                        .WithMany()
                        .HasForeignKey("BoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoardGame");
                });
#pragma warning restore 612, 618
        }
    }
}
