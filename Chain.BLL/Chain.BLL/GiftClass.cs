using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GiftClass
	{
		private readonly Chain.IDAL.GiftClass dal = new Chain.IDAL.GiftClass();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int GiftClassID)
		{
			return this.dal.Exists(GiftClassID);
		}

		public bool Exists(string ClassName, int classID)
		{
			return this.dal.Exists(ClassName, classID);
		}

		public int Add(Chain.Model.GiftClass model)
		{
			int result;
			if (this.Exists(model.GiftClassName, model.GiftClassID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.GiftClass model)
		{
			int result;
			if (this.Exists(model.GiftClassName, model.GiftClassID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public bool Delete(int GiftClassID)
		{
			return this.dal.Delete(GiftClassID);
		}

		public bool DeleteList(string GiftClassIDlist)
		{
			return this.dal.DeleteList(GiftClassIDlist);
		}

		public Chain.Model.GiftClass GetModel(int GiftClassID)
		{
			return this.dal.GetModel(GiftClassID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.GiftClass> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GiftClass> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GiftClass> modelList = new List<Chain.Model.GiftClass>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GiftClass model = new Chain.Model.GiftClass();
					if (dt.Rows[i]["GiftClassID"] != null && dt.Rows[i]["GiftClassID"].ToString() != "")
					{
						model.GiftClassID = int.Parse(dt.Rows[i]["GiftClassID"].ToString());
					}
					if (dt.Rows[i]["GiftClassName"] != null && dt.Rows[i]["GiftClassName"].ToString() != "")
					{
						model.GiftClassName = dt.Rows[i]["GiftClassName"].ToString();
					}
					if (dt.Rows[i]["GiftClassRemark"] != null && dt.Rows[i]["GiftClassRemark"].ToString() != "")
					{
						model.GiftClassRemark = dt.Rows[i]["GiftClassRemark"].ToString();
					}
					if (dt.Rows[i]["GiftParentID"] != null && dt.Rows[i]["GiftParentID"].ToString() != "")
					{
						model.GiftParentID = int.Parse(dt.Rows[i]["GiftParentID"].ToString());
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

		public int DeleteByParentID(int ParentID)
		{
			return this.dal.DeleteByParentID(ParentID);
		}

		public DataTable GetItem(int ClassID)
		{
			return this.dal.GetItem(ClassID).Tables[0];
		}

		public void CreateTreeItem(DataTable dtSource, DataTable dt, int ParentClassID, int level)
		{
			if (dt.Columns.Count <= 0)
			{
				for (int i = 0; i < dtSource.Columns.Count; i++)
				{
					dt.Columns.Add(new DataColumn(dtSource.Columns[i].ColumnName));
				}
			}
			DataRow[] dr = dtSource.Select(" GiftParentID=" + ParentClassID);
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					string temp = dr[i][j].ToString();
					if (dt.Columns[j].ColumnName == "GiftClassName")
					{
						temp = new string('-', level * 3).ToString() + temp;
					}
					dt.Rows[dt.Rows.Count - 1][j] = temp;
				}
				int CurrentID = Convert.ToInt32(dr[i]["GiftClassID"].ToString());
				this.CreateTreeItem(dtSource, dt, CurrentID, level + 1);
			}
		}
	}
}
