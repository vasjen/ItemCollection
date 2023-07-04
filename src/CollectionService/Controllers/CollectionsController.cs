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
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<Collection>> GetAllUserAsync(Guid id)
        {
          return  await _repository.GetAllAsync(p => p.ApplicationUserId == id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            System.Console.WriteLine("Recivied id is {0}",id);
            var collection = await _repository.GetByIdAsync(id);
            if (collection is null)
                return NotFound();
                
            return Ok(collection);  
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCollectionDto createCollectionDto)
        {
            var theme = createCollectionDto.Theme;
            var collection = new Collection(){
                CreatedTime = DateTimeOffset.Now,
                Name = createCollectionDto.NameCollection,
                Description = createCollectionDto.Description,
                Theme = theme,
           };
           await _repository.CreateAsync(collection);

                return Ok(collection);
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