using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFCommon.Utility;

namespace DynamicForm.DA
{
    public class FormManager_EditDA : BaseDA
    {

        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var formName = entity["FormName"];
            var f = DFPub.GetFormM(formName);
            SessionHelper.Save<FormM>("f", f);
            return base.Get(form, entity, ref message);
        }

        private List<WFTreeNode> Generate(FormM f)
        {
            var nodes = new List<WFTreeNode>();
            var root = new WFTreeNode() { id = "ROOT", pid = string.Empty, text = f.FormName };
            nodes.Add(root);

            for (int p = 0; p < f.Panels.Count; p++)
            {
                var panel = f.Panels[p];
                var panelNode = new WFTreeNode() { id = string.Format("Panel|{0}", p), pid = root.id, text = panel.PanelType };
                nodes.Add(panelNode);
                for (int r = 0; r < panel.Rows.Count; r++)
                {
                    var rowNode = new WFTreeNode() { id = string.Format("{0}|Row|{1}", panelNode.id, r), pid = panelNode.id, text = "Row" };
                    nodes.Add(rowNode);
                    for (int c1 = 0; c1 < panel.Rows[r].Controls.Count; c1++)
                    {
                        var control = panel.Rows[r].Controls[c1];
                        var controlNode = new WFTreeNode() { id = string.Format("{0}|Control|{1}", rowNode.id, c1), pid = rowNode.id, text = string.Format("{0}{1}", control.Name, control.Text) };
                        nodes.Add(controlNode);
                    }
                }

                for (int c2 = 0; c2 < panel.Columns.Count; c2++)
                {
                    var column = panel.Columns[c2];
                    var columnNode = new WFTreeNode() { id = string.Format("{0}|Column|{1}", panelNode.id, c2), pid = panelNode.id, text = string.Format("{0}{1}", column.Name, column.Text) };
                    nodes.Add(columnNode);
                }
            }
            return nodes;
        }

        public DataGridVM GetTree(DFDictionary dict)
        {
            var vm = new DataGridVM();
            var nodes = Generate(SessionHelper.Get<FormM>("f"));
            var listStatus = JsonSerializeHelper.DeserializeObject<List<WFTreeNode>>(Base64StringHelper.ConvertFromBase64String(dict["DF_TREE_VIEWSTATE"]));
            var list = WFTreeHelper.GenerateTree("ROOT", nodes);
            // 下面这段代码就是为了处理保持客户端树的状态
            WFTreeHelper.SetStatus(nodes, listStatus);
            vm.rows = list;
            return vm;
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (GetSubmitButton(entity) == "btnCopy")
            {
                return btnCopy(form, entity, ref message);
            }
            else if (GetSubmitButton(entity) == "btnUp")
            {
                return btnUp(form, entity, ref message);
            }
            else if (GetSubmitButton(entity) == "btnDown")
            {
                return btnDown(form, entity, ref message);
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        private int btnDown(FormM form, DFDictionary entity, ref string message)
        {
            var list = base.GetBase64Data<WFTreeNode>(entity, "DF_TREE_VIEWSTATE");
            var selected = list.FirstOrDefault(a => a.selected);
            if (selected != null && selected.id != "ROOT")
            {
                selected.selected = false;
                var f = SessionHelper.Get<FormM>("f");
                var ss = selected.id.Split('|').ToList();
                if (ss.Contains("Control"))
                {
                    var row = f.Panels[int.Parse(ss[1])].Rows[int.Parse(ss[3])];
                    var source = row.Controls[int.Parse(ss[5])];
                    row.Controls.Move<ControlM>(source, 1);
                }
                else if (ss.Contains("Column"))
                {
                    var panel = f.Panels[int.Parse(ss[1])];
                    var source = panel.Columns[int.Parse(ss[3])];
                    panel.Columns.Move<ColumnM>(source, 1);
                }
                else if (ss.Contains("Row"))
                {
                    var panel = f.Panels[int.Parse(ss[1])];
                    var source = panel.Rows[int.Parse(ss[3])];
                    panel.Rows.Move<RowM>(source, 1);
                }
                else
                {
                    var source = f.Panels[int.Parse(ss[1])];
                    f.Panels.Add(source.Copy(f));
                    f.Panels.Move<PanelM>(source, 1);
                }
                SessionHelper.Save<FormM>("f", f);
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        private int btnUp(FormM form, DFDictionary entity, ref string message)
        {
            var list = base.GetBase64Data<WFTreeNode>(entity, "DF_TREE_VIEWSTATE");
            var selected = list.FirstOrDefault(a => a.selected);
            if (selected != null && selected.id != "ROOT")
            {
                selected.selected = false;
                var f = SessionHelper.Get<FormM>("f");
                var ss = selected.id.Split('|').ToList();
                if (ss.Contains("Control"))
                {
                    var row = f.Panels[int.Parse(ss[1])].Rows[int.Parse(ss[3])];
                    var source = row.Controls[int.Parse(ss[5])];
                    row.Controls.Move<ControlM>(source, -1);
                }
                else if (ss.Contains("Column"))
                {
                    var panel = f.Panels[int.Parse(ss[1])];
                    var source = panel.Columns[int.Parse(ss[3])];
                    panel.Columns.Move<ColumnM>(source, -1);
                }
                else if (ss.Contains("Row"))
                {
                    var panel = f.Panels[int.Parse(ss[1])];
                    var source = panel.Rows[int.Parse(ss[3])];
                    panel.Rows.Move<RowM>(source, -1);
                }
                else
                {
                    var source = f.Panels[int.Parse(ss[1])];
                    f.Panels.Add(source.Copy(f));
                    f.Panels.Move<PanelM>(source, -1);
                }
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        private int btnCopy(FormM form, DFDictionary entity, ref string message)
        {
            var list = base.GetBase64Data<WFTreeNode>(entity, "DF_TREE_VIEWSTATE");
            var selected = list.FirstOrDefault(a => a.selected);
            if (selected != null && selected.id != "ROOT")
            {
                selected.selected = false;
                var f = SessionHelper.Get<FormM>("f");
                var ss = selected.id.Split('|').ToList();
                if (ss.Contains("Control"))
                {
                    var row = f.Panels[int.Parse(ss[1])].Rows[int.Parse(ss[3])];
                    var source = row.Controls[int.Parse(ss[5])];
                    row.Controls.Add(source.Copy(f, row));
                }
                else if (ss.Contains("Column"))
                {
                    var panel = f.Panels[int.Parse(ss[1])];
                    var source = panel.Columns[int.Parse(ss[3])];
                    panel.Columns.Add(source.Copy(f, panel));
                }
                else if (ss.Contains("Row"))
                {
                    var panel = f.Panels[int.Parse(ss[1])];
                    var source = panel.Rows[int.Parse(ss[3])];
                    panel.Rows.Add(source.Copy(f, panel));
                }
                else
                {
                    var source = f.Panels[int.Parse(ss[1])];
                    f.Panels.Add(source.Copy(f));
                }
                SessionHelper.Save<FormM>("f", f);
            }
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}