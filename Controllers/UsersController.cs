using Geotik.Entities;
using Geotik.Models;
using Geotik.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Geotik.Controllers
{
    public class UsersController : Controller
    {
        private ILogger<UsersController> _logger;
        private IGeotikRepository _repository;

        public UsersController(IGeotikRepository repository, ILogger<UsersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("api/users")]
        public IActionResult GetUsers()
        {
            _logger.LogInformation("Entering GetUsers");
            List<UserDTO> res = new List<UserDTO>();

            IEnumerable<User> users = _repository.GetUsers();

            foreach(User user in users)
            {
                res.Add(new UserDTO(user));
            }

            _logger.LogInformation("Exiting GetUsers");
            return Ok(res);
        }

        [HttpPut("api/users")]
        public IActionResult AddUser([FromBody] UserDTO user)
        {
            _logger.LogInformation("Entering AddUser");

            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User newUser = new User()
            {
                Name = user.Name,
                Account = new Account()
                {
                    Loans = new List<Loan>(),
                    OpenDate = DateTime.Now
                }
            };

            if (_repository.GetUser(newUser.Name) != null)
            {
                string message = $"user [{user.Name}] already exists";
                _logger.LogError($"AddUser - {message}");
                return Unauthorized(message);
            }

            UserDTO userAdded = new UserDTO(_repository.AddUser(newUser));

            _logger.LogInformation("Exiting AddUser");
            return Ok(userAdded);
        }
    }
}