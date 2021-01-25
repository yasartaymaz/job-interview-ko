using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Operations
{
    public class ArticleDA : EfEntityRepositoryBase<Article, MainContext>, IArticleDA
    {
        public List<Article> GetLastDescending(int take)
        {
            using (MainContext db = new MainContext())
            {
                List<Article> result = null;
                result = db.Articles.OrderByDescending(x => x.Id).Skip(0).Take(take).ToList();

                return result;
            }
        }
    }
}
