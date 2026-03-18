namespace backend.Entities;

public class IngressoEntity
{

    public IngressoEntity(int valor, int idUsuario)
    {
        if (valor <= 0)
            throw new ArgumentOutOfRangeException(nameof(valor), "O valor deve ser maior que zero.");

        if (idUsuario <= 0)
            throw new ArgumentOutOfRangeException(nameof(idUsuario), "O ID do usuário deve ser válido.");

        IdUsuario = idUsuario;
        Valor = valor;
        DataCriação = DateTime.Now;

    }

    public int IdUsuario { get; }
    public int Id { get; set; }
    public DateTime DataCriação { get; set; }
    public string Codigo { get; set; }
    public decimal Valor { get; set; }
}
