using MinimalApi.Domain.Entities;
using MinimalApi.Domain.DTOs;

namespace MinimalApi.Domain.Interfaces;

interface iAdminService
{
    Admin? Login(LoginDTO loginDTO);

    Admin? Create(Admin admin);

    List<Admin> All(int? page);

    Admin? FindById(int id);
}