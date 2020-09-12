using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFCommon.VM;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class Form_WF_M_USERDEPT_EditDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
        }

        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();

            if (!string.IsNullOrWhiteSpace(entity["UserId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "SELECT * FROM WF_M_USER where UserId=@UserId";
                    var item = db.Query<VM_WF_M_USER>(sql, new { UserId = entity["UserId"] }).FirstOrDefault();
                    if (null != item)
                    {
                        dict.Merge(DFDictionary.Create<VM_WF_M_USER>(item));
                    }
                }
            }
            return dict;


        }


        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var list = WF_M_USERDEPTLoader.GetListForUpdate(entity, ref count, start, limit);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            var currentUser = Util.GetCurrentUser();
            if (string.IsNullOrWhiteSpace(entity["UserId"]))
            {
                throw new ArgumentNullException("UserId");
            }

            var list = JsonSerializeHelper.DeserializeObject<List<VM_WF_M_USERDEPT>>(Base64StringHelper.ConvertFromBase64String(entity[DFPub.GetKey_GridHiddenValue("grid1")]));

            if (list == null)
            {
                throw new Exception("Invalid grid data");
            }

            WF_M_USERDEPTLoader.DeleteByUserId(entity["UserId"]);
            list.Where(a => a.selected).ToList().ForEach(a =>
            {
                var ud = new WF_M_USERDEPT();
                ud.PK_GUID = Guid.NewGuid().ToString();
                ud.DeptId = a.DeptId;
                ud.UserId = entity["UserId"];
                ud.CreateUser = currentUser.UserName;
                ud.CreateTime = DateTime.Now;
                ud.LastModifyUser = currentUser.UserName;
                ud.LastModifyTime = DateTime.Now;
                WF_M_USERDEPTLoader.Insert(ud);
            });
            message = "保存成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;

        }

    }
}
