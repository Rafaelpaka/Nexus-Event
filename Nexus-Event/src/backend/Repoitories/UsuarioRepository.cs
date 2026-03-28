using Dapper;
using system.Data;
using Microsoft.Data.SqlClient;
using Backend.Entities;

namespace Backend.Repositories;

public class UsuarioRepository
{
	private readonly string _connecttionString;

	public UsuarioRepository(string connectionString) {
		_connecttionString = connectionString;
	}

	private IDbConnection CriarConexao() => new SqlConnection(_connecttionString);

	public async Task<Usuario?> BuscarPorCpf(string cpf) {

		using var db = CriarConexao();
		return await db.QueryFirstOrDefaultAsync<Usuario>(
			"SELECT * FROM Usuarios Where Cpf = @Cpf",
			new { Cpf = cpf }
		);

	}

	public async Task<int> Cadastrar(Usuario usuario) {

		using var db = CriarConexao();
		return await db.ExecuteAsync(
			@"INSERT INTO Usuarios (Cpf, Nome, Email) VALUE (@Cpf, @Nome, @Email)", new { Usuario.Cpf, Usuario.Nome, Usuario.Email }
			);
	}

	






}
