﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pokedex.Infra.Contexts;

#nullable disable

namespace Pokedex.Infra.Migrations
{
    [DbContext(typeof(ApplicationPokedexDbContext))]
    [Migration("20231020030409_AddDefaultTipos")]
    partial class AddDefaultTipos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Pokedex.Domain.Entities.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtualizadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("CriadoEm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.Property<int>("PokemonTipoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PokemonTipoId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("Pokedex.Domain.Entities.PokemonTipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.HasKey("Id");

                    b.ToTable("PokemonTipos");
                });

            modelBuilder.Entity("Pokedex.Domain.Entities.Pokemon", b =>
                {
                    b.HasOne("Pokedex.Domain.Entities.PokemonTipo", "PokemonTipo")
                        .WithMany("Pokemons")
                        .HasForeignKey("PokemonTipoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PokemonTipo");
                });

            modelBuilder.Entity("Pokedex.Domain.Entities.PokemonTipo", b =>
                {
                    b.Navigation("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}
