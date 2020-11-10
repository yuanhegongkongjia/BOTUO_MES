using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using DynamicForm.Core;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WFCore;
using System.Web.SessionState;
using Dapper;
using DapperExtensions;
using System.IO;
using WFCommon;
using WFCommon.VM;
using System.Data;
using WFCommon.Utility;
using Common.Logging;
using System.Collections;
using System.ComponentModel;
using Fasterflect;
using System.Text;
using WFDataAccess;

namespace DynamicForm
{
    /// <summary>
    /// DataAnalyzeHandler 的摘要说明
    /// </summary>
    public class DataAnalyzeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var dict = DFPub.GetDFDictionary(context.Request);
            var action = dict["action"].ToLower();
            var contentType = context.Request.ContentType;
            if (string.IsNullOrWhiteSpace(contentType))
            {
                contentType = "application/json;charset=utf-8";
            }
            context.Response.ContentType = contentType;
            switch (action)
            {
                case "energyqueryonly4":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(EnergyQueryOnly4(context)));
                    break;
                case "poweranalyzeday":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeDay(context)));
                    break;
                case "poweranalyzemonth":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeMonth(context)));
                    break;
                case "poweranalyzemonthsummary":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeMonthSummary(context)));
                    break;
                case "poweranalyzemonthline":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeMonthLine(context)));
                    break;
                case "poweranalyzemonthlinesummary":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeMonthLineSummary(context)));
                    break;
                case "poweranalyzeyear":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeYear(context)));
                    break;
                case "poweranalyzeyearline":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeYearLine(context)));
                    break;
                case "poweranalyzeyearlinesummary":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeYearLineSummary(context)));
                    break;
                case "poweranalyzeyearsummary":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeYearSummary(context)));
                    break;
                case "poweranalyzedayline":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeDayLine(context)));
                    break;
                case "poweranalyzedaysummary":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeDaySummary(context)));
                    break;
                case "poweranalyzedaylinesummary":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerAnalyzeDayLineSummary(context)));
                    break;
                case "energyquerycurrent":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(EnergyQueryCurrent(context)));
                    break;
                case "powerquerycurrent":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerQueryCurrent(context)));
                    break;
                case "powerquerycurrentline":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerQueryCurrentLine(context)));
                    break;
                case "materialquery":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(MaterialQueryCurrentLine(context)));
                    break;
                case "powerdayquery":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerDayquery(context)));
                    break;
                case "powermonthquery":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerMonthquery(context)));
                    break;
                case "poweryearquery":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(PowerYearquery(context)));
                    break;
                default:
                    break;
            }
        }

        public IndexVM PowerAnalyzeDaySummary(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectDateFrom = context.Request.Params["CollectDateFrom"];
                var CollectDateTo = context.Request.Params["CollectDateTo"];
                var list = DataAnalyLoader.PowerAnalyzeDaySummary(Position, CollectDateFrom, CollectDateTo);
                var liste = new List<VM_ENERGY>();
                //日期
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data2 = JsonSerializeHelper.SerializeObject(cats);

                //系列的名字
                var names = list.Select(a => a.Position).Distinct().ToList();

                for (int i = 0; i < names.Count; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Position == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Position == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
                vm.data1 = JsonSerializeHelper.SerializeObject(names);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeDayLineSummary(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Line = context.Request.Params["Line"];
                var CollectDateFrom = context.Request.Params["CollectDateFrom"];
                var CollectDateTo = context.Request.Params["CollectDateTo"];
                var list = DataAnalyLoader.PowerAnalyzeDayLineSummary(Line, CollectDateFrom, CollectDateTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data2 = JsonSerializeHelper.SerializeObject(cats);
                var names = list.Select(a => a.Line).Distinct().ToList();
                for (int i = 0; i < names.Count; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Line == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Line == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
                vm.data1 = JsonSerializeHelper.SerializeObject(names);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeDay(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectDateFrom = context.Request.Params["CollectDateFrom"];
                var CollectDateTo = context.Request.Params["CollectDateTo"];
                var list = DataAnalyLoader.PowerAnalyzeDay(Position, CollectDateFrom, CollectDateTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var names = new string[3] { "高峰", "平谷", "低谷" };
                for (int i = 0; i < 3; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    l1.data_vm = list.Where(a => a.PeriodName == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();

                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeDayLine(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Line = context.Request.Params["Line"];
                var CollectDateFrom = context.Request.Params["CollectDateFrom"];
                var CollectDateTo = context.Request.Params["CollectDateTo"];
                var list = DataAnalyLoader.PowerAnalyzeDayLine(Line, CollectDateFrom, CollectDateTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var names = new string[3] { "高峰", "平谷", "低谷" };
                for (int i = 0; i < 3; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.PeriodName == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.PeriodName == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeMonth(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectMonthFrom = context.Request.Params["CollectMonthFrom"];
                var CollectMonthTo = context.Request.Params["CollectMonthTo"];
                var list = DataAnalyLoader.PowerAnalyzeMonth(Position, CollectMonthFrom, CollectMonthTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var names = new string[3] { "高峰", "平谷", "低谷" };
                for (int i = 0; i < 3; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.PeriodName == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.PeriodName == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeMonthLine(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Line = context.Request.Params["Line"];
                var CollectMonthFrom = context.Request.Params["CollectMonthFrom"];
                var CollectMonthTo = context.Request.Params["CollectMonthTo"];
                var list = DataAnalyLoader.PowerAnalyzeMonthLine(Line, CollectMonthFrom, CollectMonthTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var names = new string[3] { "高峰", "平谷", "低谷" };
                for (int i = 0; i < 3; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.PeriodName == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.PeriodName == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeMonthSummary(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectMonthFrom = context.Request.Params["CollectMonthFrom"];
                var CollectMonthTo = context.Request.Params["CollectMonthTo"];
                var list = DataAnalyLoader.PowerAnalyzeMonthSummary(Position, CollectMonthFrom, CollectMonthTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data2 = JsonSerializeHelper.SerializeObject(cats);
                var names = list.Select(a => a.Position).Distinct().ToList();
                for (int i = 0; i < names.Count; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Position == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Position == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
                vm.data1 = JsonSerializeHelper.SerializeObject(names);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeMonthLineSummary(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Line = context.Request.Params["Line"];
                var CollectMonthFrom = context.Request.Params["CollectMonthFrom"];
                var CollectMonthTo = context.Request.Params["CollectMonthTo"];
                var list = DataAnalyLoader.PowerAnalyzeMonthLineSummary(Line, CollectMonthFrom, CollectMonthTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data2 = JsonSerializeHelper.SerializeObject(cats);
                var names = list.Select(a => a.Line).Distinct().ToList();
                for (int i = 0; i < names.Count; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Line == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Line == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
                vm.data1 = JsonSerializeHelper.SerializeObject(names);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeYear(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectYearFrom = context.Request.Params["CollectYearFrom"];
                var CollectYearTo = context.Request.Params["CollectYearTo"];
                var list = DataAnalyLoader.PowerAnalyzeYear(Position, CollectYearFrom, CollectYearTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var names = new string[3] { "高峰", "平谷", "低谷" };
                for (int i = 0; i < 3; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.PeriodName == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.PeriodName == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeYearSummary(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectYearFrom = context.Request.Params["CollectYearFrom"];
                var CollectYearTo = context.Request.Params["CollectYearTo"];
                var list = DataAnalyLoader.PowerAnalyzeYearSummary(Position, CollectYearFrom, CollectYearTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data2 = JsonSerializeHelper.SerializeObject(cats);
                var names = list.Select(a => a.Position).Distinct().ToList();
                for (int i = 0; i < names.Count; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Position == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Position == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
                vm.data1 = JsonSerializeHelper.SerializeObject(names);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeYearLine(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Line = context.Request.Params["Line"];
                var CollectYearFrom = context.Request.Params["CollectYearFrom"];
                var CollectYearTo = context.Request.Params["CollectYearTo"];
                var list = DataAnalyLoader.PowerAnalyzeYearLine(Line, CollectYearFrom, CollectYearTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var names = new string[3] { "高峰", "平谷", "低谷" };
                for (int i = 0; i < 3; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.PeriodName == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.PeriodName == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerAnalyzeYearLineSummary(HttpContext context)
        {
            var vm = new IndexVM();
            try
            {
                var Line = context.Request.Params["Line"];
                var CollectYearFrom = context.Request.Params["CollectYearFrom"];
                var CollectYearTo = context.Request.Params["CollectYearTo"];
                var list = DataAnalyLoader.PowerAnalyzeYearLineSummary(Line, CollectYearFrom, CollectYearTo);
                var liste = new List<VM_ENERGY>();
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data2 = JsonSerializeHelper.SerializeObject(cats);
                var names = list.Select(a => a.Line).Distinct().ToList();
                for (int i = 0; i < names.Count; i++)
                {
                    var l1 = new VM_ENERGY();
                    l1.name = names[i];
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Line == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Line == l1.name).Select(b => new { b.CollectDate, b.CollectValue }).ToArray();
                    liste.Add(l1);
                }
                vm.hasError = false;
                vm.data = JsonSerializeHelper.SerializeObject(liste);
                vm.data1 = JsonSerializeHelper.SerializeObject(names);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }


        public IndexVM EnergyQueryOnly4(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            var QueryType = context.Request.Params["QueryType"];
            var Category = context.Request.Params["Category"];
            if (string.IsNullOrWhiteSpace(QueryType) || QueryType == "DAY")
            {
                var CollectDateFrom = context.Request.Params["CollectDateFrom"];
                var CollectDateTo = context.Request.Params["CollectDateTo"];

                var list = DataAnalyLoader.QueryEnergy4ByDay(CollectDateFrom, CollectDateTo, Category);
                //vm.data = JsonSerializeHelper.SerializeObject(list);
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();
                if (Category == "TIANRANQI" || Category == "ZHENGQI")
                {

                    var l1 = new VM_ENERGY();
                    l1.name = "L01";
                    l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Line == "L01").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Line == "L01").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l1);

                    var l2 = new VM_ENERGY();
                    l2.name = "L02";
                    l2.data = new decimal[cats.Count];
                    //l2.data = list.Where(a => a.Line == "L02").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l2.data_vm = list.Where(a => a.Line == "L02").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l2);


                    var l3 = new VM_ENERGY();
                    l3.name = "L03";
                    //l3.data = new decimal[cats.Count];
                    //l3.data = list.Where(a => a.Line == "L03").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l3.data_vm = list.Where(a => a.Line == "L03").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l3);



                    var l4 = new VM_ENERGY();
                    l4.name = "L04";
                    //l4.data = new decimal[cats.Count];
                    //l4.data = list.Where(a => a.Line == "L04").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l4.data_vm = list.Where(a => a.Line == "L04").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l4);


                }
                else
                {
                    var i = 0;
                    vm.data1 = JsonSerializeHelper.SerializeObject(cats);

                    var l = new VM_ENERGY();
                    l.name = "L04";
                   
                    l.data_vm = list.Where(a => a.Line == "L04").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    i = i = 1;
                    liste.Add(l);

                }


                vm.data = JsonSerializeHelper.SerializeObject(liste);

            }
            else if (QueryType == "MONTH")
            {
                var CollectMonthFrom = context.Request.Params["CollectMonthFrom"];
                var CollectMonthTo = context.Request.Params["CollectMonthTo"];

                var list = DataAnalyLoader.QueryEnergy4ByMonth(CollectMonthFrom, CollectMonthTo, Category);

                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();
                if (Category == "TIANRANQI" || Category == "ZHENGQI")
                {

                    var l1 = new VM_ENERGY();
                    l1.name = "L01";
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Line == "L01").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Line == "L01").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l1);

                    var l2 = new VM_ENERGY();
                    l2.name = "L02";
                    //l2.data = new decimal[cats.Count];
                    //l2.data = list.Where(a => a.Line == "L02").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l2.data_vm = list.Where(a => a.Line == "L02").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l2);


                    var l3 = new VM_ENERGY();
                    l3.name = "L03";
                    //l3.data = new decimal[cats.Count];
                    //l3.data = list.Where(a => a.Line == "L03").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l3.data_vm = list.Where(a => a.Line == "L03").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l3);



                    var l4 = new VM_ENERGY();
                    l4.name = "L04";
                    //l4.data = new decimal[cats.Count];
                    //l4.data = list.Where(a => a.Line == "L04").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l4.data_vm = list.Where(a => a.Line == "L04").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l4);


                }
                else
                {
                    var i = 0;
                    vm.data1 = JsonSerializeHelper.SerializeObject(cats);

                    var l = new VM_ENERGY();
                    l.name = "L04";
                    
                    l.data_vm = list.Where(a => a.Line == "L04").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    i = i = 1;
                    liste.Add(l);

                }


                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }


            else if (QueryType == "YEAR")
            {
                var CollectYearFrom = context.Request.Params["CollectYearFrom"];
                var CollectYearTo = context.Request.Params["CollectYearTo"];

                var list = DataAnalyLoader.QueryEnergy4ByYear(CollectYearFrom, CollectYearTo, Category);
                //vm.data = JsonSerializeHelper.SerializeObject(list);
                var cats = list.Select(a => a.CollectDate).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();
                if (Category == "TIANRANQI" || Category == "ZHENGQI")
                {

                    var l1 = new VM_ENERGY();
                    l1.name = "L01";
                    //l1.data = new decimal[cats.Count];
                    //l1.data = list.Where(a => a.Line == "L01").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l1.data_vm = list.Where(a => a.Line == "L01").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l1);

                    var l2 = new VM_ENERGY();
                    l2.name = "L02";
                    //l2.data = new decimal[cats.Count];
                    //l2.data = list.Where(a => a.Line == "L02").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l2.data_vm = list.Where(a => a.Line == "L02").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l2);


                    var l3 = new VM_ENERGY();
                    l3.name = "L03";
                    //l3.data = new decimal[cats.Count];
                    //l3.data = list.Where(a => a.Line == "L03").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l3.data_vm = list.Where(a => a.Line == "L03").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l3);



                    var l4 = new VM_ENERGY();
                    l4.name = "L04";
                    //l4.data = new decimal[cats.Count];
                    //l4.data = list.Where(a => a.Line == "L04").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l4.data_vm = list.Where(a => a.Line == "L04").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    liste.Add(l4);


                }
                else
                {
                    var i = 0;
                    vm.data1 = JsonSerializeHelper.SerializeObject(cats);

                    var l = new VM_ENERGY();
                    l.name = "L04";
                    //l.data = new decimal[cats.Count];
                    //l.data = list.Where(a => a.Line == "L04").Select(b => Convert.ToDecimal(b.TotalValue)).ToArray();
                    l.data_vm = list.Where(a => a.Line == "L04").Select(b => new { b.CollectDate, b.TotalValue }).ToArray();
                    i = i = 1;
                    liste.Add(l);



                }


                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }


            //vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }


        public IndexVM EnergyQueryCurrent(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            try
            {
                var Category = context.Request.Params["Category"];
                var Line = context.Request.Params["Line"];
                var list = DataAnalyLoader.EnergyQueryCurrent(Category, Line);
                var cats = list.Select(a => a.CTime).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();

                var l1 = new VM_ENERGY();
                l1.name = Line;
                l1.data_vm = list.Select(b => new { b.CTime, b.CollectValue }).ToArray();
                liste.Add(l1);
                vm.data = JsonSerializeHelper.SerializeObject(liste);

            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }


        public IndexVM PowerQueryCurrent(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            try
            {
                var Position = context.Request.Params["Position"];
                var list = DataAnalyLoader.PowerQueryCurrent(Position);
                var cats = list.Select(a => a.CTime).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();

                var l1 = new VM_ENERGY();
                l1.name = Position;
                l1.data_vm = list.Select(b => new { b.CTime, b.CollectValue }).ToArray();
                liste.Add(l1);
                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public IndexVM PowerQueryCurrentLine(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            try
            {
                var Line = context.Request.Params["Line"];
                var list = DataAnalyLoader.PowerQueryCurrentLine(Line);
                var cats = list.Select(a => a.CTime).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();

                var l1 = new VM_ENERGY();
                l1.name = Line;
                l1.data_vm = list.Select(b => new { b.CTime, b.CollectValue }).ToArray();
                liste.Add(l1);
                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }
        public IndexVM MaterialQueryCurrentLine(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            try
            {
                var Position = context.Request.Params["Position"];
                //var Position = "222";
                var CollectDateFrom = context.Request.Params["CollectDateFrom"];
                var CollectDateTo = context.Request.Params["CollectDateTo"];
                var list = DataAnalyLoader.MaterialQueryCurrent(Position, CollectDateFrom, CollectDateTo);
                var cats = list.Select(a => a.CTime).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();

                //var l1 = new VM_ENERGY();
                if (Position =="全部选择")
                {
                    //l1.name2 = list.Select(c => c.MachineName).ToArray();
                    //var l1 = new VM_ENERGY();
                    var name = list.Select(c => c.ENERGY_TYPE).Distinct().ToList();
                    for (int i = 0; i < name.Count; i++)
                    {
                        var l1 = new VM_ENERGY();
                        l1.name = name[i];
                        //l1.data = new decimal[cats.Count];
                        //l1.data = list.Where(a => a.Position == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                        l1.data_vm = list.Where(a => a.ENERGY_TYPE == l1.name).Select(b => new { b.CTime, b.Collect_Value }).ToArray();
                        liste.Add(l1);
                    }



                }
                else
                {
                    var l1 = new VM_ENERGY();
                    l1.name = Position;
                    l1.data_vm = list.Select(b => new { b.CTime, b.Collect_Value }).ToArray();
                    liste.Add(l1);
                }

                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }
        public IndexVM PowerDayquery(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectDateFrom = context.Request.Params["CollectDateFrom"];
                var CollectDateTo = context.Request.Params["CollectDateTo"];
                var list = DataAnalyLoader.POWERDAYQueryCurrent(Position, CollectDateFrom, CollectDateTo);
                var cats = list.Select(a => a.CTime).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();

                //var l1 = new VM_ENERGY();
                if (Position == "全部电表")
                {
                    //l1.name2 = list.Select(c => c.MachineName).ToArray();
                    //var l1 = new VM_ENERGY();
                    var name = list.Select(c => c.POSITION).Distinct().ToList();
                    for (int i = 0; i < name.Count; i++)
                    {
                        var l1 = new VM_ENERGY();
                        l1.name = name[i];
                        //l1.data = new decimal[cats.Count];
                        //l1.data = list.Where(a => a.Position == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                        l1.data_vm = list.Where(a => a.POSITION == l1.name).Select(b => new { b.CTime, b.Collect_Value }).ToArray();
                        liste.Add(l1);
                    }



                }
                else
                {
                    var l1 = new VM_ENERGY();
                    l1.name = Position;
                    l1.data_vm = list.Select(b => new { b.CTime, b.Collect_Value }).ToArray();
                    liste.Add(l1);
                }

                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }
        public IndexVM PowerMonthquery(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectMonthFrom = context.Request.Params["CollectMonthFrom"];
                var CollectMonthTo = context.Request.Params["CollectMonthTo"];
                var list = DataAnalyLoader.POWERMonthQueryCurrent(Position, CollectMonthFrom, CollectMonthTo);
                var cats = list.Select(a => a.CTime).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();

                //var l1 = new VM_ENERGY();
                if (Position == "全部电表")
                {
                    //l1.name2 = list.Select(c => c.MachineName).ToArray();
                    //var l1 = new VM_ENERGY();
                    var name = list.Select(c => c.POSITION).Distinct().ToList();
                    for (int i = 0; i < name.Count; i++)
                    {
                        var l1 = new VM_ENERGY();
                        l1.name = name[i];
                        //l1.data = new decimal[cats.Count];
                        //l1.data = list.Where(a => a.Position == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                        l1.data_vm = list.Where(a => a.POSITION == l1.name).Select(b => new { b.CTime, b.Collect_Value }).ToArray();
                        liste.Add(l1);
                    }



                }
                else
                {
                    var l1 = new VM_ENERGY();
                    l1.name = Position;
                    l1.data_vm = list.Select(b => new { b.CTime, b.Collect_Value }).ToArray();
                    liste.Add(l1);
                }

                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }
        public IndexVM PowerYearquery(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            try
            {
                var Position = context.Request.Params["Position"];
                var CollectYearFrom = context.Request.Params["CollectYearFrom"];
                var CollectYearTo = context.Request.Params["CollectYearTo"];
                var list = DataAnalyLoader.POWERYearQueryCurrent(Position, CollectYearFrom, CollectYearTo);
                var cats = list.Select(a => a.CTime).Distinct().ToList();
                vm.data1 = JsonSerializeHelper.SerializeObject(cats);
                var liste = new List<VM_ENERGY>();

                //var l1 = new VM_ENERGY();
                if (Position == "全部电表")
                {
                    //l1.name2 = list.Select(c => c.MachineName).ToArray();
                    //var l1 = new VM_ENERGY();
                    var name = list.Select(c => c.POSITION).Distinct().ToList();
                    for (int i = 0; i < name.Count; i++)
                    {
                        var l1 = new VM_ENERGY();
                        l1.name = name[i];
                        //l1.data = new decimal[cats.Count];
                        //l1.data = list.Where(a => a.Position == l1.name).Select(b => Convert.ToDecimal(b.CollectValue)).ToArray();
                        l1.data_vm = list.Where(a => a.POSITION == l1.name).Select(b => new { b.CTime, b.Collect_Value }).ToArray();
                        liste.Add(l1);
                    }



                }
                else
                {
                    var l1 = new VM_ENERGY();
                    l1.name = Position;
                    l1.data_vm = list.Select(b => new { b.CTime, b.Collect_Value }).ToArray();
                    liste.Add(l1);
                }

                vm.data = JsonSerializeHelper.SerializeObject(liste);
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = string.Format("获取图表数据遇到异常{0}", ex.Message);
            }
            return vm;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}