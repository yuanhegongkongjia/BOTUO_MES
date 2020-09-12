using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;

namespace DynamicForm.DA
{
    public class WFCore_StepExecutor_Edit_CustomDA : BaseDA
    {
        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            using (var db = Pub.DB)
            {
                if (string.IsNullOrWhiteSpace(entity["ExecutorValue"]))
                {
                    throw new WFException("请输入自定义SQL语句".GetRes());
                }
                var newEntity = entity.To<WF_M_STEPEXECUTOR>();
                newEntity.ExecutorId = Guid.NewGuid().ToString();
                newEntity.ExecutorPriority = ParseHelper.ParseInt(entity["ExecutorPriority"]);
                db.Insert<WF_M_STEPEXECUTOR>(newEntity);
                message = "新增成功".GetRes();
                return DFPub.EXECUTE_SUCCESS;
            }
        }
    }
}
