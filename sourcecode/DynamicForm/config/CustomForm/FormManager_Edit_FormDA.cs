using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicForm.DA
{
    public class FormManager_Edit_FormDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            var f = SessionHelper.Get<FormM>("f");
            dict.Add("FormName", f.FormName);
            dict.Add("DAImp", f.DAImp);
            dict.Add("FormId", f.FormId);
            dict.Add("FormDesc", f.FormDesc);
            return dict;
        }
    }
}