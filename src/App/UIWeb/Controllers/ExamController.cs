﻿using Business.Abstract;
using Core.Utilities;
using Core.Utilities.Results;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIWeb.HttpContextHelpers;

namespace UIWeb.Controllers
{
    public class ExamController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IArticleService _articleService;
        private readonly IExamService _examService;

        public ExamController(ISessionService sessionService, IArticleService articleService, IExamService examService)
        {
            _sessionService = sessionService;
            _articleService = articleService;
            _examService = examService;
        }

        public IActionResult Create()
        {
            List<ArticleDTO> articles = null;
            articles = _articleService.GetLastDescending(Core.Constants.ScrapeConstants.ArticleCountToScrape).Data;
            ViewBag.Articles = articles;

            return View();
        }

        public IActionResult CreateExamFromArticle(int id)
        {
            ArticleDTO article = _articleService.Get(id).Data;
            ViewBag.Article = article;

            return View();
        }

        [HttpPost]
        public IActionResult CreateExamFromArticle(int id, IFormCollection form)
        {
            List<CreateExamFromArticleDTO> questions = new List<CreateExamFromArticleDTO>();

            for (int i = 0; i < 4; i++)
            {
                CreateExamFromArticleDTO question = new CreateExamFromArticleDTO();
                question.ArticleId = id;
                question.Question = form["question" + i];
                List<string> answers = new List<string>();
                for (int j = 0; j < 4; j++)
                {
                    answers.Add(form["question" + i + "_answer" + j]);
                }
                question.Answers = answers;
                question.CorrectAnswer = Tools.StrToInt(form["question" + i + "_correctanswer"]);

                questions.Add(question);
            }

            IResult result = _examService.CreateExam(questions);
            _sessionService.CheckMessages(result.Messages);

            if (result.Success)
                return Redirect("~/exam/list");
            else
                return Redirect("~/exam/CreateExamFromArticle/" + id);
        }

        public IActionResult List()
        {
            List<ExamDTO> exams = _examService.GetList().Data;
            ViewBag.Exams = exams;

            return View();
        }

        public IActionResult View(int id)
        {
            ExamDTO exam = _examService.GetExamFull(id).Data;
            ViewBag.Exam = exam;

            return View();
        }

        public IActionResult Take(int id = 0)
        {
            if (id == 0)
            {
                _sessionService.Create(9);
                return Redirect("~/exam/list");
            }

            ExamDTO exam = _examService.GetExamFull(id).Data;
            ViewBag.Exam = exam;

            return View();
        }

        public IActionResult Delete(int id)
        {
            _examService.Delete(id);

            _sessionService.Create(10);
            return Redirect("~/exam/list");
        }
    }
}
