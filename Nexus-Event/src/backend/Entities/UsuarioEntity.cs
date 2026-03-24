namespace backend.Entities;

public class UsuarioEntity
{
    public UsuarioEntity(
        string nome,
        string login,
        string senha,
        string cpf,
        string email,
        string? telefone = null,
        string? endereco = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nome);
        ArgumentException.ThrowIfNullOrWhiteSpace(login);
        ArgumentException.ThrowIfNullOrWhiteSpace(senha);
        ArgumentException.ThrowIfNullOrWhiteSpace(cpf);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        Nome = nome;
        Login = login;
        Senha = senha;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        Endereco = endereco;
    }

    public int IdUsuario { get; private set; }
    public string Nome { get; private set; }
    public string Login { get; private set; }
    public string Senha { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string? Telefone { get; private set; }
    public string? Endereco { get; private set; }
}
