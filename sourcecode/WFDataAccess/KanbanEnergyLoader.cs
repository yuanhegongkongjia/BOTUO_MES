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
    public class KanbanEnergyLoader
    {
        public static List<VM_ENERGY_COLLECT> GetPowerData(string Line)
        {
            using (var db = Pub.DB)
            {
                var sql = @"
select Line,CollectDate,TotalValue as DataValue,Remark1 as Position from SM_T_DAYENERGY
           where Line=@Line ";
              
                sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-3,getdate()),120) and Category='POWER'
                    and Remark1 in ('MF1','MF2','MF3','MF4','MF5')";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line }).ToList();
                return list;
            }
        }
        public static List<VM_ENERGY_COLLECT> GetPowerData(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line,CollectDate,sum(cast(TotalValue as decimal(18,2))) as DataValue from SM_T_DAYENERGY
           where Line=@Line  ";
                if (!string.IsNullOrWhiteSpace(Position))
                {
                    sql += " and  Position=@Position";
                }
                if (DateTime.Now.Hour < 8)
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(day,-4,getdate()),120)";
                }
                else
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(day,-3,getdate()),120)";
                }
                sql += @" and Category='POWER' 
         group by Line,CollectDate";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();
                return list;
            }
        }
        public static List<VM_ENERGY_COLLECT> GetChunShuiData(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line,CollectDate,sum(cast(TotalValue as decimal(18,2))) as DataValue from SM_T_DAYENERGY
           where Line=@Line ";
                if (!string.IsNullOrWhiteSpace(Position))
                {
                    sql += " and  Position=@Position";
                }
                if (DateTime.Now.Hour < 8)
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-4,getdate()),120)";
                }
                else {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-3,getdate()),120)";
                }
                sql += @" and Category='CHUNSHUI'
                group by Line,CollectDate";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();
                return list;
            }
        }
        public static List<VM_ENERGY_COLLECT> GetZilaiShui(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line,CollectDate,sum(cast(TotalValue as decimal(18,2))) as DataValue from SM_T_DAYENERGY
           where Line=@Line ";
                if (!string.IsNullOrWhiteSpace(Position))
                {
                    sql += " and  Position=@Position";
                }
                if (DateTime.Now.Hour < 8)
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-4,getdate()),120)";
                }
                else {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-3,getdate()),120)";
                }
                sql += @" and Category='ZILAISHUI'
                group by Line,CollectDate";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();
                return list;
            }
        }
        public static List<VM_ENERGY_COLLECT> GetLinSuanData(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line,CollectDate,sum(cast(TotalValue as decimal(18,2))) as DataValue from SM_T_DAYENERGY
           where Line=@Line ";
                if (!string.IsNullOrWhiteSpace(Position))
                {
                    sql += " and  Position=@Position";
                }
                if (DateTime.Now.Hour < 8)
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-4,getdate()),120)";
                }
                else
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-3,getdate()),120)";
                }
                sql += @" and Category='LINSUAN'
                group by Line,CollectDate";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();
                return list;
            }
        }
        public static List<VM_ENERGY_COLLECT> GetYanSuanData(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line,CollectDate,sum(cast(TotalValue as decimal(18,2))) as DataValue from SM_T_DAYENERGY
           where Line=@Line ";
                if (!string.IsNullOrWhiteSpace(Position))
                {
                    sql += " and  Position=@Position";
                }
                if (DateTime.Now.Hour < 8)
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-4,getdate()),120)";
                }
                else
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-3,getdate()),120)";
                }
                sql += @" and Category='YANSUAN'
                group by Line,CollectDate";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();
                return list;
            }
        }
        public static List<VM_ENERGY_COLLECT> GetJianData(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line,CollectDate,sum(cast(TotalValue as decimal(18,2))) as DataValue from SM_T_DAYENERGY
           where Line=@Line ";
                if (!string.IsNullOrWhiteSpace(Position))
                {
                    sql += " and  Position=@Position";
                }
                if (DateTime.Now.Hour < 8)
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-4,getdate()),120)";
                }
                else
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-3,getdate()),120)";
                }
                sql += @" and Category='CHUNJIAN'
                group by Line,CollectDate";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();
                return list;
            }
        }
        public static List<VM_ENERGY_COLLECT> GetgetTianRanQiData(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line,CollectDate,sum(cast(TotalValue as decimal(18,2))) as DataValue from SM_T_DAYENERGY
           where Line=@Line ";
                if (!string.IsNullOrWhiteSpace(Position))
                {
                    sql += " and  Position=@Position";
                }
                if (DateTime.Now.Hour < 8)
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-4,getdate()),120)";
                }
                else
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-3,getdate()),120)";
                }
                sql += @" and Category='TIANRANQI'
                group by Line,CollectDate";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();
                return list;
            }
        }
        public static List<VM_ENERGY_COLLECT> GettZhengQiData(string Line, string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line,CollectDate,sum(cast(TotalValue as decimal(18,2))) as DataValue from SM_T_DAYENERGY
           where Line=@Line ";
                if (!string.IsNullOrWhiteSpace(Position))
                {
                    sql += " and  Position=@Position";
                }
                if (DateTime.Now.Hour < 8)
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-4,getdate()),120)";
                }
                else
                {
                    sql += @"and CollectDate>=convert(nvarchar(10),DateAdd(DAY,-3,getdate()),120)";
                }
                sql += @" and Category='ZHENGQI'
                group by Line,CollectDate";

                var list = db.Query<VM_ENERGY_COLLECT>(sql, new { Line = Line, Position = Position, CurrentTime = DateTime.Now.AddHours(-1) }).ToList();
                return list;
            }
        }

        public static string GetEnergyAlarm(string Line)
        {
            using (var db = Pub.DB)
            {
                var sql = "sp_energyalarm";
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
