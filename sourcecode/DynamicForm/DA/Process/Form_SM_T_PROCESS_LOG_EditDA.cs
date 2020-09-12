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
    public class Form_SM_T_PROCESS_LOG_EditDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "SM_T_PROCESS_YC"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            return new { PKId = entity["PKId"] };
        }

        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {

            using (var db = Pub.DB)
            {
                var sql = "select InstanceId,ADC,ATJC,AIzn,AMF1,AMF2,BDC,BTJC,BIzn,BMF1,BMF2 from v_sm_t_process_cy where InstanceId=@InstanceId";
                var item = db.Query<v_sm_t_process_cy>(sql, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (item != null)
                {
                    entity.Merge(item.ToDFDictionary());
                }

                var sql2 = "select InstanceId,Z1,Z2,Z3,Z4,Z5,Z6 from v_sm_t_process_ly where InstanceId=@InstanceId";
                var item2 = db.Query<v_sm_t_process_ly>(sql2, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (item2 != null)
                {
                    entity.Merge(item2.ToDFDictionary());
                }

                var sql3 = "select InstanceId,AQ1,AQ2,AQ3,AQ4,AQ5,AQ6,AQ7,AQ8,AWT1,BWT1,AKK,BKK,AWT2,BWT2 from sm_t_process_aq where InstanceId=@InstanceId";
                var item3 = db.Query<SM_T_PROCESS_AQ>(sql3, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (item3 != null)
                {
                    entity.Merge(item3.ToDFDictionary());
                }
            }
            return entity;
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

            if (entity["GridId"] == "grid_cy")
            {
                //查询产品质量信息
                QueryGridCY(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_ly")
            {
                //查询产品质量信息
                QueryGridLY(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_aq")
            {
                //查询产品质量信息
                QueryGridAQ(form, entity, vm, start, limit);
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        public void QueryGridLY(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from v_SM_T_PROCESS_LY_LOG where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by LastModifyTime desc", param);


            vm.results = count;
            vm.rows = list;

        }

        public void QueryGridAQ(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from SM_T_PROCESS_AQ_LOG where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by LastModifyTime desc", param);


            vm.results = count;
            vm.rows = list;

        }

        public void QueryGridCY(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from v_sm_t_process_cy_log where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by LastModifyTime desc", param);


            vm.results = count;
            vm.rows = list;

        }

    }
}