namespace frontend.Models;

public class ReservaModel
{
    public int Id { get; set; }
    public string UsuarioCpf { get; set; } = string.Empty;
    public int EventoId { get; set; }
    public string? CupomUtilizado { get; set; }
    public decimal ValorFinalPago { get; set; }
    public string? CodigoReserva { get; set; }
    public string? NomeEvento { get; set; }
    public string? NomeUsuario { get; set; }
}

public class CriarReservaRequest
{
    public string UsuarioCpf { get; set; } = string.Empty;
    public int EventoId { get; set; }
    public string? CodigoCupom { get; set; }
}