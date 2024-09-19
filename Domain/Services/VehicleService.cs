using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Infra.Db;

namespace MinimalApi.Domain.Services;

class VehicleService : iVehicleService
{
    private readonly DbContexto _contexto;
    public VehicleService(DbContexto db)
    {
        _contexto = db;
    }

    public void Delete(Vehicle vehicle)
    {
        _contexto.Vehicles.Remove(vehicle);
        _contexto.SaveChanges();
    }

    public void Update(Vehicle vehicle)
    {
        _contexto.Update(vehicle);
        _contexto.SaveChanges();
    }

    public Vehicle? FindById(int id)
    {
        return _contexto.Vehicles.Where(v => v.Id == id).FirstOrDefault();
    }

    public List<Vehicle> ReturnAll(int? page = 1, string? nome = null, string? marca = null)
    {
        var query = _contexto.Vehicles.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(v => v.Name.Contains(nome));

        if (!string.IsNullOrEmpty(marca))
            query = query.Where(v => v.Marca.Contains(marca));

        if (page != null)
            query = query.Skip(((int)page - 1) * 10).Take(10);

        return query.ToList();
    }

    public void Create(Vehicle vehicle)
    {
        _contexto.Vehicles.Add(vehicle);
        _contexto.SaveChanges();
    }
}