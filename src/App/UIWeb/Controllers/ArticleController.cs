using Business.Abstract;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIWeb.HttpContextHelpers;

namespace UIWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ISessionService _sessionService;
        public ArticleController(IArticleService articleService, ISessionService sessionService)
        {
            _articleService = articleService;
            _sessionService = sessionService;
        }

        public IActionResult Get()
        {
            IResult result= _articleService.InitArticles(Core.Constants.ScrapeConstants.ArticleCountToScrape);
            _sessionService.CheckMessages(result.Messages);

            return Redirect("~/home/index");
        }
    }
}
