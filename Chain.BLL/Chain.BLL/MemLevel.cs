using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemLevel
	{
		private readonly Chain.DAL.MemLevel dal = new Chain.DAL.MemLevel();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public string GetNameByID(int LevelID)
		{
			return this.dal.GetNameByID(LevelID);
		}

		public bool Exists(int LevelID)
		{
			return this.dal.Exists(LevelID);
		}

		public bool Exists(int LevelID, int Point)
		{
			return this.dal.Exists(LevelID, Point);
		}

		public int Add(Chain.Model.MemLevel model)
		{
			int result;
			if (this.Exists(-1, model.LevelPoint))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.MemLevel model)
		{
			int result;
			if (this.Exists(model.LevelID, model.LevelPoint))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public int Delete(int LevelID)
		{
			return this.dal.Delete(LevelID);
		}

		public bool DeleteList(string LevelIDlist)
		{
			return this.dal.DeleteList(LevelIDlist);
		}

		public Chain.Model.MemLevel GetModel(int LevelID)
		{
			return this.dal.GetModel(LevelID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public List<Chain.Model.MemLevel> GetLevelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList1(ds.Tables[0]);
		}

		public DataSet GetLists(string strWhere)
		{
			return this.dal.GetLists(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemLevel> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetLists(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemLevel> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemLevel> modelList = new List<Chain.Model.MemLevel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemLevel model = new Chain.Model.MemLevel();
					if (dt.Rows[i]["LevelID"] != null && dt.Rows[i]["LevelID"].ToString() != "")
					{
						model.LevelID = int.Parse(dt.Rows[i]["LevelID"].ToString());
					}
					if (dt.Rows[i]["LevelName"] != null && dt.Rows[i]["LevelName"].ToString() != "")
					{
						model.LevelName = dt.Rows[i]["LevelName"].ToString();
					}
					if (dt.Rows[i]["LevelPoint"] != null && dt.Rows[i]["LevelPoint"].ToString() != "")
					{
						model.LevelPoint = int.Parse(dt.Rows[i]["LevelPoint"].ToString());
					}
					if (dt.Rows[i]["ClassDiscountPercent"] != null && dt.Rows[i]["ClassDiscountPercent"].ToString() != "")
					{
						model.LevelDiscountPercent = Convert.ToDecimal(dt.Rows[i]["ClassDiscountPercent"]);
					}
					if (dt.Rows[i]["ClassPointPercent"] != null && dt.Rows[i]["ClassPointPercent"].ToString() != "")
					{
						model.LevelPointPercent = Convert.ToDecimal(dt.Rows[i]["ClassPointPercent"]);
					}
					if (dt.Rows[i]["ClassRechargePointRate"] != null && dt.Rows[i]["ClassRechargePointRate"].ToString() != "")
					{
						model.LevelRechargePointRate = Convert.ToInt32(dt.Rows[i]["ClassRechargePointRate"]);
					}
					if (dt.Rows[i]["LevellLock"] != null && dt.Rows[i]["LevellLock"].ToString() != "")
					{
						if (dt.Rows[i]["LevellLock"].ToString() == "1" || dt.Rows[i]["LevellLock"].ToString().ToLower() == "true")
						{
							model.LevellLock = true;
						}
						else
						{
							model.LevellLock = false;
						}
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		public List<Chain.Model.MemLevel> DataTableToList1(DataTable dt)
		{
			List<Chain.Model.MemLevel> modelList = new List<Chain.Model.MemLevel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemLevel model = new Chain.Model.MemLevel();
					if (dt.Rows[i]["LevelID"] != null && dt.Rows[i]["LevelID"].ToString() != "")
					{
						model.LevelID = int.Parse(dt.Rows[i]["LevelID"].ToString());
					}
					if (dt.Rows[i]["LevelName"] != null && dt.Rows[i]["LevelName"].ToString() != "")
					{
						model.LevelName = dt.Rows[i]["LevelName"].ToString();
					}
					if (dt.Rows[i]["LevelPoint"] != null && dt.Rows[i]["LevelPoint"].ToString() != "")
					{
						model.LevelPoint = int.Parse(dt.Rows[i]["LevelPoint"].ToString());
					}
					if (dt.Rows[i]["LevelDiscountPercent"] != null && dt.Rows[i]["LevelDiscountPercent"].ToString() != "")
					{
						model.LevelDiscountPercent = Convert.ToDecimal(dt.Rows[i]["LevelDiscountPercent"]);
					}
					if (dt.Rows[i]["LevelPointPercent"] != null && dt.Rows[i]["LevelPointPercent"].ToString() != "")
					{
						model.LevelPointPercent = Convert.ToDecimal(dt.Rows[i]["LevelPointPercent"]);
					}
					if (dt.Rows[i]["LevelRechargePointRate"] != null && dt.Rows[i]["LevelRechargePointRate"].ToString() != "")
					{
						model.LevelRechargePointRate = Convert.ToInt32(dt.Rows[i]["LevelRechargePointRate"]);
					}
					if (dt.Rows[i]["LevellLock"] != null && dt.Rows[i]["LevellLock"].ToString() != "")
					{
						if (dt.Rows[i]["LevellLock"].ToString() == "1" || dt.Rows[i]["LevellLock"].ToString().ToLower() == "true")
						{
							model.LevellLock = true;
						}
						else
						{
							model.LevellLock = false;
						}
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
