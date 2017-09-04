using Chain.BLL;
using Chain.Common.DEncrypt;
using Chain.Model;
using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ChainStock.Service
{
	public class PDAService : IHttpHandler
	{
		private HttpRequest Request;

		private HttpResponse Response;

		private HttpServerUtility Server;

		private HttpSessionState Session;

		private HttpContext Context;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.Buffer = true;
			context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
			context.Response.AddHeader("pragma", "no-cache");
			context.Response.AddHeader("cache-control", "");
			context.Response.CacheControl = "no-cache";
			context.Response.ContentType = "application/json";
			context.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
			context.Response.Charset = "GB2312";
			this.Request = context.Request;
			this.Response = context.Response;
			this.Session = context.Session;
			this.Server = context.Server;
			this.Context = context;
			this.Request.ContentEncoding = Encoding.GetEncoding("GB2312");
			string method = this.Request["Method"].ToString();
			MethodInfo methodInfo = base.GetType().GetMethod(method);
			methodInfo.Invoke(this, null);
		}

		public void CheckUserLogin()
		{
			int flag = 1;
			Chain.Model.SysUser User = new Chain.Model.SysUser();
			try
			{
				string username = this.Request["account"].ToString();
				string password = this.Request["password"].ToString();
				User = PubFunction.CheckUserLogin(username.Trim(), password);
				if (User != null)
				{
					string authority = this.GetAuthority(User);
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(User.UserShopID);
					if (!modelShop.ShopState)
					{
						if (!User.UserLock)
						{
							Chain.Model.SysParameter modelParameter = new Chain.BLL.SysParameter().GetModel(1);
							string strResult = string.Empty;
							if (modelParameter.AccordPrint)
							{
								strResult = string.Format("{0}|{1}", modelParameter.PrintTitle, modelParameter.PrintFootNote);
							}
							else
							{
								strResult = string.Format("{0}|{1}", modelShop.ShopPrintTitle, modelShop.ShopPrintFoot);
							}
							this.Response.Write(strResult + "|" + authority);
							return;
						}
						flag = 4;
					}
					else
					{
						flag = 3;
					}
				}
				else
				{
					flag = 2;
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void GetServerTime()
		{
			this.Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		public void GetMemInfo()
		{
			StringBuilder sb = new StringBuilder();
			string memCard = this.Request["memcard"].ToString();
			Chain.Model.Mem memInfo = new Chain.BLL.Mem().GetModelByMemCard(memCard);
			if (memInfo == null)
			{
				memInfo = new Chain.BLL.Mem().GetMemInfoByMobile(memCard);
			}
			if (memInfo != null)
			{
				Chain.Model.MemLevel LevelModel = new Chain.BLL.MemLevel().GetModel(memInfo.MemLevelID);
				sb.Append(memInfo.MemCard);
				sb.Append("|");
				sb.Append((memInfo.MemName == "") ? "无" : memInfo.MemName);
				sb.Append("|");
				sb.Append(memInfo.MemMoney.ToString("0.00"));
				sb.Append("|");
				sb.Append(memInfo.MemPoint.ToString());
				sb.Append("|");
				sb.Append(LevelModel.LevelName);
				sb.Append("|");
				sb.Append(memInfo.MemIsPast ? memInfo.MemPastTime.ToShortDateString() : "未设置");
				sb.Append("|");
				sb.Append((DESEncrypt.Decrypt(memInfo.MemPassword) == "") ? "0" : "1");
			}
			else
			{
				sb.Append("2");
			}
			this.Response.Write(sb.ToString());
		}

		public void GetMemLevelByCard()
		{
			string flag = "1";
			if (this.Request["memcard"] != null)
			{
				if (!new Chain.BLL.Mem().ExistsMemCard(this.Request["memcard"].ToString()))
				{
					DataTable dt = new Chain.BLL.MemLevel().GetList("").Tables[0];
					if (dt.Rows.Count > 0)
					{
						flag = dt.Rows[0]["LevelID"].ToString() + "|" + dt.Rows[0]["LevelName"].ToString();
					}
				}
				else
				{
					flag = "2";
				}
			}
			this.Response.Write(flag);
		}

		public void GetMemLevel()
		{
			string flag = "1";
			DataTable dt = new Chain.BLL.MemLevel().GetList("").Tables[0];
			if (dt.Rows.Count > 0)
			{
				flag = dt.Rows[0]["LevelID"].ToString() + "|" + dt.Rows[0]["LevelName"].ToString();
			}
			this.Response.Write(flag);
		}

		public void MemRegister()
		{
			string flag = "1";
			string memcard = this.Request["memcard"].ToString();
			string LevelID = this.Request["levelid"].ToString();
			string account = this.Request["account"].ToString();
			string phone = (this.Request["phone"] == null) ? "" : this.Request["phone"].ToString();
			string name = (this.Request["name"] == null) ? "" : this.Request["name"].ToString();
			Chain.Model.SysUser mdUser = new Chain.BLL.SysUser().GetModel(account);
			if (new Chain.BLL.Mem().Exists(0, memcard, phone, memcard, mdUser.UserShopID) < 0)
			{
				flag = "2";
			}
			else if (this.Request["orderaccount"] != null && this.Request["orderaccount"].ToString() != "")
			{
				if (new Chain.BLL.Mem().MemRegister(memcard, LevelID, mdUser.UserID, mdUser.UserShopID, phone, name) > 0)
				{
					flag = "0";
				}
			}
			else
			{
				flag = "ZC" + DateTime.Now.ToString("yyyyMMddHHmmss");
			}
			this.Response.Write(flag);
		}

		public void MemRecMoney()
		{
			string flag = "1";
			string memcard = this.Request["memcard"].ToString();
			string RecType = this.Request["rectype"].ToString();
			string RecMoney = this.Request["recmoney"].ToString();
			string Account = this.Request["account"].ToString();
			Chain.Model.Mem memInfo = new Chain.BLL.Mem().GetModelByMemCard(memcard);
			Chain.Model.SysUser UserModel = new Chain.BLL.SysUser().GetModel(Account);
			if (memInfo != null && UserModel != null)
			{
				if (this.Request["orderaccount"] != null && this.Request["orderaccount"].ToString() != "")
				{
					Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();
					if (!bllMemRecharge.Exists(this.Request["orderaccount"].ToString()))
					{
						if (new Chain.BLL.Mem().UpdateMoney(memInfo.MemID, decimal.Parse(RecMoney)) != 0)
						{
							flag = "0";
							bllMemRecharge.Add(new Chain.Model.MemRecharge
							{
								RechargeAccount = this.Request["orderaccount"].ToString(),
								RechargeCardBalance = memInfo.MemMoney + decimal.Parse(RecMoney),
								RechargeCreateTime = DateTime.Now,
								RechargeGive = 0m,
								RechargeIsApprove = true,
								RechargeMemID = memInfo.MemID,
								RechargeMoney = decimal.Parse(RecMoney),
								RechargeRemark = "",
								RechargeShopID = UserModel.UserShopID,
								RechargeType = ((RecType == "1") ? 2 : 3),
								RechargeUserID = UserModel.UserID
							});
							Chain.Model.MoneyChangeLog MoneyChangeModel = new Chain.Model.MoneyChangeLog();
							MoneyChangeModel.MemMoney = memInfo.MemMoney + decimal.Parse(RecMoney);
							MoneyChangeModel.MoneyChangeAccount = this.Request["orderaccount"].ToString();
							MoneyChangeModel.MoneyChangeBalance = decimal.Parse(RecMoney);
							MoneyChangeModel.MoneyChangeCash = ((RecType == "1") ? decimal.Parse(RecMoney) : 0m);
							MoneyChangeModel.MoneyChangeCreateTime = DateTime.Now;
							MoneyChangeModel.MoneyChangeGiveMoney = 0m;
							MoneyChangeModel.MoneyChangeMemID = memInfo.MemID;
							MoneyChangeModel.MoneyChangeMoney = decimal.Parse(RecMoney);
							MoneyChangeModel.MoneyChangeType = 1;
							MoneyChangeModel.MoneyChangeUnionPay = ((RecType == "2") ? decimal.Parse(RecMoney) : 0m);
							MoneyChangeModel.MoneyChangeUserID = UserModel.UserID;
							new Chain.BLL.MoneyChangeLog().Add(MoneyChangeModel);
						}
					}
					else
					{
						flag = "0";
					}
				}
				else
				{
					flag = PubFunction.curParameter.strMemRechargePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				}
				this.Response.Write(flag);
			}
		}

		public void GetGoodsInfo()
		{
			StringBuilder sb = new StringBuilder();
			string GoodsCode = this.Request["goodscode"].ToString();
			Chain.Model.Goods goodsInfo = new Chain.BLL.Goods().GetModel(GoodsCode);
			if (goodsInfo != null)
			{
				sb.Append(goodsInfo.GoodsCode);
				sb.Append("|");
				sb.Append(goodsInfo.Name);
				sb.Append("|");
				sb.Append(goodsInfo.Price.ToString("0.00"));
				sb.Append("|");
				sb.Append((goodsInfo.GoodsType == 2) ? "1" : "0");
			}
			else
			{
				sb.Append("2");
			}
			this.Response.Write(sb.ToString());
		}

		public void GetOrderGoodsInfo()
		{
			string flag = "1";
			try
			{
				StringBuilder sb = new StringBuilder();
				string Account = this.Request["account"].ToString();
				string MemCard = this.Request["memcard"].ToString();
				int TotalNum = Convert.ToInt32(this.Request["totalnum"].ToString());
				string GoodsCode = this.Request["goodscode"].ToString();
				Chain.Model.SysUser modelUser = new Chain.BLL.SysUser().GetModel(Account);
				if (modelUser != null)
				{
					Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModelByMemCard(MemCard);
					if (modelMem != null)
					{
						Chain.Model.Goods modelGoods = new Chain.BLL.Goods().GetModel(GoodsCode);
						if (modelGoods != null)
						{
							int count;
							DataTable dt = new Chain.BLL.Goods().GetGoodsStockList(modelUser.UserShopID, modelMem.MemLevelID, modelGoods.GoodsID.ToString(), 1, 1, out count, new string[]
							{
								""
							}).Tables[0];
							if (dt.Rows.Count > 0)
							{
								flag = (Convert.ToDecimal(dt.Rows[0]["DiscountPrice"]) * TotalNum).ToString() + "|" + Math.Floor(Convert.ToDecimal(dt.Rows[0]["DiscountPoint"]) * TotalNum).ToString();
							}
							else
							{
								flag = "5";
							}
						}
						else
						{
							flag = "4";
						}
					}
					else
					{
						flag = "3";
					}
				}
				else
				{
					flag = "2";
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void GetOrderCountInfo()
		{
			string flag = "1";
			try
			{
				StringBuilder sb = new StringBuilder();
				string Account = this.Request["account"].ToString();
				string MemCard = this.Request["memcard"].ToString();
				int TotalNum = 1;
				string GoodsCode = this.Request["goodscode"].ToString();
				Chain.Model.SysUser modelUser = new Chain.BLL.SysUser().GetModel(Account);
				if (modelUser != null)
				{
					Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModelByMemCard(MemCard);
					if (modelMem != null)
					{
						StringBuilder sbWhere = new StringBuilder();
						sbWhere.Append(string.Concat(new object[]
						{
							"CountDetailMemID = ",
							modelMem.MemID,
							" and GoodsCode = '",
							GoodsCode,
							"'"
						}));
						int resCount;
						DataTable dt = new Chain.BLL.MemCountDetail().GetMemCountList(1, 1, out resCount, new string[]
						{
							sbWhere.ToString()
						}).Tables[0];
						if (dt.Rows.Count > 0)
						{
							string goodsCode = dt.Rows[0]["goodscode"].ToString();
							string goodsName = dt.Rows[0]["name"].ToString();
							int number = Convert.ToInt32(dt.Rows[0]["number"].ToString());
							if (number > 0)
							{
								flag = string.Concat(new object[]
								{
									goodsCode,
									"|",
									goodsName,
									"|",
									number,
									"|",
									TotalNum
								});
							}
							else
							{
								flag = "5";
							}
						}
						else
						{
							flag = "4";
						}
					}
					else
					{
						flag = "3";
					}
				}
				else
				{
					flag = "2";
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void MemRecCount()
		{
			string flag = "1";
			try
			{
				string MemCard = this.Request["memcard"].ToString();
				string account = this.Request["account"].ToString();
				int count = Convert.ToInt32(this.Request["count"]);
				string goodscode = this.Request["goodscode"].ToString();
				string[] paylist = this.Request["payinfo"].ToString().Split(new char[]
				{
					'|'
				});
				DateTime dtExptime = DateTime.Now;
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem mdMem = bllMem.GetModelByMemCard(MemCard);
				Chain.Model.SysUser mdUser = new Chain.BLL.SysUser().GetModel(account);
				DataTable dtGoodsInfo = new Chain.BLL.Goods().GetGoodsInfo(goodscode, mdMem.MemLevelID).Tables[0];
				if (dtGoodsInfo.Rows[0]["GoodsType"].ToString() == "1")
				{
					decimal classDiscountPercent = Convert.ToDecimal(dtGoodsInfo.Rows[0]["ClassDiscountPercent"]);
					decimal classPointPercent = Convert.ToDecimal(dtGoodsInfo.Rows[0]["ClassPointPercent"]);
					decimal dclCardPayMoney = Convert.ToDecimal(paylist[0]);
					if (mdMem.MemMoney >= dclCardPayMoney)
					{
						if (this.Request["orderaccount"] != null && this.Request["orderaccount"].ToString() != "")
						{
							Chain.BLL.MemCount bllMemCount = new Chain.BLL.MemCount();
							if (!bllMemCount.Exists(this.Request["orderaccount"].ToString()))
							{
								bool bolIsCard = dclCardPayMoney > 0m;
								decimal dclCashPayMoney = Convert.ToDecimal(paylist[1]);
								bool bolIsCash = dclCashPayMoney > 0m;
								decimal dclBinkPayMoney = Convert.ToDecimal(paylist[2]);
								bool bolIsBink = dclBinkPayMoney > 0m;
								decimal dclTotalMoney = Convert.ToDecimal(this.Request["money"]);
								decimal dclDiscountMoney = dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney;
								int point = Convert.ToInt32(dclDiscountMoney * classPointPercent);
								string strOrderAccount = this.Request["orderaccount"].ToString();
								Chain.Model.MemCount modelMemCount = new Chain.Model.MemCount();
								modelMemCount.CountMemID = mdMem.MemID;
								modelMemCount.CountAccount = strOrderAccount;
								modelMemCount.CountTotalMoney = dclTotalMoney;
								modelMemCount.CountDiscountMoney = dclDiscountMoney;
								modelMemCount.CountPoint = point;
								modelMemCount.CountIsCard = bolIsCard;
								modelMemCount.CountPayCard = dclCardPayMoney;
								modelMemCount.CountIsCash = bolIsCash;
								modelMemCount.CountPayCash = dclCashPayMoney;
								modelMemCount.CountIsBink = bolIsBink;
								modelMemCount.CountPayBink = dclBinkPayMoney;
								modelMemCount.CountPayCoupon = 0m;
								modelMemCount.CountCreateTime = dtExptime;
								modelMemCount.CountPayType = 0;
								modelMemCount.CountPayCoupon = 0m;
								modelMemCount.CountRemark = "会员POS机上充次消费";
								modelMemCount.CountShopID = mdUser.UserShopID;
								modelMemCount.CountUserID = mdUser.UserID;
								int intCountID = bllMemCount.Add(modelMemCount);
								if (intCountID > 0)
								{
									Chain.Model.MemCountDetail modelCountDetail = new Chain.Model.MemCountDetail();
									modelCountDetail.CountDetailCountID = intCountID;
									modelCountDetail.CountDetailGoodsID = Convert.ToInt32(dtGoodsInfo.Rows[0]["GoodsID"]);
									modelCountDetail.CountDetailMemID = mdMem.MemID;
									modelCountDetail.CountDetailDiscountMoney = dclTotalMoney;
									modelCountDetail.CountDetailTotalNumber = count;
									modelCountDetail.CountDetailNumber = count;
									modelCountDetail.CountDetailPoint = point;
									modelCountDetail.CountCreateTime = DateTime.Now;
									Chain.BLL.MemCountDetail bllCountDetail = new Chain.BLL.MemCountDetail();
									bllCountDetail.Add(modelCountDetail);
									int intLevelID = mdMem.MemLevelID;
									Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
									if (bllPoint.Add(new Chain.Model.PointLog
									{
										PointMemID = mdMem.MemID,
										PointNumber = point,
										PointChangeType = 3,
										PointRemark = string.Concat(new object[]
										{
											"会员充次成功，消费总额：[",
											dclDiscountMoney,
											"],获得积分",
											point
										}),
										PointShopID = mdUser.UserShopID,
										PointUserID = mdUser.UserID,
										PointCreateTime = dtExptime,
										PointOrderCode = strOrderAccount
									}) > 0)
									{
										decimal dclMemMoney = mdMem.MemMoney - dclCardPayMoney;
										mdMem.MemPoint += point;
										int mem = bllMem.MemCountUpdateMem(mdMem.MemID, dclMemMoney, mdMem.MemPoint);
										MEMPointUpdate.MEMPointRate(mdMem, modelMemCount.CountPoint, modelMemCount.CountAccount, 3, mdUser.UserID, mdUser.UserShopID);
										flag = "0";
										mdMem = new Chain.BLL.Mem().GetModel(mdMem.MemID);
										string strUpdateMemLevel = PubFunction.UpdateMemLevel(mdMem);
										Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
										moneyChangeLogModel.MoneyChangeMemID = mdMem.MemID;
										moneyChangeLogModel.MoneyChangeUserID = mdUser.UserID;
										moneyChangeLogModel.MoneyChangeType = 8;
										moneyChangeLogModel.MoneyChangeAccount = strOrderAccount;
										moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
										moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
										moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
										moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
										moneyChangeLogModel.MemMoney = mdMem.MemMoney;
										moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
										moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
										new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
									}
								}
							}
							else
							{
								flag = "0";
							}
						}
						else
						{
							flag = new Chain.BLL.SysParameter().GetModel(1).MemCountPrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
						}
					}
					else
					{
						flag = "2";
					}
				}
				else
				{
					flag = "3";
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void ExpenseDiscount()
		{
			string flag = "1";
			try
			{
				string MemCard = this.Request["memcard"].ToString();
				decimal dclMoney = Convert.ToDecimal(this.Request["money"]);
				string userAccount = this.Request["account"].ToString();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModelByMemCard(MemCard);
				Chain.Model.SysUser user = new Chain.BLL.SysUser().GetModel(userAccount);
				if (modelMem != null)
				{
					Chain.BLL.SysShopMemLevel bllML = new Chain.BLL.SysShopMemLevel();
					Chain.Model.SysShopMemLevel modelML = bllML.GetModel(modelMem.MemLevelID, user.UserShopID);
					if (modelML != null)
					{
						decimal discountMoney = decimal.Round(dclMoney * modelML.ClassDiscountPercent, 2);
						int point = (modelML.ClassPointPercent == 0m) ? 0 : Convert.ToInt32(decimal.Floor(discountMoney / modelML.ClassPointPercent));
						flag = discountMoney + "|" + point;
					}
					else
					{
						flag = "3";
					}
				}
				else
				{
					flag = "2";
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void Expense()
		{
			string flag = "1";
			try
			{
				string MemCard = this.Request["memcard"].ToString();
				string account = this.Request["account"].ToString();
				string[] paylist = this.Request["payinfo"].ToString().Split(new char[]
				{
					'|'
				});
				decimal dclMoney = Convert.ToDecimal(this.Request["money"]);
				int intPoint = Convert.ToInt32(this.Request["point"]);
				string strPwd = this.Request["password"].ToString();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.SysUser mdUser = new Chain.BLL.SysUser().GetModel(account);
				Chain.Model.Mem modelMem = bllMem.GetModelByMemCard(MemCard);
				if (mdUser != null)
				{
					if (modelMem != null || bllMem.CheckMemPwd(MemCard, DESEncrypt.Encrypt(strPwd)) != null)
					{
						int intMemID = modelMem.MemID;
						decimal dclCardPayMoney = Convert.ToDecimal(paylist[0]);
						bool bolIsCard = dclCardPayMoney > 0m;
						decimal dclCashPayMoney = Convert.ToDecimal(paylist[1]);
						bool bolIsCash = dclCashPayMoney > 0m;
						decimal dclBinkPayMoney = Convert.ToDecimal(paylist[2]);
						bool bolIsBink = dclBinkPayMoney > 0m;
						decimal dclDiscountMoney = dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney;
						if (modelMem.MemMoney >= dclCardPayMoney)
						{
							if (this.Request["orderaccount"] != null && this.Request["orderaccount"].ToString() != "")
							{
								Chain.BLL.OrderLog bllOrder = new Chain.BLL.OrderLog();
								if (!bllOrder.ExistsOrderAccount(this.Request["orderaccount"].ToString()))
								{
									DateTime dtExptime = DateTime.Now;
									Chain.Model.OrderLog mdOrderLog = new Chain.Model.OrderLog();
									Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
									Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
									mdOrderLog.OrderMemID = intMemID;
									mdOrderLog.OrderType = 0;
									mdOrderLog.OrderPoint = intPoint;
									mdOrderLog.OrderTotalMoney = dclMoney;
									mdOrderLog.OrderDiscountMoney = dclDiscountMoney;
									mdOrderLog.OrderIsCard = bolIsCard;
									mdOrderLog.OrderPayCard = dclCardPayMoney;
									mdOrderLog.OrderIsCash = bolIsCash;
									mdOrderLog.OrderPayCash = dclCashPayMoney;
									mdOrderLog.OrderIsBink = bolIsBink;
									mdOrderLog.OrderPayBink = dclBinkPayMoney;
									mdOrderLog.OrderPayCoupon = 0m;
									mdOrderLog.OrderAccount = this.Request["orderaccount"].ToString();
									mdOrderLog.OrderShopID = mdUser.UserShopID;
									mdOrderLog.OrderUserID = mdUser.UserID;
									mdOrderLog.OrderRemark = "会员POS机上快速消费";
									mdOrderLog.OrderCreateTime = dtExptime;
									mdOrderLog.OrderPayType = 0;
									mdOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
									mdOrderLog.OrderCardPoint = modelMem.MemPoint + intPoint;
									int intSuccess = bllOrder.Add(mdOrderLog, mdOrderLog.OrderAccount);
									if (intSuccess > 0)
									{
										decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
										modelMem.MemConsumeMoney += mdOrderLog.OrderDiscountMoney;
										modelMem.MemPoint += mdOrderLog.OrderPoint;
										modelMem.MemConsumeLastTime = dtExptime;
										modelMem.MemConsumeCount++;
										int mem = bllMem.ExpenseUpdateMem(intMemID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
										flag = "0";
										Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
										moneyChangeLogModel.MoneyChangeMemID = intMemID;
										moneyChangeLogModel.MoneyChangeUserID = mdUser.UserID;
										moneyChangeLogModel.MoneyChangeType = 3;
										moneyChangeLogModel.MoneyChangeAccount = mdOrderLog.OrderAccount;
										moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
										moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
										moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
										moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
										moneyChangeLogModel.MemMoney = modelMem.MemMoney - dclCardPayMoney;
										moneyChangeLogModel.MoneyChangeCreateTime = dtExptime;
										moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
										new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
										mdPoint.PointMemID = intMemID;
										mdPoint.PointNumber = intPoint;
										mdPoint.PointChangeType = 2;
										mdPoint.PointRemark = string.Concat(new object[]
										{
											"会员快速消费，消费金额：[",
											dclDiscountMoney,
											"],获得积分：",
											intPoint,
											"]"
										});
										mdPoint.PointShopID = mdUser.UserShopID;
										mdPoint.PointCreateTime = dtExptime;
										mdPoint.PointUserID = mdUser.UserID;
										mdPoint.PointOrderCode = mdOrderLog.OrderAccount;
										bllPoint.Add(mdPoint);
										MEMPointUpdate.MEMPointRate(modelMem, mdOrderLog.OrderPoint, mdOrderLog.OrderAccount, 2, mdUser.UserID, mdUser.UserShopID);
										modelMem = new Chain.BLL.Mem().GetModel(intMemID);
										string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
										if (PubFunction.curParameter.bolSms && PubFunction.curParameter.bolAutoSendSMSByFastConsumption && modelMem.MemMobile != "")
										{
											if (Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
											{
												if (PubFunction.IsCanSendSms(mdUser.UserShopID, modelMem.MemMobile.Split(new char[]
												{
													','
												}).Length))
												{
													SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
													smsTemplateParameter.strCardID = modelMem.MemCard;
													smsTemplateParameter.strName = modelMem.MemName;
													smsTemplateParameter.dclTempMoney = dclDiscountMoney;
													smsTemplateParameter.dclMoney = modelMem.MemMoney;
													smsTemplateParameter.intTempPoint = intPoint;
													smsTemplateParameter.intPoint = modelMem.MemPoint;
													smsTemplateParameter.OldLevelID = modelMem.MemLevelID;
													modelMem = new Chain.BLL.Mem().GetModel(intMemID);
													smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
													smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
													smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
													string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, mdUser.UserShopID);
													SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
													Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
													modelSms.SmsMemID = modelMem.MemID;
													modelSms.SmsMobile = modelMem.MemMobile;
													modelSms.SmsContent = strSendContent;
													modelSms.SmsTime = DateTime.Now;
													modelSms.SmsShopID = mdUser.UserShopID;
													modelSms.SmsUserID = mdUser.UserID;
													modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
													modelSms.SmsAllAmount = modelSms.SmsAmount;
													Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
													bllSms.Add(modelSms);
													PubFunction.SaveSysLog(mdUser.UserID, 4, "会员消费-快速消费", string.Concat(new string[]
													{
														"快速消费发短信成功,会员卡号：[",
														modelMem.MemCard,
														"],姓名：[",
														modelMem.MemName,
														"]"
													}), mdUser.UserShopID, DateTime.Now, PubFunction.ipAdress);
												}
											}
										}
									}
								}
								else
								{
									flag = "0";
								}
							}
							else
							{
								flag = new Chain.BLL.SysParameter().GetModel(1).ExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
							}
						}
						else
						{
							flag = "2";
						}
					}
					else
					{
						flag = "4";
					}
				}
				else
				{
					flag = "3";
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsExpense()
		{
			string flag = "1";
			try
			{
				string MemCard = this.Request["memcard"].ToString();
				string account = this.Request["account"].ToString();
				string[] paylist = this.Request["payinfo"].ToString().Split(new char[]
				{
					'|'
				});
				decimal dclTotalMoney = Convert.ToDecimal(this.Request["money"]);
				int intPoint = Convert.ToInt32(this.Request["point"]);
				string goodscode = this.Request["goodscode"].ToString();
				decimal number = Convert.ToDecimal(this.Request["number"]);
				string strPwd = this.Request["password"].ToString();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.SysUser mdUser = new Chain.BLL.SysUser().GetModel(account);
				Chain.Model.Mem modelMem = bllMem.GetModelByMemCard(MemCard);
				if (modelMem != null)
				{
					if (bllMem.CheckMemPwd(MemCard, DESEncrypt.Encrypt(strPwd)) != null)
					{
						Chain.BLL.GoodsNumber bllgoodsnumber = new Chain.BLL.GoodsNumber();
						DataTable dtgoods = bllgoodsnumber.GetGoodsNumber(goodscode, mdUser.UserShopID).Tables[0];
						int gooodsid = Convert.ToInt32(dtgoods.Rows[0]["GoodsID"]);
						decimal price = decimal.Parse(dtgoods.Rows[0]["Price"].ToString());
						if (dtgoods.Rows[0]["GoodsType"].ToString() == "1" || Convert.ToInt32(dtgoods.Rows[0]["Number"]) >= number)
						{
							decimal dclCardPayMoney = Convert.ToDecimal(paylist[0]);
							bool bolIsCard = dclCardPayMoney > 0m;
							decimal dclCashPayMoney = Convert.ToDecimal(paylist[1]);
							bool bolIsCash = dclCashPayMoney > 0m;
							decimal dclBinkPayMoney = Convert.ToDecimal(paylist[2]);
							bool bolIsBink = dclBinkPayMoney > 0m;
							decimal dclDiscountMoney = dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney;
							DateTime dtExptime = DateTime.Now;
							int memID = modelMem.MemID;
							if (modelMem.MemMoney >= dclCardPayMoney)
							{
								if (this.Request["orderaccount"] != null && this.Request["orderaccount"].ToString() != "")
								{
									Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
									if (!bllOrderLog.ExistsOrderAccount(this.Request["orderaccount"].ToString()))
									{
										Chain.Model.OrderLog modelOrderLog = new Chain.Model.OrderLog();
										modelOrderLog.OrderAccount = this.Request["orderaccount"].ToString();
										modelOrderLog.OrderMemID = memID;
										modelOrderLog.OrderType = 2;
										modelOrderLog.OrderTotalMoney = dclTotalMoney;
										modelOrderLog.OrderDiscountMoney = dclDiscountMoney;
										modelOrderLog.OrderIsCard = bolIsCard;
										modelOrderLog.OrderPayCard = dclCardPayMoney;
										modelOrderLog.OrderIsCash = bolIsCash;
										modelOrderLog.OrderPayCash = dclCashPayMoney;
										modelOrderLog.OrderIsBink = bolIsBink;
										modelOrderLog.OrderPayBink = dclBinkPayMoney;
										modelOrderLog.OrderPayCoupon = 0m;
										modelOrderLog.OrderPoint = intPoint;
										modelOrderLog.OrderRemark = "会员POS机上商品消费";
										modelOrderLog.OrderPayType = 0;
										modelOrderLog.OrderShopID = mdUser.UserShopID;
										modelOrderLog.OrderUserID = mdUser.UserID;
										modelOrderLog.OrderCreateTime = dtExptime;
										modelOrderLog.OldAccount = "";
										modelOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
										int intOrderLogAdd = bllOrderLog.Add(modelOrderLog, modelOrderLog.OrderAccount);
										if (intOrderLogAdd > 0)
										{
											Chain.Model.OrderDetail modelDetail = new Chain.Model.OrderDetail();
											modelDetail.OrderID = intOrderLogAdd;
											modelDetail.GoodsID = gooodsid;
											modelDetail.OrderDetailPrice = price;
											modelDetail.OrderDetailDiscountPrice = dclDiscountMoney;
											modelDetail.OrderDetailNumber = number;
											modelDetail.OrderDetailPoint = intPoint;
											modelDetail.OrderDetailType = 0;
											new Chain.BLL.OrderDetail().Add(modelDetail);
										}
										Chain.BLL.GoodsLog bllGoodsLog = new Chain.BLL.GoodsLog();
										int intLog = bllGoodsLog.Add(new Chain.Model.GoodsLog
										{
											Type = 2,
											Remark = "商品销售出库",
											TotalPrice = dclDiscountMoney,
											GoodsAccount = modelOrderLog.OrderAccount,
											CreateTime = dtExptime,
											ShopID = mdUser.UserShopID,
											UserID = mdUser.UserID
										});
										if (intLog > 0)
										{
											Chain.BLL.GoodsLogDetail bllGoodsDetail = new Chain.BLL.GoodsLogDetail();
											Chain.Model.GoodsLogDetail modelGoodsDetail = new Chain.Model.GoodsLogDetail();
											Chain.BLL.GoodsNumber bllNumber = new Chain.BLL.GoodsNumber();
											bllNumber.UpdataGoodsNumber(new Chain.Model.GoodsNumber
											{
												GoodsID = gooodsid,
												ShopID = mdUser.UserShopID,
												Number = int.Parse((number * -1m).ToString())
											});
											modelGoodsDetail.GoodsLogID = intLog;
											modelGoodsDetail.GoodsID = gooodsid;
											modelGoodsDetail.GoodsInPrice = price;
											modelGoodsDetail.GoodsOutPrice = dclDiscountMoney / number;
											modelGoodsDetail.GoodsNumber = number;
											bllGoodsDetail.Add(modelGoodsDetail);
										}
										Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
										moneyChangeLogModel.MoneyChangeMemID = modelMem.MemID;
										moneyChangeLogModel.MoneyChangeUserID = mdUser.UserID;
										moneyChangeLogModel.MoneyChangeType = 12;
										moneyChangeLogModel.MoneyChangeAccount = modelOrderLog.OrderAccount;
										moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
										moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
										moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
										moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
										moneyChangeLogModel.MemMoney = modelMem.MemMoney - dclCardPayMoney;
										moneyChangeLogModel.MoneyChangeCreateTime = dtExptime;
										moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
										new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
										Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
										if (bllPoint.Add(new Chain.Model.PointLog
										{
											PointMemID = memID,
											PointNumber = intPoint,
											PointChangeType = 1,
											PointRemark = "会员商品消费成功，消费总额：[" + dclDiscountMoney + "]",
											PointShopID = mdUser.UserShopID,
											PointUserID = mdUser.UserID,
											PointCreateTime = dtExptime,
											PointOrderCode = modelOrderLog.OrderAccount
										}) > 0)
										{
											decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
											modelMem.MemConsumeMoney += dclDiscountMoney;
											modelMem.MemPoint += intPoint;
											modelMem.MemConsumeLastTime = dtExptime;
											modelMem.MemConsumeCount++;
											int mem = bllMem.ExpenseUpdateMem(memID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
											flag = "0";
											MEMPointUpdate.MEMPointRate(modelMem, modelOrderLog.OrderPoint, modelOrderLog.OrderAccount, 1, mdUser.UserID, mdUser.UserShopID);
											modelMem = new Chain.BLL.Mem().GetModel(memID);
											string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
											if (PubFunction.curParameter.bolSms && PubFunction.curParameter.bolAutoSendSMSByCommodityConsumption && modelMem.MemMobile != "")
											{
												if (Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
												{
													if (PubFunction.IsCanSendSms(mdUser.UserShopID, modelMem.MemMobile.Split(new char[]
													{
														','
													}).Length))
													{
														SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
														smsTemplateParameter.strCardID = modelMem.MemCard;
														smsTemplateParameter.strName = modelMem.MemName;
														smsTemplateParameter.dclTempMoney = dclDiscountMoney;
														smsTemplateParameter.dclMoney = modelMem.MemMoney;
														smsTemplateParameter.intTempPoint = intPoint;
														smsTemplateParameter.intPoint = modelMem.MemPoint;
														smsTemplateParameter.OldLevelID = modelMem.MemLevelID;
														modelMem = new Chain.BLL.Mem().GetModel(memID);
														smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
														smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
														smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
														string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, mdUser.UserShopID);
														SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
														Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
														modelSms.SmsMemID = modelMem.MemID;
														modelSms.SmsMobile = modelMem.MemMobile;
														modelSms.SmsContent = strSendContent;
														modelSms.SmsTime = DateTime.Now;
														modelSms.SmsShopID = mdUser.UserShopID;
														modelSms.SmsUserID = mdUser.UserID;
														modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
														modelSms.SmsAllAmount = modelSms.SmsAmount;
														Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
														bllSms.Add(modelSms);
														if (PubFunction.curParameter.bolShopSmsManage)
														{
															PubFunction.SetShopSms(mdUser.UserID, mdUser.UserShopID, modelMem.MemMobile.Split(new char[]
															{
																','
															}).Length, 2);
														}
													}
												}
											}
										}
									}
									else
									{
										flag = "0";
									}
								}
								else
								{
									flag = new Chain.BLL.SysParameter().GetModel(1).GoodsExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
								}
							}
							else
							{
								flag = "2";
							}
						}
						else
						{
							flag = "3";
						}
					}
					else
					{
						flag = "5";
					}
				}
				else
				{
					flag = "4";
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void MemCountExpense()
		{
			string flag = "1";
			try
			{
				string MemCard = this.Request["memcard"].ToString();
				string account = this.Request["account"].ToString();
				string goodscode = this.Request["goodscode"].ToString();
				string strPwd = this.Request["password"].ToString();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.SysUser mdUser = new Chain.BLL.SysUser().GetModel(account);
				Chain.Model.Mem modelMem = bllMem.GetModelByMemCard(MemCard);
				if (modelMem != null)
				{
					if (bllMem.CheckMemPwd(MemCard, DESEncrypt.Encrypt(strPwd)) != null)
					{
						Chain.Model.Goods mdgoods = new Chain.BLL.Goods().GetModel(goodscode);
						Chain.Model.OrderLog modelOrderLog = new Chain.Model.OrderLog();
						int memID = modelMem.MemID;
						int intUserShopID = mdUser.UserShopID;
						int intUserID = mdUser.UserID;
						DateTime dtExTime = DateTime.Now;
						Chain.BLL.MemCountDetail bllCountDetail = new Chain.BLL.MemCountDetail();
						DataTable dtCount = bllCountDetail.GetList(-1, string.Concat(new object[]
						{
							" CountDetailMemID=",
							memID,
							" and CountDetailGoodsID=",
							mdgoods.GoodsID,
							" and CountDetailNumber>0"
						}), "CountCreateTime ASC").Tables[0];
						if (dtCount.Rows.Count > 0)
						{
							if (this.Request["orderaccount"] != null && this.Request["orderaccount"].ToString() != "")
							{
								Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
								if (!bllOrderLog.ExistsOrderAccount(this.Request["orderaccount"].ToString()))
								{
									modelOrderLog.OrderAccount = this.Request["orderaccount"].ToString();
									modelOrderLog.OrderMemID = memID;
									modelOrderLog.OrderType = 7;
									modelOrderLog.OrderTotalMoney = 0m;
									modelOrderLog.OrderDiscountMoney = 0m;
									modelOrderLog.OrderIsCard = false;
									modelOrderLog.OrderPayCard = 0m;
									modelOrderLog.OrderIsCash = false;
									modelOrderLog.OrderPayCash = 0m;
									modelOrderLog.OrderIsBink = false;
									modelOrderLog.OrderPayBink = 0m;
									modelOrderLog.OrderPayCoupon = 0m;
									modelOrderLog.OrderPoint = 0;
									modelOrderLog.OrderRemark = "会员POS机上计次消费";
									modelOrderLog.OrderPayType = 0;
									modelOrderLog.OrderShopID = intUserShopID;
									modelOrderLog.OrderUserID = intUserID;
									modelOrderLog.OrderCreateTime = dtExTime;
									modelOrderLog.OldAccount = "";
									modelOrderLog.OrderCardBalance = modelMem.MemMoney;
									int intOrderLogAdd = bllOrderLog.Add(modelOrderLog, modelOrderLog.OrderAccount);
									if (intOrderLogAdd > 0)
									{
										Chain.Model.OrderDetail modelDetail = new Chain.Model.OrderDetail();
										modelDetail.OrderID = intOrderLogAdd;
										modelDetail.GoodsID = mdgoods.GoodsID;
										modelDetail.OrderDetailPrice = 0m;
										modelDetail.OrderDetailDiscountPrice = 0m;
										modelDetail.OrderDetailNumber = -1m;
										modelDetail.OrderDetailPoint = 0;
										modelDetail.OrderDetailType = 0;
										new Chain.BLL.OrderDetail().Add(modelDetail);
										int orderCount = Math.Abs(int.Parse(modelDetail.OrderDetailNumber.ToString()));
										foreach (DataRow drCount in dtCount.Rows)
										{
											if (orderCount != 0)
											{
												int detailNumber = int.Parse(drCount["CountDetailNumber"].ToString());
												if (detailNumber > orderCount)
												{
													bllCountDetail.UpdateCountDetailNumber(orderCount, int.Parse(drCount["CountDetailID"].ToString()));
													orderCount = 0;
												}
												else
												{
													bllCountDetail.UpdateCountDetailNumber(detailNumber, int.Parse(drCount["CountDetailID"].ToString()));
													orderCount -= detailNumber;
												}
											}
										}
										if (PubFunction.curParameter.bolSms && modelMem.MemMobile != "")
										{
											if (Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
											{
												if (PubFunction.IsCanSendSms(mdUser.UserShopID, modelMem.MemMobile.Split(new char[]
												{
													','
												}).Length))
												{
													SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
													smsTemplateParameter.strCardID = modelMem.MemCard;
													smsTemplateParameter.strName = modelMem.MemName;
													smsTemplateParameter.dclTempMoney = 0m;
													smsTemplateParameter.dclMoney = modelMem.MemMoney;
													smsTemplateParameter.intTempPoint = 0;
													smsTemplateParameter.intPoint = modelMem.MemPoint;
													smsTemplateParameter.OldLevelID = modelMem.MemLevelID;
													modelMem = new Chain.BLL.Mem().GetModel(memID);
													smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
													smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
													smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
													SmsTemplateParameter expr_567 = smsTemplateParameter;
													expr_567.CountItemsString += mdgoods.Name;
													smsTemplateParameter.CountItemsString = "[" + smsTemplateParameter.CountItemsString.Trim(new char[]
													{
														'、'
													}) + "]";
													string strSendContent = SMSInfo.GetSendContent(12, smsTemplateParameter, intUserShopID);
													SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
													Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
													modelSms.SmsMemID = modelMem.MemID;
													modelSms.SmsMobile = modelMem.MemMobile;
													modelSms.SmsContent = strSendContent;
													modelSms.SmsTime = DateTime.Now;
													modelSms.SmsShopID = intUserShopID;
													modelSms.SmsUserID = intUserID;
													modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
													modelSms.SmsAllAmount = modelSms.SmsAmount;
													Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
													bllSms.Add(modelSms);
													if (PubFunction.curParameter.bolShopSmsManage)
													{
														PubFunction.SetShopSms(mdUser.UserID, mdUser.UserShopID, modelMem.MemMobile.Split(new char[]
														{
															','
														}).Length, 2);
													}
												}
											}
										}
										flag = "0";
									}
								}
								else
								{
									flag = "0";
								}
							}
							else
							{
								flag = new Chain.BLL.SysParameter().GetModel(1).MemCountExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
							}
						}
						else
						{
							flag = "2";
						}
					}
					else
					{
						flag = "4";
					}
				}
				else
				{
					flag = "3";
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void AddMemPoint()
		{
			string flag = "1";
			try
			{
				string MemCard = this.Request["memcard"].ToString();
				string account = this.Request["account"].ToString();
				string strType = this.Request["type"].ToString();
				int intPointNumber = Convert.ToInt32(this.Request["number"]);
				string strRemark = "会员POS机积分变动";
				Chain.Model.SysUser mdUser = new Chain.BLL.SysUser().GetModel(account);
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModelByMemCard(MemCard);
				if (strType == "0" || modelMem.MemPoint >= intPointNumber)
				{
					if (this.Request["orderaccount"] != null && this.Request["orderaccount"].ToString() != "")
					{
						Chain.BLL.PointLog bllPontLog = new Chain.BLL.PointLog();
						if (!bllPontLog.Exists(this.Request["orderaccount"].ToString()))
						{
							Chain.Model.PointLog modelPontLog = new Chain.Model.PointLog();
							int intMemID = modelMem.MemID;
							modelPontLog.PointMemID = intMemID;
							modelPontLog.PointNumber = intPointNumber;
							modelPontLog.PointShopID = mdUser.UserShopID;
							modelPontLog.PointUserID = mdUser.UserID;
							modelPontLog.PointChangeType = 6;
							modelPontLog.PointCreateTime = DateTime.Now;
							modelPontLog.PointOrderCode = this.Request["orderaccount"].ToString();
							if (strType == "1")
							{
								modelPontLog.PointNumber *= -1;
								modelPontLog.PointRemark = string.Concat(new object[]
								{
									"手动进行减去积分,积分变动：[",
									modelPontLog.PointNumber,
									"],备注：",
									strRemark
								});
								bllPontLog.Add(modelPontLog);
							}
							else
							{
								modelPontLog.PointRemark = string.Concat(new object[]
								{
									"手动进行增加积分,积分变动：[",
									modelPontLog.PointNumber,
									"],备注：",
									strRemark
								});
								bllPontLog.Add(modelPontLog);
							}
							int intSuccess = new Chain.BLL.Mem().UpdatePoint(intMemID, modelPontLog.PointNumber);
							flag = "0";
							if (intSuccess > 0)
							{
								modelMem = new Chain.BLL.Mem().GetModel(intMemID);
								string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
							}
						}
						else
						{
							flag = "0";
						}
					}
					else
					{
						flag = new Chain.BLL.SysParameter().GetModel(1).MemPointChangePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
					}
				}
				else
				{
					flag = "2";
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void GetOrdersLog()
		{
			string flag = "1";
			try
			{
				string MemCard = this.Request["memcard"].ToString();
				string account = this.Request["account"].ToString();
				Chain.Model.SysUser modelUser = new Chain.BLL.SysUser().GetModel(account);
				if (modelUser != null)
				{
					Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModelByMemCard(MemCard);
					if (modelMem != null)
					{
						string strSql = "OrderType <> 3  and OrderType <> 5 and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderMemID = Mem.MemID  and OrderLog.OrderUserID = SysUser.UserID and OrderMemID=" + modelMem.MemID;
						Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
						int intResCount;
						DataTable dtExpense = bllOrderLog.GetListSP(12, 1, out intResCount, new string[]
						{
							strSql
						}).Tables[0];
						if (dtExpense.Rows.Count > 0)
						{
							string OrdersLog = "";
							foreach (DataRow item in dtExpense.Rows)
							{
								OrdersLog = OrdersLog + Convert.ToDateTime(item["OrderCreateTime"]).ToString("yyyyMMdd") + " ";
								int intOrderType = Convert.ToInt32(item["OrderType"]);
								bool OrderIsCard = Convert.ToBoolean(item["OrderIsCard"]);
								bool OrderIsCash = Convert.ToBoolean(item["OrderIsCash"]);
								bool OrderIsBink = Convert.ToBoolean(item["OrderIsBink"]);
								string strPayType = "";
								if (OrderIsCard)
								{
									strPayType = "余额";
								}
								if (OrderIsCash)
								{
									strPayType = "现金";
								}
								if (OrderIsBink)
								{
									strPayType = "银联";
								}
								if ((OrderIsCard && OrderIsCash) || (OrderIsCard && OrderIsBink) || (OrderIsCash && OrderIsBink))
								{
									strPayType = "联合";
								}
								if (intOrderType == 7)
								{
									strPayType = "计次";
								}
								OrdersLog = OrdersLog + strPayType + " ";
								OrdersLog = OrdersLog + Math.Round(Convert.ToDecimal(item["OrderDiscountMoney"]), 2) + "|";
							}
							for (int i = 1; i <= 6 - dtExpense.Rows.Count; i++)
							{
								OrdersLog += "暂无记录|";
							}
							flag = OrdersLog.Trim(new char[]
							{
								'|'
							});
						}
						else
						{
							flag = "4";
						}
					}
					else
					{
						flag = "3";
					}
				}
				else
				{
					flag = "2";
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void GetRptTotal()
		{
			string flag = "1";
			try
			{
				DateTime time = DateTime.Now;
				DateTime time2 = DateTime.Now;
				string checkRadion = "1";
				string account = (this.Request["account"] != null) ? this.Request["account"].ToString() : "";
				Chain.Model.SysUser userModel = new Chain.BLL.SysUser().GetModel(account);
				string shopID = "";
				string userID = "";
				int sysShopId = 0;
				if (userModel != null)
				{
					shopID = userModel.UserShopID.ToString();
					userID = userModel.UserID.ToString();
					sysShopId = userModel.UserShopID;
				}
				else
				{
					this.Response.Write("-2");
					this.Response.End();
				}
				if (!(userID == "") && userID != null)
				{
					string strUserName = PubFunction.UserIDTOName(int.Parse(userID));
				}
				if (checkRadion == "1")
				{
					time = DateTime.Today;
					time2 = DateTime.Today.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				else if (checkRadion == "2")
				{
					time = DateTime.Today.AddDays(-1.0);
					time2 = DateTime.Today.AddDays(-1.0).AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				else if (checkRadion == "3")
				{
					time = DateTime.Today.AddDays(-7.0);
					time2 = DateTime.Today.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				else if (checkRadion == "4")
				{
					time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
					time2 = DateTime.Today.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				else if (checkRadion == "5")
				{
					time = DateTime.Today.AddDays(-30.0);
					time2 = DateTime.Today.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				string memWhere = " 1=1 ";
				string rechargeWhere = " 1=1 ";
				string orderWhere = " 1=1 ";
				string countWhere = " 1=1 ";
				string memstoragetiming = " 1=1";
				string drawmoneyWhere = "1=1";
				if (shopID != "")
				{
					memWhere = memWhere + " and MemShopID=" + shopID;
					rechargeWhere = rechargeWhere + " and RechargeShopID=" + shopID;
					orderWhere = orderWhere + " and OrderShopID=" + shopID;
					countWhere = countWhere + " and CountShopID=" + shopID;
					memstoragetiming = memstoragetiming + " and StorageTimingShopID = " + shopID;
					drawmoneyWhere = drawmoneyWhere + " and DrawMoneyShopID =" + shopID;
				}
				else
				{
					memWhere = PubFunction.GetShopAuthority(sysShopId, "MemShopID", memWhere);
					rechargeWhere = PubFunction.GetShopAuthority(sysShopId, "RechargeShopID", rechargeWhere);
					orderWhere = PubFunction.GetShopAuthority(sysShopId, "OrderShopID", orderWhere);
					countWhere = PubFunction.GetShopAuthority(sysShopId, "CountShopID", countWhere);
					memstoragetiming = PubFunction.GetShopAuthority(sysShopId, "StorageTimingShopID", memstoragetiming);
					drawmoneyWhere = PubFunction.GetShopAuthority(sysShopId, "DrawMoneyShopID", drawmoneyWhere);
				}
				if (userID != "")
				{
					memWhere = memWhere + ((memWhere != "") ? " and" : "") + " MemUserID=" + userID;
					rechargeWhere = rechargeWhere + ((rechargeWhere != "") ? " and" : "") + "  RechargeUserID=" + userID;
					orderWhere = orderWhere + ((orderWhere != "") ? " and" : "") + "  OrderUserID=" + userID;
					countWhere = countWhere + ((countWhere != "") ? " and" : "") + "  CountUserID=" + userID;
					memstoragetiming = memstoragetiming + " and StorageTimingUserID = " + userID;
					drawmoneyWhere = drawmoneyWhere + " and DrawMoneyUserID =" + userID;
				}
				if (time.ToString() != "" && time2.ToString() != "")
				{
					memWhere = memWhere + ((memWhere != "") ? " and" : "") + string.Format("  MemCreateTime between '{0}' and '{1}'", time, time2);
					rechargeWhere = rechargeWhere + ((rechargeWhere != "") ? " and" : "") + string.Format(" RechargeCreateTime between '{0}' and '{1}' ", time, time2);
					orderWhere = orderWhere + ((orderWhere != "") ? " and" : "") + string.Format(" OrderCreateTime between '{0}' and '{1}' ", time, time2);
					countWhere = countWhere + ((countWhere != "") ? " and" : "") + string.Format(" CountCreateTime between '{0}' and '{1}' ", time, time2);
					memstoragetiming += string.Format(" and StorageTimingCreateTime between '{0}' and '{1}'", time, time2);
					drawmoneyWhere += string.Format(" AND DrawMoneyCreateTime between '{0}' and '{1}' ", time, time2);
				}
				DataSet ds = new Chain.BLL.SysShop().GetTotalRptData(memWhere, rechargeWhere, orderWhere, countWhere, memstoragetiming, drawmoneyWhere);
				int intNumber = 0;
				StringBuilder sbMem = new StringBuilder();
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					intNumber += int.Parse(row["MemNumber"].ToString());
					sbMem.Append(PubFunction.LevelIDToName(int.Parse(row["LevelID"].ToString())) + "：" + row["MemNumber"].ToString() + "名 ");
				}
				decimal SRechargeMoney = 0m;
				DataRow[] drs = ds.Tables[1].Select(" RechargeType=1 ");
				if (drs.Length == 1)
				{
					SRechargeMoney += decimal.Parse(drs[0][0].ToString());
				}
				decimal FRechargeGiveMoney = 0m;
				for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
				{
					FRechargeGiveMoney += decimal.Parse(ds.Tables[1].Rows[i][1].ToString());
				}
				decimal FRechargeMoney = 0m;
				drs = ds.Tables[1].Select(" RechargeType=2 ");
				if (drs.Length == 1)
				{
					FRechargeMoney += decimal.Parse(drs[0][0].ToString());
				}
				decimal FRechargeBankMoney = 0m;
				drs = ds.Tables[1].Select(" RechargeType=3 ");
				if (drs.Length == 1)
				{
					FRechargeBankMoney += decimal.Parse(drs[0][0].ToString());
				}
				decimal FRechargeTotalMoney = SRechargeMoney + FRechargeGiveMoney + FRechargeMoney + FRechargeBankMoney;
				decimal payBink = 0m;
				decimal payCash = 0m;
				decimal payCard = 0m;
				decimal payCoupon = 0m;
				decimal zljg = 0m;
				decimal totalPay = 0m;
				foreach (DataRow dr in ds.Tables[2].Rows)
				{
					payCard += decimal.Parse(dr["OrderPayCard"].ToString());
					payCash += decimal.Parse(dr["OrderPayCash"].ToString());
					payBink += decimal.Parse(dr["OrderPayBink"].ToString());
					payCoupon += decimal.Parse(dr["OrderPayCoupon"].ToString());
					totalPay += decimal.Parse(dr["OrderDiscountMoney"].ToString());
					zljg += decimal.Parse(dr["OrderPayCard"].ToString()) + decimal.Parse(dr["OrderPayBink"].ToString()) + decimal.Parse(dr["OrderPayCash"].ToString()) + decimal.Parse(dr["OrderPayCoupon"].ToString()) - decimal.Parse(dr["OrderDiscountMoney"].ToString());
				}
				decimal countPayBink = 0m;
				decimal countPayCash = 0m;
				decimal countPayCard = 0m;
				decimal countPayCoupon = 0m;
				decimal jczl = 0m;
				decimal totalCountPay = 0m;
				foreach (DataRow dr in ds.Tables[3].Rows)
				{
					countPayCard += decimal.Parse(dr["CountPayCard"].ToString());
					countPayCash += decimal.Parse(dr["CountPayCash"].ToString());
					countPayBink += decimal.Parse(dr["CountPayBink"].ToString());
					countPayCoupon += decimal.Parse(dr["CountPayCoupon"].ToString());
					totalCountPay += decimal.Parse(dr["CountDiscountMoney"].ToString());
					jczl += decimal.Parse(dr["CountPayCard"].ToString()) + decimal.Parse(dr["CountPayBink"].ToString()) + decimal.Parse(dr["CountPayCash"].ToString()) + decimal.Parse(dr["CountPayCoupon"].ToString()) - decimal.Parse(dr["CountDiscountMoney"].ToString());
				}
				decimal expenseSumMoneys = payCash - zljg;
				decimal countSumMoneys = countPayCash - jczl;
				decimal allMoney = SRechargeMoney + FRechargeMoney + expenseSumMoneys + countSumMoneys - Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]);
				decimal totalPayBank = FRechargeBankMoney + payBink + countPayBink;
				decimal totalPayCard = payCard + countPayCard;
				decimal doWorkallMoney = SRechargeMoney + FRechargeMoney + expenseSumMoneys + countSumMoneys - Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]);
				flag = string.Concat(new object[]
				{
					intNumber,
					"|",
					FRechargeTotalMoney.ToString("#0.00"),
					"|",
					FRechargeGiveMoney.ToString("#0.00"),
					"|",
					allMoney.ToString("#0.00"),
					"|",
					totalPayBank.ToString("#0.00"),
					"|",
					totalPayCard.ToString("#0.00")
				});
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		private string GetAuthority(Chain.Model.SysUser curUser)
		{
			StringBuilder sbAuthority = new StringBuilder();
			if (curUser != null)
			{
				DataTable dtAuthority = PubFunction.GetGroupAuthority(curUser.UserGroupID);
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 2) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 4) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 66) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 42) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 17) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 67) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 118) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 15) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 15) ? "1|" : "0|");
				sbAuthority.Append(PubFunction.GetPageVisit(dtAuthority, 18) ? "1" : "0");
			}
			else
			{
				sbAuthority.Append("");
			}
			return sbAuthority.ToString();
		}
	}
}
