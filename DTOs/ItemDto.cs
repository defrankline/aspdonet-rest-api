namespace SuperHeroAPI.DTOs;

public record ItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}