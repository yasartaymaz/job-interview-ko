using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IArticleService
    {
        IResult InitArticles(int count);

        IDataResult<List<ArticleDTO>> GetList();

        IDataResult<List<ArticleDTO>> GetLastDescending(int take);

        IDataResult<ArticleDTO> Get(int id);
    }
}
