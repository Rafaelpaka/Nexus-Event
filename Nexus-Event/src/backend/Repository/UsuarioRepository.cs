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

    public async Task<List<UsuarioEntity>> ListarAsync()
    {
        using var db = CriarConexao();
        return (await db.QueryAsync<UsuarioEntity>("SELECT * FROM Usuarios")).ToList();
    }

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
            @"INSERT INTO Usuarios (Cpf, Nome, Email, SenhaHash)
              VALUES (@Cpf, @Nome, @Email, @SenhaHash)",
            new { usuario.Cpf, usuario.Nome, usuario.Email, usuario.SenhaHash }
        );
    }

    public async Task<int> AtualizarAsync(UsuarioEntity usuario)
    {
        using var db = CriarConexao();
        return await db.ExecuteAsync(
            @"UPDATE Usuarios SET Nome = @Nome, Email = @Email, SenhaHash = @SenhaHash
              WHERE Cpf = @Cpf",
            new { usuario.Nome, usuario.Email, usuario.SenhaHash, usuario.Cpf }
        );
    }

    public async Task<int> DeletarAsync(string cpf)
    {
        using var db = CriarConexao();
        return await db.ExecuteAsync(
            "DELETE FROM Usuarios WHERE Cpf = @Cpf",
            new { Cpf = cpf }
        );
    }
}
