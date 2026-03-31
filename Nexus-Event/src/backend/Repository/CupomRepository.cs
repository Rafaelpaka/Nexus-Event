using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using backend.Entities;

namespace backend.Repositories;

public class CupomRepository
{
    private readonly string _connectionString;

    public CupomRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CriarConexao() => new SqlConnection(_connectionString);

    public async Task<CupomEntity?> BuscarPorCodigo(string codigo)
    {
        using var db = CriarConexao();
        return await db.QueryFirstOrDefaultAsync<CupomEntity>(
            "SELECT * FROM Cupons WHERE Codigo = @Codigo",
            new { Codigo = codigo }
        );
    }

    public async Task<int> Cadastrar(CupomEntity cupom)
    {
        using var db = CriarConexao();
        return await db.ExecuteAsync(
            @"INSERT INTO Cupons (Codigo, PorcentagemDesconto, ValorMinimoRegra)
              VALUES (@Codigo, @PorcentagemDesconto, @ValorMinimoRegra)",
            new
            {
                cupom.Codigo,
                cupom.PorcentagemDesconto,
                cupom.ValorMinimoRegra
            }
        );
    }
}