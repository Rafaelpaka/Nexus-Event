using backend.Entities;
using backend.Repositories;
using System.Security.Cryptography;
using System.Text;

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
        // CPF do administrador padrão
        var cpfAdmin = "000.000.000-00";

        // Verifica se já existe
        var existe = await _usuarioRepository.BuscarPorCpf(cpfAdmin);
        if (existe is not null)
        {
            Console.WriteLine(" Admin já existe, seed ignorado.");
            return;
        }

        // Cria o administrador
        var admin = new UsuarioEntity
        {
            Cpf       = cpfAdmin,
            Nome      = "Administrador",
            Email     = "admin@nexusevent.com",
            Senha = GerarHash("Admin@123")
        };

        await _usuarioRepository.CadastrarAsync(admin);
        Console.WriteLine(" Admin criado com sucesso!");
        Console.WriteLine("   CPF:   000.000.000-00");
        Console.WriteLine("   Email: admin@nexusevent.com");
        Console.WriteLine("   Senha: Admin@123");
    }

    private static string GerarHash(string senha)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash  = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
