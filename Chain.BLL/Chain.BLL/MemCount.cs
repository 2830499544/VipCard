using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemCount
	{
		private readonly Chain.IDAL.MemCount dal = new Chain.IDAL.MemCount();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int CountID)
		{
			return this.dal.Exists(CountID);
		}

		public bool Exists(string account)
		{
			return this.dal.Exists(account);
		}

		public int Add(Chain.Model.MemCount model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemCount model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int CountID)
		{
			return this.dal.Delete(CountID);
		}

		public bool DeleteList(string CountIDlist)
		{
			return this.dal.DeleteList(CountIDlist);
		}

		public Chain.Model.MemCount GetModel(int CountID)
		{
			return this.dal.GetModel(CountID);
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

		public List<Chain.Model.MemCount> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemCount> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemCount> modelList = new List<Chain.Model.MemCount>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemCount model = new Chain.Model.MemCount();
					if (dt.Rows[i]["CountID"] != null && dt.Rows[i]["CountID"].ToString() != "")
					{
						model.CountID = int.Parse(dt.Rows[i]["CountID"].ToString());
					}
					if (dt.Rows[i]["CountMemID"] != null && dt.Rows[i]["CountMemID"].ToString() != "")
					{
						model.CountMemID = int.Parse(dt.Rows[i]["CountMemID"].ToString());
					}
					if (dt.Rows[i]["CountAccount"] != null && dt.Rows[i]["CountAccount"].ToString() != "")
					{
						model.CountAccount = dt.Rows[i]["CountAccount"].ToString();
					}
					if (dt.Rows[i]["CountTotalMoney"] != null && dt.Rows[i]["CountTotalMoney"].ToString() != "")
					{
						model.CountTotalMoney = decimal.Parse(dt.Rows[i]["CountTotalMoney"].ToString());
					}
					if (dt.Rows[i]["CountDiscountMoney"] != null && dt.Rows[i]["CountDiscountMoney"].ToString() != "")
					{
						model.CountDiscountMoney = decimal.Parse(dt.Rows[i]["CountDiscountMoney"].ToString());
					}
					if (dt.Rows[i]["CountPayCard"] != null && dt.Rows[i]["CountPayCard"].ToString() != "")
					{
						model.CountPayCard = decimal.Parse(dt.Rows[i]["CountPayCard"].ToString());
					}
					if (dt.Rows[i]["CountPayCash"] != null && dt.Rows[i]["CountPayCash"].ToString() != "")
					{
						model.CountPayCash = decimal.Parse(dt.Rows[i]["CountPayCash"].ToString());
					}
					if (dt.Rows[i]["CountPayCoupon"] != null && dt.Rows[i]["CountPayCoupon"].ToString() != "")
					{
						model.CountPayCoupon = decimal.Parse(dt.Rows[i]["CountPayCoupon"].ToString());
					}
					if (dt.Rows[i]["CountPayType"] != null && dt.Rows[i]["CountPayType"].ToString() != "")
					{
						model.CountPayType = int.Parse(dt.Rows[i]["CountPayType"].ToString());
					}
					if (dt.Rows[i]["CountPoint"] != null && dt.Rows[i]["CountPoint"].ToString() != "")
					{
						model.CountPoint = int.Parse(dt.Rows[i]["CountPoint"].ToString());
					}
					if (dt.Rows[i]["CountRemark"] != null && dt.Rows[i]["CountRemark"].ToString() != "")
					{
						model.CountRemark = dt.Rows[i]["CountRemark"].ToString();
					}
					if (dt.Rows[i]["CountShopID"] != null && dt.Rows[i]["CountShopID"].ToString() != "")
					{
						model.CountShopID = int.Parse(dt.Rows[i]["CountShopID"].ToString());
					}
					if (dt.Rows[i]["CountCreateTime"] != null && dt.Rows[i]["CountCreateTime"].ToString() != "")
					{
						model.CountCreateTime = DateTime.Parse(dt.Rows[i]["CountCreateTime"].ToString());
					}
					if (dt.Rows[i]["CountUserID"] != null && dt.Rows[i]["CountUserID"].ToString() != "")
					{
						model.CountUserID = int.Parse(dt.Rows[i]["CountUserID"].ToString());
					}
					if (dt.Rows[i]["CountIsCard"] != null && dt.Rows[i]["CountIsCard"].ToString() != "")
					{
						if (dt.Rows[i]["CountIsCard"].ToString() == "1" || dt.Rows[i]["CountIsCard"].ToString().ToLower() == "true")
						{
							model.CountIsCard = true;
						}
						else
						{
							model.CountIsCard = false;
						}
					}
					if (dt.Rows[i]["CountIsCash"] != null && dt.Rows[i]["CountIsCash"].ToString() != "")
					{
						if (dt.Rows[i]["CountIsCash"].ToString() == "1" || dt.Rows[i]["CountIsCash"].ToString().ToLower() == "true")
						{
							model.CountIsCash = true;
						}
						else
						{
							model.CountIsCash = false;
						}
					}
					if (dt.Rows[i]["CountIsBink"] != null && dt.Rows[i]["CountIsBink"].ToString() != "")
					{
						if (dt.Rows[i]["CountIsBink"].ToString() == "1" || dt.Rows[i]["CountIsBink"].ToString().ToLower() == "true")
						{
							model.CountIsBink = true;
						}
						else
						{
							model.CountIsBink = false;
						}
					}
					if (dt.Rows[i]["CountPayBink"] != null && dt.Rows[i]["CountPayBink"].ToString() != "")
					{
						model.CountPayBink = decimal.Parse(dt.Rows[i]["CountPayBink"].ToString());
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

		public DataSet GetCountMoney(string strWhere)
		{
			return this.dal.GetCountMoney(strWhere);
		}

		public DataSet GetCountNumber(string strWhere)
		{
			return this.dal.GetCountNumber(strWhere);
		}

		public decimal GetTotalCash(string strwhere)
		{
			return this.dal.GetTotalCash(strwhere);
		}
	}
}
