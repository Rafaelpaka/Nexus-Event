using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using backend.Entities;

namespace backend.Repositories;

public class UsuarioRepository
{
    private readonly string _connectionString;

    public UsuarioRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CriarConexao() => new SqlConnection(_connectionString);

    public async Task<UsuarioEntity?> BuscarPorCpf(string cpf)
    {
        using var db = CriarConexao();
        return await db.QueryFirstOrDefaultAsync<UsuarioEntity>(
            "SELECT * FROM Usuarios WHERE Cpf = @Cpf",
            new { Cpf = cpf }
        );
    }

    public async Task<UsuarioEntity?> BuscarPorEmailAsync(string email)
    {
        using var db = CriarConexao();
        return await db.QueryFirstOrDefaultAsync<UsuarioEntity>(
            "SELECT * FROM Usuarios WHERE Email = @Email",
            new { Email = email }
        );
    }

    public async Task<int> CadastrarAsync(UsuarioEntity usuario)
    {
        using var db = CriarConexao();
        return await db.ExecuteAsync(
            @"INSERT INTO Usuarios (Cpf, Nome, Email, Senha)
              VALUES (@Cpf, @Nome, @Email, @Senha)",
            new { usuario.Cpf, usuario.Nome, usuario.Email, usuario.Senha }
        );
    }
}
