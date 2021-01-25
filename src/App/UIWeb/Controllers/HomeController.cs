using Business.Abstract;
using Entities.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;

        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            List<ArticleDTO> articles = null;
            articles = _articleService.GetLastDescending(Core.Constants.ScrapeConstants.ArticleCountToScrape).Data;
            ViewBag.Articles = articles;

            return View();
        }
    }
}
