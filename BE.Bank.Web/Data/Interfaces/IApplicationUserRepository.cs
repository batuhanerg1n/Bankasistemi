using BE.Bank.Web.Data.Entities;
using System.Collections.Generic;

namespace BE.Bank.Web.Data.Interfaces
{
    public interface IApplicationUserRepository
    {
        List<ApplicationUser> GetAll();
        ApplicationUser GetById(int id);
    }
}
