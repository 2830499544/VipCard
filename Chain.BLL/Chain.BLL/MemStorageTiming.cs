using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemStorageTiming
	{
		private readonly Chain.IDAL.MemStorageTiming dal = new Chain.IDAL.MemStorageTiming();

		public bool Exists(int StorageTimingID)
		{
			return this.dal.Exists(StorageTimingID);
		}

		public bool ExistsProject(int ProjectID)
		{
			return this.dal.ExistsProject(ProjectID);
		}

		public int Add(Chain.Model.MemStorageTiming model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemStorageTiming model)
		{
			return this.dal.Update(model);
		}

		public bool UpdateStorageResidueTime(int StorageTimingID, int StorageResidueTime)
		{
			return this.dal.UpdateStorageResidueTime(StorageTimingID, StorageResidueTime);
		}

		public bool Delete(int StorageTimingID)
		{
			return this.dal.Delete(StorageTimingID);
		}

		public bool DeleteList(string StorageTimingIDlist)
		{
			return this.dal.DeleteList(StorageTimingIDlist);
		}

		public Chain.Model.MemStorageTiming GetModel(int StorageTimingID)
		{
			return this.dal.GetModel(StorageTimingID);
		}

		public DataSet GetAllTimeByMem(int memid, int projectid)
		{
			return this.dal.GetAllTimeByMem(memid, projectid);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemStorageTiming> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemStorageTiming> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemStorageTiming> modelList = new List<Chain.Model.MemStorageTiming>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemStorageTiming model = new Chain.Model.MemStorageTiming();
					if (dt.Rows[i]["StorageTimingID"] != null && dt.Rows[i]["StorageTimingID"].ToString() != "")
					{
						model.StorageTimingID = int.Parse(dt.Rows[i]["StorageTimingID"].ToString());
					}
					if (dt.Rows[i]["StorageTimingMemID"] != null && dt.Rows[i]["StorageTimingMemID"].ToString() != "")
					{
						model.StorageTimingMemID = int.Parse(dt.Rows[i]["StorageTimingMemID"].ToString());
					}
					if (dt.Rows[i]["StorageTimingAccount"] != null && dt.Rows[i]["StorageTimingAccount"].ToString() != "")
					{
						model.StorageTimingAccount = dt.Rows[i]["StorageTimingAccount"].ToString();
					}
					if (dt.Rows[i]["StorageTimingTotalMoney"] != null && dt.Rows[i]["StorageTimingTotalMoney"].ToString() != "")
					{
						model.StorageTimingTotalMoney = decimal.Parse(dt.Rows[i]["StorageTimingTotalMoney"].ToString());
					}
					if (dt.Rows[i]["StorageTimingDiscountMoney"] != null && dt.Rows[i]["StorageTimingDiscountMoney"].ToString() != "")
					{
						model.StorageTimingDiscountMoney = decimal.Parse(dt.Rows[i]["StorageTimingDiscountMoney"].ToString());
					}
					if (dt.Rows[i]["StorageTimingIsCard"] != null && dt.Rows[i]["StorageTimingIsCard"].ToString() != "")
					{
						if (dt.Rows[i]["StorageTimingIsCard"].ToString() == "1" || dt.Rows[i]["StorageTimingIsCard"].ToString().ToLower() == "true")
						{
							model.StorageTimingIsCard = true;
						}
						else
						{
							model.StorageTimingIsCard = false;
						}
					}
					if (dt.Rows[i]["StorageTimingPayCard"] != null && dt.Rows[i]["StorageTimingPayCard"].ToString() != "")
					{
						model.StorageTimingPayCard = decimal.Parse(dt.Rows[i]["StorageTimingPayCard"].ToString());
					}
					if (dt.Rows[i]["StorageTimingIsCash"] != null && dt.Rows[i]["StorageTimingIsCash"].ToString() != "")
					{
						if (dt.Rows[i]["StorageTimingIsCash"].ToString() == "1" || dt.Rows[i]["StorageTimingIsCash"].ToString().ToLower() == "true")
						{
							model.StorageTimingIsCash = true;
						}
						else
						{
							model.StorageTimingIsCash = false;
						}
					}
					if (dt.Rows[i]["StorageTimingPayCash"] != null && dt.Rows[i]["StorageTimingPayCash"].ToString() != "")
					{
						model.StorageTimingPayCash = decimal.Parse(dt.Rows[i]["StorageTimingPayCash"].ToString());
					}
					if (dt.Rows[i]["StorageTimingIsBink"] != null && dt.Rows[i]["StorageTimingIsBink"].ToString() != "")
					{
						if (dt.Rows[i]["StorageTimingIsBink"].ToString() == "1" || dt.Rows[i]["StorageTimingIsBink"].ToString().ToLower() == "true")
						{
							model.StorageTimingIsBink = true;
						}
						else
						{
							model.StorageTimingIsBink = false;
						}
					}
					if (dt.Rows[i]["StorageTimingPayBink"] != null && dt.Rows[i]["StorageTimingPayBink"].ToString() != "")
					{
						model.StorageTimingPayBink = decimal.Parse(dt.Rows[i]["StorageTimingPayBink"].ToString());
					}
					if (dt.Rows[i]["StorageTimingPayCoupon"] != null && dt.Rows[i]["StorageTimingPayCoupon"].ToString() != "")
					{
						model.StorageTimingPayCoupon = decimal.Parse(dt.Rows[i]["StorageTimingPayCoupon"].ToString());
					}
					if (dt.Rows[i]["StorageTimingPayType"] != null && dt.Rows[i]["StorageTimingPayType"].ToString() != "")
					{
						model.StorageTimingPayType = int.Parse(dt.Rows[i]["StorageTimingPayType"].ToString());
					}
					if (dt.Rows[i]["StorageTimingPoint"] != null && dt.Rows[i]["StorageTimingPoint"].ToString() != "")
					{
						model.StorageTimingPoint = int.Parse(dt.Rows[i]["StorageTimingPoint"].ToString());
					}
					if (dt.Rows[i]["StorageTimingRemark"] != null && dt.Rows[i]["StorageTimingRemark"].ToString() != "")
					{
						model.StorageTimingRemark = dt.Rows[i]["StorageTimingRemark"].ToString();
					}
					if (dt.Rows[i]["StorageTimingShopID"] != null && dt.Rows[i]["StorageTimingShopID"].ToString() != "")
					{
						model.StorageTimingShopID = int.Parse(dt.Rows[i]["StorageTimingShopID"].ToString());
					}
					if (dt.Rows[i]["StorageTimingUserID"] != null && dt.Rows[i]["StorageTimingUserID"].ToString() != "")
					{
						model.StorageTimingUserID = int.Parse(dt.Rows[i]["StorageTimingUserID"].ToString());
					}
					if (dt.Rows[i]["StorageTimingCreateTime"] != null && dt.Rows[i]["StorageTimingCreateTime"].ToString() != "")
					{
						model.StorageTimingCreateTime = DateTime.Parse(dt.Rows[i]["StorageTimingCreateTime"].ToString());
					}
					if (dt.Rows[i]["StorageTotalTime"] != null && dt.Rows[i]["StorageTotalTime"].ToString() != "")
					{
						model.StorageTotalTime = int.Parse(dt.Rows[i]["StorageTotalTime"].ToString());
					}
					if (dt.Rows[i]["StorageResidueTime"] != null && dt.Rows[i]["StorageResidueTime"].ToString() != "")
					{
						model.StorageResidueTime = int.Parse(dt.Rows[i]["StorageResidueTime"].ToString());
					}
					if (dt.Rows[i]["StorageTimingProjectID"] != null && dt.Rows[i]["StorageTimingProjectID"].ToString() != "")
					{
						model.StorageTimingProjectID = int.Parse(dt.Rows[i]["StorageTimingProjectID"].ToString());
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

		public DataSet GetTimeTotal(string strWhere)
		{
			return this.dal.GetTimeTotal(strWhere);
		}
	}
}
