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

        public static List<VM_PROCESS> GetDESTATUS( string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,ParamTyple as ParamType, CollectValue as value from BT_MachineCollect 
                           where Position=@Position and CollectTime>=@CurrentTime order by time
    ";

                return db.Query<VM_PROCESS>(sql, new {Position = Position, CurrentTime = DateTime.Now.AddMinutes(-600) }).ToList();


            }
        }

        public static List<VM_PROCESS> GetEnergySTATUS(string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CreateTime as time, FENSHI_VALUE as value from BT_POWER_DayHour 
                           where POSITION=@Position and CreateTime>=@CurrentTime order by time
    ";

                return db.Query<VM_PROCESS>(sql, new { Position = Position, CurrentTime = DateTime.Now.AddHours(-24) }).ToList();


            }
        }


        public static List<VM_PROCESS> GetCUSTATUS()
        {
            using (var db = Pub.DB)
            {
                var sql = @"SELECT ENERGY_TYPE as SeriesName ,COLLECT_VALUE AS value,REMARK1 as time FROM BT_Material 
where CONVERT(nvarchar(50),REMARK1,105) =CONVERT(nvarchar(50),GETDATE()-1,105)
    ";

                return db.Query<VM_PROCESS>(sql, new {  }).ToList();


            }
        }


        public static List<VM_PROCESS> GetDEOEESTATUS(string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Remark1 as time,1 as ParamType, MachineOEE as value,CONVERT(datetime,CONVERT(char(8),Remark1,111)+'1') as yuechu ,DATEADD(Day,-1,CONVERT(char(8),DATEADD(Month,1,Remark1),111)+'1') as yuemo from BT_MachineAnalyse 
                           where MachineName=@Position and Remark1>=@CurrentTime order by time desc
    ";

                return db.Query<VM_PROCESS>(sql, new { Position = Position, CurrentTime = DateTime.Now.AddDays(-31) }).ToList();


            }
        }


        public static List<VM_PROCESS> GetTEMSTATUS(string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,ParamName as ParamType,Position as Position, ParamValue as value from BT_Temp 
                           where  CollectTime>=@CurrentTime order by time
    ";

                return db.Query<VM_PROCESS>(sql, new { Position = Position, CurrentTime = DateTime.Now.AddMinutes(-60) }).ToList();


            }
        }


        public static List<VM_PROCESS> GetPDSTATUS()
        {
            using (var db = Pub.DB)
            {
                var sql = @"SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE(),105) order by  CreateTime desc,collect_type )

A
UNION ALL

SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-1,105) order by  CreateTime desc,collect_type )
B
UNION ALL

SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-2,105) order by  CreateTime desc,collect_type )

C
UNION ALL

SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-3,105) order by  CreateTime desc,collect_type )

D

UNION ALL

SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-4,105) order by  CreateTime desc,collect_type )

E
UNION ALL

SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-5,105) order by  CreateTime desc,collect_type )

F
UNION ALL

SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-6,105) order by  CreateTime desc,collect_type )

G
UNION ALL
SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-7,105) order by  CreateTime desc,collect_type )

H
UNION ALL
SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-8,105) order by  CreateTime desc,collect_type )

I
UNION ALL
SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-9,105) order by  CreateTime desc,collect_type )

J
UNION ALL
SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-10,105) order by  CreateTime desc,collect_type )

K
UNION ALL
SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-11,105) order by  CreateTime desc,collect_type )

L
UNION ALL
SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-12,105) order by  CreateTime desc,collect_type )

M
UNION ALL
SELECT * from (SELECT TOP 7  COLLECT_TYPE AS SeriesName,cast(COLLECT_VALUE as decimal(18,3)) AS value,ParamTYPE AS ParamType,CreateTime AS time FROM BT_TotalCollect 
where CONVERT(nvarchar(50),CreateTime,105) =CONVERT(nvarchar(50),GETDATE()-13,105) order by  CreateTime desc,collect_type )

N

    ";

                return db.Query<VM_PROCESS>(sql, new {  }).ToList();


            }
        }



        public static List<VM_PROCESS> GetTEM1(string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,Position as ParamType, ParamValue as value from BT_Temp 
                           where Position like '南区上%' and CollectTime>=@CurrentTime order by time,Position
    ";

                return db.Query<VM_PROCESS>(sql, new { Position = Position, CurrentTime = DateTime.Now.AddMinutes(-720) }).ToList();


            }
        }

        public static List<VM_PROCESS> GetTEM11(string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,Position as ParamType, ParamValue as value from BT_Temp 
                           where Position like '南区下%' and CollectTime>=@CurrentTime order by time,Position
    ";

                return db.Query<VM_PROCESS>(sql, new { Position = Position, CurrentTime = DateTime.Now.AddMinutes(-720) }).ToList();


            }
        }
        public static List<VM_PROCESS> GetTEM2(string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,Position as ParamType, ParamValue as value from BT_Temp 
                           where Position like '北区上%' and CollectTime>=@CurrentTime order by time,Position
    ";

                return db.Query<VM_PROCESS>(sql, new { Position = Position, CurrentTime = DateTime.Now.AddMinutes(-720) }).ToList();


            }
        }
        public static List<VM_PROCESS> GetTEM22(string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectTime as time,Position as ParamType, ParamValue as value from BT_Temp 
                           where Position like '北区下%' and CollectTime>=@CurrentTime order by time,Position
    ";

                return db.Query<VM_PROCESS>(sql, new { Position = Position, CurrentTime = DateTime.Now.AddMinutes(-720) }).ToList();


            }
        }





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
                var sql = @"select top 8  CollectTime as time,cast(CollectValue as decimal)/1000.0 as value,ParamName as SeriesName from SM_T_PROCESS_COLLECT 
                    where Line=@Line and Position=@Position and ParamName like @ParamName
                   
order by ParamName,CollectTime asc";

                return db.Query<VM_PROCESS>(sql, new { Line = Line, Position = Position, ParamName = "%电压%", CurrentTime = DateTime.Now.AddHours(-1) }).ToList();


            }
        }
        //-- and CollectTime>=@CurrentTime 
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

        public static List<VM_PROCESS> GetDESTATUS(string v1, string v2)
        {
            throw new NotImplementedException();
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
