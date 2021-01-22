using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        public IActionResult Get()
        {

            return View();
        }
    }
}
