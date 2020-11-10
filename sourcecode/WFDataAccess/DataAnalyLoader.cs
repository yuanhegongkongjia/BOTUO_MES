using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using WFCore;
using DynamicForm.Core;
using WFCommon;
using WFCommon.VM;
using WFCommon.Utility;
using System.Text;
using System.Data;
namespace WFDataAccess
{
    public class DataAnalyLoader
    {
        public static List<SM_T_DAYENERGY> QueryEnergy4ByDay(string CollectDateFrom, string CollectDateTo, string Category)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                {
                    sql = @"select TotalValue,Line,CollectDate from sm_t_dayenergy 
            where Category=@Category and CollectDate>=convert(nvarchar(10),dateadd(day,-7,getdate()),120)
                order by Line,collectdate ";
                    var list = db.Query<SM_T_DAYENERGY>(sql, new { Category = Category }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select TotalValue,Line,CollectDate from sm_t_dayenergy 
            where Category=@Category ";
                    if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                    {
                        sql = sql + " and CollectDate>=@CollectDateFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = sql + " and CollectDate<=@CollectDateTo ";
                    }

                    sql = sql + "order by collectdate ";
                    var list = db.Query<SM_T_DAYENERGY>(sql, new { Category = Category, CollectDateFrom = CollectDateFrom, CollectDateTo = CollectDateTo }).ToList();
                    return list;
                }
            }
        }

