using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class ArticleDTO : IDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
