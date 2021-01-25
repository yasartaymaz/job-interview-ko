using Core.Constants;
using Core.Utilities;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UIWeb.HttpContextHelpers;

namespace UIWeb.ActionFilters
{
    public class VisitingAF : IActionFilter
    {
        private readonly ISessionService _sessionService;
        private IHttpContextAccessor _httpContextAccessor;


        public VisitingAF(ISessionService sessionService, IHttpContextAccessor httpContextAccessor)
        {
            _sessionService = sessionService;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = context.Controller as Controller;
            if (controller == null) return;

            #region urlStuff

            string currentHost = controller.HttpContext.Request.Host.Value;
            string currentUrl = controller.Request.GetDisplayUrl();
            string urlWithoutHost = null;
            controller.ViewBag.Localhost = false;
            controller.ViewBag.BaseUrl = "http://" + currentHost + "/";
            controller.ViewBag.CurrentUrl = currentUrl;
            urlWithoutHost = currentUrl.Replace(controller.ViewBag.BaseUrl, "");
            controller.ViewBag.UrlWithoutHost = urlWithoutHost;

            #endregion

            #region urlNames

            string controllerName = context.RouteData.Values["controller"].ToString().ToLower(new CultureInfo(LanguageCodes.EnUs));
            string actionName = context.RouteData.Values["action"].ToString().ToLower(new CultureInfo(LanguageCodes.EnUs));
            string pageName = controllerName + "." + actionName;

            controller.ViewBag.PageName = pageName;
            controller.ViewBag.ControllerName = controllerName;
            controller.ViewBag.ActionName = actionName;

            #endregion

            #region token&session stuff

            List<string> pagesPermittedWhenSignedOut = new List<string> { "sign.in" };
            List<string> pagesNotPermittedWhenSignedIn = new List<string> { "sign.in" };
            string token = _httpContextAccessor.HttpContext.Session.GetString(SessionTexts.AuthToken);
            if (String.IsNullOrEmpty(token))
            {
                if (Tools.IsObjectNullOrEmpty(pagesPermittedWhenSignedOut.FirstOrDefault(x => x.Contains(pageName))))
                {
                    _sessionService.Create(5);
                    context.Result = new RedirectResult("~/sign/in");
                    return;
                }
            }
            else
            {
                if (!Tools.IsObjectNullOrEmpty(pagesNotPermittedWhenSignedIn.FirstOrDefault(x => x.Contains(pageName))))
                {
                    _sessionService.Create(4);
                    context.Result = new RedirectResult("~/home/index");
                    return;
                }
                controller.ViewBag.AuthToken = _httpContextAccessor.HttpContext.Session.GetString(SessionTexts.AuthToken);
            }

            #endregion

            #region systemMessageStuff

            if (context.HttpContext.Session.GetString(SessionTexts.SM) != null)
            {
                controller.ViewBag.ScriptSystemMessage = _sessionService.InitScript(context.HttpContext.Session.GetString(SessionTexts.SM)).Data;

                if (pageName != "sign.out")
                {
                    _sessionService.Flush();
                }
            }

            #endregion

            #region titles

            List<MenuDTO> menuList = new List<MenuDTO>();
            menuList.Add(new MenuDTO() { PageName = "sign.in", PageTitle = "Giriş", ShowOnMenu = false });
            menuList.Add(new MenuDTO() { PageName = "home.index", PageTitle = "Anasayfa", Url = "home/index", ShowOnMenu = true });
            menuList.Add(new MenuDTO() { PageName = "article.get", PageTitle = "Makale Çek", Url = "article/get", ShowOnMenu = true });
            menuList.Add(new MenuDTO() { PageName = "exam.create", PageTitle = "Sınav Oluştur", Url = "exam/create", ShowOnMenu = true });
            menuList.Add(new MenuDTO() { PageName = "exam.list", PageTitle = "Sınav Listesi", Url = "exam/list", ShowOnMenu = true });
            menuList.Add(new MenuDTO() { PageName = "exam.take", PageTitle = "Sınava Gir", Url = "exam/take", ShowOnMenu = true });
            menuList.Add(new MenuDTO() { PageName = "exam.myfinishedlist", PageTitle = "Tamamladığım Sınavlar", Url = "exam.myfinishedlist", ShowOnMenu = true });
            menuList.Add(new MenuDTO() { PageName = "exam.createexamfromarticle", PageTitle = "Makaleden Sınav Oluşturma", Url = "exam.createexamfromarticle", ShowOnMenu = false });

            string pageTitle = "";
            if (!Tools.IsObjectNullOrEmpty(menuList.FirstOrDefault(x => x.PageName == pageName)))
            {
                pageTitle = menuList.FirstOrDefault(x => x.PageName == pageName).PageTitle;
                menuList.FirstOrDefault(x => x.PageName == pageName).Active = true;
            }

            controller.ViewBag.Menu = menuList.Where(x=>x.ShowOnMenu==true).ToList();
            controller.ViewBag.PageTitle = pageTitle;
            #endregion
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Controller controller = context.Controller as Controller;
            if (controller == null) return;
        }
    }
}
