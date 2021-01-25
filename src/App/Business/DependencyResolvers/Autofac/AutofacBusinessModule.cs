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
            builder.RegisterType<SignManager>().As<ISignService>();
            builder.RegisterType<AccountIdentityManager>().As<IAccountIdentityService>();
            builder.RegisterType<ArticleManager>().As<IArticleService>();
            builder.RegisterType<ExamManager>().As<IExamService>();
            builder.RegisterType<ExamQuestionManager>().As<IExamQuestionService>();
            builder.RegisterType<ExamQuestionAnswerManager>().As<IExamQuestionAnswerService>();
            #endregion

            #region data access layer
            builder.RegisterType<SystemMessageDA>().As<ISystemMessageDA>();
            builder.RegisterType<AccountDA>().As<IAccountDA>();
            builder.RegisterType<AccountIdentityDA>().As<IAccountIdentityDA>();
            builder.RegisterType<ArticleDA>().As<IArticleDA>();
            builder.RegisterType<ExamDA>().As<IExamDA>();
            builder.RegisterType<ExamQuestionDA>().As<IExamQuestionDA>();
            builder.RegisterType<ExamQuestionAnswerDA>().As<IExamQuestionAnswerDA>();
            #endregion

            #region core layer

            builder.RegisterType<StringTokenHelper>().As<ITokenHelper>();

            #endregion
        }
    }
}
