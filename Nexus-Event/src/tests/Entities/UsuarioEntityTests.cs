using System;
using backend.Entities;
using Xunit;

namespace backend.Tests.Entities;

public class UsuarioEntityTests
{
    [Fact]
    public void CriarUsuario_ComDadosValidos_DeveRetornarUsuario()
    {
        // Arrange
        var usuario = new UsuarioEntity
        {
            Nome = "Larissa",
            Login = "larissa01",
            SenhaHash = "Senha123!",
            Cpf = "12345678900",
            Email = "larissa@email.com",
            Telefone = "21999999999",
            Endereco = "Rua A"
        };

        // Act
        var nome = usuario.Nome;
        var login = usuario.Login;
        var senha = usuario.SenhaHash;
        var cpf = usuario.Cpf;
        var email = usuario.Email;
        var telefone = usuario.Telefone;
        var endereco = usuario.Endereco;

        // Assert
        Assert.NotNull(usuario);
        Assert.Equal("Larissa", nome);
        Assert.Equal("larissa01", login);
        Assert.Equal("Senha123!", senha);
        Assert.Equal("12345678900", cpf);
        Assert.Equal("larissa@email.com", email);
        Assert.Equal("21999999999", telefone);
        Assert.Equal("Rua A", endereco);
    }

    [Fact]
    public void CriarUsuario_ComNomeVazio_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new UsuarioEntity
            {
                Nome = "",
                Login = "larissa01",
                SenhaHash = "Senha123!",
                Cpf = "12345678900",
                Email = "larissa@email.com"
            };
        });
    }

    [Fact]
    public void CriarUsuario_ComLoginVazio_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new UsuarioEntity
            {
                Nome = "Larissa",
                Login = "",
                SenhaHash = "Senha123!",
                Cpf = "12345678900",
                Email = "larissa@email.com"
            };
        });
    }

    [Fact]
    public void CriarUsuario_ComSenhaVazia_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new UsuarioEntity
            {
                Nome = "Larissa",
                Login = "larissa01",
                SenhaHash = "",
                Cpf = "12345678900",
                Email = "larissa@email.com"
            };
        });
    }

    [Fact]
    public void CriarUsuario_ComCpfVazio_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new UsuarioEntity
            {
                Nome = "Larissa",
                Login = "larissa01",
                SenhaHash = "Senha123!",
                Cpf = "",
                Email = "larissa@email.com"
            };
        });
    }

    [Fact]
    public void CriarUsuario_ComEmailVazio_DeveLancarExcecao()
    {
        // Arrange
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new UsuarioEntity
            {
                Nome = "Larissa",
                Login = "larissa01",
                SenhaHash = "Senha123!",
                Cpf = "12345678900",
                Email = ""
            };
        });
    }

    [Fact]
    public void CriarUsuario_ComTelefoneEEnderecoNulos_DeveAceitar()
    {
        // Arrange
        var usuario = new UsuarioEntity
        {
            Nome = "Larissa",
            Login = "larissa01",
            SenhaHash = "Senha123!",
            Cpf = "12345678900",
            Email = "larissa@email.com",
            Telefone = null,
            Endereco = null
        };

        // Act
        var telefone = usuario.Telefone;
        var endereco = usuario.Endereco;

        // Assert
        Assert.Null(telefone);
        Assert.Null(endereco);
    }
}
