using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Backend.Entities;
using backend.Entities;

namespace Backend.Repositories;

public class EventoRepository
{
    private readonly string _connectionString;

    public EventoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CriarConexao() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<EventoEntity>> ListarTodos()
    {
        using var db = CriarConexao();
        return await db.QueryAsync<EventoEntity>(
            "SELECT * FROM Eventos"
        );
    }

    public async Task<EventoEntity?> BuscarPorId(int id)
    {
        using var db = CriarConexao();
        return await db.QueryFirstOrDefaultAsync<EventoEntity>(
            "SELECT * FROM Eventos WHERE Id = @Id",
            new { Id = id }
        );
    }
    public async Task<int> Cadastrar(EventoEntity evento)
    {
        using var db = CriarConexao();
        return await db.ExecuteAsync(
            @"INSERT INTO Eventos (Titulo, Descricao, Data, Horario, CapacidadeMaxima, Valor, Setor, DuracaoAproximada, LimitePorPessoa)
              VALUES (@Titulo, @Descricao, @Data, @Horario, @CapacidadeMaxima, @Valor, @Setor, @DuracaoAproximada, @LimitePorPessoa)",
            new { evento.Titulo, evento.Descricao, evento.Data, evento.Horario, evento.CapacidadeMaxima, evento.Valor, evento.Setor, evento.DuracaoAproximada, evento.LimitePorPessoa }
        );
    }
}
