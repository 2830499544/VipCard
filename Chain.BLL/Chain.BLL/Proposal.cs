using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Proposal
	{
		private readonly Chain.DAL.Proposal dal = new Chain.DAL.Proposal();

		public bool Exists(int ProposalID)
		{
			return this.dal.Exists(ProposalID);
		}

		public int Add(Chain.Model.Proposal model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.Proposal model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ProposalID)
		{
			return this.dal.Delete(ProposalID);
		}

		public bool DeleteList(string ProposalIDlist)
		{
			return this.dal.DeleteList(ProposalIDlist);
		}

		public Chain.Model.Proposal GetModel(int ProposalID)
		{
			return this.dal.GetModel(ProposalID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.Proposal> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Proposal> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Proposal> modelList = new List<Chain.Model.Proposal>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Proposal model = new Chain.Model.Proposal();
					if (dt.Rows[i]["ProposalID"] != null && dt.Rows[i]["ProposalID"].ToString() != "")
					{
						model.ProposalID = int.Parse(dt.Rows[i]["ProposalID"].ToString());
					}
					if (dt.Rows[i]["ProposalContent"] != null && dt.Rows[i]["ProposalContent"].ToString() != "")
					{
						model.ProposalContent = dt.Rows[i]["ProposalContent"].ToString();
					}
					if (dt.Rows[i]["MemID"] != null && dt.Rows[i]["MemID"].ToString() != "")
					{
						model.MemID = new int?(int.Parse(dt.Rows[i]["MemID"].ToString()));
					}
					if (dt.Rows[i]["MemMobile"] != null && dt.Rows[i]["MemMobile"].ToString() != "")
					{
						model.MemMobile = dt.Rows[i]["MemMobile"].ToString();
					}
					if (dt.Rows[i]["ProposalTime"] != null && dt.Rows[i]["ProposalTime"].ToString() != "")
					{
						model.ProposalTime = new DateTime?(DateTime.Parse(dt.Rows[i]["ProposalTime"].ToString()));
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

		public DataSet GetProposalInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetProposalInfo(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
