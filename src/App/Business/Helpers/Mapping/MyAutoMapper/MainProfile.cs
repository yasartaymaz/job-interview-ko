﻿using AutoMapper;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers.Mapping.MyAutoMapper
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();

            CreateMap<AccountIdentity, AccountIdentityDTO>();
            CreateMap<AccountIdentityDTO, AccountIdentity>();

            CreateMap<Article, ArticleDTO>();
            CreateMap<ArticleDTO, Article>();
        }
    }
}
