namespace ApiFinanceira.DTOs;

public class CategoriaCriadaDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Finalidade { get; set; } = string.Empty;
}
