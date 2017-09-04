using Chain.DAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class MemAddress
	{
		private readonly Chain.DAL.MemAddress dal = new Chain.DAL.MemAddress();

		public bool Exists(int ID)
		{
			return this.dal.Exists(ID);
		}

		public DataSet GetAlbumInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetAlbumInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public int Add(Chain.Model.MemAddress model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemAddress model)
		{
			return this.dal.Update(model);
		}

		public int UpdateDefaultAddressByMemID(int MemID, int IsDefault)
		{
			return this.dal.UpdateDefaultAddressByMemID(MemID, IsDefault);
		}

		public int UpdateDefaultAddressByID(int ID, int MemID, int IsDefault)
		{
			return this.dal.UpdateDefaultAddressByID(ID, MemID, IsDefault);
		}

		public bool Delete(int ID)
		{
			return this.dal.Delete(ID);
		}

		public bool DeleteList(string AlbumIDlist)
		{
			return this.dal.DeleteList(AlbumIDlist);
		}

		public Chain.Model.MemAddress GetModel(int ID)
		{
			return this.dal.GetModel(ID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
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
