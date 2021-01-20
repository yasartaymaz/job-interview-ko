using AutoMapper;
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
        private readonly IMapper _mapper;
        public AccountManager(IAccountDA accountDA, IMapper mapper)
        {
            _accountDA = accountDA;
            _mapper = mapper;
        }

        public IDataResult<AccountDTO> Get(int id)
        {
            Account entity = _accountDA.Get(x => x.Id == id);
            AccountDTO dto = _mapper.Map<AccountDTO>(entity);

            return new SuccessDataResult<AccountDTO>(dto);
        }
    }
}
