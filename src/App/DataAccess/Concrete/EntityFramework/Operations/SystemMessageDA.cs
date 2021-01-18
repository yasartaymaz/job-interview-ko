using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System.Linq;
using Entities.Abstract;

namespace DataAccess.Concrete.Main.EntityFramework.Operations
{
    public class SystemMessageDA : EfEntityRepositoryBase<SystemMessage, MainContext>, ISystemMessageDA
    {
        public SystemMessageDTO GetWithType(int id)
        {
            SystemMessageDTO result = null;
            using (MainContext db = new MainContext())
            {
                result = (from sm in db.SystemMessages
                          join smt in db.SystemMessageTypes on sm.TypeId equals smt.Id
                          where sm.Id == id
                          select new SystemMessageDTO
                          {
                              Id = sm.Id,
                              TypeId = sm.TypeId,
                              Value = sm.Value,
                              TypeValue = smt.Value,
                              TypeStyleValue = smt.StyleValue
                          }
                          ).FirstOrDefault();
            }
            return result;
        }
    }
}
