using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Message
	{
		private readonly Chain.IDAL.Message dal = new Chain.IDAL.Message();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int MessageID)
		{
			return this.dal.Exists(MessageID);
		}

		public int Add(Chain.Model.Message model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.Message model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MessageID)
		{
			return this.dal.Delete(MessageID);
		}

		public bool DeleteList(string MessageIDlist)
		{
			return this.dal.DeleteList(MessageIDlist);
		}

		public Chain.Model.Message GetModel(int MessageID)
		{
			return this.dal.GetModel(MessageID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetListSPInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSPInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetMemMessageList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetMemMessageList(PageSize, PageIndex, out resCount, strWhere);
		}

		public List<Chain.Model.Message> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Message> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Message> modelList = new List<Chain.Model.Message>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Message model = new Chain.Model.Message();
					if (dt.Rows[i]["MessageID"] != null && dt.Rows[i]["MessageID"].ToString() != "")
					{
						model.MessageID = int.Parse(dt.Rows[i]["MessageID"].ToString());
					}
					if (dt.Rows[i]["MessageMemID"] != null && dt.Rows[i]["MessageMemID"].ToString() != "")
					{
						model.MessageMemID = int.Parse(dt.Rows[i]["MessageMemID"].ToString());
					}
					if (dt.Rows[i]["MessageContent"] != null && dt.Rows[i]["MessageContent"].ToString() != "")
					{
						model.MessageContent = dt.Rows[i]["MessageContent"].ToString();
					}
					if (dt.Rows[i]["MessageTime"] != null && dt.Rows[i]["MessageTime"].ToString() != "")
					{
						model.MessageTime = DateTime.Parse(dt.Rows[i]["MessageTime"].ToString());
					}
					if (dt.Rows[i]["MessageIsReply"] != null && dt.Rows[i]["MessageIsReply"].ToString() != "")
					{
						model.MessageIsReply = int.Parse(dt.Rows[i]["MessageIsReply"].ToString());
					}
					if (dt.Rows[i]["MessageReplyContent"] != null && dt.Rows[i]["MessageReplyContent"].ToString() != "")
					{
						model.MessageReplyContent = dt.Rows[i]["MessageReplyContent"].ToString();
					}
					if (dt.Rows[i]["MessageReplyTime"] != null && dt.Rows[i]["MessageReplyTime"].ToString() != "")
					{
						model.MessageReplyTime = DateTime.Parse(dt.Rows[i]["MessageReplyTime"].ToString());
					}
					if (dt.Rows[i]["MessageReplyUserID"] != null && dt.Rows[i]["MessageReplyUserID"].ToString() != "")
					{
						model.MessageReplyUserID = int.Parse(dt.Rows[i]["MessageReplyUserID"].ToString());
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

		public DataSet GetMessenger(string strWhere)
		{
			return this.dal.GetMessenger(strWhere);
		}

		public int MessageDel(int intMemID)
		{
			return this.dal.MessageDel(intMemID);
		}

		public bool Reply(Chain.Model.Message modelMessage)
		{
			return this.dal.Reply(modelMessage);
		}
	}
}
