using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemberExplanation
	{
		private readonly Chain.IDAL.MemberExplanation dal = new Chain.IDAL.MemberExplanation();

		public bool Exists(int MemberExplanationID)
		{
			return this.dal.Exists(MemberExplanationID);
		}

		public int Add(Chain.Model.MemberExplanation model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemberExplanation model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MemberExplanationID)
		{
			return this.dal.Delete(MemberExplanationID);
		}

		public bool DeleteList(string MemberExplanationIDlist)
		{
			return this.dal.DeleteList(MemberExplanationIDlist);
		}

		public Chain.Model.MemberExplanation GetModel(int MemberExplanationID)
		{
			return this.dal.GetModel(MemberExplanationID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemberExplanation> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemberExplanation> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemberExplanation> modelList = new List<Chain.Model.MemberExplanation>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemberExplanation model = new Chain.Model.MemberExplanation();
					if (dt.Rows[i]["MemberExplanationID"] != null && dt.Rows[i]["MemberExplanationID"].ToString() != "")
					{
						model.MemberExplanationID = int.Parse(dt.Rows[i]["MemberExplanationID"].ToString());
					}
					if (dt.Rows[i]["MemberExplanationDesc"] != null && dt.Rows[i]["MemberExplanationDesc"].ToString() != "")
					{
						model.MemberExplanationDesc = dt.Rows[i]["MemberExplanationDesc"].ToString();
					}
					if (dt.Rows[i]["MemberExplanationTime"] != null && dt.Rows[i]["MemberExplanationTime"].ToString() != "")
					{
						model.MemberExplanationTime = DateTime.Parse(dt.Rows[i]["MemberExplanationTime"].ToString());
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
