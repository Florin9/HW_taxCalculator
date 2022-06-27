﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaxCalculator.Data;

#nullable disable

namespace TaxCalculator.Data.Migrations
{
    [DbContext(typeof(TaxCalculatorDbContext))]
    partial class TaxCalculatorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaxCalculator.Data.Entities.TaxBand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("LowerLimit")
                        .HasColumnType("bigint");

                    b.Property<int>("TaxRate")
                        .HasColumnType("integer");

                    b.Property<long>("UpperLimit")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("TaxBands");
                });
#pragma warning restore 612, 618
        }
    }
}
