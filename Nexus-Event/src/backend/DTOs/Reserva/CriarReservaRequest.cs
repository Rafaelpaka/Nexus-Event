namespace backend.DTOs.Reserva;

public class CriarReservaRequest
{
    public string UsuarioCpf { get; set; } = string.Empty;
    public int EventoId { get; set; }
    public string? CodigoCupom { get; set; }
}