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

        public ExamQuestionAnswerManager(IExamQuestionAnswerDA examQuestionAnswerDA, IMapper mapper)
        {
            _examQuestionAnswerDA = examQuestionAnswerDA;
            _mapper = mapper;
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
    }
}
