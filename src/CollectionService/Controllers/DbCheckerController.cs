using CollectionService.Data;
using Microsoft.AspNetCore.Mvc;

namespace CollectionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DbCheckerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DbCheckerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Items.ToList());  
        }

    }
}