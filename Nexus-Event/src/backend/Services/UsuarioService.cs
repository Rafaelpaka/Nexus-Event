using backend.Entities;
using backend.Repositories;
using backend.Utils;

namespace backend.Services;

public class UsuarioService
{
    private readonly UsuarioRepository _usuarioRepository;

    public UsuarioService(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<UsuarioEntity>> ListarUsuariosAsync()
    {
        return await _usuarioRepository.ListarAsync();
    }

    public async Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(request.Cpf))
            throw new ArgumentException("CPF é obrigatório.");
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email é obrigatório.");

        var cpfExistente = await _usuarioRepository.BuscarPorCpf(request.Cpf);
        if (cpfExistente is not null)
            throw new InvalidOperationException("Já existe um usuário com este CPF.");

        var emailExistente = await _usuarioRepository.BuscarPorEmailAsync(request.Email);
        if (emailExistente is not null)
            throw new InvalidOperationException("Já existe um usuário com este e-mail.");

        if (!string.IsNullOrWhiteSpace(request.SenhaHash))
            request.SenhaHash = HashUtils.GerarHash(request.SenhaHash);

        await _usuarioRepository.CadastrarAsync(request);
        return request;
    }

    public async Task<UsuarioEntity?> BuscarPorEmailAsync(string email)
    {
        return await _usuarioRepository.BuscarPorEmailAsync(email);
    }

    public async Task<UsuarioEntity?> BuscarPorCpfAsync(string cpf)
    {
        return await _usuarioRepository.BuscarPorCpf(cpf);
    }

    public async Task<UsuarioEntity?> AtualizarUsuarioAsync(UsuarioEntity request)
    {
        var existente = await _usuarioRepository.BuscarPorCpf(request.Cpf);
        if (existente is null)
            throw new InvalidOperationException("Usuário não encontrado.");

        existente.Nome = request.Nome ?? existente.Nome;
        existente.Email = request.Email ?? existente.Email;
        existente.Telefone = request.Telefone ?? existente.Telefone;
        existente.Endereco = request.Endereco ?? existente.Endereco;

        if (!string.IsNullOrWhiteSpace(request.SenhaHash))
            existente.SenhaHash = HashUtils.GerarHash(request.SenhaHash);

        await _usuarioRepository.AtualizarAsync(existente);
        return existente;
    }

    public async Task DeletarUsuarioAsync(string cpf)
    {
        var existente = await _usuarioRepository.BuscarPorCpf(cpf);
        if (existente is null)
            throw new InvalidOperationException("Usuário não encontrado.");

        await _usuarioRepository.DeletarAsync(cpf);
    }

    // NOVO: Login movido para o Service (antes estava no endpoint)
    public async Task<(bool sucesso, string mensagem, UsuarioEntity? usuario)> LoginAsync(string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            return (false, "Email e senha são obrigatórios.", null);

        var usuario = await _usuarioRepository.BuscarPorEmailAsync(email);
        if (usuario is null)
            return (false, "Usuário não encontrado.", null);

        var hash = HashUtils.GerarHash(senha);
        if (usuario.SenhaHash != hash)
            return (false, "Senha incorreta.", null);

        return (true, "Login realizado com sucesso.", usuario);
    }
}
