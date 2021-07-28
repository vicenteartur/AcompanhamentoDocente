using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class dbAcompanhamentoContext : DbContext
    {
        public dbAcompanhamentoContext()
        {
        }

        public dbAcompanhamentoContext(DbContextOptions<dbAcompanhamentoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
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
                optionsBuilder.UseSqlServer("Server=localhost;Database=dbAcompanhamentoDocente;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<TbAno>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbAno");

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
                    .HasName("PK_dbo.tbAtribuicaoColaboradorEscola");

                entity.ToTable("tbAtribuicaoColaboradorEscola");

                entity.HasIndex(e => e.CodigoColaborador, "IX_CodigoColaborador");

                entity.HasIndex(e => e.CodigoEscola, "IX_CodigoEscola");

                entity.HasOne(d => d.CodigoColaboradorNavigation)
                    .WithMany(p => p.TbAtribuicaoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoColaborador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbAtribuicaoColaboradorEscola_dbo.tbColaborador_CodigoColaborador");

                entity.HasOne(d => d.CodigoEscolaNavigation)
                    .WithMany(p => p.TbAtribuicaoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoEscola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbAtribuicaoColaboradorEscola_dbo.tbEscola_CodigoEscola");
            });

            modelBuilder.Entity<TbAtribuicaoComponenteCurricularAnoColaboradorEscola>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbAtribuicaoComponenteCurricularAnoColaboradorEscola");

                entity.ToTable("tbAtribuicaoComponenteCurricularAnoColaboradorEscola");

                entity.HasIndex(e => e.CodigoAno, "IX_CodigoAno");

                entity.HasIndex(e => e.CodigoAtribuicaoColaboradorEscola, "IX_CodigoAtribuicaoColaboradorEscola");

                entity.HasIndex(e => e.CodigoComponenteCurricular, "IX_CodigoComponenteCurricular");

                entity.HasOne(d => d.CodigoAnoNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoAno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbAtribuicaoComponenteCurricularAnoColaboradorEscola_dbo.tbAno_CodigoAno");

                entity.HasOne(d => d.CodigoAtribuicaoColaboradorEscolaNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoAtribuicaoColaboradorEscola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbAtribuicaoComponenteCurricularAnoColaboradorEscola_dbo.tbAtribuicaoColaboradorEscola_CodigoAtribuicaoColaboradorEscola");

                entity.HasOne(d => d.CodigoComponenteCurricularNavigation)
                    .WithMany(p => p.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas)
                    .HasForeignKey(d => d.CodigoComponenteCurricular)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbAtribuicaoComponenteCurricularAnoColaboradorEscola_dbo.tbComponenteCurricular_CodigoComponenteCurricular");
            });

            modelBuilder.Entity<TbAvaliacao>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbAvaliacao");

                entity.ToTable("tbAvaliacao");

                entity.HasIndex(e => e.CodigoColaboradorAvaliador, "IX_CodigoColaboradorAvaliador");

                entity.HasOne(d => d.CodigoColaboradorAvaliadorNavigation)
                    .WithMany(p => p.TbAvaliacaos)
                    .HasForeignKey(d => d.CodigoColaboradorAvaliador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbAvaliacao_dbo.tbColaborador_CodigoColaboradorAvaliador");
            });

            modelBuilder.Entity<TbCargo>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbCargo");

                entity.ToTable("tbCargo");

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NiveldeAcesso)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<TbCidade>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbCidade");

                entity.ToTable("tbCidade");

                entity.HasIndex(e => e.CodigoEstado, "IX_CodigoEstado");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CodigoEstadoNavigation)
                    .WithMany(p => p.TbCidades)
                    .HasForeignKey(d => d.CodigoEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbCidade_dbo.tbEstado_CodigoEstado");
            });

            modelBuilder.Entity<TbClassificacaoCriterio>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbClassificacaoCriterio");

                entity.ToTable("tbClassificacaoCriterio");

                entity.Property(e => e.Classificacao).HasMaxLength(50);
            });

            modelBuilder.Entity<TbColaborador>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbColaborador");

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
                    .HasConstraintName("FK_dbo.tbColaborador_dbo.tbCargo_CodigoCargo");
            });

            modelBuilder.Entity<TbComponenteCurricular>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbComponenteCurricular");

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
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbCriterioAvaliacao");

                entity.ToTable("tbCriterioAvaliacao");

                entity.HasIndex(e => e.CodigoClassificacaoCriterio, "IX_CodigoClassificacaoCriterio");

                entity.Property(e => e.Criterio)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.CodigoClassificacaoCriterioNavigation)
                    .WithMany(p => p.TbCriterioAvaliacaos)
                    .HasForeignKey(d => d.CodigoClassificacaoCriterio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbCriterioAvaliacao_dbo.tbClassificacaoCriterio_CodigoClassificacaoCriterio");
            });

            modelBuilder.Entity<TbCriterioAvaliado>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbCriterioAvaliado");

                entity.ToTable("tbCriterioAvaliado");

                entity.HasIndex(e => e.CodigoAvaliacao, "IX_CodigoAvaliacao");

                entity.HasIndex(e => e.CodigoCriterioAvaliacao, "IX_CodigoCriterioAvaliacao");

                entity.Property(e => e.Comentário).HasColumnType("text");

                entity.HasOne(d => d.CodigoAvaliacaoNavigation)
                    .WithMany(p => p.TbCriterioAvaliados)
                    .HasForeignKey(d => d.CodigoAvaliacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbCriterioAvaliado_dbo.tbAvaliacao_CodigoAvaliacao");

                entity.HasOne(d => d.CodigoCriterioAvaliacaoNavigation)
                    .WithMany(p => p.TbCriterioAvaliados)
                    .HasForeignKey(d => d.CodigoCriterioAvaliacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.tbCriterioAvaliado_dbo.tbCriterioAvaliacao_CodigoCriterioAvaliacao");
            });

            modelBuilder.Entity<TbEscola>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbEscola");

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
                    .HasConstraintName("FK_dbo.tbEscola_dbo.tbCidade_CodigoCidade");
            });

            modelBuilder.Entity<TbEstado>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_dbo.tbEstado");

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
