using BE.Bank.Web.Data.Interfaces;

namespace BE.Bank.Web.Data.UnitOfWork
{
    public interface IUow
    {
        public IRepository<T> GetRepository<T>() where T : class, new();
        public void SaveChanges();

    }
}