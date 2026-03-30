namespace backend.Entities;

public class CupomEntity
{
    public int Id { get; private set; }
    public string Codigo { get; private set; } = string.Empty;
    public decimal ValorDesconto { get; private set; }
    public decimal ValorMinimoCompra { get; private set; }
    public int LimiteUsoPorUsuario { get; private set; }
    public bool Ativo { get; private set; }
    
    // Controle de Disponibilidade
    public DateTime DataInicio { get; private set; }
    public DateTime DataExpiracao { get; private set; }
    public int QuantidadeTotalDisponivel { get; private set; }

    // Construtor para criação manual
    public CupomEntity(
        string codigo, 
        decimal valorDesconto, 
        decimal valorMinimoCompra, 
        int limiteUsoPorUsuario, 
        DateTime dataInicio, 
        DateTime dataExpiracao, 
        int quantidadeTotalDisponivel)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("O código do cupom não pode ser vazio.");
            
        if (valorDesconto <= 0)
            throw new ArgumentException("O valor do desconto deve ser maior que zero.");

        if (dataExpiracao <= dataInicio)
            throw new ArgumentException("A data de expiração deve ser posterior à data de início.");

        Codigo = codigo.ToUpper(); // Padroniza cupons em maiúsculas
        ValorDesconto = valorDesconto;
        ValorMinimoCompra = valorMinimoCompra;
        LimiteUsoPorUsuario = limiteUsoPorUsuario;
        DataInicio = dataInicio;
        DataExpiracao = dataExpiracao;
        QuantidadeTotalDisponivel = quantidadeTotalDisponivel;
        Ativo = true;
    }

    // Construtor vazio para o EF Core
    protected CupomEntity() { }

    // Método de negócio para verificar se o cupom é válido hoje
    public bool EstaValido()
    {
        var agora = DateTime.Now;
        return Ativo && agora >= DataInicio && agora <= DataExpiracao;
    }
}