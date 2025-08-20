using System.Net;
using System.Text;
using System.Text.Json;
using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class VeiculoRequestTest
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
    public async Task TestarIncluirVeiculo()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2023
        };

        var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");

        // Act
        var response = await Setup.client.PostAsync("/veiculos", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var veiculoCriado = JsonSerializer.Deserialize<Veiculo>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculoCriado);
        Assert.AreEqual("Civic", veiculoCriado.Nome);
        Assert.AreEqual("Honda", veiculoCriado.Marca);
        Assert.AreEqual(2023, veiculoCriado.Ano);
    }

    [TestMethod]
    public async Task TestarBuscarVeiculoPorId()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2022
        };

        var content = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");

        // Act - Primeiro criar um veículo
        var createResponse = await Setup.client.PostAsync("/veiculos", content);
        Assert.AreEqual(HttpStatusCode.Created, createResponse.StatusCode);

        var createResult = await createResponse.Content.ReadAsStringAsync();
        var veiculoCriado = JsonSerializer.Deserialize<Veiculo>(createResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Agora buscar por ID
        var getResponse = await Setup.client.GetAsync($"/veiculos/{veiculoCriado.Id}");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);

        var getResult = await getResponse.Content.ReadAsStringAsync();
        var veiculoBuscado = JsonSerializer.Deserialize<Veiculo>(getResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculoBuscado);
        Assert.AreEqual(veiculoCriado.Id, veiculoBuscado.Id);
        Assert.AreEqual("Corolla", veiculoBuscado.Nome);
    }

    [TestMethod]
    public async Task TestarListarVeiculos()
    {
        // Arrange & Act
        var response = await Setup.client.GetAsync("/veiculos");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var veiculos = JsonSerializer.Deserialize<List<Veiculo>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculos);
        Assert.IsTrue(veiculos.Count >= 0);
    }

    [TestMethod]
    public async Task TestarAtualizarVeiculo()
    {
        // Arrange - Primeiro criar um veículo
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Fusca",
            Marca = "Volkswagen",
            Ano = 1980
        };

        var createContent = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");
        var createResponse = await Setup.client.PostAsync("/veiculos", createContent);
        Assert.AreEqual(HttpStatusCode.Created, createResponse.StatusCode);

        var createResult = await createResponse.Content.ReadAsStringAsync();
        var veiculoCriado = JsonSerializer.Deserialize<Veiculo>(createResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Agora atualizar
        var veiculoAtualizado = new Veiculo
        {
            Id = veiculoCriado.Id,
            Nome = "Fusca Atualizado",
            Marca = "Volkswagen",
            Ano = 1985
        };

        var updateContent = new StringContent(JsonSerializer.Serialize(veiculoAtualizado), Encoding.UTF8, "application/json");

        // Act
        var updateResponse = await Setup.client.PutAsync($"/veiculos/{veiculoCriado.Id}", updateContent);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);

        var updateResult = await updateResponse.Content.ReadAsStringAsync();
        var veiculoModificado = JsonSerializer.Deserialize<Veiculo>(updateResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculoModificado);
        Assert.AreEqual("Fusca Atualizado", veiculoModificado.Nome);
        Assert.AreEqual(1985, veiculoModificado.Ano);
    }

    [TestMethod]
    public async Task TestarApagarVeiculo()
    {
        // Arrange - Primeiro criar um veículo
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Gol",
            Marca = "Volkswagen",
            Ano = 2010
        };

        var createContent = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8, "application/json");
        var createResponse = await Setup.client.PostAsync("/veiculos", createContent);
        Assert.AreEqual(HttpStatusCode.Created, createResponse.StatusCode);

        var createResult = await createResponse.Content.ReadAsStringAsync();
        var veiculoCriado = JsonSerializer.Deserialize<Veiculo>(createResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Act
        var deleteResponse = await Setup.client.DeleteAsync($"/veiculos/{veiculoCriado.Id}");

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);

        // Verificar se foi realmente apagado
        var getResponse = await Setup.client.GetAsync($"/veiculos/{veiculoCriado.Id}");
        Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}
