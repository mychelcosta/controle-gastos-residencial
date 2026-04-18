using ApiFinanceira.DTOs;

namespace ApiFinanceira.DTOs;

public class PessoasRelatorioGeralDto
{
    public ICollection<PessoaRelatorioDto> Pessoas { get; set; } = [];
    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }
    public decimal SaldoGeral => TotalGeralReceitas - TotalGeralDespesas;
}
