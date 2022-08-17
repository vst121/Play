using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Small HP", 40, System.DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Atitode", "Anti mati", 10, System.DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Sword", "Sword Cutter", 90, System.DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        [HttpGet("{id}")]
        public ItemDto GetById(Guid id)
        {
            var item = items.Where(i => i.Id == id).SingleOrDefault();
            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto dto)
        {
            var item = new ItemDto(Guid.NewGuid(), dto.Name, dto.Description, dto.Price, DateTimeOffset.UtcNow);
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Where(i => i.Id == id).SingleOrDefault();

            var updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingItem => existingItem.Id == id);

            items[index] = updatedItem;

            return NoContent();


        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var existingItem = items.Where(i => i.Id == id).SingleOrDefault();

            // var index = items.FindIndex(existingItem => existingItem.Id == id);

            items.Remove(existingItem);

            return NoContent();
        }
    }


}