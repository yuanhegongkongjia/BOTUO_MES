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
    public class Form_SM_T_DAYENERGYDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("delete from SM_T_DAYENERGY where Id=@Id", data.Select(a => new { Id = a["Id"] }).ToList());
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select Line,TotalValue,CollectDate,CollectYear,CollectMonth,Remark1,
                        (case when Category='YANSUAN' then N'盐酸'
                                   when Category='LINSUAN' then N'磷酸'
								   when Category='CHUNJIAN' then N'纯碱'
								   when Category='ZILAISHUI' then N'自来水'
								   when Category='CHUNSHUI' then N'纯水'
								   when Category='ZHENGQI' then N'蒸汽'
								   when Category='TIANRANQI' then N'天然气'
								   when Category='POWER' then N'电'
                                   else   N'其他' end) as Category
                        from SM_T_DAYENERGY where 1=1 and Category!=N'LUYA'";
            var param = new
            {
                Line = QueryBuilder.Like(ref sql, entity, "Line", "Line"),
                CollectDate = QueryBuilder.Like(ref sql, entity, "CollectDate", "CollectDate")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
