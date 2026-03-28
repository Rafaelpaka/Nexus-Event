using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Backend.Entities;

namespace Backend.Repositories;

public class EventoRepository
{
    private readonly string _connectionString;

    public EventoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CriarConexao() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<Evento>> ListarTodos()
    {
        using var db = CriarConexao();
        return await db.QueryAsync<Evento>(
            "SELECT * FROM Eventos"
        );
    }

    public async Task<Evento?> BuscarPorId(int id)
    {
        using var db = CriarConexao();
        return await db.QueryFirstOrDefaultAsync<Evento>(
            "SELECT * FROM Eventos WHERE Id = @Id",
            new { Id = id }
        );
    }

    public async Task<int> Cadastrar(Evento evento)
    {
        using var db = CriarConexao();
        return await db.ExecuteAsync(
            @"INSERT INTO Eventos (Nome, CapacidadeTotal, DataEvento, PrecoPadrao)
              VALUES (@Nome, @CapacidadeTotal, @DataEvento, @PrecoPadrao)",
            new { evento.Nome, evento.CapacidadeTotal, evento.DataEvento, evento.PrecoPadrao }
        );
    }
}
