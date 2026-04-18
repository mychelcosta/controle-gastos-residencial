namespace ApiFinanceira.DTOs;

public class TransacaoCriadaDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int PessoaId { get; set; }
    public int CategoriaId { get; set; }
}