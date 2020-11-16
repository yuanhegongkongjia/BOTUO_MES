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
    public class Form_BT_PaiGongDan_OP_EditDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "BT_PaiGongDan"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            //return new {ID = entity["ID"] };
            return new { CreateTime = entity["CreateTime"] };
        }

        //public override void SetAccess(FormM form, DFDictionary entity)
        //{
        //    //base.SetAccess(form, entity);
        //    var CurrentUser = Util.GetCurrentUser();
        //    // var ID = WF_T_SEQNOLoader.GenerateJobNo("G", DateTime.Now.ToString("yyyyMMdd"));
        //    var ID = WF_T_SEQNOLoader.GenerateJobNo("G","0", "0");
        //    this.Model.Add("ID", ID);
        //}
    //    public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
    //    {
    //        //var count = 0;
    //        //var sql = @"select * from SM_T_PROCESS where 1=1";
    //        //var param = new
    //        //{
    //        //    InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
    //        //};
    //        //var list = GetList(entity, ref count, start, limit, sql, "order by InstanceId", param);
    //        //vm.results = count;
    //        //vm.rows = list;
    //        //return DFPub.EXECUTE_SUCCESS;

    //        if (entity["GridId"] == "grid2")
    //        {
    //            //查询生产信息
    //            QueryGridProduct(form, entity, vm, start, limit);
    //        }

        
    //         return DFPub.EXECUTE_SUCCESS;
    //    }
    //public void QueryGridProduct(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
    //        {
    //            var count = 0;
    //            var sql = "select * from BT_PaiGongDan where 1=1";
    //            var param = new
    //            {
    //                ID = QueryBuilder.Like(ref sql, entity, "ID", "ID")
    //            };
    //            //从存储过程得到信息
    //            var list = GetList(entity, ref count, start, limit, sql, "order by ID", param);


    //            vm.results = count;
    //            vm.rows = list;

    //        }



       
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (string.IsNullOrWhiteSpace(entity["EditMode"]))
            {
                return Insert(form, entity, ref message);
            }
            var oldEntity = Get(GetSelectSql(TableName), GetParam(entity));
            if (oldEntity == null)
            {
                throw new WFException("要更新的记录不存在".GetRes());
            }
            CheckInput(form, entity);
            var newEntity = oldEntity.ToDFDictionary().Merge(entity);

            CheckData(TableName, newEntity, CurrentUserName);
            using (var db = Pub.DB)
            {
                db.Update<BT_PaiGongDan>(newEntity.To<BT_PaiGongDan>());
            }
            message = "保存成功";
            return DFPub.EXECUTE_SUCCESS;
        }
        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            var item = Get(GetSelectSql(TableName), GetParam(entity));
            if (item != null)
            {
                throw new WFException("记录已经存在".GetRes());
            }
            CheckInput(form, entity);
            var newEntity = entity;
            CheckData(TableName, newEntity, CurrentUserName);
            using (var db = Pub.DB)
            {
                db.Insert<BT_PaiGongDan>(newEntity.To<BT_PaiGongDan>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
