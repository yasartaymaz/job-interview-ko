using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SystemMessageType : IEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string StyleValue { get; set; }
    }
}
