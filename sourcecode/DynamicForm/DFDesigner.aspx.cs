using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFCommon.Utility;
using System.Linq;
using WFCore;

namespace DynamicForm
{
    public partial class DFDesigner : WFPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["action"] == "save")
            {
                var data = Request["data"];
                var title = Request["title"];
                File.WriteAllText(Path.Combine(Server.MapPath(""), "temp", title), data);

                SaveToDB(data, string.Empty);

                this.Response.Write(JsonSerializeHelper.SerializeObject(new ResultVM() { hasError = false, error = string.Empty, data = "保存成功" }));
                this.Response.End();
            }
            if (Request["action"] == "load")
            {
                var title = Request["title"];
                var data = LoadFromDB(title);
                this.Response.Write(data);
                this.Response.End();
            }
        }

        public string LoadFromDB(string ModelName)
        {
            var model = WFDA.Instance.GetModelByName(ModelName);
            if (model == null)
                throw new WFException(string.Format("根据模型名称 {0} 找不到对应的工作流模型", ModelName));

            var steps = WFDA.Instance.GetSteps(model.ModelId);
            var connectors = WFDA.Instance.GetConnectors(model.ModelId);

            var nodes = steps.Select(a =>
            {
                var node = new Node();
                node.LoadFromStep(a);
                return node;
            }).ToList();


            var lines = connectors.Select(a =>
            {
                var line = new Line();
                line.LoadFromConnector(a);
                return line;
            }).ToList();

            var root = new JObject(
                new JProperty("title", model.ModelName),
                new JProperty("areas", new JObject()),
                new JProperty("lines", new JObject(
                    lines.Select(a => new JProperty(a.id, new JObject(
                        new JProperty("name", a.name),
                        new JProperty("type", a.type),
                        new JProperty("M", a.M),
                        new JProperty("from", a.from),
                        new JProperty("to", a.to),
                        new JProperty("marked", a.marked),
                        new JProperty("strategy", a.strategy),
                        new JProperty("scripttype", a.scripttype),
                        new JProperty("ModelId", a.ModelId)
                        )))
                    )),
                new JProperty("nodes", new JObject(
                    nodes.Select(a => new JProperty(a.id, new JObject(
                        new JProperty("name", a.name),
                        new JProperty("type", a.type),
                        new JProperty("top", a.top),
                        new JProperty("left", a.left),
                        new JProperty("width", a.width),
                        new JProperty("height", a.height),
                        new JProperty("alt", a.alt),
                        new JProperty("strategy", a.strategy),
                        new JProperty("scripttype", a.scripttype),
                        new JProperty("ModelId", a.ModelId)
                        )))
                    )),
                new JProperty("initNum", nodes.Count + lines.Count)
                );
            return root.ToString();
        }

        public void SaveToDB(string data, string currentUser)
        {
            var root = JsonSerializeHelper.DeserializeObject<Dictionary<string, object>>(data);
            var title = _T(root["title"]);
            var jo_lines = root["lines"] as JObject;
            // var areas = root["areas"] as JObject;
            var jo_nodes = root["nodes"] as JObject;
            var initNum = ParseHelper.ParseInt(_T(root["initNum"])).GetValueOrDefault();

            var lines = jo_lines.Children().Cast<JProperty>().Select(a =>
            {
                var line = new Line();
                line.id = a.Name;
                line.name = GetProperty<string>(a.Value, "name");
                line.type = GetProperty<string>(a.Value, "type");
                line.M = GetProperty<decimal?>(a.Value, "M");
                line.from = GetProperty<string>(a.Value, "from");
                line.to = GetProperty<string>(a.Value, "to");
                line.marked = GetProperty<bool?>(a.Value, "marked");
                line.strategy = GetProperty<string>(a.Value, "strategy");
                line.scripttype = GetProperty<string>(a.Value, "scripttype");
                return line;
            }).ToList();


            var nodes = jo_nodes.Children().Cast<JProperty>().Select(a =>
            {
                var node = new Node();
                node.id = a.Name;
                node.name = GetProperty<string>(a.Value, "name");
                node.type = GetProperty<string>(a.Value, "type");
                node.left = GetProperty<int?>(a.Value, "left");
                node.top = GetProperty<int?>(a.Value, "top");
                node.width = GetProperty<int?>(a.Value, "width");
                node.height = GetProperty<int?>(a.Value, "height");
                node.alt = GetProperty<bool?>(a.Value, "alt");
                node.strategy = GetProperty<string>(a.Value, "strategy");
                node.scripttype = GetProperty<string>(a.Value, "scripttype");
                return node;
            }).ToList();

            var steps = nodes.Select(a =>
            {
                var step = a.ToStep();
                return step;
            });
            var connectors = lines.Select(a =>
            {
                var connector = a.ToConnector();
                return connector;
            });

            var ModelName = title;
            if (string.IsNullOrWhiteSpace(ModelName))
            {
                throw new WFException("模型名称不能为空");
            }
            var model = SaveModel(currentUser, ModelName);
            SaveSteps(currentUser, steps, model);
            SaveConnectors(currentUser, connectors, model);

            // 删除没用的数据
            var list = WFDA.Instance.GetSteps(model.ModelId)
                .Where(a => !steps.Any(b => b.StepId == a.StepId)).Select(a => a.StepId).ToList();
            WFDA.Instance.DeleteStep(list);

            list = WFDA.Instance.GetConnectors(model.ModelId)
                .Where(a => !connectors.Any(b => b.ConnectorId == a.ConnectorId)).Select(a => a.ConnectorId).ToList();
            WFDA.Instance.DeleteConnector(list);

        }
        private static void SaveConnectors(string currentUser, IEnumerable<WF_M_CONNECTOR> list, WF_M_MODEL model)
        {
            var i = 0;
            foreach (var item in list)
            {
                item.ModelId = model.ModelId;
                item.LastModifyTime = DateTime.Now;
                item.LastModifyUser = currentUser;
                i++;

                var oldEntity = WFDA.Instance.GetConnector(item.ConnectorId);
                if (oldEntity == null)
                {
                    item.CreateTime = DateTime.Now;
                    item.CreateUser = currentUser;
                    item.FromCellName = "Connections.Top.X";
                    item.ToCellName = "Connections.Left.X";
                    // 其他值已经在实体中转换过了
                    WFDA.Instance.Insert(item);
                }
                else
                {
                    oldEntity.ConnectorName = item.ConnectorName;
                    oldEntity.FromStepId = item.FromStepId;
                    oldEntity.ToStepId = item.ToStepId;
                    oldEntity.Script = item.Script;
                    oldEntity.ScriptType = item.ScriptType;
                    oldEntity.Extend01 = item.Extend01;
                    // 其他值保留原来的
                    WFDA.Instance.Update(oldEntity);
                }
            }
        }

        private static void SaveSteps(string currentUser, IEnumerable<WF_M_STEP> list, WF_M_MODEL model)
        {
            var i = 0;
            foreach (var item in list)
            {
                item.ModelId = model.ModelId;
                item.LastModifyTime = DateTime.Now;
                item.LastModifyUser = currentUser;
                i++;

                var oldEntity = WFDA.Instance.GetStep(item.StepId);
                if (oldEntity == null)
                {
                    item.CreateTime = DateTime.Now;
                    item.CreateUser = currentUser;
                    item.PinX = 5;
                    item.PinY = 5;
                    item.StepOrder = i;
                    item.IsSendMessage = 0;
                    item.AllowActions = string.Empty;
                    // 其他值已经在实体中转换过了
                    WFDA.Instance.Insert(item);
                }
                else
                {
                    oldEntity.StepName = item.StepName;
                    oldEntity.StepType = item.StepType;
                    oldEntity.Script = item.Script;
                    oldEntity.ScriptType = item.ScriptType;
                    oldEntity.Extend01 = item.Extend01;
                    // 其他值保留原来的
                    WFDA.Instance.Update(oldEntity);
                }
            }
        }

        private static WF_M_MODEL SaveModel(string currentUser, string ModelName)
        {
            var model = WFDA.Instance.GetModelByName(ModelName);
            if (model == null)
            {
                // 模型是新增的
                model = new WF_M_MODEL();
                model.ModelId = Guid.NewGuid().ToString();
                model.ModelName = ModelName;
                model.CreateTime = DateTime.Now;
                model.LastModifyTime = DateTime.Now;
                model.CreateUser = currentUser;
                model.LastModifyUser = currentUser;
                WFDA.Instance.Insert(model);
            }
            else
            {
                model.LastModifyTime = DateTime.Now;
                model.LastModifyUser = currentUser;
                WFDA.Instance.Update(model);
            }
            return model;
        }

        #region 连线实体
        private class Line
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public decimal? M { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public bool? marked { get; set; }
            public string strategy { get; set; }
            public string ModelId { get; set; }


            public WF_M_CONNECTOR ToConnector()
            {
                var entity = new WF_M_CONNECTOR();
                if (string.IsNullOrWhiteSpace(this.id))
                    entity.ConnectorId = Guid.NewGuid().ToString();
                else
                    entity.ConnectorId = this.id;

                entity.ConnectorName = this.name;
                entity.FromStepId = this.from;
                entity.ToStepId = this.to;
                entity.Script = Pub.GetHtmlSql(this.strategy);
                entity.ScriptType = 0;      // 0 表示 sql 语句
                entity.ModelId = ModelId;

                // 其他属性
                var dict = new Dictionary<string, string>();
                dict.Add("M", string.Format("{0}", this.M));
                dict.Add("type", string.Format("{0}", this.type));
                dict.Add("marked", string.Format("{0}", this.marked));
                entity.Extend01 = JsonSerializeHelper.SerializeObject(dict);

                return entity;
            }

            /// <summary>
            /// 将数据库实体转换成界面上的实体
            /// </summary>
            /// <param name="entity"></param>
            public void LoadFromConnector(WF_M_CONNECTOR entity)
            {
                this.id = entity.ConnectorId;
                this.name = entity.ConnectorName;
                this.from = entity.FromStepId;
                this.to = entity.ToStepId;
                this.strategy = Pub.GetOriginalSql(entity.Script);
                this.scripttype = entity.ScriptType.GetValueOrDefault().ToString();
                this.ModelId = entity.ModelId;

                // 默认值
                this.marked = false;
                this.M = 200;
                this.type = "lr";
                var dict = JsonSerializeHelper.DeserializeObject<Dictionary<string, string>>(entity.Extend01);
                if (dict != null)
                {
                    if (dict.ContainsKey("M"))
                        this.M = ParseHelper.ParseDecimal(dict["M"]);
                    if (dict.ContainsKey("marked"))
                        this.marked = ParseHelper.ParseBool(dict["marked"]);
                    if (dict.ContainsKey("type"))
                        this.type = dict["type"];
                }
            }

            public string scripttype { get; set; }
        }

        #endregion

        #region 节点实体
        private class Node
        {
            public string id { get; set; }
            public string name { get; set; }
            public int? left { get; set; }
            public int? top { get; set; }
            public string type { get; set; }
            public int? width { get; set; }
            public int? height { get; set; }
            public bool? alt { get; set; }
            public string strategy { get; set; }
            public string ModelId { get; set; }

            /// <summary>
            /// 将数据库实体转换成界面上的实体
            /// </summary>
            /// <param name="entity"></param>
            public void LoadFromStep(WF_M_STEP entity)
            {
                this.id = entity.StepId;
                this.name = entity.StepName;
                this.ModelId = entity.ModelId;

                this.alt = true;
                this.left = 0;
                this.top = 0;

                if (entity.StepType == "Start")
                {
                    this.type = "start";
                    this.width = 24;
                    this.height = 24;
                }
                else if (entity.StepType == "Stop")
                {
                    this.type = "end";
                    this.width = 24;
                    this.height = 24;
                }
                else
                {
                    this.type = "task";
                    this.width = 135;
                    this.height = 50;
                }
                this.strategy = Pub.GetOriginalSql(entity.Script);
                this.scripttype = entity.ScriptType.GetValueOrDefault().ToString();

                // 默认值
                var dict = JsonSerializeHelper.DeserializeObject<Dictionary<string, string>>(entity.Extend01);
                if (dict != null)
                {
                    if (dict.ContainsKey("left"))
                        this.left = ParseHelper.ParseInt(dict["left"]);
                    if (dict.ContainsKey("top"))
                        this.top = ParseHelper.ParseInt(dict["top"]);
                    if (dict.ContainsKey("width"))
                        this.width = ParseHelper.ParseInt(dict["width"]);
                    if (dict.ContainsKey("height"))
                        this.height = ParseHelper.ParseInt(dict["height"]);
                    if (dict.ContainsKey("alt"))
                        this.alt = ParseHelper.ParseBool(dict["alt"]);
                }
            }

            internal WF_M_STEP ToStep()
            {
                var entity = new WF_M_STEP();
                if (string.IsNullOrWhiteSpace(this.id))
                    entity.StepId = Guid.NewGuid().ToString();
                else
                    entity.StepId = this.id;

                entity.StepName = this.name;
                entity.Script = Pub.GetHtmlSql(this.strategy);
                entity.ScriptType = ParseHelper.ParseInt(this.scripttype).GetValueOrDefault();
                entity.ModelId = ModelId;
                if (this.type == "start")
                {
                    entity.StepType = "Start";
                }
                else if (this.type == "end")
                {
                    entity.StepType = "Stop";
                }
                else
                {
                    entity.StepType = "One Actor";
                }

                // 其他属性
                var dict = new Dictionary<string, string>();
                dict.Add("top", string.Format("{0}", this.top));
                dict.Add("left", string.Format("{0}", this.left));
                dict.Add("width", string.Format("{0}", this.width));
                dict.Add("height", string.Format("{0}", this.height));
                dict.Add("alt", string.Format("{0}", this.alt));
                entity.Extend01 = JsonSerializeHelper.SerializeObject(dict);

                return entity;
            }

            public string scripttype { get; set; }
        }
        #endregion

        T GetProperty<T>(JToken jt, string name)
        {
            var jo = jt as JObject;
            if (jo == null)
                return default(T);
            var jp = jo.Properties().FirstOrDefault(a => a.Name == name);
            if (jp == null)
                return default(T);
            return jp.Value.ToObject<T>();
        }

        string _T(object obj)
        {
            return string.Format("{0}", obj);
        }
    }
}
