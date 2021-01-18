using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIWeb.HttpContextHelpers;

namespace UIWeb.ActionFilters
{
    public class VisitingAF
    {
        private readonly ISessionService _sessionService;

        public VisitingAF(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller == null) return;


        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as Controller;
            if (controller == null) return;
        }
    }
}
