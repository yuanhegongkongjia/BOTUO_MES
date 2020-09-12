using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicForm.DA
{
    public class FormManager_Edit_ControlDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            var f = SessionHelper.Get<FormM>("f");
            // Panel|0|Row|0|Control|0
            var _Path = entity["_Path"];
            var ss = _Path.Split('|');
            var c = f.Panels[int.Parse(ss[1])].Rows[int.Parse(ss[3])].Controls[int.Parse(ss[5])];
            dict.Add("Action", c.Action);
            dict.Add("AddEmptyOption", c.AddEmptyOption);
            dict.Add("AfterScript", c.AfterScript);
            dict.Add("AutoPostBack", c.AutoPostBack);
            dict.Add("ControlAttributes", c.ControlAttributes);
            dict.Add("CustomHtml", c.CustomHtml);
            dict.Add("LabelAttributes", c.LabelAttributes);
            dict.Add("MustInput", c.MustInput);
            dict.Add("Name", c.Name);
            dict.Add("Options", c.Options);
            dict.Add("OptionType", c.OptionType);
            dict.Add("OuterAttributes", c.OuterAttributes);
            dict.Add("Text", c.Text);
            dict.Add("Type", c.Type);
            dict.Add("Value", c.Value);
            dict.Add("Width", c.Width);
            return dict;
        }
    }
}