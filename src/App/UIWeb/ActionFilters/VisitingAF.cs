using Core.Constants;
using Core.Utilities;
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

            #region UrlStuff

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
            string token = _httpContextAccessor.HttpContext.Session.GetString(SessionTexts.AuthToken);
            if (String.IsNullOrEmpty(token))
            {
                if (Tools.IsObjectNullOrEmpty(pagesPermittedWhenSignedOut.FirstOrDefault(x => x.Contains(pageName))))
                {
                    context.Result = new RedirectResult("~/sign/in");
                    return;
                }
            }
            else
            {
                controller.ViewBag.AuthToken = _httpContextAccessor.HttpContext.Session.GetString(SessionTexts.AuthToken);
            }

            #endregion
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Controller controller = context.Controller as Controller;
            if (controller == null) return;
        }
    }
}
