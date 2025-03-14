using Microsoft.AspNetCore.Mvc;
using StudyService.Models;
using StudyService.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // GET: api/users
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            try
            {
                return Ok(_context.Users.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении пользователей: {ex.Message}");
            }
        }

        // POST: api/users/register
        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] User user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Email))
                    return BadRequest("Email обязателен");
                if (string.IsNullOrEmpty(user.Password))
                    return BadRequest("Пароль обязателен");
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

        // POST: api/users/login
        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
                    return BadRequest("Email и пароль обязательны");

                var user = _context.Users
                    .FirstOrDefault(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

                if (user == null)
                    return Unauthorized("Неверный email или пароль");

                
                return Ok(new { Message = "Успешный вход", UserId = user.Id, Email = user.Email });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при входе: {ex.Message}");
            }
        }
    }

    // Вспомогательный класс для запроса логина
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
