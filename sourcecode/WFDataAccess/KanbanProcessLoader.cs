using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using WFCore;
using DynamicForm.Core;
using WFCommon;
using WFCommon.Utility;
using System.Text;
using System.Data;
using WFCommon.VM;
namespace WFDataAccess
{
    public class KanbanProcessLoader
    {
        public static List<VM_PROCESS> GetProcssData(string Line, string Position, String ParamName)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,CollectValue as value from SM_T_PROCESS_COLLECT 
                    where Line=@Line and Position=@Position and ParamName=@ParamName
                    and CollectTime>=@CurrentTime";

                return db.Query<VM_PROCESS>(sql,new {Line=Line,Position=Position, ParamName= ParamName,CurrentTime=DateTime.Now.AddMinutes(-260) }).ToList() ;


            }
        }

        public static List<VM_PROCESS> GetMFGL(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,cast(CollectValue as decimal)/1000.0 as value,ParamName as SeriesName from SM_T_PROCESS_COLLECT 
                    where Line=@Line and Position=@Position and ParamName like @ParamName
                   -- and CollectTime>=@CurrentTime 
order by ParamName,CollectTime asc";

                return db.Query<VM_PROCESS>(sql, new { Line = Line, Position = Position, ParamName = "%功率%", CurrentTime = DateTime.Now.AddHours(-1) }).ToList();


            }
        }

        public static List<VM_PROCESS> GetLW(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,cast(CollectValue as decimal) as value,ParamName as SeriesName,ParamType from SM_T_PROCESS_COLLECT 
                    where Line=@Line and Position=@Position and ParamName like @ParamName
                    and CollectTime>=@CurrentTime order by ParamName,CollectTime asc";

                return db.Query<VM_PROCESS>(sql, new { Line = Line, Position = Position, ParamName = "%温度%", CurrentTime = DateTime.Now.AddHours(-1) }).ToList();


            }
        }
        public static List<VM_PROCESS> GetLY(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,
                      cast(CollectValue as decimal) as value,
                    ParamName as SeriesName,ParamType from SM_T_PROCESS_COLLECT 
                    where Line=@Line and Position=@Position and ParamName like @ParamName
                    and CollectTime>=@CurrentTime order by ParamName,CollectTime asc";

                return db.Query<VM_PROCESS>(sql, new { Line = Line, Position = Position, ParamName = "%总炉压%", CurrentTime = DateTime.Now.AddHours(-1) }).ToList();


            }
        }

        public static List<VM_PROCESS> GetFenQuLY(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                //var sql = @"select CollectTime as time,
                //      cast(CollectValue as decimal) as value,
                //    ParamName as SeriesName,ParamType from SM_T_PROCESS_COLLECT 
                //    where Line=@Line and Position=@Position and ParamName like @ParamName
                //    and CollectTime>=@CurrentTime order by ParamName,CollectTime asc";


                var sql = @"select CollectTime as time,
                      cast(CollectValue as decimal)*10 as value,
                    ParamName as SeriesName from SM_T_LUYA 
                    where Line=@Line 
                    and CollectTime>=@CurrentTime order by CollectTime asc";
                return db.Query<VM_PROCESS>(sql, new { Line = Line, Position = Position, ParamName = "%区炉压%", CurrentTime = DateTime.Now.AddHours(-1) }).ToList();


            }
        }
        public static List<VM_PROCESS> GetMFUA(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,
                      cast(CollectValue as decimal) as value,
                    ParamName as SeriesName,ParamType from SM_T_PROCESS_COLLECT 
                    where Line=@Line and Position=@Position and (ParamName like N'%功率%' or ParamName like N'%电压%')
                    and CollectTime>=@CurrentTime order by ParamName,CollectTime asc";

                return db.Query<VM_PROCESS>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();


            }
        }
        public static List<VM_PROCESS> GetLW(string Line, string Position, string ParamName)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select 
                      CollectTime as time,
                      cast(CollectValue as decimal) as value,
                      ParamType as SeriesName 
                     from SM_T_PROCESS_COLLECT 
                    where Line=@Line 
                      and Position=@Position 
                      and ParamName =@ParamName
                      and CollectTime>=@CurrentTime 
                    order by ParamName,CollectTime asc";
              return db.Query<VM_PROCESS>(sql, new { Line = Line, Position = Position, ParamName = ParamName, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();

            }
        } 

        public static List<VM_PROCESS> GetL01ProductSpeed(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,cast(CollectValue as decimal) as value,ParamName as SeriesName from SM_T_PROCESS_COLLECT 
                    where Line=@Line and Position=@Position and ParamName like @ParamName
                    and CollectTime>=@CurrentTime order by ParamName,CollectTime asc";

                return db.Query<VM_PROCESS>(sql, new { Line = Line, Position = Position, ParamName = "%速度%", CurrentTime = DateTime.Now.AddHours(-1) }).ToList();


            }
        }
        public static List<VM_PROCESS> GetL01dianneng()
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CreateTime as time,cast(FENSHI_VALUE as decimal) as value,POSITION as SeriesName from BT_POWER_DayHour 
                    where CreateTime>=@CurrentTime order by CreateTime asc";

                return db.Query<VM_PROCESS>(sql, new {  CurrentTime = DateTime.Now.AddHours(-800) }).ToList();


            }
        }
        public static List<VM_PROCESS> GetL01diannengday()
        {
            using (var db = Pub.DB)
            {
                var sql = @"select dateadd(day,-1,CreateTime) as time,cast(COLLECT_VALUE as decimal) as value,POSITION as SeriesName from BT_POWER_DAY 
                    where CreateTime>=@CurrentTime order by CreateTime asc";

                return db.Query<VM_PROCESS>(sql, new { CurrentTime = DateTime.Now.AddDays(-7) }).ToList();


            }
        }
        public static List<VM_PROCESS> GetPRODUCTION()
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CreateTime as time,cast(COLLECT_VALUE as decimal(18,4)) as value,COLLECT_TYPE as SeriesName from BT_ZUTAIWANG_COLLECT 
                    where CreateTime>=@CurrentTime order by CreateTime asc";

                return db.Query<VM_PROCESS>(sql, new { CurrentTime = DateTime.Now.AddHours(-8) }).ToList();


            }
        }

        public static List<VM_XIAPAN> GetProductQty()
        {
            using (var db = Pub.DB)
            {
                //BLL_SM_ProductREgistry
                var sql = @"select sum(NetWeight) as Weight,CTH,CONVERT(nvarchar(10),XPRQ,120) as XPRQ
	 from smlink.BLL_SM_ProductREgistry.dbo.SM_AssemblyLineDown
    where 1 = 1 and CTH in ('L01', 'L02', 'L03', 'L04')
    and XPRQ >= DATEADD(day, -7, getdate())
--and XPRQ>='1900-01-01'
    group by CTH,XPRQ";

                return db.Query<VM_XIAPAN>(sql).ToList();
        

            }
        }

        public static List<VM_SM_T_PROCESS_PRODUCT> GetProcessData()
        {
            using (var db = Pub.DB)
            {
                //                var sql = @"select product.*,q.customercode,q.Spec as ProductSpec,p.Line,p.ProcessName from SM_T_PROCESS_PRODUCT product 
                //left join WF_T_INSTANCE i on product.InstanceId=i.InstanceId 
                //left join sm_t_process p on p.InstanceId=product.InstanceId
                //left join sm_t_process_quality q on q.instanceid=p.instanceId and q.GG=product.XianJing and q.GGXH=product.Spec
                //where i.InstanceStatus='Finished'
                //and p.ProcessStatus!='RECOVER'";
                var sql = "exec sp_getprocessdata";

                var list = db.Query<VM_SM_T_PROCESS_PRODUCT>(sql).ToList();
                return list;
            }


        }

        public static string GetLWAlarm(string Line)
        {
            using (var db = Pub.DB)
            {
                var sql = "sp_lwalarm";
                var param = new DynamicParameters();
             
                param.Add("Line", Line);
                param.Add("msg", "",null,ParameterDirection.Output);
                db.Execute(sql, param, null, null, CommandType.StoredProcedure);

                var msg=param.Get<string>("msg");
                return msg;
            }
        }

        public static string GetMFUAAlarm(string Line)
        {
            using (var db = Pub.DB)
            {
                var sql = "sp_mfuaalarm";
                var param = new DynamicParameters();

                param.Add("Line", Line);
                param.Add("msg", "", null, ParameterDirection.Output);
                db.Execute(sql, param, null, null, CommandType.StoredProcedure);

                var msg = param.Get<string>("msg");
                return msg;

            }
        }

        public static string GetTotal(string Line)
        {
            using (var db = Pub.DB)
            {
                var sql = "sp_totalproduct";
                var param = new DynamicParameters();

                param.Add("Line", Line);
                param.Add("msg", "", null, ParameterDirection.Output);
                db.Execute(sql, param, null, null, CommandType.StoredProcedure);

                var msg = param.Get<string>("msg");
                return msg;

            }
        }
    }
}
