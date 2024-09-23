using MinimalApi.Domain.Enums;

namespace MinimalApi.Domain.DTOs;

public class AdminDTO
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public Profile Profile { get; set; } = default!;

}