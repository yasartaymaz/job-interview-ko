using Business.Abstract;
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
        private readonly IExamQuestionService _examQuestionService;

        public ExamController(ISessionService sessionService, IArticleService articleService, IExamService examService, IExamQuestionAnswerService examQuestionAnswerService, IExamQuestionService examQuestionService)
        {
            _sessionService = sessionService;
            _articleService = articleService;
            _examService = examService;
            _examQuestionService = examQuestionService;
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

            if (!Tools.IsObjectNullOrEmpty(exam))
            {
                int counter = 0;
                string questionIdArrayForJavascript = "var listQuestionId = [";
                foreach (ExamQuestionDTO item in exam.Questions)
                {
                    if (counter > 0)
                    {
                        questionIdArrayForJavascript += ",";
                    }
                    questionIdArrayForJavascript += "'" + item.Id.ToString() + "'";

                    counter++;
                }
                questionIdArrayForJavascript += "];\n";

                ViewBag.QuestionIdArray = questionIdArrayForJavascript;
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            _examService.Delete(id);

            _sessionService.Create(10);
            return Redirect("~/exam/list");
        }

        [HttpPost]
        public IActionResult TakeExamViaAjax(IFormCollection form)
        {
            int accountId = Tools.StrToInt(Request.Query["accountId"].ToString());
            int examId = Tools.StrToInt(Request.Query["examId"].ToString());

            List<ExamQuestionDTO> questions = _examQuestionService.GetListByExamId(examId).Data;
            TakeExamDTO takeExamDto = new TakeExamDTO
            {
                AccountId = accountId,
                ExamId = examId
            };
            List<TakenExamAnswerDTO> answers = new List<TakenExamAnswerDTO>();
            foreach (ExamQuestionDTO item in questions)
            {
                if (!String.IsNullOrEmpty(form["givenExamAnswers[question" + item.Id + "]"]))
                {
                    TakenExamAnswerDTO answer = new TakenExamAnswerDTO
                    {
                        AccountId = accountId,
                        QuestionId = item.Id,
                        AnswerId = Tools.StrToInt(form["givenExamAnswers[question" + item.Id + "]"])
                    };
                    answers.Add(answer);
                }
            }
            takeExamDto.TakenAnswers = answers;

            takeExamDto = _examService.TakeExam(takeExamDto).Data;

            return Json(takeExamDto);
        }
    }
}
