using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class OrderTime
	{
		private readonly Chain.IDAL.OrderTime dal = new Chain.IDAL.OrderTime();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int OrderTimeID)
		{
			return this.dal.Exists(OrderTimeID);
		}

		public bool ExistsRules(int RulesID)
		{
			return this.dal.ExistsRules(RulesID);
		}

		public bool ExistsProject(int ProjectID)
		{
			return this.dal.ExistsProject(ProjectID);
		}

		public bool isUse(int Project)
		{
			return this.dal.isUse(Project);
		}

		public int Add(Chain.Model.OrderTime model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.OrderTime model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int OrderTimeID)
		{
			return this.dal.Delete(OrderTimeID);
		}

		public bool DeleteList(string OrderTimeIDlist)
		{
			return this.dal.DeleteList(OrderTimeIDlist);
		}

		public Chain.Model.OrderTime GetModel(int OrderTimeID)
		{
			return this.dal.GetModel(OrderTimeID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.OrderTime> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.OrderTime> DataTableToList(DataTable dt)
		{
			List<Chain.Model.OrderTime> modelList = new List<Chain.Model.OrderTime>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.OrderTime model = new Chain.Model.OrderTime();
					if (dt.Rows[i]["OrderTimeID"] != null && dt.Rows[i]["OrderTimeID"].ToString() != "")
					{
						model.OrderTimeID = int.Parse(dt.Rows[i]["OrderTimeID"].ToString());
					}
					if (dt.Rows[i]["OrderTimeCode"] != null && dt.Rows[i]["OrderTimeCode"].ToString() != "")
					{
						model.OrderTimeCode = dt.Rows[i]["OrderTimeCode"].ToString();
					}
					if (dt.Rows[i]["OrderMemID"] != null && dt.Rows[i]["OrderMemID"].ToString() != "")
					{
						model.OrderMemID = int.Parse(dt.Rows[i]["OrderMemID"].ToString());
					}
					if (dt.Rows[i]["OrderToken"] != null && dt.Rows[i]["OrderToken"].ToString() != "")
					{
						model.OrderToken = dt.Rows[i]["OrderToken"].ToString();
					}
					if (dt.Rows[i]["OrderMarchTime"] != null && dt.Rows[i]["OrderMarchTime"].ToString() != "")
					{
						model.OrderMarchTime = DateTime.Parse(dt.Rows[i]["OrderMarchTime"].ToString());
					}
					if (dt.Rows[i]["OrderState"] != null && dt.Rows[i]["OrderState"].ToString() != "")
					{
						if (dt.Rows[i]["OrderState"].ToString() == "1" || dt.Rows[i]["OrderState"].ToString().ToLower() == "true")
						{
							model.OrderState = true;
						}
						else
						{
							model.OrderState = false;
						}
					}
					if (dt.Rows[i]["OrderOutTime"] != null && dt.Rows[i]["OrderOutTime"].ToString() != "")
					{
						model.OrderOutTime = DateTime.Parse(dt.Rows[i]["OrderOutTime"].ToString());
					}
					if (dt.Rows[i]["OrderStartUserID"] != null && dt.Rows[i]["OrderStartUserID"].ToString() != "")
					{
						model.OrderStartUserID = int.Parse(dt.Rows[i]["OrderStartUserID"].ToString());
					}
					if (dt.Rows[i]["OrderEndUserID"] != null && dt.Rows[i]["OrderEndUserID"].ToString() != "")
					{
						model.OrderEndUserID = int.Parse(dt.Rows[i]["OrderEndUserID"].ToString());
					}
					if (dt.Rows[i]["OrderCreateTime"] != null && dt.Rows[i]["OrderCreateTime"].ToString() != "")
					{
						model.OrderCreateTime = DateTime.Parse(dt.Rows[i]["OrderCreateTime"].ToString());
					}
					if (dt.Rows[i]["OrderShopID"] != null && dt.Rows[i]["OrderShopID"].ToString() != "")
					{
						model.OrderShopID = int.Parse(dt.Rows[i]["OrderShopID"].ToString());
					}
					if (dt.Rows[i]["OrderProjectID"] != null && dt.Rows[i]["OrderProjectID"].ToString() != "")
					{
						model.OrderProjectID = int.Parse(dt.Rows[i]["OrderProjectID"].ToString());
					}
					if (dt.Rows[i]["OrderRulesID"] != null && dt.Rows[i]["OrderRulesID"].ToString() != "")
					{
						model.OrderRulesID = int.Parse(dt.Rows[i]["OrderRulesID"].ToString());
					}
					if (dt.Rows[i]["OrderRemark"] != null && dt.Rows[i]["OrderRemark"].ToString() != "")
					{
						model.OrderRemark = dt.Rows[i]["OrderRemark"].ToString();
					}
					if (dt.Rows[i]["OrderPredictTime"] != null && dt.Rows[i]["OrderPredictTime"].ToString() != "")
					{
						model.OrderPredictTime = Convert.ToDecimal(dt.Rows[i]["OrderPredictTime"]);
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
