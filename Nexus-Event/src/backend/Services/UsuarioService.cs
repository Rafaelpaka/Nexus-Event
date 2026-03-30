using System.Security.Cryptography;
using System.Text;
using backend.DTOs.Usuario;
using backend.Entities;
using backend.Repositories;
using backend.Validators;

namespace backend.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _usuarioRepository;

    public UsuarioService(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioResponse> CriarUsuarioAsync(CriarUsuarioRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        UsuarioValidator.ValidarCamposObrigatorios(request);
        UsuarioValidator.ValidarEmail(request.Email);
        UsuarioValidator.ValidarSenhaForte(request.Senha);

        var usuarioExistente = await _usuarioRepository.BuscarPorEmailAsync(request.Email);

        if (usuarioExistente is not null)
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail.");

        var senhaHash = GerarHash(request.Senha);

        var usuario = new UsuarioEntity(
            nome: request.Nome,
            login: request.Login,
            senha: senhaHash,
            cpf: request.Cpf,
            email: request.Email,
            telefone: request.Telefone,
            endereco: request.Endereco
        );

        var linhasAfetadas = await _usuarioRepository.CadastrarAsync(usuario);

        if (linhasAfetadas <= 0)
            throw new Exception("Não foi possível cadastrar o usuário.");

        return new UsuarioResponse
        {
            Nome = usuario.Nome,
            Login = usuario.Login,
            Cpf = usuario.Cpf,
            Email = usuario.Email,
            Telefone = usuario.Telefone,
            Endereco = usuario.Endereco
        };
    }

    private static string GerarHash(string senha)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}