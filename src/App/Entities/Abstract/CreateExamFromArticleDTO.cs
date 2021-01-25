using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class CreateExamFromArticleDTO : IDTO
    {
        public int ArticleId { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
