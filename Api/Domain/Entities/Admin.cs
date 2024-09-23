using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Domain.Entities;

public class Admin
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set; } = default!;
    public string Name { get; set; } = default!;
    [Required] [MaxLength(255)]
    public string Email { get; set; } = default!;
    [Required] [MaxLength(55)]
    public string Password { get; set; } = default!;

    public string Profile { get; set; } = default!;
}