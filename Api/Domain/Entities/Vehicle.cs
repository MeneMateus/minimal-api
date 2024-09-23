using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Domain.Entities;

public class Vehicle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set; } = default!;
    [Required] [MaxLength(25)]
    public string Name { get; set; } = default!;
    [Required] [MaxLength(25)]
    public string Marca { get; set; } = default!;
    [Required]
    public int Ano { get; set; } = default!;
}