using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Backend.Entities;
using backend.Entities;

namespace Backend.Repositories;

public class UsuarioRepository
{
	private readonly string _connecttionString;

	public UsuarioRepository(string connectionString) {
		_connecttionString = connectionString;
	}

	private IDbConnection CriarConexao() => new SqlConnection(_connecttionString);

	public async Task<UsuarioEntity?> BuscarPorCpf(string cpf) {

		using var db = CriarConexao();
		return await db.QueryFirstOrDefaultAsync<UsuarioEntity>(
			"SELECT * FROM Usuarios Where Cpf = @Cpf",
			new { Cpf = cpf }
		);

	}

	public async Task<int> Cadastrar(UsuarioEntity usuario) {

		using var db = CriarConexao();
		return await db.ExecuteAsync(
			@"INSERT INTO Usuarios (Cpf, Nome, Email) VALUE (@Cpf, @Nome, @Email)", new { usuario.Cpf, usuario.Nome, usuario.Email }
			);
	}

	






}
