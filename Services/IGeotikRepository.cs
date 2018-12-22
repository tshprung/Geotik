using Geotik.Entities;
using System.Collections.Generic;

namespace Geotik.Services
{
    public interface IGeotikRepository
    {
        IEnumerable<User> GetUsers();

        IEnumerable<Account> GetAccounts();

        IEnumerable<Loan> GetLoans();

        User GetUser(string username);

        User AddUser(User user);

        Account GetAccount(int id);

        IEnumerable<Loan> GetLoansByAccountId(int id);

        Loan GetLoanById(int loanId);

        Loan AddLoan(Loan loan);

        bool MarkLoanAsPayed(int loanId);

        bool DeleteLoan(int loanId);
    }
}
