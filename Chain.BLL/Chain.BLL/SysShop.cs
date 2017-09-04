using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysShop
	{
		private readonly Chain.IDAL.SysShop dal = new Chain.IDAL.SysShop();

		public bool Exists(int ShopID)
		{
			return this.dal.Exists(ShopID);
		}

		public DataSet GetListShop(int PageSize, int PageIndex, out int resCount, string strsql, params string[] strWhere)
		{
			return this.dal.GetListShop(PageSize, PageIndex, out resCount, strsql, strWhere);
		}

		public DataSet GetAllianceListShop(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetAllianceListShop(PageSize, PageIndex, out resCount, strWhere);
		}

		public int Update(Chain.Model.SysShop model)
		{
			int result;
			if (this.Exists(model.ShopName, model.ShopID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public bool Delete(int ShopID)
		{
			return this.dal.Delete(ShopID);
		}

		public string GetShopNamebyShopID(int ShopID)
		{
			return this.dal.GetShopNamebyShopID(ShopID);
		}

		public bool DeleteList(string ShopIDlist)
		{
			return this.dal.DeleteList(ShopIDlist);
		}

		public int GetShopIDByWhere(string strWhere)
		{
			return this.dal.GetShopIDByWhere(strWhere);
		}

		public Chain.Model.SysShop GetModel(int ShopID)
		{
			return this.dal.GetModel(ShopID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysShop> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysShop> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysShop> modelList = new List<Chain.Model.SysShop>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysShop model = new Chain.Model.SysShop();
					if (dt.Rows[i]["ShopID"] != null && dt.Rows[i]["ShopID"].ToString() != "")
					{
						model.ShopID = int.Parse(dt.Rows[i]["ShopID"].ToString());
					}
					if (dt.Rows[i]["ShopName"] != null && dt.Rows[i]["ShopName"].ToString() != "")
					{
						model.ShopName = dt.Rows[i]["ShopName"].ToString();
					}
					if (dt.Rows[i]["ShopTelephone"] != null && dt.Rows[i]["ShopTelephone"].ToString() != "")
					{
						model.ShopTelephone = dt.Rows[i]["ShopTelephone"].ToString();
					}
					if (dt.Rows[i]["ShopContactMan"] != null && dt.Rows[i]["ShopContactMan"].ToString() != "")
					{
						model.ShopContactMan = dt.Rows[i]["ShopContactMan"].ToString();
					}
					if (dt.Rows[i]["ShopAreaID"] != null && dt.Rows[i]["ShopAreaID"].ToString() != "")
					{
						model.ShopAreaID = int.Parse(dt.Rows[i]["ShopAreaID"].ToString());
					}
					if (dt.Rows[i]["ShopAddress"] != null && dt.Rows[i]["ShopAddress"].ToString() != "")
					{
						model.ShopAddress = dt.Rows[i]["ShopAddress"].ToString();
					}
					if (dt.Rows[i]["ShopRemark"] != null && dt.Rows[i]["ShopRemark"].ToString() != "")
					{
						model.ShopRemark = dt.Rows[i]["ShopRemark"].ToString();
					}
					if (dt.Rows[i]["ShopCreateTime"] != null && dt.Rows[i]["ShopCreateTime"].ToString() != "")
					{
						model.ShopCreateTime = DateTime.Parse(dt.Rows[i]["ShopCreateTime"].ToString());
					}
					if (dt.Rows[i]["ShopState"] != null && dt.Rows[i]["ShopState"].ToString() != "")
					{
						if (dt.Rows[i]["ShopState"].ToString() == "1" || dt.Rows[i]["ShopState"].ToString().ToLower() == "true")
						{
							model.ShopState = true;
						}
						else
						{
							model.ShopState = false;
						}
					}
					if (dt.Rows[i]["ShopPrintTitle"] != null && dt.Rows[i]["ShopPrintTitle"].ToString() != "")
					{
						model.ShopPrintTitle = dt.Rows[i]["ShopPrintTitle"].ToString();
					}
					if (dt.Rows[i]["ShopPrintFoot"] != null && dt.Rows[i]["ShopPrintFoot"].ToString() != "")
					{
						model.ShopPrintFoot = dt.Rows[i]["ShopPrintFoot"].ToString();
					}
					if (dt.Rows[i]["ShopSmsName"] != null && dt.Rows[i]["ShopSmsName"].ToString() != "")
					{
						model.ShopSmsName = dt.Rows[i]["ShopSmsName"].ToString();
					}
					if (dt.Rows[i]["SettlementInterval"] != null && dt.Rows[i]["SettlementInterval"].ToString() != "")
					{
						model.SettlementInterval = int.Parse(dt.Rows[i]["SettlementInterval"].ToString());
					}
					if (dt.Rows[i]["ShopProportion"] != null && dt.Rows[i]["ShopProportion"].ToString() != "")
					{
						model.ShopProportion = decimal.Parse(dt.Rows[i]["ShopProportion"].ToString());
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

		public int Update(int shopid, int smsType, int pointType)
		{
			return this.dal.Update(shopid, smsType, pointType);
		}

		public int GetShopPointByShopid(int shopid, int type)
		{
			return this.dal.GetShopPointByShopid(shopid, type);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(string shopName)
		{
			return this.dal.Exists(shopName);
		}

		public bool Exists(string shopName, int shopID)
		{
			return this.dal.Exists(shopName, shopID);
		}

		public int Add(Chain.Model.SysShop model)
		{
			int result;
			if (this.Exists(model.ShopName))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public DataSet GetListS(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListS(PageSize, PageIndex, strWhere, out resCount);
		}

		public string GetShopNameByShopid(string shopid)
		{
			return this.dal.GetShopNameByShopid(shopid);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, strWhere, out resCount);
		}

		public DataSet GetShop(params string[] strWhere)
		{
			return this.dal.GetShop(strWhere);
		}

		public DataSet getTotalShop(string dtStartTime, string dtEndTime, params string[] strWhere)
		{
			return this.dal.getTotalShop(dtStartTime, dtEndTime, strWhere);
		}

		public DataSet GetTotalRptData(string memWhere, string rechargeWhere, string orderWhere, string countWhere, string memstoragetimingWhere, string drawmoneyWhere)
		{
			return this.dal.GetTotalRptData(memWhere, rechargeWhere, orderWhere, countWhere, memstoragetimingWhere, drawmoneyWhere);
		}

		public DataTable GetAllianceByCard(string strWhere, int shopID)
		{
			return this.dal.GetAllianceByCard(strWhere, shopID);
		}
	}
}
