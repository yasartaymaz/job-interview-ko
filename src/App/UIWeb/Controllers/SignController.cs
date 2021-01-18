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
            return Redirect("./sign/in");
        }
    }
}
