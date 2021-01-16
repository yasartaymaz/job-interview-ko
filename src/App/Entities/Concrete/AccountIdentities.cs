using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AccountIdentities : IEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
    }
}
