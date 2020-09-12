using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;

namespace WFCommon
{
	public class BT_PaiGongDan_CLASSMAPPER : ClassMapper<BT_PaiGongDan>
	{
		public BT_PaiGongDan_CLASSMAPPER()
		{
			Map(f => f.ID).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class BT_PaiGongDan
	{
		public string ID { get; set; }
		public string PROJECT_NAME { get; set; }
		public string PRODUCT_NAME { get; set; }
		public string ORDER_NUMBER { get; set; }
		public string HE_MO { get; set; }
		public string JINFUHAO { get; set; }
		public string CHANG { get; set; }
		public string KUAN { get; set; }
		public string GAO { get; set; }
		public string UNIT { get; set; }
		public string ORDER_AMUONT { get; set; }
		public string FANG_LIANG { get; set; }
		public string BAD_NUMBER { get; set; }
		public string ZHUAN_NUMBER { get; set; }
		public string TABLE_MAKER { get; set; }
		public string WANG_PIAN_MAKE { get; set; }
		public string WANG_PIAN_ZUZHUANG { get; set; }
		public string FU_QIAN_QIE_GE { get; set; }
		public string CHENG_PIN_BAO_ZHUANG { get; set; }
		public string TIME_ZHIBIAO { get; set; }
		public string TIME_WANGPIANZHIZAUO { get; set; }
		public string TIME_WANGPIANZUZHUANG { get; set; }
		public string TIME_FUQIANQIEGE { get; set; }
		public string TIME_CHENGPINBAOZHUANG { get; set; }
		public DateTime? REMARK1 { get; set; }
		public string REMARK2 { get; set; }
		public string REMARK3 { get; set; }
		public string REMARK4 { get; set; }
		public string REMARK5 { get; set; }
		public string REMARK6 { get; set; }
	}
}
