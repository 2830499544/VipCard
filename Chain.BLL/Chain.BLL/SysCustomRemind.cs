using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysCustomRemind
	{
		private readonly Chain.IDAL.SysCustomRemind dal = new Chain.IDAL.SysCustomRemind();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int CustomRemindID)
		{
			return this.dal.Exists(CustomRemindID);
		}

		public int Add(Chain.Model.SysCustomRemind model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysCustomRemind model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int CustomRemindID)
		{
			return this.dal.Delete(CustomRemindID);
		}

		public bool DeleteList(string CustomRemindIDlist)
		{
			return this.dal.DeleteList(CustomRemindIDlist);
		}

		public Chain.Model.SysCustomRemind GetModel(int CustomRemindID)
		{
			return this.dal.GetModel(CustomRemindID);
		}

		public DataSet GetList(string strWhere, int count)
		{
			return this.dal.GetList(strWhere, count);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysCustomRemind> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysCustomRemind> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysCustomRemind> modelList = new List<Chain.Model.SysCustomRemind>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysCustomRemind model = new Chain.Model.SysCustomRemind();
					if (dt.Rows[i]["CustomRemindID"] != null && dt.Rows[i]["CustomRemindID"].ToString() != "")
					{
						model.CustomRemindID = int.Parse(dt.Rows[i]["CustomRemindID"].ToString());
					}
					if (dt.Rows[i]["CustomRemindTitle"] != null && dt.Rows[i]["CustomRemindTitle"].ToString() != "")
					{
						model.CustomRemindTitle = dt.Rows[i]["CustomRemindTitle"].ToString();
					}
					if (dt.Rows[i]["CustomRemindDetail"] != null && dt.Rows[i]["CustomRemindDetail"].ToString() != "")
					{
						model.CustomRemindDetail = dt.Rows[i]["CustomRemindDetail"].ToString();
					}
					if (dt.Rows[i]["CustomReminder"] != null && dt.Rows[i]["CustomReminder"].ToString() != "")
					{
						model.CustomReminder = dt.Rows[i]["CustomReminder"].ToString();
					}
					if (dt.Rows[i]["CustomRemindTime"] != null && dt.Rows[i]["CustomRemindTime"].ToString() != "")
					{
						model.CustomRemindTime = DateTime.Parse(dt.Rows[i]["CustomRemindTime"].ToString());
					}
					if (dt.Rows[i]["CustomRemindCreateTime"] != null && dt.Rows[i]["CustomRemindCreateTime"].ToString() != "")
					{
						model.CustomRemindCreateTime = DateTime.Parse(dt.Rows[i]["CustomRemindCreateTime"].ToString());
					}
					if (dt.Rows[i]["CustomRemindShopID"] != null && dt.Rows[i]["CustomRemindShopID"].ToString() != "")
					{
						model.CustomRemindShopID = int.Parse(dt.Rows[i]["CustomRemindShopID"].ToString());
					}
					if (dt.Rows[i]["CustomRemindUserID"] != null && dt.Rows[i]["CustomRemindUserID"].ToString() != "")
					{
						model.CustomRemindUserID = int.Parse(dt.Rows[i]["CustomRemindUserID"].ToString());
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

		public DataSet GetCustomField()
		{
			return this.dal.GetCustomField();
		}
	}
}
