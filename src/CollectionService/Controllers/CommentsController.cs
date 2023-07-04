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
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _repository;
        private readonly IUsersRepository<ApplicationUser> _userRepository;

        public CommentsController(IRepository<Comment> repository, IUsersRepository<ApplicationUser> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
          [HttpGet]
        public async Task<IEnumerable<Comment>> GetAllOfItemAsync()
        {
          return  await _repository.GetAllAsync();
        }

        [HttpGet("{ItemId}")]
        public async Task<IEnumerable<Comment>> GetAllOfItemAsync(Guid ItemId)
        {
          return  await _repository.GetAllAsync(p => p.ItemId == ItemId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment is null)
                return NotFound();
                
            return Ok(comment.AsDto());  
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCommentDto commentDto)
        {

           // var user = await _userRepository.GetByNameAsync(HttpContext.User.Identity.Name);
            var user = await _userRepository.GetUserAsync("asd");
            if (user is null)
              return NotFound($"User not found");

            var comment = new Comment{
                Name = commentDto.CommentText,
                CreatedTime = DateTimeOffset.Now,
                ApplicationUserId = user.Id,


            };
           await _repository.CreateAsync(comment);

                return Ok(comment.AsDto());
            }

        
}
}