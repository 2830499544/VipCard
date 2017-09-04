using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class EmailLog
	{
		private readonly Chain.IDAL.EmailLog dal = new Chain.IDAL.EmailLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int EmailID)
		{
			return this.dal.Exists(EmailID);
		}

		public int Add(Chain.Model.EmailLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.EmailLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int EmailID)
		{
			return this.dal.Delete(EmailID);
		}

		public bool DeleteList(string EmailIDlist)
		{
			return this.dal.DeleteList(EmailIDlist);
		}

		public Chain.Model.EmailLog GetModel(int EmailID)
		{
			return this.dal.GetModel(EmailID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.EmailLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.EmailLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.EmailLog> modelList = new List<Chain.Model.EmailLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.EmailLog model = new Chain.Model.EmailLog();
					if (dt.Rows[i]["EmailID"] != null && dt.Rows[i]["EmailID"].ToString() != "")
					{
						model.EmailID = int.Parse(dt.Rows[i]["EmailID"].ToString());
					}
					if (dt.Rows[i]["EmailAdress"] != null && dt.Rows[i]["EmailAdress"].ToString() != "")
					{
						model.EmailAdress = dt.Rows[i]["EmailAdress"].ToString();
					}
					if (dt.Rows[i]["EmailTitle"] != null && dt.Rows[i]["EmailTitle"].ToString() != "")
					{
						model.EmailTitle = dt.Rows[i]["EmailTitle"].ToString();
					}
					if (dt.Rows[i]["EmailContent"] != null && dt.Rows[i]["EmailContent"].ToString() != "")
					{
						model.EmailContent = dt.Rows[i]["EmailContent"].ToString();
					}
					if (dt.Rows[i]["EmailState"] != null && dt.Rows[i]["EmailState"].ToString() != "")
					{
						model.EmailState = int.Parse(dt.Rows[i]["EmailState"].ToString());
					}
					if (dt.Rows[i]["EmailSendTime"] != null && dt.Rows[i]["EmailSendTime"].ToString() != "")
					{
						model.EmailSendTime = DateTime.Parse(dt.Rows[i]["EmailSendTime"].ToString());
					}
					if (dt.Rows[i]["EmailShopID"] != null && dt.Rows[i]["EmailShopID"].ToString() != "")
					{
						model.EmailShopID = int.Parse(dt.Rows[i]["EmailShopID"].ToString());
					}
					if (dt.Rows[i]["EmailUserID"] != null && dt.Rows[i]["EmailUserID"].ToString() != "")
					{
						model.EmailUserID = int.Parse(dt.Rows[i]["EmailUserID"].ToString());
					}
					if (dt.Rows[i]["EmailCount"] != null && dt.Rows[i]["EmailCount"].ToString() != "")
					{
						model.EmailCount = int.Parse(dt.Rows[i]["EmailCount"].ToString());
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

		public int UpdateEmail(int intID, int state)
		{
			return this.dal.UpdateEmail(intID, state);
		}

		public int UpdateEmailCount(int intID)
		{
			return this.dal.UpdateEmailCount(intID);
		}

		public int EmailResend(int intEamilID)
		{
			return this.dal.EmailResend(intEamilID);
		}
	}
}
