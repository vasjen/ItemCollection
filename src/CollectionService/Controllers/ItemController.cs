using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollectionService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Common.Repositories;
using Common.Core.Entities;

namespace CollectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IRepository<Comment> _commentsRepository;
        // private readonly IUsersRepository<ApplicationUser> _usersRepository;

        public ItemController(IRepository<Item> itemsRepository, IRepository<Comment> commentsRepository)
        {
            _itemsRepository = itemsRepository;
            _commentsRepository = commentsRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Item>> GetAsync()
        {
          var result = await _itemsRepository.GetAllAsync();
            return  result;
        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<Comment>> GetComents()
        {
          var result = await _commentsRepository.GetAllAsync();
            return  result;
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
           // 
           // var user = await _usersRepository.GetUserAsync("asd");
           // List<Tag> tags = new List<Tag>();
           // foreach (var entity in createItemDto.Tags)
           // {
           //     tags.Add(
           //         new Tag{
           //             Name = entity
           //         });  
           // }
           // var item = new Item{
           //     Name = createItemDto.NameItem,
           //     CreatedTime = DateTimeOffset.Now,
           //     ApplicationUserId = user.Id
           // };
           // item.Tags.AddRange(tags);
           //await _itemsRepository.CreateAsync(item);

                return Ok();
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