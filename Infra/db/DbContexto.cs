using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Infra.Db;

class DbContexto : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IConfiguration _configAppSettings;
    public DbContexto(IConfiguration configAppSettings)
    {
        _configAppSettings = configAppSettings;
    }
    public DbSet<Admin> Admins { get; set; } = default!;
    public DbSet<Vehicle> Vehicles { get; set; } = default!;

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Admin>().HasData(
        new Admin
        {
            Id = 1,
            Email = "admin@teste.com",
            Password = "root",
            Name = "Administrador",
            Profile = "Admin"
        }
    );
}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var stringConnection = _configAppSettings.GetConnectionString("mysql") ?? string.Empty;
            if (!string.IsNullOrEmpty(stringConnection))
            {
                optionsBuilder.UseMySql(stringConnection, new MySqlServerVersion(new Version(8, 0, 26)));
            }
        }
    }
}