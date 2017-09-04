using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Staff
	{
		private readonly Chain.IDAL.Staff dal = new Chain.IDAL.Staff();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int StaffID)
		{
			return this.dal.Exists(StaffID);
		}

		public bool Exists(int intStaffID, string strStaffNumber)
		{
			return this.dal.Exists(intStaffID, strStaffNumber);
		}

		public int Add(Chain.Model.Staff model)
		{
			int result;
			if (this.dal.Exists(model.StaffID, model.StaffNumber))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.Staff model)
		{
			int result;
			if (this.dal.Exists(model.StaffID, model.StaffNumber))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public bool Delete(int StaffID)
		{
			return this.dal.Delete(StaffID);
		}

		public bool DeleteList(string StaffIDlist)
		{
			return this.dal.DeleteList(StaffIDlist);
		}

		public Chain.Model.Staff GetModel(int StaffID)
		{
			return this.dal.GetModel(StaffID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.Staff> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Staff> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Staff> modelList = new List<Chain.Model.Staff>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Staff model = new Chain.Model.Staff();
					if (dt.Rows[i]["StaffID"] != null && dt.Rows[i]["StaffID"].ToString() != "")
					{
						model.StaffID = int.Parse(dt.Rows[i]["StaffID"].ToString());
					}
					if (dt.Rows[i]["StaffNumber"] != null && dt.Rows[i]["StaffNumber"].ToString() != "")
					{
						model.StaffNumber = dt.Rows[i]["StaffNumber"].ToString();
					}
					if (dt.Rows[i]["StaffName"] != null && dt.Rows[i]["StaffName"].ToString() != "")
					{
						model.StaffName = dt.Rows[i]["StaffName"].ToString();
					}
					if (dt.Rows[i]["StaffSex"] != null && dt.Rows[i]["StaffSex"].ToString() != "")
					{
						if (dt.Rows[i]["StaffSex"].ToString() == "1" || dt.Rows[i]["StaffSex"].ToString().ToLower() == "true")
						{
							model.StaffSex = true;
						}
						else
						{
							model.StaffSex = false;
						}
					}
					if (dt.Rows[i]["StaffMobile"] != null && dt.Rows[i]["StaffMobile"].ToString() != "")
					{
						model.StaffMobile = dt.Rows[i]["StaffMobile"].ToString();
					}
					if (dt.Rows[i]["StaffAddress"] != null && dt.Rows[i]["StaffAddress"].ToString() != "")
					{
						model.StaffAddress = dt.Rows[i]["StaffAddress"].ToString();
					}
					if (dt.Rows[i]["StaffClassID"] != null && dt.Rows[i]["StaffClassID"].ToString() != "")
					{
						model.StaffClassID = int.Parse(dt.Rows[i]["StaffClassID"].ToString());
					}
					if (dt.Rows[i]["StaffRemark"] != null && dt.Rows[i]["StaffRemark"].ToString() != "")
					{
						model.StaffRemark = dt.Rows[i]["StaffRemark"].ToString();
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, string strTime, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strTime, strWhere);
		}
	}
}
