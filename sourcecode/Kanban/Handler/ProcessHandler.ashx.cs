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
    /// ProcessHandler 的摘要说明
    /// </summary>
    public class ProcessHandler : IHttpHandler
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
                case "l01mf":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL01MF(context)));
                    break;
                case "l01lw":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL01LW(context)));
                    break;
                case "l01productspeed":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL01ProductSpeed(context)));
                    break;
                case "l01ly"://1号线炉压
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL01LY(context)));
                    break;
                case "l01ua"://1号线MFUA
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL01MFUA(context)));
                    break;
                case "l02ua"://2号线MFUA
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL02MFUA(context)));
                    break;
                case "l03ua"://3号线MFUA
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL03MFUA(context)));
                    break;
                case "l04ua"://4号线MFUA
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL04MFUA(context)));
                    break;
                case "l02mf":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL02MF(context)));
                    break;
                case "l02lw":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL02LW(context)));
                    break;
                case "l02ly"://2号线炉压
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL02LY(context)));
                    break;
                case "l02productspeed":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL02ProductSpeed(context)));
                    break;
                case "l03mf":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL03MF(context)));
                    break;
                case "l03lw":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL03LW(context)));
                    break;
                case "l03ly"://3号线炉压
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL03LY(context)));
                    break;
                case "l04fenquly"://4号线炉压
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL04FenQuLY(context)));
                    break;
                case "l03productspeed":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL03ProductSpeed(context)));
                    break;
                case "l04mf":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL04MF(context)));
                    break;
                case "l04lw":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL04LW(context)));
                    break;
                case "l04productspeed":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetL04ProductSpeed(context)));
                    break;
                case "process":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetProcessData(context)));
                    break;
                case "gettotal":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetTotal(context)));
                    break;
                case "l01dianneng"://分时用量
                    context.Response.Write(JsonSerializeHelper.SerializeObject(Getdianneng(context)));
                    break;
                case "l01diannengday"://每日用量
                    context.Response.Write(JsonSerializeHelper.SerializeObject(Getdiannengday(context)));
                    break;
                case "l01shengchan"://生产模数数据
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetPRODUCTION(context)));
                    break;



                case "getdevicestaus":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetDeviceStatus(context)));
                    break;

                case "getdeviceoeestaus":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetDeviceOeeStatus(context)));
                    break;

                case "gettemstaus":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetTemStatus(context)));
                    break;



                case "1002wd":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetTem1(context)));
                    break;

                case "1033zz":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetProductStatus(context)));
                    break;

                case "1013sd":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetTeme(context)));
                    break;
                case "getcusstaus":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetCusStaus(context)));
                    break;

                case "getenergystaus":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(GetEnergyStaus(context)));
                    break;


                default:
                    break;
            }
        }

        public ResultVM GetEnergyStaus(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetEnergySTATUS("低压车间");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public ResultVM GetDeviceStatus(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetDESTATUS("车间西南角");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }


        public ResultVM GetCusStaus(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetCUSTATUS();


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }



        public ResultVM GetProductStatus(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetPDSTATUS();


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }




        public ResultVM GetDeviceOeeStatus(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetDEOEESTATUS("传送机");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }


        public ResultVM GetTemStatus(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetTEMSTATUS("");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public ResultVM GetTem1(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetTEM1("西南角");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }


        public ResultVM GetTeme(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetTEM2("西南角");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }









        public ResultVM GetTotal(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            

            vm.data = "累计生产时间:100H,累计产量3600公斤";

            return vm;
        }

        public ResultVM GetL01MF(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetMFGL("L01","MF");

           
            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }
        public ResultVM GetL01LY(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLY("L01", "炉子");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public ResultVM GetL02LY(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLY("L02", "炉子");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public ResultVM GetL03LY(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLY("L03", "炉子");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public IndexVM GetL04FenQuLY(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetFenQuLY("L04", "炉子");


            vm.data = JsonSerializeHelper.SerializeObject(list);
            vm.data1 = KanbanProcessLoader.GetTotal("L04");
            return vm;
        }
        public IndexVM GetProcessData(HttpContext context)
        {

            var vm = new IndexVM();
            vm.hasError = false;
            var list = new List<VM_SM_T_PROCESS_PRODUCT>();

            list = KanbanProcessLoader.GetProcessData();

            var list1 = KanbanProcessLoader.GetProductQty();
            vm.data = JsonSerializeHelper.SerializeObject(list);
            vm.data1= JsonSerializeHelper.SerializeObject(list1);
            vm.data2 = SM_T_ENVIRONMENTLoader.GetEnvironment();

            return vm;
        }

        public IndexVM GetL01MFUA(HttpContext context)

        {
            var vm = new IndexVM();
            vm.hasError = false;
            //var list = KanbanProcessLoader.GetProcessData();
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetMFUA("L01", "MF");


            vm.data = JsonSerializeHelper.SerializeObject(list);
            var total = KanbanProcessLoader.GetTotal("L01");
            vm.data1= total+KanbanProcessLoader.GetMFUAAlarm("L01");
            return vm;
        }

        public IndexVM GetL02MFUA(HttpContext context)

        {
            var vm = new IndexVM();
            vm.hasError = false;
            //var list = KanbanProcessLoader.GetProcessData();
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetMFUA("L02", "MF");
            var total = KanbanProcessLoader.GetTotal("L02");
            vm.data1 = total+KanbanProcessLoader.GetMFUAAlarm("L02");
            vm.data = JsonSerializeHelper.SerializeObject(list);



            return vm;
        }

        public IndexVM GetL03MFUA(HttpContext context)

        {
            var vm = new IndexVM();
            vm.hasError = false;
            //var list = KanbanProcessLoader.GetProcessData();
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetMFUA("L03", "MF");
            var total = KanbanProcessLoader.GetTotal("L03");
            vm.data1 = total+KanbanProcessLoader.GetMFUAAlarm("L03");
            vm.data = JsonSerializeHelper.SerializeObject(list);



            return vm;
        }

        public IndexVM GetL04MFUA(HttpContext context)

        {
            var vm = new IndexVM();
            vm.hasError = false;
            //var list = KanbanProcessLoader.GetProcessData();
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetMFUA("L04", "MF");
            var total = KanbanProcessLoader.GetTotal("L04");
            vm.data1 = total+KanbanProcessLoader.GetMFUAAlarm("L04");

            vm.data = JsonSerializeHelper.SerializeObject(list);



            return vm;
        }
        public IndexVM GetL01LW(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLW("L01", "炉子");

           

            //查找报警信息
            var msg = KanbanProcessLoader.GetLWAlarm("L01");
            vm.data = JsonSerializeHelper.SerializeObject(list);// json 字符串
            var total = KanbanProcessLoader.GetTotal("L01");
            vm.data1 = total+msg;
            return vm;
        }

        public IndexVM GetL02LW(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLW("L02", "炉子");


            vm.data = JsonSerializeHelper.SerializeObject(list);// json 字符串
            var total = KanbanProcessLoader.GetTotal("L02");
            vm.data1= total+KanbanProcessLoader.GetLWAlarm("L02");
            return vm;
        }

        public ResultVM GetL01ProductSpeed(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetL01ProductSpeed("L01", "收线");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public ResultVM GetL02MF(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetMFGL("L02", "MF");

     
            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        /// <summary>
        /// 1区炉温
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ResultVM GetL02LW1(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLW("L02", "炉子", "1区温度"); 

            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        /// <summary>
        /// 2区炉温
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ResultVM GetL02LW2(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLW("L02", "炉子", "2区温度");

            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public ResultVM GetL02ProductSpeed(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetL01ProductSpeed("L02", "收线");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public ResultVM GetL03MF(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetMFGL("L03", "MF");

            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }


        public IndexVM GetL03LW(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLW("L03", "炉子");


            vm.data = JsonSerializeHelper.SerializeObject(list);
            var total = KanbanProcessLoader.GetTotal("L03");
            vm.data1 = total+KanbanProcessLoader.GetLWAlarm("L03");
            return vm;
        }

        public ResultVM GetL03ProductSpeed(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();
            list = KanbanProcessLoader.GetL01ProductSpeed("L03", "收线");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }

        public IndexVM GetL04MF(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetMFGL("L04", "MF");

       
            vm.data = JsonSerializeHelper.SerializeObject(list);
            vm.data1 = KanbanProcessLoader.GetLWAlarm("L04");
            return vm;
        }


        public IndexVM GetL04LW(HttpContext context)
        {
            var vm = new IndexVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetLW("L04", "炉子");


            vm.data = JsonSerializeHelper.SerializeObject(list);
            var total = KanbanProcessLoader.GetTotal("L04");
            vm.data1 = total+KanbanProcessLoader.GetLWAlarm("L04");
            return vm;
        }

        public ResultVM GetL04ProductSpeed(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetL01ProductSpeed("L04", "收线");


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }
        public ResultVM Getdianneng(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetL01dianneng();


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }
        public ResultVM Getdiannengday(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetL01diannengday();


            vm.data = JsonSerializeHelper.SerializeObject(list);

            return vm;
        }
        public ResultVM GetPRODUCTION(HttpContext context)
        {
            var vm = new ResultVM();
            vm.hasError = false;
            var list = new List<VM_PROCESS>();

            list = KanbanProcessLoader.GetPRODUCTION();


            vm.data = JsonSerializeHelper.SerializeObject(list);

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