using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class OnlineMessage
	{
		private readonly Chain.DAL.OnlineMessage dal = new Chain.DAL.OnlineMessage();

		public bool Exists(int MessageID)
		{
			return this.dal.Exists(MessageID);
		}

		public DataSet GetProposalInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetProposalInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public int Add(Chain.Model.OnlineMessage model)
		{
			return this.dal.Add(model);
		}

		public bool Reply(Chain.Model.OnlineMessage modelMessage)
		{
			return this.dal.Reply(modelMessage);
		}

		public bool Update(Chain.Model.OnlineMessage model)
		{
			return this.dal.Update(model);
		}

		public bool UpdateShowStatus(string MemCard)
		{
			return this.dal.UpdateShowStatus(MemCard);
		}

		public bool Delete(int MessageID)
		{
			return this.dal.Delete(MessageID);
		}

		public bool DeleteList(string MessageIDlist)
		{
			return this.dal.DeleteList(MessageIDlist);
		}

		public Chain.Model.OnlineMessage GetModel(int MessageID)
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

		public List<Chain.Model.OnlineMessage> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.OnlineMessage> DataTableToList(DataTable dt)
		{
			List<Chain.Model.OnlineMessage> modelList = new List<Chain.Model.OnlineMessage>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.OnlineMessage model = new Chain.Model.OnlineMessage();
					if (dt.Rows[i]["MessageID"] != null && dt.Rows[i]["MessageID"].ToString() != "")
					{
						model.MessageID = int.Parse(dt.Rows[i]["MessageID"].ToString());
					}
					if (dt.Rows[i]["MessageContent"] != null && dt.Rows[i]["MessageContent"].ToString() != "")
					{
						model.MessageContent = dt.Rows[i]["MessageContent"].ToString();
					}
					if (dt.Rows[i]["MemID"] != null && dt.Rows[i]["MemID"].ToString() != "")
					{
						model.MemID = new int?(int.Parse(dt.Rows[i]["MemID"].ToString()));
					}
					if (dt.Rows[i]["MemCard"] != null && dt.Rows[i]["MemCard"].ToString() != "")
					{
						model.MemCard = dt.Rows[i]["MemCard"].ToString();
					}
					if (dt.Rows[i]["MessageTime"] != null && dt.Rows[i]["MessageTime"].ToString() != "")
					{
						model.MessageTime = new DateTime?(DateTime.Parse(dt.Rows[i]["MessageTime"].ToString()));
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
	}
}
