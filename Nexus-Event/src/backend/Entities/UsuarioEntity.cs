namespace backend.Entities;

public class UsuarioEntity
{
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Login { get; set; }
    public string? Senha { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
}