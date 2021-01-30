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
    public class TakenExamManager : ITakenExamService
    {
        private readonly ITakenExamDA _takenExamDA;
        private readonly ITakenExamAnswerService _takenExamAnswerService;
        public TakenExamManager(ITakenExamDA takenExamDA, ITakenExamAnswerService takenExamAnswerService)
        {
            _takenExamDA = takenExamDA;
            _takenExamAnswerService = takenExamAnswerService;
        }

        public IDataResult<TakeExamDTO> Insert(TakeExamDTO takeExamDTO)
        {
            TakenExam takenExam = new TakenExam
            {
                AccountId = takeExamDTO.AccountId,
                ExamId = takeExamDTO.ExamId
            };
            takenExam = _takenExamDA.Insert(takenExam);
            if (takenExam.Id > 0)
            {
                takeExamDTO.TakenExamId = takenExam.Id;
                foreach (TakenExamAnswerDTO answer in takeExamDTO.TakenAnswers)
                {
                    answer.TakenExamId = takenExam.Id;
                    _takenExamAnswerService.Insert(answer);
                }
            }

            return new SuccessDataResult<TakeExamDTO>(takeExamDTO);
        }
    }
}
