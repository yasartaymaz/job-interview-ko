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
    public class ExamDA : EfEntityRepositoryBase<Exam, MainContext>, IExamDA
    {
        public List<ExamDTO> GetListWithArticle()
        {
            using (MainContext db = new MainContext())
            {
                List<ExamDTO> result = db.Exams.Select(x => new ExamDTO
                {
                    Id = x.Id,
                    ArticleId = x.ArticleId,
                    ArticleHeader = db.Articles.FirstOrDefault().Header//defensive programming dikkate alınarak yazıldıgında bu satırı exammanagerda bu metodun sonucu snrasında article servisine baglanıp da cekilebilir.
                }).ToList();

                return result;
            }
        }
    }
}
