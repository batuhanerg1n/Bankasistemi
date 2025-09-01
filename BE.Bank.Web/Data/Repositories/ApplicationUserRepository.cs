using BE.Bank.Web.Data.Context;
using BE.Bank.Web.Data.Entities;
using BE.Bank.Web.Data.Interfaces;
using System.Collections.Generic;
using System.Linq; // Bu satırı ekleyin

namespace BE.Bank.Web.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly BankContext _bankContext;

        public ApplicationUserRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public List<ApplicationUser> GetAl()
        {
            throw new System.NotImplementedException();
        }

        public List<ApplicationUser> GetAll()
        {
            return _bankContext.ApplicationUsers.ToList();
        }
        public ApplicationUser GetById(int id)
        {
            return _bankContext.ApplicationUsers.SingleOrDefault(x => x.Id == id);
        }
        public void Create(ApplicationUser user)
        {

            _bankContext.Set<ApplicationUser>().Add(user);
            _bankContext.SaveChanges();
        }
    }
}