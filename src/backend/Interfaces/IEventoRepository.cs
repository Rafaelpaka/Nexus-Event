using backend.Entities;

namespace backend.Interfaces;

public interface IEventoRepository
{
    Task<IEnumerable<EventoEntity>> GetTodosAsync();

    Task<EventoEntity?> GetPorIdAsync(Guid id);

    Task AdicionarAsync(EventoEntity evento);

    Task AtualizarAsync(EventoEntity evento);

    Task DeletarAsync(Guid id);

    Task<IEnumerable<EventoEntity>> BuscarPorTituloAsync(string titulo);
}