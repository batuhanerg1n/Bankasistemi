using BE.Bank.Web.Data.Context;
using BE.Bank.Web.Data.Interfaces;
using BE.Bank.Web.Data.Repositories;

namespace BE.Bank.Web.Data.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly BankContext _bankContext;
        public Uow(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_bankContext);
        }
        public void SaveChanges()
        {
            _bankContext.SaveChanges();
        }
    }
}
