using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class Mem
	{
		public string GetWeiXinMemCardbyMemID(int MemID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select MemWeiXinCard from Mem");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemID;
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

		public int GetMemIDByMobile(string mobile)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 MemID from Mem ");
			strSql.Append(" where MemMobile=@MemMobile");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemMobile", SqlDbType.NVarChar, 20)
			};
			parameters[0].Value = mobile;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			int result;
			if (obj != null)
			{
				result = int.Parse(obj.ToString());
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("MemID", "Mem");
		}

		public bool Exists(int MemID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Mem");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool ExistsMemCard(string MemCard)
		{
			string strSql = string.Format("select count(1) from Mem where MemCard = '{0}'", MemCard);
			return DbHelperSQL.Exists(strSql.ToString());
		}

		public int Exists(int MemID, string MemCard, string MemMobile, string MemCardNumber,int MemShopID)
		{
			StringBuilder Sb = new StringBuilder();
			Sb.Append("DECLARE @MemID INT;");
			Sb.Append("DECLARE @MemMobile NVARCHAR(50);");
			Sb.Append("DECLARE @MemCardNumber NVARCHAR(50);");
			Sb.Append("DECLARE @MemCard NVARCHAR(50);");
			Sb.AppendFormat("SET @MemID='{0}';", MemID);
			Sb.AppendFormat("SET @MemMobile='{0}';", MemMobile);
			Sb.AppendFormat("SET @MemCardNumber='{0}';", MemCardNumber);
			Sb.AppendFormat("SET @MemCard='{0}';", MemCard);
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(MemMobile))
			{
				sb.AppendFormat("'{0}',", MemMobile);
			}
			if (!string.IsNullOrEmpty(MemCardNumber))
			{
				sb.AppendFormat("'{0}',", MemCardNumber);
			}
			if (!string.IsNullOrEmpty(MemCard))
			{
				sb.AppendFormat("'{0}',", MemCard);
			}
			string info = "";
			if (sb.Length > 0)
			{
				info = sb.ToString().Trim(new char[]
				{
					','
				});
			}
			if (MemID > 0)
			{
				Sb.AppendFormat(" IF EXISTS(SELECT 1 FROM dbo.Mem WHERE MemCard IN ({0}) AND MemID<>@MemID AND MemShopID={1}) ", info,MemShopID);
				Sb.Append(" BEGIN ");
				Sb.Append("     SELECT -1 ");
				Sb.Append(" END ");
				Sb.AppendFormat(" ELSE IF EXISTS(SELECT 1 FROM dbo.Mem WHERE  MemMobile IN ({0}) AND MemID<>@MemID AND MemShopID={1}) ", info, MemShopID);
				Sb.Append(" BEGIN ");
				Sb.Append("     SELECT -2 ");
				Sb.Append(" END ");
				Sb.AppendFormat(" ELSE IF EXISTS(SELECT 1 FROM dbo.Mem WHERE MemCardNumber IN ({0}) AND MemID<>@MemID AND MemShopID={1} ) ", info, MemShopID);
				Sb.Append(" BEGIN ");
				Sb.Append("     SELECT -6 ");
				Sb.Append(" END ");
			}
			else
			{
				Sb.AppendFormat(" IF EXISTS(SELECT 1 FROM dbo.Mem WHERE MemCard IN ({0}) AND MemShopID={1}) ", info, MemShopID);
				Sb.Append(" BEGIN ");
				Sb.Append("     SELECT -1 ");
				Sb.Append(" END ");
				Sb.AppendFormat(" ELSE IF EXISTS(SELECT 1 FROM dbo.Mem WHERE  MemMobile IN ({0}) AND MemShopID={1}) ", info, MemShopID);
				Sb.Append(" BEGIN ");
				Sb.Append("     SELECT -2 ");
				Sb.Append(" END ");
				Sb.AppendFormat(" ELSE IF EXISTS(SELECT 1 FROM dbo.Mem WHERE MemCardNumber IN ({0}) AND MemShopID={1}) ", info, MemShopID);
				Sb.Append(" BEGIN ");
				Sb.Append("     SELECT -6 ");
				Sb.Append(" END ");
			}
			DataSet ds = DbHelperSQL.Query(Sb.ToString());
			DataTable dt = (ds.Tables.Count > 0) ? ds.Tables[0] : new DataTable();
			int result;
			if (dt.Rows.Count > 0)
			{
				result = (int)dt.Rows[0][0];
			}
			else
			{
				result = 1;
			}
			return result;
		}

		public DataSet GetItemAll(int MemID)
		{
			string sql_mem = " select * from Mem where MemID =" + MemID;
			return DbHelperSQL.Query(sql_mem);
		}

		public int Add(Chain.Model.Mem model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Mem(");
			strSql.Append("MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemWeiXinCard,MemCardNumber,MemAttention)");
			strSql.Append(" values (");
			strSql.Append("@MemCard,@MemPassword,@MemName,@MemSex,@MemIdentityCard,@MemMobile,@MemPhoto,@MemBirthdayType,@MemBirthday,@MemIsPast,@MemPastTime,@MemPoint,@MemPointAutomatic,@MemMoney,@MemEmail,@MemAddress,@MemState,@MemRecommendID,@MemLevelID,@MemShopID,@MemCreateTime,@MemRemark,@MemUserID,@MemTelePhone,@MemQRCode,@MemProvince,@MemCity,@MemCounty,@MemVillage,@MemQuestion,@MemAnswer,@MemWeiXinCard,@MemCardNumber,@MemAttention)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPassword", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemSex", SqlDbType.Bit, 1),
				new SqlParameter("@MemIdentityCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@MemBirthdayType", SqlDbType.Bit, 1),
				new SqlParameter("@MemBirthday", SqlDbType.DateTime),
				new SqlParameter("@MemIsPast", SqlDbType.Bit, 1),
				new SqlParameter("@MemPastTime", SqlDbType.DateTime),
				new SqlParameter("@MemPoint", SqlDbType.Int, 4),
				new SqlParameter("@MemPointAutomatic", SqlDbType.Bit, 1),
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemEmail", SqlDbType.VarChar, 50),
				new SqlParameter("@MemAddress", SqlDbType.NVarChar, 200),
				new SqlParameter("@MemState", SqlDbType.Int, 1),
				new SqlParameter("@MemRecommendID", SqlDbType.Int, 4),
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@MemShopID", SqlDbType.Int, 4),
				new SqlParameter("@MemCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MemRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@MemUserID", SqlDbType.Int, 4),
				new SqlParameter("@MemTelePhone", SqlDbType.VarChar, 50),
				new SqlParameter("@MemQRCode", SqlDbType.VarChar, 500),
				new SqlParameter("@MemProvince", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCity", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCounty", SqlDbType.VarChar, 50),
				new SqlParameter("@MemVillage", SqlDbType.VarChar, 50),
				new SqlParameter("@MemQuestion", SqlDbType.VarChar, 500),
				new SqlParameter("@MemAnswer", SqlDbType.VarChar, 500),
				new SqlParameter("@MemWeiXinCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemCardNumber", SqlDbType.VarChar, 50),
				new SqlParameter("@MemAttention", SqlDbType.Int)
			};
			parameters[0].Value = model.MemCard;
			parameters[1].Value = model.MemPassword;
			parameters[2].Value = model.MemName;
			parameters[3].Value = model.MemSex;
			parameters[4].Value = model.MemIdentityCard;
			parameters[5].Value = model.MemMobile;
			parameters[6].Value = model.MemPhoto;
			parameters[7].Value = model.MemBirthdayType;
			parameters[8].Value = model.MemBirthday;
			parameters[9].Value = model.MemIsPast;
			parameters[10].Value = model.MemPastTime;
			parameters[11].Value = model.MemPoint;
			parameters[12].Value = model.MemPointAutomatic;
			parameters[13].Value = model.MemMoney;
			parameters[14].Value = model.MemEmail;
			parameters[15].Value = model.MemAddress;
			parameters[16].Value = model.MemState;
			parameters[17].Value = model.MemRecommendID;
			parameters[18].Value = model.MemLevelID;
			parameters[19].Value = model.MemShopID;
			parameters[20].Value = model.MemCreateTime;
			parameters[21].Value = model.MemRemark;
			parameters[22].Value = model.MemUserID;
			parameters[23].Value = model.MemTelePhone;
			parameters[24].Value = model.MemQRCode;
			parameters[25].Value = model.MemProvince;
			parameters[26].Value = model.MemCity;
			parameters[27].Value = model.MemCounty;
			parameters[28].Value = model.MemVillage;
			parameters[29].Value = model.MemQuestion;
			parameters[30].Value = model.MemAnswer;
			parameters[31].Value = model.MemWeiXinCard;
			parameters[32].Value = model.MemCardNumber;
			parameters[33].Value = model.MemAttention;
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

		public int MemRegister(string CardID, string LevelID, int userid, int shopid, string phone, string name)
		{
			string strSql = string.Concat(new object[]
			{
				"INSERT [Mem] ([MemCard] , [MemPassword] , [MemName] , [MemSex] , [MemIdentityCard] , [MemMobile] , [MemPhoto] , [MemBirthdayType] , [MemBirthday] , [MemIsPast] , [MemPastTime] , [MemPoint] , [MemPointAutomatic] , [MemMoney] , [MemEmail] , [MemAddress] , [MemState] , [MemRecommendID] , [MemLevelID] , [MemShopID] , [MemCreateTime] , [MemRemark] , [MemUserID] , [MemTelePhone] , [MemQRCode] , [MemProvince] , [MemCity] , [MemCounty] , [MemVillage] , [MemCardNumber] ) VALUES ('",
				CardID,
				"' , 'E62A9E6C1892C6BB' , '",
				name,
				"' , 1 , '' , '",
				phone,
				"' , '' , 1 , '1900-01-01 00:00:00.000' , 0 , '2900-01-01 00:00:00.000' , 0 , 1 , .0000 , '' , '' , 0 , 0 , ",
				LevelID,
				" , ",
				shopid,
				" , GETDATE() , '' , ",
				userid,
				" , '' , '' , '' , '' , '' , '' , '' )"
			});
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public int AddCustomField(string MemCard, Hashtable customhash)
		{
			MemCustomField custom = new MemCustomField();
			DataSet ds = custom.GetList(" CustomType=1");
			StringBuilder sbCustom = new StringBuilder();
			if (ds.Tables[0].Rows.Count > 0)
			{
				sbCustom.Append(" update Mem set ");
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
				sbCustom.AppendFormat(" where MemCard='{0}'", MemCard);
			}
			return DbHelperSQL.ExecuteSql(sbCustom.ToString());
		}

		public int Update(Chain.Model.Mem model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Mem set ");
			strSql.Append("MemCard=@MemCard,");
			strSql.Append("MemName=@MemName,");
			strSql.Append("MemSex=@MemSex,");
			strSql.Append("MemIdentityCard=@MemIdentityCard,");
			strSql.Append("MemMobile=@MemMobile,");
			strSql.Append("MemPhoto=@MemPhoto,");
			strSql.Append("MemBirthday=@MemBirthday,");
			strSql.Append("MemIsPast=@MemIsPast,");
			strSql.Append("MemPastTime=@MemPastTime,");
			strSql.Append("MemPoint=@MemPoint,");
			strSql.Append("MemMoney=@MemMoney,");
			strSql.Append("MemEmail=@MemEmail,");
			strSql.Append("MemAddress=@MemAddress,");
			strSql.Append("MemState=@MemState,");
			strSql.Append("MemRecommendID=@MemRecommendID,");
			strSql.Append("MemLevelID=@MemLevelID,");
			strSql.Append("MemShopID=@MemShopID,");
			strSql.Append("MemCreateTime=@MemCreateTime,");
			strSql.Append("MemRemark=@MemRemark,");
			strSql.Append("MemConsumeMoney=@MemConsumeMoney,");
			strSql.Append("MemUserID=@MemUserID,");
			strSql.Append("MemTelePhone=@MemTelePhone,");
			strSql.Append("MemProvince=@MemProvince,");
			strSql.Append("MemCity=@MemCity,");
			strSql.Append("MemCounty=@MemCounty,");
			strSql.Append("MemVillage=@MemVillage,");
			strSql.Append("MemQuestion=@MemQuestion,");
			strSql.Append("MemAnswer=@MemAnswer,");
			strSql.Append("MemWeiXinCard=@MemWeiXinCard,");
			strSql.Append("MemCardNumber=@MemCardNumber,");
			strSql.Append("MemWeiXinCards=@MemWeiXinCards,");
			strSql.Append("MemAttention=@MemAttention");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemSex", SqlDbType.Bit, 1),
				new SqlParameter("@MemIdentityCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@MemBirthday", SqlDbType.DateTime),
				new SqlParameter("@MemIsPast", SqlDbType.Bit, 1),
				new SqlParameter("@MemPastTime", SqlDbType.DateTime),
				new SqlParameter("@MemPoint", SqlDbType.Int, 4),
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemEmail", SqlDbType.VarChar, 50),
				new SqlParameter("@MemAddress", SqlDbType.NVarChar, 200),
				new SqlParameter("@MemState", SqlDbType.TinyInt, 1),
				new SqlParameter("@MemRecommendID", SqlDbType.Int, 4),
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@MemShopID", SqlDbType.Int, 4),
				new SqlParameter("@MemCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MemRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@MemConsumeMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemUserID", SqlDbType.Int, 4),
				new SqlParameter("@MemTelePhone", SqlDbType.VarChar, 50),
				new SqlParameter("@MemProvince", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCity", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCounty", SqlDbType.VarChar, 50),
				new SqlParameter("@MemVillage", SqlDbType.VarChar, 50),
				new SqlParameter("@MemQuestion", SqlDbType.VarChar, 500),
				new SqlParameter("@MemAnswer", SqlDbType.VarChar, 500),
				new SqlParameter("@MemWeiXinCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemCardNumber", SqlDbType.VarChar, 50),
				new SqlParameter("@MemWeiXinCards", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemAttention", SqlDbType.Int),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MemCard;
			parameters[1].Value = model.MemName;
			parameters[2].Value = model.MemSex;
			parameters[3].Value = model.MemIdentityCard;
			parameters[4].Value = model.MemMobile;
			parameters[5].Value = model.MemPhoto;
			parameters[6].Value = model.MemBirthday;
			parameters[7].Value = model.MemIsPast;
			parameters[8].Value = model.MemPastTime;
			parameters[9].Value = model.MemPoint;
			parameters[10].Value = model.MemMoney;
			parameters[11].Value = model.MemEmail;
			parameters[12].Value = model.MemAddress;
			parameters[13].Value = model.MemState;
			parameters[14].Value = model.MemRecommendID;
			parameters[15].Value = model.MemLevelID;
			parameters[16].Value = model.MemShopID;
			parameters[17].Value = model.MemCreateTime;
			parameters[18].Value = model.MemRemark;
			parameters[19].Value = model.MemConsumeMoney;
			parameters[20].Value = model.MemUserID;
			parameters[21].Value = model.MemTelePhone;
			parameters[22].Value = model.MemProvince;
			parameters[23].Value = model.MemCity;
			parameters[24].Value = model.MemCounty;
			parameters[25].Value = model.MemVillage;
			parameters[26].Value = model.MemQuestion;
			parameters[27].Value = model.MemAnswer;
			parameters[28].Value = model.MemWeiXinCard;
			parameters[29].Value = model.MemCardNumber;
			parameters[30].Value = model.MemWeiXinCards;
			parameters[31].Value = model.MemAttention;
			parameters[32].Value = model.MemID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int UpdateMemSelf(Chain.Model.Mem model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Mem set ");
			strSql.Append("MemIdentityCard=@MemIdentityCard,");
			strSql.Append("MemMobile=@MemMobile,");
			strSql.Append("MemBirthday=@MemBirthday,");
			strSql.Append("MemMoney=@MemMoney,");
			strSql.Append("MemEmail=@MemEmail,");
			strSql.Append("MemAddress=@MemAddress,");
			strSql.Append("MemRecommendID=@MemRecommendID,");
			strSql.Append("MemRemark=@MemRemark,");
			strSql.Append("MemTelePhone=@MemTelePhone,");
			strSql.Append("MemPhoto=@MemPhoto");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemIdentityCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@MemBirthday", SqlDbType.DateTime),
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemEmail", SqlDbType.VarChar, 50),
				new SqlParameter("@MemAddress", SqlDbType.NVarChar, 200),
				new SqlParameter("@MemRecommendID", SqlDbType.Int, 4),
				new SqlParameter("@MemRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@MemTelePhone", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MemIdentityCard;
			parameters[1].Value = model.MemMobile;
			parameters[2].Value = model.MemBirthday;
			parameters[3].Value = model.MemMoney;
			parameters[4].Value = model.MemEmail;
			parameters[5].Value = model.MemAddress;
			parameters[6].Value = model.MemRecommendID;
			parameters[7].Value = model.MemRemark;
			parameters[8].Value = model.MemTelePhone;
			parameters[9].Value = model.MemPhoto;
			parameters[10].Value = model.MemID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int ExpenseUpdateMem(int memID, decimal dclMemMoney, decimal memConsumeMoney, int point, DateTime dtime, int count)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Mem set ");
			strSql.Append("MemMoney=@MemMoney,");
			strSql.Append("MemConsumeMoney=@MemConsumeMoney,");
			strSql.Append("MemPoint=@MemPoint,");
			strSql.Append("MemConsumeLastTime=@MemConsumeLastTime,");
			strSql.Append("MemConsumeCount=@MemConsumeCount");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemConsumeMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemPoint", SqlDbType.Int, 4),
				new SqlParameter("@MemConsumeLastTime", SqlDbType.DateTime),
				new SqlParameter("@MemConsumeCount", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = dclMemMoney;
			parameters[1].Value = memConsumeMoney;
			parameters[2].Value = point;
			parameters[3].Value = dtime;
			parameters[4].Value = count;
			parameters[5].Value = memID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int MemCountUpdateMem(int memID, decimal dclMemMoney, int point)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Mem set ");
			strSql.Append("MemMoney=@MemMoney,");
			strSql.Append("MemPoint=@MemPoint");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemPoint", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = dclMemMoney;
			parameters[1].Value = point;
			parameters[2].Value = memID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public bool Delete(int MemID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Mem ");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public DataSet IsCanDelMem(int MemID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int)
			};
			parameters[0].Value = MemID;
			return DbHelperSQL.RunProcedure("MemIsCanDel", parameters, "#MemIsCanDel");
		}

		public bool DeleteList(string MemIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Mem ");
			strSql.Append(" where MemID in (" + MemIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Mem GetModel(int MemID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 *");
			strSql.Append(" from Mem");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemID;
			Chain.Model.Mem model = new Chain.Model.Mem();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Mem result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.Mem GetModelByMemCard(string MemCard)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 *");
			strSql.Append(" from Mem");
			strSql.Append(" where MemCard=@MemCard");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = MemCard;
			Chain.Model.Mem model = new Chain.Model.Mem();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Mem result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
                strSql.Remove(0, strSql.Length);
                strSql.Append(" select top 1 *");
                strSql.Append(" from Mem");
                strSql.Append(" where isnull(memcardnumber,'')=@MemCard");
                DataSet ds1 = DbHelperSQL.Query(strSql.ToString(), parameters);

                if (ds1.Tables[0].Rows.Count > 0)
                    result = DataRowToModel(ds1.Tables[0].Rows[0]);
                else
                    result = null;
			}
			return result;
		}

		public DataTable GetMemAdressBymencard(string MemCard)
		{
			StringBuilder strSql = new StringBuilder();
			string sql = string.Format("select (select Name from SysArea where Mem.MemProvince=SysArea.ID) as MemProvinceName,(select Name from SysArea where Mem.MemCity=SysArea.ID) as MemCityName,(select Name from SysArea where Mem.MemCounty=SysArea.ID) as MemCountyName,(select Name from SysArea where Mem.MemVillage=SysArea.ID) as MemVillageName,MemAddress from  Mem where MemCard='{0}'", MemCard);
			strSql.Append(sql);
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			DataTable result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = ds.Tables[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.Mem GetMemInfoByMobile(string Mobile)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 *");
			strSql.Append(" from Mem");
			strSql.Append(" where MemMobile=@MemMobile");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemMobile", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = Mobile;
			Chain.Model.Mem model = new Chain.Model.Mem();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Mem result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.Mem DataRowToModel(DataRow row)
		{
			Chain.Model.Mem model = new Chain.Model.Mem();
			if (row != null)
			{
				if (row["MemAttention"] != null && row["MemAttention"].ToString() != "")
				{
					model.MemAttention = int.Parse(row["MemAttention"].ToString());
				}
				if (row["MemWeiXinCards"] != null && row["MemWeiXinCards"].ToString() != "")
				{
					model.MemWeiXinCards = row["MemWeiXinCards"].ToString();
				}
				if (row["MemID"] != null && row["MemID"].ToString() != "")
				{
					model.MemID = int.Parse(row["MemID"].ToString());
				}
				if (row["MemCard"] != null)
				{
					model.MemCard = row["MemCard"].ToString();
				}
				if (row["MemPassword"] != null)
				{
					model.MemPassword = row["MemPassword"].ToString();
				}
				if (row["MemName"] != null)
				{
					model.MemName = row["MemName"].ToString();
				}
				if (row["MemSex"] != null && row["MemSex"].ToString() != "")
				{
					if (row["MemSex"].ToString() == "1" || row["MemSex"].ToString().ToLower() == "true")
					{
						model.MemSex = true;
					}
					else
					{
						model.MemSex = false;
					}
				}
				if (row["MemIdentityCard"] != null)
				{
					model.MemIdentityCard = row["MemIdentityCard"].ToString();
				}
				if (row["MemMobile"] != null)
				{
					model.MemMobile = row["MemMobile"].ToString();
				}
				if (row["MemPhoto"] != null)
				{
					model.MemPhoto = row["MemPhoto"].ToString();
				}
				if (row["MemBirthdayType"] != null && row["MemBirthdayType"].ToString() != "")
				{
					if (row["MemBirthdayType"].ToString() == "1" || row["MemBirthdayType"].ToString().ToLower() == "true")
					{
						model.MemBirthdayType = true;
					}
					else
					{
						model.MemBirthdayType = false;
					}
				}
				if (row["MemBirthday"] != null && row["MemBirthday"].ToString() != "")
				{
					model.MemBirthday = DateTime.Parse(row["MemBirthday"].ToString());
				}
				if (row["MemIsPast"] != null && row["MemIsPast"].ToString() != "")
				{
					if (row["MemIsPast"].ToString() == "1" || row["MemIsPast"].ToString().ToLower() == "true")
					{
						model.MemIsPast = true;
					}
					else
					{
						model.MemIsPast = false;
					}
				}
				if (row["MemPastTime"] != null && row["MemPastTime"].ToString() != "")
				{
					model.MemPastTime = DateTime.Parse(row["MemPastTime"].ToString());
				}
				if (row["MemPoint"] != null && row["MemPoint"].ToString() != "")
				{
					model.MemPoint = int.Parse(row["MemPoint"].ToString());
				}
				if (row["MemPointAutomatic"] != null && row["MemPointAutomatic"].ToString() != "")
				{
					if (row["MemPointAutomatic"].ToString() == "1" || row["MemPointAutomatic"].ToString().ToLower() == "true")
					{
						model.MemPointAutomatic = true;
					}
					else
					{
						model.MemPointAutomatic = false;
					}
				}
				if (row["MemMoney"] != null && row["MemMoney"].ToString() != "")
				{
					model.MemMoney = decimal.Parse(row["MemMoney"].ToString());
				}
				if (row["MemConsumeMoney"] != null && row["MemConsumeMoney"].ToString() != "")
				{
					model.MemConsumeMoney = decimal.Parse(row["MemConsumeMoney"].ToString());
				}
				if (row["MemConsumeLastTime"] != null && row["MemConsumeLastTime"].ToString() != "")
				{
					model.MemConsumeLastTime = DateTime.Parse(row["MemConsumeLastTime"].ToString());
				}
				if (row["MemConsumeCount"] != null && row["MemConsumeCount"].ToString() != "")
				{
					model.MemConsumeCount = int.Parse(row["MemConsumeCount"].ToString());
				}
				if (row["MemEmail"] != null)
				{
					model.MemEmail = row["MemEmail"].ToString();
				}
				if (row["MemAddress"] != null)
				{
					model.MemAddress = row["MemAddress"].ToString();
				}
				if (row["MemState"] != null && row["MemState"].ToString() != "")
				{
					model.MemState = int.Parse(row["MemState"].ToString());
				}
				if (row["MemRecommendID"] != null && row["MemRecommendID"].ToString() != "")
				{
					model.MemRecommendID = int.Parse(row["MemRecommendID"].ToString());
				}
				if (row["MemLevelID"] != null && row["MemLevelID"].ToString() != "")
				{
					model.MemLevelID = int.Parse(row["MemLevelID"].ToString());
				}
				if (row["MemShopID"] != null && row["MemShopID"].ToString() != "")
				{
					model.MemShopID = int.Parse(row["MemShopID"].ToString());
				}
				if (row["MemCreateTime"] != null && row["MemCreateTime"].ToString() != "")
				{
					model.MemCreateTime = DateTime.Parse(row["MemCreateTime"].ToString());
				}
				if (row["MemUserID"] != null && row["MemUserID"].ToString() != "")
				{
					model.MemUserID = int.Parse(row["MemUserID"].ToString());
				}
				if (row["MemTelePhone"] != null)
				{
					model.MemTelePhone = row["MemTelePhone"].ToString();
				}
				if (row["MemRemark"] != null)
				{
					model.MemRemark = row["MemRemark"].ToString();
				}
				if (row["MemQRCode"] != null)
				{
					model.MemQRCode = row["MemQRCode"].ToString();
				}
				if (row["MemProvince"] != null && row["MemProvince"].ToString() != "")
				{
					model.MemProvince = row["MemProvince"].ToString();
				}
				if (row["MemCity"] != null && row["MemCity"].ToString() != "")
				{
					model.MemCity = row["MemCity"].ToString();
				}
				if (row["MemCounty"] != null && row["MemCounty"].ToString() != "")
				{
					model.MemCounty = row["MemCounty"].ToString();
				}
				if (row["MemVillage"] != null && row["MemVillage"].ToString() != "")
				{
					model.MemVillage = row["MemVillage"].ToString();
				}
				if (row["MemQuestion"] != null && row["MemQuestion"].ToString() != "")
				{
					model.MemQuestion = row["MemQuestion"].ToString();
				}
				if (row["MemAnswer"] != null && row["MemAnswer"].ToString() != "")
				{
					model.MemAnswer = row["MemAnswer"].ToString();
				}
				if (row["MemWeiXinCard"] != null && row["MemWeiXinCard"].ToString() != "")
				{
					model.MemWeiXinCard = row["MemWeiXinCard"].ToString();
				}
				if (row["MemCardNumber"] != null)
				{
					model.MemCardNumber = row["MemCardNumber"].ToString();
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select *");
			strSql.Append(" FROM Mem ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM Mem ");
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
			strSql.Append("select count(1) FROM Mem ");
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
				strSql.Append("order by T.MemID desc");
			}
			strSql.Append(")AS Row, T.*  from Mem T ");
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
			string tableName = "Mem,SysShop,MemLevel,SysUser,SysShopMemLevel";
			string[] columns = new string[]
			{
				"Mem.*,SysShop.ShopName,MemLevel.*,ClassDiscountPercent,ClassPointPercent,isnull(Mem.MemConsumeMoney,0) as ConsumeMoney, SysUser.UserName,(select Name from SysArea where Mem.MemProvince=SysArea.ID) as MemProvinceName,(select Name from SysArea where Mem.MemCity=SysArea.ID) as MemCityName,(select Name from SysArea where Mem.MemCounty=SysArea.ID) as MemCountyName,(select Name from SysArea where Mem.MemVillage=SysArea.ID) as MemVillageName,isnull((select sum(CountDetailNumber) from MemCountDetail where CountDetailMemID=Mem.MemID),0) as MemCountNumber,SysShopMemLevel.ClassRechargePointRate ",
				"(SELECT COUNT(1) FROM dbo.Mem ca WHERE ca.MemRecommendID=dbo.Mem.MemID AND ca.MemRecommendID>0 ) AS RecommendCount,ISNULL((SELECT SUM(ISNULL(PointNumber,0)) FROM dbo.PointLog cp WHERE EXISTS(SELECT 1 FROM dbo.Mem caa WHERE caa.MemRecommendID=dbo.Mem.MemID AND cp.PointGiveMemID=caa.MemID)),0) AS RecommendPoint"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemID", "MemID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetMemExpense(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_RptMemExpense";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemConsumeLastTime", "MemCard", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetPointRankList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Mem,SysShop,MemLevel";
			string[] columns = new string[]
			{
				"Mem.*,SysShop.ShopName,MemLevel.*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemPoint", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int DrawMoney(int memID, decimal money)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update Mem set ");
			strSql.Append(" MemMoney=MemMoney-@MemMoney");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = money;
			parameter[1].Value = memID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int ExistPwd(int MemID, string oldPwd)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat("select MemPassword from Mem where MemID={0}", MemID);
			string strPwd = DbHelperSQL.GetSingle(strSql.ToString()).ToString();
			int result;
			if (strPwd != oldPwd)
			{
				result = -1;
			}
			else
			{
				result = 1;
			}
			return result;
		}

		public int UpdateMemPwd(int memID, string newPwd)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update Mem set MemPassword=@MemPassword ");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemPassword", SqlDbType.VarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = newPwd;
			parameter[1].Value = memID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int UpdateMemState(int memID, int state)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update Mem set MemState=@MemState ");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemState", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = state;
			parameter[1].Value = memID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int UpdateMoney(decimal money, int memID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update Mem set MemMoney = MemMoney+@MemMoney ");
			strSql.Append(" where MemID=@MemID ");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = money;
			parameter[1].Value = memID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int UpdatePoint(int memID, int point)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update Mem set MemPoint = MemPoint+@MemPoint ");
			strSql.Append(" where MemID =@MemID ");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemPoint", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = point;
			parameter[1].Value = memID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public bool ChangeCard(Chain.Model.Mem modelMem, string newMemCard, string newPwd)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update mem set MemCard=@MemCard, ");
			strSql.Append(" [MemPassword]=@MemPassword");
			strSql.Append(" where MemID=@MemID ");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPassword", SqlDbType.VarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = newMemCard;
			parameter[1].Value = newPwd;
			parameter[2].Value = modelMem.MemID;
			int row = DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
			return row > 0;
		}

		public DataSet GetMemConsumeMoney(string strWhere)
		{
			string sql_mem = string.Format(" select sum(MemConsumeMoney)as MemConsumeMoney from Mem where {0}", strWhere);
			return DbHelperSQL.Query(sql_mem);
		}

		public void UpdateLevel(int memID, int levelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update Mem set MemLevelID =@MemLevelID ");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = levelID;
			parameter[1].Value = memID;
			DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int UpdateMemPastTime(int memID, DateTime pastTime)
		{
			StringBuilder strSql = new StringBuilder();
			Chain.Model.Mem modelMem = this.GetModel(memID);
			int result;
			if (modelMem.MemIsPast)
			{
				strSql.Append(" update Mem set MemPastTime =@MemPastTime,");
				strSql.Append(" MemIsPast=0");
				strSql.Append(" where MemID=@MemID");
				SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@MemPastTime", SqlDbType.DateTime),
					new SqlParameter("@MemID", SqlDbType.Int, 4)
				};
				parameter[0].Value = pastTime;
				parameter[1].Value = memID;
				result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
			}
			else
			{
				strSql.Append(" update Mem set MemPastTime =@MemPastTime");
				strSql.Append(" where MemID=@MemID");
				SqlParameter[] parameter = new SqlParameter[]
				{
					new SqlParameter("@MemPastTime", SqlDbType.DateTime),
					new SqlParameter("@MemID", SqlDbType.Int, 4)
				};
				parameter[0].Value = pastTime;
				parameter[1].Value = memID;
				result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
			}
			return result;
		}

		public int SysUpdateMemIsPast()
		{
			string sql_mem = " update Mem set MemIsPast = 1 where getdate()>= MemPastTime ";
			return DbHelperSQL.ExecuteSql(sql_mem);
		}

		public DataSet GetBirthdayList(int day, int shopID, int count)
		{
			StringBuilder sbSql = new StringBuilder();
			if (day > 0)
			{
				sbSql.AppendFormat("select top " + count + " Mem.*,(select ShopName from SysShop where Mem.MemShopID = SysShop.ShopID) as ShopName,(select LevelName from MemLevel where Mem.MemLevelID = MemLevel.LevelID) as LevelName from Mem where month(MemBirthday)=month(getdate()) and (day(MemBirthday)-day(getdate()))>0 and (day(MemBirthday)-day(getdate()))<={0}", day);
			}
			else if (day == 0)
			{
				sbSql.AppendFormat("select top " + count + " Mem.*,(select ShopName from SysShop where Mem.MemShopID = SysShop.ShopID) as ShopName,(select LevelName from MemLevel where Mem.MemLevelID = MemLevel.LevelID) as LevelName from Mem where month(MemBirthday)=month(getdate()) and day(MemBirthday)-day(getdate())={0}", day);
			}
			if (shopID != 1)
			{
				sbSql.AppendFormat(" and MemShopID={0}", shopID);
			}
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public DataSet GetBirthdayList(int day, int shopID)
		{
			StringBuilder sbSql = new StringBuilder();
			if (day > 0)
			{
				sbSql.AppendFormat("select Mem.*,(select ShopName from SysShop where Mem.MemShopID = SysShop.ShopID) as ShopName,(select LevelName from MemLevel where Mem.MemLevelID = MemLevel.LevelID) as LevelName from Mem where DATEDIFF(DAY,GETDATE(),DATEADD(YEAR,DATEDIFF(YEAR,MemBirthday,GETDATE()),MemBirthday)) BETWEEN 1 AND {0}", day);
			}
			else if (day == 0)
			{
				sbSql.Append("select Mem.*,(select ShopName from SysShop where Mem.MemShopID = SysShop.ShopID) as ShopName,(select LevelName from MemLevel where Mem.MemLevelID = MemLevel.LevelID) as LevelName from Mem where DATEDIFF(DAY,GETDATE(),DATEADD(YEAR,DATEDIFF(YEAR,MemBirthday,GETDATE()),MemBirthday))=0");
			}
			if (shopID != 1)
			{
				sbSql.AppendFormat(" and MemShopID={0}", shopID);
			}
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public DataSet GetMemPastTime(string strSql, int shopID, int count)
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select top " + count + " mem.*,SysShop.ShopName,MemLevel.LevelName from mem inner join SysShop on mem.MemShopID = SysShop.ShopID inner join MemLevel on Mem.MemLevelID = MemLevel.LevelID where MemID<>0 and MemPastTime <> '2900-01-01'");
			sbSql.Append(strSql);
			if (shopID != 1)
			{
				sbSql.AppendFormat(" and MemShopID={0}", shopID);
			}
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public DataSet GetMemPastTime(string strSql, int shopID)
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select mem.*,SysShop.ShopName,MemLevel.LevelName from mem inner join SysShop on mem.MemShopID = SysShop.ShopID inner join MemLevel on Mem.MemLevelID = MemLevel.LevelID where MemID<>0 and MemPastTime <> '2900-01-01'");
			sbSql.Append(strSql);
			if (shopID != 1)
			{
				sbSql.AppendFormat(" and MemShopID={0}", shopID);
			}
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public DataSet GetMemPointReset(string strSql, int type, int count)
		{
			StringBuilder sbSql = new StringBuilder();
			if (type == 0)
			{
				sbSql.Append(" select top " + count + " MemID,MemCard,MemName,MemPoint,MemMobile,MemPastTime,MemConsumeLastTime,MemConsumeCount,DATEDIFF(day,MemConsumeLastTime,getdate()) as CountingDown,");
			}
			else
			{
				sbSql.Append(" select top " + count + " MemID,MemCard,MemName,MemPoint,MemMobile,MemPastTime,MemConsumeLastTime,MemConsumeCount,DATEDIFF(day,MemConsumeLastTime,getdate()) as CountingDown,");
			}
			sbSql.Append(" (select ShopName from SysShop where Mem.MemShopID = SysShop.ShopID) as ShopName,(select LevelName from MemLevel where Mem.MemLevelID = MemLevel.LevelID) as LevelName");
			sbSql.Append(" from Mem where MemID<>0");
			if (strSql.ToString() != "")
			{
				sbSql.Append(" and " + strSql);
			}
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public DataSet GetMemPointReset(string strSql, int type)
		{
			StringBuilder sbSql = new StringBuilder();
			if (type == 0)
			{
				sbSql.Append(" select MemID,MemCard,MemName,MemPoint,MemMobile,MemPastTime,MemConsumeLastTime,MemConsumeCount,DATEDIFF(day,MemConsumeLastTime,getdate()) as CountingDown,");
			}
			else
			{
				sbSql.Append(" select MemID,MemCard,MemName,MemPoint,MemMobile,MemPastTime,MemConsumeLastTime,MemConsumeCount,DATEDIFF(day,MemConsumeLastTime,getdate()) as CountingDown,");
			}
			sbSql.Append(" (select ShopName from SysShop where Mem.MemShopID = SysShop.ShopID) as ShopName,(select LevelName from MemLevel where Mem.MemLevelID = MemLevel.LevelID) as LevelName");
			sbSql.Append(" from Mem where MemID<>0");
			if (strSql.ToString() != "")
			{
				sbSql.Append(" and " + strSql);
			}
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public bool ClearMemberPoint(int MemID)
		{
			string strSql = string.Format("update Mem  set MemPoint=0 where MemID={0}", MemID);
			DbHelperSQL.ExecuteSql(strSql);
			return true;
		}

		public DataSet GetMemExpense(int PageSize, int PageIndex, out int resCount, string strTime, params string[] strWhere)
		{
			string tableName = "Mem,SysShop";
			string[] columns = new string[]
			{
				"Mem.MemID,Mem.MemCard,Mem.MemName,Mem.MemMobile,Mem.MemConsumeCount,SysShop.ShopID,SysShop.ShopName,(select isnull(sum(OrderDiscountMoney),0) from OrderLog where Mem.MemID=OrderLog.OrderMemID and OrderType <> 4 and OrderType <> 5 and 1=1 ) as DiscountMoney"
			};
			columns[0] = columns[0].Replace("1=1", strTime);
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetMemExpenseToWeiXin(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_RptMemExpense";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "OrderCreateTime", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetMemExpenseDetail(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select * from V_RptMemExpense");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" Order by OrderCreateTime desc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public bool ExeclDataInput(ArrayList sqlArray)
		{
			return DbHelperSQL.ExecuteSqlTran(sqlArray);
		}

		public DataSet GetDataByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@strWhere", SqlDbType.NVarChar, 2000)
			};
			parameters[0].Value = starttime;
			parameters[1].Value = endtime;
			parameters[2].Value = strwhere;
			return DbHelperSQL.RunProcedure("MonthData", parameters, "#MyData");
		}

		public DataTable CheckMemPwd(string strAccount, string strPassword)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select  top 1 MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemWeiXinCard,MemCardNumber,MemWeiXinCards,MemAttention from Mem ");
			strSql.Append(" where (MemCard =@MemCard or MemMobile=@MemCard or MemCardNumber=@MemCard) and MemPassword=@MemPassword ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPassword", SqlDbType.VarChar, 200)
			};
			parameters[0].Value = strAccount;
			parameters[1].Value = strPassword;
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			return ds.Tables[0];
		}

		public int SecuritySettings(int intMemID, string strEmail, string strQuestion, string strAnswer)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Update Mem set");
			strSql.Append(" MemEmail=@MemEmail,");
			strSql.Append(" MemQuestion=@MemQuestion,");
			strSql.Append(" MemAnswer=@MemAnswer");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemEmail", SqlDbType.VarChar, 50),
				new SqlParameter("@MemQuestion", SqlDbType.VarChar, 500),
				new SqlParameter("@MemAnswer", SqlDbType.VarChar, 500),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = strEmail;
			parameters[1].Value = strQuestion;
			parameters[2].Value = strAnswer;
			parameters[3].Value = intMemID;
			object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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

		public int updateMemSyaArea(string strProvince, string strCity, string strCounty, string strVillage, int memId)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Update Mem set ");
			strSql.Append(" MemProvince=@MemProvince,");
			strSql.Append(" MemCity=@MemCity,");
			strSql.Append(" MemCounty=@MemCounty,");
			strSql.Append(" MemVillage=@MemVillage");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemProvince", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCity", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCounty", SqlDbType.VarChar, 50),
				new SqlParameter("@MemVillage", SqlDbType.VarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = strProvince;
			parameter[1].Value = strCity;
			parameter[2].Value = strCounty;
			parameter[3].Value = strVillage;
			parameter[4].Value = memId;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public DataSet WeiXinLogin(string WeiXinCard, string PassWord)
		{
            string sql = string.Format("select *  from mem  where (MemWeiXinCard='{0}' or MemCard='{0}' or MemMobile='{0}' or MemCardNumber='{0}') and MemPassword='{1}'", WeiXinCard, PassWord);
			DataSet ds = DbHelperSQL.Query(sql);
			DataSet result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = ds;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int WeiXinRegister(string MemName, string PassWord, string telnumber, string referrerMemId, string memWeiXinCard, int MemShopID)
		{
			string sql3 = string.Format("select MemMobile from mem where MemMobile='{0}'", telnumber);
			DataSet dsMemMobile = DbHelperSQL.Query(sql3);
			int result;
			if (dsMemMobile.Tables[0].Rows.Count > 0)
			{
				result = -1;
			}
			else
			{
				DateTime shijian = DateTime.Now;
				string sql4 = string.Format("insert  into mem (MemCard,MemName, MemPassword, MemMobile,MemBirthday,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemUserID,MemPastTime,MemWeiXinCard,MemWeiXinCards,MemAttention) values ('{0}','{1}','{2}','{3}','1900/1/1 0:00:00',0,'{4}',0,{7},'{5}',1,'2900/1/1 0:00:00','{6}','{6}',1)", new object[]
				{
					telnumber,
					MemName,
					PassWord,
					telnumber,
					referrerMemId,
					shijian,
					memWeiXinCard,
					MemShopID
				});
				string sql5 = string.Format("select MemID from mem where MemMobile='{0}' and MemPassword='{1}'", telnumber, PassWord);
				int nums = DbHelperSQL.ExecuteSql(sql4);
				if (nums > 0)
				{
					DataSet ds = DbHelperSQL.Query(sql5);
					result = Convert.ToInt32(ds.Tables[0].Rows[0]["MemID"]);
				}
				else
				{
					result = -2;
				}
			}
			return result;
		}

		public Chain.Model.Mem GetMemByWeiXinCard(string weiXinCard)
		{
			string sqlStr = "select * from Mem where MemWeiXinCard=@MemWeiXinCard";
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemWeiXinCard", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = weiXinCard;
			Chain.Model.Mem model = new Chain.Model.Mem();
			DataTable dt = DbHelperSQL.Query(sqlStr, parameters).Tables[0];
			Chain.Model.Mem result;
			if (dt.Rows.Count > 0)
			{
				result = this.DataRowToModel(dt.Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.Mem GetModelByMemMobile(string MemMobile)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 *");
			strSql.Append(" from Mem");
			strSql.Append(" where MemMobile=@MemMobile");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemMobile", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = MemMobile;
			Chain.Model.Mem model = new Chain.Model.Mem();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Mem result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetMemBirthday(int day)
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select * from Mem where  ");
			sbSql.Append("datepart(year,MemBirthday)!='1900' ");
			sbSql.Append("and datepart(month,getdate())=datepart(month,MemBirthday-@day) ");
			sbSql.Append("and datepart(day,MemBirthday-@day)=datepart(day,getdate()) ");
			sbSql.Append("and MemMobile!='' ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@day", SqlDbType.Int, 4)
			};
			parameters[0].Value = day;
			return DbHelperSQL.Query(sbSql.ToString(), parameters);
		}

		public DataSet GetMemPast(int day)
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select * from Mem where  ");
			sbSql.Append("datepart(year,MemPastTime-@day)=datepart(year,getdate()) ");
			sbSql.Append("and datepart(month,getdate())=datepart(month,MemPastTime-@day) ");
			sbSql.Append("and datepart(day,MemPastTime-@day)=datepart(day,getdate()) ");
			sbSql.Append("and MemMobile!='' ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@day", SqlDbType.Int, 4)
			};
			parameters[0].Value = day;
			return DbHelperSQL.Query(sbSql.ToString(), parameters);
		}

		public int GetMemCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select count(MemID) from Mem");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			int result;
			if (obj != null)
			{
				result = Convert.ToInt32(obj);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int AddMemFirst(Chain.Model.Mem model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Mem(");
			strSql.Append("MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemWeiXinCard,MemExpenseShop,MemCardNumber)");
			strSql.Append(" values (");
			strSql.Append("@MemCard,@MemPassword,@MemName,@MemSex,@MemIdentityCard,@MemMobile,@MemPhoto,@MemBirthdayType,@MemBirthday,@MemIsPast,@MemPastTime,@MemPoint,@MemPointAutomatic,@MemMoney,@MemEmail,@MemAddress,@MemState,@MemRecommendID,@MemLevelID,@MemShopID,@MemCreateTime,@MemRemark,@MemUserID,@MemTelePhone,@MemQRCode,@MemProvince,@MemCity,@MemCounty,@MemVillage,@MemQuestion,@MemAnswer,@MemWeiXinCard,@MemExpenseShop,@MemCardNumber)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPassword", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemSex", SqlDbType.Bit, 1),
				new SqlParameter("@MemIdentityCard", SqlDbType.VarChar, 50),
				new SqlParameter("@MemMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@MemBirthdayType", SqlDbType.Bit, 1),
				new SqlParameter("@MemBirthday", SqlDbType.DateTime),
				new SqlParameter("@MemIsPast", SqlDbType.Bit, 1),
				new SqlParameter("@MemPastTime", SqlDbType.DateTime),
				new SqlParameter("@MemPoint", SqlDbType.Int, 4),
				new SqlParameter("@MemPointAutomatic", SqlDbType.Bit, 1),
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MemEmail", SqlDbType.VarChar, 50),
				new SqlParameter("@MemAddress", SqlDbType.NVarChar, 200),
				new SqlParameter("@MemState", SqlDbType.TinyInt, 1),
				new SqlParameter("@MemRecommendID", SqlDbType.Int, 4),
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@MemShopID", SqlDbType.Int, 4),
				new SqlParameter("@MemCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MemRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@MemUserID", SqlDbType.Int, 4),
				new SqlParameter("@MemTelePhone", SqlDbType.VarChar, 50),
				new SqlParameter("@MemQRCode", SqlDbType.VarChar, 500),
				new SqlParameter("@MemProvince", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCity", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCounty", SqlDbType.VarChar, 50),
				new SqlParameter("@MemVillage", SqlDbType.VarChar, 50),
				new SqlParameter("@MemQuestion", SqlDbType.VarChar, 500),
				new SqlParameter("@MemAnswer", SqlDbType.VarChar, 500),
				new SqlParameter("@MemWeiXinCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemExpenseShop", SqlDbType.NVarChar),
				new SqlParameter("@MemCardNumber", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = model.MemCard;
			parameters[1].Value = model.MemPassword;
			parameters[2].Value = model.MemName;
			parameters[3].Value = model.MemSex;
			parameters[4].Value = model.MemIdentityCard;
			parameters[5].Value = model.MemMobile;
			parameters[6].Value = model.MemPhoto;
			parameters[7].Value = model.MemBirthdayType;
			parameters[8].Value = model.MemBirthday;
			parameters[9].Value = model.MemIsPast;
			parameters[10].Value = model.MemPastTime;
			parameters[11].Value = model.MemPoint;
			parameters[12].Value = model.MemPointAutomatic;
			parameters[13].Value = model.MemMoney;
			parameters[14].Value = model.MemEmail;
			parameters[15].Value = model.MemAddress;
			parameters[16].Value = model.MemState;
			parameters[17].Value = model.MemRecommendID;
			parameters[18].Value = model.MemLevelID;
			parameters[19].Value = model.MemShopID;
			parameters[20].Value = model.MemCreateTime;
			parameters[21].Value = model.MemRemark;
			parameters[22].Value = model.MemUserID;
			parameters[23].Value = model.MemTelePhone;
			parameters[24].Value = model.MemQRCode;
			parameters[25].Value = model.MemProvince;
			parameters[26].Value = model.MemCity;
			parameters[27].Value = model.MemCounty;
			parameters[28].Value = model.MemVillage;
			parameters[29].Value = model.MemQuestion;
			parameters[30].Value = model.MemAnswer;
			parameters[31].Value = model.MemWeiXinCard;
			parameters[32].Value = "";
			parameters[33].Value = model.MemCardNumber;
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

		public DataTable GetModelDetail(params string[] strWhere)
		{
			string tableName = "Mem,SysShop,MemLevel,SysUser,SysShopMemLevel";
			string[] columns = new string[]
			{
				"Mem.*,SysShop.ShopName,MemLevel.*,ClassDiscountPercent,ClassPointPercent,isnull(Mem.MemConsumeMoney,0) as ConsumeMoney, SysUser.UserName,(select Name from SysArea where Mem.MemProvince=SysArea.ID) as MemProvinceName,(select Name from SysArea where Mem.MemCity=SysArea.ID) as MemCityName,(select Name from SysArea where Mem.MemCounty=SysArea.ID) as MemCountyName,(select Name from SysArea where Mem.MemVillage=SysArea.ID) as MemVillageName,isnull((select sum(CountDetailNumber) from MemCountDetail where CountDetailMemID=Mem.MemID),0) as MemCountNumber,SysShopMemLevel.ClassRechargePointRate "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemID", "MemID", false, 1, 1, recordCount);
			return ds.Tables[0];
		}

		public Chain.Model.Mem GetModel(string memCard)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 *");
			strSql.Append(" from Mem");
			strSql.Append(" where MemCard=@MemCard");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = memCard;
			Chain.Model.Mem model = new Chain.Model.Mem();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Mem result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataTable getMemRecommend(object memid)
		{
			StringBuilder Sql = new StringBuilder();
			Sql.AppendFormat("SELECT ROW_NUMBER() OVER(ORDER BY MemID) AS Rowid ,ISNULL((SELECT SUM(ISNULL(PointNumber,0)) FROM dbo.PointLog WHERE PointMemID='{0}' AND PointGiveMemID=dbo.Mem.MemID),0) AS SumNumber,MemCard,MemName,MemMobile FROM dbo.Mem WHERE MemRecommendID='{0}' ORDER BY MemID ASC", memid);
			DataTable result;
			using (DataTable dt = DbHelperSQL.Query(Sql.ToString()).Tables[0])
			{
				result = dt;
			}
			return result;
		}

		public DataTable GetMemRecommendList(int memid)
		{
			StringBuilder Sql = new StringBuilder();
			Sql.AppendFormat("SELECT MemID, MemCard,MemName,MemMobile FROM dbo.Mem WHERE MemRecommendID='{0}' ORDER BY MemID ASC", memid);
			DataTable result;
			using (DataTable dt = DbHelperSQL.Query(Sql.ToString()).Tables[0])
			{
				result = dt;
			}
			return result;
		}

		public DataSet GetListS(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "Mem";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataTable getMemPayList(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "V_MemPay INNER JOIN dbo.Mem ON V_MemPay.MemID=dbo.Mem.MemID INNER JOIN dbo.SysUser ON MemUserID=UserID INNER JOIN dbo.SysShop ON MemShopID=ShopID";
			string[] columns = new string[]
			{
				"V_MemPay.*,MemCard,MemName,MemMobile,MemCreateTime,UserName,MemMoney,MemPoint,MemConsumeMoney,ShopName,MemState"
			};
			int recordCount = 1;
			DataTable result;
			using (DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "Mem.MemID", true, PageSize, PageIndex, recordCount))
			{
				resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
				result = ds.Tables[0];
			}
			return result;
		}

		public int UpdateMemWeiXinCard(int memID, string openid)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update Mem set MemWeiXinCards=@openid,MemWeiXinCard=@openid ");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@openid", SqlDbType.VarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = openid;
			parameter[1].Value = memID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int GetMemIDByWhere(string strwhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 MemID");
			strSql.Append(" from Mem");
			strSql.Append(" where ");
			strSql.Append(strwhere);
			Chain.Model.Mem model = new Chain.Model.Mem();
			object memid = DbHelperSQL.GetSingle(strSql.ToString());
			int result;
			if (memid != null)
			{
				int id = int.Parse(memid.ToString());
				result = id;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int WeiXinRegister(string MemCard, string MemName, string PassWord, string telnumber, string referrerMemId, string memWeiXinCard, int MemShopID, string hidtf)
		{
			string sql3 = string.Format("select MemMobile from mem where MemMobile='{0}'", telnumber);
			DataSet dsMemMobile = DbHelperSQL.Query(sql3);
			int result;
			if (dsMemMobile.Tables[0].Rows.Count > 0)
			{
				result = -1;
			}
			else
			{
				DateTime shijian = DateTime.Now;
				string sql4 = string.Format("insert  into mem (MemCard,MemName, MemPassword, MemMobile,MemBirthday,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemUserID,MemPastTime,MemWeiXinCard,MemWeiXinCards,MemAttention) values ('{0}','{1}','{2}','{3}','1900/1/1 0:00:00',0,'{4}',0,{7},'{5}',1,'2900/1/1 0:00:00','{6}','{6}',1)", new object[]
				{
					MemCard,
					MemName,
					PassWord,
					telnumber,
					referrerMemId,
					shijian,
					memWeiXinCard,
					MemShopID
				});
				if (hidtf == "0")
				{
					sql4 = string.Format("insert  into mem (MemCard,MemName, MemPassword, MemMobile,MemBirthday,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemUserID,MemPastTime,MemWeiXinCard,MemWeiXinCards,MemAttention) values ('{0}','{1}','{2}','{3}','1900/1/1 0:00:00',0,'{4}',0,{7},'{5}',1,'2900/1/1 0:00:00','{6}','{6}',1)", new object[]
					{
						MemCard,
						MemName,
						PassWord,
						telnumber,
						referrerMemId,
						shijian,
						memWeiXinCard,
						MemShopID
					});
				}
				else
				{
					sql4 = string.Format("insert  into mem (MemCard,MemName, MemPassword, MemMobile,MemBirthday,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemUserID,MemPastTime,MemWeiXinCard,MemWeiXinCards,MemAttention) values ('{0}','{1}','{2}','{3}','1900/1/1 0:00:00',0,'{4}',0,{7},'{5}',1,'2900/1/1 0:00:00','{6}','{6}',2)", new object[]
					{
						MemCard,
						MemName,
						PassWord,
						telnumber,
						referrerMemId,
						shijian,
						memWeiXinCard,
						MemShopID
					});
				}
				string sql5 = string.Format("select MemID from mem where MemMobile='{0}' and MemPassword='{1}'", telnumber, PassWord);
				int nums = DbHelperSQL.ExecuteSql(sql4);
				if (nums > 0)
				{
					DataSet ds = DbHelperSQL.Query(sql5);
					result = Convert.ToInt32(ds.Tables[0].Rows[0]["MemID"]);
				}
				else
				{
					result = -2;
				}
			}
			return result;
		}

		public DataSet GetMemBillList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM  V_MemAllExpense");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public Chain.Model.Mem GetMemWeiXinCardModel(string strMemWeiXinCard, string strAttributes)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 *");
			strSql.Append(" from Mem");
			strSql.AppendFormat(" where {0}=@{0}", strAttributes);
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter(string.Format("@{0}", strAttributes), SqlDbType.VarChar, 50)
			};
			parameters[0].Value = strMemWeiXinCard;
			Chain.Model.Mem model = new Chain.Model.Mem();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Mem result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
