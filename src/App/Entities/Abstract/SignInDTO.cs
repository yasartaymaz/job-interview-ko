using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class SignInDTO : IDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int VisitId { get; set; }
        public int VisitDetailId { get; set; }
        public string Token { get; set; }
    }
}
