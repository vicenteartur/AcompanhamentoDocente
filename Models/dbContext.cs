using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
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
        public virtual DbSet<TbCriterioComponenteCurricular> TbCriterioComponenteCurriculars { get; set; }
        public virtual DbSet<TbEscola> TbEscolas { get; set; }
        public virtual DbSet<TbEstado> TbEstados { get; set; }
        public virtual DbSet<TbModalidade> TbModalidades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=dbAcompanhamentoDocente;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<TbAno>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkAno");

                entity.ToTable("tbAno");

                entity.Property(e => e.Ano)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Periodo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Turma)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CodigoModalidadeNavigation)
                    .WithMany(p => p.TbAnos)
                    .HasForeignKey(d => d.CodigoModalidade)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbAno_tbModalidade");
            });

            modelBuilder.Entity<TbAtribuicaoColaboradorEscola>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkAtribuicaoColaboradorEscola");

                entity.ToTable("tbAtribuicaoColaboradorEscola");

                entity.HasIndex(e => e.CodigoColaborador, "IX_CodigoColaborador");

                entity.HasIndex(e => e.CodigoEscola, "IX_CodigoEscola");

                entity.HasOne(d => d.CodigoColaboradorNavigation)
                    .WithMany(p => p.TbAtribuicaoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoColaborador)
                    .HasConstraintName("fkCodigoColaborador");

                entity.HasOne(d => d.CodigoEscolaNavigation)
                    .WithMany(p => p.TbAtribuicaoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoEscola)
                    .HasConstraintName("fkCodigoEscola");
            });

            modelBuilder.Entity<TbAtribuicaoComponenteCurricularAnoColaboradorEscola>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkAtribuicaoComponenteCurricularAnoColaboradorEscola");

                entity.ToTable("tbAtribuicaoComponenteCurricularAnoColaboradorEscola");

                entity.HasIndex(e => e.CodigoAno, "IX_CodigoAno");

                entity.HasIndex(e => e.CodigoAtribuicaoColaboradorEscola, "IX_CodigoAtribuicaoColaboradorEscola");

                entity.HasIndex(e => e.CodigoComponenteCurricular, "IX_CodigoComponenteCurricular");

                entity.HasOne(d => d.CodigoAnoNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoAno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoAno");

                entity.HasOne(d => d.CodigoAtribuicaoColaboradorEscolaNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoAtribuicaoColaboradorEscola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbAtribuicaoComponenteCurricularAnoColaboradorEscola_dbo.tbAtribuicaoColaboradorEscola_CodigoAtribuicaoColaboradorEscola");

                entity.HasOne(d => d.CodigoComponenteCurricularNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoComponenteCurricular)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoComponenteCurricular");
            });

            modelBuilder.Entity<TbAvaliacao>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkAvaliacao");

                entity.ToTable("tbAvaliacao");

                entity.HasIndex(e => e.CodigoColaboradorAvaliador, "IX_CodigoColaboradorAvaliador");

                entity.Property(e => e.Datarealizacao)
                    .HasColumnType("date")
                    .HasColumnName("datarealizacao");

                entity.HasOne(d => d.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscolaNavigation)
                    .WithMany(p => p.TbAvaliacaos)
                    .HasForeignKey(d => d.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkAtribuicaoComponenteCurricularAnoColaboradorEscola");

                entity.HasOne(d => d.CodigoColaboradorAvaliadorNavigation)
                    .WithMany(p => p.TbAvaliacaos)
                    .HasForeignKey(d => d.CodigoColaboradorAvaliador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoColaboradorAvaliador");
            });

            modelBuilder.Entity<TbCargo>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkCargo");

                entity.ToTable("tbCargo");

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TbCidade>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkCidade");

                entity.ToTable("tbCidade");

                entity.HasIndex(e => e.CodigoEstado, "IX_CodigoEstado");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CodigoEstadoNavigation)
                    .WithMany(p => p.TbCidades)
                    .HasForeignKey(d => d.CodigoEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoEstado");
            });

            modelBuilder.Entity<TbClassificacaoCriterio>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkClassificacaoCriterio");

                entity.ToTable("tbClassificacaoCriterio");

                entity.Property(e => e.Classificacao).HasMaxLength(50);
            });

            modelBuilder.Entity<TbColaborador>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkColaborador");

                entity.ToTable("tbColaborador");

                entity.HasIndex(e => e.CodigoCargo, "IX_CodigoCargo");

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
                    .HasConstraintName("fkCodigoCargo");
            });

            modelBuilder.Entity<TbComponenteCurricular>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkComponenteCurricular");

                entity.ToTable("tbComponenteCurricular");

                entity.Property(e => e.ComponenteCurricular)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength(true);

                entity.Property(e => e.SubArea)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CodigoModalidadeNavigation)
                    .WithMany(p => p.TbComponenteCurriculars)
                    .HasForeignKey(d => d.CodigoModalidade)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoModalidade");
            });

            modelBuilder.Entity<TbCriterioAvaliacao>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkCriterioAvaliacao");

                entity.ToTable("tbCriterioAvaliacao");

                entity.HasIndex(e => e.CodigoClassificacaoCriterio, "IX_CodigoClassificacaoCriterio");

                entity.Property(e => e.Criterio)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.CodigoClassificacaoCriterioNavigation)
                    .WithMany(p => p.TbCriterioAvaliacaos)
                    .HasForeignKey(d => d.CodigoClassificacaoCriterio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoClassificacaoCriterio");
            });

            modelBuilder.Entity<TbCriterioAvaliado>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkCriterioAvaliado");

                entity.ToTable("tbCriterioAvaliado");

                entity.HasIndex(e => e.CodigoAvaliacao, "IX_CodigoAvaliacao");

                entity.HasIndex(e => e.CodigoCriterioAvaliacao, "IX_CodigoCriterioAvaliacao");

                entity.Property(e => e.Comentario).HasColumnType("text");

                entity.HasOne(d => d.CodigoAvaliacaoNavigation)
                    .WithMany(p => p.TbCriterioAvaliados)
                    .HasForeignKey(d => d.CodigoAvaliacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoAvaliacao");

                entity.HasOne(d => d.CodigoCriterioAvaliacaoNavigation)
                    .WithMany(p => p.TbCriterioAvaliados)
                    .HasForeignKey(d => d.CodigoCriterioAvaliacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoCriterioAvaliacao");
            });

            modelBuilder.Entity<TbCriterioComponenteCurricular>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbCriterioComponenteCurricular");

                entity.HasOne(d => d.CodigoComponenteCurricularNavigation)
                    .WithMany(p => p.TbCriterioComponenteCurriculars)
                    .HasForeignKey(d => d.CodigoComponenteCurricular)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoCCurricular");

                entity.HasOne(d => d.CodigoCriterioAvaliacaoNavigation)
                    .WithMany(p => p.TbCriterioComponenteCurriculars)
                    .HasForeignKey(d => d.CodigoCriterioAvaliacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCodigoCAvaliacao");
            });

            modelBuilder.Entity<TbEscola>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkEscola");

                entity.ToTable("tbEscola");

                entity.HasIndex(e => e.CodigoCidade, "IX_CodigoCidade");

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
                    .HasConstraintName("fkCodigoCidade");
            });

            modelBuilder.Entity<TbEstado>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("pkEstado");

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

            modelBuilder.Entity<TbModalidade>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("tbModalidade");

                entity.Property(e => e.Modalidade)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
