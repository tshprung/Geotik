using Geotik.Entities;
using System;
using System.Collections.Generic;

namespace Geotik.Models
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime OpenDate { get; set; }
        public ICollection<LoanDTO> Loans { get; set; }
  
        public AccountDTO()
        { }

        public AccountDTO(Account account)
        {
            Id = account.Id;
            UserName = account.User.Name;
            OpenDate = account.OpenDate;
            Loans = new List<LoanDTO>();
            foreach(Loan loan in account.Loans)
            {
                Loans.Add(new LoanDTO(loan));
            }
        }
    }
}
