using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Servicos;

[TestClass]
public class VeiculoServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }

    [TestMethod]
    public void TestandoSalvarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2023;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);

        // Assert
        Assert.AreEqual(1, veiculoServico.Todos(1).Count());
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2023;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.AreEqual(1, veiculoDoBanco?.Id);
        Assert.AreEqual("Civic", veiculoDoBanco?.Nome);
    }

    [TestMethod]
    public void TestandoAtualizarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2023;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);
        veiculo.Nome = "Civic Atualizado";
        veiculoServico.Atualizar(veiculo);

        var veiculoAtualizado = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.AreEqual("Civic Atualizado", veiculoAtualizado?.Nome);
    }

    [TestMethod]
    public void TestandoApagarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo = new Veiculo();
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2023;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo);
        veiculoServico.Apagar(veiculo);

        var veiculoRemovido = veiculoServico.BuscaPorId(veiculo.Id);

        // Assert
        Assert.IsNull(veiculoRemovido);
    }

    [TestMethod]
    public void TestandoBuscaComFiltroPorNome()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo1 = new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2023 };
        var veiculo2 = new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2022 };

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo1);
        veiculoServico.Incluir(veiculo2);

        var veiculosFiltrados = veiculoServico.Todos(1, "Civic");

        // Assert
        Assert.AreEqual(1, veiculosFiltrados.Count);
        Assert.AreEqual("Civic", veiculosFiltrados.First().Nome);
    }

    [TestMethod]
    public void TestandoBuscaComFiltroPorMarca()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculo1 = new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2023 };
        var veiculo2 = new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2022 };

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo1);
        veiculoServico.Incluir(veiculo2);

        var veiculosFiltrados = veiculoServico.Todos(1, null, "Honda");

        // Assert
        Assert.AreEqual(1, veiculosFiltrados.Count);
        Assert.AreEqual("Honda", veiculosFiltrados.First().Marca);
    }

    [TestMethod]
    public void TestandoPaginacao()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

        var veiculoServico = new VeiculoServico(context);

        // Criar mais de 10 veículos para testar paginação
        for (int i = 1; i <= 15; i++)
        {
            var veiculo = new Veiculo
            {
                Nome = $"Veiculo{i}",
                Marca = $"Marca{i}",
                Ano = 2020 + i
            };
            veiculoServico.Incluir(veiculo);
        }

        // Act
        var primeiraPagina = veiculoServico.Todos(1);
        var segundaPagina = veiculoServico.Todos(2);

        // Assert
        Assert.AreEqual(10, primeiraPagina.Count);
        Assert.AreEqual(5, segundaPagina.Count);
    }
}
