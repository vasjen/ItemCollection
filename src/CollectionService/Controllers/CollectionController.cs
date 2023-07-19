using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Common.Core.Entities;
using Microsoft.AspNetCore.Identity;
using CollectionService.Extensions;
using Common.Models;
using Common.Repositories;
using Common.EFCore;

namespace CollectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionController : ControllerBase
    {
       
        private readonly IRepository<Collection> _repository;
        private readonly ILogger<CollectionController> _logger;
        private readonly FieldCreationService _fieldCreator;

        public CollectionController(IRepository<Collection> repository, ILogger<CollectionController> logger, FieldCreationService fieldCreator)
        {
            _repository = repository;
            _logger = logger;
            _fieldCreator = fieldCreator;
        }
        
    

        

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            
          return  Ok( _repository.GetAllAsync().GetAwaiter().GetResult().Select(p => p.AsDto()));
        }
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<Collection>> GetAllUserAsync(Guid id)
        {
          return  await _repository.GetAllAsync(p => p.ApplicationUserId == id);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
           
            var collection = await _repository.GetByIdAsync(id);
            if (collection is null)
                return NotFound();
                
            return Ok(collection);  
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(CreateCollectionDto createCollectionDto)
        {
            System.Console.WriteLine($"Collection: {createCollectionDto.ApplicationUserId} => {createCollectionDto.Theme} {createCollectionDto.Theme}");
            var theme = createCollectionDto.Theme;
            var collection = new Collection(){
                CreatedTime = DateTimeOffset.Now,
                Name = createCollectionDto.NameCollection,
                Description = createCollectionDto.Description,
                Theme = createCollectionDto.Theme,
                ApplicationUserId = createCollectionDto.ApplicationUserId
           };
           await _repository.CreateAsync(collection);
           var collectionId = collection.Id;
            
                await _fieldCreator.CreateField(createCollectionDto.Fields, collectionId);
            
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