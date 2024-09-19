using System.Data.Common;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Domain.DTOs;
using MinimalApi.Infra.Db;

namespace MinimalApi.Domain.Services;

class AdminService : iAdminService
{
    private readonly DbContexto _contexto;
    public AdminService(DbContexto db)
    {
        _contexto = db;
    }
    public Admin? Login(LoginDTO loginDTO)
    {
        return _contexto.Admins.FirstOrDefault(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password);
    }

    public Admin? Create(Admin admin)
    {
        _contexto.Admins.Add(admin);
        _contexto.SaveChanges();

        return admin;
    }

    public List<Admin> All(int? page)
    {
        return _contexto.Admins.ToList();
    }

    public Admin? FindById(int id)
    {
        return _contexto.Admins.Find(id);
    }
}