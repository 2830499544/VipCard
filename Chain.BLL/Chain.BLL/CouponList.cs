using Chain.DAL;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class CouponList
	{
		private readonly Chain.DAL.CouponList dal = new Chain.DAL.CouponList();

		public int GetMemCouponID(int CouPonID)
		{
			return this.dal.GetMemCouponID(CouPonID);
		}

		public bool SendCoupon(int MemID, int CID)
		{
			return this.dal.SendCoupon(MemID, CID);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public DataSet GetCouponDetailNew(string strWhere)
		{
			return this.dal.GetCouponDetailNew(strWhere);
		}

		public bool Exists(int CID)
		{
			return this.dal.Exists(CID);
		}

		public int Add(Chain.Model.CouponList model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.CouponList model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int CID)
		{
			return this.dal.Delete(CID);
		}

		public bool DeleteList(string CIDlist)
		{
			return this.dal.DeleteList(CIDlist);
		}

		public Chain.Model.CouponList GetModel(string CouPon)
		{
			Chain.Model.CouponList clist = this.dal.GetModel(CouPon);
			Coupon coupon = new Coupon();
			Chain.Model.Coupon model = coupon.GetModel(Convert.ToInt32(clist.CouPonID));
			clist.CouponMinMoney = model.CouponMinMoney;
			clist.CouponNumber = model.CouponNumber;
			clist.CouponStart = model.CouponStart;
			clist.CouponEnd = model.CouponEnd;
			clist.CouponDayNum = model.CouponDayNum;
			clist.CouponType = model.CouponType;
			clist.CouponEffective = model.CouponEffective;
			return clist;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.CouponList> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.CouponList> DataTableToList(DataTable dt)
		{
			List<Chain.Model.CouponList> modelList = new List<Chain.Model.CouponList>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.CouponList model = this.dal.DataRowToModel(dt.Rows[i]);
					if (model != null)
					{
						modelList.Add(model);
					}
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, strWhere, out resCount);
		}

		public DataSet GetMemInfoListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetMemInfoListSP(PageSize, PageIndex, strWhere, out resCount);
		}

		public DataSet GetCouponDetail(string strWhere)
		{
			return this.dal.GetCouponDetail(strWhere);
		}

		public bool DataUpdateTran(ArrayList sqlArray)
		{
			return this.dal.DataUpdateTran(sqlArray);
		}
	}
}
