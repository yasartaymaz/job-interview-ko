using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IExamService
    {
        IResult CreateExam(List<CreateExamFromArticleDTO> questions);
        
        IDataResult<List<ExamDTO>> GetList();

        IDataResult<ExamDTO> GetExamFull(int id);
        
        void Delete(int id);

        IDataResult<TakeExamDTO> TakeExam(TakeExamDTO takeExamDto);
    }
}
