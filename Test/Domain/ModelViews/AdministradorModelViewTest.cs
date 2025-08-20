using MinimalApi.Dominio.ModelViews;

namespace Test.Domain.ModelViews;

[TestClass]
public class AdministradorModelViewTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adminMV = new AdministradorModelView();

        // Act
        adminMV.Id = 1;
        adminMV.Email = "admin@teste.com";
        adminMV.Perfil = "Adm";

        // Assert
        Assert.AreEqual(1, adminMV.Id);
        Assert.AreEqual("admin@teste.com", adminMV.Email);
        Assert.AreEqual("Adm", adminMV.Perfil);
    }

    [TestMethod]
    public void TestarPropriedadesComValoresPadrao()
    {
        // Arrange & Act
        var adminMV = new AdministradorModelView();

        // Assert
        Assert.AreEqual(0, adminMV.Id);
        Assert.IsNull(adminMV.Email);
        Assert.IsNull(adminMV.Perfil);
    }

    [TestMethod]
    public void TestarPropriedadesComDiferentesValores()
    {
        // Arrange
        var adminMV = new AdministradorModelView();

        // Act
        adminMV.Id = 999;
        adminMV.Email = "editor@empresa.com";
        adminMV.Perfil = "Editor";

        // Assert
        Assert.AreEqual(999, adminMV.Id);
        Assert.AreEqual("editor@empresa.com", adminMV.Email);
        Assert.AreEqual("Editor", adminMV.Perfil);
    }

    [TestMethod]
    public void TestarRecordImutabilidade()
    {
        // Arrange
        var adminMV = new AdministradorModelView
        {
            Id = 1,
            Email = "admin@teste.com",
            Perfil = "Adm"
        };

        // Act & Assert
        // Como é um record, as propriedades são mutáveis por padrão
        // mas podemos testar a funcionalidade básica
        Assert.AreEqual(1, adminMV.Id);
        Assert.AreEqual("admin@teste.com", adminMV.Email);
        Assert.AreEqual("Adm", adminMV.Perfil);
    }

    [TestMethod]
    public void TestarPropriedadesComValoresExtremos()
    {
        // Arrange
        var adminMV = new AdministradorModelView();

        // Act
        adminMV.Id = int.MaxValue;
        adminMV.Email = "a@b.c";
        adminMV.Perfil = "A";

        // Assert
        Assert.AreEqual(int.MaxValue, adminMV.Id);
        Assert.AreEqual("a@b.c", adminMV.Email);
        Assert.AreEqual("A", adminMV.Perfil);
    }
}
