using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class ExamQuestionDTO : IDTO
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Value { get; set; }
    }
}
