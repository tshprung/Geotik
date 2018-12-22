using System;
using System.Collections.Generic;
using System.Linq;

namespace Geotik.Entities
{
    public static class GeotikExtensions
    {
        public static void EnsureSeedDataForContext(this GeotikContext context)
        {
            //If we already have data - exit
            if (context.Users.Any())
            {
                return;
            }

            //if we have no data - add sample data
            List<User> users = new List<User>()
            {
                new User()
                {
                    Name = "Tal",
                    Account = new Account()
                        {
                            OpenDate = DateTime.Now,
                            Loans = new List<Loan>()
                        }
                },
                new User()
                {
                    Name = "Dan",
                    Account = new Account()
                    {
                        OpenDate = DateTime.Now,
                        Loans = new List<Loan>()
                        {
                            new Loan()
                            {
                                Ammount = 10000,
                                Description = "University Tuition",
                                Borrow = true
                            }
                        }
                    }
                },
                new User()
                {
                    Name = "Liat",
                    Account = new Account()
                    {
                        OpenDate = DateTime.Now,
                        Loans = new List<Loan>()
                        {
                            new Loan()
                            {
                                Ammount = 10000,
                                Description = "University Tuition",
                                Borrow = true
                            },
                            new Loan()
                            {
                                Ammount = 20000,
                                Description = "Investment",
                                Borrow = false
                            }
                        }
                    }
                }
        };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
