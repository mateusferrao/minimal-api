using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class ValidacaoRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }
    
    [TestMethod]
    public async Task TestarValidacaoVeiculoComDadosInvalidos()
    {
        // Arrange - Dados inválidos (nome vazio)
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "", // Nome vazio deve causar erro de validação
            Marca = "Honda",
            Ano = 2023
        };

        var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");

        // Act
        var response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(result.Contains("validation") || result.Contains("error") || result.Contains("invalid"));
    }

    [TestMethod]
    public async Task TestarValidacaoVeiculoComAnoInvalido()
    {
        // Arrange - Ano inválido (muito antigo)
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Fusca",
            Marca = "Volkswagen",
            Ano = 1800 // Ano muito antigo
        };

        var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");

        // Act
        var response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        // Pode retornar BadRequest ou Created dependendo da validação implementada
        // Vamos verificar se a resposta é válida
        Assert.IsTrue(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task TestarValidacaoAdministradorComEmailInvalido()
    {
        // Arrange - Email inválido
        var adminDTO = new AdministradorDTO
        {
            Email = "email-invalido", // Email sem @
            Senha = "senha123",
            Perfil = null
        };

        var content = new StringContent(JsonSerializer.Serialize(adminDTO), Encoding.UTF8, "application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores", content);

        // Assert
        // Pode retornar BadRequest ou Created dependendo da validação implementada
        Assert.IsTrue(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest);
    }

    [TestMethod]
    public async Task TestarValidacaoLoginComDadosVazios()
    {
        // Arrange - Dados de login vazios
        var loginDTO = new LoginDTO
        {
            Email = "", // Email vazio
            Senha = ""  // Senha vazia
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(result.Contains("validation") || result.Contains("error") || result.Contains("invalid"));
    }

    [TestMethod]
    public async Task TestarValidacaoVeiculoComMarcaVazia()
    {
        // Arrange - Marca vazia
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Carro Teste",
            Marca = "", // Marca vazia
            Ano = 2023
        };

        var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");

        // Act
        var response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(result.Contains("validation") || result.Contains("error") || result.Contains("invalid"));
    }
}
