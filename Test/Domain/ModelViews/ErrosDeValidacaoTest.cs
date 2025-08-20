using MinimalApi.Dominio.ModelViews;

namespace Test.Domain.ModelViews;

[TestClass]
public class ErrosDeValidacaoTest
{
    [TestMethod]
    public void TestarGetSetPropriedadeMensagens()
    {
        // Arrange
        var erros = new ErrosDeValidacao();
        var mensagens = new List<string> { "Campo obrigatório", "Formato inválido" };

        // Act
        erros.Mensagens = mensagens;

        // Assert
        Assert.AreEqual(2, erros.Mensagens.Count);
        Assert.AreEqual("Campo obrigatório", erros.Mensagens[0]);
        Assert.AreEqual("Formato inválido", erros.Mensagens[1]);
    }

    [TestMethod]
    public void TestarPropriedadeComValorPadrao()
    {
        // Arrange & Act
        var erros = new ErrosDeValidacao();

        // Assert
        Assert.IsNull(erros.Mensagens);
    }

    [TestMethod]
    public void TestarPropriedadeComListaVazia()
    {
        // Arrange
        var erros = new ErrosDeValidacao();

        // Act
        erros.Mensagens = new List<string>();

        // Assert
        Assert.IsNotNull(erros.Mensagens);
        Assert.AreEqual(0, erros.Mensagens.Count);
    }

    [TestMethod]
    public void TestarPropriedadeComListaNula()
    {
        // Arrange
        var erros = new ErrosDeValidacao();
        erros.Mensagens = new List<string> { "Erro 1" };

        // Act
        erros.Mensagens = null;

        // Assert
        Assert.IsNull(erros.Mensagens);
    }

    [TestMethod]
    public void TestarPropriedadeComMensagensComplexas()
    {
        // Arrange
        var erros = new ErrosDeValidacao();
        var mensagens = new List<string>
        {
            "O campo 'Email' é obrigatório",
            "O campo 'Senha' deve ter pelo menos 6 caracteres",
            "O campo 'Perfil' deve ser 'Adm' ou 'Editor'"
        };

        // Act
        erros.Mensagens = mensagens;

        // Assert
        Assert.AreEqual(3, erros.Mensagens.Count);
        Assert.IsTrue(erros.Mensagens.Any(m => m.Contains("Email")));
        Assert.IsTrue(erros.Mensagens.Any(m => m.Contains("Senha")));
        Assert.IsTrue(erros.Mensagens.Any(m => m.Contains("Perfil")));
    }
}
