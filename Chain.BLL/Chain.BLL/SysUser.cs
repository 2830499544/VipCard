using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysUser
	{
		private readonly Chain.IDAL.SysUser dal = new Chain.IDAL.SysUser();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public string GetUserNameByUserID(string userid)
		{
			return this.dal.GetUserNameByUserID(userid);
		}

		public bool Exists(string useraccount)
		{
			return this.dal.Exists(useraccount);
		}

		public bool ExiNumber(string usernumber)
		{
			return this.dal.ExiNumber(usernumber);
		}

		public bool Exists(string useraccount, string usernumber)
		{
			return this.dal.Exists(useraccount, usernumber);
		}

		public bool Exists(string useraccount, int userid)
		{
			return this.dal.Exists(useraccount, userid);
		}

		public int Add(Chain.Model.SysUser model)
		{
			int result;
			if (this.Exists(model.UserAccount))
			{
				result = -1;
			}
			else if (this.ExiNumber(model.UserNumber))
			{
				result = -4;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.SysUser model)
		{
			int result;
			if (this.Exists(model.UserAccount, model.UserID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public bool Delete(int UserID)
		{
			return this.dal.Delete(UserID);
		}

		public DataSet GetIsCanDelUser(int userid)
		{
			return this.dal.GetIsCanDelUser(userid);
		}

		public bool DeleteList(string UserIDlist)
		{
			return this.dal.DeleteList(UserIDlist);
		}

		public Chain.Model.SysUser GetModel(int UserID)
		{
			return this.dal.GetModel(UserID);
		}

		public Chain.Model.SysUser GetModel(string Account)
		{
			return this.dal.GetModel(Account);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysUser> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public Chain.Model.SysUser CheckUserLogin(string UserAccount, string Md5Pwd)
		{
			DataTable db = this.dal.CheckUserLogin(UserAccount, Md5Pwd);
			Chain.Model.SysUser result;
			if (db.Rows.Count > 0)
			{
				result = this.DataTableToList(db)[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public List<Chain.Model.SysUser> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysUser> modelList = new List<Chain.Model.SysUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysUser model = new Chain.Model.SysUser();
					if (dt.Rows[i]["UserID"] != null && dt.Rows[i]["UserID"].ToString() != "")
					{
						model.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
					}
					if (dt.Rows[i]["UserAccount"] != null && dt.Rows[i]["UserAccount"].ToString() != "")
					{
						model.UserAccount = dt.Rows[i]["UserAccount"].ToString();
					}
					if (dt.Rows[i]["UserName"] != null && dt.Rows[i]["UserName"].ToString() != "")
					{
						model.UserName = dt.Rows[i]["UserName"].ToString();
					}
					if (dt.Rows[i]["UserPassword"] != null && dt.Rows[i]["UserPassword"].ToString() != "")
					{
						model.UserPassword = dt.Rows[i]["UserPassword"].ToString();
					}
					if (dt.Rows[i]["UserShopID"] != null && dt.Rows[i]["UserShopID"].ToString() != "")
					{
						model.UserShopID = int.Parse(dt.Rows[i]["UserShopID"].ToString());
					}
					if (dt.Rows[i]["UserGroupID"] != null && dt.Rows[i]["UserGroupID"].ToString() != "")
					{
						model.UserGroupID = int.Parse(dt.Rows[i]["UserGroupID"].ToString());
					}
					if (dt.Rows[i]["UserLock"] != null && dt.Rows[i]["UserLock"].ToString() != "")
					{
						if (dt.Rows[i]["UserLock"].ToString() == "1" || dt.Rows[i]["UserLock"].ToString().ToLower() == "true")
						{
							model.UserLock = true;
						}
						else
						{
							model.UserLock = false;
						}
					}
					if (dt.Rows[i]["UserRemark"] != null && dt.Rows[i]["UserRemark"].ToString() != "")
					{
						model.UserPassword = dt.Rows[i]["UserRemark"].ToString();
					}
					if (dt.Rows[i]["UserCreateTime"] != null && dt.Rows[i]["UserCreateTime"].ToString() != "")
					{
						model.UserCreateTime = DateTime.Parse(dt.Rows[i]["UserCreateTime"].ToString());
					}
					if (dt.Rows[i]["UserTelephone"] != null && dt.Rows[i]["UserTelephone"].ToString() != "")
					{
						model.UserTelephone = dt.Rows[i]["UserTelephone"].ToString();
					}
					if (dt.Rows[i]["UserNumber"] != null && dt.Rows[i]["UserNumber"].ToString() != "")
					{
						model.UserNumber = dt.Rows[i]["UserNumber"].ToString();
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

		public DataSet GetList(int PageSize, int PageIndex, string[] where, out int resCount)
		{
			return this.dal.GetList(PageSize, PageIndex, where, out resCount);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public int ExistPwd(int UserID, string oldPwd)
		{
			return this.dal.ExistPwd(UserID, oldPwd);
		}

		public int UpdateUserPwd(int UserID, string oldPwd, string newPwd)
		{
			int exist = this.ExistPwd(UserID, oldPwd);
			int result;
			if (exist != 1)
			{
				result = exist;
			}
			else
			{
				result = this.dal.UpdateUserPwd(UserID, newPwd);
			}
			return result;
		}

		public int UpdateUserLock(int ShopID, int type)
		{
			return this.dal.UpdateUserLock(ShopID, type);
		}
	}
}
