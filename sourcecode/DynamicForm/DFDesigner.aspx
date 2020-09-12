<%@ Page Title="" Language="C#" MasterPageFile="DFSite.Master" AutoEventWireup="true" CodeBehind="DFDesigner.aspx.cs" Inherits="DynamicForm.DFDesigner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--[if lt IE 9]>
<?import namespace="v" implementation="#default#VML" ?>
<![endif]-->
    <link href="gooflow0.8/default.css" rel="stylesheet" />
    <link href="gooflow0.8/GooFlow/codebase/GooFlow.css" rel="stylesheet" />
    <script src="gooflow0.8/jquery.min.js"></script>
    <script src="gooflow0.8/json2.js"></script>
    <script src="gooflow0.8/GooFunc.js"></script>
    <script src="gooflow0.8/GooFlow/codebase/GooFlow.js"></script>
    <script src="gooflow0.8/GooFlow/codebase/GooFlow_color.js"></script>
    <script src="gooflow0.8/GooFlow/codebase/GooFlow_public.js"></script>

    <script type="text/javascript">
        var demo;
        var loadData = function (demo, title) {
            var jqxhr = $.post('DFDesigner.aspx?action=load', {
                title: title
            })
            .done(function (data) {
                var ret = data;
                if (typeof (data) == 'string') {
                    ret = BUI.JSON.parse(data);
                }
                if (ret) {
                    dfLog(ret);
                    demo.loadData(ret);
                }
            })
            .fail(function () {
                alert('网络错误或者远程处理发生错误');
            });
        }

        $(document).ready(function () {
            var property = {
                toolBtns: ["start", "end", "task"],
                headLabel: true,
                haveHead: true,
                headBtns: ["save", "reload"], //如果haveHead=true，则定义HEAD区的按钮
                haveTool: true,
                haveGroup: false,
                useOperStack: true,
                width: $(window).width() - 700,
                height: $(window).height()
            };
            var remark = {
                "cursor": "选择指针",
                "direct": "节点连线",
                "start": "开始节点",
                "end": "结束节点",
                "task": "普通节点",
                "chat": "决策结点",
                "group": "区域"
            }

            demo = $.createGooFlow($("#demo"), property);
            var title = decodeURI(getQueryStringByName('ModelName'));
            demo.setTitle(title);
            demo.setNodeRemarks(remark);
            loadData(demo, title);

            // 刷新重新载入
            demo.onFreshClick = function () {
                demo.clearData();
                $('#strategy').val('');
                $('#scripttype').val('');
                loadData(demo, title);
            };

            $('.GooFlow_item').dblclick(function () {
                alert('nihao');
            });

            // 保存数据到服务器
            demo.onBtnSaveClick = function () {
                var jqxhr = $.post('DFDesigner.aspx?action=save', {
                    title: title,
                    data: BUI.JSON.stringify({
                        title: demo.$title,
                        areas: demo.$areaData,
                        lines: demo.$lineData,
                        nodes: demo.$nodeData,
                        initNum: demo.$nodeCount + demo.$lineCount + demo.$areaCount
                    })
                })
                .done(function (data) {
                    var ret = data;
                    if (typeof (data) == 'string') {
                        ret = BUI.JSON.parse(data);
                    }
                    if (ret && ret.data) {
                        alert(ret.data);
                    }
                    else {
                        alert('保存失败');
                    }
                })
                .fail(function () {
                    alert('网络错误或者远程处理发生错误');
                });
            }

            var dialog;
            BUI.use('bui/overlay', function (Overlay) {
                dialog = new Overlay.Dialog({
                    elCls: 'custom-dialog',
                    width: 700,
                    height: $(window).height() + 40,
                    //配置DOM容器的编号
                    contentId: 'content',
                    mask: false,
                    closeAction: 'hide',
                    closeable: false,
                    align: {
                        //node : '#t1',//对齐的节点
                        points: ['cr', 'cr'], //对齐参考：http://dxq613.github.io/#positon
                        offset: [0, 0] //偏移
                    }
                });
                $('#strategy').width(667);
                dialog.show();
            });

            // 失去焦点的时候保存数据
            demo.onItemBlur = function (id, type) {
                var data = this.getItemInfo(id, type);
                data.strategy = $('#strategy').val();
                data.scripttype = $('#scripttype').val();
                $('#strategy').val('');
                $('#scripttype').val('');
                return true;
            }

            // 得到焦点的时候显示数据
            demo.onItemFocus = function (id, type) {
                dialog.show();
                var data = this.getItemInfo(id, type);
                $('#strategy').val(data.strategy);
                $('#scripttype').val(data.scripttype);
                return true;
            };
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="demo"></div>

    <!-- 此节点内部的内容会在弹出框内显示,默认隐藏此节点-->
    <div id="content">
        <div class="row" style="height: 280px; margin-bottom: 10px;">
            <div class="span12">
                <textarea id="strategy" style="height: 280px;" class="span11"></textarea>
            </div>
        </div>
        <div class="row">
            <select id="scripttype" name="scripttype">
                <option value=""></option>
                <option value="0">SQL</option>
                <option value="1">自定义代码</option>
                <option value="2">自定义插件</option>
            </select>
        </div>
        <div class="row">
            <div class="span4">
                <div id="__dfList1">
                </div>
            </div>
            <div class="span4">
                <div id="__dfList2">
                </div>
            </div>
            <div class="span4">
                <div id="__dfList3">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            BUI.use(['bui/list', 'bui/data'], function (List, Data) {
                var store = new Data.Store({
                    url: 'DFHandler.ashx?DF_FORMNAME=WF_PublicCode&action=querylist&CodeType=BusinessStrategy',
                    autoLoad: true
                });
                var list1 = new List.SimpleList({
                    elCls: 'bui-select-list',
                    render: '#__dfList1',
                    store: store,
                    height: 250,
                    width: 150
                });
                list1.render();

                list1.on('itemclick', function (ev) {
                    $('#strategy').val(ev.item.value.replace(/@ModelName/g, decodeURI(getQueryStringByName('ModelName'))));
                });
            });
            BUI.use(['bui/list', 'bui/data'], function (List, Data) {
                var store = new Data.Store({
                    url: 'DFHandler.ashx?DF_FORMNAME=WFCore_Step&action=querylist&ModelId={0}'.format(getQueryStringByName('ModelId')),
                    autoLoad: true
                });
                var list1 = new List.SimpleList({
                    elCls: 'bui-select-list',
                    render: '#__dfList2',
                    store: store,
                    height: 250,
                    width: 150
                });
                list1.render();

                list1.on('itemclick', function (ev) {
                    insertText(document.getElementById('strategy'), ev.item.value.replace(/@ModelName/g, decodeURI(getQueryStringByName('ModelName'))));

                });
            });


            BUI.use(['bui/list', 'bui/data'], function (List, Data) {
                var store = new Data.Store({
                    url: 'DFHandler.ashx?DF_FORMNAME=WFCore_Step&action=querylist&subAction=queryVariables&ModelId={0}'.format(getQueryStringByName('ModelId')),
                    autoLoad: true
                });
                var list1 = new List.SimpleList({
                    elCls: 'bui-select-list',
                    render: '#__dfList3',
                    store: store,
                    height: 250,
                    width: 350
                });
                list1.render();

                list1.on('itemclick', function (ev) {
                    insertText(document.getElementById('strategy'), ev.item.value.replace(/@ModelName/g, decodeURI(getQueryStringByName('ModelName'))));

                });
            });
        });
    </script>
</asp:Content>
