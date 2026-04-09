using System.Net.Http.Json;
using frontend.Models;

namespace frontend.Services;

public class EventoService
{
    private readonly HttpClient _http;

    public EventoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<EventoModel>> ListarTodos()
    {
        return await _http.GetFromJsonAsync<List<EventoModel>>("/api/eventos")
               ?? new List<EventoModel>();
    }

    public async Task<(bool sucesso, string mensagem)> Cadastrar(CriarEventoRequest request)
    {
        var response = await _http.PostAsJsonAsync("/api/eventos", request);
        var mensagem = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode
            ? (true, "Evento cadastrado com sucesso!")
            : (false, mensagem);
    }
}