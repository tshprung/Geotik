using Geotik.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Geotik.Models
{
    public class LoanDTO
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description must be a string with maximal length of 200 characters")]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int Ammount { get; set; }

        public bool Borrow { get; set; }

        public bool Payed { get; set; }

        public LoanDTO()
        {
        }

        public LoanDTO(Loan loan)
        {
            Id = loan.Id;
            Description = loan.Description;
            Borrow = loan.Borrow;
            Ammount = loan.Ammount;
            Payed = loan.Payed;
            AccountId = loan.AccountId;
            UserName = loan.Account.User.Name;
        }
    }    
}
