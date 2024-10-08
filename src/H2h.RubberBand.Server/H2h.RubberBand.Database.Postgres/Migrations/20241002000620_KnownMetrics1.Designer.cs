﻿// <auto-generated />
using System;
using H2h.RubberBand.Database.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H2h.RubberBand.Database.Postgres.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20241002000620_KnownMetrics1")]
    partial class KnownMetrics1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseHiLo(modelBuilder, "EntityFrameworkHiLoSequence");

            modelBuilder.HasSequence("EntityFrameworkHiLoSequence")
                .IncrementsBy(10);

            modelBuilder.Entity("H2h.RubberBand.Database.Entities.ClientConfigEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("LineId"));

                    b.Property<int>("MaxAgeSeconds")
                        .HasColumnType("integer");

                    b.Property<string>("OptionValues")
                        .HasColumnType("jsonb");

                    b.Property<string>("ServiceEnvironment")
                        .HasColumnType("text");

                    b.Property<string>("ServiceName")
                        .HasColumnType("text");

                    b.HasKey("LineId");

                    b.HasIndex("ServiceName", "ServiceEnvironment")
                        .IsUnique();

                    b.ToTable("apm_client_configuration", "public");
                });

            modelBuilder.Entity("H2h.RubberBand.Database.Entities.ErrorEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("LineId"));

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

                    b.ToTable("apm_errors", "public");
                });

            modelBuilder.Entity("H2h.RubberBand.Database.Entities.LogEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("LineId"));

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

                    b.ToTable("apm_log", "public");
                });

            modelBuilder.Entity("H2h.RubberBand.Database.Entities.MetricEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("LineId"));

                    b.Property<string>("Data")
                        .HasColumnType("jsonb");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.Property<decimal?>("System_cpu_total_norm_pct")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("System_memory_actual_free")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("System_memory_total")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("System_process_cgroup_memory_mem_usage_bytes")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("System_process_cgroup_memory_stats_inactive_file_bytes")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("System_process_cpu_total_norm_pct")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("System_process_memory_rss_bytes")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("System_process_memory_size")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp");

                    b.HasKey("LineId");

                    b.HasIndex("Time");

                    b.ToTable("apm_metrics", "public");
                });

            modelBuilder.Entity("H2h.RubberBand.Database.Entities.TransactionEntity", b =>
                {
                    b.Property<long>("LineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<long>("LineId"));

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

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("LineId");

                    b.HasIndex("Time");

                    b.ToTable("apm_transaction", "public");
                });
#pragma warning restore 612, 618
        }
    }
}
