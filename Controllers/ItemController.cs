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
    public IEnumerable<ItemDto> GetItems()
    {
        return _repository.GetItems().Select(item => item.asDto());
    }

    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetItem(Guid id)
    {
        var item = _repository.GetItem(id);
        if (item is null) return NotFound();

        return item.asDto();
    }

    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
    {
        Item item = new()
        {
            Id = Guid.NewGuid(),
            Price = createItemDto.Price,
            Name = createItemDto.Name,
            CreatedAt = DateTimeOffset.UtcNow
        };
        _repository.CreateItem(item);
        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.asDto());
    }

    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
    {
        var row = _repository.GetItem(id);
        if (row is null) return NotFound();
        var updatedItem = row with { Name = updateItemDto.Name, Price = updateItemDto.Price };
        _repository.UpdateItem(updatedItem);
        return new NoContentResult();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
        var row = _repository.GetItem(id);
        if (row is null) return NotFound();
        _repository.DeleteItem(id);
        return new NoContentResult();
    }
}