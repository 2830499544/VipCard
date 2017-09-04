using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class ScreenPopUp
	{
		private readonly Chain.IDAL.ScreenPopUp dal = new Chain.IDAL.ScreenPopUp();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int CallerID)
		{
			return this.dal.Exists(CallerID);
		}

		public int Add(Chain.Model.ScreenPopUp model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.ScreenPopUp model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int CallerID)
		{
			return this.dal.Delete(CallerID);
		}

		public bool DeleteList(string CallerIDlist)
		{
			return this.dal.DeleteList(CallerIDlist);
		}

		public Chain.Model.ScreenPopUp GetModel(int CallerID)
		{
			return this.dal.GetModel(CallerID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.ScreenPopUp> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.ScreenPopUp> DataTableToList(DataTable dt)
		{
			List<Chain.Model.ScreenPopUp> modelList = new List<Chain.Model.ScreenPopUp>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.ScreenPopUp model = new Chain.Model.ScreenPopUp();
					if (dt.Rows[i]["CallerID"] != null && dt.Rows[i]["CallerID"].ToString() != "")
					{
						model.CallerID = int.Parse(dt.Rows[i]["CallerID"].ToString());
					}
					if (dt.Rows[i]["CallerMemID"] != null && dt.Rows[i]["CallerMemID"].ToString() != "")
					{
						model.CallerMemID = int.Parse(dt.Rows[i]["CallerMemID"].ToString());
					}
					if (dt.Rows[i]["CallerMobile"] != null && dt.Rows[i]["CallerMobile"].ToString() != "")
					{
						model.CallerMobile = dt.Rows[i]["CallerMobile"].ToString();
					}
					if (dt.Rows[i]["CallerIsMem"] != null && dt.Rows[i]["CallerIsMem"].ToString() != "")
					{
						model.CallerIsMem = dt.Rows[i]["CallerIsMem"].ToString();
					}
					if (dt.Rows[i]["CallerState"] != null && dt.Rows[i]["CallerState"].ToString() != "")
					{
						model.CallerState = dt.Rows[i]["CallerState"].ToString();
					}
					if (dt.Rows[i]["CallerDuration"] != null && dt.Rows[i]["CallerDuration"].ToString() != "")
					{
						model.CallerDuration = dt.Rows[i]["CallerDuration"].ToString();
					}
					if (dt.Rows[i]["CallerRemark"] != null && dt.Rows[i]["CallerRemark"].ToString() != "")
					{
						model.CallerRemark = dt.Rows[i]["CallerRemark"].ToString();
					}
					if (dt.Rows[i]["CallerCreateTime"] != null && dt.Rows[i]["CallerCreateTime"].ToString() != "")
					{
						model.CallerCreateTime = DateTime.Parse(dt.Rows[i]["CallerCreateTime"].ToString());
					}
					if (dt.Rows[i]["CallerUserID"] != null && dt.Rows[i]["CallerUserID"].ToString() != "")
					{
						model.CallerUserID = int.Parse(dt.Rows[i]["CallerUserID"].ToString());
					}
					if (dt.Rows[i]["CallerShopID"] != null && dt.Rows[i]["CallerShopID"].ToString() != "")
					{
						model.CallerShopID = int.Parse(dt.Rows[i]["CallerShopID"].ToString());
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

		public DataSet GetScreenPopUpList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetScreenPopUpList(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
