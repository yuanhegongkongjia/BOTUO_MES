using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class Sample_SelectPostBackDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            this.Model.Add("DisplayTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (!string.IsNullOrWhiteSpace(entity["SelectRole"]))
            {
                var list = new List<DFSelectItem>();
                using (var db = Pub.DB)
                {
                    var sql = @"select b.userid as value,b.username as text from wf_m_userrole a 
left outer join wf_m_user b on a.userid=b.userid 
where a.roleid=@roleid and b.userid is not null";
                    list = db.Query<DFSelectItem>(sql, new { roleid = entity["SelectRole"] }).ToList();
                }
                
                SetSelectDataSource(form, "SelectUser", list, false);
            }
            base.SetAccess(form, entity);
        }
    }
}
