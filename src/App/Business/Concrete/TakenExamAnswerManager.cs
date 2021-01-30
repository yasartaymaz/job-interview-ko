using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class TakenExamAnswerManager : ITakenExamAnswerService
    {
        private readonly ITakenExamAnswerDA _takenExamAnswerDA;
        private readonly IMapper _mapper;
        public TakenExamAnswerManager(ITakenExamAnswerDA takenExamAnswerDA, IMapper mapper)
        {
            _takenExamAnswerDA = takenExamAnswerDA;
            _mapper = mapper;
        }

        public void Insert(TakenExamAnswerDTO answer)
        {
            TakenExamAnswer entity = _mapper.Map<TakenExamAnswer>(answer);
            _takenExamAnswerDA.Insert(entity);
        }
    }
}
