using backend.Entities;
using Xunit;

namespace backend.Tests.Entities;

public class ReservaEntityTests
{
    [Fact]
    public void Deve_Criar_Reserva_Com_Dados_Validos() // Caso de sucesso
    {
        var reserva = new ReservaEntity(
            UsuarioCpf: "12345678900",
            EventoId: 1,
            CupomUtilizado: "DESC10",
            ValorFinalPago: 100,
            NomeEvento: "Show Rock",
            NomeUsuario: "André"
        );

        Assert.NotNull(reserva);
        Assert.Equal("12345678900", reserva.UsuarioCpf);
        Assert.Equal(1, reserva.EventoId);
        Assert.Equal("DESC10", reserva.CupomUtilizado);
        Assert.Equal(100, reserva.ValorFinalPago);
        Assert.Equal("Show Rock", reserva.NomeEvento);
        Assert.Equal("André", reserva.NomeUsuario);
    }

    [Fact]
    public void Nao_Deve_Criar_Reserva_Com_Cpf_Vazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new ReservaEntity(
                UsuarioCpf: "",
                EventoId: 1,
                ValorFinalPago: 100,
                NomeUsuario: "André"
            ));
    }

    [Fact]
    public void Nao_Deve_Criar_Reserva_Com_Evento_Invalido()
    {
        Assert.Throws<ArgumentException>(() =>
            new ReservaEntity(
                UsuarioCpf: "12345678900",
                EventoId: 0,
                ValorFinalPago: 100,
                NomeUsuario: "André"
            ));
    }

    [Fact]
    public void Nao_Deve_Criar_Reserva_Com_Valor_Zero()
    {
        Assert.Throws<ArgumentException>(() =>
            new ReservaEntity(
                UsuarioCpf: "12345678900",
                EventoId: 1,
                ValorFinalPago: 0,
                NomeUsuario: "André"
            ));
    }

    [Fact]
    public void Nao_Deve_Criar_Reserva_Com_Valor_Negativo()
    {
        Assert.Throws<ArgumentException>(() =>
            new ReservaEntity(
                UsuarioCpf: "12345678900",
                EventoId: 1,
                ValorFinalPago: -50,
                NomeUsuario: "André"
            ));
    }

    [Fact]
    public void Nao_Deve_Criar_Reserva_Com_NomeUsuario_Vazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new ReservaEntity(
                UsuarioCpf: "12345678900",
                EventoId: 1,
                ValorFinalPago: 100,
                NomeUsuario: ""
            ));
    }

    [Fact]
    public void Deve_Aceitar_Campos_Opcionais_Nulos()
    {
        var reserva = new ReservaEntity(
                UsuarioCpf: "12345678900",
                EventoId: 1,
                ValorFinalPago: 100,
                NomeUsuario: "André",
                CupomUtilizado: null,
                NomeEvento: null
        );

        Assert.Null(reserva.CupomUtilizado);
        Assert.Null(reserva.NomeEvento);
        
    }

}
