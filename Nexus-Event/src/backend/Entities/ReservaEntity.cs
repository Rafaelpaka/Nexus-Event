namespace backend.Entities;

public class ReservaEntity
{
    private string _usuarioCpf = string.Empty;
    private int _eventoId;
    private decimal _valorFinalPago;
    private string? _nomeUsuario;

    public int Id { get; set; }

    public string UsuarioCpf
    {
        get => _usuarioCpf;
        set => _usuarioCpf = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("CPF do usuário é obrigatório.");
    }

    public int EventoId
    {
        get => _eventoId;
        set => _eventoId = value > 0
            ? value
            : throw new ArgumentException("EventoId deve ser maior que zero.");
    }

    public string? CupomUtilizado { get; set; }

    public decimal ValorFinalPago
    {
        get => _valorFinalPago;
        set => _valorFinalPago = value > 0
            ? value
            : throw new ArgumentException("Valor final pago deve ser maior que zero.");
    }

    public string? CodigoReserva { get; set; }

    public string? NomeEvento { get; set; }

    public string? NomeUsuario
    {
        get => _nomeUsuario;
        set => _nomeUsuario = value is null || !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Nome do usuário é obrigatório.");
    }
}