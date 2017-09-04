using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysRotate
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("RotateID", "SysRotate");
		}

		public bool Exists(int RotateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysRotatePrizeLog");
			strSql.Append(" where RotateID=@RotateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RotateID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysRotate model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysRotate(");
			strSql.Append("IsWinOne,OneName,TwoName,ThreeName,FourName,FiveName,SixName,OneMobile,TwoMobile,ThreeMobile,FourMobile,FiveMobile,SixMobile,ImageUrl,RotateRegion,OneRate,TwoRate,ThreeRate,FourRate,FiveRate,SixRate,OnePrizeWinCount,TwoPrizeWinCount,ThreePrizeWinCount,FourPrizeWinCount,FivePrizeWinCount,SixPrizeWinCount,RotateName,StartTime,EndTime,RotateRemark,RotateCount,PersonTotalCount,PersonDayCount,OnePrizeName,OnePrizeCount,TwoPrizeName,TwoPrizeCount,ThreePrizeName,ThreePrizeCount,FourPrizeName,FourPrizeCount,FivePrizeName,FivePrizeCount,SixPrizeName,SixPrizeCount,CreateTime,CreateUserID)");
			strSql.Append(" values (");
			strSql.Append("@IsWinOne,@OneName,@TwoName,@ThreeName,@FourName,@FiveName,@SixName,@OneMobile,@TwoMobile,@ThreeMobile,@FourMobile,@FiveMobile,@SixMobile,@ImageUrl,@RotateRegion,@OneRate,@TwoRate,@ThreeRate,@FourRate,@FiveRate,@SixRate,@OnePrizeWinCount,@TwoPrizeWinCount,@ThreePrizeWinCount,@FourPrizeWinCount,@FivePrizeWinCount,@SixPrizeWinCount,@RotateName,@StartTime,@EndTime,@RotateRemark,@RotateCount,@PersonTotalCount,@PersonDayCount,@OnePrizeName,@OnePrizeCount,@TwoPrizeName,@TwoPrizeCount,@ThreePrizeName,@ThreePrizeCount,@FourPrizeName,@FourPrizeCount,@FivePrizeName,@FivePrizeCount,@SixPrizeName,@SixPrizeCount,@CreateTime,@CreateUserID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateName", SqlDbType.NVarChar, 50),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@RotateRemark", SqlDbType.NVarChar, 2000),
				new SqlParameter("@RotateCount", SqlDbType.Int, 4),
				new SqlParameter("@PersonTotalCount", SqlDbType.Int, 4),
				new SqlParameter("@PersonDayCount", SqlDbType.Int, 4),
				new SqlParameter("@OnePrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@OnePrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@TwoPrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@TwoPrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@ThreePrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@ThreePrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@FourPrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@FourPrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@FivePrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@FivePrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@SixPrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@SixPrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@OnePrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@TwoPrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@ThreePrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@FourPrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@FivePrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@SixPrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@OneRate", SqlDbType.Decimal, 2),
				new SqlParameter("@TwoRate", SqlDbType.Decimal, 2),
				new SqlParameter("@ThreeRate", SqlDbType.Decimal, 2),
				new SqlParameter("@FourRate", SqlDbType.Decimal, 2),
				new SqlParameter("@FiveRate", SqlDbType.Decimal, 2),
				new SqlParameter("@SixRate", SqlDbType.Decimal, 2),
				new SqlParameter("@RotateRegion", SqlDbType.VarChar, 50),
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 200),
				new SqlParameter("@OneMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@TwoMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@ThreeMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@FourMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@FiveMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@SixMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@OneName", SqlDbType.VarChar, 50),
				new SqlParameter("@TwoName", SqlDbType.VarChar, 50),
				new SqlParameter("@ThreeName", SqlDbType.VarChar, 50),
				new SqlParameter("@FourName", SqlDbType.VarChar, 50),
				new SqlParameter("@FiveName", SqlDbType.VarChar, 50),
				new SqlParameter("@SixName", SqlDbType.VarChar, 50),
				new SqlParameter("@IsWinOne", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.RotateName;
			parameters[1].Value = model.StartTime;
			parameters[2].Value = model.EndTime;
			parameters[3].Value = model.RotateRemark;
			parameters[4].Value = model.RotateCount;
			parameters[5].Value = model.PersonTotalCount;
			parameters[6].Value = model.PersonDayCount;
			parameters[7].Value = model.OnePrizeName;
			parameters[8].Value = model.OnePrizeCount;
			parameters[9].Value = model.TwoPrizeName;
			parameters[10].Value = model.TwoPrizeCount;
			parameters[11].Value = model.ThreePrizeName;
			parameters[12].Value = model.ThreePrizeCount;
			parameters[13].Value = model.FourPrizeName;
			parameters[14].Value = model.FourPrizeCount;
			parameters[15].Value = model.FivePrizeName;
			parameters[16].Value = model.FivePrizeCount;
			parameters[17].Value = model.SixPrizeName;
			parameters[18].Value = model.SixPrizeCount;
			parameters[19].Value = model.CreateTime;
			parameters[20].Value = model.CreateUserID;
			parameters[21].Value = model.OnePrizeWinCount;
			parameters[22].Value = model.TwoPrizeWinCount;
			parameters[23].Value = model.ThreePrizeWinCount;
			parameters[24].Value = model.FourPrizeWinCount;
			parameters[25].Value = model.FivePrizeWinCount;
			parameters[26].Value = model.SixPrizeWinCount;
			parameters[27].Value = model.OneRate;
			parameters[28].Value = model.TwoRate;
			parameters[29].Value = model.ThreeRate;
			parameters[30].Value = model.FourRate;
			parameters[31].Value = model.FiveRate;
			parameters[32].Value = model.SixRate;
			parameters[33].Value = model.RotateRegion;
			parameters[34].Value = model.ImageUrl;
			parameters[35].Value = model.OneMobile;
			parameters[36].Value = model.TwoMobile;
			parameters[37].Value = model.ThreeMobile;
			parameters[38].Value = model.FourMobile;
			parameters[39].Value = model.FiveMobile;
			parameters[40].Value = model.SixMobile;
			parameters[41].Value = model.OneName;
			parameters[42].Value = model.TwoName;
			parameters[43].Value = model.ThreeName;
			parameters[44].Value = model.FourName;
			parameters[45].Value = model.FiveName;
			parameters[46].Value = model.SixName;
			parameters[47].Value = model.IsWinOne;
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

		public int UpdateWinCount(Chain.Model.SysRotate model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysRotate set ");
			strSql.Append("OnePrizeWinCount=@OnePrizeWinCount,");
			strSql.Append("TwoPrizeWinCount=@TwoPrizeWinCount,");
			strSql.Append("ThreePrizeWinCount=@ThreePrizeWinCount,");
			strSql.Append("FourPrizeWinCount=@FourPrizeWinCount,");
			strSql.Append("FivePrizeWinCount=@FivePrizeWinCount,");
			strSql.Append("SixPrizeWinCount=@SixPrizeWinCount");
			strSql.Append(" where RotateID=@RotateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OnePrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@TwoPrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@ThreePrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@FourPrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@FivePrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@SixPrizeWinCount", SqlDbType.Int, 4),
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.OnePrizeWinCount;
			parameters[1].Value = model.TwoPrizeWinCount;
			parameters[2].Value = model.ThreePrizeWinCount;
			parameters[3].Value = model.FourPrizeWinCount;
			parameters[4].Value = model.FivePrizeWinCount;
			parameters[5].Value = model.SixPrizeWinCount;
			parameters[6].Value = model.RotateID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public int Update(Chain.Model.SysRotate model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysRotate set ");
			strSql.Append("IsWinOne=@IsWinOne,");
			strSql.Append("OneName=@OneName,");
			strSql.Append("TwoName=@TwoName,");
			strSql.Append("ThreeName=@ThreeName,");
			strSql.Append("FourName=@FourName,");
			strSql.Append("FiveName=@FiveName,");
			strSql.Append("SixName=@SixName,");
			strSql.Append("OneMobile=@OneMobile,");
			strSql.Append("TwoMobile=@TwoMobile,");
			strSql.Append("ThreeMobile=@ThreeMobile,");
			strSql.Append("FourMobile=@FourMobile,");
			strSql.Append("FiveMobile=@FiveMobile,");
			strSql.Append("SixMobile=@SixMobile,");
			strSql.Append("ImageUrl=@ImageUrl,");
			strSql.Append("RotateRegion=@RotateRegion,");
			strSql.Append("RotateName=@RotateName,");
			strSql.Append("StartTime=@StartTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("RotateRemark=@RotateRemark,");
			strSql.Append("RotateCount=@RotateCount,");
			strSql.Append("PersonTotalCount=@PersonTotalCount,");
			strSql.Append("PersonDayCount=@PersonDayCount,");
			strSql.Append("OnePrizeName=@OnePrizeName,");
			strSql.Append("OnePrizeCount=@OnePrizeCount,");
			strSql.Append("TwoPrizeName=@TwoPrizeName,");
			strSql.Append("TwoPrizeCount=@TwoPrizeCount,");
			strSql.Append("ThreePrizeName=@ThreePrizeName,");
			strSql.Append("ThreePrizeCount=@ThreePrizeCount,");
			strSql.Append("FourPrizeName=@FourPrizeName,");
			strSql.Append("FourPrizeCount=@FourPrizeCount,");
			strSql.Append("FivePrizeName=@FivePrizeName,");
			strSql.Append("FivePrizeCount=@FivePrizeCount,");
			strSql.Append("SixPrizeName=@SixPrizeName,");
			strSql.Append("SixPrizeCount=@SixPrizeCount,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("OneRate=@OneRate,");
			strSql.Append("TwoRate=@TwoRate,");
			strSql.Append("ThreeRate=@ThreeRate,");
			strSql.Append("FourRate=@FourRate,");
			strSql.Append("FiveRate=@FiveRate,");
			strSql.Append("SixRate=@SixRate");
			strSql.Append(" where RotateID=@RotateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateName", SqlDbType.NVarChar, 50),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@RotateRemark", SqlDbType.NVarChar, 2000),
				new SqlParameter("@RotateCount", SqlDbType.Int, 4),
				new SqlParameter("@PersonTotalCount", SqlDbType.Int, 4),
				new SqlParameter("@PersonDayCount", SqlDbType.Int, 4),
				new SqlParameter("@OnePrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@OnePrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@TwoPrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@TwoPrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@ThreePrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@ThreePrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@FourPrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@FourPrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@FivePrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@FivePrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@SixPrizeName", SqlDbType.VarChar, 50),
				new SqlParameter("@SixPrizeCount", SqlDbType.Int, 4),
				new SqlParameter("@RotateID", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@OneRate", SqlDbType.Decimal, 2),
				new SqlParameter("@TwoRate", SqlDbType.Decimal, 2),
				new SqlParameter("@ThreeRate", SqlDbType.Decimal, 2),
				new SqlParameter("@FourRate", SqlDbType.Decimal, 2),
				new SqlParameter("@FiveRate", SqlDbType.Decimal, 2),
				new SqlParameter("@SixRate", SqlDbType.Decimal, 2),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@RotateRegion", SqlDbType.VarChar, 50),
				new SqlParameter("@ImageUrl", SqlDbType.VarChar, 200),
				new SqlParameter("@OneMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@TwoMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@ThreeMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@FourMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@FiveMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@SixMobile", SqlDbType.VarChar, 2000),
				new SqlParameter("@OneName", SqlDbType.VarChar, 50),
				new SqlParameter("@TwoName", SqlDbType.VarChar, 50),
				new SqlParameter("@ThreeName", SqlDbType.VarChar, 50),
				new SqlParameter("@FourName", SqlDbType.VarChar, 50),
				new SqlParameter("@FiveName", SqlDbType.VarChar, 50),
				new SqlParameter("@SixName", SqlDbType.VarChar, 50),
				new SqlParameter("@IsWinOne", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.RotateName;
			parameters[1].Value = model.StartTime;
			parameters[2].Value = model.EndTime;
			parameters[3].Value = model.RotateRemark;
			parameters[4].Value = model.RotateCount;
			parameters[5].Value = model.PersonTotalCount;
			parameters[6].Value = model.PersonDayCount;
			parameters[7].Value = model.OnePrizeName;
			parameters[8].Value = model.OnePrizeCount;
			parameters[9].Value = model.TwoPrizeName;
			parameters[10].Value = model.TwoPrizeCount;
			parameters[11].Value = model.ThreePrizeName;
			parameters[12].Value = model.ThreePrizeCount;
			parameters[13].Value = model.FourPrizeName;
			parameters[14].Value = model.FourPrizeCount;
			parameters[15].Value = model.FivePrizeName;
			parameters[16].Value = model.FivePrizeCount;
			parameters[17].Value = model.SixPrizeName;
			parameters[18].Value = model.SixPrizeCount;
			parameters[19].Value = model.RotateID;
			parameters[20].Value = model.CreateTime;
			parameters[21].Value = model.OneRate;
			parameters[22].Value = model.TwoRate;
			parameters[23].Value = model.ThreeRate;
			parameters[24].Value = model.FourRate;
			parameters[25].Value = model.FiveRate;
			parameters[26].Value = model.SixRate;
			parameters[27].Value = model.CreateUserID;
			parameters[28].Value = model.RotateRegion;
			parameters[29].Value = model.ImageUrl;
			parameters[30].Value = model.OneMobile;
			parameters[31].Value = model.TwoMobile;
			parameters[32].Value = model.ThreeMobile;
			parameters[33].Value = model.FourMobile;
			parameters[34].Value = model.FiveMobile;
			parameters[35].Value = model.SixMobile;
			parameters[36].Value = model.OneName;
			parameters[37].Value = model.TwoName;
			parameters[38].Value = model.ThreeName;
			parameters[39].Value = model.FourName;
			parameters[40].Value = model.FiveName;
			parameters[41].Value = model.SixName;
			parameters[42].Value = model.IsWinOne;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public string GetRotateIDByRotateRegion(string RotateRegion)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 RotateID from SysRotate ");
			strSql.Append(" where RotateRegion=@RotateRegion");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateRegion", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = RotateRegion;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			string result;
			if (obj != null)
			{
				result = obj.ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}

		public int Delete(int RotateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysRotate ");
			strSql.Append(" where RotateID=@RotateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RotateID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public bool DeleteList(string RotateIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysRotate ");
			strSql.Append(" where RotateID in (" + RotateIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysRotate GetModelByDate()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 IsWinOne,RotateRegion,OneRate,TwoRate,ThreeRate,FourRate,FiveRate,SixRate,CreateTime,CreateUserID,OnePrizeWinCount,TwoPrizeWinCount,ThreePrizeWinCount,FourPrizeWinCount,FivePrizeWinCount,SixPrizeWinCount,RotateID,RotateName,StartTime,EndTime,RotateRemark,RotateCount,PersonTotalCount,PersonDayCount,OnePrizeName,OnePrizeCount,TwoPrizeName,TwoPrizeCount,ThreePrizeName,ThreePrizeCount,FourPrizeName,FourPrizeCount,FivePrizeName,FivePrizeCount,SixPrizeName,SixPrizeCount from SysRotate ");
			strSql.Append(" where convert(char(10),getdate())>=convert(char(10),startTime) and  convert(char(10),getdate())<=convert(char(10),EndTime)");
			Chain.Model.SysRotate model = new Chain.Model.SysRotate();
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			Chain.Model.SysRotate result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["IsWinOne"] != null && ds.Tables[0].Rows[0]["IsWinOne"].ToString() != "")
				{
					model.IsWinOne = int.Parse(ds.Tables[0].Rows[0]["IsWinOne"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateRegion"] != null && ds.Tables[0].Rows[0]["RotateRegion"].ToString() != "")
				{
					model.RotateRegion = ds.Tables[0].Rows[0]["RotateRegion"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OneRate"] != null && ds.Tables[0].Rows[0]["OneRate"].ToString() != "")
				{
					model.OneRate = decimal.Parse(ds.Tables[0].Rows[0]["OneRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TwoRate"] != null && ds.Tables[0].Rows[0]["TwoRate"].ToString() != "")
				{
					model.TwoRate = decimal.Parse(ds.Tables[0].Rows[0]["TwoRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ThreeRate"] != null && ds.Tables[0].Rows[0]["ThreeRate"].ToString() != "")
				{
					model.ThreeRate = decimal.Parse(ds.Tables[0].Rows[0]["ThreeRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FourRate"] != null && ds.Tables[0].Rows[0]["FourRate"].ToString() != "")
				{
					model.FourRate = decimal.Parse(ds.Tables[0].Rows[0]["FourRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FiveRate"] != null && ds.Tables[0].Rows[0]["FiveRate"].ToString() != "")
				{
					model.FiveRate = decimal.Parse(ds.Tables[0].Rows[0]["FiveRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SixRate"] != null && ds.Tables[0].Rows[0]["SixRate"].ToString() != "")
				{
					model.SixRate = decimal.Parse(ds.Tables[0].Rows[0]["SixRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
				{
					model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateUserID"] != null && ds.Tables[0].Rows[0]["CreateUserID"].ToString() != "")
				{
					model.CreateUserID = int.Parse(ds.Tables[0].Rows[0]["CreateUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OnePrizeWinCount"] != null && ds.Tables[0].Rows[0]["OnePrizeWinCount"].ToString() != "")
				{
					model.OnePrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["OnePrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TwoPrizeWinCount"] != null && ds.Tables[0].Rows[0]["TwoPrizeWinCount"].ToString() != "")
				{
					model.TwoPrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["TwoPrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ThreePrizeWinCount"] != null && ds.Tables[0].Rows[0]["ThreePrizeWinCount"].ToString() != "")
				{
					model.ThreePrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["ThreePrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FourPrizeWinCount"] != null && ds.Tables[0].Rows[0]["FourPrizeWinCount"].ToString() != "")
				{
					model.FourPrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["FourPrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FivePrizeWinCount"] != null && ds.Tables[0].Rows[0]["FivePrizeWinCount"].ToString() != "")
				{
					model.FivePrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["FivePrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SixPrizeWinCount"] != null && ds.Tables[0].Rows[0]["SixPrizeWinCount"].ToString() != "")
				{
					model.SixPrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["SixPrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateID"] != null && ds.Tables[0].Rows[0]["RotateID"].ToString() != "")
				{
					model.RotateID = int.Parse(ds.Tables[0].Rows[0]["RotateID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateName"] != null && ds.Tables[0].Rows[0]["RotateName"].ToString() != "")
				{
					model.RotateName = ds.Tables[0].Rows[0]["RotateName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
				{
					model.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EndTime"] != null && ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
				{
					model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateRemark"] != null && ds.Tables[0].Rows[0]["RotateRemark"].ToString() != "")
				{
					model.RotateRemark = ds.Tables[0].Rows[0]["RotateRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RotateID"] != null && ds.Tables[0].Rows[0]["RotateID"].ToString() != "")
				{
					model.RotateID = int.Parse(ds.Tables[0].Rows[0]["RotateID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateCount"] != null && ds.Tables[0].Rows[0]["RotateCount"].ToString() != "")
				{
					model.RotateCount = int.Parse(ds.Tables[0].Rows[0]["RotateCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PersonTotalCount"] != null && ds.Tables[0].Rows[0]["PersonTotalCount"].ToString() != "")
				{
					model.PersonTotalCount = int.Parse(ds.Tables[0].Rows[0]["PersonTotalCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PersonDayCount"] != null && ds.Tables[0].Rows[0]["PersonDayCount"].ToString() != "")
				{
					model.PersonDayCount = int.Parse(ds.Tables[0].Rows[0]["PersonDayCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OnePrizeName"] != null && ds.Tables[0].Rows[0]["OnePrizeName"].ToString() != "")
				{
					model.OnePrizeName = ds.Tables[0].Rows[0]["OnePrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OnePrizeCount"] != null && ds.Tables[0].Rows[0]["OnePrizeCount"].ToString() != "")
				{
					model.OnePrizeCount = int.Parse(ds.Tables[0].Rows[0]["OnePrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TwoPrizeName"] != null && ds.Tables[0].Rows[0]["TwoPrizeName"].ToString() != "")
				{
					model.TwoPrizeName = ds.Tables[0].Rows[0]["TwoPrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["TwoPrizeCount"] != null && ds.Tables[0].Rows[0]["TwoPrizeCount"].ToString() != "")
				{
					model.TwoPrizeCount = int.Parse(ds.Tables[0].Rows[0]["TwoPrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ThreePrizeName"] != null && ds.Tables[0].Rows[0]["ThreePrizeName"].ToString() != "")
				{
					model.ThreePrizeName = ds.Tables[0].Rows[0]["ThreePrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ThreePrizeCount"] != null && ds.Tables[0].Rows[0]["ThreePrizeCount"].ToString() != "")
				{
					model.ThreePrizeCount = int.Parse(ds.Tables[0].Rows[0]["ThreePrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FourPrizeName"] != null && ds.Tables[0].Rows[0]["FourPrizeName"].ToString() != "")
				{
					model.FourPrizeName = ds.Tables[0].Rows[0]["FourPrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FourPrizeCount"] != null && ds.Tables[0].Rows[0]["FourPrizeCount"].ToString() != "")
				{
					model.FourPrizeCount = int.Parse(ds.Tables[0].Rows[0]["FourPrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FivePrizeName"] != null && ds.Tables[0].Rows[0]["FivePrizeName"].ToString() != "")
				{
					model.FivePrizeName = ds.Tables[0].Rows[0]["OnePrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FivePrizeCount"] != null && ds.Tables[0].Rows[0]["FivePrizeCount"].ToString() != "")
				{
					model.FivePrizeCount = int.Parse(ds.Tables[0].Rows[0]["FivePrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SixPrizeName"] != null && ds.Tables[0].Rows[0]["SixPrizeName"].ToString() != "")
				{
					model.SixPrizeName = ds.Tables[0].Rows[0]["SixPrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SixPrizeCount"] != null && ds.Tables[0].Rows[0]["SixPrizeCount"].ToString() != "")
				{
					model.SixPrizeCount = int.Parse(ds.Tables[0].Rows[0]["SixPrizeCount"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.SysRotate GetModel(int RotateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 IsWinOne,OneName,TwoName,ThreeName,FourName,FiveName,SixName, OneMobile,TwoMobile,ThreeMobile,FourMobile,FiveMobile,SixMobile,ImageUrl,RotateRegion,OneRate,TwoRate,ThreeRate,FourRate,FiveRate,SixRate,CreateTime,CreateUserID,OnePrizeWinCount,TwoPrizeWinCount,ThreePrizeWinCount,FourPrizeWinCount,FivePrizeWinCount,SixPrizeWinCount,RotateID,RotateName,StartTime,EndTime,RotateRemark,RotateCount,PersonTotalCount,PersonDayCount,OnePrizeName,OnePrizeCount,TwoPrizeName,TwoPrizeCount,ThreePrizeName,ThreePrizeCount,FourPrizeName,FourPrizeCount,FivePrizeName,FivePrizeCount,SixPrizeName,SixPrizeCount from SysRotate ");
			strSql.Append(" where RotateID=@RotateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RotateID;
			Chain.Model.SysRotate model = new Chain.Model.SysRotate();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysRotate result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["IsWinOne"] != null && ds.Tables[0].Rows[0]["IsWinOne"].ToString() != "")
				{
					model.IsWinOne = int.Parse(ds.Tables[0].Rows[0]["IsWinOne"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OneName"] != null && ds.Tables[0].Rows[0]["OneName"].ToString() != "")
				{
					model.OneName = ds.Tables[0].Rows[0]["OneName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["TwoName"] != null && ds.Tables[0].Rows[0]["TwoName"].ToString() != "")
				{
					model.TwoName = ds.Tables[0].Rows[0]["TwoName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ThreeName"] != null && ds.Tables[0].Rows[0]["ThreeName"].ToString() != "")
				{
					model.ThreeName = ds.Tables[0].Rows[0]["ThreeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FourName"] != null && ds.Tables[0].Rows[0]["FourName"].ToString() != "")
				{
					model.FourName = ds.Tables[0].Rows[0]["FourName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FiveName"] != null && ds.Tables[0].Rows[0]["FiveName"].ToString() != "")
				{
					model.FiveName = ds.Tables[0].Rows[0]["FiveName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SixName"] != null && ds.Tables[0].Rows[0]["SixName"].ToString() != "")
				{
					model.SixName = ds.Tables[0].Rows[0]["SixName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OneMobile"] != null && ds.Tables[0].Rows[0]["OneMobile"].ToString() != "")
				{
					model.OneMobile = ds.Tables[0].Rows[0]["OneMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["TwoMobile"] != null && ds.Tables[0].Rows[0]["TwoMobile"].ToString() != "")
				{
					model.TwoMobile = ds.Tables[0].Rows[0]["TwoMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ThreeMobile"] != null && ds.Tables[0].Rows[0]["ThreeMobile"].ToString() != "")
				{
					model.ThreeMobile = ds.Tables[0].Rows[0]["ThreeMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FourMobile"] != null && ds.Tables[0].Rows[0]["FourMobile"].ToString() != "")
				{
					model.FourMobile = ds.Tables[0].Rows[0]["FourMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FiveMobile"] != null && ds.Tables[0].Rows[0]["FiveMobile"].ToString() != "")
				{
					model.FiveMobile = ds.Tables[0].Rows[0]["FiveMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SixMobile"] != null && ds.Tables[0].Rows[0]["SixMobile"].ToString() != "")
				{
					model.SixMobile = ds.Tables[0].Rows[0]["SixMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ImageUrl"] != null && ds.Tables[0].Rows[0]["ImageUrl"].ToString() != "")
				{
					model.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RotateRegion"] != null && ds.Tables[0].Rows[0]["RotateRegion"].ToString() != "")
				{
					model.RotateRegion = ds.Tables[0].Rows[0]["RotateRegion"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OneRate"] != null && ds.Tables[0].Rows[0]["OneRate"].ToString() != "")
				{
					model.OneRate = decimal.Parse(ds.Tables[0].Rows[0]["OneRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TwoRate"] != null && ds.Tables[0].Rows[0]["TwoRate"].ToString() != "")
				{
					model.TwoRate = decimal.Parse(ds.Tables[0].Rows[0]["TwoRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ThreeRate"] != null && ds.Tables[0].Rows[0]["ThreeRate"].ToString() != "")
				{
					model.ThreeRate = decimal.Parse(ds.Tables[0].Rows[0]["ThreeRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FourRate"] != null && ds.Tables[0].Rows[0]["FourRate"].ToString() != "")
				{
					model.FourRate = decimal.Parse(ds.Tables[0].Rows[0]["FourRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FiveRate"] != null && ds.Tables[0].Rows[0]["FiveRate"].ToString() != "")
				{
					model.FiveRate = decimal.Parse(ds.Tables[0].Rows[0]["FiveRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SixRate"] != null && ds.Tables[0].Rows[0]["SixRate"].ToString() != "")
				{
					model.SixRate = decimal.Parse(ds.Tables[0].Rows[0]["SixRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
				{
					model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateUserID"] != null && ds.Tables[0].Rows[0]["CreateUserID"].ToString() != "")
				{
					model.CreateUserID = int.Parse(ds.Tables[0].Rows[0]["CreateUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OnePrizeWinCount"] != null && ds.Tables[0].Rows[0]["OnePrizeWinCount"].ToString() != "")
				{
					model.OnePrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["OnePrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TwoPrizeWinCount"] != null && ds.Tables[0].Rows[0]["TwoPrizeWinCount"].ToString() != "")
				{
					model.TwoPrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["TwoPrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ThreePrizeWinCount"] != null && ds.Tables[0].Rows[0]["ThreePrizeWinCount"].ToString() != "")
				{
					model.ThreePrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["ThreePrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FourPrizeWinCount"] != null && ds.Tables[0].Rows[0]["FourPrizeWinCount"].ToString() != "")
				{
					model.FourPrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["FourPrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FivePrizeWinCount"] != null && ds.Tables[0].Rows[0]["FivePrizeWinCount"].ToString() != "")
				{
					model.FivePrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["FivePrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SixPrizeWinCount"] != null && ds.Tables[0].Rows[0]["SixPrizeWinCount"].ToString() != "")
				{
					model.SixPrizeWinCount = int.Parse(ds.Tables[0].Rows[0]["SixPrizeWinCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateID"] != null && ds.Tables[0].Rows[0]["RotateID"].ToString() != "")
				{
					model.RotateID = int.Parse(ds.Tables[0].Rows[0]["RotateID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateName"] != null && ds.Tables[0].Rows[0]["RotateName"].ToString() != "")
				{
					model.RotateName = ds.Tables[0].Rows[0]["RotateName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
				{
					model.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EndTime"] != null && ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
				{
					model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateRemark"] != null && ds.Tables[0].Rows[0]["RotateRemark"].ToString() != "")
				{
					model.RotateRemark = ds.Tables[0].Rows[0]["RotateRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RotateID"] != null && ds.Tables[0].Rows[0]["RotateID"].ToString() != "")
				{
					model.RotateID = int.Parse(ds.Tables[0].Rows[0]["RotateID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateCount"] != null && ds.Tables[0].Rows[0]["RotateCount"].ToString() != "")
				{
					model.RotateCount = int.Parse(ds.Tables[0].Rows[0]["RotateCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PersonTotalCount"] != null && ds.Tables[0].Rows[0]["PersonTotalCount"].ToString() != "")
				{
					model.PersonTotalCount = int.Parse(ds.Tables[0].Rows[0]["PersonTotalCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PersonDayCount"] != null && ds.Tables[0].Rows[0]["PersonDayCount"].ToString() != "")
				{
					model.PersonDayCount = int.Parse(ds.Tables[0].Rows[0]["PersonDayCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OnePrizeName"] != null && ds.Tables[0].Rows[0]["OnePrizeName"].ToString() != "")
				{
					model.OnePrizeName = ds.Tables[0].Rows[0]["OnePrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OnePrizeCount"] != null && ds.Tables[0].Rows[0]["OnePrizeCount"].ToString() != "")
				{
					model.OnePrizeCount = int.Parse(ds.Tables[0].Rows[0]["OnePrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TwoPrizeName"] != null && ds.Tables[0].Rows[0]["TwoPrizeName"].ToString() != "")
				{
					model.TwoPrizeName = ds.Tables[0].Rows[0]["TwoPrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["TwoPrizeCount"] != null && ds.Tables[0].Rows[0]["TwoPrizeCount"].ToString() != "")
				{
					model.TwoPrizeCount = int.Parse(ds.Tables[0].Rows[0]["TwoPrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ThreePrizeName"] != null && ds.Tables[0].Rows[0]["ThreePrizeName"].ToString() != "")
				{
					model.ThreePrizeName = ds.Tables[0].Rows[0]["ThreePrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ThreePrizeCount"] != null && ds.Tables[0].Rows[0]["ThreePrizeCount"].ToString() != "")
				{
					model.ThreePrizeCount = int.Parse(ds.Tables[0].Rows[0]["ThreePrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FourPrizeName"] != null && ds.Tables[0].Rows[0]["FourPrizeName"].ToString() != "")
				{
					model.FourPrizeName = ds.Tables[0].Rows[0]["FourPrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FourPrizeCount"] != null && ds.Tables[0].Rows[0]["FourPrizeCount"].ToString() != "")
				{
					model.FourPrizeCount = int.Parse(ds.Tables[0].Rows[0]["FourPrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FivePrizeName"] != null && ds.Tables[0].Rows[0]["FivePrizeName"].ToString() != "")
				{
					model.FivePrizeName = ds.Tables[0].Rows[0]["FivePrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["FivePrizeCount"] != null && ds.Tables[0].Rows[0]["FivePrizeCount"].ToString() != "")
				{
					model.FivePrizeCount = int.Parse(ds.Tables[0].Rows[0]["FivePrizeCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SixPrizeName"] != null && ds.Tables[0].Rows[0]["SixPrizeName"].ToString() != "")
				{
					model.SixPrizeName = ds.Tables[0].Rows[0]["SixPrizeName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SixPrizeCount"] != null && ds.Tables[0].Rows[0]["SixPrizeCount"].ToString() != "")
				{
					model.SixPrizeCount = int.Parse(ds.Tables[0].Rows[0]["SixPrizeCount"].ToString());
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
			strSql.Append("select * ");
			strSql.Append(" FROM SysRotate ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
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
			strSql.Append(" RotateID,LogUserID,LogActionID,LogType,LogDetail,LogShopID,LogCreateTime,LogIPAdress ");
			strSql.Append(" FROM SysRotate ");
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
			strSql.Append("select count(1) FROM SysRotate ");
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
				strSql.Append("order by T.RotateID desc");
			}
			strSql.Append(")AS Row, T.*  from SysRotate T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "SysRotate,SysUser";
			string[] columns = new string[]
			{
				"SysRotate.*,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CreateTime", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetActionList(string MemCard)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 * from SysRotate");
			strSql.AppendFormat(" where LogDetail like '%' + '{0}' + '%' ", MemCard);
			strSql.Append(" and LogActionID>0");
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
