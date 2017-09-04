using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class StaffClass
	{
		private readonly Chain.IDAL.StaffClass dal = new Chain.IDAL.StaffClass();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ClassID)
		{
			return this.dal.Exists(ClassID);
		}

		public bool Exists(int intClassID, string strClassName, int ShopID)
		{
			return this.dal.Exists(intClassID, strClassName, ShopID);
		}

		public int Add(Chain.Model.StaffClass model)
		{
			int result;
			if (this.Exists(model.ClassID, model.ClassName, model.ClassShopID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.StaffClass model)
		{
			int result;
			if (this.Exists(model.ClassID, model.ClassName, model.ClassShopID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public int Delete(int ClassID)
		{
			return this.dal.Delete(ClassID);
		}

		public bool DeleteList(string ClassIDlist)
		{
			return this.dal.DeleteList(ClassIDlist);
		}

		public Chain.Model.StaffClass GetModel(int ClassID)
		{
			return this.dal.GetModel(ClassID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.StaffClass> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.StaffClass> DataTableToList(DataTable dt)
		{
			List<Chain.Model.StaffClass> modelList = new List<Chain.Model.StaffClass>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.StaffClass model = new Chain.Model.StaffClass();
					if (dt.Rows[i]["ClassID"] != null && dt.Rows[i]["ClassID"].ToString() != "")
					{
						model.ClassID = int.Parse(dt.Rows[i]["ClassID"].ToString());
					}
					if (dt.Rows[i]["ClassName"] != null && dt.Rows[i]["ClassName"].ToString() != "")
					{
						model.ClassName = dt.Rows[i]["ClassName"].ToString();
					}
					if (dt.Rows[i]["ClassType"] != null && dt.Rows[i]["ClassType"].ToString() != "")
					{
						if (dt.Rows[i]["ClassType"].ToString() == "1" || dt.Rows[i]["ClassType"].ToString().ToLower() == "true")
						{
							model.ClassType = true;
						}
						else
						{
							model.ClassType = false;
						}
					}
					if (dt.Rows[i]["ClassPercent"] != null && dt.Rows[i]["ClassPercent"].ToString() != "")
					{
						model.ClassPercent = decimal.Parse(dt.Rows[i]["ClassPercent"].ToString());
					}
					if (dt.Rows[i]["ClassShopID"] != null && dt.Rows[i]["ClassShopID"].ToString() != "")
					{
						model.ClassShopID = int.Parse(dt.Rows[i]["ClassShopID"].ToString());
					}
					if (dt.Rows[i]["ClassRemark"] != null && dt.Rows[i]["ClassRemark"].ToString() != "")
					{
						model.ClassRemark = dt.Rows[i]["ClassRemark"].ToString();
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
