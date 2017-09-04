using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class WeiXinLog
	{
		private readonly Chain.IDAL.WeiXinLog dal = new Chain.IDAL.WeiXinLog();

		public bool Exists(int WeiXinLogID)
		{
			return this.dal.Exists(WeiXinLogID);
		}

		public int Add(Chain.Model.WeiXinLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.WeiXinLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int WeiXinLogID)
		{
			return this.dal.Delete(WeiXinLogID);
		}

		public bool Delete(string MemWeiXinCard)
		{
			return this.dal.Delete(MemWeiXinCard);
		}

		public bool DeleteList(string WeiXinLogIDlist)
		{
			return this.dal.DeleteList(WeiXinLogIDlist);
		}

		public Chain.Model.WeiXinLog GetModel(string MemWeiXinCard)
		{
			return this.dal.GetModel(MemWeiXinCard);
		}

		public Chain.Model.WeiXinLog GetModel(int WeiXinLogID)
		{
			return this.dal.GetModel(WeiXinLogID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.WeiXinLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.WeiXinLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.WeiXinLog> modelList = new List<Chain.Model.WeiXinLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.WeiXinLog model = new Chain.Model.WeiXinLog();
					if (dt.Rows[i]["WeiXinLogID"] != null && dt.Rows[i]["WeiXinLogID"].ToString() != "")
					{
						model.WeiXinLogID = int.Parse(dt.Rows[i]["WeiXinLogID"].ToString());
					}
					if (dt.Rows[i]["MemWeiXinCard"] != null && dt.Rows[i]["MemWeiXinCard"].ToString() != "")
					{
						model.MemWeiXinCard = dt.Rows[i]["MemWeiXinCard"].ToString();
					}
					if (dt.Rows[i]["RecordContent"] != null && dt.Rows[i]["RecordContent"].ToString() != "")
					{
						model.RecordContent = dt.Rows[i]["RecordContent"].ToString();
					}
					if (dt.Rows[i]["RecordContentType"] != null && dt.Rows[i]["RecordContentType"].ToString() != "")
					{
						model.RecordContentType = int.Parse(dt.Rows[i]["RecordContentType"].ToString());
					}
					if (dt.Rows[i]["StatusCode"] != null && dt.Rows[i]["StatusCode"].ToString() != "")
					{
						model.StatusCode = dt.Rows[i]["StatusCode"].ToString();
					}
					if (dt.Rows[i]["RandomCode"] != null && dt.Rows[i]["RandomCode"].ToString() != "")
					{
						model.RandomCode = dt.Rows[i]["RandomCode"].ToString();
					}
					if (dt.Rows[i]["ErrorTimes"] != null && dt.Rows[i]["ErrorTimes"].ToString() != "")
					{
						model.ErrorTimes = int.Parse(dt.Rows[i]["ErrorTimes"].ToString());
					}
					if (dt.Rows[i]["WeiXinLogCreateTime"] != null && dt.Rows[i]["WeiXinLogCreateTime"].ToString() != "")
					{
						model.WeiXinLogCreateTime = DateTime.Parse(dt.Rows[i]["WeiXinLogCreateTime"].ToString());
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
