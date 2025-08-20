using MinimalApi.DTOs;
using MinimalApi.Dominio.Enuns;

namespace Test.Domain.DTOs;

[TestClass]
public class AdministradorDTOTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adminDTO = new AdministradorDTO();

        // Act
        adminDTO.Email = "admin@teste.com";
        adminDTO.Senha = "admin123";
        adminDTO.Perfil = Perfil.Adm;

        // Assert
        Assert.AreEqual("admin@teste.com", adminDTO.Email);
        Assert.AreEqual("admin123", adminDTO.Senha);
        Assert.AreEqual(Perfil.Adm, adminDTO.Perfil);
    }

    [TestMethod]
    public void TestarPropriedadesComValoresPadrao()
    {
        // Arrange & Act
        var adminDTO = new AdministradorDTO();

        // Assert
        Assert.IsNull(adminDTO.Email);
        Assert.IsNull(adminDTO.Senha);
        Assert.IsNull(adminDTO.Perfil);
    }

    [TestMethod]
    public void TestarPropriedadesComDiferentesValores()
    {
        // Arrange
        var adminDTO = new AdministradorDTO();

        // Act
        adminDTO.Email = "editor@empresa.com";
        adminDTO.Senha = "editor123";
        adminDTO.Perfil = Perfil.Editor;

        // Assert
        Assert.AreEqual("editor@empresa.com", adminDTO.Email);
        Assert.AreEqual("editor123", adminDTO.Senha);
        Assert.AreEqual(Perfil.Editor, adminDTO.Perfil);
    }

    [TestMethod]
    public void TestarPropriedadesComPerfilNull()
    {
        // Arrange
        var adminDTO = new AdministradorDTO();

        // Act
        adminDTO.Email = "usuario@teste.com";
        adminDTO.Senha = "senha123";
        adminDTO.Perfil = null;

        // Assert
        Assert.AreEqual("usuario@teste.com", adminDTO.Email);
        Assert.AreEqual("senha123", adminDTO.Senha);
        Assert.IsNull(adminDTO.Perfil);
    }

    [TestMethod]
    public void TestarPropriedadesComEmailComplexo()
    {
        // Arrange
        var adminDTO = new AdministradorDTO();

        // Act
        adminDTO.Email = "admin.teste+tag@dominio.com.br";
        adminDTO.Senha = "Senha@123";
        adminDTO.Perfil = Perfil.Adm;

        // Assert
        Assert.AreEqual("admin.teste+tag@dominio.com.br", adminDTO.Email);
        Assert.AreEqual("Senha@123", adminDTO.Senha);
        Assert.AreEqual(Perfil.Adm, adminDTO.Perfil);
    }
}
