namespace backend.DTOs.Cupom;

public class CriarCupomRequest
{
    public string Codigo { get; set; } = string.Empty;
    public decimal PorcentagemDesconto { get; set; }
    public decimal ValorMinimoRegra { get; set; }
    public int? LimiteUsoPorUsuario { get; set; } 
    public bool Disponibilidade { get; set; }       
}