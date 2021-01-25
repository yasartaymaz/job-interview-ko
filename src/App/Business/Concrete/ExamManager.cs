using AutoMapper;
using Business.Abstract;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ExamManager : IExamService
    {
        private readonly IExamDA _examDA;
        private readonly IMapper _mapper;
        private readonly IExamQuestionService _examQuestionService;
        private readonly IExamQuestionAnswerService _examQuestionAnswerService;
        private readonly IArticleService _articleService;

        public ExamManager(IExamDA examDA, IMapper mapper, IExamQuestionService examQuestionService, IExamQuestionAnswerService examQuestionAnswerService, IArticleService articleService)
        {
            _examDA = examDA;
            _mapper = mapper;
            _examQuestionService = examQuestionService;
            _examQuestionAnswerService = examQuestionAnswerService;
            _articleService = articleService;
        }

        public IResult CreateExam(List<CreateExamFromArticleDTO> questions)
        {
            if (Tools.IsObjectNullOrEmpty(questions))
            {
                return new ErrorResult(new List<int> { 8 });
            }

            int articleId = questions.First().ArticleId;
            ExamDTO examDto = new ExamDTO
            {
                ArticleId = articleId
            };
            examDto = Insert(examDto).Data;

            InsertQuestions(questions, examDto.Id);

            return new SuccessResult(new List<int> { 7 });
        }

        public IDataResult<ExamDTO> Insert(ExamDTO input)
        {
            Exam entity = _mapper.Map<Exam>(input);
            entity = _examDA.Insert(entity);
            input = _mapper.Map<ExamDTO>(entity);

            return new SuccessDataResult<ExamDTO>(input);
        }

        private void InsertQuestions(List<CreateExamFromArticleDTO> questions, int examId)
        {
            foreach (CreateExamFromArticleDTO question in questions)
            {
                ExamQuestionDTO examQuestion = new ExamQuestionDTO
                {
                    ExamId = examId,
                    Value = question.Question
                };
                examQuestion = _examQuestionService.Insert(examQuestion).Data;

                InsertAnswers(question.Answers, examQuestion.Id, question.CorrectAnswer);
            }
        }

        private void InsertAnswers(List<string> answers, int examQuestionId, int correctAnswer)
        {
            int counter = 0;
            foreach (string answer in answers)
            {
                ExamQuestionAnswerDTO examQuestionAnswer = new ExamQuestionAnswerDTO
                {
                    QuestionId = examQuestionId,
                    Value = answer
                };
                examQuestionAnswer.Correct = correctAnswer == counter ? 1 : 0;
                _examQuestionAnswerService.Insert(examQuestionAnswer);
                counter++;
            }
        }

        public IDataResult<List<ExamDTO>> GetList()
        {
            List<ExamDTO> result = null;
            result = _examDA.GetListWithArticle();

            return new SuccessDataResult<List<ExamDTO>>(result);
        }

        public IDataResult<ExamDTO> GetExamFull(int id)
        {
            ExamDTO exam = Get(id).Data;
            exam.Article = _articleService.Get(exam.ArticleId).Data;
            exam.Questions = _examQuestionService.GetListByExamId(id).Data;
            foreach (ExamQuestionDTO item in exam.Questions)
            {
                item.Answers = _examQuestionAnswerService.GetListByQuestionId(item.Id).Data;
            }

            return new SuccessDataResult<ExamDTO>(exam);
        }

        private IDataResult<ExamDTO> Get(int id)
        {
            Exam entity = _examDA.Get(x => x.Id == id);
            ExamDTO exam = _mapper.Map<ExamDTO>(entity);

            return new SuccessDataResult<ExamDTO>(exam);
        }

        public void Delete(int id)
        {
            ExamDTO exam = Get(id).Data;
            Exam entity = _mapper.Map<Exam>(exam);
            _examDA.Delete(entity);
        }
    }
}
