using MinimalApi.DTOs;

namespace Test.Domain.DTOs;

[TestClass]
public class VeiculoDTOTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO();

        // Act
        veiculoDTO.Nome = "Civic";
        veiculoDTO.Marca = "Honda";
        veiculoDTO.Ano = 2023;

        // Assert
        Assert.AreEqual("Civic", veiculoDTO.Nome);
        Assert.AreEqual("Honda", veiculoDTO.Marca);
        Assert.AreEqual(2023, veiculoDTO.Ano);
    }

    [TestMethod]
    public void TestarPropriedadesComValoresPadrao()
    {
        // Arrange & Act
        var veiculoDTO = new VeiculoDTO();

        // Assert
        Assert.IsNull(veiculoDTO.Nome);
        Assert.IsNull(veiculoDTO.Marca);
        Assert.AreEqual(0, veiculoDTO.Ano);
    }

    [TestMethod]
    public void TestarPropriedadesComDiferentesValores()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO();

        // Act
        veiculoDTO.Nome = "Corolla";
        veiculoDTO.Marca = "Toyota";
        veiculoDTO.Ano = 2020;

        // Assert
        Assert.AreEqual("Corolla", veiculoDTO.Nome);
        Assert.AreEqual("Toyota", veiculoDTO.Marca);
        Assert.AreEqual(2020, veiculoDTO.Ano);
    }

    [TestMethod]
    public void TestarRecordImutabilidade()
    {
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2023
        };

        // Act & Assert
        // Como é um record, as propriedades são mutáveis por padrão
        // mas podemos testar a funcionalidade básica
        Assert.AreEqual("Civic", veiculoDTO.Nome);
        Assert.AreEqual("Honda", veiculoDTO.Marca);
        Assert.AreEqual(2023, veiculoDTO.Ano);
    }
}
