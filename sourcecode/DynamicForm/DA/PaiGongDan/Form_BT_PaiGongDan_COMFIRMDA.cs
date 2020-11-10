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
    public class Form_BT_PaiGongDan_COMFIRMDA : CommonBaseDA
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
            return new { ID = entity["ID"] };
            //return new { ORDER_NUMBER = entity["ORDER_NUMBER"] };
        }
        //public override void SetAccess(FormM form, DFDictionary entity)
        //{
        //    //base.SetAccess(form, entity);
        //    // var CurrentUser = Util.GetCurrentUser();
        //    var ORDER_NUMBER = WF_T_SEQNOLoader.GenerateJobNo("G", DateTime.Now.ToString("yyyyMMdd"));
        //    //var ID = WF_T_SEQNOLoader.UpdateJobNo("0","0","0","0","0");
        //    //var ID = WF_T_SEQNOLoader.GenerateJobNo("G", "0");
        //    this.Model.Add("ORDER_NUMBER", ORDER_NUMBER);
        //}


        private void Submit(DynamicForm.Core.DFDictionary entity, ref string message)
        {
            //var user = Util.GetCurrentUser();
           // var page = ((this.Parent as ucForm).Page as DFIndexWF);
            // 如果是提交按钮，业务 ucForm1 保存成功的话，就可以进行提交
            //if (base.GetSubmitButton(entity) == "btnSubmit" && page.UcForm1.DA.ExecuteResult == DFPub.EXECUTE_SUCCESS)
            //{
                
           // var InstanceStepExecutorId = entity["InstanceStepExecutorId"];
               // if (!string.IsNullOrWhiteSpace(InstanceStepExecutorId))
               // {
                    //new StateEngineHelper().AutoFlow(InstanceStepExecutorId, "提交".GetRes(), user.UserId, user.UserName);
              base.WriteScript(string.Format("alert('提交成功，本窗口将自动关闭！');window.top.close();"), ref message);
               // }

            //update SeqNo
            var str = entity["ORDER_NUMBER"].Split('-');
            WF_T_SEQNOLoader.UpdateJobNo(str[0], str[1], str[2]);
            message = "保存成功";
            //return DFPub.EXECUTE_SUCCESS;


            //}
        }



        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            Submit(entity, ref message);
            return base.Update(form, entity, ref message);

       
        }


        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            //var count = 0;
            //var sql = @"select * from SM_T_PROCESS where 1=1";
            //var param = new
            //{
            //    InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            //};
            //var list = GetList(entity, ref count, start, limit, sql, "order by InstanceId", param);
            //vm.results = count;
            //vm.rows = list;
            //return DFPub.EXECUTE_SUCCESS;

            if (entity["GridId"] == "grid2")
            {
                //查询生产信息
                QueryGridProduct(form, entity, vm, start, limit);
            }


            return DFPub.EXECUTE_SUCCESS;
        }
        public void QueryGridProduct(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from BT_PaiGongDan where 1=1";
            var param = new
            {
                ORDER_NUMBER = QueryBuilder.Like(ref sql, entity, "ORDER_NUMBER", "ORDER_NUMBER")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by ORDER_NUMBER", param);


            vm.results = count;
            vm.rows = list;

        }

        //public override int Update(FormM form, DFDictionary entity, ref string message)
        //{
        //    if (string.IsNullOrWhiteSpace(entity["EditMode"]))
        //    {
        //        return Insert(form, entity, ref message);
        //    }
        //    var oldEntity = Get(GetSelectSql(TableName), GetParam(entity));
        //    if (oldEntity == null)
        //    {
        //        throw new WFException("要更新的记录不存在".GetRes());
        //    }
        //    CheckInput(form, entity);
        //    var newEntity = oldEntity.ToDFDictionary().Merge(entity);

        //    CheckData(TableName, newEntity, CurrentUserName);
        //    using (var db = Pub.DB)
        //    {
        //        db.Insert<BT_PaiGongDan>(newEntity.To<BT_PaiGongDan>());
        //    }
        //    message = "保存成功";
        //    return DFPub.EXECUTE_SUCCESS;
        //}


 



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
                //db.Insert<BT_PaiGongDan>(newEntity.To<BT_PaiGongDan>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
