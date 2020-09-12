var nodeRows = [], lineRows = [], areaRows = [], focusItemID, focusItemType;
function onLoadSuccess(data) {
    var nodes = $.parseJSON(data.flowData).nodes;
    for (var i in nodes) {
        var rows = CreateAttributeData("node", i, nodes[i], data.node_custom_attributes, data.custom_attribute_values);
        nodeRows.push({ "nodeID": i, "rows": rows, "ID": nodes[i].ID || 0 });
    }
    //连接线属性
    var lines = $.parseJSON(data.flowData).lines;
    for (var i in lines) {
        var rows = CreateAttributeData("line", i, lines[i], data.line_custom_attributes, data.custom_attribute_values);
        lineRows.push({ "lineID": i, "rows": rows, "ID": lines[i].ID || 0 });
    }
    var areas = $.parseJSON(data.flowData).areas;
    for (var i in areas) {
        var rows = CreateAttributeData("area", i, areas[i]);
        areaRows.push({ "areaID": i, "rows": rows, "ID": areas[i].ID || 0 });
    }
    //加载流程属性信息
    $.ajaxGet({
        url: "/Flow/GetFlow",
        data: { "id": flowID },
        success: function (data) {
            if (data.status == 1) {
                var model = eval(data.model)[0];
                //显示流程属性
                var rows = [
                           { "name": "流程名称", "value": "" + model.FlowName + "", "group": "流程属性" },
                           { "name": "表单地址", "value": "" + model.FormUrl + "", "group": "流程属性" },
                           { "name": "是否禁用", "value": model.IsDisabled, "group": "流程属性" },
                           { "name": "创建时间", "value": timeFormatter(model.CreateOn), "group": "流程属性" },
                           { "name": "创建人", "value": model.CreateBy, "group": "流程属性" },
                           { "name": "修改时间", "value": timeFormatter(model.ModifiedOn), "group": "流程属性" },
                           { "name": "修改人", "value": model.ModifiedBy, "group": "流程属性" }
                ];
                $("#tt_grid").propertygrid("loadData", { 'total': rows.length, "rows": rows });
            }
            else
                jqAlert('数据加载失败:' + data.error, 'error')
        }
    });
    hideLoading(gooFlow.$bgDiv);
}
function onLoadError(status, errorThrown) {
    $('.datagrid-mask-msg').remove();
    $('.datagrid-mask').remove();
    jqAlert('图形加载失败:' + status, 'error')
}
function CreateAttributeData(type, id, jsonData, custom_jsonData, custom_jsonValue) {
    var rows;
    if (type == "node") {
        rows = [{ "field": "NodeID", "name": "节点ID", "value": "" + id + "", "group": "节点属性" },
                 { "field": "NodeName", "name": "节点名称", "value": "" + jsonData.name + "", "group": "节点属性" },
                 { "field": "NodeType", "name": "节点类型", "value": "" + jsonData.type + "", "group": "节点属性" },
                 { "field": "NodeLeft", "name": "X坐标", "value": jsonData.left, "group": "节点属性" },
                 { "field": "NodeTop", "name": "Y坐标", "value": jsonData.top, "group": "节点属性" },
                 { "field": "NodeWidth", "name": "宽度", "value": jsonData.width, "group": "节点属性" },
                 {
                     "field": "StepType", "name": "步骤类型", "value": jsonData.stepType || "", "group": "步骤设置",
                     editor: {
                         'type': 'combobox',
                         'options': {
                             valueField: 'text',
                             textField: 'text',
                             editable: false,
                             panelHeight: 'auto',
                             hasDownArrow: false,
                             buttonIcon: 'icon-clear',
                             url: '/Flow/GetFlowComboboxData?enumName=StepType',
                             onClickButton: function () { $(this).textbox('clear'); }
                         }
                     }
                 },
                 {
                     "field": "DecisionMode", "name": "决策方式", "value": jsonData.decisionMode || "", "group": "步骤设置",
                     "hiddenField": { "name": "DecisionValue", "value": jsonData.decisionValue || "" },
                     editor: {
                         'type': 'textbox',
                         'options': {
                             prompt: '点我设置决策方案哦.',
                             buttonIcon: 'icon-clear',
                             onClickButton: function () { $(this).textbox('clear'); }
                         }
                     }
                 },
                 { "field": "AuditorType", "name": "参与者类型", "value": jsonData.auditorType || "", "group": "待办参与者" },
                 {
                     "field": "AuditorText", "name": "设置参与者", "value": jsonData.auditorText || "", "group": "待办参与者",
                     "hiddenField": { "name": "AuditorValue", "value": jsonData.auditorValue || "" },
                     editor: {
                         'type': 'textbox',
                         'options': {
                             prompt: '点我选择参与者哦.',
                             buttonIcon: 'icon-clear',
                             onClickButton: function () {
                                 for (var i = 0; i < nodeRows.length; i++) {
                                     if (nodeRows[i].field == "AuditorText") {
                                         nodeRows[i].hiddenField.value = "";
                                         break;
                                     }
                                 }
                                 $(this).textbox('clear');
                             }
                         }
                     }
                 }];
        var editor;
        for (var i in custom_jsonData) {
            if (custom_jsonData[i].Editor) {
                if (custom_jsonData[i].Editor == "checkbox") {
                    editor = { 'type': 'checkbox', 'options': { 'on': '是', 'off': '否' } };
                }
                else if (custom_jsonData[i].Editor == "combobox") {
                    editor = {
                        'type': 'combobox',
                        'options': {
                            valueField: 'text',
                            textField: 'text',
                            editable: false,
                            panelHeight: 'auto',
                            hasDownArrow: false,
                            buttonIcon: 'icon-clear',
                            url: encodeURI(custom_jsonData[i].DataSource || ""),
                            onClickButton: function () {
                                $(this).textbox('clear');
                            }
                        }
                    };
                }
                else if (custom_jsonData[i].Editor == "textbox" && custom_jsonData[i].AttributeName == "PassDoEventMode") {
                    editor = {
                        'type': 'textbox',
                        'options': {
                            prompt: '点我设置执行方案哦.',
                            buttonIcon: 'icon-clear',
                            onClickButton: function () { $(this).textbox('clear'); }
                        }
                    }
                }
                else
                    editor = custom_jsonData[i].Editor;
            }
            else {
                editor = null;
            }
            var custom_value = {};
            $(custom_jsonValue).each(function (j, o) {
                if (o.ElementID == id && o.AttrID == custom_jsonData[i].ID) {
                    custom_value = o;
                    return false;
                }
            });
            rows.push({
                "field": "" + custom_jsonData[i].AttributeName + "",
                "name": "" + custom_jsonData[i].AttributeTitle + "",
                "value": custom_value.AttrValue,
                "group": "" + custom_jsonData[i].GroupName + "",
                "attrID": "" + custom_jsonData[i].ID + "",
                "editor": editor,
                "id": custom_value.ID || 0,
                "hdValue": custom_value.HiddenValue || ""
            });
        }
    }
    else if (type == "line") {
        rows = [{ "field": "LineID", "name": "连接线ID", "value": "" + id + "", "group": "线条属性" },
                { "field": "LineName", "name": "连接线名称", "value": "" + jsonData.name + "", "group": "线条属性" },
                { "field": "LineType", "name": "连接线类型", "value": "" + jsonData.type + "", "group": "线条属性" },
                { "field": "LineFrom", "name": "开始节点ID", "value": jsonData.from, "group": "线条属性" },
                { "field": "LineTo", "name": "结束节点ID", "value": jsonData.to, "group": "线条属性" },
                { "field": "LineM", "name": "M值", "value": jsonData.M || 0, "group": "线条属性" },
                {
                    "field": "ConditionName", "name": "流转条件", "value": jsonData.conditionName || "", "group": "条件设置",
                    "hiddenField": { "name": "ConditionID", "value": jsonData.conditionID || "" },
                    editor: {
                        'type': 'textbox',
                        'options': {
                            prompt: '点我选择条件哦.',
                            buttonIcon: 'icon-clear',
                            onClickButton: function () {
                                $(this).textbox('clear');
                            }
                        }
                    }
                }];
        //#region 连接线自定义属性加载
        //var editor;
        //for (var i in custom_jsonData) {
        //    if (custom_jsonData[i].Editor) {
        //        if (custom_jsonData[i].Editor == "checkbox") {
        //            editor = { 'type': 'checkbox', 'options': { 'on': '是', 'off': '否' } };
        //        }
        //        else if (custom_jsonData[i].Editor == "textbox") {
        //            editor = {
        //                'type': 'textbox',
        //                'options': {
        //                    prompt: '点我选择条件哦.',
        //                    buttonIcon: 'icon-clear',
        //                    url: custom_jsonData[i].DefaultValue,
        //                    onClickButton: function () {
        //                        $(this).textbox('clear');
        //                    }
        //                }
        //            };
        //        }
        //        else
        //            editor = custom_jsonData[i].Editor;
        //    }
        //    else
        //        editor = null;
        //    var custom_value = {};
        //    $(custom_jsonValue).each(function (j, o) {
        //        if (o.ElementID == id && o.AttrID == custom_jsonData[i].ID) {
        //            custom_value = o;
        //            return false;
        //        }
        //    });
        //    rows.push({
        //        "field": "" + custom_jsonData[i].AttributeName + "",
        //        "name": "" + custom_jsonData[i].AttributeTitle + "",
        //        "value": "" + (custom_value.AttrValue || ""),
        //        "group": "" + custom_jsonData[i].GroupName + "",
        //        "attrID": "" + custom_jsonData[i].ID + "",
        //        "editor": editor,
        //        "id": custom_value.ID || 0,
        //        "hdValue": "" + (custom_value.HiddenValue || "") + ""
        //    });
        //}
        //#endregion
    }
    else if (type == "area") {
        rows = [
              { "field": "AreaID", "name": "分组区ID", "value": "" + id + "", "group": "分组区域属性" },
              { "field": "AreaName", "name": "分组区名称", "value": "" + jsonData.name + "", "group": "分组区域属性" },
              { "field": "AreaLeft", "name": "X坐标", "value": jsonData.left, "group": "分组区域属性" },
              { "field": "AreaTop", "name": "Y坐标", "value": jsonData.top, "group": "分组区域属性" },
              { "field": "AreaWidth", "name": "宽度", "value": jsonData.width, "group": "分组区域属性" },
              { "field": "AreaHeight", "name": "高度", "value": jsonData.height, "group": "分组区域属性" },
              { "field": "AreaColor", "name": "颜色", "value": jsonData.color, "group": "分组区域属性" }
        ];
    }
    return rows;
}
//获取自定义属性
function GetCustomeAttributes(flowID, fn) {
    $.ajaxGet({
        url: "/Flow/GetFlowAttributes",
        data: { "flowID": flowID },
        async: false,
        cache: true,
        success: function (data) {
            fn(data);
        }
    });
}
//流程元素的操作公共方法
function FlowElementAction(id, type, action, changes) {
    if (type == "node") {
        $(nodeRows).each(function (i, item) {
            if (item.nodeID == id) {
                if (action == "modify") {
                    $(item.rows).each(function (i, row) {
                        for (var j in changes) {
                            if (row.field == j) {
                                row.value = changes[j];
                            }
                            $("#tt_grid").datagrid("refreshRow", i);
                        }
                    });
                }
                else if (action == "delete") {
                    nodeRows.splice(i, 1);
                }
                else if (action == "focus") {
                    $("#tt_grid").propertygrid("loadData", { "rows": nodeRows[i].rows });
                }
                return;
            }
        });
    }
    else if (type == "line") {
        $(lineRows).each(function (i, item) {
            if (item.lineID == id) {
                if (action == "modify") {
                    $(item.rows).each(function (i, row) {
                        for (var j in changes) {
                            if (row.field == j) {
                                row.value = changes[j];
                            }
                            $("#tt_grid").datagrid("refreshRow", i);
                        }
                    });
                }
                else if (action == "delete") {
                    lineRows.splice(i, 1);
                }
                else if (action == "focus") {
                    $("#tt_grid").propertygrid("loadData", { "rows": lineRows[i].rows });
                }
                return;
            }
        });
    }
    else if (type == "area") {
        $(areaRows).each(function (i, item) {
            if (item.areaID == id) {
                if (action == "modify") {
                    $(item.rows).each(function (i, row) {
                        for (var j in changes) {
                            if (row.field == j) {
                                row.value = changes[j];
                            }
                        }
                    });
                    $("#tt_grid").propertygrid("loadData", { "rows": areaRows[i].rows });
                }
                else if (action == "delete") {
                    areaRows.splice(i, 1);
                }
                return;
            }
        });
    }
}

/*
$.extend($.fn.datagrid.defaults.editors, {
    checkbox: {
        init: function (container, options) {
            var input = $('<input type="checkbox" class="checkbox">').appendTo(container);
            input.val(options.on);
            input.attr("offval", options.off);
            return input;
        },
        destroy: function (target) {
            $(target).remove();
        },
        getValue: function (target) {
            if ($(target).is(":checked")) {
                return $(target).val();
            } else {
                return $(target).attr("offval");
            }
        },
        setValue: function (target, value) {
            return $(target).val() == value ? true : false;
        },
        resize: function (target, width) {
            $(target)._outerWidth(width);
        }
    }
});
*/