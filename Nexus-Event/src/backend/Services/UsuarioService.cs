using System.Security.Cryptography;
using System.Text;
using backend.Entities;
using backend.Repositories;

namespace backend.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _usuarioRepository;

    public UsuarioService(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Cpf))
            throw new ArgumentException("CPF é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email é obrigatório.");

        var existente = await _usuarioRepository.BuscarPorEmailAsync(request.Email);
        if (existente is not null)
            throw new InvalidOperationException("Já existe um usuário com este e-mail.");

        if (!string.IsNullOrWhiteSpace(request.Senha))
            request.Senha = GerarHash(request.Senha);

        await _usuarioRepository.CadastrarAsync(request);
        return request;
    }

    private static string GerarHash(string senha)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}