using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class Form_SM_T_PROCESSDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "SM_T_PROCESS"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            return new { InstanceId = entity["InstanceId"] };
        }

        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("delete from SM_T_PROCESS where InstanceId=@InstanceId", data.Select(a => new { InstanceId = a["InstanceId"] }).ToList());
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            
            //var oldEntity = Get(GetSelectSql(TableName), GetParam(entity));
            //if (oldEntity == null)
            //{
            //    throw new WFException("要更新的记录不存在".GetRes());
            //}
            CheckInput(form, entity);
            var instance = Get("select * from sm_t_process where Instanceid=@InstanceId", new { InstanceId = entity["InstanceId"] });

            var item = Get(GetSelectSql(TableName), GetParam(entity));

            if (item != null)
            {
                //update
                var newEntity = item.ToDFDictionary().Merge(entity);
                CheckData(TableName, newEntity, CurrentUserName);
                newEntity.Add("ProcessStatus", "INFLOW");
                SaveData(TableName, newEntity, IMPORT_TYPE_UPDATE);
            }
            else
            {
                //insert
                
                var newEntity = entity;
                CheckData(TableName, newEntity, CurrentUserName);

                newEntity.Add("ProcessStatus","INFLOW");

            
                SaveData(TableName, newEntity, IMPORT_TYPE_INSERT);
            }

            message = "保存成功";
            return DFPub.EXECUTE_SUCCESS;
        }
        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            var item = Get(GetSelectSql(TableName), GetParam(entity));
            if (item != null)
            {
                throw new WFException("记录已经存在".GetRes());
            }
            CheckInput(form, entity);
            var newEntity = entity;
            CheckData(TableName, newEntity, CurrentUserName);
            using (var db = Pub.DB)
            {
                db.Update<SM_T_PROCESS>(newEntity.To<SM_T_PROCESS>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }


        public override void CheckInput(FormM form, DFDictionary entity)
        {
            base.CheckInput(form, entity);
            if(string.IsNullOrWhiteSpace(entity["ZDB"]))
            {
                throw new WFException("请输入振动泵信息");
            }
            //碱洗电流最大值控制数字
            //var jxdlmax = ParseHelper.ParseDecimal(entity["JXDLMax"]);
            //if (!jxdlmax.HasValue)
            //{
            //    throw new WFException("碱洗电流最大值必须是数字");
            //}
            ////碱洗电流最小值控制数字
            //var jxdlmin = ParseHelper.ParseDecimal(entity["JXDLMin"]);
            //if (!jxdlmin.HasValue)
            //{
            //    throw new WFException("碱洗电流最小值必须是数字");
            //}

            //总炉压标准值控制数字
            var TotalLYStandard = ParseHelper.ParseDecimal(entity["TotalLYStandard"]);
            if (!TotalLYStandard.HasValue)
            {
                throw new WFException("总炉压标准值必须是数字");
            }

            ////总炉压最大值控制数字
            //var TotalLYMax = ParseHelper.ParseDecimal(entity["TotalLYMax"]);
            //if (!TotalLYMax.HasValue)
            //{
            //    throw new WFException("总炉压最大值必须是数字");
            //}

            ////总炉压最小值控制数字
            //var TotalLYMin = ParseHelper.ParseDecimal(entity["TotalLYMin"]);
            //if (!TotalLYMin.HasValue)
            //{
            //    throw new WFException("总炉压最小值必须是数字");
            //}

            //排线速度标准控制数字
            var PXSpeedStandard = ParseHelper.ParseDecimal(entity["PXSpeedStandard"]);
            if (!PXSpeedStandard.HasValue)
            {
                throw new WFException("排线速度标准值必须是数字");
            }

            ////排线速度最大控制数字
            //var PXSpeedMax = ParseHelper.ParseDecimal(entity["PXSpeedMax"]);
            //if (!PXSpeedMax.HasValue)
            //{
            //    throw new WFException("排线速度最大值必须是数字");
            //}

            ////排线速度最小控制数字
            //var PXSpeedMin = ParseHelper.ParseDecimal(entity["PXSpeedMin"]);
            //if (!PXSpeedMin.HasValue)
            //{
            //    throw new WFException("排线速度最小值必须是数字");
            //}

            //收线张力标准控制数字
            var SXZLStandard = ParseHelper.ParseDecimal(entity["SXZLStandard"]);
            if (!SXZLStandard.HasValue)
            {
                throw new WFException("收线张力标准值必须是数字");
            }

            //收线张力最大控制数字
            //var SXZLMax = ParseHelper.ParseDecimal(entity["SXZLMax"]);
            //if (!SXZLMax.HasValue)
            //{
            //    throw new WFException("收线张力最大值必须是数字");
            //}

            //收线张力最小控制数字
            //var SXZLMin = ParseHelper.ParseDecimal(entity["SXZLMin"]);
            //if (!SXZLMin.HasValue)
            //{
            //    throw new WFException("收线张力最小值必须是数字");
            //}
            
            using (var db = Pub.DB)
            {
                //检查收线的生产信息
                var s1 = "select count(1) from sm_t_process_product where InstanceId=@InstanceId";
                var c1 = db.Query<int>(s1, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if(c1==0||c1!=8)
                {
                    throw new WFException("收线的生产信息AQ1-AQ8必须填写");
                }
                //检查收线的槽液工艺
                var s2 = "select count(1) from sm_t_process_cy where InstanceId=@InstanceId";
                var c2= db.Query<int>(s2, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (c2 == 0)
                {
                    throw new WFException("收线的槽液工艺设定必须填写");
                }
                //检查收线的浴槽信息
                var s3 = "select count(1) from sm_t_process_yc where InstanceId=@InstanceId";
                var c3 = db.Query<int>(s3, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (c3 == 0)
                {
                    throw new WFException("收线的浴槽信息必须填写");
                }
                //检查收线的产品质量标准
                var s4 = "select count(1) from sm_t_process_quality where InstanceId=@InstanceId";
                var c4 = db.Query<int>(s4, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (c4 == 0)
                {
                    throw new WFException("收线的产品质量标准信息必须填写");
                }
                //检查放线的炉压信息
                var s5 = "select count(1) from sm_t_process_ly where InstanceId=@InstanceId";
                var c5 = db.Query<int>(s5, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (c5 == 0)
                {
                    throw new WFException("放线的炉压信息必须填写");
                }
                //检查放线的AQ信息
                var s6 = "select count(1) from sm_t_process_aq where InstanceId=@InstanceId";
                var c6 = db.Query<int>(s6, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (c6 == 0)
                {
                    throw new WFException("放线的AQ准信息必须填写");
                }
            }


        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            //var count = 0;
            //var sql = @"select * from SM_T_PROCESS where 1=1";
            //var param = new
            //{
            //    InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            //};
            //var list = GetList(entity, ref count, start, limit, sql, "order by InstanceId", param);
            //vm.results = count;
            //vm.rows = list;
            //return DFPub.EXECUTE_SUCCESS;

            if (entity["GridId"] == "grid_product")
            {
                //查询生产信息
                QueryGridProduct(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_yc")
            {
                //查询浴槽设定信息
                QueryGridYC(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_quality")
            {
                //查询产品质量信息
                QueryGridQuality(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_cy")
            {
                //查询产品质量信息
                QueryGridCY(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_ly")
            {
                //查询产品质量信息
                QueryGridLY(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_lw")
            {
                //查询产品质量信息
                QueryGridLW(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_lqf")
            {
                //查询产品质量信息
                QueryGridLQF(form, entity, vm, start, limit);
            }
            else if (entity["GridId"] == "grid_aq")
            {
                //查询产品质量信息
                QueryGridAQ(form, entity, vm, start, limit);
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        public void QueryGridProduct(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from SM_T_PROCESS_PRODUCT where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by LinePosition", param);


            vm.results = count;
            vm.rows = list;

        }

        public void QueryGridYC(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from v_SM_T_PROCESS_YC where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by PKId", param);


            vm.results = count;
            vm.rows = list;

        }


        public void QueryGridQuality(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from v_SM_T_PROCESS_QUALITY where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by GG", param);


            vm.results = count;
            vm.rows = list;

        }

        public void QueryGridLY(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from v_SM_T_PROCESS_LY where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by PKid", param);


            vm.results = count;
            vm.rows = list;

        }


        public void QueryGridLW(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from SM_T_PROCESS_LW where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by PKid", param);


            vm.results = count;
            vm.rows = list;
        }

        public void QueryGridLQF(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from SM_T_PROCESS_LQF where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by PKid", param);


            vm.results = count;
            vm.rows = list;
        }
        public void QueryGridAQ(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from SM_T_PROCESS_AQ where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by PKid", param);


            vm.results = count;
            vm.rows = list;

        }

        public void QueryGridCY(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            var count = 0;
            var sql = "select * from v_sm_t_process_cy where 1=1";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by PKId", param);


            vm.results = count;
            vm.rows = list;

        }
    }
}
