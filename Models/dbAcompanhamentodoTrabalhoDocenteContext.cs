﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class dbAcompanhamentodoTrabalhoDocenteContext : DbContext
    {
        public dbAcompanhamentodoTrabalhoDocenteContext()
        {
        }

        public dbAcompanhamentodoTrabalhoDocenteContext(DbContextOptions<dbAcompanhamentodoTrabalhoDocenteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAno> TbAnos { get; set; }
        public virtual DbSet<TbAtribuicaoColaboradorEscola> TbAtribuicaoColaboradorEscolas { get; set; }
        public virtual DbSet<TbAtribuicaoComponenteCurricularAnoColaboradorEscola> TbAtribuicaoComponenteCurricularAnoColaboradorEscolas { get; set; }
        public virtual DbSet<TbAvaliacao> TbAvaliacaos { get; set; }
        public virtual DbSet<TbCargo> TbCargos { get; set; }
        public virtual DbSet<TbCidade> TbCidades { get; set; }
        public virtual DbSet<TbClassificacaoCriterio> TbClassificacaoCriterios { get; set; }
        public virtual DbSet<TbColaborador> TbColaboradors { get; set; }
        public virtual DbSet<TbComponenteCurricular> TbComponenteCurriculars { get; set; }
        public virtual DbSet<TbCriterioAvaliacao> TbCriterioAvaliacaos { get; set; }
        public virtual DbSet<TbCriterioAvaliado> TbCriterioAvaliados { get; set; }
        public virtual DbSet<TbEscola> TbEscolas { get; set; }
        public virtual DbSet<TbEstado> TbEstados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=dbAcompanhamentodoTrabalhoDocente;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<TbAno>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbAno");

                entity.Property(e => e.Ano)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Modalidade)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Periodo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Turma)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbAtribuicaoColaboradorEscola>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkAtribuicaoColaboradorEscola");

                entity.ToTable("tbAtribuicaoColaboradorEscola");

                entity.HasOne(d => d.CodigoColaboradorNavigation)
                    .WithMany(p => p.TbAtribuicaoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoColaborador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkColaborador");

                entity.HasOne(d => d.CodigoEscolaNavigation)
                    .WithMany(p => p.TbAtribuicaoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoEscola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoEscola");
            });

            modelBuilder.Entity<TbAtribuicaoComponenteCurricularAnoColaboradorEscola>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbAtribuicaoComponenteCurricularAnoColaboradorEscola");

                entity.HasOne(d => d.CodigoAnoNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoAno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbAtribuicaoComponenteCurricularAnoColaboradorEscola_tbAno");

                entity.HasOne(d => d.CodigoAtribuicaoColaboradorEscolaNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoAtribuicaoColaboradorEscola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkAtribuicaoColaboradorEscola");

                entity.HasOne(d => d.CodigoComponenteCurricularNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoComponenteCurricular)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbAtribuicaoComponenteCurricularAnoColaboradorEscola_tbComponenteCurricular");
            });

            modelBuilder.Entity<TbAvaliacao>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbAvaliacao");

                entity.HasOne(d => d.CodigoColaboradorAvaliadorNavigation)
                    .WithMany(p => p.TbAvaliacaos)
                    .HasForeignKey(d => d.CodigoColaboradorAvaliador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbAvaliacao_tbColaborador");
            });

            modelBuilder.Entity<TbCargo>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbCargo");

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TbCidade>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbCidade");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CodigoEstadoNavigation)
                    .WithMany(p => p.TbCidades)
                    .HasForeignKey(d => d.CodigoEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkEstado");
            });

            modelBuilder.Entity<TbClassificacaoCriterio>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbClassificacaoCriterio");

                entity.Property(e => e.Classificacao).HasMaxLength(50);
            });

            modelBuilder.Entity<TbColaborador>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbColaborador");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsFixedLength(true);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsFixedLength(true);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CodigoCargoNavigation)
                    .WithMany(p => p.TbColaboradors)
                    .HasForeignKey(d => d.CodigoCargo)
                    .HasConstraintName("fkCargo");
            });

            modelBuilder.Entity<TbComponenteCurricular>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbComponenteCurricular");

                entity.Property(e => e.ComponenteCurricular)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength(true);

                entity.Property(e => e.SubArea)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbCriterioAvaliacao>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbCriterioAvaliacao");

                entity.Property(e => e.Criterio)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.CodigoClassificacaoCriterioNavigation)
                    .WithMany(p => p.TbCriterioAvaliacaos)
                    .HasForeignKey(d => d.CodigoClassificacaoCriterio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbCriterioAvaliacao_tbClassificacaoCriterio");
            });

            modelBuilder.Entity<TbCriterioAvaliado>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbCriterioAvaliado");

                entity.Property(e => e.Comentário).HasColumnType("text");

                entity.HasOne(d => d.CodigoAvaliacaoNavigation)
                    .WithMany(p => p.TbCriterioAvaliados)
                    .HasForeignKey(d => d.CodigoAvaliacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkAvaliacao");

                entity.HasOne(d => d.CodigoCriterioAvaliacaoNavigation)
                    .WithMany(p => p.TbCriterioAvaliados)
                    .HasForeignKey(d => d.CodigoCriterioAvaliacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCriterioAvaliacao");
            });

            modelBuilder.Entity<TbEscola>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbEscola");

                entity.HasIndex(e => e.Codigo, "UN_tbEscola_INEP")
                    .IsUnique();

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Escola)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Inep).HasColumnName("INEP");

                entity.Property(e => e.Rua)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CodigoCidadeNavigation)
                    .WithMany(p => p.TbEscolas)
                    .HasForeignKey(d => d.CodigoCidade)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCidade");
            });

            modelBuilder.Entity<TbEstado>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbEstado");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sigla)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
