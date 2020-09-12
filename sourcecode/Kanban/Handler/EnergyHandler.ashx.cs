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
namespace Kanban.Handler
{
    /// <summary>
    /// EnergyHandler 的摘要说明
    /// </summary>
    public class EnergyHandler : IHttpHandler
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
                case "getpower":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetPower(context)));
                    break;
                case "getchunshui":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetChunShui(context)));
                    break;
                case "getzilaishui":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetZilaiShui(context)));
                    break;
                case "getlinsuan":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetLinSuan(context)));
                    break;
                case "getyansuan":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetYanSuan(context)));
                    break;
                case "getjian":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetJian(context)));
                    break;
                case "gettianranqi":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetTianRanQi(context)));
                    break;
                case "gettzhengqi":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetZhengQi(context)));
                    break;
                case "getpowerdata":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetPowerData(context)));
                    break;
                case "requestenergyalarm":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetEnergyAlarm(context)));
                    break;
                default:
                    break;
            }
        }

        public ResultVM GetEnergyAlarm(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var Line = context.Request.Params["Line"];
            var msg = KanbanEnergyLoader.GetEnergyAlarm(Line);
            var total = KanbanProcessLoader.GetTotal(Line);
            vm.data = total+msg;
            return vm;
        }

        public ResultVM GetPowerData(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var Line = context.Request.Params["Line"];
            var list = KanbanEnergyLoader.GetPowerData(Line);
            vm.data = JsonSerializeHelper.SerializeObject(list);
            return vm;
        }

        public ResultVM GetPower(HttpContext context) {
            var vm = new ResultVM();
            vm.hasError = false;
            var Line = context.Request.Params["Line"];
            var list = new List<VM_ENERGY_COLLECT>();

            list = KanbanEnergyLoader.GetPowerData(Line, "");
            var data = new List<VM_ENERGY>();

            var dates = list.Select(a => a.CollectDate).Distinct().ToList();
            foreach(var d in dates)
            {
               var item=data.Where(a=>a.name==d).FirstOrDefault();
                if (item == null)
                {
                    item = new VM_ENERGY();
                    item.name = d;
                    
                    var dlist = list.Where(a => a.CollectDate == d).OrderBy(b => b.Line).ToList();
                    item.data = new Decimal[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        item.data[i] = dlist[i].DataValue;
                    }
                    data.Add(item);
                }
                else
                {

                }
            }

            vm.data = JsonSerializeHelper.SerializeObject(data);

            return vm;
        }
        public ResultVM GetChunShui(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_ENERGY_COLLECT>();
            var Line = context.Request.Params["Line"];
            list = KanbanEnergyLoader.GetChunShuiData(Line, "");
            var data = new List<VM_ENERGY>();

            var dates = list.Select(a => a.CollectDate).Distinct().ToList();
            foreach (var d in dates)
            {
                var item = data.Where(a => a.name == d).FirstOrDefault();
                if (item == null)
                {
                    item = new VM_ENERGY();
                    item.name = d;

                    var dlist = list.Where(a => a.CollectDate == d).OrderBy(b => b.Line).ToList();
                    item.data = new Decimal[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        item.data[i] = dlist[i].DataValue;
                    }
                    data.Add(item);
                }
                else
                {

                }
            }

            vm.data = JsonSerializeHelper.SerializeObject(data);

            return vm;
        }
        public ResultVM GetZilaiShui(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_ENERGY_COLLECT>();
            var Line = context.Request.Params["Line"];
            list = KanbanEnergyLoader.GetZilaiShui(Line, "");
            var data = new List<VM_ENERGY>();

            var dates = list.Select(a => a.CollectDate).Distinct().ToList();
            foreach (var d in dates)
            {
                var item = data.Where(a => a.name == d).FirstOrDefault();
                if (item == null)
                {
                    item = new VM_ENERGY();
                    item.name = d;

                    var dlist = list.Where(a => a.CollectDate == d).OrderBy(b => b.Line).ToList();
                    item.data = new Decimal[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        item.data[i] = dlist[i].DataValue;
                    }
                    data.Add(item);
                }
                else
                {

                }
            }

            vm.data = JsonSerializeHelper.SerializeObject(data);

            return vm;
        }
        public ResultVM GetLinSuan(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_ENERGY_COLLECT>();
            var Line = context.Request.Params["Line"];
            list = KanbanEnergyLoader.GetLinSuanData(Line, "");
            var data = new List<VM_ENERGY>();

            var dates = list.Select(a => a.CollectDate).Distinct().ToList();
            foreach (var d in dates)
            {
                var item = data.Where(a => a.name == d).FirstOrDefault();
                if (item == null)
                {
                    item = new VM_ENERGY();
                    item.name = d;

                    var dlist = list.Where(a => a.CollectDate == d).OrderBy(b => b.Line).ToList();
                    item.data = new Decimal[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        item.data[i] = dlist[i].DataValue;
                    }
                    data.Add(item);
                }
                else
                {

                }
            }

            vm.data = JsonSerializeHelper.SerializeObject(data);

            return vm;
        }
        public ResultVM GetYanSuan(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_ENERGY_COLLECT>();
            var Line = context.Request.Params["Line"];
            list = KanbanEnergyLoader.GetYanSuanData(Line, "");
            var data = new List<VM_ENERGY>();

            var dates = list.Select(a => a.CollectDate).Distinct().ToList();
            foreach (var d in dates)
            {
                var item = data.Where(a => a.name == d).FirstOrDefault();
                if (item == null)
                {
                    item = new VM_ENERGY();
                    item.name = d;

                    var dlist = list.Where(a => a.CollectDate == d).OrderBy(b => b.Line).ToList();
                    item.data = new Decimal[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        item.data[i] = dlist[i].DataValue;
                    }
                    data.Add(item);
                }
                else
                {

                }
            }

            vm.data = JsonSerializeHelper.SerializeObject(data);

            return vm;
        }
        public ResultVM GetJian(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_ENERGY_COLLECT>();
            var Line = context.Request.Params["Line"];
            list = KanbanEnergyLoader.GetJianData(Line, "");
            var data = new List<VM_ENERGY>();

            var dates = list.Select(a => a.CollectDate).Distinct().ToList();
            foreach (var d in dates)
            {
                var item = data.Where(a => a.name == d).FirstOrDefault();
                if (item == null)
                {
                    item = new VM_ENERGY();
                    item.name = d;

                    var dlist = list.Where(a => a.CollectDate == d).OrderBy(b => b.Line).ToList();
                    item.data = new Decimal[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        item.data[i] = dlist[i].DataValue;
                    }
                    data.Add(item);
                }
                else
                {

                }
            }

            vm.data = JsonSerializeHelper.SerializeObject(data);

            return vm;
        }
        public ResultVM GetTianRanQi(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_ENERGY_COLLECT>();
            var Line = context.Request.Params["Line"];
            list = KanbanEnergyLoader.GetgetTianRanQiData(Line, "");
            var data = new List<VM_ENERGY>();

            var dates = list.Select(a => a.CollectDate).Distinct().ToList();
            foreach (var d in dates)
            {
                var item = data.Where(a => a.name == d).FirstOrDefault();
                if (item == null)
                {
                    item = new VM_ENERGY();
                    item.name = d;

                    var dlist = list.Where(a => a.CollectDate == d).OrderBy(b => b.Line).ToList();
                    item.data = new Decimal[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        item.data[i] = dlist[i].DataValue;
                    }
                    data.Add(item);
                }
                else
                {

                }
            }

            vm.data = JsonSerializeHelper.SerializeObject(data);

            return vm;
        }
        public ResultVM GetZhengQi(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var Line = context.Request.Params["Line"];
            var list = new List<VM_ENERGY_COLLECT>();

            list = KanbanEnergyLoader.GettZhengQiData(Line, "");
            var data = new List<VM_ENERGY>();

            var dates = list.Select(a => a.CollectDate).Distinct().ToList();
            foreach (var d in dates)
            {
                var item = data.Where(a => a.name == d).FirstOrDefault();
                if (item == null)
                {
                    item = new VM_ENERGY();
                    item.name = d;

                    var dlist = list.Where(a => a.CollectDate == d).OrderBy(b => b.Line).ToList();
                    item.data = new Decimal[dlist.Count];
                    for (int i = 0; i < dlist.Count; i++)
                    {
                        item.data[i] = dlist[i].DataValue;
                    }
                    data.Add(item);
                }
                else
                {

                }
            }

            vm.data = JsonSerializeHelper.SerializeObject(data);

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