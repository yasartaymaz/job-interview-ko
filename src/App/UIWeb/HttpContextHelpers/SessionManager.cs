using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIWeb.HttpContextHelpers
{
    public class SessionManager : ISessionService
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly ISystemMessageService _systemMessageService;

        public SessionManager(IHttpContextAccessor httpContextAccessor, ISystemMessageService systemMessageService)
        {
            this._httpContextAccessor = httpContextAccessor;
            _systemMessageService = systemMessageService;
        }

        public void Create(int id)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            List<SystemMessageDTO> ListSMM = new List<SystemMessageDTO>();
            SystemMessageDTO SMM = new SystemMessageDTO() { Id = id };
            if (httpContext.Session.GetString(SessionTexts.SM) == "")
            {
                ListSMM.Add(SMM);
            }
            else
            {
                if (httpContext.Session.GetString(SessionTexts.SM) != null)
                {
                    ListSMM = JsonConvert.DeserializeObject<List<SystemMessageDTO>>(httpContext.Session.GetString(SessionTexts.SM));
                }
                ListSMM.Add(SMM);
            }
            string sessionSmData = JsonConvert.SerializeObject(ListSMM);
            httpContext.Session.SetString(SessionTexts.SM, sessionSmData);
        }

        public void Flush()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Session.SetString(SessionTexts.SM, "");
        }

        public List<SystemMessageDTO> GetAll(List<SystemMessageDTO> SM)
        {
            List<SystemMessageDTO> ListSMM = new List<SystemMessageDTO>();
            foreach (SystemMessageDTO item in SM)
            {
                SystemMessageDTO vm = null;
                vm = _systemMessageService.Get(item.Id).Data;
                if (vm != null)
                {
                    ListSMM.Add(vm);
                }
            }
            return ListSMM;
        }

        public void CheckMessages(List<int> messages)
        {
            if (messages != null && messages.Count() > 0)
            {
                foreach (int item in messages)
                {
                    Create(item);
                }
            }
        }

        public void Clear()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public IDataResult<string> InitScript(string v)
        {
            var context = _httpContextAccessor.HttpContext;
            StringBuilder scriptSystemMessage = new StringBuilder();
            string smmText = context.Session.GetString(SessionTexts.SM);

            List<SystemMessageDTO> listSystemMessages = JsonConvert.DeserializeObject<List<SystemMessageDTO>>(smmText);
            if (listSystemMessages != null)
            {
                listSystemMessages = GetAll(listSystemMessages);
                int countSystemMessages = 1;

                foreach (SystemMessageDTO item in listSystemMessages)
                {
                    if (countSystemMessages == 1)
                    {
                        scriptSystemMessage.Append("<script type=\"text/javascript\">");
                    }

                    scriptSystemMessage.Append("toastr." + item.TypeStyleValue + "('" + item.Value + "', '" + item.TypeValue + "', { timeOut: 5000});");

                    if (countSystemMessages == listSystemMessages.Count())
                    {
                        scriptSystemMessage.Append("</script>");
                    }
                    countSystemMessages++;
                }
            }

            return new SuccessDataResult<string>(scriptSystemMessage.ToString());
        }
    }
}
