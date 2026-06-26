using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using backend.Entities;
using backend.DTOs.Evento;

namespace backend.Repositories;

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
            @"INSERT INTO Eventos (Nome, CapacidadeTotal, DataEvento, PrecoPadrao, ImagemUrl)
              VALUES (@Nome, @CapacidadeTotal, @DataEvento, @PrecoPadrao, @ImagemUrl)",
            new
            {
                evento.Nome,
                evento.CapacidadeTotal,
                evento.DataEvento,
                evento.PrecoPadrao,
                evento.ImagemUrl
            }
        );
    }

    // NOVO: Estatísticas com JOIN (Item 2 da AV2)
    public async Task<IEnumerable<EventoEstatisticaResponse>> ObterEstatisticas()
    {
        using var db = CriarConexao();
        return await db.QueryAsync<EventoEstatisticaResponse>(
            @"SELECT
                E.Id,
                E.Nome,
                E.CapacidadeTotal,
                E.DataEvento,
                COALESCE(SUM(R.ValorFinalPago), 0) AS ReceitaTotal,
                COUNT(R.Id) AS TotalReservas,
                (E.CapacidadeTotal - COUNT(R.Id)) AS VagasRestantes,
                ROUND((CAST(COUNT(R.Id) AS FLOAT) / E.CapacidadeTotal) * 100, 2) AS OcupacaoPercentual
              FROM Eventos E
              LEFT JOIN Reservas R ON E.Id = R.EventoId
              GROUP BY E.Id, E.Nome, E.CapacidadeTotal, E.DataEvento
              ORDER BY E.DataEvento"
        );
    }

    // NOVO: Pesquisa avançada com filtros
    public async Task<IEnumerable<EventoEntity>> Pesquisar(PesquisarEventoRequest filtros)
    {
        using var db = CriarConexao();

        var sql = @"SELECT E.* FROM Eventos E WHERE 1=1";
        var parameters = new DynamicParameters();

        if (!string.IsNullOrWhiteSpace(filtros.Nome))
        {
            sql += " AND E.Nome LIKE @Nome";
            parameters.Add("Nome", $"%{filtros.Nome}%");
        }

        if (filtros.DataInicio.HasValue)
        {
            sql += " AND E.DataEvento >= @DataInicio";
            parameters.Add("DataInicio", filtros.DataInicio.Value);
        }

        if (filtros.DataFim.HasValue)
        {
            sql += " AND E.DataEvento <= @DataFim";
            parameters.Add("DataFim", filtros.DataFim.Value);
        }

        if (filtros.PrecoMinimo.HasValue)
        {
            sql += " AND E.PrecoPadrao >= @PrecoMinimo";
            parameters.Add("PrecoMinimo", filtros.PrecoMinimo.Value);
        }

        if (filtros.PrecoMaximo.HasValue)
        {
            sql += " AND E.PrecoPadrao <= @PrecoMaximo";
            parameters.Add("PrecoMaximo", filtros.PrecoMaximo.Value);
        }

        if (filtros.ApenasComVagas == true)
        {
            sql += @" AND E.CapacidadeTotal > (
                        SELECT COUNT(*) FROM Reservas R WHERE R.EventoId = E.Id
                     )";
        }

        sql += " ORDER BY E.DataEvento";

        return await db.QueryAsync<EventoEntity>(sql, parameters);
    }
}
