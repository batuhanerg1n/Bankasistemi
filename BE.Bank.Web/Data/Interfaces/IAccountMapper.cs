using BE.Bank.Web.Data.Entities;
using BE.Bank.Web.Models;

namespace BE.Bank.Web.Data.Interfaces
{
    public interface IAccountMapper
    {
        public Account Map(AccountCreateModel model);
    }
}
