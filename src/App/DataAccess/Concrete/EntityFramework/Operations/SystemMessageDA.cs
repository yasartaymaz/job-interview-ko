using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Operations
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
