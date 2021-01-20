using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class TakenExam : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ExamId { get; set; }
    }
}
