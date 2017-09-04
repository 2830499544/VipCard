using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SmsTemplate
	{
		private readonly Chain.IDAL.SmsTemplate dal = new Chain.IDAL.SmsTemplate();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int TemplateID)
		{
			return this.dal.Exists(TemplateID);
		}

		public int Add(Chain.Model.SmsTemplate model)
		{
			return this.dal.Add(model);
		}

		public int Update(Chain.Model.SmsTemplate model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int TemplateID)
		{
			return this.dal.Delete(TemplateID);
		}

		public bool DeleteList(string TemplateIDlist)
		{
			return this.dal.DeleteList(TemplateIDlist);
		}

		public Chain.Model.SmsTemplate GetModel(int TemplateID)
		{
			return this.dal.GetModel(TemplateID);
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

		public List<Chain.Model.SmsTemplate> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SmsTemplate> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SmsTemplate> modelList = new List<Chain.Model.SmsTemplate>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SmsTemplate model = new Chain.Model.SmsTemplate();
					if (dt.Rows[i]["TemplateID"] != null && dt.Rows[i]["TemplateID"].ToString() != "")
					{
						model.TemplateID = int.Parse(dt.Rows[i]["TemplateID"].ToString());
					}
					if (dt.Rows[i]["TemplateName"] != null && dt.Rows[i]["TemplateName"].ToString() != "")
					{
						model.TemplateName = dt.Rows[i]["TemplateName"].ToString();
					}
					if (dt.Rows[i]["TemplateContent"] != null && dt.Rows[i]["TemplateContent"].ToString() != "")
					{
						model.TemplateContent = dt.Rows[i]["TemplateContent"].ToString();
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
