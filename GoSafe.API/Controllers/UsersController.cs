using GoSafe.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoSafe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] tblUser user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }
            // You can add additional validation here (e.g., check for required fields)
            _context.tblUsers.Add(user);
            _context.SaveChanges();
            return Ok(user.Id);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.tblUsers.Where(u => !u.IsDeleted).ToList();
            return Ok(users);
        }
    }
}
