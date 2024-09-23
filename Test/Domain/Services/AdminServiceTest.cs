using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Services;
using MinimalApi.Infra.Db;

namespace Test.Domain.Services;

[TestClass]
public class AdministradorServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }


    [TestMethod]
    public void TestandoSalvarAdministrador()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Admins");

        var adm = new Admin();
        adm.Name = "teste";
        adm.Email = "teste@teste.com";
        adm.Password = "teste";
        adm.Profile = "Admin";

        var administradorServico = new AdminService(context);

        // Act
        administradorServico.Create(adm);

        // Assert
        Assert.AreEqual(1, administradorServico.All(1).Count());
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Admins");

        var adm = new Admin();
        adm.Name = "teste";
        adm.Email = "teste@teste.com";
        adm.Password = "teste";
        adm.Profile = "Admin";

        var administradorServico = new AdminService(context);

        // Act
        administradorServico.Create(adm);
        var admDoBanco = administradorServico.FindById(adm.Id);

        // Assert
        Assert.AreEqual(1, admDoBanco?.Id);
    }
}