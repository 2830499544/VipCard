using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemChild
	{
		private readonly Chain.DAL.MemChild dal = new Chain.DAL.MemChild();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int LevelID)
		{
			return this.dal.Exists(LevelID);
		}

		public bool Exists(int LevelID, int Point)
		{
			return this.dal.Exists(LevelID, Point);
		}

		public int Add(Chain.Model.MemChild model)
		{
			return this.dal.Add(model);
		}

		public int Update(Chain.Model.MemChild model)
		{
			return this.dal.Update(model);
		}

		public int Delete(int LevelID)
		{
			return this.dal.Delete(LevelID);
		}

		public bool DeleteList(string LevelIDlist)
		{
			return this.dal.DeleteList(LevelIDlist);
		}

		public Chain.Model.MemChild GetModel(int LevelID)
		{
			return this.dal.GetModel(LevelID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public List<Chain.Model.MemChild> GetLevelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList1(ds.Tables[0]);
		}

		public DataSet GetLists(string strWhere)
		{
			return this.dal.GetLists(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemChild> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetLists(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemChild> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemChild> modelList = new List<Chain.Model.MemChild>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemChild model = new Chain.Model.MemChild();
					modelList.Add(model);
				}
			}
			return modelList;
		}

		public List<Chain.Model.MemChild> DataTableToList1(DataTable dt)
		{
			List<Chain.Model.MemChild> modelList = new List<Chain.Model.MemChild>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemChild model = new Chain.Model.MemChild();
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
	}
}
