using CollectionService.Data;
using CollectionService.Models;
using CollectionService.Repositories;
using CollectionService.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Item> _repository;

        public ItemsController(AppDbContext context, IRepository<Item> repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<IEntity>?> GetAsync()
        {
            //var items = await _context.Items.ToListAsync();
          //  var items = (await _repository.GetAllItemsAsync())?.Select(p => p.AsDto());
          var items = await _repository.GetAllItemsAsync();
            return items;  
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!await _context.Items.AnyAsync(p => p.Id == id))
                return NotFound();
                
            var item = await _context.Items.Where(p => p.Id == id).SingleAsync();
            return Ok(item);  
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateItemDto createItemDto)
        {
            var tags = createItemDto.Tags;
            foreach (var some in tags)
            {
                if (! await _context.Tags.AnyAsync(p => p.Name == some))
                {
                    var tag = new Tag{
                        Name = some
                    };
                    await _context.Tags.AddAsync(tag);
                }
            }
            var item = new Item{
                Name = createItemDto.NameItem,
                Quantity = createItemDto.Quantity,
                CreatedDate = DateTimeOffset.Now

            };
           await _context.Items.AddAsync(item);
           await _context.SaveChangesAsync();

                return Ok(item);
            }

        //  [HttpPut("{id}")]
        //  public IActionResult Update(Guid id, UpdateItemDto updateItemDto)
        //  {
        //       if (!items.Any(p => p.Id == id))
        //          return NotFound();
    //
        //      var item = items.Where(p => p.Id == id).Single();
        //      var updatedItem = item with
        //      {
        //          NameItem = updateItemDto.NameItem,
        //          Tags = updateItemDto.Tags,
        //          Quantity = updateItemDto.Quantity
      //      };
      //      var index = items.FindIndex(p => p.Id == id);
      //      items[index] = updatedItem;
//
      //      return Ok();
      //  }
//
      //  [HttpDelete("{id}")]
      //  public IActionResult Remove(Guid id)
      //  {
      //      if (!items.Any(p => p.Id == id))
      //          return NotFound();
//
      //      var item = items.Where(p => p.Id == id).Single();
      //      items.Remove(item);
      //      return Ok();
      //  }
    }//
}