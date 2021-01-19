using Business.Abstract;
using Entities.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIWeb.Controllers
{
    public class SignController : Controller
    {
        private readonly IAccountService _accountService;

        public SignController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return Redirect("~/sign/in");
        }

        public IActionResult In()
        {
            return View();
        }

        [HttpPost]
        public IActionResult In(SignInDTO form)
        {
            AccountDTO accountdto = _accountService.SignIn(form).Data;
            return Redirect("~/sign/in");
        }
    }
}
