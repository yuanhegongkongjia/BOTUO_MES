using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicForm.DA
{
    public class FormManager_Edit_PanelDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            var f = SessionHelper.Get<FormM>("f");
            // Panel|0
            var _Path = entity["_Path"];
            var ss = _Path.Split('|');
            var c = f.Panels[int.Parse(ss[1])];
            dict.Add("Additional", c.Additional);
            dict.Add("AfterHtml", c.AfterHtml);
            dict.Add("AfterScript", c.AfterScript);
            dict.Add("BeforeHtml", c.BeforeHtml);
            dict.Add("PanelDesc", c.PanelDesc);
            dict.Add("PanelId", c.PanelId);
            dict.Add("PanelName", c.PanelName);
            dict.Add("PanelPlugins", c.PanelPlugins);
            dict.Add("PanelType", c.PanelType);
            dict.Add("Position", c.Position);
            dict.Add("TabContainer", c.TabContainer);
            dict.Add("ToolbarScript", c.ToolbarScript);
            return dict;
        }
    }
}