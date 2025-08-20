using MinimalApi.Dominio.Enuns;

namespace Test.Domain.Enuns;

[TestClass]
public class PerfilTest
{
    [TestMethod]
    public void TestarValoresDoEnum()
    {
        // Arrange & Act
        var adm = Perfil.Adm;
        var editor = Perfil.Editor;

        // Assert
        Assert.AreEqual(0, (int)adm);
        Assert.AreEqual(1, (int)editor);
    }

    [TestMethod]
    public void TestarConversaoDeString()
    {
        // Arrange & Act
        var admString = "Adm";
        var editorString = "Editor";

        var adm = Enum.Parse<Perfil>(admString);
        var editor = Enum.Parse<Perfil>(editorString);

        // Assert
        Assert.AreEqual(Perfil.Adm, adm);
        Assert.AreEqual(Perfil.Editor, editor);
    }

    [TestMethod]
    public void TestarTodosOsValores()
    {
        // Arrange & Act
        var valores = Enum.GetValues<Perfil>();

        // Assert
        Assert.AreEqual(2, valores.Length);
        Assert.IsTrue(valores.Contains(Perfil.Adm));
        Assert.IsTrue(valores.Contains(Perfil.Editor));
    }
}
