using BE.Bank.Web.Data.Entities;
using BE.Bank.Web.Models;
using System.Collections.Generic;

namespace BE.Bank.Web.Mapping
{
    public interface IUserMapper
    {
        public List<UserListModel> MapToListOfUserList(List<ApplicationUser> users);
        public UserListModel MapToUserList(ApplicationUser user);
    }
}
