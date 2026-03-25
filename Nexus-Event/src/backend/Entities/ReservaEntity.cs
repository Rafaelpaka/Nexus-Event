namespace Backend.Entities;

public class ReservaEntity
{
    public int Id { get; private set; }
    public string UsuarioCpf { get; private set; }
    public int EventoId { get; private set; }
    public string? CupomUtilizado { get; private set; }
    public decimal ValorFinalPago { get; private set; }

    public virtual UsuarioEntity Usuario { get; private set; }
    public virtual EventoEntity Evento { get; private set; }
    public virtual ICollection<IngressoEntity> Ingressos { get; private set; }

    protected ReservaEntity() { }

    public ReservaEntity(string usuarioCpf, int eventoId, decimal valorFinalPago, string? cupomUtilizado = null)
    {
        if (string.IsNullOrWhiteSpace(usuarioCpf))
            throw new ArgumentException("O CPF do usuário é obrigatório.");

        if (eventoId <= 0)
            throw new ArgumentException("O ID do evento deve ser válido.");

        if (valorFinalPago < 0)
            throw new ArgumentException("O valor final não pode ser negativo.");

        UsuarioCpf = usuarioCpf;
        EventoId = eventoId;
        ValorFinalPago = valorFinalPago;
        CupomUtilizado = cupomUtilizado;
        Ingressos = new List<IngressoEntity>();
    }
}
