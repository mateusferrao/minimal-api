using MinimalApi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 1;
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2023;

        // Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Civic", veiculo.Nome);
        Assert.AreEqual("Honda", veiculo.Marca);
        Assert.AreEqual(2023, veiculo.Ano);
    }

    [TestMethod]
    public void TestarPropriedadesComValoresPadrao()
    {
        // Arrange & Act
        var veiculo = new Veiculo();

        // Assert
        Assert.AreEqual(0, veiculo.Id);
        Assert.IsNull(veiculo.Nome);
        Assert.IsNull(veiculo.Marca);
        Assert.AreEqual(0, veiculo.Ano);
    }

    [TestMethod]
    public void TestarPropriedadesComDiferentesValores()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 999;
        veiculo.Nome = "Corolla";
        veiculo.Marca = "Toyota";
        veiculo.Ano = 2020;

        // Assert
        Assert.AreEqual(999, veiculo.Id);
        Assert.AreEqual("Corolla", veiculo.Nome);
        Assert.AreEqual("Toyota", veiculo.Marca);
        Assert.AreEqual(2020, veiculo.Ano);
    }
}
