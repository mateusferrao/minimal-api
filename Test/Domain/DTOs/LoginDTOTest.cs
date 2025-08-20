using MinimalApi.DTOs;

namespace Test.Domain.DTOs;

[TestClass]
public class LoginDTOTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var loginDTO = new LoginDTO();

        // Act
        loginDTO.Email = "teste@teste.com";
        loginDTO.Senha = "senha123";

        // Assert
        Assert.AreEqual("teste@teste.com", loginDTO.Email);
        Assert.AreEqual("senha123", loginDTO.Senha);
    }

    [TestMethod]
    public void TestarPropriedadesComValoresPadrao()
    {
        // Arrange & Act
        var loginDTO = new LoginDTO();

        // Assert
        Assert.IsNull(loginDTO.Email);
        Assert.IsNull(loginDTO.Senha);
    }

    [TestMethod]
    public void TestarPropriedadesComDiferentesValores()
    {
        // Arrange
        var loginDTO = new LoginDTO();

        // Act
        loginDTO.Email = "admin@empresa.com";
        loginDTO.Senha = "admin123";

        // Assert
        Assert.AreEqual("admin@empresa.com", loginDTO.Email);
        Assert.AreEqual("admin123", loginDTO.Senha);
    }

    [TestMethod]
    public void TestarPropriedadesComEmailComplexo()
    {
        // Arrange
        var loginDTO = new LoginDTO();

        // Act
        loginDTO.Email = "usuario.teste+tag@dominio.com.br";
        loginDTO.Senha = "Senha@123";

        // Assert
        Assert.AreEqual("usuario.teste+tag@dominio.com.br", loginDTO.Email);
        Assert.AreEqual("Senha@123", loginDTO.Senha);
    }
}
