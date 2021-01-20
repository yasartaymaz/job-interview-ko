using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class MenuDTO : IDTO
    {
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
        public bool ShowOnMenu { get; set; }
    }
}
