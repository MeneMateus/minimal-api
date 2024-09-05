using MinimalApi.Domain.Entities;
using MinimalApi.DTOs;

namespace MinimalApi.Domain.Interfaces;

interface iAdminService
{
    Admin? Login(LoginDTO loginDTO);
}