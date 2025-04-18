﻿// <auto-generated />
using System;
using CurrencyExchange.Persistence.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CurrencyExchange.Persistence.Migrations
{
    [DbContext(typeof(CurrencyExchangeDbContext))]
    partial class CurrencyExchangeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CurrencyExchange.Domain.Models.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Sign")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("CurrencyExchange.Domain.Models.ExchangeRates", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BaseCurrencyId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Rate")
                        .HasPrecision(18, 6)
                        .HasColumnType("numeric(18,6)");

                    b.Property<Guid>("TargetCurrencyId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BaseCurrencyId");

                    b.HasIndex("TargetCurrencyId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("CurrencyExchange.Domain.Models.ExchangeRates", b =>
                {
                    b.HasOne("CurrencyExchange.Domain.Models.Currency", "BaseCurrency")
                        .WithMany("BaseCurrencyRates")
                        .HasForeignKey("BaseCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CurrencyExchange.Domain.Models.Currency", "TargetCurrency")
                        .WithMany("TargetCurrencyRates")
                        .HasForeignKey("TargetCurrencyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BaseCurrency");

                    b.Navigation("TargetCurrency");
                });

            modelBuilder.Entity("CurrencyExchange.Domain.Models.Currency", b =>
                {
                    b.Navigation("BaseCurrencyRates");

                    b.Navigation("TargetCurrencyRates");
                });
#pragma warning restore 612, 618
        }
    }
}
