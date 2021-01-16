using Autofac;
using Business.Abstract;
using Business.Concrete;
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

            #endregion

            #region dataAccess layer

            builder.RegisterType<EfAccountDA>().As<IAccountDA>();

            #endregion

            base.Load(builder);
        }
    }
}
