using backend.Entities;
using backend.Repositories;

namespace backend.Services;

public class ReservaService
{
    private readonly ReservaRepository _reservaRepo;
    private readonly UsuarioRepository _usuarioRepo;
    private readonly EventoRepository _eventoRepo;
    private readonly CupomRepository _cupomRepo;

    public ReservaService(
        ReservaRepository reservaRepo,
        UsuarioRepository usuarioRepo,
        EventoRepository eventoRepo,
        CupomRepository cupomRepo)
    {
        _reservaRepo = reservaRepo;
        _usuarioRepo = usuarioRepo;
        _eventoRepo = eventoRepo;
        _cupomRepo = cupomRepo;
    }

    public async Task<IEnumerable<ReservaEntity>> ListarPorCpf(string cpf)
    {
        return await _reservaRepo.ListarPorCpf(cpf);
    }

    public async Task<(bool sucesso, string mensagem, ReservaEntity? reserva)>
        CriarReserva(string cpf, int eventoId, string? codigoCupom)
    {
        var usuario = await _usuarioRepo.BuscarPorCpf(cpf);
        if (usuario is null)
            return (false, "Usuário não encontrado.", null);

        var evento = await _eventoRepo.BuscarPorId(eventoId);
        if (evento is null)
            return (false, "Evento não encontrado.", null);

        var reservasDoUsuario = await _reservaRepo.ContarPorCpfEEvento(cpf, eventoId);
        if (reservasDoUsuario >= 2)
            return (false, "CPF já atingiu o limite de 2 reservas para este evento.", null);

        var totalReservas = await _reservaRepo.ContarPorEvento(eventoId);
        if (totalReservas >= evento.CapacidadeTotal)
            return (false, "Evento esgotado.", null);

        decimal valorFinal = evento.PrecoPadrao;
        if (!string.IsNullOrWhiteSpace(codigoCupom))
        {
            var cupom = await _cupomRepo.BuscarPorCodigo(codigoCupom);
            if (cupom is not null)
            {
                if (!cupom.Disponibilidade)
                    return (false, "Cupom inativo.", null);

                if (cupom.LimiteUsoPorUsuario.HasValue)
                {
                    var usosCupom = await _reservaRepo.ContarUsoCupomPorCpf(cpf, codigoCupom);
                    if (usosCupom >= cupom.LimiteUsoPorUsuario.Value)
                        return (false, "Limite de uso deste cupom atingido para este CPF.", null);
                }

                if (evento.PrecoPadrao >= cupom.ValorMinimoRegra)
                {
                    valorFinal = evento.PrecoPadrao -
                        (evento.PrecoPadrao * cupom.PorcentagemDesconto / 100);
                }
            }
        }

        var reserva = new ReservaEntity
        {
            UsuarioCpf = cpf,
            EventoId = eventoId,
            CupomUtilizado = codigoCupom,
            ValorFinalPago = valorFinal,
            CodigoReserva = GerarCodigoReserva()
        };

        await _reservaRepo.Cadastrar(reserva);
        return (true, "Reserva criada com sucesso.", reserva);
    }

    public async Task<(bool sucesso, string mensagem)> Cancelar(int id, string cpf)
    {
        var reserva = await _reservaRepo.BuscarPorId(id);

        if (reserva is null)
            return (false, "Reserva não encontrada.");

        if (reserva.UsuarioCpf != cpf)
            return (false, "Esta reserva não pertence ao CPF informado.");

        await _reservaRepo.Cancelar(id);
        return (true, "Reserva cancelada com sucesso.");
    }

    public async Task<(bool sucesso, string mensagem)> Desativar(string codigo)
    {
        var cupom = await _cupomRepo.BuscarPorCodigo(codigo);
        if (cupom is null)
            return (false, "Cupom não encontrado.");

        if (!cupom.Disponibilidade)
            return (false, "Cupom já está inativo.");

        await _cupomRepo.Desativar(codigo);
        return (true, $"Cupom {codigo} desativado com sucesso.");
    }

    private static string GerarCodigoReserva()
    {
        var data = DateTime.Now.ToString("yyyyMMdd");
        var aleatorio = new Random().Next(1000, 9999);
        return $"NE-{data}-{aleatorio}";
    }
}