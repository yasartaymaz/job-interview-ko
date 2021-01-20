﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class TakenExamAnswer : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int Correct { get; set; }
    }
}
