using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SymbolShow
	{
		private readonly Chain.IDAL.SymbolShow dal = new Chain.IDAL.SymbolShow();

		public bool Exists(int SymbolID)
		{
			return this.dal.Exists(SymbolID);
		}

		public int Add(Chain.Model.SymbolShow model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SymbolShow model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int SymbolID)
		{
			return this.dal.Delete(SymbolID);
		}

		public bool DeleteList(string SymbolIDlist)
		{
			return this.dal.DeleteList(SymbolIDlist);
		}

		public Chain.Model.SymbolShow GetModel(int SymbolID)
		{
			return this.dal.GetModel(SymbolID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SymbolShow> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SymbolShow> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SymbolShow> modelList = new List<Chain.Model.SymbolShow>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SymbolShow model = new Chain.Model.SymbolShow();
					if (dt.Rows[i]["SymbolID"] != null && dt.Rows[i]["SymbolID"].ToString() != "")
					{
						model.SymbolID = int.Parse(dt.Rows[i]["SymbolID"].ToString());
					}
					if (dt.Rows[i]["SymbolTitle"] != null && dt.Rows[i]["SymbolTitle"].ToString() != "")
					{
						model.SymbolTitle = dt.Rows[i]["SymbolTitle"].ToString();
					}
					if (dt.Rows[i]["SymbolPhoto"] != null && dt.Rows[i]["SymbolPhoto"].ToString() != "")
					{
						model.SymbolPhoto = dt.Rows[i]["SymbolPhoto"].ToString();
					}
					if (dt.Rows[i]["SymbolDesc"] != null && dt.Rows[i]["SymbolDesc"].ToString() != "")
					{
						model.SymbolDesc = dt.Rows[i]["SymbolDesc"].ToString();
					}
					if (dt.Rows[i]["SymbolTime"] != null && dt.Rows[i]["SymbolTime"].ToString() != "")
					{
						model.SymbolTime = DateTime.Parse(dt.Rows[i]["SymbolTime"].ToString());
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

		public DataSet GetSymbolShowInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetSymbolShowInfo(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
