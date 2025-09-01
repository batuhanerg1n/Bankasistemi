using BE.Bank.Web.Data.Entities;
using BE.Bank.Web.Data.Interfaces;
using BE.Bank.Web.Models;

namespace BE.Bank.Web.Mapping
{
    public class AccountMapper : IAccountMapper
    {
        public Account Map(AccountCreateModel model)
        {
            return new Account
            {
                AccountNumber = model.AccountNumber,
                ApplicationUserId = model.ApplicationUserId,
                Balance = model.Balance
            };
        }
    }
}
