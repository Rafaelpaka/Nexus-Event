namespace backend.DTOs.Evento;

public class PesquisarEventoRequest
{
    public string? Nome { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public decimal? PrecoMinimo { get; set; }
    public decimal? PrecoMaximo { get; set; }
    public bool? ApenasComVagas { get; set; }
}
