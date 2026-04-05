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
}