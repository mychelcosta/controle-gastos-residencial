using System.ComponentModel.DataAnnotations;

namespace ApiFinanceira.Models;

public class Categoria
{
    [Key]
     public int Id { get; set; }

    [Required(ErrorMessage = "É obrigatório informar a descrição."),
        MaxLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres.")]
     public required string Descricao { get; set; }

    [Required(ErrorMessage = "É obrigatório informar a finalidade."),
        EnumDataType(typeof(Finalidade), ErrorMessage = "Finalidade inválida.")]
     public Finalidade Finalidade { get; set; }
}

public enum Finalidade
{
    Receita = 1,
    Despesa = 2,
    Ambas = 3
}