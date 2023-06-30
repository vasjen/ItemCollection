using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollectionService.Data;
using CollectionService.Models;
using CollectionService.Repositories;
using CollectionService.Extensions;

namespace CollectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IRepository<Tag> _tagsRepository;
        private readonly IUsersRepository<ApplicationUser> _usersRepository;

        public ItemsController(IRepository<Item> itemsRepository, IRepository<Tag> tagsRepository, IUsersRepository<ApplicationUser> usersRepository)
        {
            _itemsRepository = itemsRepository;
            _tagsRepository = tagsRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
          var result = await _itemsRepository.GetAllAsync();
            return Ok(result.Select(p => p.AsDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var item = await _itemsRepository.GetByIdAsync(id);
            if (item is null)
                return NotFound();
        
            return Ok(item.AsDto());  
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateItemDto createItemDto)
        {
            var user = await _usersRepository.GetUserAsync("asd");
            List<Tag> tags = new List<Tag>();
            foreach (var entity in createItemDto.Tags)
            {
                tags.Add(
                    new Tag{
                        Name = entity
                    });  
            }
            var item = new Item{
                Name = createItemDto.NameItem,
                CreatedTime = DateTimeOffset.Now,
                ApplicationUserId = user.Id
            };
            item.Tags.AddRange(tags);
           await _itemsRepository.CreateAsync(item);

                return Ok(item);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateItemDto updateItemDto)
        {
            await _itemsRepository.UpdateAsync(id);
          return Ok();
      }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
           await _itemsRepository.RemoveAsync(id);
           return Ok();
    } 
}
}