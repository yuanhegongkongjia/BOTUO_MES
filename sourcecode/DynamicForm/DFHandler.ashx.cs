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
    /// Summary description for DFHandler
    /// </summary>
    public class DFHandler : IHttpHandler, IRequiresSessionState
    {
        
        public static readonly ILog m_log = LogManager.GetLogger("DEBUG");
        public void ProcessRequest(HttpContext context)
        {
            InitializeHelper.Init();
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
                case "queryform":
                    if (dict.Data.ContainsKey(DFPub.DF_DATAGRID_EXPORT))
                    {
                        var vm = QueryForm(dict);
                        if (vm.hasError)
                        {
                            m_log.Error(vm.error);
                        }
                        context.Response.Write(JsonSerializeHelper.SerializeObject(new DataGridVM() { data = DFPub.PhysicalToRelative(vm.data.ToString()) }));
                    }
                    else
                    {
                        context.Response.Write(JsonSerializeHelper.SerializeObject(QueryForm(dict)));
                    }
                    break;
                case "deleteform":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(DeleteForm(dict)));
                    break;
                case "clientreport":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(ClientReport(dict)));
                    break;
                case "querylist":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(QueryList(dict)));
                    break;
                case "saveparameter":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(SaveParameter(context, dict)));
                    break;
                case "uploadfile":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(UploadFile(context, dict)));
                    break;
                case "deleteuploadfile":
                    context.Response.Write(JsonSerializeHelper.SerializeObject(DeleteUploadFile(context, dict)));
                    break;
                //case "kanban":
                //    context.Response.Write(JsonSerializeHelper.SerializeObject(Kanban(context)));
                //    break;
                default:
                    context.Response.Write(JsonSerializeHelper.SerializeObject(ExecuteMethod(dict["action"], dict)));
                    break;
            }
        }

        //public DataGridVM Kanban(HttpContext context)
        //{
        //    var vm = new DataGridVM();


        //    var total = 0;
        //    var list = JLLL_T_ORDERLoader.Kanban(ref total);

        //    vm.rows = list;
        //    vm.results = total;
        //    return vm;
        //}

        private DataGridVM DeleteUploadFile(HttpContext context, DFDictionary dict)
        {
            var vm = new DataGridVM();
            try
            {
                ArgumentCheck.CheckMustInput(dict, "FileId");
                using (var db = Pub.DB)
                {
                    var sql = "delete from XDSW_T_FILE where FileId=@FileId";
                    db.Execute(sql, new { FileId = dict["FileId"] });
                }
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = ex.Message;
            }
            return vm;
        }

        private DataGridVM UploadFile(HttpContext context, DFDictionary dict)
        {
            var vm = new DataGridVM();
            try
            {
                int maxSize = 1024 * 1024 * 10;
                if (context.Request.Files.Count == 0)
                {
                    throw new WFException("请上传文件");
                }
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    var item = context.Request.Files[i];

                    if (item.InputStream == null || item.InputStream.Length > maxSize)
                    {
                        throw new WFException(string.Format("第 {0} 个上传文件大小超过限制", i + 1));
                    }
                }

                var user = Util.GetCurrentUser();
                var list = new List<XDSW_T_FILE>();
                for (int i = 0; i < context.Request.Files.Count; i++)
                {
                    var item = context.Request.Files[i];
                    // 将文件插入到数据库中
                    var entity = new XDSW_T_FILE();
                    using (var db = Pub.DB)
                    {
                        entity.FileId = Guid.NewGuid().ToString();
                        entity.FileName = Path.GetFileName(item.FileName);
                        entity.FileData = StreamHelper.ToBytes(item.InputStream);
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUser = user.UserName;
                        entity.CreateTime = DateTime.Now;
                        entity.CreateUser = user.UserName;
                        list.Add(entity);
                        db.Insert(entity);
                    }
                }
                vm.rows = list.Select(a => new { FileId = a.FileId, FileName = a.FileName }).ToList();
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = ex.Message;
            }
            return vm;
        }

        private DFDictionary ClientReport(DFDictionary dict)
        {
            FormM form = null;
            DFDictionary entity = null;
            DFPub.SetDBEntity(dict, ref form, ref entity);
            var ret = new DFDictionary();
            var message = string.Empty;
            var da = NinjectHelper.Get<IDA>(form.DAImp);
            try
            {
                if (null == da)
                {
                    throw new Exception("Invalid DAImp");
                }
                if (da.Update(form, entity, ref message) != 0)
                {
                    ret.Add("hasError", "true");
                }
                var baseDA = (BaseDA)da;
                ret.Add("Debug", entity["Debug"]);
                ret.Add("ReportName", Path.GetFileName(baseDA.ReportPath));
                ret.Add("UploadFileUrl", baseDA.GetUrlFolder() + "/kindeditor/asp.net/upload_json.ashx");
                ret.Add("ReportUrl", baseDA.GetUrlRoot() + DFPub.PhysicalToRelative(baseDA.ReportPath));
                if (baseDA.ReportDataSource != null)
                {
                    ret.Add("ReportDataSource", Convert.ToBase64String(SerializeHelper.DataSetSerializer(baseDA.ReportDataSource)));
                }
                ret.Add("error", message);
                m_log.Debug(JsonSerializeHelper.SerializeObject(ret));
            }
            catch (Exception ex)
            {
                ret.Add("hasError", "true");
                ret.Add("error", ex.Message);
                m_log.Error(ex.Message, ex);
            }
            return ret;
        }

        /// <summary>
        /// 反射返回参数
        /// </summary>
        /// <param name="serverFunctionName"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        private DataGridVM ExecuteMethod(string serverFunctionName, DFDictionary dict)
        {
            try
            {
                var array = serverFunctionName.Split('.');
                if (array.Length <= 1)
                {
                    throw new WFException(string.Format("无效的方法 {0}", serverFunctionName));
                }
                var name = string.Join(".", array.Take(array.Count() - 1));
                var method = array.Last();

                var type = System.Reflection.Assembly.GetExecutingAssembly().GetType(name);
                if (type == null)
                {
                    array = name.Split('.');
                    var assembly = string.Join(".", array.Take(array.Count() - 1));
                    type = Type.GetType(string.Format("{0},{1}", name, assembly));
                }
                var instance = type.CreateInstance();
                MethodInvoker mi = type.DelegateForCallMethod(method, new[] { typeof(DFDictionary) });
                var ret = mi(instance, dict);
                var vm = ret as DataGridVM;
                if (vm == null)
                {
                    throw new WFException(string.Format("方法 {0} 返回值不是类型 DataGridVM", serverFunctionName));
                }
                return vm;
            }
            catch (Exception ex)
            {
                var vm = new DataGridVM();
                vm.hasError = true;
                vm.error = ex.Message;
                return vm;
            }
        }

        /// <summary>
        /// 反射测试
        /// API 调用
        /// http://localhost/xdsw/DFHandler.ashx?action=DynamicForm.DFHandler.Test&Age=1&Name=庄国龙
        /// 客户端 JS 调用
        /// dfGetData('DynamicForm.DFHandler.Test', {'Age':'1','Name':'Test'}, function(data){ dfLog(data.rows.Age); });
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private DataGridVM Test(DFDictionary dict)
        {
            var vm = new DataGridVM();
            vm.rows = new { Name = dict["Name"], Age = dict["Age"], CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") };
            return vm;
        }

        [Obsolete("该方法已经过期，请使用 DynamicForm.Core.BaseDA.SaveClientData 方法代替")]
        public DataGridVM SaveData(DFDictionary dict)
        {
            var vm = new DataGridVM();
            SaveParameter(dict);
            return vm;
        }

        [Obsolete("该方法已经过期，请使用 DynamicForm.Core.BaseDA.SaveClientData 方法代替")]
        public static void SaveParameter(object obj)
        {
            var context = HttpContext.Current;
            context.Session[DFPub.DF_DATA_EXCHANGE] = obj;
        }

        [Obsolete("该方法已经过期，请使用 DynamicForm.Core.BaseDA.GetClientData 方法代替")]
        public static object GetObjectParameter()
        {
            var context = HttpContext.Current;
            return context.Session[DFPub.DF_DATA_EXCHANGE];
        }

        [Obsolete("该方法已经过期，这个方法不经常使用，如果要使用，需要封装在 BaseDA 中")]
        internal static void ClearObjectParameter()
        {
            var context = HttpContext.Current;
            context.Session[DFPub.DF_DATA_EXCHANGE] = null;
        }

        //[Obsolete("该方法已经过期，请使用 DynamicForm.Core.BaseDA.SaveClientData 方法代替")]
        private object SaveParameter(HttpContext context, DFDictionary dict)
        {
            context.Session[DFPub.DF_DATA_EXCHANGE] = dict[DFPub.DF_DATA_EXCHANGE];
            return null;
        }

        [Obsolete("该方法已经过期，请使用 DynamicForm.Core.BaseDA.GetClientData 方法代替")]
        public static List<Dictionary<string, string>> GetParameter()
        {

            var data = HttpContext.Current.Session[DFPub.DF_DATA_EXCHANGE];
            if (data == null || string.IsNullOrWhiteSpace(data.ToString()))
            {
                return null;
            }
            else
            {
                return JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(data.ToString());
            }
        }


        private object QueryList(DFDictionary dict)
        {
            var vm = QueryForm(dict);
            return vm;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        internal static DataGridVM QueryForm(DFDictionary queryParameters)
        {
            FormM form = null;
            DFDictionary entity = null;
            DFPub.SetDBEntity(queryParameters, ref form, ref entity);
            if (null == form)
            {
                throw new ArgumentException("DF_FORMNAME");
            }
            var start = ParseHelper.ParseInt(queryParameters["start"]).GetValueOrDefault();
            var limit = ParseHelper.ParseInt(queryParameters["limit"]).GetValueOrDefault();
            // 如果客户端是导出，那么就设置记录数为最大值
            if (queryParameters.Data.ContainsKey(DFPub.DF_DATAGRID_EXPORT))
            {
                start = 0;
                limit = int.MaxValue;
            }
            var vm = new DataGridVM();
            var message = string.Empty;
            if (!string.IsNullOrWhiteSpace(form.DAImp))
            {
                var da = NinjectHelper.Get<IDA>(form.DAImp);
                try
                {
                    if (null == da)
                    {
                        throw new Exception("Invalid DAImp");
                    }
                    da.Query(form, entity, vm, start, limit, ref message);
                    if (vm.rows == null)
                    {
                        vm.rows = new List<string>();
                    }
                    if (vm.rows.GetType() == typeof(DataTable))
                    {
                        if (entity.Data.ContainsKey(DFPub.DF_DATAGRID_EXPORT))
                        {
                            vm.data = ExportHelper.Export(((DataTable)vm.rows), form, vm.data as List<GridColumnM>);
                        }
                    }
                    else
                    {
                        ConvertToDisplayText((IList)vm.rows);
                        if (entity.Data.ContainsKey(DFPub.DF_DATAGRID_EXPORT))
                        {
                            vm.data = ExportHelper.Export(((IList)vm.rows), form);
                        }
                    }
                }
                catch (Exception ex)
                {
                    m_log.Error(ex.Message, ex);
                    vm.hasError = true;
                    vm.error = ex.Message;
                    message = ex.Message;
                }
            }
            if (vm.rows == null)
            {
                vm.rows = new List<string>();
            }
            return vm;
        }

        /// <summary>
        /// 将代码转成文本
        /// </summary>
        /// <param name="list"></param>
        public static void ConvertToDisplayText(IList list)
        {
            Type objType = list.GetType().GetGenericArguments()[0];
            object value = null;
            if (list != null && list.Count > 0)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objType);
                foreach (PropertyDescriptor property in properties)
                {
                    foreach (var attribute in property.Attributes)
                    {
                        if (attribute.GetType() == typeof(DateTimeConverterAttribute))
                        {
                            var attr = (DateTimeConverterAttribute)attribute;
                            foreach (var item in list)
                            {
                                if (string.IsNullOrWhiteSpace(attr.SourceField))
                                {
                                    value = property.GetValue(item);
                                }
                                else
                                {
                                    value = item.GetPropertyValue(attr.SourceField);
                                }
                                var dt = ParseHelper.ParseDate(string.Format("{0}", value));
                                if (!dt.HasValue || dt < DateTime.Parse("1970-01-01"))
                                {
                                    property.SetValue(item, string.Empty);
                                }
                                else
                                {
                                    property.SetValue(item, dt.Value.ToString(attr.Format));
                                }
                            }
                        }
                        else if (attribute.GetType() == typeof(PublicCodeConverterAttribute))
                        {
                            var attr = (PublicCodeConverterAttribute)attribute;
                            foreach (var item in list)
                            {
                                if (string.IsNullOrWhiteSpace(attr.SourceField))
                                {
                                    value = property.GetValue(item);
                                }
                                else
                                {
                                    value = item.GetPropertyValue(attr.SourceField);
                                }
                                property.SetValue(item, PUBLICCODELoader.Value2DisplayText(attr.CodeType, string.Format("{0}", value)));
                            }
                        }
                    }


                    if (property.Attributes.Count > 0)
                    {
                        var attribute = property.Attributes[0];
                    }
                }
            }
        }

        internal static DataGridVM DeleteForm(DFDictionary queryParameters)
        {
            FormM form = null;
            DFDictionary entity = null;
            DFPub.SetDBEntity(queryParameters, ref form, ref entity);
            var vm = new DataGridVM();
            var message = string.Empty;
            var da = NinjectHelper.Get<IDA>(form.DAImp);
            try
            {
                if (null == da)
                {
                    throw new Exception("Invalid DAImp");
                }
                if (da.Delete(form, entity, vm, ref message) != 0)
                {
                    vm.hasError = true;
                }
                vm.error = message;
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message, ex);
                vm.hasError = true;
                vm.error = ex.Message;
            }
            return vm;
        }
    }

}