        public static List<SM_T_DAYENERGY> QueryEnergy4ByMonth(string CollectMonthFrom, string CollectMonthTo, string Category)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                if (string.IsNullOrWhiteSpace(CollectMonthFrom) && string.IsNullOrWhiteSpace(CollectMonthTo))
                {
                    sql = @"select TotalValue,Line,CollectDate from v_sm_t_energy_month 
            where Category=@Category and collectdate>=convert(nvarchar(7),dateadd(month,-7,getdate()),120)

                order by CollectDate ";
                    var list = db.Query<SM_T_DAYENERGY>(sql, new { Category = Category }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select TotalValue,Line, CollectDate from v_sm_t_energy_month 
            where Category = @Category ";

                    if (!string.IsNullOrWhiteSpace(CollectMonthFrom))
                    {
                        sql = sql + " and CollectDate>=@CollectMonthFrom";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectMonthTo))
                    {
                        sql = sql + " and CollectDate<=@CollectMonthTo ";
                    }

                    sql = sql + "  order by collectdate ";
                    var list = db.Query<SM_T_DAYENERGY>(sql, new { Category = Category, CollectMonthFrom =  CollectMonthFrom, CollectMonthTo = CollectMonthTo }).ToList();
                    return list;
                }
            }
        }


        public static List<SM_T_DAYENERGY> QueryEnergy4ByYear(string CollectYearFrom, string CollectYearTo, string Category)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                if (string.IsNullOrWhiteSpace(CollectYearFrom) && string.IsNullOrWhiteSpace(CollectYearFrom))
                {
                    sql = @"select sum(TotalValue) as TotalValue,Line,CollectYear as CollectDate from sm_t_dayenergy 
            where Category=@Category and CollectYear>=convert(nvarchar(4),getdate(),120)-7
group by CollectYear,Line                
order by collectyear ";
                    var list = db.Query<SM_T_DAYENERGY>(sql, new { Category = Category }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(TotalValue) as TotalValue,Line,CollectYear as CollectDate from sm_t_dayenergy 
            where Category=@Category ";
                    if (!string.IsNullOrWhiteSpace(CollectYearFrom))
                    {
                        sql = sql + " and CollectYear>=@CollectYearFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectYearTo))
                    {
                        sql = sql + " and CollectYear<=@CollectYearTo ";
                    }

                    sql = sql + " group by Line,CollectYear  order by collectyear ";
                    var list = db.Query<SM_T_DAYENERGY>(sql, new { Category = Category, CollectYearFrom = CollectYearFrom, CollectYearTo = CollectYearTo }).ToList();
                    return list;
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeDay(string Position, string CollectDateFrom, string CollectDateTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectDate from SM_T_POWER
              where Position = @Position and CollectDate>= convert(nvarchar(10), getdate()-7, 120) 
group by Position,PeriodName,CollectDate order by collectDate ";
                    var list = db.Query<SM_T_POWER>(sql, new { Position = Position }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectDate from SM_T_POWER
              where Position = @Position ";
                    if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                    {
                        sql = sql + "and CollectDate>=@CollectDateFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = sql + "and CollectDate<=@CollectDateTo ";
                    }
                    sql += " group by Position,PeriodName,CollectDate order by collectDate ";

                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Position = Position,
                        CollectDateFrom = CollectDateFrom,
                        CollectDateTo = CollectDateTo
                    }).ToList();
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeDayLine(string Line, string CollectDateFrom, string CollectDateTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line as Position,PeriodName,CollectDate from SM_T_POWER
              where Line = @Line and CollectDate>= convert(nvarchar(10), getdate()-7, 120) 
group by Line,PeriodName,CollectDate order by collectDate ";
                    var list = db.Query<SM_T_POWER>(sql, new { Line = Line }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line as Position,PeriodName,CollectDate from SM_T_POWER
              where Line = @Line ";
                    if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                    {
                        sql = sql + "and CollectDate>=@CollectDateFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = sql + "and CollectDate<=@CollectDateTo ";
                    }
                    sql += " group by Line,PeriodName,CollectDate order by collectDate ";

                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Line = Line,
                        CollectDateFrom = CollectDateFrom,
                        CollectDateTo = CollectDateTo
                    }).ToList();
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeDayLineSummary(string Line, string CollectDateFrom, string CollectDateTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                var str = Line.Split(',');
                if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line ,CollectDate from SM_T_POWER
              where Line in @Line and CollectDate>= convert(nvarchar(10), getdate()-7, 120) 
group by Line,CollectDate order by collectDate ";
                    var list = db.Query<SM_T_POWER>(sql, new { Line = str.ToArray() }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line,CollectDate from SM_T_POWER
              where Line in @Line ";
                    if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                    {
                        sql = sql + "and CollectDate>=@CollectDateFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = sql + "and CollectDate<=@CollectDateTo ";
                    }
                    sql += " group by Line,CollectDate order by collectDate ";

                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Line = str.ToArray(),
                        CollectDateFrom = CollectDateFrom,
                        CollectDateTo = CollectDateTo
                    }).ToList();
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeDaySummary(string Position, string CollectDateFrom, string CollectDateTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                //Position = Position.Replace(",", "','");
                //Position = string.Format("'{0}'", Position);
                var str = Position.Split(',');
                if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,CollectDate from SM_T_POWER
              where Position in @Position and CollectDate>= convert(nvarchar(10), getdate()-7, 120) 
group by Position,CollectDate order by collectDate asc";
                    var list = db.Query<SM_T_POWER>(sql, new { Position = str.ToArray() }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,CollectDate from SM_T_POWER
              where Position in @Position ";
                    if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                    {
                        sql = sql + "and CollectDate>=@CollectDateFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = sql + "and CollectDate<=@CollectDateTo ";
                    }
                    sql += " group by Position,CollectDate order by collectDate asc";

                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Position = str.ToArray(),
                        CollectDateFrom = CollectDateFrom,
                        CollectDateTo = CollectDateTo
                    }).ToList();
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeMonth(string Position, string CollectMonthFrom, string CollectMonthTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                var CollectYearFrom = 0;
                var MonthFrom = 0;
                var CollectYearTo = 0;
                var MonthTo = 0;

                if (string.IsNullOrWhiteSpace(CollectMonthFrom) && string.IsNullOrWhiteSpace(CollectMonthTo))
                {
                    CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                    CollectMonthTo = DateTime.Now.ToString("yyyy-MM");

                    sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectMonth as CollectDate from SM_T_POWER
              where Position = @Position and CollectMonth>=@CollectMonthFrom 
               and CollectMonth<=@CollectMonthTo 
group by Position,PeriodName,CollectYear,CollectMonth
order by CollectMonth 
";
                    var list = db.Query<SM_T_POWER>(sql, new { Position = Position, CollectMonthFrom = CollectMonthFrom, CollectMonthTo = CollectMonthTo }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectMonth as CollectDate from SM_T_POWER
              where Position = @Position ";

                    if (!string.IsNullOrWhiteSpace(CollectMonthFrom))
                    {

                        sql = sql + " and CollectMonth>=@CollectMonthFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectMonthTo))
                    {

                        sql = sql + " and CollectMonth<=@CollectMonthTo ";
                    }


                    sql = sql + " group by Position,PeriodName,CollectMonth ";
                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Position = Position,

                        CollectMonthFrom = CollectMonthFrom,
                        CollectMonthTo = CollectMonthTo,

                    }).ToList();
                }
            }
        }


        public static List<SM_T_POWER> PowerAnalyzeMonthSummary(string Position, string CollectMonthFrom, string CollectMonthTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                var str = Position.Split(',');


                if (string.IsNullOrWhiteSpace(CollectMonthFrom) && string.IsNullOrWhiteSpace(CollectMonthTo))
                {
                    CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                    CollectMonthTo = DateTime.Now.ToString("yyyy-MM");

                    sql = @"select sum(CollectValue) as CollectValue,Position,CollectMonth as CollectDate from SM_T_POWER
              where Position in @Position and CollectMonth>=@CollectMonthFrom 
               and CollectMonth<=@CollectMonthTo 
group by Position,CollectMonth
order by Position,CollectMonth 
";
                    var list = db.Query<SM_T_POWER>(sql, new { Position = str.ToArray(), CollectMonthFrom = CollectMonthFrom, CollectMonthTo = CollectMonthTo }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,CollectMonth as CollectDate from SM_T_POWER
              where Position in @Position ";

                    if (!string.IsNullOrWhiteSpace(CollectMonthFrom))
                    {

                        sql = sql + " and CollectMonth>=@CollectMonthFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectMonthTo))
                    {

                        sql = sql + " and CollectMonth<=@CollectMonthTo ";
                    }


                    sql = sql + " group by Position,CollectMonth order by Position,CollectMonth";
                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Position = str.ToArray(),

                        CollectMonthFrom = CollectMonthFrom,
                        CollectMonthTo = CollectMonthTo,

                    }).ToList();
                }
            }
        }


        public static List<SM_T_POWER> PowerAnalyzeMonthLine(string Line, string CollectMonthFrom, string CollectMonthTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                var CollectYearFrom = 0;
                var MonthFrom = 0;
                var CollectYearTo = 0;
                var MonthTo = 0;

                if (string.IsNullOrWhiteSpace(CollectMonthFrom) && string.IsNullOrWhiteSpace(CollectMonthTo))
                {
                    CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                    CollectMonthTo = DateTime.Now.ToString("yyyy-MM");

                    sql = @"select sum(CollectValue) as CollectValue,Line,PeriodName,CollectMonth as CollectDate from SM_T_POWER
              where Line = @Line and CollectMonth>=@CollectMonthFrom 
               and CollectMonth<=@CollectMonthTo 
group by Line,PeriodName,CollectYear,CollectMonth
order by CollectMonth 
";
                    var list = db.Query<SM_T_POWER>(sql, new { Line = Line, CollectMonthFrom = CollectMonthFrom, CollectMonthTo = CollectMonthTo }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line,PeriodName,CollectMonth as CollectDate from SM_T_POWER
              where Line = @Line ";

                    if (!string.IsNullOrWhiteSpace(CollectMonthFrom))
                    {

                        sql = sql + " and CollectMonth>=@CollectMonthFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectMonthTo))
                    {

                        sql = sql + " and CollectMonth<=@CollectMonthTo ";
                    }


                    sql = sql + " group by Line,PeriodName,CollectMonth order by CollectMonth";
                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Line = Line,
                     
                        CollectMonthFrom = CollectMonthFrom,
                        CollectMonthTo = CollectMonthTo
                       
                    }).ToList();
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeMonthLineSummary(string Line, string CollectMonthFrom, string CollectMonthTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";

                var str = Line.Split(',');

                if (string.IsNullOrWhiteSpace(CollectMonthFrom) && string.IsNullOrWhiteSpace(CollectMonthTo))
                {
                    CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                    CollectMonthTo = DateTime.Now.ToString("yyyy-MM");

                    sql = @"select sum(CollectValue) as CollectValue,Line,CollectMonth as CollectDate from SM_T_POWER
              where Line in @Line and CollectMonth>=@CollectMonthFrom 
               and CollectMonth<=@CollectMonthTo 
group by Line,CollectMonth
order by CollectMonth,Line
";
                    var list = db.Query<SM_T_POWER>(sql, new { Line = str.ToArray(), CollectMonthFrom = CollectMonthFrom, CollectMonthTo = CollectMonthTo }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line,CollectMonth as CollectDate from SM_T_POWER
              where Line in @Line ";

                    if (!string.IsNullOrWhiteSpace(CollectMonthFrom))
                    {

                        sql = sql + " and CollectMonth>=@CollectMonthFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectMonthTo))
                    {

                        sql = sql + " and CollectMonth<=@CollectMonthTo ";
                    }


                    sql = sql + " group by Line,CollectMonth order by CollectMonth,Line ";
                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Line = str.ToArray(),

                        CollectMonthFrom = CollectMonthFrom,
                        CollectMonthTo = CollectMonthTo,

                    }).ToList();
                }
            }
        }
        public static List<SM_T_POWER> PowerAnalyzeYear(string Position, string CollectYearFrom, string CollectYearTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                if (string.IsNullOrWhiteSpace(CollectYearFrom) && string.IsNullOrWhiteSpace(CollectYearTo))
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectYear as CollectDate from SM_T_POWER
              where Position = @Position and CollectYear>= convert(nvarchar(4), getdate(), 120) - 7 
group by Position,PeriodName,CollectYear order by collectYear ";
                    var list = db.Query<SM_T_POWER>(sql, new { Position = Position }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectYear as CollectDate from SM_T_POWER
              where Position = @Position ";
                    if (!string.IsNullOrWhiteSpace(CollectYearFrom))
                    {
                        sql = sql + "and CollectYear>=@CollectYearFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectYearTo))
                    {
                        sql = sql + "and CollectYear<=@CollectYearTo ";
                    }
                    sql += " group by Position,PeriodName,CollectYear order by CollectYear ";

                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Position = Position,
                        CollectYearFrom = CollectYearFrom,
                        CollectYearTo = CollectYearTo
                    }).ToList();
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeYearLine(string Line, string CollectYearFrom, string CollectYearTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                if (string.IsNullOrWhiteSpace(CollectYearFrom) && string.IsNullOrWhiteSpace(CollectYearTo))
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line,PeriodName,CollectYear as CollectDate from SM_T_POWER
              where Line = @Line and CollectYear>= convert(nvarchar(4), getdate(), 120) - 7 
group by Line,PeriodName,CollectYear order by collectYear ";
                    var list = db.Query<SM_T_POWER>(sql, new { Line = Line }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line,PeriodName,CollectYear as CollectDate from SM_T_POWER
              where Line = @Line ";
                    if (!string.IsNullOrWhiteSpace(CollectYearFrom))
                    {
                        sql = sql + "and CollectYear>=@CollectYearFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectYearTo))
                    {
                        sql = sql + "and CollectYear<=@CollectYearTo ";
                    }
                    sql += " group by Line,PeriodName,CollectYear order by CollectYear ";

                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Line = Line,
                        CollectYearFrom = CollectYearFrom,
                        CollectYearTo = CollectYearTo
                    }).ToList();
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeYearSummary(string Position, string CollectYearFrom, string CollectYearTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                var str = Position.Split(',');
                if (string.IsNullOrWhiteSpace(CollectYearFrom) && string.IsNullOrWhiteSpace(CollectYearTo))
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,CollectYear as CollectDate from SM_T_POWER
              where Position in @Position and CollectYear>= convert(nvarchar(4), getdate(), 120) - 7 
group by Position,CollectYear order by collectYear ";
                    var list = db.Query<SM_T_POWER>(sql, new { Position = str.ToArray() }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Position,CollectYear as CollectDate from SM_T_POWER
              where Position in @Position ";
                    if (!string.IsNullOrWhiteSpace(CollectYearFrom))
                    {
                        sql = sql + "and CollectYear>=@CollectYearFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectYearTo))
                    {
                        sql = sql + "and CollectYear<=@CollectYearTo ";
                    }
                    sql += " group by Position,CollectYear order by CollectYear ";

                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Position = str.ToArray(),
                        CollectYearFrom = CollectYearFrom,
                        CollectYearTo = CollectYearTo
                    }).ToList();
                }
            }
        }

        public static List<SM_T_POWER> PowerAnalyzeYearLineSummary(string Line, string CollectYearFrom, string CollectYearTo)
        {
            using (var db = Pub.DB)
            {
                var sql = "";
                var str = Line.Split(',');
                if (string.IsNullOrWhiteSpace(CollectYearFrom) && string.IsNullOrWhiteSpace(CollectYearTo))
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line,CollectYear as CollectDate from SM_T_POWER
              where Line in @Line and CollectYear>= convert(nvarchar(4), getdate(), 120) - 7 
group by Line,CollectYear order by collectYear ";
                    var list = db.Query<SM_T_POWER>(sql, new { Line = str.ToArray() }).ToList();
                    return list;
                }
                else
                {
                    sql = @"select sum(CollectValue) as CollectValue,Line,CollectYear as CollectDate from SM_T_POWER
              where Line in @Line ";
                    if (!string.IsNullOrWhiteSpace(CollectYearFrom))
                    {
                        sql = sql + "and CollectYear>=@CollectYearFrom ";
                    }
                    if (!string.IsNullOrWhiteSpace(CollectYearTo))
                    {
                        sql = sql + "and CollectYear<=@CollectYearTo ";
                    }
                    sql += " group by Line,PeriodName,CollectYear order by CollectYear ";

                    return db.Query<SM_T_POWER>(sql, new
                    {
                        Line = str.ToArray(),
                        CollectYearFrom = CollectYearFrom,
                        CollectYearTo = CollectYearTo
                    }).ToList();
                }
            }
        }

        public static List<VM_SM_T_ENERGY_COLLECT> EnergyQueryCurrent(string Category, string Line)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Position,Line,CollectValue,SUBSTRING(convert(nvarchar(50),CollectTime,120),12,5) as CTime from SM_T_ENERGY_COLLECT 
                        where Category=@Category and Line=@Line
                        and CollectTime>=@CollectTime";

                var CollectTime = "";
                if (DateTime.Now.Hour >= 8)
                {
                    //取当天数据
                    CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                }
                else
                {
                    //取前一天八点开始的数据
                    CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
                }
                sql += " order by collectTime ";

                return db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Category = Category, Line = Line, CollectTime = CollectTime }).ToList();
            }
        }

        public static List<VM_SM_T_ENERGY_COLLECT> PowerQueryCurrent(string Position)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select CollectValue,SUBSTRING(convert(nvarchar(50),CollectTime,120),12,5) as CTime,Position from SM_T_ENERGY_COLLECT 
                        where Position=@Position  
                        and CollectTime>=@CollectTime";

                var CollectTime = "";
                if (DateTime.Now.Hour >=8)
                {
                    //取当天数据
                    CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                }
                else
                {
                    //取前一天八点开始的数据
                    CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
                }

                sql += " order by collecttime";

                return db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position, CollectTime = CollectTime }).ToList();
            }
        }

        public static List<VM_SM_T_ENERGY_COLLECT> MaterialQueryCurrent(string Position, string CollectDateFrom, string CollectDateTo)
        {
            using (var db = Pub.DB)
            {

                var sql = "";
                if (Position=="全部选择")
                {
                    if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = @"select Collect_Value, SUBSTRING(convert(nvarchar(50), Remark1, 110), 0, 6) as CTime,ENERGY_TYPE from BT_Material
                          where   
                         Remark1>=convert(nvarchar(50),dateadd(day,-7,getdate()),120)
                order by Remark1,ENERGY_TYPE";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql).ToList();
                        return list;
                    }

                    else
                    {
                        sql = @"select Collect_Value,SUBSTRING(convert(nvarchar(50),Remark1,110),0,6) as CTime,ENERGY_TYPE from BT_Material 
                       where";
                        if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                        {
                            sql = sql + " Remark1>=@CollectDateFrom ";
                        }
                        if (!string.IsNullOrWhiteSpace(CollectDateTo))
                        {
                            sql = sql + " and Remark1<=@CollectDateTo ";
                        }

                        sql = sql + "order by Remark1 ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { CollectDateFrom = CollectDateFrom, CollectDateTo = CollectDateTo }).ToList();
                        return list;
                    }


                }
                else
                {
                    if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = @"select Collect_Value, SUBSTRING(convert(nvarchar(50), Remark1, 110), 0, 6) as CTime,ENERGY_TYPE from BT_Material
                          where ENERGY_TYPE=@Position and
                         Remark1>=convert(nvarchar(50),dateadd(day,-7,getdate()),120)
                order by Remark1,ENERGY_TYPE ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position }).ToList();
                        return list;
                    }

                    else
                    {
                        sql = @"select Collect_Value,SUBSTRING(convert(nvarchar(50),Remark1,110),0,6) as CTime,ENERGY_TYPE from BT_Material 
                       where ENERGY_TYPE=@Position ";
                        if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                        {
                            sql = sql + " and Remark1>=@CollectDateFrom ";
                        }
                        if (!string.IsNullOrWhiteSpace(CollectDateTo))
                        {
                            sql = sql + " and Remark1<=@CollectDateTo ";
                        }

                        sql = sql + "order by Remark1 ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position, CollectDateFrom = CollectDateFrom, CollectDateTo = CollectDateTo }).ToList();
                        return list;
                    }
                }
                //var CollectTime = "";
                //if (DateTime.Now.Hour >= 8)
                //{
                //    //取当天数据
                //    CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                //}
                //else
                //{
                //    //取前一天八点开始的数据
                //    CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
                //}

                // sql += " order by Remark1";

                //return db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position }).ToList();
            }
        }
        public static List<VM_SM_T_ENERGY_COLLECT> POWERDAYQueryCurrent(string Position, string CollectDateFrom, string CollectDateTo)
        {
            using (var db = Pub.DB)
            {

                var sql = "";
                if (Position == "全部电表")
                {
                    if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = @"select COLLECT_VALUE as Collect_Value , REMARK2 as CTime,POSITION from BT_POWER_DAY
                          where   
                         REMARK2>=convert(nvarchar(50),dateadd(day,-7,getdate()),120)
                order by REMARK2,POSITION";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql).ToList();
                        return list;
                    }

                    else
                    {
                        sql = @"select COLLECT_VALUE as Collect_Value,REMARK2 as CTime,POSITION from BT_POWER_DAY
                       where";
                        if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                        {
                            sql = sql + " REMARK2>=@CollectDateFrom ";
                        }
                        if (!string.IsNullOrWhiteSpace(CollectDateTo))
                        {
                            sql = sql + " and REMARK2<=@CollectDateTo ";
                        }

                        sql = sql + "order by CreateTime ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { CollectDateFrom = CollectDateFrom, CollectDateTo = CollectDateTo }).ToList();
                        return list;
                    }


                }
                else
                {
                    if (string.IsNullOrWhiteSpace(CollectDateFrom) && string.IsNullOrWhiteSpace(CollectDateTo))
                    {
                        sql = @"select COLLECT_VALUE as Collect_Value, REMARK2 as CTime,POSITION from BT_POWER_DAY
                          where POSITION=@Position and
                         REMARK2>=convert(nvarchar(50),dateadd(day,-7,getdate()),120)
                order by CreateTime,POSITION";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position }).ToList();
                        return list;
                    }

                    else
                    {
                        sql = @"select COLLECT_VALUE as Collect_Value,REMARK2 as CTime,POSITION from BT_POWER_DAY 
                       where POSITION=@Position ";
                        if (!string.IsNullOrWhiteSpace(CollectDateFrom))
                        {
                            sql = sql + " and REMARK2>=@CollectDateFrom ";
                        }
                        if (!string.IsNullOrWhiteSpace(CollectDateTo))
                        {
                            sql = sql + " and REMARK2<=@CollectDateTo ";
                        }

                        sql = sql + "order by REMARK2 ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position, CollectDateFrom = CollectDateFrom, CollectDateTo = CollectDateTo }).ToList();
                        return list;
                    }
                }
                //var CollectTime = "";
                //if (DateTime.Now.Hour >= 8)
                //{
                //    //取当天数据
                //    CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                //}
                //else
                //{
                //    //取前一天八点开始的数据
                //    CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
                //}

                // sql += " order by Remark1";

                //return db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position }).ToList();
            }
        }
        public static List<VM_SM_T_ENERGY_COLLECT> POWERMonthQueryCurrent(string Position, string CollectMonthFrom, string CollectMonthTo)
        {
            using (var db = Pub.DB)
            {

                var sql = "";
                if (Position == "全部电表")
                {
                    if (string.IsNullOrWhiteSpace(CollectMonthFrom) && string.IsNullOrWhiteSpace(CollectMonthTo))
                    {
                        sql = @"select Collect_Value,REMARK2 as CTime,POSITION from BT_POWER_MONTH
                          where   
                         REMARK2>=SUBSTRING(convert(nvarchar(50), dateadd(month,-6,GETDATE()), 120),0,8)
                order by REMARK2,POSITION";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql).ToList();
                        return list;
                    }

                    else
                    {
                        sql = @"select Collect_Value, REMARK2 as CTime,POSITION from BT_POWER_MONTH
                       where";
                        if (!string.IsNullOrWhiteSpace(CollectMonthFrom))
                        {
                            sql = sql + " REMARK2>=@CollectMonthFrom ";
                        }
                        if (!string.IsNullOrWhiteSpace(CollectMonthTo))
                        {
                            sql = sql + " and REMARK2<=@CollectMonthTo ";
                        }

                        sql = sql + "order by REMARK2 ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { CollectMonthFrom = CollectMonthFrom, CollectMonthTo = CollectMonthTo }).ToList();
                        return list;
                    }


                }
                else
                {
                    if (string.IsNullOrWhiteSpace(CollectMonthFrom) && string.IsNullOrWhiteSpace(CollectMonthTo))
                    {
                        sql = @"select Collect_Value, REMARK2 as CTime,POSITION from BT_POWER_MONTH
                          where POSITION=@Position and
                        REMARK2>=SUBSTRING(convert(nvarchar(50), dateadd(month,-6,GETDATE()), 120),0,8)
                order by REMARK2,POSITION";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position }).ToList();
                        return list;
                    }

                    else
                    {
                        sql = @"select Collect_Value, REMARK2 as CTime,POSITION from BT_POWER_MONTH
                          where POSITION=@Position  ";
                        if (!string.IsNullOrWhiteSpace(CollectMonthFrom))
                        {
                            sql = sql + " and REMARK2>=@CollectDateFrom ";
                        }
                        if (!string.IsNullOrWhiteSpace(CollectMonthTo))
                        {
                            sql = sql + " and REMARK2<=@CollectDateTo ";
                        }

                        sql = sql + "order by REMARK2 ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position, CollectDateFrom = CollectMonthFrom, CollectDateTo = CollectMonthTo }).ToList();
                        return list;
                    }
                }
                //var CollectTime = "";
                //if (DateTime.Now.Hour >= 8)
                //{
                //    //取当天数据
                //    CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                //}
                //else
                //{
                //    //取前一天八点开始的数据
                //    CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
                //}

                // sql += " order by Remark1";

                //return db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position }).ToList();
            }
        }
        public static List<VM_SM_T_ENERGY_COLLECT> POWERYearQueryCurrent(string Position, string CollectYearFrom, string CollectYearTo)
        {
            using (var db = Pub.DB)
            {

                var sql = "";
                if (Position == "全部电表")
                {
                    if (string.IsNullOrWhiteSpace(CollectYearFrom) && string.IsNullOrWhiteSpace(CollectYearTo))
                    {
                        sql = @"select Collect_Value,REMARK2 as CTime,POSITION from BT_POWER_YEAR
                          where   
                         REMARK2>=SUBSTRING(convert(nvarchar(50), dateadd(year,-3,GETDATE()), 120),0,5)
                order by REMARK2,POSITION";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql).ToList();
                        return list;
                    }

                    else
                    {
                        sql = @"select Collect_Value, REMARK2 as CTime,POSITION from BT_POWER_YEAR
                       where";
                        if (!string.IsNullOrWhiteSpace(CollectYearFrom))
                        {
                            sql = sql + " REMARK2>=@CollectYearFrom ";
                        }
                        if (!string.IsNullOrWhiteSpace(CollectYearTo))
                        {
                            sql = sql + " and REMARK2<=@CollectYearTo ";
                        }

                        sql = sql + "order by REMARK2 ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { CollectYearFrom = CollectYearFrom, CollectYearTo = CollectYearTo }).ToList();
                        return list;
                    }


                }
                else
                {
                    if (string.IsNullOrWhiteSpace(CollectYearFrom) && string.IsNullOrWhiteSpace(CollectYearTo))
                    {
                        sql = @"select Collect_Value,REMARK2 as CTime,POSITION from BT_POWER_YEAR
                          where Position=@Position and
                         REMARK2>=SUBSTRING(convert(nvarchar(50), dateadd(year,-3,GETDATE()), 120),0,5)
                order by REMARK2,POSITION";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position }).ToList();
                        return list;
                    }

                    else
                    {
                        sql = @"select Collect_Value,REMARK2 as CTime,POSITION from BT_POWER_YEAR
                          where Position=@Position and";
                        if (!string.IsNullOrWhiteSpace(CollectYearFrom))
                        {
                            sql = sql + "  REMARK2>=@CollectYearFrom ";
                        }
                        if (!string.IsNullOrWhiteSpace(CollectYearTo))
                        {
                            sql = sql + " and REMARK2<=@CollectYearTo ";
                        }

                        sql = sql + "order by REMARK2 ";
                        var list = db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position, CollectYearFrom = CollectYearFrom, CollectYearTo = CollectYearTo }).ToList();
                        return list;
                    }
                }
                //var CollectTime = "";
                //if (DateTime.Now.Hour >= 8)
                //{
                //    //取当天数据
                //    CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                //}
                //else
                //{
                //    //取前一天八点开始的数据
                //    CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
                //}

                // sql += " order by Remark1";

                //return db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Position = Position }).ToList();
            }
        }


        public static List<VM_SM_T_ENERGY_COLLECT> PowerQueryCurrentLine(string Line)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select Line as Position,CollectTime as CTime,sum(CollectValue) as CollectValue  from (select Line,SUBSTRING(convert(nvarchar(50),CollectTime,120),12,5) as CollectTime,CollectValue from SM_T_ENERGY_COLLECT 
                        where  Line=@Line and Category='POWER'
                        and CollectTime>=@CollectTime) b group by Line,CollectTime";

                var CollectTime = "";
                if (DateTime.Now.Hour > 8)
                {
                    //取当天数据
                    CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
                }
                else
                {
                    //取前一天八点开始的数据
                    CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
                }

                sql += " order by collecttime";

                return db.Query<VM_SM_T_ENERGY_COLLECT>(sql, new { Line = Line, CollectTime = CollectTime }).ToList();
            }
        }
    }
}
   
