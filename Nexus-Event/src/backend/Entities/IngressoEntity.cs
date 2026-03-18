namespace backend.Entities.IngressoEntity;

public class IngressoEntity
{

    public IngressoEntity(int valor, int idUsuario)
    {
        if (valor <= 0)
            throw 'Não é possível a criação de um ingresso com valor igual a 0 ou menor';
    }


    public int Id { get; set; }
    public DateTime DataCriação { get; set; }
    public string Codigo { get; set; }
    public decimal Valor { get; set; }
}
