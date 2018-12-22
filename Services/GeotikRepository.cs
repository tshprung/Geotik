using System.Collections.Generic;
using System.Linq;
using Geotik.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Geotik.Services
{
    public class GeotikRepository : IGeotikRepository
    {
        private GeotikContext _context;

        public GeotikRepository(GeotikContext context)
        {
            _context = context;
        }

        public Loan AddLoan(Loan loan)
        {
            EntityEntry<Loan> loanAdded = _context.Loans.Add(loan);
            _context.SaveChanges();
            return loanAdded.Entity;
        }

        public User AddUser(User user)
        {
            EntityEntry<User> res = _context.Users.Add(user);
            _context.SaveChanges();
            return res.Entity;
        }

        public bool DeleteLoan(int loanId)
        {
            Loan loan = _context.Loans.FirstOrDefault(l => l.Id == loanId);

            if (loan == null)
            {
                return false;
            }

            _context.Loans.Remove(loan);
            _context.SaveChanges();
            return true;
        }

        public Account GetAccount(int id)
        {
            return _context.Accounts
                .Include(a => a.Loans)
                .Where(a => a.Id.Equals(id))
                .FirstOrDefault();
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts
                .Include(a => a.User)
                .Include(a => a.Loans)
                .OrderBy(a => a.User.Name).ToList();
        }

        public Loan GetLoanById(int loanId)
        {
            return _context.Loans
                .FirstOrDefault(l => l.Id.Equals(loanId));
        }

        public IEnumerable<Loan> GetLoans()
        {
            return _context.Loans
                .Include(l => l.Account)
                .Include(l => l.Account.User)
                .OrderBy(l => l.Account.User.Name).ToList();
        }

        public IEnumerable<Loan> GetLoansByAccountId(int accountId)
        {
            return _context.Accounts
                .Where(a => a.Id.Equals(accountId))
                .FirstOrDefault().Loans.ToList();
        }

        public User GetUser(string username)
        {
            return _context.Users.Include(u => u.Account)
                .Include(u => u.Account.Loans)
                .Where(u => u.Name.Equals(username))
                .FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users
                .Include(u => u.Account)
                .OrderBy(u => u.Name).ToList();
        }

        public bool MarkLoanAsPayed(int loanId)
        {
            Loan loan = _context.Loans.FirstOrDefault(l => l.Id == loanId);

            if (loan == null)
            {
                return false;
            }

            loan.Payed = true;
            _context.SaveChanges();
            return true;
        }
    }
}
