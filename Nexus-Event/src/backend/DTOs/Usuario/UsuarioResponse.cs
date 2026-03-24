namespace backend.DTOs.Usuario;

public class UsuarioResponse
{
    public int IdUsuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
}
