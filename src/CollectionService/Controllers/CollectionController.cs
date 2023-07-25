using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Common.Core.Entities;
using Microsoft.AspNetCore.Identity;
using CollectionService.Extensions;
using Common.Core.Entities;
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
        private readonly AppDbContext _context;

        public CollectionController(
            IRepository<Collection> repository, 
            ILogger<CollectionController> logger, 
            FieldCreationService fieldCreator,
            AppDbContext context)
        {
            _repository = repository;
            _logger = logger;
            _fieldCreator = fieldCreator;
            _context = context;
        }
        
    

        

        [HttpGet]
        public async Task<IEnumerable<Collection>> GetAsync()
        {
            
            return await _repository.GetAllAsync();
           
        }
        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<Collection>> GetAllUserAsync(Guid id)
        {
          return  await _repository.GetAllAsync(p => p.ApplicationUserId == id);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetFields(Guid id)
        {
          var result = await _context.Fields
                    .Where(p => p.CollectionId == id)
                    .Include(p => p.FieldBool)
                    .Include(p => p.FieldDate)
                    .Include(p => p.FieldInt)
                    .Include(p => p.FieldString)
                    .Include(p => p.FieldText)
                    .SingleAsync();
            
            System.Console.WriteLine("Bool: {0}", result.FieldBool.Count);
            foreach (var item in result.FieldBool)
            {
                System.Console.WriteLine(item.Name);
            }
            System.Console.WriteLine("Int: {0}", result.FieldInt.Count);
            foreach (var item in result.FieldInt)
            {
                System.Console.WriteLine(item.Name);
            }
            System.Console.WriteLine("Date: {0}", result.FieldDate.Count);
            foreach (var item in result.FieldDate)
            {
                System.Console.WriteLine(item.Name);
            }
            System.Console.WriteLine("string: {0}", result.FieldString.Count);
            foreach (var item in result.FieldString)
            {
                System.Console.WriteLine(item.Name);
            }
            return  Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<Collection> GetByIdAsync(Guid id)
        {
           
            var collection = _context.Collections
                            .Where(p => p.Id == id)
                            .Include(p => p.Fields)
                                .ThenInclude(f => f.FieldBool)
                            .Include(p => p.Fields)
                                .ThenInclude(f => f.FieldInt)
                            .Include(p => p.Fields)
                                .ThenInclude(f => f.FieldString)
                            .Include(p => p.Fields)
                                .ThenInclude(f => f.FieldDate)
                            .Include(p => p.Fields)
                                .ThenInclude(f => f.FieldText)
                            .Include(p => p.Items)
                            .Single();
                
            return collection;  
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> CreateAsync(CreateCollectionDto createCollectionDto)
        {   
            System.Console.WriteLine("In controller ->");
            foreach (var item in createCollectionDto.Fields)
            {
                System.Console.WriteLine("Data from result in createCollectionDto.result: \n {0} - {1} \n", item.Key, item.Value);
            }

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
            
                return Ok(collection.Id);
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