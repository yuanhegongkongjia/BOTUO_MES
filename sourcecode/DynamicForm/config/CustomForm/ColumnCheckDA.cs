using Dapper;
using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WFCommon.Utility;
using WFCore;

namespace DynamicForm.DA
{
    public class ColumnCheckDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            var tableName = entity["TableName"];
            List<VM_ColumnMetadata> list = null;

            // 如果是客户端点击查询按钮，或者回传回的数据没有客户端数据
            IDBHelper dbHelper;
            using (var db = Pub.DB)
            {
                dbHelper = DBHelper.GetDBHelper(db);
            }
            list = dbHelper.LoadColumns(tableName);
            SetSelectDataSource(form, "GetValueList", list.Select(a => new DFSelectItem(a.ColumnText + " " + a.ColumnName, a.ColumnName)).ToList(), true);
            base.SetAccess(form, entity);
        }
    }
}