using System;
using backend.Entities;
using Xunit;

namespace backend.Tests.Entities;

public class EventoEntityTests
{
    [Fact]
    public void CriarEvento_ComDadosValidos_DeveRetornarEvento()
    {
        // Arrange
        var evento = new EventoEntity
        {
            Nome = "Show Rock",
            CapacidadeTotal = 5000,
            DataEvento = DateTime.Now.AddDays(10),
            PrecoPadrao = 200
        };

        // Act
        var nome = evento.Nome;
        var capacidade = evento.CapacidadeTotal;
        var preco = evento.PrecoPadrao;

        // Assert
        Assert.NotNull(evento);
        Assert.Equal("Show Rock", nome);
        Assert.Equal(5000, capacidade);
        Assert.Equal(200, preco);
    }

    [Fact]
    public void CriarEvento_ComNomeVazio_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new EventoEntity
            {
                Nome = "",
                CapacidadeTotal = 5000,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = 200
            };
        });
    }

    [Fact]
    public void CriarEvento_ComCapacidadeNegativa_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new EventoEntity
            {
                Nome = "Show",
                CapacidadeTotal = -100,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = 200
            };
        });
    }

    [Fact]
    public void CriarEvento_ComCapacidadeZero_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new EventoEntity
            {
                Nome = "Show Rock",
                CapacidadeTotal = 0,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = 200
            };
        });
    }

    [Fact]
    public void CriarEvento_ComDataNoPassado_DeveAceitar()
    {
        // Arrange — entidade não valida data: regra é do EventoService
        var evento = new EventoEntity
        {
            Nome = "Evento",
            CapacidadeTotal = 5000,
            DataEvento = DateTime.Now.AddDays(-1),
            PrecoPadrao = 100
        };

        // Assert
        Assert.NotNull(evento);
        Assert.True(evento.DataEvento < DateTime.Now);
    }

    [Fact]
    public void CriarEvento_ComPrecoZero_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new EventoEntity
            {
                Nome = "Show Rock",
                CapacidadeTotal = 5000,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = 0
            };
        });
    }

    [Fact]
    public void CriarEvento_ComPrecoNegativo_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new EventoEntity
            {
                Nome = "Show Rock",
                CapacidadeTotal = 5000,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = -50
            };
        });
    }
}
