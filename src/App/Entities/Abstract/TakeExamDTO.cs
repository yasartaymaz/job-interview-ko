using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class TakeExamDTO : IDTO
    {
        public int AccountId { get; set; }
        public int ExamId { get; set; }
        public int TakenExamId { get; set; }
        public List<TakenExamAnswerDTO> TakenAnswers { get; set; }
    }
}
