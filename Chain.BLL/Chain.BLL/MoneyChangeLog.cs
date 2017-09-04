using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MoneyChangeLog
	{
		private readonly Chain.IDAL.MoneyChangeLog dal = new Chain.IDAL.MoneyChangeLog();

		public bool Exists(int MoneyChangeID)
		{
			return this.dal.Exists(MoneyChangeID);
		}

		public bool Exists(string orderaccount)
		{
			return this.dal.Exists(orderaccount);
		}

		public int Add(Chain.Model.MoneyChangeLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MoneyChangeLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MoneyChangeID)
		{
			return this.dal.Delete(MoneyChangeID);
		}

		public bool DeleteList(string MoneyChangeIDlist)
		{
			return this.dal.DeleteList(MoneyChangeIDlist);
		}

		public Chain.Model.MoneyChangeLog GetModel(int MoneyChangeID)
		{
			return this.dal.GetModel(MoneyChangeID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MoneyChangeLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MoneyChangeLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MoneyChangeLog> modelList = new List<Chain.Model.MoneyChangeLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MoneyChangeLog model = new Chain.Model.MoneyChangeLog();
					if (dt.Rows[i]["MoneyChangeID"] != null && dt.Rows[i]["MoneyChangeID"].ToString() != "")
					{
						model.MoneyChangeID = int.Parse(dt.Rows[i]["MoneyChangeID"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeMemID"] != null && dt.Rows[i]["MoneyChangeMemID"].ToString() != "")
					{
						model.MoneyChangeMemID = int.Parse(dt.Rows[i]["MoneyChangeMemID"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeUserID"] != null && dt.Rows[i]["MoneyChangeUserID"].ToString() != "")
					{
						model.MoneyChangeUserID = int.Parse(dt.Rows[i]["MoneyChangeUserID"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeType"] != null && dt.Rows[i]["MoneyChangeType"].ToString() != "")
					{
						model.MoneyChangeType = int.Parse(dt.Rows[i]["MoneyChangeType"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeAccount"] != null && dt.Rows[i]["MoneyChangeAccount"].ToString() != "")
					{
						model.MoneyChangeAccount = dt.Rows[i]["MoneyChangeAccount"].ToString();
					}
					if (dt.Rows[i]["MoneyChangeMoney"] != null && dt.Rows[i]["MoneyChangeMoney"].ToString() != "")
					{
						model.MoneyChangeMoney = decimal.Parse(dt.Rows[i]["MoneyChangeMoney"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeCash"] != null && dt.Rows[i]["MoneyChangeCash"].ToString() != "")
					{
						model.MoneyChangeCash = decimal.Parse(dt.Rows[i]["MoneyChangeCash"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeBalance"] != null && dt.Rows[i]["MoneyChangeBalance"].ToString() != "")
					{
						model.MoneyChangeBalance = decimal.Parse(dt.Rows[i]["MoneyChangeBalance"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeUnionPay"] != null && dt.Rows[i]["MoneyChangeUnionPay"].ToString() != "")
					{
						model.MoneyChangeUnionPay = decimal.Parse(dt.Rows[i]["MoneyChangeUnionPay"].ToString());
					}
					if (dt.Rows[i]["MemMoney"] != null && dt.Rows[i]["MemMoney"].ToString() != "")
					{
						model.MemMoney = decimal.Parse(dt.Rows[i]["MemMoney"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeCreateTime"] != null && dt.Rows[i]["MoneyChangeCreateTime"].ToString() != "")
					{
						model.MoneyChangeCreateTime = DateTime.Parse(dt.Rows[i]["MoneyChangeCreateTime"].ToString());
					}
					if (dt.Rows[i]["MoneyChangeGiveMoney"] != null && dt.Rows[i]["MoneyChangeGiveMoney"].ToString() != "")
					{
						model.MoneyChangeGiveMoney = decimal.Parse(dt.Rows[i]["MoneyChangeGiveMoney"].ToString());
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

		public DataSet GetMoneyChangeLog(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetMoneyChangeLog(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public DataSet GetMoneyChange(string strSql)
		{
			return this.dal.GetMoneyChange(strSql);
		}
	}
}
