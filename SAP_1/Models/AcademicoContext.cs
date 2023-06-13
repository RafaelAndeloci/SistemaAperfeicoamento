using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SAP_1.Models;

public partial class AcademicoContext : DbContext
{
    public AcademicoContext(DbContextOptions<AcademicoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> TbCursos { get; set; }

    public virtual DbSet<CursoOferecido> TbCursosOferecidos { get; set; }

    public virtual DbSet<Departamento> TbDepartamentos { get; set; }

    public virtual DbSet<Empregado> TbEmpregados { get; set; }

    public virtual DbSet<GradesSalario> TbGradesSalarios { get; set; }

    public virtual DbSet<Historico> TbHistoricos { get; set; }

    public virtual DbSet<Matricula> TbMatriculas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("pk_tb_cursos_id_curso");

            entity.Property(e => e.Categoria).IsFixedLength();
        });

        modelBuilder.Entity<CursoOferecido>(entity =>
        {
            entity.HasKey(e => new { e.IdCurso, e.DtInicio }).HasName("pk_tb_cursos_oferecidos");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.TbCursosOferecidos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_cursos_oferecidos_id_curso");

            entity.HasOne(d => d.IdInstrutorNavigation).WithMany(p => p.TbCursosOferecidos).HasConstraintName("fk_tb_cursos_oferecidos_id_instrutor");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("pk_tb_departamentos_id_depto");

            entity.HasOne(d => d.IdGerenteNavigation).WithMany(p => p.TbDepartamentos).HasConstraintName("fk_tb_departamentos_id_gerente");
        });

        modelBuilder.Entity<Empregado>(entity =>
        {
            entity.HasKey(e => e.IdEmpregado).HasName("pk_tb_emp_id_emp");

            entity.Property(e => e.IdDepartamento).HasDefaultValueSql("((10))");

            entity.HasOne(d => d.IdGerenteNavigation).WithMany(p => p.InverseIdGerenteNavigation).HasConstraintName("fk_tb_emp_id_gerente");
        });

        modelBuilder.Entity<GradesSalario>(entity =>
        {
            entity.HasKey(e => e.IdGrade).HasName("pk_tb_grades_id_gradee");
        });

        modelBuilder.Entity<Historico>(entity =>
        {
            entity.HasKey(e => new { e.IdEmpregado, e.DtInicio }).HasName("pk_tb_historicos");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.TbHistoricos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_historicos_id_depto");

            entity.HasOne(d => d.IdEmpregadoNavigation).WithMany(p => p.TbHistoricos).HasConstraintName("fk_tb_historicos_id_emp");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => new { e.IdParticipante, e.IdCurso, e.DtInicio }).HasName("pk_tb_matriculas");

            entity.Property(e => e.IdParticipante).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.TbMatriculas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_matriculas_id_participante");

            entity.HasOne(d => d.CursoOferecido).WithMany(p => p.TbMatriculas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tb_matriculas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
