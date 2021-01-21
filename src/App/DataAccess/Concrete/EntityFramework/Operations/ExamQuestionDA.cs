using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Operations
{
    public class ExamQuestionDA : EfEntityRepositoryBase<ExamQuestion, MainContext>, IExamQuestionDA
    {
    }
}
