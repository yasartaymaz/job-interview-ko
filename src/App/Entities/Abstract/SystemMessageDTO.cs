using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class SystemMessageDTO : IDTO
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Value { get; set; }
        public string TypeValue { get; set; }
        public string TypeStyleValue { get; set; }
    }
}
