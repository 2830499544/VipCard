using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class RechargeRule
	{
		private readonly Chain.DAL.RechargeRule dal = new Chain.DAL.RechargeRule();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int RuleID)
		{
			return this.dal.Exists(RuleID);
		}

		public int Add(Chain.Model.RechargeRule model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.RechargeRule model)
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

		public Chain.Model.RechargeRule GetModel(int RuleID)
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public List<Chain.Model.RechargeRule> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.RechargeRule> DataTableToList(DataTable dt)
		{
			List<Chain.Model.RechargeRule> modelList = new List<Chain.Model.RechargeRule>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.RechargeRule model = new Chain.Model.RechargeRule();
					if (dt.Rows[i]["RuleID"] != null && dt.Rows[i]["RuleID"].ToString() != "")
					{
						model.RuleID = int.Parse(dt.Rows[i]["RuleID"].ToString());
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

		public Chain.Model.RechargeRule GetModelByNewsRuleID(string RuleNUmber)
		{
			return this.dal.GetModelByNewsRuleID(RuleNUmber);
		}
	}
}
