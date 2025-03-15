// using Microsoft.AspNetCore.Mvc;
// using StudyService.Models;
// using StudyService.Data;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace StudyService.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UsersController : ControllerBase
//     {
//         private readonly UserRepository _userRepository;
//         private readonly StudyServiceDbContext _context;

//         public UsersController(StudyServiceDbContext context)
//         {
//             _context = context;
//         }

//         [HttpGet]
//         public ActionResult<List<User>> GetUsers()
//         {
//             try
//             {
//                 return Ok(_context.Users.ToList());
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Ошибка при получении пользователей: {ex.Message}");
//             }
//         }

//         [HttpPost("register")]
//         public ActionResult<User> Register([FromBody] User user)
//         {
//             try
//             {
//                 if (user == null || string.IsNullOrEmpty(user.Email))
//                     return BadRequest("Email обязателен");
//                 if (string.IsNullOrEmpty(user.Password))
//                     return BadRequest("Пароль обязателен");
//                 if (_context.Users.Any(u => u.Email == user.Email))
//                     return BadRequest("Пользователь с таким email уже существует");

//                 _context.Users.Add(user);
//                 _context.SaveChanges();
//                 return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Ошибка при регистрации: {ex.Message}");
//             }
//         }
//         //         [HttpPost("register")]
// //         public ActionResult<User> RegisterUser([FromBody] User user)
// //         {
// //             if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
// //             {
// //                 return BadRequest("Email и пароль обязательны");
// //             }

// //             if (_context.Users.Any(u => u.Id == user.Id))
// //             {
// //                 return Conflict("Пользователь с таким id уже существует");
// //             }

// //             if (_context.Users.Any(u => u.Email == user.Email))
// //             {
// //                 return Conflict("Пользователь с таким email уже существует");
// //             }

// //             try
// //             {
// //                 _context.Users.Add(user); // Добавляем пользователя в контекст
// //                 _context.SaveChanges();   // Сохраняем в базе
// //                 return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
// //             }
// //             catch (Exception ex)
// //             {
// //                 return StatusCode(500, $"Ошибка при регистрации: {ex.Message}");
// //             }
// //         }

//         [HttpPost("login")]
//         public ActionResult<User> Login([FromBody] LoginRequest loginRequest)
//         {
//             if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
//             {
//                 return BadRequest("Email и пароль обязательны");
//             }

//             var user = _userRepository.GetUserByEmail(loginRequest.Email);
//             if (user == null || user.Password != loginRequest.Password)
//             {
//                 return Unauthorized("Неверный email или пароль");
//             }

//             // Возвращаем данные пользователя без пароля
//             return Ok(new
//             {
//                 user.Id,
//                 user.Name,
//                 user.Surname,
//                 user.Email,
//                 user.Stage,
//                 user.Course,
//                 user.ProfileDescription
//             });
//         }
//     }

//     public class LoginRequest
//     {
//         public string Email { get; set; }
//         public string Password { get; set; }
//     }
// }
using Microsoft.AspNetCore.Mvc;
using StudyService.Models;
using StudyService.Data;
using System;
using System.Collections.Generic;

namespace StudyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly StudyServiceDbContext _context;

        public UsersController(StudyServiceDbContext context, UserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            try
            {
                return Ok(_userRepository.GetAllUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении пользователей: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public ActionResult<User> RegisterUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email и пароль обязательны");
            }

            if (_userRepository.UserExistsById(user.Id))
            {
                return Conflict("Пользователь с таким id уже существует");
            }

            if (_userRepository.UserExistsByEmail(user.Email))
            {
                return Conflict("Пользователь с таким email уже существует");
            }

            try
            {
                _userRepository.AddUser(user);
                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при регистрации: {ex.Message}");
            }
        }

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

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}