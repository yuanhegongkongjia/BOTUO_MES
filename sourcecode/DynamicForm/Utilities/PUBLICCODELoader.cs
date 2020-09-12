using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFCommon;
using WFCommon.Utility;
using WFCore;

namespace DynamicForm
{
    public class PUBLICCODELoader
    {
        public static readonly PUBLICCODELoader Instance = new PUBLICCODELoader();

        Dictionary<string, List<WF_M_PUBLICCODE>> dict = null;
        public PUBLICCODELoader()
        {
            dict = new Dictionary<string, List<WF_M_PUBLICCODE>>();
            Init(SystemConstants.English);
            Init(SystemConstants.ChineseSimplified);
            Init(SystemConstants.ChineseTraditional);
        }

        void Init(string language)
        {
            var list = new List<WF_M_PUBLICCODE>();
            list.Add(new WF_M_PUBLICCODE() { CodeType = "是否生效", CodeName = "".GetRes(language), CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "是否生效", CodeName = "不生效".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "是否生效", CodeName = "生效".GetRes(language), CodeValue = "1" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsAllowNull", CodeName = "".GetRes(language), CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsAllowNull", CodeName = "否".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsAllowNull", CodeName = "是".GetRes(language), CodeValue = "1" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsActive", CodeName = "".GetRes(language), CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsActive", CodeName = "停用".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsActive", CodeName = "启用".GetRes(language), CodeValue = "1" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "FieldType", CodeName = "".GetRes(language), CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "FieldType", CodeName = "文本".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "FieldType", CodeName = "数字".GetRes(language), CodeValue = "1" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "FieldType", CodeName = "日期".GetRes(language), CodeValue = "2" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "FieldType", CodeName = "公用代码".GetRes(language), CodeValue = "3" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "FieldType", CodeName = "附件".GetRes(language), CodeValue = "4" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "SOSTATUS", CodeName = "未做任何处理".GetRes(language), CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "SOSTATUS", CodeName = "已拆解成工单".GetRes(language), CodeValue = "1" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "SOSTATUS", CodeName = "已合并成工单".GetRes(language), CodeValue = "2" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "SOSTATUS", CodeName = "已直接转成工单".GetRes(language), CodeValue = "3" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "YESNO", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "YESNO", CodeName = "否".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "YESNO", CodeName = "是".GetRes(language), CodeValue = "1" });


            list.Add(new WF_M_PUBLICCODE() { CodeType = "STATUS", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "STATUS", CodeName = "停用".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "STATUS", CodeName = "启用".GetRes(language), CodeValue = "1" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsSendMessage", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsSendMessage", CodeName = "否".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "IsSendMessage", CodeName = "是".GetRes(language), CodeValue = "1" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "DeptRelated", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "DeptRelated", CodeName = "部门不相关".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "DeptRelated", CodeName = "部门相关".GetRes(language), CodeValue = "1" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "ExecutorType", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ExecutorType", CodeName = "用户".GetRes(language), CodeValue = "User" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ExecutorType", CodeName = "角色".GetRes(language), CodeValue = "Role" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ExecutorType", CodeName = "自定义".GetRes(language), CodeValue = "Custom" });

            // { '1': '保存','2':'提交','3':'同意','4':'不同意','5':'回退','6':'转签','7':'加签' }
            list.Add(new WF_M_PUBLICCODE() { CodeType = "AllowActions", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "AllowActions", CodeName = "保存".GetRes(language), CodeValue = "1" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "AllowActions", CodeName = "提交".GetRes(language), CodeValue = "2" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "AllowActions", CodeName = "同意".GetRes(language), CodeValue = "3" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "AllowActions", CodeName = "不同意".GetRes(language), CodeValue = "4" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "AllowActions", CodeName = "回退".GetRes(language), CodeValue = "5" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "AllowActions", CodeName = "转签".GetRes(language), CodeValue = "6" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "AllowActions", CodeName = "加签".GetRes(language), CodeValue = "7" });

            // var customTypes = {'0': getRes('SQL语句'), '1': getRes('自定义代码'), '2': getRes('自定义插件')};
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ScriptType", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ScriptType", CodeName = "SQL语句".GetRes(language), CodeValue = "0" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ScriptType", CodeName = "自定义代码".GetRes(language), CodeValue = "1" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ScriptType", CodeName = "自定义插件".GetRes(language), CodeValue = "2" });

            // {'Finished':'已完成','Unfinished':'未完成'}
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ExecuteStatus", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ExecuteStatus", CodeName = "已完成".GetRes(language), CodeValue = "Finished" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "ExecuteStatus", CodeName = "未完成".GetRes(language), CodeValue = "Unfinished" });

