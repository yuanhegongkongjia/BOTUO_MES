using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using WFCore;
using DynamicForm.Core;
using WFCommon;
using WFCommon.Utility;
using System.Text;

namespace WFDataAccess
{
    public class WF_T_SEQNOLoader
    {
        public static string GenerateJobNo(string prefix1, string prefix2)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_T_SEQNO where 1=1";
                if (!string.IsNullOrWhiteSpace(prefix1))
                {
                    sql += " and Prefix1=@Prefix1";
                }
                if (!string.IsNullOrWhiteSpace(prefix2))
                {
                    sql += " and Prefix2=@Prefix2";
                }
                //if (!string.IsNullOrWhiteSpace(prefix3))
                //{
                //    sql += " and Prefix3=@Prefix3";
                //}
                var parameters = new
                {
                    Prefix1 = prefix1,
                    Prefix2 = prefix2,
                    //Prefix3 = prefix3,
                    seqnoid = Guid.NewGuid().ToString(),
                    //CURRENTSEQNO = currentSeqNo.ToString()
                };
                var entity = db.ExecuteDataTable(sql, parameters);
                if (entity.Rows.Count > 0)
                {
                    //sql = "UPDATE WF_T_SEQNO SET CURRENTSEQNO=@CURRENTSEQNO WHERE SEQNOID=@SEQNOID";
                    //db.Execute(sql, new { CURRENTSEQNO = currentSeqNo, SEQNOID = entity.Rows[0]["SEQNOID"].ToString() });
                    return string.Format("{0}-{1}-{2}", prefix1, prefix2, entity.Rows[0]["CurrentSeqNo"].ToString());
                }
                else
                {
                    //sql = "insert into WF_T_SEQNO(seqnoid,prefix1,prefix2,prefix3,CURRENTSEQNO) values(@seqnoid,@prefix1,@prefix2,@prefix3,@CURRENTSEQNO)";
                    //db.Execute(sql, parameters);
                    return string.Format("{0}-{1}-{2}", prefix1, prefix2, "0001");

                }
            }

        }


        public static void UpdateJobNo(string Prefix1, string Prefix2, string CurrentSeqNo)
        {
            using (var db = Pub.DB)
            {
                var seq = int.Parse(CurrentSeqNo) + 1;
                var sql = "select * from WF_T_SEQNO where Prefix1=@Prefix1 and Prefix2=@Prefix2 and CurrentSeqNo=@CurrentSeqNo";
                var item = db.Query<WF_T_SEQNO>(sql, new { Prefix1 = Prefix1, Prefix2 = Prefix2, CurrentSeqNo = CurrentSeqNo }).FirstOrDefault();
                if (item == null)
                {
                    sql = "insert into WF_T_SEQNO(seqnoid,prefix1,prefix2,CURRENTSEQNO) values(@seqnoid,@prefix1,@prefix2,@CURRENTSEQNO)";
                    db.Execute(sql, new { seqnoid = Guid.NewGuid().ToString(), prefix1 = Prefix1, prefix2 = Prefix2, CURRENTSEQNO = seq.ToString().PadLeft(4, '0') });
                }
                else
                {
                    item.CurrentSeqNo = seq.ToString().PadLeft(4, '0');
                    db.Update(item);
                }
            }
        }

    }
}