using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.DTO;

public class VendedorAtualizarRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3,
        ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string? Nome { get; set; }
}