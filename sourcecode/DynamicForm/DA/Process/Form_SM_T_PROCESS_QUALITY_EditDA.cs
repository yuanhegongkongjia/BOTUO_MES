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
    public class Form_SM_T_PROCESS_QUALITY_EditDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "SM_T_PROCESS_QUALITY"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            return new { PKId = entity["PKId"] };
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            //BLL_SM_ProductREgistry
            var sql = @"select distinct cast(pp.XianJing as nvarchar(50)) as GG,pp.Spec as GGXH ,sc.GH,
sc.MaxTYD,
sc.QDStandard,sc.QDRange,sc.QDPreControlRange,sc.QDQualifiedRange,
sc.MSStandard,sc.MSRange,sc.MSPreControlRange,sc.MSQualifiedRange,
sc.gkgStandard,sc.gkgRange,sc.gkgPreControlRange,sc.gkgQualifiedRange,
sc.CuStandard,sc.CuRange,sc.CuPreControlRange,sc.CuQualifiedRange,
sc.BCuStandard,sc.MinBCu,sc.BCuQualifiedRange,sc.BCuPreControlRange,
pq.CustomerCode,pq.Spec,pq.QDLevel,pq.AverQD,pq.MaxQD,pq.MinQD
from sm_t_process_product pp
left join smlink.BLL_SM_ProductREgistry.dbo.SM_InspectStandardConfigure sc on pp.XianJing=cast(sc.GG as decimal(18,2)) and pp.Spec=sc.GGXH and PP.GangHao=sc.GH
left join sm_t_process_quality pq on pp.InstanceId=pq.InstanceId and pq.GG=pp.XianJing and pq.GGXH=pp.Spec and pq.GH=pp.GangHao

where 1=1 and pp.InstanceId=@InstanceId";
            var param = new
            {
                InstanceId = entity["InstanceId"]
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by GG", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            //线径信息填写

            var list = base.GetGridClientData<SM_T_PROCESS_QUALITY>(entity, "grid1");
            //if (list.Count == 0)
            //{
            //    throw new WFException("没有为订单分配周转箱");
            //}
            base.Delete("delete from  SM_T_PROCESS_QUALITY where InstanceId=@InstanceId", new { InstanceId = entity["InstanceId"] });

            foreach (var l in list)
            {
                var dEntity = new DFDictionary();
                dEntity = l.ToDFDictionary();
                dEntity.Add("InstanceId", entity["InstanceId"]);



                CheckData("SM_T_PROCESS_QUALITY", dEntity, CurrentUserName);

                SaveData("SM_T_PROCESS_QUALITY", dEntity, IMPORT_TYPE_INSERT);
            }

            message = "保存成功";
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();
            return DFPub.EXECUTE_SUCCESS;
            //if (string.IsNullOrWhiteSpace(entity["EditMode"]))
            //{
            //    return Insert(form, entity, ref message);
            //}
            //var oldEntity = Get(GetSelectSql(TableName), GetParam(entity));
            //if (oldEntity == null)
            //{
            //    throw new WFException("要更新的记录不存在".GetRes());
            //}
            //CheckInput(form, entity);
            //var newEntity = oldEntity.ToDFDictionary().Merge(entity);

            //CheckData(TableName, newEntity, CurrentUserName);
            //using (var db = Pub.DB)
            //{
            //    db.Update<SM_T_PROCESS_QUALITY>(newEntity.To<SM_T_PROCESS_QUALITY>());
            //}
            //message = "保存成功";
            //return DFPub.EXECUTE_SUCCESS;
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
                db.Insert<SM_T_PROCESS_QUALITY>(newEntity.To<SM_T_PROCESS_QUALITY>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
