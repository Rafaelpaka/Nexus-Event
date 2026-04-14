using backend.Entities;
using Xunit;

namespace backend.Tests.Entities;

public class CupomEntityTests
{
    [Fact]
    public void Deve_Criar_Cupom_Com_Dados_Validos() // Caso de Sucesso
    {
        var cupom = new CupomEntity
        {
            Codigo = "DESC10",
            PorcentagemDesconto = 10,
            ValorMinimoRegra = 100,
            LimiteUsoPorUsuario = 2,
            Disponibilidade = true
        };

        Assert.NotNull(cupom);
        Assert.Equal("DESC10", cupom.Codigo);
        Assert.Equal(10, cupom.PorcentagemDesconto);
        Assert.Equal(100, cupom.ValorMinimoRegra);
        Assert.Equal(2, cupom.LimiteUsoPorUsuario);
        Assert.Equal(true, cupom.Disponibilidade);
    }

    [Fact]
    public void Nao_Deve_Criar_Cupom_Com_Codigo_Vazio()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var cupom = new CupomEntity
            {
                Codigo = "",
                PorcentagemDesconto = 10,
                ValorMinimoRegra = 100,
                LimiteUsoPorUsuario = 2,
                Disponibilidade = true
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Cupom_Com_Desconto_Zero()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var cupom = new CupomEntity
            {
                Codigo = "DESC0",
                PorcentagemDesconto = 0,
                LimiteUsoPorUsuario = 2,
                ValorMinimoRegra = 100,
                Disponibilidade = true
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Cupom_Com_Desconto_Negativo()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var cupom = new CupomEntity
            {
                Codigo = "DESC10",
                PorcentagemDesconto = -10,
                ValorMinimoRegra = 100,
                LimiteUsoPorUsuario = 2,
                Disponibilidade = true
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Cupom_Com_Desconto_Maior_Que_100()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var cupom = new CupomEntity
            {
                Codigo = "DESC200",
                PorcentagemDesconto = 101,
                ValorMinimoRegra = 100,
                LimiteUsoPorUsuario = 2,
                Disponibilidade = true
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Cupom_Com_Valor_Minimo_Negativo()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var cupom = new CupomEntity
            {
                Codigo = "DESC10",
                PorcentagemDesconto = 10,
                ValorMinimoRegra = -50,
                LimiteUsoPorUsuario = 2,
                Disponibilidade = true
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Cupom_Com_Limite_Maior_Que_2()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var cupom = new CupomEntity
            {
                Codigo = "DESC10",
                PorcentagemDesconto = 10,
                ValorMinimoRegra = 100,
                LimiteUsoPorUsuario = 3,
                Disponibilidade = true
            };
        });
    }
}
