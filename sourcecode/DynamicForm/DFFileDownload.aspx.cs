using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using DapperExtensions;
using WFCore;
using WFCommon;
using System.IO;
using DynamicForm.Core;
using WFCommon.Utility;

namespace DynamicForm
{
    public partial class DFFileDownload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeHelper.Init();
            var reportFullPath = Path.Combine(DFPub.GetCurrentPhysicalFolder(), string.Format("temp\\{0}", Guid.NewGuid().ToString()));
            var folder = Path.GetDirectoryName(reportFullPath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // 删除以前的文件
            OldFileProcessor.DeleteOldFile(folder, 4);

            // 客户端下载显示的文件名
            var fileName = Path.GetFileName(reportFullPath);

            // 判断是不是从数据库中下载文件
            var FileId = Request["fileid"];
            var DownloadFileName = Request["DownloadFileName"];
            if (!string.IsNullOrWhiteSpace(FileId))
            {
                using (var db = Pub.DB)
                {
                    var file = db.Query<XDSW_T_FILE>("select * from XDSW_T_FILE where FileId=@FileId", new { FileId = FileId }).FirstOrDefault();
                    var stream = StreamHelper.ToStream(file.FileData);
                    StreamHelper.SaveStream(stream, reportFullPath);
                    fileName = file.FileName;
                }
            }

            // 判断是否是下载工作流模型
            var ModelId = Request["ModelId"];
            if (!string.IsNullOrWhiteSpace(ModelId))
            {
                var entity = WFDA.Instance.ModelSaveToLocal(ModelId, reportFullPath);
                fileName = string.Format("{0}.dat", entity.Model.ModelName);
            }
            if (!string.IsNullOrWhiteSpace(DownloadFileName))
            {
                reportFullPath = DownloadFileName;
                var str = DownloadFileName.Split('\\');
                fileName = str[str.Length - 1];// D:\Ceprei\myproject\sumin\sourcecode\DynamicForm\kindeditor\attached\pdf\\20190723\20190723160214_4592.pdf
            }

            //以字符流的形式下载文件
            FileStream fs = new FileStream(reportFullPath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}