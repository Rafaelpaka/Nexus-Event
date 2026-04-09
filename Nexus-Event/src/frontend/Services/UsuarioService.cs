using System.Net.Http.Json;
using frontend.Models;

namespace frontend.Services;

public class UsuarioService
{
    private readonly HttpClient _http;

    public UsuarioService(HttpClient http)
    {
        _http = http;
    }

    public async Task<(bool sucesso, string mensagem)> Cadastrar(CriarUsuarioRequest request)
    {
        var response = await _http.PostAsJsonAsync("/api/usuarios", request);
        var mensagem = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode
            ? (true, "Usuário cadastrado com sucesso!")
            : (false, mensagem);
    }

    public async Task<(bool sucesso, string mensagem, UsuarioLogado? usuario)> Login(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("/api/usuarios/login", request);

        if (response.IsSuccessStatusCode)
        {
            var usuario = await response.Content.ReadFromJsonAsync<UsuarioLogado>();
            return (true, "Login realizado com sucesso!", usuario);
        }

        var mensagem = await response.Content.ReadAsStringAsync();
        return (false, mensagem, null);
    }
}