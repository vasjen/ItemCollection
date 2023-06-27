using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollectionService.Data;
using CollectionService.Models;
using CollectionService.Repositories;

namespace CollectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Collection> _repository;

        public CollectionsController(AppDbContext context, IRepository<Collection> repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Collection>> GetAsync()
        {
          return  await _repository.GetAllAsync();
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
           var item = new Collection();
           await _repository.CreateAsync(item);

                return Ok(item);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateItemDto updateItemDto)
        {
            await _repository.UpdateAsync(id);
          return Ok();
      }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
           await _repository.RemoveAsync(id);
           return Ok();
    } 
}
}