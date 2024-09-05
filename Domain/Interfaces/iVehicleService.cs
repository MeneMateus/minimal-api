using MinimalApi.Domain.Entities;
using MinimalApi.DTOs;

namespace MinimalApi.Domain.Interfaces;

interface iVehicleService
{
    List<Vehicle> ReturnAll(int? page = 1, string? nome = null, string? marca = null);
    Vehicle? FindById(int id);

    void Create(Vehicle vehicle);

    void Delete(Vehicle vehicle);

    void Update(Vehicle vehicle);
}