using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.DTOs;
using SuperHeroAPI.Entities;
using SuperHeroAPI.Extensions;
using SuperHeroAPI.Repositories;

namespace SuperHeroAPI.Controllers;

[ApiController]
[Route("api/items")]
public class ItemController : ControllerBase
{
    private readonly IItemRepository _repository;

    public ItemController(IItemRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
        return (await _repository.GetItemsAsync()).Select(item => item.asDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
    {
        var item = await _repository.GetItemAsync(id);
        if (item is null) return NotFound();

        return item.asDto();
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto createItemDto)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Price = createItemDto.Price,
            Name = createItemDto.Name,
            CreatedAt = DateTimeOffset.UtcNow
        };
        await _repository.CreateItemAsync(item);
        return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.asDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItemDto)
    {
        var row = await _repository.GetItemAsync(id);
        if (row is null) return NotFound();
        var updatedItem = row with { Name = updateItemDto.Name, Price = updateItemDto.Price };
        await _repository.UpdateItemAsync(updatedItem);
        return new NoContentResult();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id)
    {
        var row = await _repository.GetItemAsync(id);
        if (row is null) return NotFound();
        await _repository.DeleteItemAsync(id);
        return new NoContentResult();
    }
}