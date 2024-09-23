using MinimalApi.Domain.Entities;

namespace Test.Domain.Entities;

[TestClass]
public class AdminTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var adm = new Admin();

        // Act

        adm.Id = 1;
        adm.Email = "admin@teste.com";
        adm.Name = "Administrador";
        adm.Password = "root";
        adm.Profile = "Admin";

        // Assert
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("admin@teste.com", adm.Email);
        Assert.AreEqual("Administrador", adm.Name);
        Assert.AreEqual("root", adm.Password);
        Assert.AreEqual("Admin", adm.Profile);
    }
}