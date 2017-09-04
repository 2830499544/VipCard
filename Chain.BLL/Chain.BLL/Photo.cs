using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Photo
	{
		private readonly Chain.DAL.Photo dal = new Chain.DAL.Photo();

		public bool Exists(int PhotoID)
		{
			return this.dal.Exists(PhotoID);
		}

		public DataSet GetPhotoInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetPhotoInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public int Add(Chain.Model.Photo model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.Photo model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int PhotoID)
		{
			return this.dal.Delete(PhotoID);
		}

		public bool DeleteList(string PhotoIDlist)
		{
			return this.dal.DeleteList(PhotoIDlist);
		}

		public Chain.Model.Photo GetModel(int PhotoID)
		{
			return this.dal.GetModel(PhotoID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.Photo> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Photo> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Photo> modelList = new List<Chain.Model.Photo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Photo model = new Chain.Model.Photo();
					if (dt.Rows[i]["PhotoID"] != null && dt.Rows[i]["PhotoID"].ToString() != "")
					{
						model.PhotoID = int.Parse(dt.Rows[i]["PhotoID"].ToString());
					}
					if (dt.Rows[i]["PhotoName"] != null && dt.Rows[i]["PhotoName"].ToString() != "")
					{
						model.PhotoName = dt.Rows[i]["PhotoName"].ToString();
					}
					if (dt.Rows[i]["PhotoPhoto"] != null && dt.Rows[i]["PhotoPhoto"].ToString() != "")
					{
						model.PhotoPhoto = dt.Rows[i]["PhotoPhoto"].ToString();
					}
					if (dt.Rows[i]["PhotoDesc"] != null && dt.Rows[i]["PhotoDesc"].ToString() != "")
					{
						model.PhotoDesc = dt.Rows[i]["PhotoDesc"].ToString();
					}
					if (dt.Rows[i]["PhotoCreateTime"] != null && dt.Rows[i]["PhotoCreateTime"].ToString() != "")
					{
						model.PhotoCreateTime = DateTime.Parse(dt.Rows[i]["PhotoCreateTime"].ToString());
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
