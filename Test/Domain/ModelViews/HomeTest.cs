using MinimalApi.Dominio.ModelViews;

namespace Test.Domain.ModelViews;

[TestClass]
public class HomeTest
{
    [TestMethod]
    public void TestarPropriedadeMensagem()
    {
        // Arrange & Act
        var home = new Home();

        // Assert
        Assert.AreEqual("Bem vindo a API de veículos - Minimal API", home.Mensagem);
    }

    [TestMethod]
    public void TestarPropriedadeDoc()
    {
        // Arrange & Act
        var home = new Home();

        // Assert
        Assert.AreEqual("/swagger", home.Doc);
    }

    [TestMethod]
    public void TestarEstruturaImutavel()
    {
        // Arrange & Act
        var home = new Home();

        // Assert
        // Como é uma struct, as propriedades são readonly (get-only)
        Assert.IsNotNull(home.Mensagem);
        Assert.IsNotNull(home.Doc);
        Assert.IsTrue(home.Mensagem.Length > 0);
        Assert.IsTrue(home.Doc.Length > 0);
    }

    [TestMethod]
    public void TestarValoresFixos()
    {
        // Arrange & Act
        var home1 = new Home();
        var home2 = new Home();

        // Assert
        // Como são propriedades get-only com valores fixos, devem ser sempre iguais
        Assert.AreEqual(home1.Mensagem, home2.Mensagem);
        Assert.AreEqual(home1.Doc, home2.Doc);
    }
}
