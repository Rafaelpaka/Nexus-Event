using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using backend.Entities;

namespace backend.Repositories;

public class ReservaRepository
{
    private readonly string _connectionString;

    public ReservaRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CriarConexao() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<ReservaEntity>> ListarPorCpf(string cpf)
    {
        using var db = CriarConexao();
        return await db.QueryAsync<ReservaEntity>(
            @"SELECT
                R.Id,
                R.UsuarioCpf,
                R.EventoId,
                R.CupomUtilizado,
                R.ValorFinalPago,
                R.CodigoReserva,
                E.Nome AS NomeEvento,
                U.Nome AS NomeUsuario
              FROM Reservas R
              INNER JOIN Eventos  E ON R.EventoId   = E.Id
              INNER JOIN Usuarios U ON R.UsuarioCpf = U.Cpf
              WHERE R.UsuarioCpf = @Cpf",
            new { Cpf = cpf }
        );
    }

    public async Task<ReservaEntity?> BuscarPorId(int id)
    {
        using var db = CriarConexao();
        return await db.QueryFirstOrDefaultAsync<ReservaEntity>(
            "SELECT * FROM Reservas WHERE Id = @Id",
            new { Id = id }
        );
    }

    public async Task<int> ContarPorCpfEEvento(string cpf, int eventoId)
    {
        using var db = CriarConexao();
        return await db.ExecuteScalarAsync<int>(
            @"SELECT COUNT(*) FROM Reservas
              WHERE UsuarioCpf = @Cpf
              AND   EventoId   = @EventoId",
            new { Cpf = cpf, EventoId = eventoId }
        );
    }

    public async Task<int> ContarPorEvento(int eventoId)
    {
        using var db = CriarConexao();
        return await db.ExecuteScalarAsync<int>(
            @"SELECT COUNT(*) FROM Reservas
              WHERE EventoId = @EventoId",
            new { EventoId = eventoId }
        );
    }

    public async Task<int> ContarUsoCupomPorCpf(string cpf, string codigoCupom)
    {
        using var db = CriarConexao();
        return await db.ExecuteScalarAsync<int>(
            @"SELECT COUNT(*) FROM Reservas
              WHERE UsuarioCpf     = @Cpf
              AND   CupomUtilizado = @CupomUtilizado",
            new { Cpf = cpf, CupomUtilizado = codigoCupom }
        );
    }

    public async Task<int> Cadastrar(ReservaEntity reserva)
    {
        using var db = CriarConexao();
        return await db.ExecuteAsync(
            @"INSERT INTO Reservas
                (UsuarioCpf, EventoId, CupomUtilizado, ValorFinalPago, CodigoReserva)
              VALUES
                (@UsuarioCpf, @EventoId, @CupomUtilizado, @ValorFinalPago, @CodigoReserva)",
            new
            {
                reserva.UsuarioCpf,
                reserva.EventoId,
                reserva.CupomUtilizado,
                reserva.ValorFinalPago,
                reserva.CodigoReserva
            }
        );
    }

    public async Task<int> Cancelar(int id)
    {
        using var db = CriarConexao();
        return await db.ExecuteAsync(
            "DELETE FROM Reservas WHERE Id = @Id",
            new { Id = id }
        );
    }
}