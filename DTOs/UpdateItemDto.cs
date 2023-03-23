using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.DTOs;

public class UpdateItemDto
{
    [Required] public string Name { get; init; } = string.Empty;

    [Required] [Range(0, 100000)] public decimal Price { get; init; }
}