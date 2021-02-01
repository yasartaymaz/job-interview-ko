using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ExamQuestionAnswerManager : IExamQuestionAnswerService
    {
        private readonly IExamQuestionAnswerDA _examQuestionAnswerDA;
        private readonly IMapper _mapper;
        private readonly IExamQuestionService _examQuestionService;

        public ExamQuestionAnswerManager(IExamQuestionAnswerDA examQuestionAnswerDA, IMapper mapper, IExamQuestionService examQuestionService)
        {
            _examQuestionAnswerDA = examQuestionAnswerDA;
            _mapper = mapper;
            _examQuestionService = examQuestionService;
        }

        public void Insert(ExamQuestionAnswerDTO examQuestionAnswer)
        {
            ExamQuestionAnswer entity = _mapper.Map<ExamQuestionAnswer>(examQuestionAnswer);
            _examQuestionAnswerDA.Insert(entity);
        }

        public IDataResult<List<ExamQuestionAnswerDTO>> GetListByQuestionId(int id)
        {
            List<ExamQuestionAnswer> entities = _examQuestionAnswerDA.GetList(x => x.QuestionId == id);
            List<ExamQuestionAnswerDTO> dto = _mapper.Map<List<ExamQuestionAnswerDTO>>(entities);

            return new SuccessDataResult<List<ExamQuestionAnswerDTO>>(dto);
        }

        public IDataResult<List<ExamQuestionAnswerDTO>> GetListCorrectAnswersByExamId(int examId)
        {
            List<ExamQuestionDTO> questions = _examQuestionService.GetListByExamId(examId).Data;
            List<ExamQuestionAnswerDTO> answers = new List<ExamQuestionAnswerDTO>();
            foreach (ExamQuestionDTO question in questions)
            {
                ExamQuestionAnswer answerRow = _examQuestionAnswerDA.Get(x => x.QuestionId == question.Id && x.Correct == 1);
                ExamQuestionAnswerDTO answerRowDto = _mapper.Map<ExamQuestionAnswerDTO>(answerRow);
                answers.Add(answerRowDto);
            }

            return new SuccessDataResult<List<ExamQuestionAnswerDTO>>(answers);
        }

        public IDataResult<List<ExamQuestionDTO>> GetListByQuestions(List<ExamQuestionDTO> questions)
        {
            foreach (ExamQuestionDTO item in questions)
            {
                item.Answers = GetListByQuestionId(item.Id).Data;
            }

            return new SuccessDataResult<List<ExamQuestionDTO>>(questions);
        }
    }
}
