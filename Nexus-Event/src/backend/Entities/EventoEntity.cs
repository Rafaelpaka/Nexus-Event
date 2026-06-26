namespace backend.Entities;

public class EventoEntity
{
    private string _nome = string.Empty;
    private int _capacidadeTotal;
    private decimal _precoPadrao;

    public int Id { get; set; }

    public string Nome
    {
        get => _nome;
        set => _nome = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Nome é obrigatório.");
    }

    public int CapacidadeTotal
    {
        get => _capacidadeTotal;
        set => _capacidadeTotal = value > 0
            ? value
            : throw new ArgumentException("Capacidade total deve ser maior que zero.");
    }

    // DataEvento não valida passado aqui: a regra é do EventoService.Cadastrar().
    // Dapper lê eventos antigos do banco sem quebrar.
    public DateTime DataEvento { get; set; }

    public decimal PrecoPadrao
    {
        get => _precoPadrao;
        set => _precoPadrao = value > 0
            ? value
            : throw new ArgumentException("O preço deve ser maior que zero.");
    }

    public string? ImagemUrl { get; set; }
}