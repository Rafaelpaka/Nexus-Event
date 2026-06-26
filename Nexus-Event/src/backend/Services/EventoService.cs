using backend.DTOs.Evento;
using backend.Entities;
using backend.Repositories;

namespace backend.Services;

public class EventoService
{
    private readonly EventoRepository _repo;

    public EventoService(EventoRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<EventoEntity>> ListarTodos()
    {
        return await _repo.ListarTodos();
    }

    public async Task<EventoEntity?> BuscarPorId(int id)
    {
        return await _repo.BuscarPorId(id);
    }

    public async Task<(bool sucesso, string mensagem)> Cadastrar(EventoEntity evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nome))
            return (false, "O nome do evento é obrigatório.");

        if (evento.CapacidadeTotal <= 0)
            return (false, "A capacidade total deve ser maior que zero.");

        if (evento.PrecoPadrao < 0)
            return (false, "O preço não pode ser negativo.");

        if (evento.DataEvento < DateTime.Now)
            return (false, "A data do evento não pode estar no passado.");

        await _repo.Cadastrar(evento);
        return (true, "Evento cadastrado com sucesso.");
    }

    // NOVO: Estatísticas com regras de negócio
    public async Task<(bool sucesso, string mensagem, IEnumerable<EventoEstatisticaResponse>? dados)> ObterEstatisticas()
    {
        var dados = await _repo.ObterEstatisticas();
        if (dados is null || !dados.Any())
            return (false, "Nenhum evento encontrado.", null);

        return (true, "Estatísticas obtidas com sucesso.", dados);
    }

    // NOVO: Pesquisa avançada com validações
    public async Task<(bool sucesso, string mensagem, IEnumerable<EventoEntity>? resultados)> Pesquisar(PesquisarEventoRequest filtros)
    {
        // Validação 1: Período de datas consistente
        if (filtros.DataInicio.HasValue && filtros.DataFim.HasValue &&
            filtros.DataInicio.Value > filtros.DataFim.Value)
            return (false, "A data de início não pode ser maior que a data de fim.", null);

        // Validação 2: Preço mínimo não pode ser maior que máximo
        if (filtros.PrecoMinimo.HasValue && filtros.PrecoMaximo.HasValue &&
            filtros.PrecoMinimo.Value > filtros.PrecoMaximo.Value)
            return (false, "O preço mínimo não pode ser maior que o preço máximo.", null);

        // Validação 3: Preço não pode ser negativo
        if ((filtros.PrecoMinimo.HasValue && filtros.PrecoMinimo.Value < 0) ||
            (filtros.PrecoMaximo.HasValue && filtros.PrecoMaximo.Value < 0))
            return (false, "Os preços não podem ser negativos.", null);

        var resultados = await _repo.Pesquisar(filtros);
        return (true, "Pesquisa realizada com sucesso.", resultados);
    }
}
