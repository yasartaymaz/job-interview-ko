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

        public ExamManager(IExamDA examDA, IMapper mapper, IExamQuestionService examQuestionService)
        {
            _examDA = examDA;
            _mapper = mapper;
            _examQuestionService = examQuestionService;
        }

        public IResult CreateExam(List<CreateExamFromArticleDTO> questions)
        {
            if (Tools.IsObjectNullOrEmpty(questions))
            {
                return new ErrorResult(new List<int> { 8 });
            }
            
            //insert exam from article
            int articleId = questions.First().ArticleId;
            ExamDTO examDto = new ExamDTO
            {
                ArticleId=articleId
            };
            examDto = Insert(examDto).Data;

            //insert question
            foreach (CreateExamFromArticleDTO question in questions)
            {
                ExamQuestionDTO examQuestion = new ExamQuestionDTO
                {
                    ExamId = examDto.Id,
                    Value = question.Question
                };
                examQuestion = _examQuestionService.Insert(examQuestion).Data;

                foreach (string answer in question.Answers)
                {
                    //insert answer
                }
            }


            


            return new SuccessResult(new List<int> { 7 });
        }

        public IDataResult<ExamDTO> Insert(ExamDTO input)
        {
            Exam entity = _mapper.Map<Exam>(input);
            entity= _examDA.Insert(entity);
            input = _mapper.Map<ExamDTO>(entity);

            return new SuccessDataResult<ExamDTO>(input);
        }
    }
}
