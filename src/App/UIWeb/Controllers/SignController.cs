using Business.Abstract;
using Core.Utilities;
using Core.Utilities.Results;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIWeb.HttpContextHelpers;

namespace UIWeb.Controllers
{
    public class SignController : Controller
    {
        private readonly ISignService _signService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISessionService _sessionService;

        public SignController(ISignService signService, IHttpContextAccessor httpContextAccessor, ISessionService sessionService)
        {
            _signService = signService;
            _httpContextAccessor = httpContextAccessor;
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            return Redirect("~/sign/in");
        }

        public IActionResult In()
        {
            return View();
        }

        [HttpPost]
        public IActionResult In(SignInDTO form)
        {
            IDataResult<SignInDTO> result = _signService.SignIn(form);//bu satırda "var result" olarak da kullabilirdik ama tip belirtmeyi daha cok tercih ediyorum.
            if (Tools.IsObjectNullOrEmpty(result.Data))
            {
                _sessionService.CheckMessages(result.Messages);
                return Redirect("~/sign/in");
            }

            _httpContextAccessor.HttpContext.Session.SetString(Core.Constants.SessionTexts.AuthToken, result.Data.Token);
            _sessionService.CheckMessages(result.Messages);
            return Redirect("~/home/index");
        }

        public IActionResult Out()
        {
            _httpContextAccessor.HttpContext.Session.Remove(Core.Constants.SessionTexts.AuthToken);
            return Redirect("~/sign/in");
        }
    }
}
