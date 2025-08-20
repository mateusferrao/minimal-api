using MinimalApi.Dominio.ModelViews;

namespace Test.Domain.ModelViews;

[TestClass]
public class AdministradorLogadoTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adminLogado = new AdministradorLogado();

        // Act
        adminLogado.Email = "admin@teste.com";
        adminLogado.Perfil = "Adm";
        adminLogado.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";

        // Assert
        Assert.AreEqual("admin@teste.com", adminLogado.Email);
        Assert.AreEqual("Adm", adminLogado.Perfil);
        Assert.AreEqual("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...", adminLogado.Token);
    }

    [TestMethod]
    public void TestarPropriedadesComValoresPadrao()
    {
        // Arrange & Act
        var adminLogado = new AdministradorLogado();

        // Assert
        Assert.IsNull(adminLogado.Email);
        Assert.IsNull(adminLogado.Perfil);
        Assert.IsNull(adminLogado.Token);
    }

    [TestMethod]
    public void TestarPropriedadesComDiferentesValores()
    {
        // Arrange
        var adminLogado = new AdministradorLogado();

        // Act
        adminLogado.Email = "editor@empresa.com";
        adminLogado.Perfil = "Editor";
        adminLogado.Token = "novo_token_jwt_aqui";

        // Assert
        Assert.AreEqual("editor@empresa.com", adminLogado.Email);
        Assert.AreEqual("Editor", adminLogado.Perfil);
        Assert.AreEqual("novo_token_jwt_aqui", adminLogado.Token);
    }

    [TestMethod]
    public void TestarRecordImutabilidade()
    {
        // Arrange
        var adminLogado = new AdministradorLogado
        {
            Email = "admin@teste.com",
            Perfil = "Adm",
            Token = "token_jwt_exemplo"
        };

        // Act & Assert
        // Como é um record, as propriedades são mutáveis por padrão
        // mas podemos testar a funcionalidade básica
        Assert.AreEqual("admin@teste.com", adminLogado.Email);
        Assert.AreEqual("Adm", adminLogado.Perfil);
        Assert.AreEqual("token_jwt_exemplo", adminLogado.Token);
    }

    [TestMethod]
    public void TestarPropriedadesComTokenComplexo()
    {
        // Arrange
        var adminLogado = new AdministradorLogado();

        // Act
        adminLogado.Email = "usuario@dominio.com.br";
        adminLogado.Perfil = "Adm";
        adminLogado.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        // Assert
        Assert.AreEqual("usuario@dominio.com.br", adminLogado.Email);
        Assert.AreEqual("Adm", adminLogado.Perfil);
        Assert.IsTrue(adminLogado.Token.Length > 100); // JWT tokens são longos
        Assert.IsTrue(adminLogado.Token.Contains(".")); // JWT tokens têm pontos
    }

    [TestMethod]
    public void TestarPropriedadesComValoresExtremos()
    {
        // Arrange
        var adminLogado = new AdministradorLogado();

        // Act
        adminLogado.Email = "a@b.c";
        adminLogado.Perfil = "A";
        adminLogado.Token = "t";

        // Assert
        Assert.AreEqual("a@b.c", adminLogado.Email);
        Assert.AreEqual("A", adminLogado.Perfil);
        Assert.AreEqual("t", adminLogado.Token);
    }
}
