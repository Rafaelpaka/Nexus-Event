using frontend.Models;

namespace frontend.Services;

public class AuthService
{
    public UsuarioLogado? UsuarioAtual { get; private set; }
    public bool EstaLogado => UsuarioAtual is not null;
    public bool EhAdmin => UsuarioAtual?.Cpf is "000.000.000-00" or "00000000000";

    public event Action? OnChange;

    public void Logar(UsuarioLogado usuario)
    {
        UsuarioAtual = usuario;
        NotificarMudanca();
    }

    public void Deslogar()
    {
        UsuarioAtual = null;
        NotificarMudanca();
    }

    public void NotificarMudanca()
    {
        OnChange?.Invoke();
    }
}
