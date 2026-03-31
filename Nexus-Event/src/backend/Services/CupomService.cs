using backend.Entities;
using backend.Repositories;

namespace backend.Services;

public class CupomService
{
    private readonly CupomRepository _repo;

    public CupomService(CupomRepository repo)
    {
        _repo = repo;
    }

    public async Task<CupomEntity?> BuscarPorCodigo(string codigo)
    {
        return await _repo.BuscarPorCodigo(codigo);
    }

    public async Task<(bool sucesso, string mensagem)> Cadastrar(CupomEntity cupom)
    {
        if (string.IsNullOrWhiteSpace(cupom.Codigo))
            return (false, "O código do cupom é obrigatório.");

        if (cupom.PorcentagemDesconto <= 0)
            return (false, "A porcentagem de desconto deve ser maior que zero.");

        if (cupom.ValorMinimoRegra < 0)
            return (false, "O valor mínimo não pode ser negativo.");

        var existente = await _repo.BuscarPorCodigo(cupom.Codigo);
        if (existente is not null)
            return (false, "Já existe um cupom com este código.");

        await _repo.Cadastrar(cupom);
        return (true, "Cupom cadastrado com sucesso.");
    }
}