using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollectionService.Data;
using CollectionService.Models;
using CollectionService.Repositories;
using Microsoft.AspNetCore.Identity;
using CollectionService.Extensions;
using CollectionService.Models.Authentication;
using CollectionService.Services;
using Microsoft.AspNetCore.Authorization;

namespace CollectionService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository<ApplicationUser> _usersRepository;
        private readonly ITokenCreationService _jwtService;

        // private readonly UserManager<ApplicationUser> _manager;
        //
        // public UsersController(UserManager<ApplicationUser> manager)
        // {
        //     _manager = manager;
        // }
        public UsersController(IUsersRepository<ApplicationUser> usersRepository, ITokenCreationService jwtService)
       {
            _usersRepository = usersRepository;
            _jwtService = jwtService;
       }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _usersRepository.GetUserAsync(id);
            if (user is null)
                return NotFound();
        
            return Ok(user);  
        }
        [Authorize]
        [HttpGet("{userName}")]
        public async Task<IActionResult> GetByNameAsync(string userName)
        {
            var user = await _usersRepository.GetUserAsync(userName);
            if (user is null)
                return NotFound();
        
            return Ok(user);  
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] User user)
        {
            await _usersRepository.CreateAsync(
            new ApplicationUser() { UserName = user.UserName, Email = user.Email },
            user.Password
        );

       
                return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken([FromForm] AuthenticationRequest request)
    {
       
        if (!ModelState.IsValid)
        {
            return BadRequest("Bad credentials");
        }
    
        var user = await _usersRepository.GetUserAsync(request.UserName);
    
        if (user == null)
        {
            return BadRequest("Bad credentials");
        }
    
        var isPasswordValid = await _usersRepository.Login(user, request.Password);
    
        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }

        var token =  _usersRepository.CreateToken(user);
        return Ok(token);
}   
    
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAsync(Guid id, UpdateItemDto updateItemDto)
        //{
        //    await _itemsRepository.UpdateAsync(id);
        //  return Ok();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var user = await _usersRepository.GetUserAsync(id);
            if (user is null)
                return NotFound();

           await _usersRepository.RemoveAsync(id);
           return Ok();
    } 
}
}