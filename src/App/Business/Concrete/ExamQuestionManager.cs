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
    public class ExamQuestionManager : IExamQuestionService
    {
        private readonly IExamQuestionDA _examQuestionDA;
        private readonly IMapper _mapper;

        public ExamQuestionManager(IExamQuestionDA examQuestionDA, IMapper mapper)
        {
            _examQuestionDA = examQuestionDA;
            _mapper = mapper;
        }

        public IDataResult<ExamQuestionDTO> Insert(ExamQuestionDTO input)
        {
            ExamQuestion entity = _mapper.Map<ExamQuestion>(input);
            entity = _examQuestionDA.Insert(entity);
            input = _mapper.Map<ExamQuestionDTO>(entity);

            return new SuccessDataResult<ExamQuestionDTO>(input);
        }

        public IDataResult<List<ExamQuestionDTO>> GetListByExamId(int id)
        {
            List<ExamQuestion> entities = _examQuestionDA.GetList(x => x.ExamId == id);
            List<ExamQuestionDTO> dto = _mapper.Map<List<ExamQuestionDTO>>(entities);

            return new SuccessDataResult<List<ExamQuestionDTO>>(dto);
        }
    }
}
