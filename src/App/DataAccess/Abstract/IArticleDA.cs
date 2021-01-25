using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IArticleDA : IEntityRepository<Article>
    {
        List<Article> GetLastDescending(int take);
    }
}
