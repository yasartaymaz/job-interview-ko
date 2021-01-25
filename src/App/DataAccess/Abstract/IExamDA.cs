using Core.DataAccess;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IExamDA : IEntityRepository<Exam>
    {
        List<ExamDTO> GetListWithArticle();
    }
}
