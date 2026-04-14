using backend.Entities;
using Xunit;

namespace backend.Tests.Entities;

public class EventoEntityTests
{
    [Fact]
    public void Deve_Criar_Evento_Com_Dados_Validos() // Caso de Sucesso
    {
        var evento = new EventoEntity
        {
            Nome = "Show Rock",
            CapacidadeTotal = 5000,
            DataEvento = DateTime.Now.AddDays(10),
            PrecoPadrao = 200
        };

        Assert.NotNull(evento);
        Assert.Equal("Show Rock", evento.Nome);
        Assert.Equal(5000, evento.CapacidadeTotal);
        Assert.Equal(200, evento.PrecoPadrao);
    }

    [Fact]
    public void Nao_Deve_Criar_Evento_Com_Nome_Vazio()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var evento = new EventoEntity
            {
                Nome = "",
                CapacidadeTotal = 5000,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = 200
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Evento_Com_Capacidade_Negativa()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var evento = new EventoEntity
            {
                Nome = "Show",
                CapacidadeTotal = -100,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = 200
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Evento_Com_Capacidade_Invalida()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var evento = new EventoEntity
            {
                Nome = "Show Rock",
                CapacidadeTotal = 0,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = 200
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Evento_Com_Data_No_Passado()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var evento = new EventoEntity
            {
                Nome = "Evento",
                CapacidadeTotal = 5000,
                DataEvento = DateTime.Now.AddDays(-1),
                PrecoPadrao = 100
            };
        });
    }

    [Fact]
    public void Nao_Deve_Criar_Evento_Com_Preco_Invalido()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var evento = new EventoEntity
            {
                Nome = "Show Rock",
                CapacidadeTotal = 5000,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = 0
            };
        });
    }
    
    [Fact]
    public void Nao_Deve_Criar_Evento_Com_Preco_Negativo()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var evento = new EventoEntity
            {
                Nome = "Show Rock",
                CapacidadeTotal = 5000,
                DataEvento = DateTime.Now.AddDays(10),
                PrecoPadrao = -50
            };
        });
    }
   
}
