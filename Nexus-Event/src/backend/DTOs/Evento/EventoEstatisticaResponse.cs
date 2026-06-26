namespace backend.DTOs.Evento;

public class EventoEstatisticaResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CapacidadeTotal { get; set; }
    public int TotalReservas { get; set; }
    public int VagasRestantes { get; set; }
    public decimal OcupacaoPercentual { get; set; }
    public decimal ReceitaTotal { get; set; }
    public DateTime DataEvento { get; set; }
}
