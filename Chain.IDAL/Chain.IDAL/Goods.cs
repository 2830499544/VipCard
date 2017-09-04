using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class Goods
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("GoodsID", "Goods");
		}

		public bool Exists(int GoodsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Goods");
			strSql.Append(" where GoodsID=@GoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GoodsID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.Goods model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Goods(");
			strSql.Append("GoodsCode,GoodsClassID,Name,NameCode,Unit,GoodsNumber,SalePercet,GoodsSaleNumber,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsPicture,GoodsCreateTime,CreateShopID)");
			strSql.Append(" values (");
			strSql.Append("@GoodsCode,@GoodsClassID,@Name,@NameCode,@Unit,@GoodsNumber,@SalePercet,@GoodsSaleNumber,@Price,@CommissionType,@CommissionNumber,@Point,@MinPercent,@GoodsType,@GoodsBidPrice,@GoodsRemark,@GoodsPicture,@GoodsCreateTime,@CreateShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@GoodsClassID", SqlDbType.Int, 4),
				new SqlParameter("@Name", SqlDbType.VarChar, 100),
				new SqlParameter("@NameCode", SqlDbType.VarChar, 20),
				new SqlParameter("@Unit", SqlDbType.NVarChar, 10),
				new SqlParameter("@GoodsNumber", SqlDbType.Int, 4),
				new SqlParameter("@SalePercet", SqlDbType.Real, 4),
				new SqlParameter("@GoodsSaleNumber", SqlDbType.Int, 4),
				new SqlParameter("@Price", SqlDbType.Money, 8),
				new SqlParameter("@CommissionType", SqlDbType.Int, 4),
				new SqlParameter("@CommissionNumber", SqlDbType.Float, 8),
				new SqlParameter("@Point", SqlDbType.Int, 4),
				new SqlParameter("@MinPercent", SqlDbType.Real, 4),
				new SqlParameter("@GoodsType", SqlDbType.TinyInt, 1),
				new SqlParameter("@GoodsBidPrice", SqlDbType.Money, 8),
				new SqlParameter("@GoodsRemark", SqlDbType.VarChar, 100),
				new SqlParameter("@GoodsPicture", SqlDbType.VarChar, 50),
				new SqlParameter("@GoodsCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CreateShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GoodsCode;
			parameters[1].Value = model.GoodsClassID;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.NameCode;
			parameters[4].Value = model.Unit;
			parameters[5].Value = model.GoodsNumber;
			parameters[6].Value = model.SalePercet;
			parameters[7].Value = model.GoodsSaleNumber;
			parameters[8].Value = model.Price;
			parameters[9].Value = model.CommissionType;
			parameters[10].Value = model.CommissionNumber;
			parameters[11].Value = model.Point;
			parameters[12].Value = model.MinPercent;
			parameters[13].Value = model.GoodsType;
			parameters[14].Value = model.GoodsBidPrice;
			parameters[15].Value = model.GoodsRemark;
			parameters[16].Value = model.GoodsPicture;
			parameters[17].Value = model.GoodsCreateTime;
			parameters[18].Value = model.CreateShopID;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(obj);
			}
			return result;
		}

		public bool Update(Chain.Model.Goods model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Goods set ");
			strSql.Append("GoodsCode=@GoodsCode,");
			strSql.Append("GoodsClassID=@GoodsClassID,");
			strSql.Append("Name=@Name,");
			strSql.Append("NameCode=@NameCode,");
			strSql.Append("Unit=@Unit,");
			strSql.Append("GoodsNumber=@GoodsNumber,");
			strSql.Append("SalePercet=@SalePercet,");
			strSql.Append("GoodsSaleNumber=@GoodsSaleNumber,");
			strSql.Append("Price=@Price,");
			strSql.Append("CommissionType=@CommissionType,");
			strSql.Append("CommissionNumber=@CommissionNumber,");
			strSql.Append("Point=@Point,");
			strSql.Append("MinPercent=@MinPercent,");
			strSql.Append("GoodsType=@GoodsType,");
			strSql.Append("GoodsBidPrice=@GoodsBidPrice,");
			strSql.Append("GoodsRemark=@GoodsRemark,");
			strSql.Append("GoodsPicture=@GoodsPicture,");
			strSql.Append("GoodsCreateTime=@GoodsCreateTime,");
			strSql.Append("CreateShopID=@CreateShopID");
			strSql.Append(" where GoodsID=@GoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@GoodsClassID", SqlDbType.Int, 4),
				new SqlParameter("@Name", SqlDbType.VarChar, 100),
				new SqlParameter("@NameCode", SqlDbType.VarChar, 20),
				new SqlParameter("@Unit", SqlDbType.NVarChar, 10),
				new SqlParameter("@GoodsNumber", SqlDbType.Int, 4),
				new SqlParameter("@SalePercet", SqlDbType.Real, 4),
				new SqlParameter("@GoodsSaleNumber", SqlDbType.Int, 4),
				new SqlParameter("@Price", SqlDbType.Money, 8),
				new SqlParameter("@CommissionType", SqlDbType.Int, 4),
				new SqlParameter("@CommissionNumber", SqlDbType.Float, 8),
				new SqlParameter("@Point", SqlDbType.Int, 4),
				new SqlParameter("@MinPercent", SqlDbType.Real, 4),
				new SqlParameter("@GoodsType", SqlDbType.TinyInt, 1),
				new SqlParameter("@GoodsBidPrice", SqlDbType.Money, 8),
				new SqlParameter("@GoodsRemark", SqlDbType.VarChar, 100),
				new SqlParameter("@GoodsPicture", SqlDbType.VarChar, 50),
				new SqlParameter("@GoodsCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CreateShopID", SqlDbType.Int, 4),
				new SqlParameter("@GoodsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GoodsCode;
			parameters[1].Value = model.GoodsClassID;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.NameCode;
			parameters[4].Value = model.Unit;
			parameters[5].Value = model.GoodsNumber;
			parameters[6].Value = model.SalePercet;
			parameters[7].Value = model.GoodsSaleNumber;
			parameters[8].Value = model.Price;
			parameters[9].Value = model.CommissionType;
			parameters[10].Value = model.CommissionNumber;
			parameters[11].Value = model.Point;
			parameters[12].Value = model.MinPercent;
			parameters[13].Value = model.GoodsType;
			parameters[14].Value = model.GoodsBidPrice;
			parameters[15].Value = model.GoodsRemark;
			parameters[16].Value = model.GoodsPicture;
			parameters[17].Value = model.GoodsCreateTime;
			parameters[18].Value = model.CreateShopID;
			parameters[19].Value = model.GoodsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int GoodsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Goods ");
			strSql.Append(" where GoodsID=@GoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GoodsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string GoodsIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Goods ");
			strSql.Append(" where GoodsID in (" + GoodsIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Goods GetModel(int GoodsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GoodsID,GoodsCode,GoodsClassID,Name,NameCode,Unit,GoodsNumber,SalePercet,GoodsSaleNumber,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsPicture,GoodsCreateTime,CreateShopID from Goods ");
			strSql.Append(" where GoodsID=@GoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GoodsID;
			Chain.Model.Goods model = new Chain.Model.Goods();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Goods result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["GoodsID"] != null && ds.Tables[0].Rows[0]["GoodsID"].ToString() != "")
				{
					model.GoodsID = int.Parse(ds.Tables[0].Rows[0]["GoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsCode"] != null && ds.Tables[0].Rows[0]["GoodsCode"].ToString() != "")
				{
					model.GoodsCode = ds.Tables[0].Rows[0]["GoodsCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsClassID"] != null && ds.Tables[0].Rows[0]["GoodsClassID"].ToString() != "")
				{
					model.GoodsClassID = int.Parse(ds.Tables[0].Rows[0]["GoodsClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
				{
					model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NameCode"] != null && ds.Tables[0].Rows[0]["NameCode"].ToString() != "")
				{
					model.NameCode = ds.Tables[0].Rows[0]["NameCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Unit"] != null && ds.Tables[0].Rows[0]["Unit"].ToString() != "")
				{
					model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsNumber"] != null && ds.Tables[0].Rows[0]["GoodsNumber"].ToString() != "")
				{
					model.GoodsNumber = int.Parse(ds.Tables[0].Rows[0]["GoodsNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SalePercet"] != null && ds.Tables[0].Rows[0]["SalePercet"].ToString() != "")
				{
					model.SalePercet = decimal.Parse(ds.Tables[0].Rows[0]["SalePercet"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsSaleNumber"] != null && ds.Tables[0].Rows[0]["GoodsSaleNumber"].ToString() != "")
				{
					model.GoodsSaleNumber = int.Parse(ds.Tables[0].Rows[0]["GoodsSaleNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
				{
					model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CommissionType"] != null && ds.Tables[0].Rows[0]["CommissionType"].ToString() != "")
				{
					model.CommissionType = int.Parse(ds.Tables[0].Rows[0]["CommissionType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CommissionNumber"] != null && ds.Tables[0].Rows[0]["CommissionNumber"].ToString() != "")
				{
					model.CommissionNumber = decimal.Parse(ds.Tables[0].Rows[0]["CommissionNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Point"] != null && ds.Tables[0].Rows[0]["Point"].ToString() != "")
				{
					model.Point = int.Parse(ds.Tables[0].Rows[0]["Point"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MinPercent"] != null && ds.Tables[0].Rows[0]["MinPercent"].ToString() != "")
				{
					model.MinPercent = decimal.Parse(ds.Tables[0].Rows[0]["MinPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsType"] != null && ds.Tables[0].Rows[0]["GoodsType"].ToString() != "")
				{
					model.GoodsType = int.Parse(ds.Tables[0].Rows[0]["GoodsType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsBidPrice"] != null && ds.Tables[0].Rows[0]["GoodsBidPrice"].ToString() != "")
				{
					model.GoodsBidPrice = decimal.Parse(ds.Tables[0].Rows[0]["GoodsBidPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsRemark"] != null && ds.Tables[0].Rows[0]["GoodsRemark"].ToString() != "")
				{
					model.GoodsRemark = ds.Tables[0].Rows[0]["GoodsRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsPicture"] != null && ds.Tables[0].Rows[0]["GoodsPicture"].ToString() != "")
				{
					model.GoodsPicture = ds.Tables[0].Rows[0]["GoodsPicture"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsCreateTime"] != null && ds.Tables[0].Rows[0]["GoodsCreateTime"].ToString() != "")
				{
					model.GoodsCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["GoodsCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateShopID"] != null && ds.Tables[0].Rows[0]["CreateShopID"].ToString() != "")
				{
					model.CreateShopID = int.Parse(ds.Tables[0].Rows[0]["CreateShopID"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.Goods GetModel(string GoodsCode)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GoodsID,GoodsCode,GoodsClassID,Name,NameCode,Unit,GoodsNumber,SalePercet,GoodsSaleNumber,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsPicture,GoodsCreateTime,CreateShopID from Goods ");
			strSql.Append(" where GoodsCode=@GoodsCode");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsCode", SqlDbType.VarChar, 255)
			};
			parameters[0].Value = GoodsCode;
			Chain.Model.Goods model = new Chain.Model.Goods();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Goods result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["GoodsID"] != null && ds.Tables[0].Rows[0]["GoodsID"].ToString() != "")
				{
					model.GoodsID = int.Parse(ds.Tables[0].Rows[0]["GoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsCode"] != null && ds.Tables[0].Rows[0]["GoodsCode"].ToString() != "")
				{
					model.GoodsCode = ds.Tables[0].Rows[0]["GoodsCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsClassID"] != null && ds.Tables[0].Rows[0]["GoodsClassID"].ToString() != "")
				{
					model.GoodsClassID = int.Parse(ds.Tables[0].Rows[0]["GoodsClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
				{
					model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NameCode"] != null && ds.Tables[0].Rows[0]["NameCode"].ToString() != "")
				{
					model.NameCode = ds.Tables[0].Rows[0]["NameCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Unit"] != null && ds.Tables[0].Rows[0]["Unit"].ToString() != "")
				{
					model.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsNumber"] != null && ds.Tables[0].Rows[0]["GoodsNumber"].ToString() != "")
				{
					model.GoodsNumber = int.Parse(ds.Tables[0].Rows[0]["GoodsNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SalePercet"] != null && ds.Tables[0].Rows[0]["SalePercet"].ToString() != "")
				{
					model.SalePercet = decimal.Parse(ds.Tables[0].Rows[0]["SalePercet"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsSaleNumber"] != null && ds.Tables[0].Rows[0]["GoodsSaleNumber"].ToString() != "")
				{
					model.GoodsSaleNumber = int.Parse(ds.Tables[0].Rows[0]["GoodsSaleNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Price"] != null && ds.Tables[0].Rows[0]["Price"].ToString() != "")
				{
					model.Price = decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CommissionType"] != null && ds.Tables[0].Rows[0]["CommissionType"].ToString() != "")
				{
					model.CommissionType = int.Parse(ds.Tables[0].Rows[0]["CommissionType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CommissionNumber"] != null && ds.Tables[0].Rows[0]["CommissionNumber"].ToString() != "")
				{
					model.CommissionNumber = decimal.Parse(ds.Tables[0].Rows[0]["CommissionNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Point"] != null && ds.Tables[0].Rows[0]["Point"].ToString() != "")
				{
					model.Point = int.Parse(ds.Tables[0].Rows[0]["Point"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MinPercent"] != null && ds.Tables[0].Rows[0]["MinPercent"].ToString() != "")
				{
					model.MinPercent = decimal.Parse(ds.Tables[0].Rows[0]["MinPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsType"] != null && ds.Tables[0].Rows[0]["GoodsType"].ToString() != "")
				{
					model.GoodsType = int.Parse(ds.Tables[0].Rows[0]["GoodsType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsBidPrice"] != null && ds.Tables[0].Rows[0]["GoodsBidPrice"].ToString() != "")
				{
					model.GoodsBidPrice = decimal.Parse(ds.Tables[0].Rows[0]["GoodsBidPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsRemark"] != null && ds.Tables[0].Rows[0]["GoodsRemark"].ToString() != "")
				{
					model.GoodsRemark = ds.Tables[0].Rows[0]["GoodsRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsPicture"] != null && ds.Tables[0].Rows[0]["GoodsPicture"].ToString() != "")
				{
					model.GoodsPicture = ds.Tables[0].Rows[0]["GoodsPicture"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsCreateTime"] != null && ds.Tables[0].Rows[0]["GoodsCreateTime"].ToString() != "")
				{
					model.GoodsCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["GoodsCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateShopID"] != null && ds.Tables[0].Rows[0]["CreateShopID"].ToString() != "")
				{
					model.CreateShopID = int.Parse(ds.Tables[0].Rows[0]["CreateShopID"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select GoodsID,GoodsCode,GoodsClassID,Name,NameCode,Unit,GoodsNumber,SalePercet,GoodsSaleNumber,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsPicture,GoodsCreateTime,Goods.CreateShopID,ClassName ");
			strSql.Append(" FROM Goods,GoodsClass where  Goods.GoodsClassID = GoodsClass.ClassID");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" and " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetGoodsList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select *");
			strSql.Append(" FROM  Goods where " + strWhere);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" GoodsID,GoodsCode,GoodsClassID,Name,NameCode,Unit,GoodsNumber,SalePercet,GoodsSaleNumber,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsPicture,GoodsCreateTime,CreateShopID ");
			strSql.Append(" FROM Goods ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM Goods ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(obj);
			}
			return result;
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby);
			}
			else
			{
				strSql.Append("order by T.GoodsID desc");
			}
			strSql.Append(")AS Row, T.*  from Goods T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetGoodsInfo(string goodscode, int levelid)
		{
			string strSql = "select goods.GoodsType,goods.GoodsID,goods.[Name],goods.Price, GoodsClassDiscount.* from goods inner join GoodsClassDiscount on goods.GoodsClassID = GoodsClassDiscount.GoodsClassID " + string.Format(" WHERE goods.goodscode = '{0}' and GoodsClassDiscount.MemLevelID = {1}", goodscode, levelid);
			return DbHelperSQL.Query(strSql);
		}

		public bool Exists(string strGoodsCode)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Goods");
			strSql.Append(" where GoodsCode=@GoodsCode");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsCode", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = strGoodsCode;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string strGoodsCode, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Goods");
			strSql.Append(" where GoodsCode=@GoodsCode and CreateShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@ShopID", SqlDbType.Int)
			};
			parameters[0].Value = strGoodsCode;
			parameters[1].Value = ShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int goodsID, string goodsCode)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select count(1) from Goods");
			strSql.Append(" where GoodsCode=@GoodsCode and GoodsID<>@GoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@GoodsID", SqlDbType.Int)
			};
			parameters[0].Value = goodsCode;
			parameters[1].Value = goodsID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int goodsID, string goodsCode, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select count(1) from Goods");
			strSql.Append(" where GoodsCode=@GoodsCode and GoodsID<>@GoodsID and ShopID<>@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@GoodsID", SqlDbType.Int),
				new SqlParameter("@ShopID", SqlDbType.Int)
			};
			parameters[0].Value = goodsCode;
			parameters[1].Value = goodsID;
			parameters[2].Value = ShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int AddCustomField(string strGoodsCode, Hashtable customhash)
		{
			MemCustomField custom = new MemCustomField();
			DataSet ds = custom.GetList(" CustomType=2");
			StringBuilder sbCustom = new StringBuilder();
			if (ds.Tables[0].Rows.Count > 0)
			{
				sbCustom.Append(" update Goods set ");
				int i = 1;
				string strValue = "";
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					if (null != customhash[dr["CustomField"].ToString()])
					{
						strValue = customhash[dr["CustomField"].ToString()].ToString();
					}
					sbCustom.AppendFormat(" {0}='{1}' ", dr["CustomField"].ToString(), strValue);
					if (i != ds.Tables[0].Rows.Count)
					{
						sbCustom.Append(",");
					}
					i++;
				}
				sbCustom.AppendFormat(" where GoodsCode='{0}'", strGoodsCode);
			}
			return DbHelperSQL.ExecuteSql(sbCustom.ToString());
		}

		public DataSet GetItemAll(int intGoodsID)
		{
			string sql_mem = " select * from Goods where GoodsID =" + intGoodsID;
			return DbHelperSQL.Query(sql_mem);
		}

		public DataSet GetList(int PageSiza, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Goods";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "GoodsID", false, PageSiza, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Goods,GoodsClass,GoodsNumber";
			string[] columns = new string[]
			{
				"Goods.*,ClassName,ShopID"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "GoodsCreateTime", "Goods.GoodsID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetGoodsExpense(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = this.GetListGoodsSql(columns, strWhere, "totalNumber", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListGoodsSql(string[] column, string[] condition, string IndexColumn, bool IsAsc, int PageSize, int Page, int RecordCount)
		{
			string tempsql = "";
			string tempsql2 = "";
			for (int i = 0; i < column.Length; i++)
			{
				tempsql += column[i];
				if (i < column.Length - 1)
				{
					tempsql += ",";
				}
			}
			if (condition != null && condition.Length > 0)
			{
				for (int i = 0; i < condition.Length; i++)
				{
					if (!(condition[i] == ""))
					{
						tempsql2 += condition[i];
						if (i < condition.Length - 1)
						{
							tempsql2 += " and ";
						}
					}
				}
			}
			tempsql2 = DbHelperSQL.ProcessSql(tempsql2);
			SqlParameter[] paras = new SqlParameter[]
			{
				new SqlParameter("@ReturnFields", SqlDbType.VarChar, 500),
				new SqlParameter("@OrderFields", SqlDbType.VarChar, 500),
				new SqlParameter("@PageSize", SqlDbType.Int, 4),
				new SqlParameter("@PageIndex", SqlDbType.Int, 4),
				new SqlParameter("@PKField", SqlDbType.VarChar, 255),
				new SqlParameter("@IsDesc", SqlDbType.Bit),
				new SqlParameter("@Where", SqlDbType.VarChar, 4000)
			};
			paras[0].Value = tempsql;
			paras[1].Value = IndexColumn;
			paras[2].Value = PageSize;
			paras[3].Value = Page;
			paras[4].Value = IndexColumn;
			paras[5].Value = (IsAsc ? 0 : 1);
			paras[6].Value = tempsql2;
			return DbHelperSQL.ExecuteProcedure("CP_GoodsExpendPage", paras);
		}

		public DataSet GetGoodLists(int PageSize, int PageIndex, out int resCount, string strTime, params string[] strWhere)
		{
			string tableName = "Goods ,GoodsClass";
			string[] columns = new string[]
			{
				"Goods.*,(SELECT ISNULL(SUM(PointNumber),0) from PointLOG AS L where L.PointMemID=Mem.MEMID and L.PointChangeTYPE=9 and 1=1  )AS RatePointCount,(SELECT ISNULL(SUM(PointNumber),0) from PointLOG AS A where A.PointMemID=Mem.MEMID and A.PointChangeTYPE=9)AS RatePointNow"
			};
			columns[0] = columns[0].Replace("and 1=1", strTime);
			strWhere[0] = strWhere[0].Substring(32);
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemCreateTime", "MemID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetGoodsList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_GoodsNumber";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ShopID", "GoodsID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetGoodsListByMember(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_GoodsNumberByMember";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "Number", "GoodsID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetGoodsListByNoMember(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_GoodsNumberNoMember";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "Number", "GoodsID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetGoodsStockList(int ShopId, int MemLevelId, string GoodsIdList, int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "GoodsNumber,SysShop,Goods,GoodsClassDiscount";
			string[] columns = new string[]
			{
				"GoodsNumber.Number, SysShop.ShopName, GoodsNumber.ShopID, Goods.*, GoodsClassDiscount.MemLevelID, GoodsClassDiscount.ClassDiscountPercent, GoodsClassDiscount.ClassPointPercent"
			};
			List<string> con = new List<string>();
			for (int i = 0; i < strWhere.Length; i++)
			{
				con.Add(strWhere[i]);
			}
			con.Add(" GoodsNumber.GoodsID = Goods.GoodsID and Goods.GoodsClassID = GoodsClassDiscount.GoodsClassID and GoodsNumber.ShopID = SysShop.ShopID and GoodsNumber.ShopID = GoodsClassDiscount.DiscountShopID");
			if (ShopId != -1)
			{
				con.Add(" GoodsNumber.ShopID = " + ShopId);
			}
			if (MemLevelId == -1)
			{
				con.Add(" GoodsClassDiscount.MemLevelId = (select MIN(MemLevelID) from GoodsClassDiscount)");
			}
			else
			{
				con.Add(" GoodsClassDiscount.MemLevelId = " + MemLevelId);
			}
			if (!string.IsNullOrEmpty(GoodsIdList))
			{
				con.Add(" GoodsNumber.GoodsID in (" + GoodsIdList.Trim() + ")");
			}
			string[] condition = con.ToArray();
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, condition, "ID", "GoodsNumber.GoodsID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetStockRemind(string strWhere, int count)
		{
			return DbHelperSQL.Query(string.Format("select top " + count + " * from  V_GoodsNumber where {0} ", strWhere));
		}

		public DataSet GetStockRemind(string strWhere)
		{
			return DbHelperSQL.Query(string.Format("select * from  V_GoodsNumber where {0} ", strWhere));
		}

		public DataSet GetGoodsListByShopID(int PageSize, int PageIndex, int ShopID, out int resCount, params string[] strWhere)
		{
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = this.GetGoodsNumberSql(columns, strWhere, "id", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetGoodsNumberSql(string[] column, string[] condition, string IndexColumn, bool IsAsc, int PageSize, int Page, int RecordCount)
		{
			string tempsql = "";
			string tempsql2 = "";
			for (int i = 0; i < column.Length; i++)
			{
				tempsql += column[i];
				if (i < column.Length - 1)
				{
					tempsql += ",";
				}
			}
			if (condition != null && condition.Length > 0)
			{
				for (int i = 0; i < condition.Length; i++)
				{
					if (!(condition[i] == ""))
					{
						tempsql2 += condition[i];
						if (i < condition.Length - 1)
						{
							tempsql2 += " and ";
						}
					}
				}
			}
			tempsql2 = DbHelperSQL.ProcessSql(tempsql2);
			SqlParameter[] paras = new SqlParameter[]
			{
				new SqlParameter("@ReturnFields", SqlDbType.VarChar, 500),
				new SqlParameter("@OrderFields", SqlDbType.VarChar, 255),
				new SqlParameter("@PageSize", SqlDbType.Int, 4),
				new SqlParameter("@PageIndex", SqlDbType.Int, 4),
				new SqlParameter("@PKField", SqlDbType.VarChar, 255),
				new SqlParameter("@IsDesc", SqlDbType.Bit),
				new SqlParameter("@Where", SqlDbType.VarChar, 1000)
			};
			paras[0].Value = tempsql;
			paras[1].Value = IndexColumn;
			paras[2].Value = PageSize;
			paras[3].Value = Page;
			paras[4].Value = IndexColumn;
			paras[5].Value = (IsAsc ? 0 : 1);
			paras[6].Value = tempsql2;
			return DbHelperSQL.ExecuteProcedure("CP_GoodsNumberPage", paras);
		}

		public bool ExeclDataInput(ArrayList sqlArray)
		{
			return DbHelperSQL.ExecuteSqlTran(sqlArray);
		}

		public int GetGoodsID(string GoodsAccount)
		{
			string sql = string.Format("select GoodsID from dbo.Goods where GoodsCode='{0}'", GoodsAccount);
			DataSet ds = DbHelperSQL.Query(sql);
			return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
		}
	}
}
