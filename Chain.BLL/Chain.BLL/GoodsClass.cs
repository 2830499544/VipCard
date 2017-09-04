using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GoodsClass
	{
		private readonly Chain.IDAL.GoodsClass dal = new Chain.IDAL.GoodsClass();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ClassID)
		{
			return this.dal.Exists(ClassID);
		}

		public int Add(Chain.Model.GoodsClass model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GoodsClass model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ClassID)
		{
			return this.dal.Delete(ClassID);
		}

		public bool DeleteList(string ClassIDlist)
		{
			return this.dal.DeleteList(ClassIDlist);
		}

		public Chain.Model.GoodsClass GetModel(int ClassID)
		{
			return this.dal.GetModel(ClassID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.GoodsClass> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GoodsClass> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GoodsClass> modelList = new List<Chain.Model.GoodsClass>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GoodsClass model = new Chain.Model.GoodsClass();
					if (dt.Rows[i]["ClassID"] != null && dt.Rows[i]["ClassID"].ToString() != "")
					{
						model.ClassID = int.Parse(dt.Rows[i]["ClassID"].ToString());
					}
					if (dt.Rows[i]["ClassName"] != null && dt.Rows[i]["ClassName"].ToString() != "")
					{
						model.ClassName = dt.Rows[i]["ClassName"].ToString();
					}
					if (dt.Rows[i]["ClassRemark"] != null && dt.Rows[i]["ClassRemark"].ToString() != "")
					{
						model.ClassRemark = dt.Rows[i]["ClassRemark"].ToString();
					}
					if (dt.Rows[i]["ParentID"] != null && dt.Rows[i]["ParentID"].ToString() != "")
					{
						model.ParentID = int.Parse(dt.Rows[i]["ParentID"].ToString());
					}
					if (dt.Rows[i]["CreateShopID"] != null && dt.Rows[i]["CreateShopID"].ToString() != "")
					{
						model.CreateShopID = int.Parse(dt.Rows[i]["CreateShopID"].ToString());
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

		public bool Exists(int classID, string ClassName, int CreateShopID)
		{
			return this.dal.Exists(classID, ClassName, CreateShopID);
		}

		public bool Exists(string ClassName, int ShopID)
		{
			return this.dal.Exists(ClassName, ShopID);
		}

		public int UpdateByShop(Chain.Model.GoodsClass model)
		{
			int result;
			if (this.Exists(model.ClassID, model.ClassName, model.CreateShopID))
			{
				result = -1;
			}
			else
			{
				result = (this.dal.Update(model) ? 1 : -3);
			}
			return result;
		}

		public int DeleteByParentID(int ParentID)
		{
			return this.dal.DeleteByParentID(ParentID);
		}

		public DataTable GetItem(int ClassID)
		{
			return this.dal.GetItem(ClassID).Tables[0];
		}

		public DataSet GetListByShopID(int ShopID)
		{
			return this.dal.GetListByShopID(ShopID);
		}

		public void CreateTreeItem(DataTable dtSource, DataTable dt, int ParentClassID, int level)
		{
			if (dt.Columns.Count <= 0)
			{
				for (int i = 0; i < dtSource.Columns.Count; i++)
				{
					dt.Columns.Add(new DataColumn(dtSource.Columns[i].ColumnName));
				}
			}
			DataRow[] dr = dtSource.Select(" ParentID=" + ParentClassID);
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					string temp = dr[i][j].ToString();
					if (dt.Columns[j].ColumnName == "ClassName")
					{
						temp = new string('-', level * 3).ToString() + temp;
					}
					dt.Rows[dt.Rows.Count - 1][j] = temp;
				}
				int CurrentID = Convert.ToInt32(dr[i]["ClassID"].ToString());
				this.CreateTreeItem(dtSource, dt, CurrentID, level + 1);
			}
		}

		public int DeleteClassByShop(int ClassID, int ShopID)
		{
			GoodsNumber bllGN = new GoodsNumber();
			GoodsClassAuthority bllGCA = new GoodsClassAuthority();
			GoodsClassDiscount bllGCD = new GoodsClassDiscount();
			GoodsClass bllGC = new GoodsClass();
			Goods bllG = new Goods();
			Chain.Model.GoodsClass modelGC = bllGC.GetModel(ClassID);
			int result;
			if (modelGC == null)
			{
				result = -100;
			}
			else
			{
				if (modelGC.ParentID == 0)
				{
					int childClassCount = bllGCA.GetClassCountByParentID(modelGC.ClassID);
					if (childClassCount > 0)
					{
						int childClassCountInShop = bllGCA.GetClassCountByParentID(modelGC.ClassID, ShopID);
						if (childClassCountInShop > 0)
						{
							result = -2;
							return result;
						}
						if (bllGN.GetGoodsCount(ClassID, ShopID) == 0)
						{
							bllGCA.DeleteAuthority(ClassID, ShopID);
							result = 1;
							return result;
						}
						result = -1;
						return result;
					}
				}
				int goodsCount = bllGN.GetGoodsCount(ClassID, ShopID);
				if (goodsCount == 0)
				{
					bllGCA.DeleteAuthority(ClassID, ShopID);
					bllGCD.DeleteDiscount(ClassID, ShopID);
					DataTable dt = bllGCA.GetShopIDListByClass(ClassID).Tables[0];
					bool isClassShare = false;
					foreach (DataRow item in dt.Rows)
					{
						if (Convert.ToInt32(item["ShopID"]) != ShopID)
						{
							isClassShare = true;
							break;
						}
					}
					if (!isClassShare)
					{
						bllGC.Delete(ClassID);
					}
					result = 1;
				}
				else
				{
					result = -1;
				}
			}
			return result;
		}
	}
}
