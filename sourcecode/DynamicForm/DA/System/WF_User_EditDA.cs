using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFCommon.VM;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class WF_User_EditDA : BaseDA
    {
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (entity["EditMode"] != "Edit")
            {
                return Insert(form, entity, ref  message);
            }
            var currentUser = Util.GetCurrentUser();
            var dict = new DFDictionary();
            /*基本查询语句*/
            var sql = "select * from WF_M_USER where 1=1";

            /*查询条件*/
            sql += " and UserId=@UserId";


            if (entity["Category"] == "SENDER")
            {
                if (string.IsNullOrWhiteSpace(entity["CompanyName"]))
                {
                    throw new WFException("请输入托运单位");
                }
            }

            using (var db = Pub.DB)
            {
                var parameters = new { UserId = entity["UserId"] };
                var oldEntity = db.Query<WF_M_USER>(sql, parameters).FirstOrDefault();
                if (oldEntity == null)
                {
                    throw new WFException("记录已经不存在".GetRes());
                }

                var newEntity = DFDictionary.Create<WF_M_USER>(oldEntity).Merge(entity).To<WF_M_USER>();
                newEntity.Status = ParseHelper.ParseInt(entity["Status"]);
                newEntity.LastModifyUser = currentUser.UserName;
                newEntity.LastModifyTime = DateTime.Now;
                db.Update(newEntity);
             
                message = "保存成功".GetRes();
                return DFPub.EXECUTE_SUCCESS;
            }
        }
        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            if (string.IsNullOrWhiteSpace(entity["UserName"]))
            {
                throw new WFException("用户名必须输入".GetRes());
            }
            var currentUser = Util.GetCurrentUser();
            var dict = new DFDictionary();
            /*基本查询语句*/
            var sql = "select * from WF_M_USER where 1=1";

            /*查询条件*/
            sql += " and UserName=@UserName";

            using (var db = Pub.DB)
            {
                var parameters = new { UserName = entity["UserName"] };
                var oldEntity = db.Query<WF_M_USER>(sql, parameters).FirstOrDefault();
                if (oldEntity != null)
                {
                    throw new WFException("用户名已经存在".GetRes());
                }


                if (entity["Category"] == "SENDER")
                {
                    if (string.IsNullOrWhiteSpace(entity["CompanyName"]))
                    {
                        throw new WFException("请输入托运单位");
                    }
                }

                var newEntity = entity.To<WF_M_USER>();
                newEntity.UserId = Guid.NewGuid().ToString();
                newEntity.Password = HashHelper.GenerateUserHash(newEntity.UserName, "123456");
                newEntity.Status = ParseHelper.ParseInt(entity["Status"]);
                newEntity.CreateUser = currentUser.UserName;
                newEntity.CreateTime = DateTime.Now;
                newEntity.LastModifyUser = currentUser.UserName;
                newEntity.LastModifyTime = DateTime.Now;
                //var company = WF_M_COMPANYLoader.Get(newEntity.CompanyCode);
                //newEntity.CompanyName = company.CompanyName;
                db.Insert(newEntity);
                
                    // 默认为 role_user 角色组用户
                    UserRoleLoader.Create(newEntity.UserId, Constants.ROLE_USER, currentUser.UserName);

                message = "新增成功".GetRes();
                return DFPub.EXECUTE_SUCCESS;

            }
        }
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            /*基本查询语句*/
            var sql = @"select a.* from WF_M_USER a where 1=1";

            /*查询条件*/
            sql += " and a.UserId=@UserId";
            if (!string.IsNullOrWhiteSpace(entity["UserId"]))
            {
                using (var db = Pub.DB)
                {
                    var parameters = new { UserId = entity["UserId"] };
                    var item = db.Query<VM_WF_M_USER>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<VM_WF_M_USER>(item);
                    }
                }
            }
            return dict;
        }
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            // 编辑的时候用户名是不能修改的
            if (entity["EditMode"] == "Edit")
            {
                var c = form.GetControlM("UserName");
                if (null != c)
                {
                    c.Readonly = true;
                }
            }
            var list = new List<DFSelectItem>();
            SetSelectDataSource(form, "CompanyName", list, false);

            this.Model.Add("CompanyName", "");
            if (!string.IsNullOrWhiteSpace(entity["Category"]) )
            {

                using (var db = Pub.DB)
                {
                    var sql = "";
                    if (entity["Category"] == "SENDER")
                    {
                        sql = "select distinct TuoYun as value,TuoYun as text from YZ_M_TUOYUN where IsActive='Y'";
                        list = db.Query<DFSelectItem>(sql, new { }).ToList();
                    }
                    //else if(entity["Category"] == "RECEIVER")
                    //{
                    //    sql = "select distinct ReceiverCompany as value,ReceiverCompany as text from YZ_T_RECEIVER where IsActive='Y'";
                    //    list = db.Query<DFSelectItem>(sql, new { }).ToList();
                    //}
                    else if (entity["Category"] == "FORWARDER")
                    {
                        sql = "select distinct Forwarder as value,Forwarder as text from YZ_M_FORWARDER where IsActive='Y'";
                        list = db.Query<DFSelectItem>(sql, new { }).ToList();
                    }

                    //list = db.Query<DFSelectItem>(sql, new {  }).ToList();
                }
                SetSelectDataSource(form, "CompanyName", list, false);
            }
            base.SetAccess(form, entity);
        }
    }
}
