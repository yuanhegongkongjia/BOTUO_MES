using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using Dapper;
using DapperExtensions;
using System.IO;

namespace DynamicForm.kindeditor
{
    /// <summary>
    /// DFFileUploadHandler 的摘要说明
    /// </summary>
    public class DFFileUploadHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int maxSize = 1024 * 1024 * 1024;
            if(context.Request.Files.Count == 0)
            {
                showError(context, "请选择文件".GetRes());
                return;
            }
            HttpPostedFile imgFile = context.Request.Files[0];
            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError(context, "上传文件大小超过限制".GetRes());
                return;
            }

            try
            {
                var user = Util.GetCurrentUser();


                // 将文件插入到数据库中
                var entity = new XDSW_T_FILE();
                using (var db = Pub.DB)
                {
                    //var sql = "select top 1 FileId from XDSW_T_FILE where FileName=@FileName";
                    //var dbFileId = db.Query<string>(sql, new { FileName = Path.GetFileName(imgFile.FileName) }).FirstOrDefault();
                    //if (!string.IsNullOrWhiteSpace(dbFileId))
                    //{
                    //    showError(context, "文件名已经存在".GetRes());
                    //    return;
                    //}
                    entity.FileId = Guid.NewGuid().ToString();
                    entity.FileName = Path.GetFileName(imgFile.FileName);
                    entity.FileData = StreamHelper.ToBytes(imgFile.InputStream);
                    entity.LastModifyTime = DateTime.Now;
                    entity.LastModifyUser = user.UserName;
                    entity.CreateTime = DateTime.Now;
                    entity.CreateUser = user.UserName;
                    entity.Extend01 = "TempFile";
                    db.Insert(entity);
                }
                Hashtable hash = new Hashtable();
                hash["error"] = 0;
                hash["fileName"] = entity.FileName;
                hash["fileId"] = entity.FileId;
                hash["message"] = "上传成功";
                context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                context.Response.Write(JsonSerializeHelper.SerializeObject(hash));
            }
            catch (Exception ex)
            {
                showError(context, ex.Message);
                return;
            }
        }

        private void showError(HttpContext context, string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            context.Response.Write(JsonSerializeHelper.SerializeObject(hash));
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