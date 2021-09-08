﻿// <auto-generated />
using System;
using AcompanhamentoDocente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AcompanhamentoDocente.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20210903122531_Acomp")]
    partial class Acomp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Latin1_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAno", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ano")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength(true);

                    b.Property<int>("CodigoModalidade")
                        .HasColumnType("int");

                    b.Property<string>("Periodo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Turma")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength(true);

                    b.HasKey("Codigo")
                        .HasName("pkAno");

                    b.HasIndex("CodigoModalidade");

                    b.ToTable("tbAno");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAtribuicaoColaboradorEscola", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Ativa")
                        .HasColumnType("tinyint");

                    b.Property<int>("CodigoColaborador")
                        .HasColumnType("int");

                    b.Property<int>("CodigoEscola")
                        .HasColumnType("int");

                    b.HasKey("Codigo")
                        .HasName("pkAtribuicaoColaboradorEscola");

                    b.HasIndex(new[] { "CodigoColaborador" }, "IX_CodigoColaborador");

                    b.HasIndex(new[] { "CodigoEscola" }, "IX_CodigoEscola");

                    b.ToTable("tbAtribuicaoColaboradorEscola");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAtribuicaoComponenteCurricularAnoColaboradorEscola", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Ativa")
                        .HasColumnType("tinyint");

                    b.Property<int>("CodigoAno")
                        .HasColumnType("int");

                    b.Property<int>("CodigoAtribuicaoColaboradorEscola")
                        .HasColumnType("int");

                    b.Property<int>("CodigoComponenteCurricular")
                        .HasColumnType("int");

                    b.HasKey("Codigo")
                        .HasName("pkAtribuicaoComponenteCurricularAnoColaboradorEscola");

                    b.HasIndex(new[] { "CodigoAno" }, "IX_CodigoAno");

                    b.HasIndex(new[] { "CodigoAtribuicaoColaboradorEscola" }, "IX_CodigoAtribuicaoColaboradorEscola");

                    b.HasIndex(new[] { "CodigoComponenteCurricular" }, "IX_CodigoComponenteCurricular");

                    b.ToTable("tbAtribuicaoComponenteCurricularAnoColaboradorEscola");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAvaliacao", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola")
                        .HasColumnType("int");

                    b.Property<int>("CodigoColaboradorAvaliador")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datarealizacao")
                        .HasColumnType("date")
                        .HasColumnName("datarealizacao");

                    b.Property<byte>("Finalizada")
                        .HasColumnType("tinyint");

                    b.HasKey("Codigo")
                        .HasName("pkAvaliacao");

                    b.HasIndex("CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola");

                    b.HasIndex(new[] { "CodigoColaboradorAvaliador" }, "IX_CodigoColaboradorAvaliador");

                    b.ToTable("tbAvaliacao");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCargo", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NiveldeAcesso")
                        .HasColumnType("int");

                    b.HasKey("Codigo")
                        .HasName("pkCargo");

                    b.ToTable("tbCargo");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCidade", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CodigoEstado")
                        .HasColumnType("int");

                    b.HasKey("Codigo")
                        .HasName("pkCidade");

                    b.HasIndex(new[] { "CodigoEstado" }, "IX_CodigoEstado");

                    b.ToTable("tbCidade");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbClassificacaoCriterio", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Classificacao")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Codigo")
                        .HasName("pkClassificacaoCriterio");

                    b.ToTable("tbClassificacaoCriterio");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbColaborador", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Ativo")
                        .HasColumnType("tinyint");

                    b.Property<int?>("CodigoCargo")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nchar(80)")
                        .IsFixedLength(true);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nchar(80)")
                        .IsFixedLength(true);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength(true);

                    b.HasKey("Codigo")
                        .HasName("pkColaborador");

                    b.HasIndex(new[] { "CodigoCargo" }, "IX_CodigoCargo");

                    b.ToTable("tbColaborador");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbComponenteCurricular", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Ativa")
                        .HasColumnType("tinyint");

                    b.Property<int>("CodigoModalidade")
                        .HasColumnType("int");

                    b.Property<string>("ComponenteCurricular")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nchar(25)")
                        .IsFixedLength(true);

                    b.Property<string>("SubArea")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength(true);

                    b.HasKey("Codigo")
                        .HasName("pkComponenteCurricular");

                    b.HasIndex("CodigoModalidade");

                    b.ToTable("tbComponenteCurricular");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCriterioAvaliacao", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Ativa")
                        .HasColumnType("tinyint");

                    b.Property<int>("CodigoClassificacaoCriterio")
                        .HasColumnType("int");

                    b.Property<string>("Criterio")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Codigo")
                        .HasName("pkCriterioAvaliacao");

                    b.HasIndex(new[] { "CodigoClassificacaoCriterio" }, "IX_CodigoClassificacaoCriterio");

                    b.ToTable("tbCriterioAvaliacao");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCriterioAvaliado", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoAvaliacao")
                        .HasColumnType("int");

                    b.Property<int>("CodigoCriterioAvaliacao")
                        .HasColumnType("int");

                    b.Property<string>("Comentario")
                        .HasColumnType("text");

                    b.Property<byte>("Conceito")
                        .HasColumnType("tinyint");

                    b.HasKey("Codigo")
                        .HasName("pkCriterioAvaliado");

                    b.HasIndex(new[] { "CodigoAvaliacao" }, "IX_CodigoAvaliacao");

                    b.HasIndex(new[] { "CodigoCriterioAvaliacao" }, "IX_CodigoCriterioAvaliacao");

                    b.ToTable("tbCriterioAvaliado");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCriterioComponenteCurricular", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Ativa")
                        .HasColumnType("int");

                    b.Property<int>("CodigoComponenteCurricular")
                        .HasColumnType("int");

                    b.Property<int>("CodigoCriterioAvaliacao")
                        .HasColumnType("int");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoComponenteCurricular");

                    b.HasIndex("CodigoCriterioAvaliacao");

                    b.ToTable("tbCriterioComponenteCurricular");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbEscola", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Ativa")
                        .HasColumnType("tinyint");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CodigoCidade")
                        .HasColumnType("int");

                    b.Property<string>("Escola")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Inep")
                        .HasColumnType("int")
                        .HasColumnName("INEP");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Codigo")
                        .HasName("pkEscola");

                    b.HasIndex(new[] { "CodigoCidade" }, "IX_CodigoCidade");

                    b.ToTable("tbEscola");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbEstado", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .IsFixedLength(true);

                    b.HasKey("Codigo")
                        .HasName("pkEstado");

                    b.ToTable("tbEstado");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbModalidade", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Modalidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength(true);

                    b.HasKey("Codigo");

                    b.ToTable("tbModalidade");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAno", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbModalidade", "CodigoModalidadeNavigation")
                        .WithMany("TbAnos")
                        .HasForeignKey("CodigoModalidade")
                        .HasConstraintName("FK_tbAno_tbModalidade")
                        .IsRequired();

                    b.Navigation("CodigoModalidadeNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAtribuicaoColaboradorEscola", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbColaborador", "CodigoColaboradorNavigation")
                        .WithMany("TbAtribuicaoColaboradorEscolas")
                        .HasForeignKey("CodigoColaborador")
                        .HasConstraintName("fkCodigoColaborador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcompanhamentoDocente.Models.TbEscola", "CodigoEscolaNavigation")
                        .WithMany("TbAtribuicaoColaboradorEscolas")
                        .HasForeignKey("CodigoEscola")
                        .HasConstraintName("fkCodigoEscola")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CodigoColaboradorNavigation");

                    b.Navigation("CodigoEscolaNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAtribuicaoComponenteCurricularAnoColaboradorEscola", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbAno", "CodigoAnoNavigation")
                        .WithMany("TbAtribuicaoComponenteCurricularAnoColaboradorEscolas")
                        .HasForeignKey("CodigoAno")
                        .HasConstraintName("fkCodigoAno")
                        .IsRequired();

                    b.HasOne("AcompanhamentoDocente.Models.TbAtribuicaoColaboradorEscola", "CodigoAtribuicaoColaboradorEscolaNavigation")
                        .WithMany("TbAtribuicaoComponenteCurricularAnoColaboradorEscolas")
                        .HasForeignKey("CodigoAtribuicaoColaboradorEscola")
                        .HasConstraintName("FK_dbo.tbAtribuicaoComponenteCurricularAnoColaboradorEscola_dbo.tbAtribuicaoColaboradorEscola_CodigoAtribuicaoColaboradorEscola")
                        .IsRequired();

                    b.HasOne("AcompanhamentoDocente.Models.TbComponenteCurricular", "CodigoComponenteCurricularNavigation")
                        .WithMany("TbAtribuicaoComponenteCurricularAnoColaboradorEscolas")
                        .HasForeignKey("CodigoComponenteCurricular")
                        .HasConstraintName("fkCodigoComponenteCurricular")
                        .IsRequired();

                    b.Navigation("CodigoAnoNavigation");

                    b.Navigation("CodigoAtribuicaoColaboradorEscolaNavigation");

                    b.Navigation("CodigoComponenteCurricularNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAvaliacao", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbAtribuicaoComponenteCurricularAnoColaboradorEscola", "CodigoAtribuicaoComponenteCurricularAnoColaboradorEscolaNavigation")
                        .WithMany("TbAvaliacaos")
                        .HasForeignKey("CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola")
                        .HasConstraintName("fkAtribuicaoComponenteCurricularAnoColaboradorEscola")
                        .IsRequired();

                    b.HasOne("AcompanhamentoDocente.Models.TbColaborador", "CodigoColaboradorAvaliadorNavigation")
                        .WithMany("TbAvaliacaos")
                        .HasForeignKey("CodigoColaboradorAvaliador")
                        .HasConstraintName("fkCodigoColaboradorAvaliador")
                        .IsRequired();

                    b.Navigation("CodigoAtribuicaoComponenteCurricularAnoColaboradorEscolaNavigation");

                    b.Navigation("CodigoColaboradorAvaliadorNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCidade", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbEstado", "CodigoEstadoNavigation")
                        .WithMany("TbCidades")
                        .HasForeignKey("CodigoEstado")
                        .HasConstraintName("fkCodigoEstado")
                        .IsRequired();

                    b.Navigation("CodigoEstadoNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbColaborador", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbCargo", "CodigoCargoNavigation")
                        .WithMany("TbColaboradors")
                        .HasForeignKey("CodigoCargo")
                        .HasConstraintName("fkCodigoCargo");

                    b.Navigation("CodigoCargoNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbComponenteCurricular", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbModalidade", "CodigoModalidadeNavigation")
                        .WithMany("TbComponenteCurriculars")
                        .HasForeignKey("CodigoModalidade")
                        .HasConstraintName("fkCodigoModalidade")
                        .IsRequired();

                    b.Navigation("CodigoModalidadeNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCriterioAvaliacao", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbClassificacaoCriterio", "CodigoClassificacaoCriterioNavigation")
                        .WithMany("TbCriterioAvaliacaos")
                        .HasForeignKey("CodigoClassificacaoCriterio")
                        .HasConstraintName("fkCodigoClassificacaoCriterio")
                        .IsRequired();

                    b.Navigation("CodigoClassificacaoCriterioNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCriterioAvaliado", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbAvaliacao", "CodigoAvaliacaoNavigation")
                        .WithMany("TbCriterioAvaliados")
                        .HasForeignKey("CodigoAvaliacao")
                        .HasConstraintName("fkCodigoAvaliacao")
                        .IsRequired();

                    b.HasOne("AcompanhamentoDocente.Models.TbCriterioAvaliacao", "CodigoCriterioAvaliacaoNavigation")
                        .WithMany("TbCriterioAvaliados")
                        .HasForeignKey("CodigoCriterioAvaliacao")
                        .HasConstraintName("fkCodigoCriterioAvaliacao")
                        .IsRequired();

                    b.Navigation("CodigoAvaliacaoNavigation");

                    b.Navigation("CodigoCriterioAvaliacaoNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCriterioComponenteCurricular", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbComponenteCurricular", "CodigoComponenteCurricularNavigation")
                        .WithMany("TbCriterioComponenteCurriculars")
                        .HasForeignKey("CodigoComponenteCurricular")
                        .HasConstraintName("fkCodigoCCurricular")
                        .IsRequired();

                    b.HasOne("AcompanhamentoDocente.Models.TbCriterioAvaliacao", "CodigoCriterioAvaliacaoNavigation")
                        .WithMany("TbCriterioComponenteCurriculars")
                        .HasForeignKey("CodigoCriterioAvaliacao")
                        .HasConstraintName("fkCodigoCAvaliacao")
                        .IsRequired();

                    b.Navigation("CodigoComponenteCurricularNavigation");

                    b.Navigation("CodigoCriterioAvaliacaoNavigation");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbEscola", b =>
                {
                    b.HasOne("AcompanhamentoDocente.Models.TbCidade", "CodigoCidadeNavigation")
                        .WithMany("TbEscolas")
                        .HasForeignKey("CodigoCidade")
                        .HasConstraintName("fkCodigoCidade")
                        .IsRequired();

                    b.Navigation("CodigoCidadeNavigation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAno", b =>
                {
                    b.Navigation("TbAtribuicaoComponenteCurricularAnoColaboradorEscolas");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAtribuicaoColaboradorEscola", b =>
                {
                    b.Navigation("TbAtribuicaoComponenteCurricularAnoColaboradorEscolas");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAtribuicaoComponenteCurricularAnoColaboradorEscola", b =>
                {
                    b.Navigation("TbAvaliacaos");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbAvaliacao", b =>
                {
                    b.Navigation("TbCriterioAvaliados");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCargo", b =>
                {
                    b.Navigation("TbColaboradors");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCidade", b =>
                {
                    b.Navigation("TbEscolas");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbClassificacaoCriterio", b =>
                {
                    b.Navigation("TbCriterioAvaliacaos");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbColaborador", b =>
                {
                    b.Navigation("TbAtribuicaoColaboradorEscolas");

                    b.Navigation("TbAvaliacaos");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbComponenteCurricular", b =>
                {
                    b.Navigation("TbAtribuicaoComponenteCurricularAnoColaboradorEscolas");

                    b.Navigation("TbCriterioComponenteCurriculars");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbCriterioAvaliacao", b =>
                {
                    b.Navigation("TbCriterioAvaliados");

                    b.Navigation("TbCriterioComponenteCurriculars");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbEscola", b =>
                {
                    b.Navigation("TbAtribuicaoColaboradorEscolas");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbEstado", b =>
                {
                    b.Navigation("TbCidades");
                });

            modelBuilder.Entity("AcompanhamentoDocente.Models.TbModalidade", b =>
                {
                    b.Navigation("TbAnos");

                    b.Navigation("TbComponenteCurriculars");
                });
#pragma warning restore 612, 618
        }
    }
}