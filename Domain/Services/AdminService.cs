using System.Data.Common;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using MinimalApi.DTOs;
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
}