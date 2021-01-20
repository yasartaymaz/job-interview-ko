using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Business.Concrete
{
    public class AccountIdentityManager : IAccountIdentityService
    {
        private readonly IAccountIdentityDA _accountIdentityDA;
        private readonly IMapper _mapper;

        public AccountIdentityManager(IAccountIdentityDA accountIdentityDA, IMapper mapper)
        {
            _accountIdentityDA = accountIdentityDA;
            _mapper = mapper;
        }

        public IDataResult<AccountIdentityDTO> GetByEmail(string email)
        {
            AccountIdentityDTO dto = null;
            AccountIdentity entity = _accountIdentityDA.Get(x => x.Email == email);
            dto = _mapper.Map<AccountIdentityDTO>(entity);

            return new SuccessDataResult<AccountIdentityDTO>(dto);
        }

        public IDataResult<AccountIdentityDTO> GetByEmailAndPassword(AccountIdentityDTO input, string passwordToCheck)
        {
            if (input.Password == passwordToCheck)
            {
                return new SuccessDataResult<AccountIdentityDTO>(input);
            }

            return new ErrorDataResult<AccountIdentityDTO>();
        }
    }
}
