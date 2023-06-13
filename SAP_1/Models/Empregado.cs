using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAP_1.Models;

[Table("tb_empregados")]
public partial class Empregado
{
    [Key]
    [Column("id_empregado")]
    public int IdEmpregado { get; set; }

    [Column("nm_empregado")]
    [StringLength(60)]
    [Unicode(false)]
    public string NmEmpregado { get; set; } = null!;

    [Column("iniciais_empregado")]
    [StringLength(5)]
    [Unicode(false)]
    public string IniciaisEmpregado { get; set; } = null!;

    [Column("ds_cargo")]
    [StringLength(40)]
    [Unicode(false)]
    public string? DsCargo { get; set; }

    [Column("id_gerente")]
    public int? IdGerente { get; set; }

    [Column("dt_nascimento", TypeName = "date")]
    public DateTime DtNascimento { get; set; }

    [Column("salario", TypeName = "numeric(7, 2)")]
    public decimal Salario { get; set; }

    [Column("comissao")]
    public double? Comissao { get; set; }

    [Column("id_departamento")]
    public int? IdDepartamento { get; set; }

    [Column("fg_ativo")]
    public bool? FgAtivo { get; set; }

    [ForeignKey("IdGerente")]
    [InverseProperty("InverseIdGerenteNavigation")]
    public virtual Empregado? IdGerenteNavigation { get; set; }

    [InverseProperty("IdGerenteNavigation")]
    public virtual ICollection<Empregado> InverseIdGerenteNavigation { get; set; } = new List<Empregado>();

    [InverseProperty("IdInstrutorNavigation")]
    public virtual ICollection<CursoOferecido> TbCursosOferecidos { get; set; } = new List<CursoOferecido>();

    [InverseProperty("IdGerenteNavigation")]
    public virtual ICollection<Departamento> TbDepartamentos { get; set; } = new List<Departamento>();

    [InverseProperty("IdEmpregadoNavigation")]
    public virtual ICollection<Historico> TbHistoricos { get; set; } = new List<Historico>();

    [InverseProperty("IdParticipanteNavigation")]
    public virtual ICollection<Matricula> TbMatriculas { get; set; } = new List<Matricula>();
}
