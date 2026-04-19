namespace ApiFinanceira.DTOs;

public class TransacaoCriarDto
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public int Tipo { get; set; }
    public int PessoaId { get; set; }
    public int CategoriaId { get; set; }
}