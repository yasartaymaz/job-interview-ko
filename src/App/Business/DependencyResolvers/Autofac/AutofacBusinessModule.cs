using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Token;
using Core.Utilities.Security.Token.StringToken;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region business layer
            builder.RegisterType<AccountManager>().As<IAccountService>();
            builder.RegisterType<SystemMessageManager>().As<ISystemMessageService>();
            #endregion

            #region data access layer
            builder.RegisterType<SystemMessageDA>().As<ISystemMessageDA>();
            builder.RegisterType<AccountDA>().As<IAccountDA>();
            #endregion

            #region core layer

            builder.RegisterType<StringTokenHelper>().As<ITokenHelper>();

            #endregion
        }
    }
}
