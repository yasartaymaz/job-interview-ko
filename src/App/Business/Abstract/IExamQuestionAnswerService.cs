using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IExamQuestionAnswerService
    {
        void Insert(ExamQuestionAnswerDTO examQuestionAnswer);

        IDataResult<List<ExamQuestionAnswerDTO>> GetListByQuestionId(int id);

        IDataResult<List<ExamQuestionAnswerDTO>> GetListCorrectAnswersByExamId(int examId);
    }
}
