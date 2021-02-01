using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework.Operations
{
    public class TakenExamDA : EfEntityRepositoryBase<TakenExam, MainContext>, ITakenExamDA
    {
    }
}
