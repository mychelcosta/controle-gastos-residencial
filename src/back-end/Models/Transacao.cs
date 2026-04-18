using System.ComponentModel.DataAnnotations;

namespace ApiFinanceira.Models;

public class Transacao
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "É obrigatório informar a descrição."),
        MaxLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres.")]
    public required string Descricao { get; set; }

    [Required(ErrorMessage = "É obrigatório informar o valor.")]
    public required decimal Valor { get; set; }

    public Tipo Tipo { get; set; }
}

public enum Tipo
{
    Receita = 1,
    Despesa = 2
}