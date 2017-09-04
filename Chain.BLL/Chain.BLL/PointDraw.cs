using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class PointDraw
	{
		private readonly Chain.IDAL.PointDraw dal = new Chain.IDAL.PointDraw();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public DataSet GetVerifyListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetVerifyListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public Chain.Model.PointDraw GetModel(int DrawID)
		{
			return this.dal.GetModel(DrawID);
		}

		public bool Exists(int DrawID)
		{
			return this.dal.Exists(DrawID);
		}

		public int Add(Chain.Model.PointDraw model)
		{
			return this.dal.Add(model);
		}

		public bool Delete(int DrawID)
		{
			return this.dal.Delete(DrawID);
		}

		public bool DeleteList(string DrawIDlist)
		{
			return this.dal.DeleteList(DrawIDlist);
		}

		public bool CheckPointDraw(int DrawID, int status)
		{
			return this.dal.CheckPointDraw(DrawID, status);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.PointDraw> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public bool Update(Chain.Model.PointDraw model)
		{
			return this.dal.Update(model);
		}

		public List<Chain.Model.PointDraw> DataTableToList(DataTable dt)
		{
			List<Chain.Model.PointDraw> modelList = new List<Chain.Model.PointDraw>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.PointDraw model = new Chain.Model.PointDraw();
					if (dt.Rows[i]["DrawID"] != null && dt.Rows[i]["DrawID"].ToString() != "")
					{
						model.DrawID = int.Parse(dt.Rows[i]["DrawID"].ToString());
					}
					if (dt.Rows[i]["DrawShopID"] != null && dt.Rows[i]["DrawShopID"].ToString() != "")
					{
						model.DrawShopID = int.Parse(dt.Rows[i]["DrawShopID"].ToString());
					}
					if (dt.Rows[i]["DrawPoint"] != null && dt.Rows[i]["DrawPoint"].ToString() != "")
					{
						model.DrawPoint = int.Parse(dt.Rows[i]["DrawPoint"].ToString());
					}
					if (dt.Rows[i]["DrawAmount"] != null && dt.Rows[i]["DrawAmount"].ToString() != "")
					{
						model.DrawAmount = int.Parse(dt.Rows[i]["DrawAmount"].ToString());
					}
					if (dt.Rows[i]["DrawStatus"] != null && dt.Rows[i]["DrawStatus"].ToString() != "")
					{
						model.DrawStatus = int.Parse(dt.Rows[i]["DrawStatus"].ToString());
					}
					if (dt.Rows[i]["DrawCreateTime"] != null && dt.Rows[i]["DrawCreateTime"].ToString() != "")
					{
						model.DrawCreateTime = DateTime.Parse(dt.Rows[i]["DrawCreateTime"].ToString());
					}
					if (dt.Rows[i]["DrawCreateUserID"] != null && dt.Rows[i]["DrawCreateUserID"].ToString() != "")
					{
						model.DrawCreateUserID = int.Parse(dt.Rows[i]["DrawCreateUserID"].ToString());
					}
					if (dt.Rows[i]["DrawVerifyTime"] != null && dt.Rows[i]["DrawVerifyTime"].ToString() != "")
					{
						model.DrawVerifyTime = DateTime.Parse(dt.Rows[i]["DrawVerifyTime"].ToString());
					}
					if (dt.Rows[i]["DrawVerifyUserID"] != null && dt.Rows[i]["DrawVerifyUserID"].ToString() != "")
					{
						model.DrawVerifyUserID = int.Parse(dt.Rows[i]["DrawVerifyUserID"].ToString());
					}
					model.DrawRemark = dt.Rows[i]["DrawRemark"].ToString();
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

		public int GetShopPointDraw(int ShopID)
		{
			return this.dal.GetShopPointDraw(ShopID);
		}
	}
}
