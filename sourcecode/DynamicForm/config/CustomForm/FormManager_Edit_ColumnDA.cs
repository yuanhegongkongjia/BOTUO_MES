using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicForm.DA
{
    public class FormManager_Edit_ColumnDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            var f = SessionHelper.Get<FormM>("f");
            // Panel|0|Column|1
            var _Path = entity["_Path"];
            var ss = _Path.Split('|');
            var c = f.Panels[int.Parse(ss[1])].Columns[int.Parse(ss[3])];
            dict.Add("AfterScript", c.AfterScript);
            dict.Add("Editor", c.Editor);
            dict.Add("Name", c.Name);
            dict.Add("OtherAttributes", c.OtherAttributes);
            dict.Add("Renderer", c.Renderer);
            dict.Add("Sortable", c.Sortable);
            dict.Add("SortName", c.SortName);
            dict.Add("Text", c.Text);
            dict.Add("Visible", c.Visible);
            dict.Add("Width", c.Width);
            return dict;
        }
    }
}