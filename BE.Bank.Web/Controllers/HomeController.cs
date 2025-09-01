using BE.Bank.Web.Data.Context;
using BE.Bank.Web.Data.Entities;
using BE.Bank.Web.Data.Interfaces;
using BE.Bank.Web.Data.UnitOfWork;
using BE.Bank.Web.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace BE.Bank.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUserMapper _userMapper;
        private readonly IUow _uow;

        public HomeController(IUserMapper userMapper, /* IApplicationUserRepository applicationUserRepository*/
IUow uow)
        {
            _userMapper = userMapper;
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View(_userMapper.MapToListOfUserList(_uow.GetRepository<ApplicationUser>().GetAll()));
        }
    }
}  
