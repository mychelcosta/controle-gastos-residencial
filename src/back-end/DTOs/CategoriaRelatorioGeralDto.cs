namespace ApiFinanceira.DTOs;

public class CategoriaRelatorioGeralDto
{
    public ICollection<CategoriaRelatorioDto> Categorias { get; set; } = [];
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }
    public decimal SaldoGeral => TotalGeralReceitas - TotalGeralDespesas;
}
