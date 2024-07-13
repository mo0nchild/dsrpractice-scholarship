﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Scholarship.Database.Loans.Context;

#nullable disable

namespace Scholarship.Database.Loans.Migrations
{
    [DbContext(typeof(LoansDbContext))]
    [Migration("20240711195709_Initialization")]
    partial class Initialization
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Scholarship.Database.Loans.Entities.LoanInfo", b =>
                {
                    b.Property<Guid>("Uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("BeforeTime")
                        .HasColumnType("date");

                    b.Property<Guid>("ClientUuid")
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("CloseTime")
                        .HasColumnType("date");

                    b.Property<string>("CreditorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CreditorPatronymic")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CreditorSurname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double>("MoneyAmount")
                        .HasColumnType("double precision");

                    b.Property<DateOnly>("OpenTime")
                        .HasColumnType("date");

                    b.HasKey("Uuid");

                    b.HasIndex("Uuid")
                        .IsUnique();

                    b.ToTable("LoanInfo", "public");
                });
#pragma warning restore 612, 618
        }
    }
}