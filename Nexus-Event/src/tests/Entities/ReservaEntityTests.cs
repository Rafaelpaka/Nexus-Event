using System;
using backend.Entities;
using Xunit;

namespace backend.Tests.Entities;

public class ReservaEntityTests
{
    [Fact]
    public void CriarReserva_ComDadosValidos_DeveRetornarReserva()
    {
        // Arrange
        var reserva = new ReservaEntity
        {
            UsuarioCpf = "12345678900",
            EventoId = 1,
            CupomUtilizado = "DESC10",
            ValorFinalPago = 100,
            NomeEvento = "Show Rock",
            NomeUsuario = "André",
            CodigoReserva = "NE-20260617-1234"
        };

        // Act
        var cpf = reserva.UsuarioCpf;
        var eventoId = reserva.EventoId;
        var cupom = reserva.CupomUtilizado;
        var valor = reserva.ValorFinalPago;
        var nomeEvento = reserva.NomeEvento;
        var nomeUsuario = reserva.NomeUsuario;

        // Assert
        Assert.NotNull(reserva);
        Assert.Equal("12345678900", cpf);
        Assert.Equal(1, eventoId);
        Assert.Equal("DESC10", cupom);
        Assert.Equal(100, valor);
        Assert.Equal("Show Rock", nomeEvento);
        Assert.Equal("André", nomeUsuario);
    }

    [Fact]
    public void CriarReserva_ComCpfVazio_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new ReservaEntity
            {
                UsuarioCpf = "",
                EventoId = 1,
                ValorFinalPago = 100
            };
        });
    }

    [Fact]
    public void CriarReserva_ComEventoIdZero_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new ReservaEntity
            {
                UsuarioCpf = "12345678900",
                EventoId = 0,
                ValorFinalPago = 100
            };
        });
    }

    [Fact]
    public void CriarReserva_ComValorZero_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new ReservaEntity
            {
                UsuarioCpf = "12345678900",
                EventoId = 1,
                ValorFinalPago = 0
            };
        });
    }

    [Fact]
    public void CriarReserva_ComValorNegativo_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new ReservaEntity
            {
                UsuarioCpf = "12345678900",
                EventoId = 1,
                ValorFinalPago = -50
            };
        });
    }

    [Fact]
    public void CriarReserva_ComNomeUsuarioVazio_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new ReservaEntity
            {
                UsuarioCpf = "12345678900",
                EventoId = 1,
                ValorFinalPago = 100,
                NomeUsuario = ""
            };
        });
    }

    [Fact]
    public void CriarReserva_ComCamposOpcionaisNulos_DeveAceitar()
    {
        // Arrange
        var reserva = new ReservaEntity
        {
            UsuarioCpf = "12345678900",
            EventoId = 1,
            ValorFinalPago = 100,
            NomeUsuario = "André",
            CupomUtilizado = null,
            NomeEvento = null
        };

        // Act
        var cupom = reserva.CupomUtilizado;
        var nomeEvento = reserva.NomeEvento;

        // Assert
        Assert.Null(cupom);
        Assert.Null(nomeEvento);
    }
}
