using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WFCommon.Utility;

namespace DynamicForm.DA
{
    public class FormManagerDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
        }

        public DataGridVM GetTree(DFDictionary dict)
        {
            var vm = new DataGridVM();
            var di = new DirectoryInfo(DFPub.ConfigFolder);
            var nodes = new List<WFTreeNode>();
            nodes.Add(new WFTreeNode() { id = "ROOT", pid = string.Empty, text = "所有表单" });

            foreach (var item in di.GetFiles("*.xml", SearchOption.AllDirectories).OrderBy(a => a.FullName))
            {
                var el = XElement.Load(item.FullName);
                if (el.Name != "Form")
                {
                    continue;
                }
                nodes.Add(new WFTreeNode()
                {
                    id = DFPub.GetAttrValue(el, "Name"),
                    pid = "ROOT",
                    text = string.Format("{0} {1}", DFPub.GetAttrValue(el, "Name"), DFPub.GetAttrValue(el, "Desc"))
                });
            }

            var listStatus = JsonSerializeHelper.DeserializeObject<List<WFTreeNode>>(Base64StringHelper.ConvertFromBase64String(dict["DF_TREE_VIEWSTATE"]));
            var list = WFTreeHelper.GenerateTree("ROOT", nodes);
            // 下面这段代码就是为了处理保持客户端树的状态
            WFTreeHelper.SetStatus(nodes, listStatus);
            vm.rows = list;
            return vm;
        }
    }
}