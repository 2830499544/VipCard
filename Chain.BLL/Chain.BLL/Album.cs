using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Album
	{
		private readonly Chain.DAL.Album dal = new Chain.DAL.Album();

		public bool Exists(int AlbumID)
		{
			return this.dal.Exists(AlbumID);
		}

		public DataSet GetAlbumInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetAlbumInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public int Add(Chain.Model.Album model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.Album model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int AlbumID)
		{
			return this.dal.Delete(AlbumID);
		}

		public bool DeleteList(string AlbumIDlist)
		{
			return this.dal.DeleteList(AlbumIDlist);
		}

		public Chain.Model.Album GetModel(int AlbumID)
		{
			return this.dal.GetModel(AlbumID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.Album> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Album> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Album> modelList = new List<Chain.Model.Album>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Album model = new Chain.Model.Album();
					if (dt.Rows[i]["AlbumID"] != null && dt.Rows[i]["AlbumID"].ToString() != "")
					{
						model.AlbumID = int.Parse(dt.Rows[i]["AlbumID"].ToString());
					}
					if (dt.Rows[i]["AlbumName"] != null && dt.Rows[i]["AlbumName"].ToString() != "")
					{
						model.AlbumName = dt.Rows[i]["AlbumName"].ToString();
					}
					if (dt.Rows[i]["AlbumPhoto"] != null && dt.Rows[i]["AlbumPhoto"].ToString() != "")
					{
						model.AlbumPhoto = dt.Rows[i]["AlbumPhoto"].ToString();
					}
					if (dt.Rows[i]["AlbumDesc"] != null && dt.Rows[i]["AlbumDesc"].ToString() != "")
					{
						model.AlbumDesc = dt.Rows[i]["AlbumDesc"].ToString();
					}
					if (dt.Rows[i]["AlbumCreateTime"] != null && dt.Rows[i]["AlbumCreateTime"].ToString() != "")
					{
						model.AlbumCreateTime = DateTime.Parse(dt.Rows[i]["AlbumCreateTime"].ToString());
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
	}
}
