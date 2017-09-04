using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysGroup
	{
		private readonly Chain.IDAL.SysGroup dal = new Chain.IDAL.SysGroup();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int GroupID)
		{
			return this.dal.Exists(GroupID);
		}

		public int Add(Chain.Model.SysGroup model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysGroup model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int GroupID)
		{
			return this.dal.Delete(GroupID);
		}

		public bool DeleteList(string GroupIDlist)
		{
			return this.dal.DeleteList(GroupIDlist);
		}

		public Chain.Model.SysGroup GetModelbyGroupType(int GroupID, int GroupType)
		{
			return this.dal.GetModelbyGroupType(GroupID, GroupType);
		}

		public Chain.Model.SysGroup GetModel(int GroupID)
		{
			return this.dal.GetModel(GroupID);
		}

		public Chain.Model.SysGroup GetModelByGroupType(int GroupType)
		{
			return this.dal.GetModelByGroupType(GroupType);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysGroup> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysGroup> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysGroup> modelList = new List<Chain.Model.SysGroup>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysGroup model = new Chain.Model.SysGroup();
					if (dt.Rows[i]["GroupID"] != null && dt.Rows[i]["GroupID"].ToString() != "")
					{
						model.GroupID = int.Parse(dt.Rows[i]["GroupID"].ToString());
					}
					if (dt.Rows[i]["GroupName"] != null && dt.Rows[i]["GroupName"].ToString() != "")
					{
						model.GroupName = dt.Rows[i]["GroupName"].ToString();
					}
					if (dt.Rows[i]["GroupRemark"] != null && dt.Rows[i]["GroupRemark"].ToString() != "")
					{
						model.GroupRemark = dt.Rows[i]["GroupRemark"].ToString();
					}
					if (dt.Rows[i]["ParentGroupID"] != null && dt.Rows[i]["ParentGroupID"].ToString() != "")
					{
						model.ParentGroupID = int.Parse(dt.Rows[i]["ParentGroupID"].ToString());
					}
					if (dt.Rows[i]["ParentIDStr"] != null && dt.Rows[i]["ParentIDStr"].ToString() != "")
					{
						model.ParentIDStr = dt.Rows[i]["ParentIDStr"].ToString();
					}
					if (dt.Rows[i]["IsPublic"] != null && dt.Rows[i]["IsPublic"].ToString() != "")
					{
						if (dt.Rows[i]["IsPublic"].ToString() == "1" || dt.Rows[i]["IsPublic"].ToString().ToLower() == "true")
						{
							model.IsPublic = true;
						}
						else
						{
							model.IsPublic = false;
						}
					}
					if (dt.Rows[i]["CreateUserID"] != null && dt.Rows[i]["CreateUserID"].ToString() != "")
					{
						model.CreateUserID = int.Parse(dt.Rows[i]["CreateUserID"].ToString());
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

		public DataSet GetListS(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListS(PageSize, PageIndex, strWhere, out resCount);
		}

		public DataSet GetGroupAuthority(string strWhere)
		{
			return this.dal.GetGroupAuthority(strWhere);
		}

		public DataSet GetGroupAuthority(int GroupID)
		{
			return this.dal.GetGroupAuthority(GroupID);
		}

		public DataTable BuildGroupTree(DataTable dt, int rootGroupID, int depth, ref DataTable dtTree)
		{
			DataRow[] dr = dt.Select("ParentGroupID=" + rootGroupID);
			for (int i = 0; i < dr.Length; i++)
			{
				string strLeft = "|---".PadLeft(depth * 2 + 4, '\u3000');
				dr[i]["GroupName"] = strLeft + dr[i]["GroupName"].ToString();
				dtTree.Rows.Add(dr[i].ItemArray);
				this.BuildGroupTree(dt, int.Parse(dr[i]["GroupID"].ToString()), depth + 1, ref dtTree);
			}
			return dtTree;
		}

		public int DeleteGroup(Chain.Model.SysGroup model)
		{
			int result;
			if (model == null || model.GroupID == 0)
			{
				result = -1;
			}
			else
			{
				SysGroupAuthority bllSGA = new SysGroupAuthority();
				this.UpdateParentId(model.GroupID, model.ParentGroupID);
				this.Delete(model.GroupID);
				bllSGA.DeleteList(model.GroupID);
				result = 1;
			}
			return result;
		}

		public bool UpdateParentId(int OldParentGroupID, int NewParentGroupID)
		{
			return this.dal.UpdateParentId(OldParentGroupID, NewParentGroupID);
		}

		public DataTable GetSysGroupByID(int groupID)
		{
			return this.dal.GetSysGroupByID(groupID);
		}

		public DataTable GetSysGroupByParentID(int groupID)
		{
			return this.dal.GetSysGroupByParentID(groupID);
		}
	}
}
