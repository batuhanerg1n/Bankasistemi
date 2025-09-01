using BE.Bank.Web.Data.Context;
using BE.Bank.Web.Data.Entities;
using BE.Bank.Web.Data.Interfaces;
using BE.Bank.Web.Data.Repositories;
using BE.Bank.Web.Data.UnitOfWork;
using BE.Bank.Web.Mapping;
using BE.Bank.Web.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace BE.Bank.Web.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IApplicationUserRepository _applicationUserRepository;
        //private readonly IAccountRepository _accountRepository;
        //private readonly IUserMapper _userMapper;
        //private readonly IAccountMapper _accountMapper;
        //// Constructor injection
        //public AccountController(IUserMapper userMapper, IApplicationUserRepository applicationUserRepository, IAccountRepository accountRepository, IAccountMapper accountMapper)
        //{
        //    _userMapper = userMapper;
        //    _applicationUserRepository = applicationUserRepository;
        //    _accountRepository = accountRepository;
        //    _accountMapper = accountMapper;
        //}

        //private readonly IRepository<Account> _accountRepository;
        //private readonly IRepository<ApplicationUser> _userRepository;
        //private IEnumerable<object> accounts;

        //public AccountController(IRepository<Account> accountRepository, IRepository<ApplicationUser> userRepository)
        //{
        //    _accountRepository = accountRepository;
        //    _userRepository = userRepository;
        //}
        private readonly IUow _uow;

        public AccountController(IUow uow)
        {
            _uow = uow;
        }

        public IActionResult Create(int id)
        {
            var userInfo = _uow.GetRepository<ApplicationUser>().GetById(id);
            return View(new UserListModel
            {
                Id = userInfo.Id,
                Name = userInfo.Name,
                Surname = userInfo.Surname

            });
        }
        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _uow.GetRepository<Account>().Create(new Account
            {
                AccountNumber = model.AccountNumber,
                Balance = model.Balance,
                ApplicationUserId = model.ApplicationUserId // Doğru kullanım
                
            });
            _uow.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            var query = _uow.GetRepository<Account>().GetQueryable();
            var accounts = query.Where(x => x.ApplicationUserId == userId).ToList();
            var user = _uow.GetRepository<ApplicationUser>().GetById(userId);

            if (user != null)
                ViewBag.FullName = user.Name + " " + user.Surname;
            else
                ViewBag.FullName = "Kullanıcı Bulunamadı";

            var list = new List<AccountListModel>();
            foreach (var account in accounts)
            {
                list.Add(new()
                {
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.ApplicationUserId,
                    Balance = account.Balance,
                    Id = account.Id
                });
            }

            return View(list);
        }
        [HttpGet]
        public IActionResult SendMoney(int accountId)
        {
            var query = _uow.GetRepository<Account>().GetQueryable();
            var accounts = query.Where(x => x.Id != accountId).ToList();

            var list = new List<AccountListModel>();
            ViewBag.SenderId = accountId;
            foreach (var account in accounts)
            {
                list.Add(new()
                {
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.ApplicationUserId,
                    Balance = account.Balance,
                    Id = account.Id
                });
            }
            //var list2 =new SelectList(list);
            //var Items=list2.Items;
            return View(new SelectList(list, "Id", "AccountNumber"));
        }
        [HttpPost]

        public IActionResult SendMoney(SendMoneyModel model)
        {
            //snederId
            //AccountId
            var senderAccount= _uow.GetRepository<Account>().GetById(model.SenderId);
            senderAccount.Balance -= model.Amount;
            _uow.GetRepository<Account>().Update(senderAccount);
            var account= _uow.GetRepository<Account>().GetById(model.AccountId);
            account.Balance += model.Amount;
            _uow.GetRepository<Account>().Update(account);
            _uow.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}
