﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAP_1.Models;

[PrimaryKey("IdParticipante", "IdCurso", "DtInicio")]
[Table("tb_matriculas")]
public partial class Matricula
{
    [Key]
    [Column("id_participante")]
    public int IdParticipante { get; set; }

    [Key]
    [Column("id_curso")]
    [StringLength(6)]
    [Unicode(false)]
    public string IdCurso { get; set; } = null!;

    [Key]
    [Column("dt_inicio", TypeName = "date")]
    public DateTime DtInicio { get; set; }

    [Column("avaliacao")]
    public int? Avaliacao { get; set; }

    [Column("fg_ativo")]
    public bool? FgAtivo { get; set; }

    [ForeignKey("IdParticipante")]
    [InverseProperty("TbMatriculas")]
    public virtual Empregado IdParticipanteNavigation { get; set; } = null!;

    [ForeignKey("IdCurso, DtInicio")]
    [InverseProperty("TbMatriculas")]
    public virtual CursoOferecido CursoOferecido { get; set; } = null!;
}
