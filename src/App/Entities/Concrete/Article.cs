using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Article : IEntity
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
