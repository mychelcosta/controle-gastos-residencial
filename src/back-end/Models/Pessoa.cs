using System.ComponentModel.DataAnnotations;

namespace ApiFinanceira.Models;

public class Pessoa
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "É obrigatório informar o nome."),
        MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "É obrigatório informar a idade.")]
    public required int Idade { get; set; }

    public ICollection<Transacao> Transacoes { get; set; } = [];
}
