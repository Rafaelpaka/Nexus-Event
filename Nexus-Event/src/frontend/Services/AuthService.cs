using frontend.Models;

namespace frontend.Services;

public class AuthService
{
	public UsuarioLogado? UsuarioAtual { get; private set; }
	public bool EstaLogado => UsuarioAtual is not null;
	public bool EhAdmin => UsuarioAtual?.Cpf == "000.000.000-00";

	public event Action? OnChange;

	public void Logar(UsuarioLogado usuario)
	{
		UsuarioAtual = usuario;
		OnChange?.Invoke();
	}

	public void Deslogar()
	{
		UsuarioAtual = null;
		OnChange?.Invoke();
	}
}