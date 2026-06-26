namespace backend.Entities;

public class UsuarioEntity
{
    private string _cpf = string.Empty;
    private string _nome = string.Empty;
    private string _email = string.Empty;
    private string? _login;
    private string? _senhaHash;

    public string Cpf
    {
        get => _cpf;
        set => _cpf = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("CPF é obrigatório.");
    }

    public string Nome
    {
        get => _nome;
        set => _nome = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Nome é obrigatório.");
    }

    public string Email
    {
        get => _email;
        set => _email = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Email é obrigatório.");
    }

    public string? Login
    {
        get => _login;
        set => _login = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Login é obrigatório.");
    }

    public string? SenhaHash
    {
        get => _senhaHash;
        set => _senhaHash = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Senha é obrigatória.");
    }

    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
}