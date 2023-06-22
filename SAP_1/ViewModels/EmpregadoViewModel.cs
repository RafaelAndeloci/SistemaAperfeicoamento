using System.ComponentModel.DataAnnotations;
using SAP_1.Models;

namespace SAP_1.ViewModels;

public class EmpregadoViewModel
{
    public Empregado Empregado { get; set; } = new Empregado();

    [Required]
    [MaxLength(60)]
    public string? Comentario { get; set; } = string.Empty;
}