using System;
using backend.Entities;
using Xunit;

namespace backend.Tests.Entities;

public class CupomEntityTests
{
    [Fact]
    public void CriarCupom_ComDadosValidos_DeveRetornarCupom()
    {
        // Arrange
        var cupom = new CupomEntity
        {
            Codigo = "DESC10",
            PorcentagemDesconto = 10,
            ValorMinimoRegra = 100,
            LimiteUsoPorUsuario = 2,
            Disponibilidade = true
        };

        // Act
        var codigo = cupom.Codigo;
        var desconto = cupom.PorcentagemDesconto;
        var valorMinimo = cupom.ValorMinimoRegra;
        var limite = cupom.LimiteUsoPorUsuario;
        var disponivel = cupom.Disponibilidade;

        // Assert
        Assert.NotNull(cupom);
        Assert.Equal("DESC10", codigo);
        Assert.Equal(10, desconto);
        Assert.Equal(100, valorMinimo);
        Assert.Equal(2, limite);
        Assert.True(disponivel);
    }

    [Fact]
    public void CriarCupom_ComCodigoVazio_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new CupomEntity
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
    public void CriarCupom_ComDescontoZero_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new CupomEntity
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
    public void CriarCupom_ComDescontoNegativo_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new CupomEntity
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
    public void CriarCupom_ComDescontoMaiorQueCem_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new CupomEntity
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
    public void CriarCupom_ComValorMinimoNegativo_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new CupomEntity
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
    public void CriarCupom_ComLimiteMaiorQueDois_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new CupomEntity
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
