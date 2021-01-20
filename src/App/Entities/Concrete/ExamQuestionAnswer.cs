using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ExamQuestionAnswer : IEntity
    {
        public int MyProperty { get; set; }
        public int QuestionId { get; set; }
        public string Value { get; set; }
        public int Correct { get; set; }
    }
}
