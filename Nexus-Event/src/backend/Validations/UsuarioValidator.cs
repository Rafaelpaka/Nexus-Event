using System.Text.RegularExpressions;
using backend.DTOs.Usuario;

namespace backend.Validators;

public static class UsuarioValidator
{
    public static void ValidarCamposObrigatorios(CriarUsuarioRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Login))
            throw new ArgumentException("O login é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.SenhaHash))
            throw new ArgumentException("A senha é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Cpf))
            throw new ArgumentException("O CPF é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("O e-mail é obrigatório.");
    }

    public static void ValidarEmail(string email)
    {
        var emailValido = Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase);

        if (!emailValido)
            throw new ArgumentException("O e-mail informado é inválido.");
    }

    public static void ValidarSenhaForte(string senha)
    {
        if (senha.Length < 8)
            throw new ArgumentException("A senha deve ter pelo menos 8 caracteres.");

        if (!senha.Any(char.IsUpper))
            throw new ArgumentException("A senha deve conter pelo menos uma letra maiúscula.");

        if (!senha.Any(char.IsLower))
            throw new ArgumentException("A senha deve conter pelo menos uma letra minúscula.");

        if (!senha.Any(char.IsDigit))
            throw new ArgumentException("A senha deve conter pelo menos um número.");

        if (!senha.Any(ch => !char.IsLetterOrDigit(ch)))
            throw new ArgumentException("A senha deve conter pelo menos um caractere especial.");
    }
}