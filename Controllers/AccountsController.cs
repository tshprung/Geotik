using System.Collections.Generic;
using Geotik.Entities;
using Geotik.Models;
using Geotik.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geotik.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private IGeotikRepository _repository;
        private ILogger<AccountsController> _logger;

        public AccountsController(IGeotikRepository repository, ILogger<AccountsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetAccounts()
        {
            _logger.LogInformation("Entering GetAccounts");
            List<AccountDTO> result = new List<AccountDTO>();
            IEnumerable<Account> accounts = _repository.GetAccounts();
            foreach(Account account in accounts)
            {
                result.Add(new AccountDTO(account));
            }

            _logger.LogInformation($"Exiting GetAccounts. Retreived [{result.Count}] accounts");

            return Ok(new JsonResult(result));
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            _logger.LogInformation("Entering GetAccountById");

            Account account = _repository.GetAccount(id);

            if (account == null)
            {
                _logger.LogError($"GetAccountById - account by id [{id}] was not found.");
                return NotFound();
            }

            _logger.LogInformation("Exiting GetAccountById");
            return Ok(new AccountDTO(account));
        }
    }
}