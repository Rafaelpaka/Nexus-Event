using System.Net.Http.Json;
using frontend.Models;

namespace frontend.Services;

public class CupomService
{
	private readonly HttpClient _http;

	public CupomService(HttpClient http)
	{
		_http = http;
	}

	public async Task<(bool sucesso, string mensagem)> Cadastrar(CriarCupomRequest request)
	{
		var response = await _http.PostAsJsonAsync("/api/cupons", request);
		var mensagem = await response.Content.ReadAsStringAsync();

		return response.IsSuccessStatusCode
			? (true, "Cupom cadastrado com sucesso!")
			: (false, mensagem);
	}

	public async Task<(bool sucesso, string mensagem)> Desativar(string codigo)
	{
		var response = await _http.PutAsync($"/api/cupons/{codigo}/desativar", null);
		var mensagem = await response.Content.ReadAsStringAsync();

		return response.IsSuccessStatusCode
			? (true, $"Cupom {codigo} desativado com sucesso!")
			: (false, mensagem);
	}
}