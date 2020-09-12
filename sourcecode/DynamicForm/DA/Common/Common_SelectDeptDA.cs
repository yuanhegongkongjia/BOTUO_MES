using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFCommon.VM;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class Common_SelectDeptDA : BaseDA
    {
        /// <summary>
        ///部门查询
        /// </summary>
        /// <param name="form"></param>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var list = WF_M_DEPTLoader.GetList(entity, ref count, start, limit);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
