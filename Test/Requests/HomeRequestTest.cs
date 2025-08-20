using System.Net;
using System.Text.Json;
using MinimalApi.Dominio.ModelViews;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class HomeRequestTest
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
    public async Task TestarEndpointHome()
    {
        // Arrange & Act
        var response = await Setup.client.GetAsync("/");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var home = JsonSerializer.Deserialize<Home>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(home);
        Assert.AreEqual("Bem vindo a API de veículos - Minimal API", home.Mensagem);
        Assert.AreEqual("/swagger", home.Doc);
    }

    [TestMethod]
    public async Task TestarEndpointSwagger()
    {
        // Arrange & Act
        var response = await Setup.client.GetAsync("/swagger");

        // Assert
        // O Swagger pode retornar diferentes status codes dependendo da configuração
        // Mas deve retornar algo (não deve ser 404)
        Assert.AreNotEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod]
    public async Task TestarEndpointNaoExistente()
    {
        // Arrange & Act
        var response = await Setup.client.GetAsync("/endpoint-inexistente");

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
