namespace backend.Entities;

public class UsuarioEntity
{

public UsuarioEntity(string nome, string login, string senha, string cpf, string email, string? telefone = null, string? endereco = null)
    {
        // Validação de nulos ou vazios para campos obrigatórios
        ArgumentException.ThrowIfNullOrEmpty(nome);
        ArgumentException.ThrowIfNullOrEmpty(login);
        ArgumentException.ThrowIfNullOrEmpty(senha);
        ArgumentException.ThrowIfNullOrEmpty(cpf);
        ArgumentException.ThrowIfNullOrEmpty(email);

        Nome = nome;
        Login = login;
        Senha = senha;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        Endereco = endereco;
    }

    public int idUsuario { get; private set; }
    public string name {get; set; } = string.Empty;
    public string login {get; private set; } = string.Empty;
    public string senha {get; private set; } = string.Empty;
    public string cpf {get; private set; } = string.Empty;
    public string email {get; set; } = string.Empty;
    public string? telefone {get; set; } = string.Empty;
    public string? endereco {get; set; } = string.Empty;

}