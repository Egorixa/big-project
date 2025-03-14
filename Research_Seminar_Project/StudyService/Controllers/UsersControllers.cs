using Microsoft.AspNetCore.Mvc;
using StudyService.Models;
using StudyService.Data;

namespace StudyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
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
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email и пароль обязательны");
            }

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

        // POST: api/users/login
        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Email и пароль обязательны");
            }

            var user = _userRepository.GetUserByEmail(loginRequest.Email);
            if (user == null || user.Password != loginRequest.Password)
            {
                return Unauthorized("Неверный email или пароль");
            }

            // Возвращаем данные пользователя без пароля
            return Ok(new
            {
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.Stage,
                user.Course,
                user.ProfileDescription
            });
        }
    }

    // Модель для запроса логина
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
