namespace backend.Entities;

public class CupomEntity
{
    private string _codigo = string.Empty;
    private decimal _porcentagemDesconto;
    private decimal _valorMinimoRegra;
    private int? _limiteUsoPorUsuario;

    public string Codigo
    {
        get => _codigo;
        set => _codigo = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("O código do cupom é obrigatório.");
    }

    public decimal PorcentagemDesconto
    {
        get => _porcentagemDesconto;
        set => _porcentagemDesconto = value is > 0 and <= 100
            ? value
            : throw new ArgumentException("A porcentagem de desconto deve ser maior que zero e no máximo 100.");
    }

    public decimal ValorMinimoRegra
    {
        get => _valorMinimoRegra;
        set => _valorMinimoRegra = value >= 0
            ? value
            : throw new ArgumentException("O valor mínimo não pode ser negativo.");
    }

    public int? LimiteUsoPorUsuario
    {
        get => _limiteUsoPorUsuario;
        set => _limiteUsoPorUsuario = value is null || value <= 2
            ? value
            : throw new ArgumentException("O limite de uso por usuário não pode ser maior que 2.");
    }

    public bool Disponibilidade { get; set; }
}