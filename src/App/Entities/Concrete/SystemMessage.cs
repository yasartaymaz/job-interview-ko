using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SystemMessage : IEntity
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Value { get; set; }
    }
}
