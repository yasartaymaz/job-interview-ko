using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Exam : IEntity
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
    }
}
