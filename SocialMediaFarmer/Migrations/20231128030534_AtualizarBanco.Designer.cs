﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialMediaFarmer.Models;

#nullable disable

namespace SocialMediaFarmer.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20231128030534_AtualizarBanco")]
    partial class AtualizarBanco
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SocialMediaFarmer.Models.Pergunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Conteudo")
                        .HasColumnType("text");

                    b.Property<DateTime>("DataPublicacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Resolvida")
                        .HasColumnType("boolean");

                    b.Property<string>("Titulo")
                        .HasColumnType("text");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pergunta");
                });

            modelBuilder.Entity("SocialMediaFarmer.Models.Resposta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Aceita")
                        .HasColumnType("boolean");

                    b.Property<string>("Conteudo")
                        .HasColumnType("text");

                    b.Property<DateTime>("DataPublicacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PerguntaId")
                        .HasColumnType("integer");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PerguntaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Resposta");
                });

            modelBuilder.Entity("SocialMediaFarmer.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SocialMediaFarmer.Models.Pergunta", b =>
                {
                    b.HasOne("SocialMediaFarmer.Models.Usuario", "Usuario")
                        .WithMany("Perguntas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SocialMediaFarmer.Models.Resposta", b =>
                {
                    b.HasOne("SocialMediaFarmer.Models.Pergunta", "Pergunta")
                        .WithMany("Respostas")
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaFarmer.Models.Usuario", "Usuario")
                        .WithMany("Respostas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pergunta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SocialMediaFarmer.Models.Pergunta", b =>
                {
                    b.Navigation("Respostas");
                });

            modelBuilder.Entity("SocialMediaFarmer.Models.Usuario", b =>
                {
                    b.Navigation("Perguntas");

                    b.Navigation("Respostas");
                });
#pragma warning restore 612, 618
        }
    }
}
