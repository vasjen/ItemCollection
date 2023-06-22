using Microsoft.AspNetCore.Mvc;

namespace CollectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private static List<ItemDto> items = new(){
            new ItemDto(Guid.NewGuid(), "FirstItem", new string[ ]{"first","second"}, 1, DateTimeOffset.Now),
            new ItemDto(Guid.NewGuid(), "SecondItem", new string[ ]{"third","fouth"}, 2, DateTimeOffset.Now),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;  
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (!items.Any(p => p.Id == id))
                return NotFound();
                
            var item = items.Where(p => p.Id == id).FirstOrDefault();
            return Ok(item);  
        }

        [HttpPost]
        public IActionResult Create(CreateItemDto createItemDto)
        {
            var item = new ItemDto (
                Guid.NewGuid(),
                createItemDto.NameItem,
                createItemDto.Tags,
                createItemDto.Quantity,
                DateTimeOffset.Now
            );
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new {id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateItemDto updateItemDto)
        {
             if (!items.Any(p => p.Id == id))
                return NotFound();

            var item = items.Where(p => p.Id == id).Single();
            var updatedItem = item with
            {
                NameItem = updateItemDto.NameItem,
                Tags = updateItemDto.Tags,
                Quantity = updateItemDto.Quantity
            };
            var index = items.FindIndex(p => p.Id == id);
            items[index] = updatedItem;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            if (!items.Any(p => p.Id == id))
                return NotFound();

            var item = items.Where(p => p.Id == id).Single();
            items.Remove(item);
            return Ok();
        }
    }
}