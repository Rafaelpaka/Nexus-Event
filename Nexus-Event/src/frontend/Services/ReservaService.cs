using System.Net.Http.Json;
using frontend.Models;

namespace frontend.Services;

public class ReservaService
{
    private readonly HttpClient _http;

    public ReservaService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ReservaModel>> ListarPorCpf(string cpf)
    {
        return await _http.GetFromJsonAsync<List<ReservaModel>>($"/api/reservas/{cpf}")
               ?? new List<ReservaModel>();
    }

    public async Task<(bool sucesso, string mensagem)> Criar(CriarReservaRequest request)
    {
        var response = await _http.PostAsJsonAsync("/api/reservas", request);
        var mensagem = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode
            ? (true, "Reserva criada com sucesso!")
            : (false, mensagem);
    }

    public async Task<(bool sucesso, string mensagem)> Cancelar(int id, string cpf)
    {
        var response = await _http.DeleteAsync($"/api/reservas/{id}/{cpf}");
        var mensagem = await response.Content.ReadAsStringAsync();

        return response.IsSuccessStatusCode
            ? (true, "Reserva cancelada com sucesso!")
            : (false, mensagem);
    }
}