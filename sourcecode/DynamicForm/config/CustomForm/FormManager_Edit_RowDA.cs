using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicForm.DA
{
    public class FormManager_Edit_RowDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            var f = SessionHelper.Get<FormM>("f");
            // Panel|0|Row|0
            var _Path = entity["_Path"];
            var ss = _Path.Split('|');
            var c = f.Panels[int.Parse(ss[1])].Rows[int.Parse(ss[3])];
            dict.Add("RowAttributes", c.RowAttributes);
            return dict;
        }
    }
}