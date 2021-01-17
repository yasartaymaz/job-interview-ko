using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountDA _accountDA;

        public AccountManager(IAccountDA accountDA)
        {
            _accountDA = accountDA;
        }

        public IDataResult<AccountDTO> Get(int id)
        {
            Account entities = _accountDA.Get(x => x.Id == id);
            //AccountDTO dto=_map
            return new SuccessDataResult<AccountDTO>();
        }
    }
}
