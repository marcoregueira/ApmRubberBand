﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace H2h.RubberBand.Database.Postgres.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20200726000000_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Npgsql:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Relational:Sequence:.EntityFrameworkHiLoSequence", "'EntityFrameworkHiLoSequence', '', '1', '10', '', '', 'Int64', 'False'");

            modelBuilder.Entity("Windows.Metrics.Ingest.Ef.ErrorEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("App")
                        .HasColumnType("text");

                    b.Property<string>("Data")
                        .HasColumnType("jsonb");

                    b.Property<string>("ErrorId")
                        .HasColumnType("text");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp");

                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.HasKey("LineId");

                    b.HasIndex("Time");

                    b.ToTable("apm_errors");
                });

            modelBuilder.Entity("Windows.Metrics.Ingest.Ef.LogEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("App")
                        .HasColumnType("text");

                    b.Property<string>("Data")
                        .HasColumnType("jsonb");

                    b.Property<string>("Database")
                        .HasColumnType("text");

                    b.Property<decimal>("Duration")
                        .HasColumnType("numeric");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .HasColumnType("text");

                    b.Property<string>("LogId")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("RemoteHost")
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp");

                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("LineId");

                    b.HasIndex("Time");

                    b.ToTable("apm_log");
                });

            modelBuilder.Entity("Windows.Metrics.Ingest.Ef.MetricEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Data")
                        .HasColumnType("jsonb");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp");

                    b.HasKey("LineId");

                    b.HasIndex("Time");

                    b.ToTable("apm_metrics");
                });

            modelBuilder.Entity("Windows.Metrics.Ingest.Ef.TransactionEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("App")
                        .HasColumnType("text");

                    b.Property<string>("Data")
                        .HasColumnType("jsonb");

                    b.Property<string>("Database")
                        .HasColumnType("text");

                    b.Property<decimal>("Duration")
                        .HasColumnType("numeric");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ParentId")
                        .HasColumnType("text");

                    b.Property<string>("RemoteHost")
                        .HasColumnType("text");

                    b.Property<string>("Result")
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp");

                    b.Property<string>("TransactionId")
                        .HasColumnType("text");

                    b.Property<string>("TransactionType")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("LineId");

                    b.HasIndex("Time");

                    b.ToTable("apm_transaction");
                });
#pragma warning restore 612, 618
        }
    }
}
