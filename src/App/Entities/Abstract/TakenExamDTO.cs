using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class TakenExamDTO : IDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ExamId { get; set; }
        public string ArticleHeader { get; set; }
    }
}