            // {'Finished':'已完成','Running':'进行中','Aborted':'已取消'}
            list.Add(new WF_M_PUBLICCODE() { CodeType = "InstanceStatus", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "InstanceStatus", CodeName = "已完成".GetRes(language), CodeValue = "Finished" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "InstanceStatus", CodeName = "进行中".GetRes(language), CodeValue = "Running" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "InstanceStatus", CodeName = "已取消".GetRes(language), CodeValue = "Aborted" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "InstanceStepStatus", CodeName = "", CodeValue = "" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "InstanceStepStatus", CodeName = "已完成".GetRes(language), CodeValue = "Left" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "InstanceStepStatus", CodeName = "进行中".GetRes(language), CodeValue = "Entered" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "InstanceStepStatus", CodeName = "未开始".GetRes(language), CodeValue = "NotEnter" });

            list.Add(new WF_M_PUBLICCODE() { CodeType = "SYSTEM_COMPANY", CodeName = "信达科技".GetRes(language), CodeValue = "XDKJ" });
            list.Add(new WF_M_PUBLICCODE() { CodeType = "SYSTEM_COMPANY", CodeName = "信达生物".GetRes(language), CodeValue = "XDSW" });
            LoadPublicCodeFromDB(list, language);
            dict.Add(language, list);
        }


        private void LoadPublicCodeFromDB(List<WF_M_PUBLICCODE> list, string language)
        {
            var codeList = WFCorePublicCodeHelper.QueryPublicCodeDefault();
            foreach (var c in codeList)
            {
                var item = list.FirstOrDefault(a => a.CodeType == c.CodeType && a.CodeName == c.CodeName);
                if (item != null)
                {
                    list.Remove(item);
                }
                list.Add(new WF_M_PUBLICCODE() { CodeType = c.CodeType, CodeName = c.CodeName.GetRes(language), CodeValue = c.CodeValue });
            }

        }

        public static string Value2DisplayText(string codeType, string value)
        {
            value = string.Format("{0}", value).Trim();
            var list = new List<string>();
            foreach (var item in value.Split(','))
            {
                var entity = PUBLICCODELoader.Instance.dict[WFRes.Instance.GetLanguage()].FirstOrDefault(a => a.CodeType == codeType && a.CodeValue == item);
                if (entity == null)
                {
                    list.Add(item);
                }
                else
                {
                    list.Add(string.Format("{0}", entity.CodeName).Trim());
                }
            }
            return string.Join(",", list);
        }

        public static string DisplayText2Value(string codeType, string displayText)
        {
            displayText = string.Format("{0}", displayText).Trim();
            var list = new List<string>();
            foreach (var item in displayText.Split(','))
            {
                var entity = PUBLICCODELoader.Instance.dict[WFRes.Instance.GetLanguage()].FirstOrDefault(a => a.CodeType == codeType && a.CodeName == item);
                if (entity == null)
                {
                    list.Add(displayText);
                }
                else
                {
                    list.Add(string.Format("{0}", entity.CodeValue).Trim());
                }
            }
            return string.Join(",", list);
        }


    }
}