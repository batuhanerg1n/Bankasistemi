using BE.Bank.Web.Data.Context;
using BE.Bank.Web.Data.Entities;
using BE.Bank.Web.Data.Interfaces;

namespace BE.Bank.Web.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public readonly BankContext _bankContext;

        public AccountRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public void Create(Account account)
        {
            
            _bankContext.Set<Account>().Add(account);
            _bankContext.SaveChanges();
        }
    }
}
