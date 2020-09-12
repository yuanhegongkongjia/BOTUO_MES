using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using System.IO;

namespace DynamicForm.DA
{
    public class WFCore_Model_UploadDA : BaseDA
    {
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            var Upload = entity["Upload"];
            if (string.IsNullOrWhiteSpace(Upload))
            {
                throw new WFException("请上传文件");
            }
            var path = DFPub.RelativeToPhysical(Upload);
            if (!File.Exists(path))
            {
                throw new WFException(string.Format("文件 {0} 不存在", path));
            }
            WFDA.Instance.ModelSaveToDB(path);

            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('上传成功');");
            message = sb.ToString();
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
