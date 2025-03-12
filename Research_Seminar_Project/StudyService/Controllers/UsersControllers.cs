using Microsoft.AspNetCore.Mvc;
using StudyService.Models;
using StudyService.Data;

namespace StudyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserRepository _userRepository;

        public UsersController()
        {
            _userRepository = new UserRepository();
        }

        // POST: api/users/register
        [HttpPost("register")]
        public ActionResult<User> RegisterUser([FromBody] User user)
        {
            // Проверка на пустые поля
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email и пароль обязательны");
            }

            // Проверка, существует ли пользователь с таким email
            if (_userRepository.GetUserByEmail(user.Email) != null)
            {
                return Conflict("Пользователь с таким email уже существует");
            }

            _userRepository.AddUser(user);
            return CreatedAtAction(nameof(RegisterUser), new { id = user.Id }, user);
        }

        // GET: api/users (для тестирования, потом можно убрать)
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }
    }
}