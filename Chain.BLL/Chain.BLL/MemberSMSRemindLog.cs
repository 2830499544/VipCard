using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemberSMSRemindLog
	{
		private readonly Chain.IDAL.MemberSMSRemindLog dal = new Chain.IDAL.MemberSMSRemindLog();

		public DataSet GetMemBirthdayed()
		{
			return this.dal.GetMemBirthdayed();
		}

		public DataSet GetMemPasted()
		{
			return this.dal.GetMemPasted();
		}

		public bool DeleteMemByMemID(int MemID)
		{
			return this.dal.DeleteMemByMemID(MemID);
		}

		public bool Exists(int MemberSMSRemindID)
		{
			return this.dal.Exists(MemberSMSRemindID);
		}

		public int Add(Chain.Model.MemberSMSRemindLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemberSMSRemindLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MemberSMSRemindID)
		{
			return this.dal.Delete(MemberSMSRemindID);
		}

		public bool DeleteList(string MemberSMSRemindIDlist)
		{
			return this.dal.DeleteList(MemberSMSRemindIDlist);
		}

		public Chain.Model.MemberSMSRemindLog GetModel(int MemberSMSRemindID)
		{
			return this.dal.GetModel(MemberSMSRemindID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemberSMSRemindLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemberSMSRemindLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemberSMSRemindLog> modelList = new List<Chain.Model.MemberSMSRemindLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemberSMSRemindLog model = new Chain.Model.MemberSMSRemindLog();
					if (dt.Rows[i]["MemberSMSRemindID"] != null && dt.Rows[i]["MemberSMSRemindID"].ToString() != "")
					{
						model.MemberSMSRemindID = int.Parse(dt.Rows[i]["MemberSMSRemindID"].ToString());
					}
					if (dt.Rows[i]["MemberSMSRemindMemID"] != null && dt.Rows[i]["MemberSMSRemindMemID"].ToString() != "")
					{
						model.MemberSMSRemindMemID = int.Parse(dt.Rows[i]["MemberSMSRemindMemID"].ToString());
					}
					if (dt.Rows[i]["MemberSMSRemindMobile"] != null && dt.Rows[i]["MemberSMSRemindMobile"].ToString() != "")
					{
						model.MemberSMSRemindMobile = dt.Rows[i]["MemberSMSRemindMobile"].ToString();
					}
					if (dt.Rows[i]["MemberSMSRemindContent"] != null && dt.Rows[i]["MemberSMSRemindContent"].ToString() != "")
					{
						model.MemberSMSRemindContent = dt.Rows[i]["MemberSMSRemindContent"].ToString();
					}
					if (dt.Rows[i]["MemberSMSRemindShopID"] != null && dt.Rows[i]["MemberSMSRemindShopID"].ToString() != "")
					{
						model.MemberSMSRemindShopID = int.Parse(dt.Rows[i]["MemberSMSRemindShopID"].ToString());
					}
					if (dt.Rows[i]["MemberSMSRemindTime"] != null && dt.Rows[i]["MemberSMSRemindTime"].ToString() != "")
					{
						model.MemberSMSRemindTime = DateTime.Parse(dt.Rows[i]["MemberSMSRemindTime"].ToString());
					}
					if (dt.Rows[i]["MemberSMSRemindUserID"] != null && dt.Rows[i]["MemberSMSRemindUserID"].ToString() != "")
					{
						model.MemberSMSRemindUserID = int.Parse(dt.Rows[i]["MemberSMSRemindUserID"].ToString());
					}
					if (dt.Rows[i]["MemberSMSRemindAmount"] != null && dt.Rows[i]["MemberSMSRemindAmount"].ToString() != "")
					{
						model.MemberSMSRemindAmount = int.Parse(dt.Rows[i]["MemberSMSRemindAmount"].ToString());
					}
					if (dt.Rows[i]["MemberSMSRemindAllAmount"] != null && dt.Rows[i]["MemberSMSRemindAllAmount"].ToString() != "")
					{
						model.MemberSMSRemindAllAmount = int.Parse(dt.Rows[i]["MemberSMSRemindAllAmount"].ToString());
					}
					if (dt.Rows[i]["MemberSMSRemindType"] != null && dt.Rows[i]["MemberSMSRemindType"].ToString() != "")
					{
						model.MemberSMSRemindType = int.Parse(dt.Rows[i]["MemberSMSRemindType"].ToString());
					}
					if (dt.Rows[i]["MemberSMSRemindBirthday"] != null && dt.Rows[i]["MemberSMSRemindBirthday"].ToString() != "")
					{
						model.MemberSMSRemindBirthday = DateTime.Parse(dt.Rows[i]["MemberSMSRemindBirthday"].ToString());
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
