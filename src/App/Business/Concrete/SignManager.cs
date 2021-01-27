using Business.Abstract;
using Core.Utilities;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Token;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class SignManager : ISignService
    {
        private readonly IAccountIdentityService _accountIdentityService;
        private readonly IAccountService _accountService;
        private readonly ITokenHelper _tokenHelper;
        public SignManager(IAccountIdentityService accountIdentityService, IAccountService accountService, ITokenHelper tokenHelper)
        {
            _accountIdentityService = accountIdentityService;
            _accountService = accountService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<SignInDTO> SignIn(SignInDTO input)
        {
            AccountIdentityDTO accountIdentity = _accountIdentityService.GetByEmail(input.Email).Data;
            if (Tools.IsObjectNullOrEmpty(accountIdentity))
            {
                return new ErrorDataResult<SignInDTO>(new List<int> { 2 });
            }

            string passwordToCheck= HashingHelper.CreateCryptedText(input.Password, accountIdentity.SecretKey);
            accountIdentity = _accountIdentityService.GetByEmailAndPassword(accountIdentity, passwordToCheck).Data;

            if (Tools.IsObjectNullOrEmpty(accountIdentity))
            {
                return new ErrorDataResult<SignInDTO>(new List<int> { 3 });
            }

            string token = _tokenHelper.CreateToken(40, false);
            input.Token = token;
            input.AccountId = accountIdentity.AccountId;

            return new SuccessDataResult<SignInDTO>(input, new List<int> { 1 });
        }
    }
}
