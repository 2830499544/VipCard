using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysUserWork
	{
		private readonly Chain.DAL.SysUserWork dal = new Chain.DAL.SysUserWork();

		public bool Exists(int SysUserWorkID)
		{
			return this.dal.Exists(SysUserWorkID);
		}

		public int Add(Chain.Model.SysUserWork model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysUserWork model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int SysUserWorkID)
		{
			return this.dal.Delete(SysUserWorkID);
		}

		public bool DeleteList(string SysUserWorkIDlist)
		{
			return this.dal.DeleteList(SysUserWorkIDlist);
		}

		public Chain.Model.SysUserWork GetModel(int SysUserWorkID)
		{
			return this.dal.GetModel(SysUserWorkID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysUserWork> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysUserWork> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysUserWork> modelList = new List<Chain.Model.SysUserWork>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysUserWork model = new Chain.Model.SysUserWork();
					if (dt.Rows[i]["SysUserWorkID"] != null && dt.Rows[i]["SysUserWorkID"].ToString() != "")
					{
						model.SysUserWorkID = int.Parse(dt.Rows[i]["SysUserWorkID"].ToString());
					}
					if (dt.Rows[i]["UserID"] != null && dt.Rows[i]["UserID"].ToString() != "")
					{
						model.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
					}
					if (dt.Rows[i]["StartTime"] != null && dt.Rows[i]["StartTime"].ToString() != "")
					{
						model.StartTime = DateTime.Parse(dt.Rows[i]["StartTime"].ToString());
					}
					if (dt.Rows[i]["EedTime"] != null && dt.Rows[i]["EedTime"].ToString() != "")
					{
						model.EedTime = DateTime.Parse(dt.Rows[i]["EedTime"].ToString());
					}
					if (dt.Rows[i]["AddNewUser"] != null && dt.Rows[i]["AddNewUser"].ToString() != "")
					{
						model.AddNewUser = int.Parse(dt.Rows[i]["AddNewUser"].ToString());
					}
					if (dt.Rows[i]["CardMoney"] != null && dt.Rows[i]["CardMoney"].ToString() != "")
					{
						model.CardMoney = decimal.Parse(dt.Rows[i]["CardMoney"].ToString());
					}
					if (dt.Rows[i]["ExpenseSumMoneys"] != null && dt.Rows[i]["ExpenseSumMoneys"].ToString() != "")
					{
						model.ExpenseSumMoneys = decimal.Parse(dt.Rows[i]["ExpenseSumMoneys"].ToString());
					}
					if (dt.Rows[i]["ExpenseBinkMoneys"] != null && dt.Rows[i]["ExpenseBinkMoneys"].ToString() != "")
					{
						model.ExpenseBinkMoneys = decimal.Parse(dt.Rows[i]["ExpenseBinkMoneys"].ToString());
					}
					if (dt.Rows[i]["ExpenseCouponMoneys"] != null && dt.Rows[i]["ExpenseCouponMoneys"].ToString() != "")
					{
						model.ExpenseCouponMoneys = decimal.Parse(dt.Rows[i]["ExpenseCouponMoneys"].ToString());
					}
					if (dt.Rows[i]["SRechargeMoney"] != null && dt.Rows[i]["SRechargeMoney"].ToString() != "")
					{
						model.SRechargeMoney = decimal.Parse(dt.Rows[i]["SRechargeMoney"].ToString());
					}
					if (dt.Rows[i]["FRechargeMoney"] != null && dt.Rows[i]["FRechargeMoney"].ToString() != "")
					{
						model.FRechargeMoney = decimal.Parse(dt.Rows[i]["FRechargeMoney"].ToString());
					}
					if (dt.Rows[i]["RechargeBank"] != null && dt.Rows[i]["RechargeBank"].ToString() != "")
					{
						model.RechargeBank = decimal.Parse(dt.Rows[i]["RechargeBank"].ToString());
					}
					if (dt.Rows[i]["FRechargeGiveMoney"] != null && dt.Rows[i]["FRechargeGiveMoney"].ToString() != "")
					{
						model.FRechargeGiveMoney = decimal.Parse(dt.Rows[i]["FRechargeGiveMoney"].ToString());
					}
					if (dt.Rows[i]["AllMoneys"] != null && dt.Rows[i]["AllMoneys"].ToString() != "")
					{
						model.AllMoneys = decimal.Parse(dt.Rows[i]["AllMoneys"].ToString());
					}
					if (dt.Rows[i]["sjMoneys"] != null && dt.Rows[i]["sjMoneys"].ToString() != "")
					{
						model.sjMoneys = decimal.Parse(dt.Rows[i]["sjMoneys"].ToString());
					}
					if (dt.Rows[i]["HandoverUserID"] != null && dt.Rows[i]["HandoverUserID"].ToString() != "")
					{
						model.HandoverUserID = int.Parse(dt.Rows[i]["HandoverUserID"].ToString());
					}
					if (dt.Rows[i]["Arrearage"] != null && dt.Rows[i]["Arrearage"].ToString() != "")
					{
						model.Arrearage = decimal.Parse(dt.Rows[i]["Arrearage"].ToString());
					}
					if (dt.Rows[i]["Ispay"] != null && dt.Rows[i]["Ispay"].ToString() != "")
					{
						if (dt.Rows[i]["Ispay"].ToString() == "1" || dt.Rows[i]["Ispay"].ToString().ToLower() == "true")
						{
							model.Ispay = true;
						}
						else
						{
							model.Ispay = false;
						}
					}
					if (dt.Rows[i]["remark"] != null && dt.Rows[i]["remark"].ToString() != "")
					{
						model.remark = dt.Rows[i]["remark"].ToString();
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
	}
}
