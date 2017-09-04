using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GoodsNumber
	{
		private readonly Chain.IDAL.GoodsNumber dal = new Chain.IDAL.GoodsNumber();

		public bool Exists(int ID)
		{
			return this.dal.Exists(ID);
		}

		public int Add(Chain.Model.GoodsNumber model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GoodsNumber model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID)
		{
			return this.dal.Delete(ID);
		}

		public bool DeleteList(string IDlist)
		{
			return this.dal.DeleteList(IDlist);
		}

		public Chain.Model.GoodsNumber GetModel(int ID)
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

		public List<Chain.Model.GoodsNumber> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GoodsNumber> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GoodsNumber> modelList = new List<Chain.Model.GoodsNumber>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GoodsNumber model = new Chain.Model.GoodsNumber();
					if (dt.Rows[i]["ID"] != null && dt.Rows[i]["ID"].ToString() != "")
					{
						model.ID = int.Parse(dt.Rows[i]["ID"].ToString());
					}
					if (dt.Rows[i]["GoodsID"] != null && dt.Rows[i]["GoodsID"].ToString() != "")
					{
						model.GoodsID = int.Parse(dt.Rows[i]["GoodsID"].ToString());
					}
					if (dt.Rows[i]["ShopID"] != null && dt.Rows[i]["ShopID"].ToString() != "")
					{
						model.ShopID = int.Parse(dt.Rows[i]["ShopID"].ToString());
					}
					if (dt.Rows[i]["Number"] != null && dt.Rows[i]["Number"].ToString() != "")
					{
						model.Number = decimal.Parse(dt.Rows[i]["Number"].ToString());
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

		public int InsertGoodsNumber(int intGoodsID)
		{
			return this.dal.InsertGoodsNumber(intGoodsID);
		}

		public int InsertGoodsNumber(int intGoodsID, int intShopID)
		{
			return this.dal.InsertGoodsNumber(intGoodsID, intShopID);
		}

		public DataSet GetGoodsNumber(int intGoodsID, int intShopID)
		{
			return this.dal.GetGoodsNumber(intGoodsID, intShopID);
		}

		public DataSet CkeckInShopGoods(int intGoodsID, int intShopID)
		{
			return this.dal.CkeckInShopGoods(intGoodsID, intShopID);
		}

		public DataSet GetGoodsNumber(string strGoodsCode, int intShopID)
		{
			return this.dal.GetGoodsNumber(strGoodsCode, intShopID);
		}

		public DataSet GetGoodsNumberDelServe(string strGoodsCode, int intShopID)
		{
			return this.dal.GetGoodsNumberDelServe(strGoodsCode, intShopID);
		}

		public int UpdataGoodsNumber(Chain.Model.GoodsNumber model)
		{
			return this.dal.UpdataGoodsNumber(model);
		}

		public void InitShopData()
		{
			this.dal.InitShopData();
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public int UpdateGoodsNumber(int goodsID, decimal goodsNumber, int shopID)
		{
			return this.dal.UpdateGoodsNumber(goodsID, goodsNumber, shopID);
		}

		public decimal GetNumber(int goodsID, int shopID)
		{
			return this.dal.GetNumber(goodsID, shopID);
		}

		public int GetGoodsCount(int ClassID, int ShopID)
		{
			return this.dal.GetGoodsCount(ClassID, ShopID);
		}

		public int SyncGoods(int GoodsID)
		{
			return this.dal.SyncGoods(GoodsID);
		}

		public int SyncGoods(int GoodsID, int ShopID)
		{
			return this.dal.SyncGoods(GoodsID, ShopID);
		}

		public int SyncGoods(int GoodsID, List<int> ShopList)
		{
			return this.dal.SyncGoods(GoodsID, ShopList);
		}

		public DataSet GetAllGoodsidByShopID(int ShopID)
		{
			return this.dal.GetAllGoodsidByShopID(ShopID);
		}

		public bool ChenkOutShopGoodsNumber(int OutShopID, int GoodsID, decimal OutNumber)
		{
			return this.dal.ChenkOutShopGoodsNumber(OutShopID, GoodsID, OutNumber);
		}

		public bool UPdataOutShopGoodsNumber(int OutShopID, int GoodsID, decimal OutNumber)
		{
			return this.dal.UPdataOutShopGoodsNumber(OutShopID, GoodsID, OutNumber);
		}

		public bool UPdataInShopGoodsNumber(int OutShopID, int GoodsID, int OutNumber)
		{
			return this.dal.UPdataInShopGoodsNumber(OutShopID, GoodsID, OutNumber);
		}

		public DataSet GetShopIDListByGoods(int GoodsID)
		{
			return this.dal.GetShopIDListByGoods(GoodsID);
		}

		public bool DeleteNumber(int GoodsID, int ShopID)
		{
			return this.dal.DeleteNumber(GoodsID, ShopID);
		}

		public bool DeleteNumber(int ShopID)
		{
			return this.dal.DeleteNumber(ShopID);
		}
	}
}
