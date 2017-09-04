using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class WeiXinRule
	{
		private readonly Chain.DAL.WeiXinRule dal = new Chain.DAL.WeiXinRule();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int RuleID)
		{
			return this.dal.Exists(RuleID);
		}

		public int Add(Chain.Model.WeiXinRule model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.WeiXinRule model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int RuleID)
		{
			return this.dal.Delete(RuleID);
		}

		public bool DeleteList(string RuleIDlist)
		{
			return this.dal.DeleteList(RuleIDlist);
		}

		public Chain.Model.WeiXinRule GetModel(int RuleID)
		{
			return this.dal.GetModel(RuleID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.WeiXinRule> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.WeiXinRule> DataTableToList(DataTable dt)
		{
			List<Chain.Model.WeiXinRule> modelList = new List<Chain.Model.WeiXinRule>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.WeiXinRule model = new Chain.Model.WeiXinRule();
					if (dt.Rows[i]["RuleID"] != null && dt.Rows[i]["RuleID"].ToString() != "")
					{
						model.RuleID = int.Parse(dt.Rows[i]["RuleID"].ToString());
					}
					if (dt.Rows[i]["RuleNUmber"] != null && dt.Rows[i]["RuleNUmber"].ToString() != "")
					{
						model.RuleNUmber = dt.Rows[i]["RuleNUmber"].ToString();
					}
					if (dt.Rows[i]["RuleNewsType"] != null && dt.Rows[i]["RuleNewsType"].ToString() != "")
					{
						model.RuleNewsType = dt.Rows[i]["RuleNewsType"].ToString();
					}
					if (dt.Rows[i]["RuleDesc"] != null && dt.Rows[i]["RuleDesc"].ToString() != "")
					{
						model.RuleDesc = dt.Rows[i]["RuleDesc"].ToString();
					}
					if (dt.Rows[i]["RuleContent"] != null && dt.Rows[i]["RuleContent"].ToString() != "")
					{
						model.RuleContent = dt.Rows[i]["RuleContent"].ToString();
					}
					if (dt.Rows[i]["RuleUserID"] != null && dt.Rows[i]["RuleUserID"].ToString() != "")
					{
						model.RuleUserID = int.Parse(dt.Rows[i]["RuleUserID"].ToString());
					}
					if (dt.Rows[i]["RuleCreateTime"] != null && dt.Rows[i]["RuleCreateTime"].ToString() != "")
					{
						model.RuleCreateTime = DateTime.Parse(dt.Rows[i]["RuleCreateTime"].ToString());
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

		public string AttentionStr()
		{
			return this.dal.AttentionStr();
		}

		public DataTable Attention()
		{
			return this.dal.Attention();
		}

		public string Reply1()
		{
			return this.dal.Reply1();
		}

		public string Reply2()
		{
			return this.dal.Reply2();
		}

		public string GetCardDesc()
		{
			return this.dal.GetCardDesc();
		}

		public string ErrorStr()
		{
			return this.dal.ErrorStr();
		}

		public Chain.Model.WeiXinRule GetModelByNewsRuleID(string RuleNUmber)
		{
			return this.dal.GetModelByNewsRuleID(RuleNUmber);
		}
	}
}
