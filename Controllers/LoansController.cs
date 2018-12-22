using System.Collections.Generic;
using Geotik.Entities;
using Geotik.Models;
using Geotik.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geotik.Controllers
{
    [Route("api/loans")]
    public class LoansController : Controller
    {
        private ILogger<LoansController> _logger;
        private IGeotikRepository _repository;

        public LoansController(IGeotikRepository repository, ILogger<LoansController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetAllLoans()
        {
            _logger.LogInformation("Entering GetAllLoans");
            List<LoanDTO> res = new List<LoanDTO>();

            IEnumerable<Loan> allLoans = _repository.GetLoans();

            foreach (Loan loan in allLoans)
            {
                res.Add(new LoanDTO(loan));
            }

            _logger.LogInformation("Exiting GetAllLoans");
            return Ok(res);
        }

        [HttpGet("{name}/all")]
        public IActionResult GetLoansByUsername(string name)
        {
            _logger.LogInformation("Entering GetLoansByUsername");
            List<LoanDTO> res = new List<LoanDTO>();

            User user = _repository.GetUser(name);

            if (user == null)
            {
                string message = $"GetLoansByUsername. User name [{name}] was not found.";
                _logger.LogInformation(message);
                return NotFound(message);
            }

            if (user.Account.Loans != null)
            {
                foreach (Loan loan in user.Account.Loans)
                {
                    res.Add(new LoanDTO(loan));
                }
            }

            _logger.LogInformation($"Exiting GetLoansByUsername. Found [{res.Count}] loans.");
            return Ok(res);
        }

        [HttpPost("{name}")]
        public IActionResult AddLoanByUsername(string name,
            [FromBody] LoanDTO loan)
        {
            _logger.LogInformation("Entering AddLoanByUsername");

            if (loan == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = _repository.GetUser(name);

            if (user == null)
            {
                string message = $"AddLoanByUsername. Could not find user by name [{name}].";
                _logger.LogError(message);
                return BadRequest(message);
            }

            Loan newLoan = new Loan()
            {
                Account = user.Account,
                Ammount = loan.Ammount,
                Borrow = loan.Borrow,
                Description = loan.Description,
                Payed = loan.Payed
            };

            LoanDTO loanAdded = new LoanDTO(_repository.AddLoan(newLoan));

            _logger.LogInformation($"Exiting AddLoanByUsername");
            return Ok(loanAdded);
        }

        [HttpGet("{id}", Name = "GetLoanById")]
        public IActionResult GetLoanById(int id)
        {
            _logger.LogInformation("Entering GetLoanById");
            Loan loan = _repository.GetLoanById(id);

            if (loan == null)
            {
                string message = $"GetLoanById. Could not find Loan by Id [{id}].";
                _logger.LogError(message);
                return NotFound(message);
            }

            LoanDTO res = new LoanDTO(loan);

            _logger.LogInformation("Exiting GetLoanById");
            return Ok(res);
        }

        [HttpPut("{id}/payed")]
        public IActionResult MarkLoanAsPayed(int id)
        {
            _logger.LogInformation($"Entering MarkLoanAsPayed");

            if (!_repository.MarkLoanAsPayed(id))
            {
                string message = $"MarkLoanAsPayed. Loan by Id [{id}] was not found.";
                _logger.LogError(message);
                return NotFound(message);
            }

            _logger.LogInformation($"Exiting MarkLoanAsPayed");
            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult DeleteLoan(int id)
        {
            _logger.LogInformation("Entering DeleteLoan");

            if (!_repository.DeleteLoan(id))
            {
                string message = $"DeleteLoan. Could not find Loan by Id [{id}].";
                _logger.LogError(message);
                return NotFound(message);
            }

            _logger.LogInformation("Exiting DeleteLoan");
            return NoContent();
        }
    }
}