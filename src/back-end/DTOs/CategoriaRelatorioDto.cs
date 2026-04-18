namespace ApiFinanceira.DTOs;

public class CategoriaRelatorioDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Finalidade { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo => TotalReceitas - TotalDespesas;
}
