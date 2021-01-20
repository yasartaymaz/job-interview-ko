using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class ExamDTO : IDTO
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
    }
}
