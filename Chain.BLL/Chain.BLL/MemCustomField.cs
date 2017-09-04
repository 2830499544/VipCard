using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemCustomField
	{
		private readonly Chain.IDAL.MemCustomField dal = new Chain.IDAL.MemCustomField();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(string CustomField)
		{
			return this.dal.Exists(CustomField);
		}

		public bool Exists(int CustomFieldID, string CustomFieldName)
		{
			return this.dal.Exists(CustomFieldID, CustomFieldName);
		}

		public bool ExistTheSameColumnName(int CustomType, string CustomFieldName)
		{
			string TemplateColumnsName;
			if (CustomType == 1)
			{
				TemplateColumnsName = "会员卡号,姓名,性别,身份证号码,手机号码,固定电话,生日,积分,余额,历史消费金额,电子邮箱,地址,会员等级ID,开卡商家ID,办卡日期,备注,卡面号码";
			}
			else
			{
				TemplateColumnsName = "商品编码,商品名称,商品简码,商品分类ID,计量单位,参考进价,零售单价,商品积分,商品类型,最低折扣,提成类型,提成金额(比例),商品简介";
			}
			return TemplateColumnsName.Contains(CustomFieldName);
		}

		public int Add(Chain.Model.MemCustomField model)
		{
			int result;
			if (this.ExistTheSameColumnName(model.CustomType, model.CustomFieldName) || this.Exists(model.CustomField))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.MemCustomField model)
		{
			int result;
			if (this.Exists(model.CustomFieldID, model.CustomFieldName))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public bool Delete(int CustomFieldID)
		{
			return this.dal.Delete(CustomFieldID);
		}

		public bool DeleteList(string CustomFieldIDlist)
		{
			return this.dal.DeleteList(CustomFieldIDlist);
		}

		public Chain.Model.MemCustomField GetModel(int CustomFieldID)
		{
			return this.dal.GetModel(CustomFieldID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataRow[] CustomGetList(string strWhere)
		{
			DataSet ds = this.GetAllList();
			return ds.Tables[0].Select(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemCustomField> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemCustomField> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemCustomField> modelList = new List<Chain.Model.MemCustomField>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemCustomField model = new Chain.Model.MemCustomField();
					if (dt.Rows[i]["CustomFieldID"] != null && dt.Rows[i]["CustomFieldID"].ToString() != "")
					{
						model.CustomFieldID = int.Parse(dt.Rows[i]["CustomFieldID"].ToString());
					}
					if (dt.Rows[i]["CustomType"] != null && dt.Rows[i]["CustomType"].ToString() != "")
					{
						model.CustomType = int.Parse(dt.Rows[i]["CustomType"].ToString());
					}
					if (dt.Rows[i]["CustomField"] != null && dt.Rows[i]["CustomField"].ToString() != "")
					{
						model.CustomField = dt.Rows[i]["CustomField"].ToString();
					}
					if (dt.Rows[i]["CustomFieldName"] != null && dt.Rows[i]["CustomFieldName"].ToString() != "")
					{
						model.CustomFieldName = dt.Rows[i]["CustomFieldName"].ToString();
					}
					if (dt.Rows[i]["CustomFieldIsNull"] != null && dt.Rows[i]["CustomFieldIsNull"].ToString() != "")
					{
						if (dt.Rows[i]["CustomFieldIsNull"].ToString() == "1" || dt.Rows[i]["CustomFieldIsNull"].ToString().ToLower() == "true")
						{
							model.CustomFieldIsNull = true;
						}
						else
						{
							model.CustomFieldIsNull = false;
						}
					}
					if (dt.Rows[i]["CustomFieldIsShow"] != null && dt.Rows[i]["CustomFieldIsShow"].ToString() != "")
					{
						if (dt.Rows[i]["CustomFieldIsShow"].ToString() == "1" || dt.Rows[i]["CustomFieldIsShow"].ToString().ToLower() == "true")
						{
							model.CustomFieldIsShow = true;
						}
						else
						{
							model.CustomFieldIsShow = false;
						}
					}
					if (dt.Rows[i]["CustomFieldType"] != null && dt.Rows[i]["CustomFieldType"].ToString() != "")
					{
						model.CustomFieldType = dt.Rows[i]["CustomFieldType"].ToString();
					}
					if (dt.Rows[i]["CustomFieldInfo"] != null && dt.Rows[i]["CustomFieldInfo"].ToString() != "")
					{
						model.CustomFieldInfo = dt.Rows[i]["CustomFieldInfo"].ToString();
					}
					if (dt.Rows[i]["CustomFieldShopID"] != null && dt.Rows[i]["CustomFieldShopID"].ToString() != "")
					{
						model.CustomFieldShopID = int.Parse(dt.Rows[i]["CustomFieldShopID"].ToString());
					}
					if (dt.Rows[i]["CustomFieldCreateTime"] != null && dt.Rows[i]["CustomFieldCreateTime"].ToString() != "")
					{
						model.CustomFieldCreateTime = DateTime.Parse(dt.Rows[i]["CustomFieldCreateTime"].ToString());
					}
					if (dt.Rows[i]["CustomFieldUserID"] != null && dt.Rows[i]["CustomFieldUserID"].ToString() != "")
					{
						model.CustomFieldUserID = int.Parse(dt.Rows[i]["CustomFieldUserID"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		public DataSet GetAllList()
		{
			return this.GetList("");
		}

		public int GetRecordCount(string strWhere)
		{
			return this.dal.GetRecordCount(strWhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public DataSet GetAllCustom()
		{
			return this.dal.GetAllCustom();
		}

		public int UpdateCustomFieldShow(string CustomFieldID, bool IsShow)
		{
			return this.dal.UpdateCustomFieldShow(CustomFieldID, IsShow);
		}
	}
}
