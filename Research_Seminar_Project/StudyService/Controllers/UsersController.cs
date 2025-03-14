using Microsoft.AspNetCore.Mvc;
using StudyService.Models;
using StudyService.Data;

namespace StudyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StudyServiceDbContext _context;

        public UsersController(StudyServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpPost("register")] // Добавляем маршрут /register
        public ActionResult<User> Register([FromBody] User user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Email))
                    return BadRequest("Email обязателен");
                if (_context.Users.Any(u => u.Email == user.Email))
                    return BadRequest("Пользователь с таким email уже существует");

                _context.Users.Add(user);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при регистрации: {ex.Message}");
            }
        }
    }
}
