using Core.Utilities.Results;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIWeb.HttpContextHelpers
{
    public interface ISessionService
    {
        void Create(int id);

        void Flush();

        List<SystemMessageDTO> GetAll(List<SystemMessageDTO> SM);

        void CheckMessages(List<int> messages);

        void Clear();

        IDataResult<string> InitScript(string v);
    }
}
