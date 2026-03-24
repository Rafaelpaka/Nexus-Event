using backend.Entities;
using Xunit;

namespace backend.Tests.Entities;

public class UsuarioEntityTests
{
    [Fact]
    public void Deve_Criar_Usuario_Com_Dados_Validos() // Caso de Sucesso
    {
        var usuario = new UsuarioEntity(
            nome: "Larissa",
            login: "larissa01",
            senha: "Senha123!",
            cpf: "12345678900",
            email: "larissa@email.com",
            telefone: "21999999999",
            endereco: "Rua A"
        );

        Assert.NotNull(usuario);
        Assert.Equal("Larissa", usuario.Nome);
        Assert.Equal("larissa01", usuario.Login);
        Assert.Equal("Senha123!", usuario.Senha);
        Assert.Equal("12345678900", usuario.Cpf);
        Assert.Equal("larissa@email.com", usuario.Email);
        Assert.Equal("21999999999", usuario.Telefone);
        Assert.Equal("Rua A", usuario.Endereco);
    }

    [Fact]
    public void Nao_Deve_Criar_Usuario_Com_Nome_Vazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new UsuarioEntity(
                nome: "",
                login: "larissa01",
                senha: "Senha123!",
                cpf: "12345678900",
                email: "larissa@email.com"
            ));
    }

    [Fact]
    public void Nao_Deve_Criar_Usuario_Com_Login_Vazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new UsuarioEntity(
                nome: "Larissa",
                login: "",
                senha: "Senha123!",
                cpf: "12345678900",
                email: "larissa@email.com"
            ));
    }

    [Fact]
    public void Nao_Deve_Criar_Usuario_Com_Senha_Vazia()
    {
        Assert.Throws<ArgumentException>(() =>
            new UsuarioEntity(
                nome: "Larissa",
                login: "larissa01",
                senha: "",
                cpf: "12345678900",
                email: "larissa@email.com"
            ));
    }

    [Fact]
    public void Nao_Deve_Criar_Usuario_Com_Cpf_Vazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new UsuarioEntity(
                nome: "Larissa",
                login: "larissa01",
                senha: "Senha123!",
                cpf: "",
                email: "larissa@email.com"
            ));
    }

    [Fact]
    public void Nao_Deve_Criar_Usuario_Com_Email_Vazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new UsuarioEntity(
                nome: "Larissa",
                login: "larissa01",
                senha: "Senha123!",
                cpf: "12345678900",
                email: ""
            ));
    }

    [Fact]
    public void Deve_Aceitar_Telefone_E_Endereco_Nulos()
    {
        var usuario = new UsuarioEntity(
            nome: "Larissa",
            login: "larissa01",
            senha: "Senha123!",
            cpf: "12345678900",
            email: "larissa@email.com",
            telefone: null,
            endereco: null
        );

        Assert.Null(usuario.Telefone);
        Assert.Null(usuario.Endereco);
    }
}
