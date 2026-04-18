using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFinanceira.Models;

[Table("Transacoes")]
public class Transacao
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "É obrigatório informar a descrição."),
        MaxLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres.")]
    public required string Descricao { get; set; }

    [Required(ErrorMessage = "É obrigatório informar o valor.")]
    public required decimal Valor { get; set; }

    [Required(ErrorMessage = "É obrigatório informar o tipo."),
        EnumDataType(typeof(Tipo), ErrorMessage = "Tipo inválido.")]
    public Tipo Tipo { get; set; }

    [Required(ErrorMessage = "É obrigatório informar o ID da categoria.")]
    public int CategoriaId { get; set; }
    [ForeignKey("CategoriaId")]
    public Categoria? Categorias { get; set; }

    [Required(ErrorMessage = "É obrigatório informar o ID da pessoa.")]
    public int PessoaId { get; set; }
    [ForeignKey("PessoaId")]
    public Pessoa? Pessoa { get; set; }
}

public enum Tipo
{
    Receita = 1,
    Despesa = 2
}