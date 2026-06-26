using backend.Entities;
using backend.Repositories;
using backend.Utils;

namespace backend.Services;

public class SeedService
{
    private readonly UsuarioRepository _usuarioRepository;

    public SeedService(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task CriarAdminSeNaoExistir()
    {
        string? cpfAdmin = Environment.GetEnvironmentVariable("ADMIN_CPF");
        string? emailAdmin = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        string? senhaAdmin = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

        if (string.IsNullOrWhiteSpace(cpfAdmin) ||
            string.IsNullOrWhiteSpace(emailAdmin) ||
            string.IsNullOrWhiteSpace(senhaAdmin))
        {
            throw new InvalidOperationException("Variáveis de ambiente do admin não configuradas.");
        }

        var existe = await _usuarioRepository.BuscarPorCpf(cpfAdmin);
        if (existe is not null)
        {
            Console.WriteLine("Admin já existe, seed ignorado.");
            return;
        }

        var admin = new UsuarioEntity
        {
            Cpf       = cpfAdmin,
            Nome      = "Administrador",
            Email     = emailAdmin,
            SenhaHash = HashUtils.GerarHash(senhaAdmin)
        };

        await _usuarioRepository.CadastrarAsync(admin);

        Console.WriteLine("Admin criado com sucesso!");
        Console.WriteLine($"CPF:   {cpfAdmin}");
        Console.WriteLine($"Email: {emailAdmin}");
        Console.WriteLine("Senha: (oculta por segurança)");
    }
}
