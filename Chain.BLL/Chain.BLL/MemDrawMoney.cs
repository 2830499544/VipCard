using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemDrawMoney
	{
		private readonly Chain.IDAL.MemDrawMoney dal = new Chain.IDAL.MemDrawMoney();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int DrawMoneyID)
		{
			return this.dal.Exists(DrawMoneyID);
		}

		public int Add(Chain.Model.MemDrawMoney model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemDrawMoney model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int DrawMoneyID)
		{
			return this.dal.Delete(DrawMoneyID);
		}

		public bool DeleteList(string DrawMoneyIDlist)
		{
			return this.dal.DeleteList(DrawMoneyIDlist);
		}

		public Chain.Model.MemDrawMoney GetModel(int DrawMoneyID)
		{
			return this.dal.GetModel(DrawMoneyID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemDrawMoney> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemDrawMoney> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemDrawMoney> modelList = new List<Chain.Model.MemDrawMoney>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemDrawMoney model = new Chain.Model.MemDrawMoney();
					if (dt.Rows[i]["DrawMoneyID"] != null && dt.Rows[i]["DrawMoneyID"].ToString() != "")
					{
						model.DrawMoneyID = int.Parse(dt.Rows[i]["DrawMoneyID"].ToString());
					}
					if (dt.Rows[i]["DrawMoneyMemID"] != null && dt.Rows[i]["DrawMoneyMemID"].ToString() != "")
					{
						model.DrawMoneyMemID = int.Parse(dt.Rows[i]["DrawMoneyMemID"].ToString());
					}
					if (dt.Rows[i]["DrawMoneyAccount"] != null && dt.Rows[i]["DrawMoneyAccount"].ToString() != "")
					{
						model.DrawMoneyAccount = dt.Rows[i]["DrawMoneyAccount"].ToString();
					}
					if (dt.Rows[i]["DrawMoney"] != null && dt.Rows[i]["DrawMoney"].ToString() != "")
					{
						model.DrawMoney = decimal.Parse(dt.Rows[i]["DrawMoney"].ToString());
					}
					if (dt.Rows[i]["DrawActualMoney"] != null && dt.Rows[i]["DrawActualMoney"].ToString() != "")
					{
						model.DrawActualMoney = decimal.Parse(dt.Rows[i]["DrawActualMoney"].ToString());
					}
					if (dt.Rows[i]["DrawMoneyRemark"] != null && dt.Rows[i]["DrawMoneyRemark"].ToString() != "")
					{
						model.DrawMoneyRemark = dt.Rows[i]["DrawMoneyRemark"].ToString();
					}
					if (dt.Rows[i]["DrawMoneyShopID"] != null && dt.Rows[i]["DrawMoneyShopID"].ToString() != "")
					{
						model.DrawMoneyShopID = int.Parse(dt.Rows[i]["DrawMoneyShopID"].ToString());
					}
					if (dt.Rows[i]["DrawMoneyUserID"] != null && dt.Rows[i]["DrawMoneyUserID"].ToString() != "")
					{
						model.DrawMoneyUserID = int.Parse(dt.Rows[i]["DrawMoneyUserID"].ToString());
					}
					if (dt.Rows[i]["DrawMoneyCreateTime"] != null && dt.Rows[i]["DrawMoneyCreateTime"].ToString() != "")
					{
						model.DrawMoneyCreateTime = DateTime.Parse(dt.Rows[i]["DrawMoneyCreateTime"].ToString());
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

		public decimal GetDrawMoney(string strWhere)
		{
			return this.dal.GetDrawMoney(strWhere);
		}

		public DataSet GetDataByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			return this.dal.GetDataByTime(starttime, endtime, strwhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetDrawMoneyCount(string strSql)
		{
			return this.dal.GetDrawMoneyCount(strSql);
		}
	}
}
