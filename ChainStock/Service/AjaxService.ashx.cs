using Chain.BLL;
using Chain.Common;
using Chain.Common.DEncrypt;
using Chain.DBUtility;
using Chain.Model;
using Chain.Wechat;
using Newtonsoft.Json;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.SessionState;
using WebTest;

namespace ChainStock.Service
{
	public class AjaxService : IHttpHandler, IRequiresSessionState
	{
		private HttpRequest Request;

		private HttpResponse Response;

		private HttpSessionState Session;

		private HttpServerUtility Server;

		private HttpContext Context;

		private LoginLogic login;

		private Chain.Model.GoodsClass modelGdClass = new Chain.Model.GoodsClass();

		private Chain.BLL.GoodsClass bllGdClass = new Chain.BLL.GoodsClass();

		private Chain.Model.Goods modelGoods = new Chain.Model.Goods();

		private Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();

		private Chain.BLL.OrderDetail bllDetail = new Chain.BLL.OrderDetail();

		private Chain.Model.OrderDetail modelDetail = new Chain.Model.OrderDetail();

		private Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();

		private Chain.Model.OrderLog modelOrderLog = new Chain.Model.OrderLog();

		private Chain.Model.PointLog modelPoint = new Chain.Model.PointLog();

		private Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();

		private Chain.Model.MemCount modelCount = new Chain.Model.MemCount();

		private Chain.BLL.MemCount bllCount = new Chain.BLL.MemCount();

		private Chain.Model.MemCountDetail modelCountDetail = new Chain.Model.MemCountDetail();

		private Chain.BLL.MemCountDetail bllCountDetail = new Chain.BLL.MemCountDetail();

		private Chain.Model.StaffMoney modelStaffMoney = new Chain.Model.StaffMoney();

		private Chain.BLL.StaffMoney bllStaffMoney = new Chain.BLL.StaffMoney();

		private Chain.Model.GoodsNumber modelNumber = new Chain.Model.GoodsNumber();

		private Chain.BLL.GoodsNumber bllNumber = new Chain.BLL.GoodsNumber();

		private Chain.Model.GoodsLog modelGoodsLog = new Chain.Model.GoodsLog();

		private Chain.BLL.GoodsLog bllGoodsLog = new Chain.BLL.GoodsLog();

		private Chain.Model.Mem modelMem = new Chain.Model.Mem();

		private Chain.BLL.Mem bllMem = new Chain.BLL.Mem();

		private Chain.Model.GoodsAllot modelGoodsAllot = new Chain.Model.GoodsAllot();

		private Chain.BLL.GoodsAllot bllGoodsAllot = new Chain.BLL.GoodsAllot();

		private Chain.Model.GoodsAllotDetail modelAllotDetail = new Chain.Model.GoodsAllotDetail();

		private Chain.BLL.GoodsAllotDetail bllAllotDetail = new Chain.BLL.GoodsAllotDetail();

		private Chain.BLL.GoodsLogDetail bllGoodsDetail = new Chain.BLL.GoodsLogDetail();

		private Chain.Model.GoodsLogDetail modelGoodsDetail = new Chain.Model.GoodsLogDetail();

		private Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();

		private Chain.Model.MemRecharge modelMemRecharge = new Chain.Model.MemRecharge();

		private Chain.Model.GiftClass modelGiftClass = new Chain.Model.GiftClass();

		private Chain.BLL.GiftClass bllGiftClass = new Chain.BLL.GiftClass();

		private Chain.Model.OrderTime modelOrderTime = new Chain.Model.OrderTime();

		private Chain.BLL.OrderTime bllOrderTime = new Chain.BLL.OrderTime();

		private Chain.Model.Message modelMessage = new Chain.Model.Message();

		private Chain.BLL.Message bllMessage = new Chain.BLL.Message();

        private Chain.BLL.RegisterShop bllRegisterShop = new RegisterShop();


        private Chain.Model.SysUser UserModel
		{
			get
			{
				if (this.login == null)
				{
					this.login = LoginLogic.LoginStatus();
				}
				Chain.Model.SysUser result;
				if (this.login.IsLoggedOn && this.login.LoginUser != null)
				{
					result = this.login.LoginUser;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		private void LogError(Exception ex)
		{
			string err = string.Concat(new string[]
			{
				"会员系统 Caught in AjaxService.ashx   \r\nError in:",
				this.Request.Url.ToString(),
				"\r\nError Message:",
				ex.Message.ToString(),
				"\r\nStack Trace:",
				ex.StackTrace.ToString()
			});
			try
			{
				Chain.Model.SysError mdSysError = new Chain.Model.SysError();
				mdSysError.ErrorTime = DateTime.Now;
				mdSysError.ErrorContent = err;
				mdSysError.Ipaddress = PubFunction.ipAdress;
				LoginLogic login = LoginLogic.LoginStatus();
				if (login.IsLoggedOn && login.LoginUser != null)
				{
					mdSysError.UserID = login.LoginUser.UserID;
					mdSysError.ShopID = login.LoginUser.UserShopID;
				}
				new Chain.BLL.SysError().Add(mdSysError);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.Buffer = true;
			context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
			context.Response.AddHeader("pragma", "no-cache");
			context.Response.AddHeader("cache-control", "");
			context.Response.CacheControl = "no-cache";
			context.Response.ContentType = "text/plain";
			this.Request = context.Request;
			this.Response = context.Response;
			this.Session = context.Session;
			this.Server = context.Server;
			this.Context = context;
			string method = this.Request["Method"].ToString();
			MethodInfo methodInfo = base.GetType().GetMethod(method);
			methodInfo.Invoke(this, null);
		}

        #region 注册商铺
        /// <summary>
        /// 注册商铺
        /// 参数:
        /// shopName:企业名称
        /// shopCode:企业代码
        /// shopTelephone:联系电话
        /// shopAddress:详细地址
        /// userPassword:管理员密码
        /// SerailNumber:序列号
        /// 1:(序列号可以使用)2:（序列号已经使用）3:（序列号锁定）4:（序列号锁定）5:(序列号不存在)6:(商铺代码已存在)7:(商铺名称已存在)
        /// </summary>
        public void RegiserShop()
        {
            int flag = 0;
            try
            {
                
                int shopType = 3; //商家店铺
                string shopImageUrl = "../images/shop.png";//店铺图标(必填)
                int shopProvince = 0;//省份
                int shopCity = 0;//城市
                int shopCounty = 0;//区县
                decimal rechargeMaxMoney = 9999999999999m;//一次允许最大充值金额(必填)
                bool isRecharge = true;//允许充值
                bool isMainShop = true;//是否是直营店
                
                string shopName = this.Request["shopName"].ToString();//店铺名称
                string shopContactMan = this.Request["shopCode"].ToString();//改为商铺代码
                string shopTelephone = this.Request["shopTelephone"].ToString();//联系电话
                string shopAddress = this.Request["shopAddress"].ToString();//详细地址
                string serailNumber = this.Request["SerailNumber"].ToString();//详细地址

                string shopRemark = "";// 备注
                string shopTitle = shopName; //小票打印台头
                string shopFoot = shopName;//小票打印脚注
                string shopSmsName = shopName;//短信后缀
                int settlementInterval = 30;//商家结算周期
                decimal shopProportion = 0;//销售提成比例
                decimal RechargeProportion = 0;//充值提成比例
                decimal totalRate = 0;//返利总比例系数

                //检查注册码
                flag =  bllRegisterShop.SerianNumberInfo(serailNumber);
                if (flag != 1)
                {
                    this.Context.Response.Write(flag);
                    return;
                }

                //检查商铺代码
                bool result = bllRegisterShop.ShopCodeExists(shopContactMan);
                if (result)
                {
                    flag = 6;
                    this.Context.Response.Write(flag);
                    return;
                }

                //新增商家
                Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
                modelShop.ShopImageUrl = shopImageUrl;
                modelShop.ShopProvince = shopProvince;
                modelShop.ShopCity = shopCity;
                modelShop.ShopCounty = shopCounty;
                modelShop.ShopName = shopName;
                modelShop.IsMain = isMainShop;
                modelShop.ShopContactMan = shopContactMan;
                modelShop.ShopTelephone = shopTelephone;
                modelShop.ShopAddress = shopAddress;
                modelShop.ShopRemark = shopRemark;
                modelShop.ShopCreateTime = DateTime.Now;
                modelShop.ShopPrintTitle = shopTitle;
                modelShop.ShopPrintFoot = shopFoot;
                modelShop.ShopSmsName = shopSmsName;
                modelShop.SettlementInterval = settlementInterval;
                modelShop.ShopProportion = shopProportion;
                modelShop.RechargeProportion = RechargeProportion;
                modelShop.ShopType = shopType;
                modelShop.TotalRate = totalRate;
                modelShop.IsRecharge = isRecharge;
                modelShop.RechargeMaxMoney = rechargeMaxMoney;
                modelShop.IsAllianceProgram =false;//是否为联盟商
                modelShop.FatherShopID = 0;//联盟商编号
                modelShop.SmsType = 1;//短信不足发送应按：(1)不准发送  (2)短信透支（短信量变负数）  
                modelShop.PointType = 1;// Convert.ToInt32(this.Request["PointType"]);
                Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
                modelShop.ShopState = false;//店铺状态 True:锁定 False:不锁定
                flag = bllShop.Add(modelShop);
                //商铺名称已存在
                if ( flag==-1)
                {
                    flag = 7;
                    this.Context.Response.Write(flag);
                    return;
                }
                modelShop.ShopID = flag;
                if (flag > 0)
                {
                    Chain.BLL.SysShopMemLevel bllSysShopMemLevel = new Chain.BLL.SysShopMemLevel();
                    Chain.BLL.MemLevel bllLevel = new Chain.BLL.MemLevel();
                    DataTable dtMemLevel = bllLevel.GetAllList().Tables[0];
                    foreach (DataRow rowMemLevel in dtMemLevel.Rows)
                    {
                        int levelID = int.Parse(rowMemLevel["LevelID"].ToString());
                        bllSysShopMemLevel.Add(new Chain.Model.SysShopMemLevel
                        {
                            ShopID = flag,
                            MemLevelID = levelID,
                            ClassDiscountPercent = bllLevel.GetModel(levelID).LevelDiscountPercent,
                            ClassPointPercent = bllLevel.GetModel(levelID).LevelPointPercent,
                            ClassRechargePointRate = bllLevel.GetModel(levelID).LevelRechargePointRate
                        });
                    }
                    string strShopIDArray = "";
                    DataTable dt = bllShop.GetList("ShopID>0").Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strShopIDArray = strShopIDArray + dt.Rows[i]["ShopID"].ToString() + ",";
                    }
                    strShopIDArray = strShopIDArray.Remove(strShopIDArray.Length - 1);
                    Chain.BLL.SysShopAuthority bllShopAuthority = new Chain.BLL.SysShopAuthority();
                    Chain.Model.SysShopAuthority modelShopAuthority = new Chain.Model.SysShopAuthority();
                    modelShopAuthority.ShopAuthorityShopID = new int?(flag);
                    modelShopAuthority.ShopAuthorityData = flag.ToString();
                    int shop = bllShopAuthority.Add(modelShopAuthority);
                    if (shop > 0)
                    {
                        if (modelShop.FatherShopID != 0)
                        {
                            Chain.Model.SysShopAuthority modelFatherShopAuthority = bllShopAuthority.GetModel(modelShop.FatherShopID);
                            modelFatherShopAuthority.ShopAuthorityData = modelFatherShopAuthority.ShopAuthorityData + "," + shop;
                            bllShopAuthority.Update(modelFatherShopAuthority);
                        }
                        DataTable dtShopAuthority = bllShopAuthority.GetList(" ShopAuthorityShopID=1 ").Tables[0];
                        if (dtShopAuthority.Rows.Count > 0)
                        {
                            modelShopAuthority.ShopAuthorityID = int.Parse(dtShopAuthority.Rows[0]["ShopAuthorityID"].ToString());
                            modelShopAuthority.ShopAuthorityShopID = new int?(1);
                            modelShopAuthority.ShopAuthorityData = strShopIDArray;
                            bllShopAuthority.Update(modelShopAuthority);
                        }
                        else
                        {
                            modelShopAuthority.ShopAuthorityShopID = new int?(1);
                            modelShopAuthority.ShopAuthorityData = strShopIDArray;
                            int ShopAuthorityID = bllShopAuthority.Add(modelShopAuthority);
                            if (ShopAuthorityID > 0)
                            {
                                modelShopAuthority.ShopAuthorityID = ShopAuthorityID;
                                modelShopAuthority.ShopAuthorityShopID = new int?(1);
                                modelShopAuthority.ShopAuthorityData = strShopIDArray;
                                bllShopAuthority.Update(modelShopAuthority);
                            }
                        }
                    }
                    PubFunction.SaveSysLog(99999, 1, "商家新增", string.Concat(new string[]
                    {
                        "新增商家,商家名称：[",
                        modelShop.ShopName,
                        "],商家联系人：[",
                        modelShop.ShopContactMan,
                        "],联系方式：[",
                        modelShop.ShopTelephone,
                        "]"
                    }), modelShop.ShopID, DateTime.Now, PubFunction.ipAdress);
                }

                //新增管理员
                Chain.Model.SysUser modelUser = new Chain.Model.SysUser();
                Chain.Model.SysLog modelLog = new Chain.Model.SysLog();
                Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
                modelUser.UserName = shopName+"(管理员)";// 用户名称
                modelUser.UserAccount = shopContactMan + "_Admin";// 用户帐号;
                modelUser.UserTelephone = "";//用户电话
                modelUser.UserNumber = shopContactMan + "_Admin";//用户编号
                modelUser.UserPassword = DESEncrypt.Encrypt(this.Request["userPassword"].ToString());//用户密码
                int intUserID = this.UserModel.UserID;
                int intUserShopID = this.UserModel.UserShopID;
                modelUser.UserLock = false;
                modelUser.UserShopID = modelShop.ShopID;
                modelUser.UserGroupID = 3;//商铺管理员组
                modelUser.UserRemark = "";//备注
                modelUser.UserCreateTime = DateTime.Now;
                flag = bllUser.Add(modelUser);
                if (flag > 0)
                {
                    //Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(modelUser.UserShopID);
                    Chain.Model.SysGroup modelGroup = new Chain.BLL.SysGroup().GetModel(modelUser.UserGroupID);
                    PubFunction.SaveSysLog(intUserID, 1, "用户新增", string.Concat(new string[]
                    {
                        "新增用户,用户名称：[",
                        modelUser.UserName,
                        "],用户所属商家：[",
                        modelShop.ShopName,
                        "],所属权限组：[",
                        modelGroup.GroupName,
                        "]"
                    }), this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);

                    bllRegisterShop.Register(serailNumber, modelShop.ShopID);
                }

            }
            catch (Exception e)
            {
                this.LogError(e);
                flag = -3;
            }
            this.Context.Response.Write(flag);
        }
        #endregion

        #region 序列号

        public void MakeSerialNumber( )
        {
            int flag = 0;
            try
            {
                int number = int.Parse(this.Request["Number"].ToString());
                bool isLock = bool.Parse(this.Request["IsLock"].ToString());
                int intIsLock = isLock ? 1 : 0;

                Chain.BLL.SysSerialNumber sysSerialNumber = new SysSerialNumber();
                flag = sysSerialNumber.MakeSN(number, intIsLock);
            }
            catch (Exception e)
            {
                this.LogError(e);
                flag = 3;
            }
            this.Context.Response.Write(flag);
        }

        /// <summary>
        /// 序列号
        /// </summary>
        /// <param name="ID"></param>
        public void SerialNumberLock()
        {
            int flag = 0;
            try
            {
                int id = int.Parse(this.Request["ID"].ToString());
                bool isLock = bool.Parse(this.Request["IsLock"].ToString());
                int intIsLock = isLock == false ? 1 : 0;

                Chain.BLL.SysSerialNumber sysSerialNumber = new SysSerialNumber();
                flag = sysSerialNumber.Lock(id, intIsLock);
            }
            catch(Exception e)
            {
                this.LogError(e);
                flag = 3;
            }
            this.Context.Response.Write(flag);
        }


        public void SerialNumberCard()
        {
            int flag = 0;
            try
            {
                int id = int.Parse(this.Request["ID"].ToString());
                bool isCard = bool.Parse(this.Request["IsCard"].ToString());
                int intIsCard = isCard == false ? 1 : 0;

                Chain.BLL.SysSerialNumber sysSerialNumber = new SysSerialNumber();
                flag = sysSerialNumber.Card(id, intIsCard);
            }
            catch( Exception e)
            {
                this.LogError(e);
                flag = 3;
            }
            this.Context.Response.Write(flag);
        }
        #endregion

        public void CheckUserLogin()
		{
			int flag = 0;
			try
			{
				string username = this.Request["Account"].ToString();
				string password = this.Request["password"].ToString();
				string valcode = this.Request["valcode"].ToString();
				if (PubFunction.curParameter.DateValidity <= DateTime.Now)
				{
					flag = 5;
				}
				else
				{
					ValCodeModel modeValCode = (ValCodeModel)this.Session["ValCode"];
					this.Session["ValCode"] = null;
					if (modeValCode != null)
					{
						if (modeValCode.CodeFailure || valcode != modeValCode.valCode)
						{
							flag = 4;
						}
						else
						{
							LoginLogic login = new LoginLogic();
							login.Login(username.Trim(), password);
							if (login.IsLoggedOn && login.LoginUser != null)
							{
								flag = 1;
								PubFunction.SysEnableGoods();
								OnlineBiz.Add(login.LoginUser.UserID, this.Session.SessionID, this.Request.UserHostAddress, this.Request.UserAgent);
								PubFunction.curParameter = null;
								PubFunction.SysUpdateMemIsPast();
								PubFunction.SysEdition(PubFunction.curParameter.istry);
								this.Response.Cookies.Add(new HttpCookie("PrintPreview", PubFunction.curParameter.PrintPreview.ToString()));
								PubFunction.DeleteUpLoad(this.Server.MapPath("~/Upload/Members"));
								PubFunction.DeleteUpLoad(this.Server.MapPath("~/Upload/Goods"));
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 3;
			}
			this.Context.Response.Write(flag);
		}

        /// <summary>
        /// 商户登陆
        /// </summary>
        public void CheckUserLogin_Merchant()
        {
            int flag = 0;
            try
            {
                string username = this.Request["Account"].ToString();
                string password = this.Request["password"].ToString();
                string valcode = this.Request["valcode"].ToString();
                if (PubFunction.curParameter.DateValidity <= DateTime.Now)
                {
                    flag = 5;
                }
                else
                {
                    ValCodeModel modeValCode = (ValCodeModel)this.Session["ValCode"];
                    this.Session["ValCode"] = null;
                    if (modeValCode != null)
                    {
                        if (modeValCode.CodeFailure || valcode != modeValCode.valCode)
                        {
                            flag = 4;
                        }
                        else
                        {
                            LoginLogic login = new LoginLogic();
                            login.Login(username.Trim(), password);
                            if (login.IsLoggedOn && login.LoginUser != null)
                            {
                                flag = 1;
                                PubFunction.SysEnableGoods();
                                OnlineBiz.Add(login.LoginUser.UserID, this.Session.SessionID, this.Request.UserHostAddress, this.Request.UserAgent);
                                PubFunction.curParameter = null;
                                PubFunction.SysUpdateMemIsPast();
                                PubFunction.SysEdition(PubFunction.curParameter.istry);
                                this.Response.Cookies.Add(new HttpCookie("PrintPreview", PubFunction.curParameter.PrintPreview.ToString()));
                                PubFunction.DeleteUpLoad(this.Server.MapPath("~/Upload/Members"));
                                PubFunction.DeleteUpLoad(this.Server.MapPath("~/Upload/Goods"));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.LogError(e);
                flag = 3;
            }
            this.Context.Response.Write(flag);
        }

        public void GetMem()
		{
			string msgResponse = "";
			try
			{
				string memCard = (this.Request["memCard"].ToString() != "") ? this.Request["memCard"].ToString() : "";
				Chain.BLL.SubMem bllSubMem = new Chain.BLL.SubMem();
				DataTable dtSubMem = bllSubMem.GetList(" SubCardNumber='" + memCard + "' and IsUsed ='true' ").Tables[0];
				if (dtSubMem != null)
				{
					if (dtSubMem.Rows.Count > 0)
					{
						memCard = dtSubMem.Rows[0]["MemCard"].ToString();
					}
				}
				int intUserShopID = this.UserModel.UserShopID;
				StringBuilder strSql = new StringBuilder();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				string strCustomField = "";
				DataTable dt = new Chain.BLL.MemCustomField().GetList(" CustomType=1").Tables[0];
				foreach (DataRow dr in dt.Rows)
				{
					strCustomField = strCustomField + dr["CustomField"].ToString() + ",";
				}
				strSql.Append(" 1=1 ");
				if (memCard != "")
				{
					strSql.AppendFormat(" and (MemCard='{0}' or MemMobile='{0}' or MemCardNumber='{0}')", memCard);
				}
				strSql.Append(" and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID=SysUser.UserID");
				strSql.Append(" and Mem.MemID>0 AND dbo.SysShopMemLevel.MemLevelID=Mem.MemLevelID");
				strSql.AppendFormat(" AND dbo.SysShopMemLevel.ShopID='{0}' ", this.UserModel.UserShopID);
                //是否是联盟商
                Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(this.UserModel.UserShopID);
                if (modelShop.FatherShopID==0)
                    strSql.AppendFormat(" AND Mem.MemShopID='{0}' ", this.UserModel.UserShopID);

                int counts = 0;
				DataTable dtMem = bllMem.GetListSP(20, 1, out counts, new string[]
				{
					strSql.ToString()
				}).Tables[0];
				for (int i = 0; i < dtMem.Rows.Count; i++)
				{
					dtMem.Rows[i]["MemRemark"] = StringPlus.HtmlDecode(dtMem.Rows[i]["MemRemark"].ToString());
				}
				if (dtMem != null)
				{
					msgResponse = JsonPlus.ToJson(dtMem, "MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemCreateTime,MemRemark,MemLevelID,MemShopID,MemUserID,ShopName," + strCustomField + "LevelID,LevelName,LevelPoint,ClassDiscountPercent,ClassPointPercent,UserName,MemTelePhone,MemQRCode,MemProvinceName,MemCityName,MemCountyName,MemVillageName,MemCardNumber,MemCountNumber,ClassRechargePointRate");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetQueryMem()
		{
			string msgResponse = "";
			try
			{
				int intSize = int.Parse(this.Request["size"].ToString());
				int intIndex = int.Parse(this.Request["index"].ToString());
				string memCard = (this.Request["memCard"].ToString() != "") ? this.Request["memCard"].ToString() : "";
				int intUserShopID = this.UserModel.UserShopID;
				StringBuilder strSql = new StringBuilder();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				string strCustomField = "";
				DataTable dt = new Chain.BLL.MemCustomField().GetList(" CustomType=1").Tables[0];
				foreach (DataRow dr in dt.Rows)
				{
					strCustomField = strCustomField + dr["CustomField"].ToString() + ",";
				}
				strSql.Append(" 1=1 ");
				if (memCard != "")
				{
					strSql.AppendFormat(" and (MemCard like '%{0}%' or MemName like '%{0}%' or  MemMobile like '%{0}%' or MemCardNumber like '%{0}%')", memCard);
				}
				strSql.Append(" and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID=SysUser.UserID");
				strSql.Append(" and Mem.MemID>0");
				strSql.Append(" and Mem.MemShopID =SysShopMemLevel.ShopID and SysShopMemLevel.MemLevelID=MemLevel.LevelID ");
				int counts = 0;
				DataTable dtMem = bllMem.GetListSP(intSize, intIndex, out counts, new string[]
				{
					PubFunction.GetShopAuthority(intUserShopID, "MemShopID", strSql.ToString())
				}).Tables[0];
				for (int i = 0; i < dtMem.Rows.Count; i++)
				{
					string strRemark = dtMem.Rows[i]["MemRemark"].ToString().Replace("\n", "<\br>");
					dtMem.Rows[i]["MemRemark"] = strRemark.Replace("\"", "\\\"").Replace("\r\n", "").Replace("\n", "");
				}
				if (dtMem != null)
				{
					msgResponse = JsonPlus.ToJson(dtMem, "MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemCreateTime,MemRemark,MemLevelID,MemShopID,MemUserID,ShopName," + strCustomField + "LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevelRechargePointRate,UserName,MemTelePhone,MemQRCode,MemCardNumber");
					msgResponse = string.Concat(new object[]
					{
						"{\"RecordCount\":",
						counts,
						",\"List\":",
						msgResponse,
						"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetMemRecommend()
		{
			string msgResponse = "";
			try
			{
				int intMemID = (this.Request["MemID"].ToString() != "") ? int.Parse(this.Request["MemID"].ToString()) : 0;
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = new Chain.Model.Mem();
				modelMem = bllMem.GetModel(intMemID);
				msgResponse = modelMem.MemCard;
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetAllMemList()
		{
			string msgResponse = "";
			try
			{
				string condition = (this.Request["Condition"].ToString() != "") ? this.Request["Condition"].ToString() : "";
				string strCustomField = "";
				DataTable dt = new Chain.BLL.MemCustomField().GetList(" CustomType=1").Tables[0];
				foreach (DataRow dr in dt.Rows)
				{
					strCustomField = strCustomField + dr["CustomField"].ToString() + ",";
				}
				int counts = 0;
				condition += " and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID = SysUser.UserID and SysShopMemLevel.ShopID= Mem.MemShopID and MemLevel.LevelID=SysShopMemLevel.MemLevelID ";
				DataTable dtMem = new Chain.BLL.Mem().GetListSP(100000, 1, out counts, new string[]
				{
					condition
				}).Tables[0];
				if (dtMem != null)
				{
					msgResponse = JsonPlus.ToJson(dtMem, "MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemCreateTime,MemRemark,MemLevelID,MemShopID,MemUserID,ShopName," + strCustomField + "LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevelRechargePointRate,UserName,MemTelePhone");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void MemCheckPwd()
		{
			int flag = 0;
			int strMemID = (this.Request["memID"].ToString() != "") ? int.Parse(this.Request["memID"].ToString()) : 0;
			string strMemPassword = this.Request["memPassword"].ToString();
			Chain.Model.Mem modelMem = new Chain.Model.Mem();
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			modelMem = bllMem.GetModel(strMemID);
			if (modelMem.MemPassword == DESEncrypt.Encrypt(strMemPassword.Trim()))
			{
				flag = 1;
			}
			this.Context.Response.Write(flag);
		}

		public void MemAdd()
		{
			int flag = 0;
			string arg_3B_0 = (this.Request["txtMemID"] != "") ? this.Request["txtMemID"].ToString() : "";
			try
			{
				string strMemCard = (this.Request["txtMemCard"].ToString() != "") ? this.Request["txtMemCard"].ToString() : "";
				string strMemName = (this.Request["txtMemName"].ToString() != "") ? this.Request["txtMemName"].ToString() : "";
				string strMemPassword = (this.Request["txtMemPassword"].ToString() != "") ? this.Request["txtMemPassword"].ToString() : "";
				int intMemSex = (this.Request["sltMemSex"].ToString() != "") ? int.Parse(this.Request["sltMemSex"].ToString()) : 0;
				string strMemIdentityCard = (this.Request["txtMemIdentityCard"].ToString() != "") ? this.Request["txtMemIdentityCard"].ToString() : "";
				string strMemBirthday = (this.Request["txtMemBirthday"].ToString() != "") ? this.Request["txtMemBirthday"].ToString() : "1900-1-1";
				string strMemMobile = (this.Request["txtMemMobile"].ToString() != "") ? this.Request["txtMemMobile"].ToString() : "";
				int intMemPoint = (this.Request["txtMemPoint"].ToString() != "") ? int.Parse(this.Request["txtMemPoint"].ToString()) : 0;
				decimal dclMemMoney = (this.Request["txtMemMoney"].ToString() != "") ? decimal.Parse(this.Request["txtMemMoney"].ToString()) : 0m;
				string strMemEmail = (this.Request["txtMemEmail"].ToString() != "") ? this.Request["txtMemEmail"].ToString() : "";
				string strMemAddress = (this.Request["txtMemAddress"].ToString() != "") ? this.Request["txtMemAddress"].ToString() : "";
				int intMemState = (this.Request["sltMemState"].ToString() != "") ? int.Parse(this.Request["sltMemState"].ToString()) : -1;
				int intMemLevelID = (this.Request["sltMemLevelID"].ToString() != "") ? int.Parse(this.Request["sltMemLevelID"].ToString()) : 0;
				int intMemShopID = (this.Request["sltShop"].ToString() != "") ? int.Parse(this.Request["sltShop"].ToString()) : 0;
				int intMemRecommendID = (this.Request["txtMemRecommendID"].ToString() != "") ? int.Parse(this.Request["txtMemRecommendID"].ToString()) : 0;
				string strMemCreateTime = (this.Request["txtMemCreateTime"].ToString() != "") ? this.Request["txtMemCreateTime"].ToString() : "1900-1-1";
				string strPastTime = (this.Request["txtMemPastTime"].ToString() != "") ? this.Request["txtMemPastTime"].ToString() : "2900-1-1";
				string strMemPhoto = (this.Request["txtMemPhoto"].ToString() != "") ? this.Request["txtMemPhoto"].ToString() : "";
				string strMemRemark = (this.Request["txtMemRemark"].ToString() != "") ? this.Request["txtMemRemark"].ToString() : "";
				strMemRemark = StringPlus.HtmlEncode(strMemRemark);
				int intMemUserID = (this.Request["sltMemUserID"].ToString() != "") ? int.Parse(this.Request["sltMemUserID"].ToString()) : 0;
				string strMemTelePhone = (this.Request["txtTelephone"].ToString() != "") ? this.Request["txtTelephone"].ToString() : "";
				bool bolMSM = this.Request["chkSMS"] != null && this.Request["chkSMS"] == "on";
				bool bolMMS = this.Request["chkMMS"] != null && this.Request["chkMMS"] == "on";
				string strImg = (this.Request["hidImgSrc"] != "") ? this.Request["hidImgSrc"].ToString() : "";
				string strRemark = string.Concat(new string[]
				{
					"会员登记,会员卡号：[",
					strMemCard,
					"],姓名：[",
					strMemName,
					"],会员等级：[",
					PubFunction.LevelIDToName(intMemLevelID),
					"]"
				});
				string strMemProvince = this.Request["ucSysArea$sltProvince"].ToString();
				string strMemCity = this.Request["ucSysArea$sltCity"].ToString();
				string strMemCounty = this.Request["ucSysArea$sltCounty"].ToString();
				string strMemVillage = this.Request["ucSysArea$sltVillage"].ToString();
				string strMemCardNumber = (!string.IsNullOrEmpty(this.Request["txtCardNumber"])) ? this.Request["txtCardNumber"].ToString() : "";
				Chain.Model.Mem modelMem = new Chain.Model.Mem();
				modelMem.MemCard = strMemCard.Trim();
				modelMem.MemName = strMemName.Trim();
				modelMem.MemPassword = DESEncrypt.Encrypt(strMemPassword.Trim());
				modelMem.MemSex = PubFunction.SetMemSex(intMemSex);
				modelMem.MemIdentityCard = strMemIdentityCard;
				DateTime dteMemBirthday = default(DateTime);
				DateTime.TryParse(strMemBirthday, out dteMemBirthday);
				modelMem.MemBirthday = dteMemBirthday;
				modelMem.MemBirthdayType = true;
				modelMem.MemMobile = strMemMobile.Trim();
				modelMem.MemPoint = intMemPoint;
				modelMem.MemPointAutomatic = true;
				modelMem.MemMoney = dclMemMoney;
				modelMem.MemConsumeMoney = 0m;
				modelMem.MemEmail = strMemEmail.Trim();
				modelMem.MemAddress = strMemAddress;
				modelMem.MemState = intMemState;
				modelMem.MemLevelID = intMemLevelID;
				modelMem.MemShopID = intMemShopID;
				modelMem.MemRecommendID = intMemRecommendID;
				DateTime dteMemPastTime = default(DateTime);
				DateTime.TryParse(strPastTime, out dteMemPastTime);
				modelMem.MemPastTime = dteMemPastTime;
				if (dteMemPastTime > DateTime.Now)
				{
					modelMem.MemIsPast = false;
				}
				modelMem.MemPhoto = strMemPhoto.Trim();
				DateTime dteMemCreateTime = default(DateTime);
				DateTime.TryParse(strMemCreateTime, out dteMemCreateTime);
				modelMem.MemCreateTime = dteMemCreateTime;
				modelMem.MemRemark = strMemRemark;
				modelMem.MemUserID = intMemUserID;
				modelMem.MemTelePhone = strMemTelePhone;
				modelMem.MemQRCode = strImg;
				modelMem.MemProvince = strMemProvince;
				modelMem.MemCity = strMemCity;
				modelMem.MemCounty = strMemCounty;
				modelMem.MemVillage = strMemVillage;
				modelMem.MemCardNumber = strMemCardNumber;
				modelMem.MemAttention = 2;
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				if (!PubFunction.IsShopPoint(intMemShopID, ref intMemPoint))
				{
					flag = -8;
					this.Context.Response.Write(flag);
					return;
				}
				modelMem.MemPoint = intMemPoint;
				Chain.BLL.SubMem bllSubMem = new Chain.BLL.SubMem();
				DataTable dtSubMem = bllSubMem.GetList(" SubCardNumber='" + strMemCard + "'").Tables[0];
				if (dtSubMem != null)
				{
					if (dtSubMem.Rows.Count > 0)
					{
						flag = -1;
						this.Context.Response.Write(flag);
						return;
					}
				}
				if (PubFunction.curParameter.bolIsSendCard)
				{
					if (PubFunction.IsCanRegisterCard(intMemShopID, strMemCard, strMemCardNumber))
					{
						flag = bllMem.Add(modelMem);
						if (PubFunction.curParameter.bolShopPointManage)
						{
							PubFunction.SetShopPoint(intMemUserID, intMemShopID, intMemPoint, "会员登记赠送会员积分", 2);
						}
					}
					else
					{
						flag = -7;
					}
				}
				else
				{
					flag = bllMem.Add(modelMem);
                    if (PubFunction.curParameter.bolShopPointManage)
					{
						PubFunction.SetShopPoint(intMemUserID, intMemShopID, intMemPoint, "会员登记赠送会员积分", 2);
					}
				}
				if (flag > 0)
				{
					modelMem.MemID = flag;
					if (PubFunction.curParameter.bolIsMemRegisterStaff)
					{
						decimal dclStaffMoney = (!string.IsNullOrEmpty(this.Request["txtRegisterStaffMoney"])) ? Convert.ToDecimal(this.Request["txtRegisterStaffMoney"].ToString()) : 0m;
						int intStaff = (!string.IsNullOrEmpty(this.Request["sltStaff"].ToString())) ? Convert.ToInt32(this.Request["sltStaff"].ToString()) : 0;
						if (intStaff != 0 && dclStaffMoney != 0m)
						{
							this.modelStaffMoney.StaffID = intStaff;
							this.modelStaffMoney.StaffTotalMoney = dclStaffMoney;
							this.modelStaffMoney.StaffOrderCode = "DJ" + DateTime.Now.ToString("yyMMddHHmmssffff");
							this.modelStaffMoney.StaffMemID = flag;
							this.modelStaffMoney.StaffShopID = intMemShopID;
							this.modelStaffMoney.StaffCreateTime = DateTime.Now;
							this.modelStaffMoney.StaffType = 1;
							this.bllStaffMoney.Add(this.modelStaffMoney);
						}
					}
					this.MemRecommendPoint(modelMem, intMemShopID, intMemUserID);
					DataRow[] drs = new Chain.BLL.MemCustomField().CustomGetList(" CustomType=1 ");
					Hashtable hash = new Hashtable();
					if (drs.Length > 0)
					{
						DataRow[] array = drs;
						for (int i = 0; i < array.Length; i++)
						{
							DataRow dr = array[i];
							hash.Add(dr["CustomField"].ToString(), this.Request["Mem_Custom_" + dr["CustomField"].ToString()]);
						}
						bllMem.AddCustomField(modelMem.MemCard, hash);
					}
					if (intMemPoint != 0)
					{
						Chain.Model.PointLog modelPoint = new Chain.Model.PointLog();
						modelPoint.PointMemID = flag;
						modelPoint.PointNumber = intMemPoint;
						modelPoint.PointChangeType = 5;
						modelPoint.PointRemark = "会员登记赠送积分，增加积分,积分变动：" + intMemPoint.ToString();
						modelPoint.PointShopID = intMemShopID;
						modelPoint.PointCreateTime = DateTime.Now;
						modelPoint.PointUserID = intMemUserID;
						modelPoint.PointOrderCode = PubFunction.curParameter.strMemPointChangePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
						Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
						bllPoint.Add(modelPoint);
					}
					if (dclMemMoney != 0m)
					{
						Chain.Model.MemRecharge modelRecharge = new Chain.Model.MemRecharge();
						modelRecharge.RechargeMemID = flag;
						modelRecharge.RechargeType = 1;
						modelRecharge.RechargeMoney = dclMemMoney;
						modelRecharge.RechargeGive = dclMemMoney;
						modelRecharge.RechargeRemark = "会员登记赠送金额,初始金额：" + dclMemMoney.ToString();
						modelRecharge.RechargeShopID = this.UserModel.UserShopID;
						modelRecharge.RechargeCreateTime = DateTime.Now;
						modelRecharge.RechargeAccount = PubFunction.curParameter.strMemRechargePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
						modelRecharge.RechargeUserID = intMemUserID;
						modelRecharge.RechargeCardBalance = dclMemMoney;
						modelRecharge.RechargeIsApprove = true;
						Chain.BLL.MemRecharge bllRecharge = new Chain.BLL.MemRecharge();
						bllRecharge.Add(modelRecharge);
						Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
						moneyChangeLogModel.MoneyChangeMemID = flag;
						moneyChangeLogModel.MoneyChangeUserID = this.UserModel.UserID;
						moneyChangeLogModel.MoneyChangeType = 5;
						moneyChangeLogModel.MoneyChangeAccount = PubFunction.curParameter.strMemRechargePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
						moneyChangeLogModel.MoneyChangeMoney = dclMemMoney;
						moneyChangeLogModel.MemMoney = dclMemMoney;
						moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
						moneyChangeLogModel.MoneyChangeGiveMoney = dclMemMoney;
						new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
					}
					if (modelMem.MemMobile != "")
					{
						if (bolMSM)
						{
							if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
							{
								flag = -3;
							}
							else if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
							{
								','
							}).Length))
							{
								string strSendContent = SMSInfo.GetSendContent(1, new SmsTemplateParameter
								{
									strCardID = modelMem.MemCard,
									strName = modelMem.MemName,
									dclMoney = dclMemMoney,
									dclTempMoney = dclMemMoney,
									intTempPoint = intMemPoint,
									intPoint = intMemPoint,
									OldLevelID = intMemLevelID,
									NewLevelID = intMemLevelID,
									MemBirthday = modelMem.MemBirthday,
									MemPastTime = modelMem.MemPastTime,
								
								}, modelMem.MemShopID);
								SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
								Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
								modelSms.SmsMemID = modelMem.MemID;
								modelSms.SmsMobile = modelMem.MemMobile;
								modelSms.SmsContent = strSendContent;
								modelSms.SmsTime = DateTime.Now;
								modelSms.SmsShopID = modelMem.MemShopID;
								modelSms.SmsUserID = modelMem.MemUserID;
								modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
								modelSms.SmsAllAmount = modelSms.SmsAmount;
								Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
								bllSms.Add(modelSms);
								if (PubFunction.curParameter.bolShopSmsManage)
								{
									PubFunction.SetShopSms(intMemUserID, this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
									{
										','
									}).Length, 2);
								}
							}
							else
							{
								flag = -5;
							}
						}
						if (bolMMS)
						{
							string UID = PubFunction.curParameter.strMMSSeries;
							string PWD = PubFunction.curParameter.strMMSSerialPwd;
							string Content = strMemCard.Trim();
							string Mobile = strMemMobile;
							string Subject = "会员" + strMemName + "二维码卡号";
							if (int.Parse(WebTest.SendMessage.GetBalance(UID, PWD)) > 0)
							{
								string Retu = WebTest.SendMessage.SendMMSToWG(UID, PWD, Mobile, Subject, Content);
							}
						}
					}
					PubFunction.SaveSysLog(intMemUserID, 1, "会员登记", strRemark, intMemShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void MemRecommendPoint(Chain.Model.Mem modelMem, int ShopID, int Uid)
		{
			if (modelMem.MemRecommendID > 0 && PubFunction.curParameter.intRecommendPoint > 0)
			{
				int point = PubFunction.curParameter.intRecommendPoint;
				if (PubFunction.IsShopPoint(ShopID, ref point))
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					Chain.Model.Mem MemRecommend = bllMem.GetModel(modelMem.MemRecommendID);
					Chain.Model.PointLog modelPontLog = new Chain.Model.PointLog();
					Chain.BLL.PointLog bllPontLog = new Chain.BLL.PointLog();
					modelPontLog.PointMemID = modelMem.MemRecommendID;
					modelPontLog.PointNumber = PubFunction.curParameter.intRecommendPoint;
					modelPontLog.PointShopID = modelMem.MemShopID;
					modelPontLog.PointUserID = modelMem.MemUserID;
					modelPontLog.PointChangeType = 7;
					modelPontLog.PointCreateTime = DateTime.Now;
					modelPontLog.PointGiveMemID = modelMem.MemID;
					modelPontLog.PointRemark = string.Concat(new object[]
					{
						"推荐会员获得积分,推荐卡号：[",
						modelMem.MemCard,
						"] 姓名：[",
						modelMem.MemName,
						"] 获得积分：[",
						modelPontLog.PointNumber,
						"]"
					});
					bllPontLog.Add(modelPontLog);
					if (PubFunction.curParameter.bolShopPointManage)
					{
						PubFunction.SetShopPoint(Uid, ShopID, point, string.Concat(new object[]
						{
							"会员注册,推荐会员获取积分：[",
							modelPontLog.PointNumber,
							"],商家扣除积分：[",
							modelPontLog.PointNumber,
							"]"
						}), 2);
					}
					int intSuccess = bllMem.UpdatePoint(modelPontLog.PointMemID, modelPontLog.PointNumber);
					if (intSuccess > 0)
					{
						PubFunction.UpdateMemLevel(bllMem.GetModel(modelPontLog.PointMemID));
					}
				}
			}
		}

		public void MemEdit()
		{
			int flag = 0;
			string arg_3B_0 = (this.Request["txtMemID"] != "") ? this.Request["txtMemID"].ToString() : "";
			try
			{
				Chain.Model.Mem modelMem = new Chain.Model.Mem();
				int strMemID = (this.Request["txtMemID"].ToString() != "") ? int.Parse(this.Request["txtMemID"].ToString()) : 0;
				string strMemCard = (this.Request["txtMemCard"].ToString() != "") ? this.Request["txtMemCard"].ToString() : "";
				string strMemName = (this.Request["txtMemName"].ToString() != "") ? this.Request["txtMemName"].ToString() : "";
				int intMemSex = (this.Request["sltMemSex"].ToString() != "") ? int.Parse(this.Request["sltMemSex"].ToString()) : 0;
				string strMemIdentityCard = (this.Request["txtMemIdentityCard"].ToString() != "") ? this.Request["txtMemIdentityCard"].ToString() : "";
				string strMemBirthday = (this.Request["txtMemBirthday"].ToString() != "") ? this.Request["txtMemBirthday"].ToString() : "1900-1-1";
				string strMemMobile = (this.Request["txtMemMobile"].ToString() != "") ? this.Request["txtMemMobile"].ToString() : "";
				int arg_23B_0 = (this.Request["txtMemPoint"].ToString() != "") ? int.Parse(this.Request["txtMemPoint"].ToString()) : 0;
				decimal arg_280_0 = (this.Request["txtMemMoney"].ToString() != "") ? decimal.Parse(this.Request["txtMemMoney"].ToString()) : 0m;
				string strMemEmail = (this.Request["txtMemEmail"].ToString() != "") ? this.Request["txtMemEmail"].ToString() : "";
				string strMemAddress = (this.Request["txtMemAddress"].ToString() != "") ? this.Request["txtMemAddress"].ToString() : "";
				int intMemState = (this.Request["sltMemState"].ToString() != "") ? int.Parse(this.Request["sltMemState"].ToString()) : 0;
				int intMemLevelID = (this.Request["sltMemLevelID"].ToString() != "") ? int.Parse(this.Request["sltMemLevelID"].ToString()) : 0;
				string strMemCreateTime = (this.Request["txtMemCreateTime"].ToString() != "") ? this.Request["txtMemCreateTime"].ToString() : "1900-1-1";
				int intMemRecommendID = (this.Request["txtMemRecommendID"].ToString() != "") ? int.Parse(this.Request["txtMemRecommendID"].ToString()) : 0;
				string strPastTime = (this.Request["txtMemPastTime"].ToString() != "") ? this.Request["txtMemPastTime"].ToString() : "2900-1-1";
				string strMemPhoto = (this.Request["txtMemPhoto"].ToString() != "") ? this.Request["txtMemPhoto"].ToString() : "";
				string strMemRemark = (this.Request["txtMemRemark"].ToString() != "") ? this.Request["txtMemRemark"].ToString() : "";
				strMemRemark = StringPlus.HtmlEncode(strMemRemark);
				int intMemUserID = (this.Request["sltMemUserID"].ToString() != "") ? int.Parse(this.Request["sltMemUserID"].ToString()) : 0;
				string strMemTelePhone = (this.Request["txtTelephone"].ToString() != "") ? this.Request["txtTelephone"].ToString() : "";
				string strMemProvince = this.Request["ucSysArea$sltProvince"].ToString();
				string strMemCity = this.Request["ucSysArea$sltCity"].ToString();
				string strMemCounty = this.Request["ucSysArea$sltCounty"].ToString();
				string strMemVillage = this.Request["ucSysArea$sltVillage"].ToString();
				string strMemCardNumber = (!string.IsNullOrEmpty(this.Request["txtCardNumber"])) ? this.Request["txtCardNumber"].ToString() : "";
				modelMem = new Chain.BLL.Mem().GetModel(strMemID);
				if (DateTime.Parse(strMemCreateTime) != DateTime.Parse(modelMem.MemCreateTime.ToShortDateString()))
				{
					Chain.BLL.SysLog bllLog = new Chain.BLL.SysLog();
					DataTable dt = bllLog.GetActionList(strMemCard).Tables[0];
					if (dt.Rows.Count > 0)
					{
						flag = -4;
					}
				}
				if (flag != -4)
				{
					modelMem.MemID = strMemID;
					modelMem.MemCard = strMemCard.Trim();
					modelMem.MemName = strMemName.Trim();
					if (intMemSex == 0)
					{
						modelMem.MemSex = true;
					}
					else
					{
						modelMem.MemSex = false;
					}
					modelMem.MemIdentityCard = strMemIdentityCard;
					DateTime dteMemBirthday = default(DateTime);
					DateTime.TryParse(strMemBirthday, out dteMemBirthday);
					if (strMemBirthday != "1900-1-1" && modelMem.MemBirthday.ToString("yyyy-MM-dd") != dteMemBirthday.ToString("yyyy-MM-dd") && modelMem.MemBirthday.ToString("MM-dd") != dteMemBirthday.ToString("MM-dd"))
					{
						Chain.BLL.MemberSMSRemindLog MemberSMSRemindLogBll = new Chain.BLL.MemberSMSRemindLog();
						MemberSMSRemindLogBll.DeleteMemByMemID(modelMem.MemID);
					}
					modelMem.MemBirthday = dteMemBirthday;
					modelMem.MemMobile = strMemMobile.Trim();
					modelMem.MemEmail = strMemEmail.Trim();
					modelMem.MemAddress = strMemAddress;
					modelMem.MemCreateTime = DateTime.Parse(strMemCreateTime);
					modelMem.MemState = intMemState;
					modelMem.MemLevelID = intMemLevelID;
					DateTime dteMemPastTime = default(DateTime);
					DateTime.TryParse(strPastTime, out dteMemPastTime);
					modelMem.MemPastTime = dteMemPastTime;
					if (dteMemPastTime > DateTime.Now)
					{
						modelMem.MemIsPast = false;
					}
					modelMem.MemRecommendID = intMemRecommendID;
					modelMem.MemPhoto = strMemPhoto.Trim();
					modelMem.MemRemark = strMemRemark;
					modelMem.MemTelePhone = strMemTelePhone;
					modelMem.MemProvince = strMemProvince;
					modelMem.MemCity = strMemCity;
					modelMem.MemCounty = strMemCounty;
					modelMem.MemVillage = strMemVillage;
					modelMem.MemCardNumber = strMemCardNumber;
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					flag = bllMem.Update(modelMem);
					if (flag > 0)
					{
						DataRow[] drs = new Chain.BLL.MemCustomField().CustomGetList(" CustomType=1 ");
						Hashtable hash = new Hashtable();
						if (drs.Length > 0)
						{
							DataRow[] array = drs;
							for (int i = 0; i < array.Length; i++)
							{
								DataRow dr = array[i];
								hash.Add(dr["CustomField"].ToString(), this.Request["Mem_Custom_" + dr["CustomField"].ToString()]);
							}
							bllMem.AddCustomField(modelMem.MemCard, hash);
						}
						PubFunction.SaveSysLog(intMemUserID, 3, "会员编辑", string.Concat(new string[]
						{
							"编辑会员,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"]"
						}), modelMem.MemShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -5;
			}
			this.Context.Response.Write(flag);
		}

		public void MemDelete()
		{
			string msgResponse = "0";
			try
			{
				int intMemID = (this.Request["strMemID"].ToString() != "") ? int.Parse(this.Request["strMemID"].ToString()) : 0;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				DataTable dtIsDel = this.bllMem.IsCanDelMem(intMemID).Tables[0];
				if (dtIsDel.Rows.Count > 0)
				{
					if (dtIsDel.Rows[0]["strResultsList"].ToString() != "")
					{
						msgResponse = dtIsDel.Rows[0]["strResultsList"].ToString();
					}
				}
				if (msgResponse == "0")
				{
					if (this.bllMem.Delete(intMemID))
					{
						msgResponse = "1";
						PubFunction.SaveSysLog(intUserID, 2, "会员删除", string.Concat(new string[]
						{
							"删除会员,会员卡号：[",
							this.modelMem.MemCard,
							"],姓名：[",
							this.modelMem.MemName,
							"]"
						}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				msgResponse = "0";
			}
			msgResponse = "{\"result\":\"" + msgResponse + "\"}";
			this.Context.Response.Write(msgResponse);
		}

		public void GetMemList()
		{
			string msgResponse = "";
			try
			{
				string strMemCard = (this.Request["memCard"].ToString() != "") ? this.Request["memCard"].ToString() : "";
				int intMemLevelID = (this.Request["memLevelID"].ToString() != "") ? int.Parse(this.Request["memLevelID"].ToString()) : 0;
				int intMemShopID = (this.Request["memShopID"].ToString() != "") ? int.Parse(this.Request["memShopID"].ToString()) : 0;
				int intUserShopID = this.UserModel.UserShopID;
				StringBuilder strSql = new StringBuilder();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				strSql.Append("1=1");
				if (strMemCard != "")
				{
					strSql.AppendFormat(" and (Mem.MemCard ='{0}' or Mem.MemName like '%{0}%' or Mem.MemMobile='{0}' or Mem.MemCardNumber='{0}' )", strMemCard);
				}
				if (intMemLevelID != 0)
				{
					strSql.AppendFormat(" and Mem.MemLevelID={0}", intMemLevelID);
				}
				if (intMemShopID != 0)
				{
					strSql.AppendFormat(" and Mem.MemShopID={0}", intMemShopID);
				}
				strSql.Append(" and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID=SysUser.UserID");
				strSql.AppendFormat(" AND SysShopMemLevel.MemLevelID=dbo.Mem.MemLevelID AND SysShopMemLevel.ShopID='{0}'", (this.UserModel == null) ? -1 : this.UserModel.UserShopID);
				string Sqlwhere = PubFunction.GetShopAuthority(intUserShopID, "MemShopID", strSql.ToString());
				int counts = 1;
				DataTable dtMem = bllMem.GetListSP(100000, 1, out counts, new string[]
				{
					Sqlwhere
				}).Tables[0];
				if (dtMem != null)
				{
					msgResponse = JsonPlus.ToJson(dtMem, "MemID,MemCard,MemName,MemMobile,MemLevelID,MemShopID,ShopName,LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,MemEmail");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void MemPointReset()
		{
			int flag = 0;
			try
			{
				string strMemID = this.Request["memID"].ToString();
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				strMemID = strMemID.Remove(strMemID.Length - 1);
				string[] strArrayMemID = strMemID.Split(new char[]
				{
					','
				});
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = new Chain.Model.Mem();
				for (int i = 0; i < strArrayMemID.Length; i++)
				{
					modelMem = bllMem.GetModel(int.Parse(strArrayMemID[i].ToString()));
					bllMem.UpdatePoint(int.Parse(strArrayMemID[i].ToString()), modelMem.MemPoint * -1);
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 4, "会员列表", string.Concat(new string[]
					{
						"积分清零,会员卡号：[",
						modelMem.MemCard,
						"],姓名：[",
						modelMem.MemName,
						"]"
					}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void MemLevelDelete()
		{
			int flag = 0;
			try
			{
				int intLevelID = (this.Request["LevelID"].ToString() != "") ? int.Parse(this.Request["LevelID"].ToString()) : -1;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				DataTable dt = bllMem.GetList(" MemLevelID = " + intLevelID).Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = -1;
				}
				else
				{
					Chain.Model.MemLevel modelMemLevel = new Chain.Model.MemLevel();
					Chain.BLL.MemLevel bllMemLevel = new Chain.BLL.MemLevel();
					modelMemLevel = bllMemLevel.GetModel(intLevelID);
					if (modelMemLevel != null)
					{
						using (TransactionScope scope = new TransactionScope())
						{
							flag = bllMemLevel.Delete(intLevelID);
							new Chain.BLL.SysShopMemLevel().DeleteByMemLevelID(intLevelID);
							new Chain.BLL.GoodsClassDiscount().DelGoodsClassDiscountByMemLevelID(intLevelID);
							PubFunction.SaveSysLog(intUserID, 2, "会员等级", "删除会员等级,等级名称：[" + modelMemLevel.LevelName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
							scope.Complete();
						}
					}
					else
					{
						flag = -2;
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void GetMemLevel()
		{
			string msgResponse = "";
			try
			{
				int intLevelID = int.Parse(this.Request["LevelID"].ToString());
				Chain.BLL.MemLevel bllMemLevel = new Chain.BLL.MemLevel();
				DataTable dtMemLevel = bllMemLevel.GetList("LevelID=" + intLevelID).Tables[0];
				if (dtMemLevel != null)
				{
					msgResponse = JsonPlus.ToJson(dtMemLevel, "LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,LevelRechargePointRate");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetMemLevelList()
		{
			string msgResponse = "";
			try
			{
				Chain.BLL.MemLevel bllMemLevel = new Chain.BLL.MemLevel();
				DataTable dtMemLevel = bllMemLevel.GetList("").Tables[0];
				if (dtMemLevel != null)
				{
					msgResponse = JsonPlus.ToJson(dtMemLevel, "LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelRechargePointRate");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void DrawMoney()
		{
			int flag = -1;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			string strAccount = this.Request["account"].ToString();
			int memID = (this.Request["memID"].ToString() != "") ? int.Parse(this.Request["memID"]) : 0;
			decimal money = (this.Request["money"].ToString() != "") ? decimal.Parse(this.Request["money"].ToString()) : 0m;
			decimal actualmoney = (this.Request["actualmoney"].ToString() != "") ? decimal.Parse(this.Request["actualmoney"].ToString()) : 0m;
			string remark = (this.Request["remark"].ToString() != "") ? this.Request["remark"].ToString() : "";
			bool sendMSM = this.Request["sendSMS"] == "true";
			try
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				if (modelMem.MemMoney < actualmoney)
				{
					flag = -3;
				}
				else
				{
					Chain.Model.MemDrawMoney modelDrawMoney = new Chain.Model.MemDrawMoney();
					Chain.BLL.MemDrawMoney bllDrawMoney = new Chain.BLL.MemDrawMoney();
					string strRemark = string.Concat(new string[]
					{
						"会员账户提现,会员卡号：[",
						modelMem.MemCard,
						"],姓名：[",
						modelMem.MemName,
						"],提现扣减金额：[",
						actualmoney.ToString(),
						"],备注：",
						remark
					});
					modelDrawMoney.DrawMoneyMemID = memID;
					modelDrawMoney.DrawMoneyAccount = strAccount;
					modelDrawMoney.DrawMoney = money;
					modelDrawMoney.DrawActualMoney = actualmoney;
					modelDrawMoney.DrawMoneyRemark = remark;
					modelDrawMoney.DrawMoneyShopID = intUserShopID;
					modelDrawMoney.DrawMoneyUserID = intUserID;
					modelDrawMoney.DrawMoneyCreateTime = DateTime.Now;
					int intDrawMoney = bllDrawMoney.Add(modelDrawMoney);
					if (intDrawMoney > 0)
					{
						flag = bllMem.DrawMoney(modelMem.MemID, actualmoney);
						Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
						moneyChangeLogModel.MoneyChangeMemID = memID;
						moneyChangeLogModel.MoneyChangeUserID = intUserID;
						moneyChangeLogModel.MoneyChangeType = 7;
						moneyChangeLogModel.MoneyChangeAccount = strAccount;
						moneyChangeLogModel.MoneyChangeMoney = -actualmoney;
						moneyChangeLogModel.MoneyChangeBalance = -actualmoney;
						moneyChangeLogModel.MemMoney = modelMem.MemMoney - actualmoney;
						moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
						moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
						if (modelMem.MemMobile != "")
						{
							if (sendMSM)
							{
								if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
								{
									flag = -2;
								}
								else if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
								{
									','
								}).Length))
								{
									string strSendContent = SMSInfo.GetSendContent(3, new SmsTemplateParameter
									{
										strCardID = modelMem.MemCard,
										strName = modelMem.MemName,
										dclTempMoney = money,
										dclMoney = modelMem.MemMoney - money,
										intTempPoint = 0,
										intPoint = modelMem.MemPoint,
										OldLevelID = modelMem.MemLevelID,
										NewLevelID = modelMem.MemLevelID,
										MemBirthday = modelMem.MemBirthday,
										MemPastTime = modelMem.MemPastTime
									}, intUserShopID);
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
										PubFunction.SetShopSms(intUserID, this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
										{
											','
										}).Length, 4);
									}
								}
								else
								{
									flag = -5;
								}
							}
							PubFunction.SaveSysLog(intUserID, 4, "会员账户提现", strRemark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void ChangePwd()
		{
			int flag = 0;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			int memID = (this.Request["memID"].ToString() != "") ? int.Parse(this.Request["memID"]) : 0;
			string oldPwd = (this.Request["oldPwd"].ToString() != "") ? this.Request["oldPwd"].ToString() : "";
			string newPwd = (this.Request["newPwd"].ToString() != "") ? this.Request["newPwd"].ToString() : "";
			string remark = (this.Request["remark"].ToString() != "") ? this.Request["remark"].ToString() : "无";
			bool isOldPwd = Convert.ToBoolean(this.Request["isOldPwd"].ToString());
			try
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				oldPwd = DESEncrypt.Encrypt(oldPwd);
				newPwd = DESEncrypt.Encrypt(newPwd);
				flag = bllMem.UpdateMemPwd(isOldPwd, modelMem.MemID, newPwd, oldPwd);
				PubFunction.SaveSysLog(intUserID, 3, "会员修改密码", string.Concat(new string[]
				{
					"修改会员密码,会员卡号：[",
					modelMem.MemCard,
					"],会员姓名：[",
					modelMem.MemName,
					"],备注：",
					remark
				}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -2;
			}
			this.Context.Response.Write(flag);
		}

		public void MemLock()
		{
			int flag = -1;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			int intMemID = (this.Request["memID"].ToString() != "") ? int.Parse(this.Request["memID"].ToString()) : 0;
			int intState = (this.Request["state"].ToString() != "") ? int.Parse(this.Request["state"].ToString()) : 0;
			string strRemark = (this.Request["remark"].ToString() != "") ? this.Request["remark"].ToString() : "无";
			try
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
				flag = bllMem.UpdateMemState(intMemID, intState);
				PubFunction.SaveSysLog(intUserID, 3, "会员锁定", string.Concat(new string[]
				{
					"会员状态修改,会员卡号：[",
					modelMem.MemCard,
					"],姓名：[",
					modelMem.MemName,
					"],备注：",
					strRemark
				}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void RechargeMoney()
		{
			int flag = -1;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			int intMemID = (this.Request["memID"].ToString() != "") ? int.Parse(this.Request["memID"].ToString()) : 0;
			string rechargeAccount = this.Request["rechargeAccount"].ToString();
			decimal money = (this.Request["money"].ToString() != "") ? decimal.Parse(this.Request["money"]) : 0m;
			decimal giveMoney = (this.Request["giveMoney"].ToString() != "") ? decimal.Parse(this.Request["giveMoney"]) : 0m;
			string strRemark = (this.Request["remark"].ToString() != "") ? this.Request["remark"].ToString() : "无";
			DateTime createTime = DateTime.Parse(this.Request["createTime"]);
			bool sendMSM = this.Request["sendSMS"] == "true";
			bool isbank = this.Request["isbank"] == "true";
			int point = (string.IsNullOrEmpty(this.Request["point"].ToString()) || this.Request["point"] == null) ? 0 : Convert.ToInt32(this.Request["point"].ToString());
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
			Chain.BLL.PointLog bllPoingLog = new Chain.BLL.PointLog();
			Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
			string Remark = string.Concat(new object[]
			{
				"会员账户充值,充值金额：[",
				money.ToString(),
				"],赠送：[",
				giveMoney,
				"],备注：",
				strRemark
			});
			try
			{
				decimal sumMoney = money + giveMoney;
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
				if (!modelShop.IsRecharge)
				{
					flag = -6;
					this.Context.Response.Write(flag);
					return;
				}
				Chain.BLL.SysShopSettlement bllSettle = new Chain.BLL.SysShopSettlement();
				bllSettle.UpDateSettlement();
				string startTime = bllSettle.GetEndTime(intUserShopID);
				if (startTime == "")
				{
					startTime = modelShop.ShopCreateTime.ToString();
				}
				int Period = modelShop.SettlementInterval;
				string sTime = DateTime.Parse(startTime).ToString("yyyy-MM-dd 00:00:00");
				string eTime = DateTime.Parse(startTime).AddDays(1.0).ToString("yyyy-MM-dd 00:00:00");
				decimal rechargeMoneySum = new Chain.BLL.MemRecharge().GetRechargeMoney(string.Concat(new object[]
				{
					" RechargeShopID=",
					intUserShopID,
					" and RechargeCreateTime>='",
					sTime,
					"' and RechargeCreateTime<='",
					eTime,
					"'"
				})) + new Chain.BLL.MemRecharge().GetGiveMoney(string.Concat(new object[]
				{
					" RechargeShopID=",
					intUserShopID,
					" and RechargeCreateTime>='",
					sTime,
					"' and RechargeCreateTime<='",
					eTime,
					"'"
				}));
				if (rechargeMoneySum + sumMoney > modelShop.RechargeMaxMoney && Period > 0)
				{
					flag = -7;
					this.Context.Response.Write(flag);
					return;
				}
				Chain.Model.MemRecharge mdRechange = new Chain.Model.MemRecharge();
				mdRechange.RechargeMemID = modelMem.MemID;
				mdRechange.RechargeAccount = rechargeAccount;
				mdRechange.RechargeMoney = money + giveMoney;
				mdRechange.RechargeShopID = intUserShopID;
				mdRechange.RechargeUserID = intUserID;
				mdRechange.RechargeCreateTime = createTime;
				mdRechange.RechargeIsApprove = true;
				mdRechange.RechargeRemark = strRemark;
				mdRechange.RechargePoint = point;
				if (isbank)
				{
					mdRechange.RechargeType = 3;
				}
				else
				{
					mdRechange.RechargeType = 2;
				}
				mdRechange.RechargeGive = giveMoney;
				mdRechange.RechargeCardBalance = modelMem.MemMoney + sumMoney;
				flag = new Chain.BLL.MemRecharge().Add(mdRechange);
				modelMem.MemMoney += sumMoney;
				modelMem.MemPoint += point;
				bllMem.Update(modelMem);
				Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
				moneyChangeLogModel.MoneyChangeMemID = intMemID;
				moneyChangeLogModel.MoneyChangeUserID = intUserID;
				moneyChangeLogModel.MoneyChangeType = 1;
				moneyChangeLogModel.MoneyChangeAccount = rechargeAccount;
				moneyChangeLogModel.MoneyChangeMoney = sumMoney;
				if (!isbank)
				{
					moneyChangeLogModel.MoneyChangeCash = money;
				}
				else
				{
					moneyChangeLogModel.MoneyChangeUnionPay = money;
				}
				moneyChangeLogModel.MemMoney = bllMem.GetModel(intMemID).MemMoney;
				moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
				moneyChangeLogModel.MoneyChangeGiveMoney = giveMoney;
				new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
				mdPoint.PointMemID = intMemID;
				mdPoint.PointNumber = point;
				mdPoint.PointChangeType = 15;
				mdPoint.PointRemark = string.Concat(new object[]
				{
					"会员充值，充值金额：[",
					money,
					"],获得积分：[",
					point,
					"]"
				});
				mdPoint.PointShopID = intUserShopID;
				mdPoint.PointCreateTime = DateTime.Now;
				mdPoint.PointUserID = intUserID;
				mdPoint.PointOrderCode = rechargeAccount;
				this.bllPoint.Add(mdPoint);
				if (PubFunction.curParameter.bolShopPointManage)
				{
					PubFunction.SetShopPoint(intUserID, intUserShopID, 1, point, string.Concat(new object[]
					{
						"会员充值金额：[",
						money,
						"],获得积分：[",
						point,
						"]，运营商扣除积分：[",
						point,
						"]"
					}), 2);
				}
				MEMPointUpdate.MEMPointRate(modelMem, point, rechargeAccount, 15, intUserID, intUserShopID);
				modelMem = new Chain.BLL.Mem().GetModel(intMemID);
				PubFunction.UpdateMemLevel(modelMem);
				if (modelMem.MemMobile != "")
				{
					if (sendMSM)
					{
						if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
						{
							flag = -2;
						}
						else if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
						{
							','
						}).Length))
						{
							string strSendContent = SMSInfo.GetSendContent(2, new SmsTemplateParameter
							{
								strCardID = modelMem.MemCard,
								strName = modelMem.MemName,
								dclTempMoney = money + giveMoney,
								dclMoney = modelMem.MemMoney,
								intTempPoint = 0,
								intPoint = modelMem.MemPoint,
								OldLevelID = modelMem.MemLevelID,
								NewLevelID = modelMem.MemLevelID,
								MemBirthday = modelMem.MemBirthday,
								MemPastTime = modelMem.MemPastTime
							}, intUserShopID);
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
						}
						else
						{
							flag = -5;
						}
					}
				}
				PubFunction.SaveSysLog(intUserID, 3, "会员充值", Remark, intUserShopID, createTime, PubFunction.ipAdress);
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void RechargeRevoke()
		{
			Chain.BLL.PointLog bllPoingLog = new Chain.BLL.PointLog();
			Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intRechargeID = (this.Request["rechargeID"] != "") ? int.Parse(this.Request["rechargeID"].ToString()) : 0;
				int arg_9D_0 = (this.Request["memID"] != "") ? int.Parse(this.Request["memID"].ToString()) : 0;
				if (intRechargeID != 0)
				{
					Chain.Model.MemRecharge modelRecharge = new Chain.BLL.MemRecharge().GetModel(intRechargeID);
					int intMemID = modelRecharge.RechargeMemID;
					this.modelMem = this.bllMem.GetModel(intMemID);
					if (this.modelMem.MemMoney < modelRecharge.RechargeMoney)
					{
						flag = -2;
					}
					else
					{
						DateTime dtRecharge = Convert.ToDateTime(modelRecharge.RechargeCreateTime);
						DataTable dtOrderLog = new Chain.BLL.OrderLog().GetList(1, "OrderMemID=" + intMemID + " and OrderPayCard>0", " OrderCreateTime DESC").Tables[0];
						if (dtOrderLog.Rows.Count > 0)
						{
							DateTime dtOrder = Convert.ToDateTime(dtOrderLog.Rows[0]["OrderCreateTime"].ToString());
							if (DateTime.Compare(dtRecharge, dtOrder) < 0)
							{
								flag = -2;
							}
						}
						if (flag != -2)
						{
							DataTable dtMemCount = new Chain.BLL.MemCount().GetList(1, "CountMemID=" + intMemID + " and CountPayCard>0", " CountCreateTime DESC").Tables[0];
							if (dtMemCount.Rows.Count > 0)
							{
								DateTime dtCount = Convert.ToDateTime(dtMemCount.Rows[0]["CountCreateTime"].ToString());
								if (DateTime.Compare(dtRecharge, dtCount) < 0)
								{
									flag = -3;
								}
							}
							if (flag != -3)
							{
								int memRevoke = this.bllMem.DrawMoney(intMemID, modelRecharge.RechargeMoney);
								this.modelMem = this.bllMem.GetModel(intMemID);
								if (memRevoke > 0)
								{
									if (new Chain.BLL.MemRecharge().Delete(intRechargeID))
									{
										PubFunction.SaveSysLog(intUserID, 2, "会员充值", string.Concat(new string[]
										{
											"会员充值撤单,充值单号:[",
											modelRecharge.RechargeAccount,
											"],充值会员:[",
											this.modelMem.MemName,
											"]"
										}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
										flag = 1;
										mdPoint.PointMemID = intMemID;
										mdPoint.PointNumber = modelRecharge.RechargePoint * -1;
										mdPoint.PointChangeType = 18;
										mdPoint.PointRemark = string.Concat(new object[]
										{
											"会员充值撤单，变动金额：[",
											modelRecharge.RechargeMoney,
											"],扣除积分：[",
											modelRecharge.RechargePoint * -1,
											"]"
										});
										mdPoint.PointShopID = intUserShopID;
										mdPoint.PointCreateTime = DateTime.Now;
										mdPoint.PointUserID = intUserID;
										mdPoint.PointOrderCode = modelRecharge.RechargeAccount;
										this.bllPoint.Add(mdPoint);
										MEMPointUpdate.MEMPointRate(this.modelMem, modelRecharge.RechargePoint * -1, modelRecharge.RechargeAccount, 18, intUserID, intUserShopID);
										if (PubFunction.curParameter.bolShopPointManage)
										{
											PubFunction.SetShopPoint(intUserID, intUserShopID, 1, modelRecharge.RechargePoint * -1, "会员充值撤单返还积分", 7);
										}
										this.modelMem.MemPoint += modelRecharge.RechargePoint * -1;
										this.bllMem.Update(this.modelMem);
										PubFunction.UpdateMemLevel(this.modelMem);
										Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
										moneyChangeLogModel.MoneyChangeMemID = intMemID;
										moneyChangeLogModel.MoneyChangeUserID = intUserID;
										moneyChangeLogModel.MoneyChangeType = 2;
										moneyChangeLogModel.MoneyChangeAccount = modelRecharge.RechargeAccount;
										moneyChangeLogModel.MoneyChangeMoney = -modelRecharge.RechargeMoney;
										if (moneyChangeLogModel.MoneyChangeType != 1)
										{
											if (modelRecharge.RechargeType != 2)
											{
												moneyChangeLogModel.MoneyChangeUnionPay = -modelRecharge.RechargeMoney + modelRecharge.RechargeGive;
											}
											else
											{
												moneyChangeLogModel.MoneyChangeCash = -modelRecharge.RechargeMoney + modelRecharge.RechargeGive;
											}
										}
										moneyChangeLogModel.MemMoney = this.modelMem.MemMoney - modelRecharge.RechargeMoney;
										moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
										moneyChangeLogModel.MoneyChangeGiveMoney = ((modelRecharge.RechargeGive == 0m) ? 0m : (-modelRecharge.RechargeGive));
										new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void ChangeCard()
		{
			int flag = -1;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			int arg_54_0 = (this.Request["memID"].ToString() != "") ? int.Parse(this.Request["memID"]) : 0;
			string newCard = (this.Request["newCard"].ToString() != "") ? this.Request["newCard"].ToString() : "";
			string copyPwd = (this.Request["copyPwd"].ToString() != "") ? this.Request["copyPwd"].ToString() : "";
			string oldPwd = (this.Request["oldPwd"].ToString() != "") ? this.Request["oldPwd"].ToString() : "";
			string newPwd = (this.Request["newPwd"].ToString() != "") ? this.Request["newPwd"].ToString() : "";
			string strRemark = (this.Request["remark"].ToString() != "") ? this.Request["remark"].ToString() : "无";
			try
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(int.Parse(this.Request["memID"]));
				oldPwd = DESEncrypt.Encrypt(oldPwd);
				if (oldPwd != modelMem.MemPassword && PubFunction.curParameter.bolPwd)
				{
					flag = -3;
				}
				else if (bllMem.Exists(modelMem.MemID, newCard, "", "", modelMem.MemShopID) != 1)
				{
					flag = -4;
				}
				else if (PubFunction.curParameter.bolIsSendCard)
				{
					if (PubFunction.IsCanRegisterCard(intUserShopID, newCard, ""))
					{
						if (PubFunction.curParameter.bolPwd)
						{
							if (copyPwd == "true")
							{
								newPwd = oldPwd;
							}
							else
							{
								newPwd = DESEncrypt.Encrypt(newPwd);
							}
						}
						else if (copyPwd == "true")
						{
							newPwd = modelMem.MemPassword;
						}
						else
						{
							newPwd = DESEncrypt.Encrypt(newPwd);
						}
						if (bllMem.ChangeCard(modelMem, newCard, newPwd))
						{
							flag = 1;
							PubFunction.SaveSysLog(intUserID, 3, "会员换卡", string.Concat(new string[]
							{
								"会员换卡,会员卡号：[",
								modelMem.MemCard,
								"],会员姓名：[",
								modelMem.MemName,
								"],卡号更换为：[",
								newCard,
								"],备注：",
								strRemark
							}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
						else
						{
							flag = 0;
						}
					}
					else
					{
						flag = -7;
					}
				}
				else
				{
					if (PubFunction.curParameter.bolPwd)
					{
						if (copyPwd == "true")
						{
							newPwd = oldPwd;
						}
						else
						{
							newPwd = DESEncrypt.Encrypt(newPwd);
						}
					}
					else if (copyPwd == "true")
					{
						newPwd = modelMem.MemPassword;
					}
					else
					{
						newPwd = DESEncrypt.Encrypt(newPwd);
					}
					if (bllMem.ChangeCard(modelMem, newCard, newPwd))
					{
						flag = 1;
						PubFunction.SaveSysLog(intUserID, 3, "会员换卡", string.Concat(new string[]
						{
							"会员换卡,会员卡号：[",
							modelMem.MemCard,
							"],会员姓名：[",
							modelMem.MemName,
							"],卡号更换为：[",
							newCard,
							"],备注：",
							strRemark
						}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
					else
					{
						flag = 0;
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void DelayTime()
		{
			int flag = -1;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intMemID = (this.Request["memID"].ToString() != "") ? int.Parse(this.Request["memID"].ToString()) : 0;
				DateTime pastTime = DateTime.Parse(this.Request["newPastTime"]);
				string strRemark = (this.Request["remark"] != "") ? this.Request["remark"].ToString() : "无";
				Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(intMemID);
				flag = new Chain.BLL.Mem().UpdateMemPastTime(intMemID, pastTime);
				Chain.BLL.MemberSMSRemindLog MemberSMSRemindLogBll = new Chain.BLL.MemberSMSRemindLog();
				MemberSMSRemindLogBll.DeleteMemByMemID(intMemID);
				PubFunction.SaveSysLog(intUserID, 3, "会员延期", string.Concat(new string[]
				{
					"会员延期,会员卡号:[",
					modelMem.MemCard,
					"],姓名：[",
					modelMem.MemName,
					"],延期到:[",
					pastTime.ToShortDateString(),
					"],备注：",
					strRemark
				}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void MemCount()
		{
			string flag = "0";
			try
			{
				int intMemID = (this.Request["MemID"].ToString() != "") ? int.Parse(this.Request["MemID"].ToString()) : 0;
				decimal dclTotalMoney = (this.Request["Money"].ToString() != "") ? decimal.Parse(this.Request["Money"].ToString()) : 0m;
				decimal dclDiscountMoney = (this.Request["DiscountMoney"].ToString() != "") ? decimal.Parse(this.Request["DiscountMoney"].ToString()) : 0m;
				int intPoint = (this.Request["Point"].ToString() != "") ? int.Parse(this.Request["Point"].ToString()) : 0;
				decimal dclCardPayMoney = (this.Request["parameter[0][CardMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CardMoney]"]) : 0m;
				decimal dclCashPayMoney = (this.Request["parameter[0][CashMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CashMoney]"]) : 0m;
				decimal dclBinkPayMoney = (this.Request["parameter[0][BinkMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][BinkMoney]"]) : 0m;
				decimal dclCouponPayMoney = (this.Request["parameter[0][CouponMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CouponMoney]"]) : 0m;
				string strOrderTime = this.Request["OrderTime"].ToString();
				string strOrderAccount = this.Request["orderAccount"].ToString();
				string strRemark = (this.Request["Remark"].ToString() != "") ? this.Request["Remark"].ToString() : "无";
				string strOrderType = this.Request["parameter[0][payType]"];
				bool IsMSM = this.Request["IsSMS"] == "true";
				bool bolIsCard = bool.Parse(this.Request["parameter[0][IsCard]"]);
				bool bolIsCash = bool.Parse(this.Request["parameter[0][IsCash]"]);
				bool bolIsBink = bool.Parse(this.Request["parameter[0][IsBink]"]);
				int intDataCount = (this.Request["DataCount"].ToString() != "") ? int.Parse(this.Request["DataCount"].ToString()) : 0;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intPayType = 0;
				int usePoint = int.Parse(this.Request["parameter[0][usePoint]"]);
				decimal usePointAmount = decimal.Parse(this.Request["parameter[0][usePointAmount]"]);
				if (PubFunction.IsShopPoint(intUserShopID, ref intPoint))
				{
					Chain.Model.MemCount modelMemCount = new Chain.Model.MemCount();
					modelMemCount.CountMemID = intMemID;
					modelMemCount.CountAccount = strOrderAccount;
					modelMemCount.CountTotalMoney = dclTotalMoney;
					modelMemCount.CountDiscountMoney = dclDiscountMoney;
					modelMemCount.CountPoint = intPoint;
					modelMemCount.CountIsCard = bolIsCard;
					modelMemCount.CountPayCard = dclCardPayMoney;
					modelMemCount.CountIsCash = bolIsCash;
					modelMemCount.CountPayCash = dclCashPayMoney;
					modelMemCount.CountIsBink = bolIsBink;
					modelMemCount.CountPayBink = dclBinkPayMoney;
					modelMemCount.CountPayCoupon = dclCouponPayMoney;
					modelMemCount.CountCreateTime = DateTime.Now;
					modelMemCount.CountPayType = intPayType;
					modelMemCount.CountPayCoupon = dclCouponPayMoney;
					modelMemCount.CountRemark = strRemark;
					modelMemCount.CountShopID = intUserShopID;
					modelMemCount.CountUserID = intUserID;
					Chain.BLL.MemCount bllMemCount = new Chain.BLL.MemCount();
					int intCountID = bllMemCount.Add(modelMemCount);
					if (intCountID > 0)
					{
						for (int i = 0; i < intDataCount; i++)
						{
							Chain.Model.MemCountDetail modelCountDetail = new Chain.Model.MemCountDetail();
							modelCountDetail.CountDetailCountID = intCountID;
							modelCountDetail.CountDetailGoodsID = int.Parse(this.Request["Data[" + i + "][GoodsID]"]);
							modelCountDetail.CountDetailMemID = intMemID;
							modelCountDetail.CountDetailDiscountMoney = decimal.Parse(this.Request["Data[" + i + "][ExpMoney]"]);
							modelCountDetail.CountDetailTotalNumber = int.Parse(this.Request["Data[" + i + "][ExpNum]"]);
							modelCountDetail.CountDetailNumber = int.Parse(this.Request["Data[" + i + "][ExpNum]"]);
							modelCountDetail.CountDetailPoint = int.Parse(this.Request["Data[" + i + "][ExpPoint]"]);
							modelCountDetail.CountCreateTime = DateTime.Now;
							Chain.BLL.MemCountDetail bllCountDetail = new Chain.BLL.MemCountDetail();
							bllCountDetail.Add(modelCountDetail);
							if (strOrderType != "EmptyBills")
							{
								int intStaffID = (this.Request["data[" + i + "][ExpStaffName]"] != "") ? int.Parse(this.Request["data[" + i + "][ExpStaffName]"]) : 0;
								if (intStaffID != 0)
								{
									this.modelStaffMoney.StaffID = intStaffID;
									this.modelStaffMoney.StaffTotalMoney = Math.Abs(decimal.Parse(this.Request["data[" + i + "][ExpStaffMoney]"]));
									this.modelStaffMoney.StaffOrderCode = strOrderAccount;
									this.modelStaffMoney.StaffMemID = intMemID;
									this.modelStaffMoney.StaffGoodsID = int.Parse(this.Request["data[" + i + "][GoodsID]"]);
									this.modelStaffMoney.StaffShopID = intUserShopID;
									this.modelStaffMoney.StaffCreateTime = DateTime.Now;
									this.modelStaffMoney.StaffOrderDetailID = intCountID;
									this.modelStaffMoney.StaffType = 2;
									this.bllStaffMoney.Add(this.modelStaffMoney);
								}
							}
						}
						Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
						Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
						int intLevelID = modelMem.MemLevelID;
						this.modelPoint.PointMemID = intMemID;
						this.modelPoint.PointNumber = intPoint;
						this.modelPoint.PointChangeType = 3;
						this.modelPoint.PointRemark = string.Concat(new object[]
						{
							"会员充次成功，消费总额：[",
							dclDiscountMoney,
							"],获得积分：[",
							intPoint,
							"]"
						});
						this.modelPoint.PointShopID = intUserShopID;
						this.modelPoint.PointUserID = intUserID;
						this.modelPoint.PointCreateTime = DateTime.Parse(strOrderTime);
						this.modelPoint.PointOrderCode = strOrderAccount;
						if (this.bllPoint.Add(this.modelPoint) > 0)
						{
							decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
							modelMem.MemPoint += intPoint - usePoint;
							int mem = bllMem.MemCountUpdateMem(intMemID, dclMemMoney, modelMem.MemPoint);
							MEMPointUpdate.MEMPointRate(modelMem, modelMemCount.CountPoint, modelMemCount.CountAccount, 3, intUserID, intUserShopID);
							modelMem = new Chain.BLL.Mem().GetModel(intMemID);
							string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
							PubFunction.SetShopPoint(intUserID, intUserShopID, intPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderAccount,
								"],会员充次金额：[",
								dclDiscountMoney,
								"],扣除商家积分：[",
								intPoint,
								"]"
							}), 2);
							if (usePoint != 0)
							{
								PubFunction.SetShopPoint(intUserID, intUserShopID, -usePoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],会员充次抵用积分：[",
									usePoint,
									"]，商家回收积分：[",
									usePoint,
									"]"
								}), 4);
								Chain.Model.PointLog modelPointLog = new Chain.Model.PointLog();
								modelPointLog.PointMemID = intMemID;
								modelPointLog.PointNumber = usePoint;
								modelPointLog.PointChangeType = 1;
								modelPointLog.PointRemark = string.Concat(new object[]
								{
									"会员充次成功,抵用积分：[",
									usePoint,
									"],抵用金额：[",
									usePointAmount,
									"]"
								});
								modelPointLog.PointShopID = intUserShopID;
								modelPointLog.PointUserID = intUserID;
								modelPointLog.PointCreateTime = DateTime.Now;
								modelPointLog.PointOrderCode = strOrderAccount;
								this.bllPoint.Add(modelPointLog);
							}
							Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
							Chain.Model.SysShop modelShop = bllSysShop.GetModel(intUserShopID);
							decimal flTotalRate = decimal.Parse(Convert.ToDouble(modelShop.TotalRate).ToString("#0.00"));
							if (flTotalRate > 0m)
							{
								int flTotalPoint = (int)(flTotalRate * intPoint);
								decimal alliancePercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.AllianceRebatePercent).ToString("#0.00"));
								decimal cardShopPercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.CardShopRebatePercent).ToString("#0.00"));
								int alliancePoint = (int)(flTotalPoint * alliancePercent);
								int cardShopPoint = (int)(flTotalPoint * cardShopPercent);
								int zbPoint = flTotalPoint - alliancePoint - cardShopPoint;
								Chain.BLL.ReturnPointLog bllReturnPoint = new Chain.BLL.ReturnPointLog();
								bllReturnPoint.Add(new Chain.Model.ReturnPointLog
								{
									OrderAccount = strOrderAccount,
									MemID = intMemID,
									TotalPoint = flTotalPoint,
									AlliancePoint = alliancePoint,
									ZbPoint = zbPoint,
									CardShopPoint = cardShopPoint,
									Remark = "会员充次,商家返利积分",
									ReturnShopID = intUserShopID,
									CreateTime = DateTime.Now
								});
								PubFunction.SetShopPoint(intUserID, intUserShopID, intUserShopID, flTotalPoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],会员充次得积分：[",
									intPoint,
									"],返利总比例：[",
									flTotalRate,
									"],商家扣除返利总积分：[",
									flTotalPoint,
									"]"
								}), 2);
								PubFunction.SetShopPoint(intUserID, intUserShopID, modelShop.FatherShopID, -alliancePoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],商家总返利积分：[",
									flTotalPoint,
									"],联盟商返利比例：[",
									alliancePercent,
									"],联盟商得到返利积分：[",
									alliancePoint,
									"]"
								}), 3);
								PubFunction.SetShopPoint(intUserID, intUserShopID, modelMem.MemShopID, -cardShopPoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],商家总返利积分：[",
									flTotalPoint,
									"],开卡商家返利比例：[",
									cardShopPercent,
									"],开卡商家得到返利积分：[",
									cardShopPoint,
									"]"
								}), 3);
								PubFunction.SetShopPoint(intUserID, intUserShopID, 1, -zbPoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],商家总返利积分：[",
									flTotalPoint,
									"],运营商得到返利积分：[",
									zbPoint,
									"]"
								}), 3);
							}
							Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
							moneyChangeLogModel.MoneyChangeMemID = intMemID;
							moneyChangeLogModel.MoneyChangeUserID = intUserID;
							moneyChangeLogModel.MoneyChangeType = 8;
							moneyChangeLogModel.MoneyChangeAccount = strOrderAccount;
							moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
							moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
							moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
							moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
							moneyChangeLogModel.MemMoney = modelMem.MemMoney;
							moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
							moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
							new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
							flag = "{\"strUpdateMemLevel\":\"" + strUpdateMemLevel + "\"}";
							if (IsMSM)
							{
								if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
								{
									flag = "-2";
								}
								else if (modelMem.MemMobile != "")
								{
									if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
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
										smsTemplateParameter.OldLevelID = intLevelID;
										modelMem = new Chain.BLL.Mem().GetModel(intMemID);
										smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
										smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
										smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
										string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, intUserShopID);
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
											PubFunction.SetShopSms(intUserID, this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
											{
												','
											}).Length, 4);
										}
									}
									else
									{
										flag = "-5";
									}
								}
							}
							string Remark = string.Concat(new object[]
							{
								"会员充次,会员卡号：[",
								modelMem.MemCard,
								"],姓名：[",
								modelMem.MemName,
								"],订单号：[",
								strOrderAccount,
								"],消费金额：[",
								dclDiscountMoney,
								"],获得积分：[",
								intPoint,
								"],备注：",
								strRemark
							});
							PubFunction.SaveSysLog(intUserID, 4, "会员充次", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
				}
				else
				{
					flag = "-6";
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-1";
			}
			this.Context.Response.Write(flag);
		}

		public void GetModelByMemCard()
		{
			string flag = "1";
			string memCard = this.Context.Request["memCard"];
			try
			{
				flag = ((new Chain.BLL.Mem().GetModelByMemCard(memCard) == null) ? "0" : "1");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void GetGoodsClassModel()
		{
			string flag = "0";
			try
			{
				int intClassID = int.Parse(this.Request["ClassID"]);
				DataTable dtClass = this.bllGdClass.GetItem(intClassID);
				if (dtClass.Rows.Count != 1)
				{
					flag = "-1";
				}
				else
				{
					string strClass = JsonPlus.ToJson(dtClass.Rows[0], "");
					int intParentID = int.Parse(dtClass.Rows[0]["ParentID"].ToString());
					string strParent = "{}";
					if (intParentID != 0)
					{
						DataTable dtParent = this.bllGdClass.GetItem(intParentID);
						if (dtParent.Rows.Count != 1)
						{
							flag = "-2";
						}
						else
						{
							strParent = JsonPlus.ToJson(dtParent.Rows[0], "");
						}
					}
					flag = string.Concat(new string[]
					{
						"{\"ClassModel\":",
						strClass,
						",\"ParentModel\":",
						strParent,
						"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-3";
			}
			this.Context.Response.Write(flag);
		}

		public void GetGoodsClassList()
		{
			string flag = "0";
			try
			{
				int intShopID = string.IsNullOrEmpty(this.Request["ShopID"].ToString()) ? 1 : int.Parse(this.Request["ShopID"]);
				using (DataTable dtSource = this.bllGdClass.GetListByShopID(intShopID).Tables[0])
				{
					DataTable dt = dtSource.Clone();
					this.GetGoodClassTree(0, 0, dt, dtSource);
					if (dt.Rows.Count == 0)
					{
						flag = "-1";
					}
					else
					{
						flag = JsonPlus.ToJson(dt, "");
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-3";
			}
			this.Context.Response.Write(flag);
		}

		public void GetGoodClassTree(int intParentID, int intLevel, DataTable dt, DataTable dtSource)
		{
			DataRow[] dr = dtSource.Select(" ParentID=" + intParentID);
			for (int i = 0; i < dr.Length; i++)
			{
				dr[i]["ClassName"] = new string('-', intLevel * 3).ToString() + dr[i]["ClassName"].ToString();
				dt.Rows.Add(dt.NewRow());
				dt.Rows[dt.Rows.Count - 1]["ClassID"] = dr[i]["ClassID"];
				dt.Rows[dt.Rows.Count - 1]["ClassName"] = dr[i]["ClassName"];
				this.GetGoodClassTree(Convert.ToInt32(dr[i]["ClassID"]), intLevel + 1, dt, dtSource);
			}
		}

		public void GoodsClassAdd()
		{
			int flag = 0;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			try
			{
				string strClassName = this.Request["ClassName"];
				strClassName = PubFunction.RemoveSpace(strClassName);
				string strClassRemark = (this.Request["ClassRemark"] != "") ? this.Request["ClassRemark"].ToString() : "";
				strClassRemark = PubFunction.RemoveSpace(strClassRemark);
				this.modelGdClass.ClassName = strClassName;
				this.modelGdClass.ParentID = int.Parse(this.Request["ParentID"]);
				this.modelGdClass.ClassRemark = strClassRemark;
				this.modelGdClass.CreateShopID = int.Parse(this.Request["ShopID"]);
				flag = this.bllGdClass.Add(this.modelGdClass);
				if (flag > 0)
				{
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
					PubFunction.SaveSysLog(intUserID, 1, "商品分类", "新增商品分类，分类名称：[" + this.modelGdClass.ClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
					new Chain.BLL.GoodsClassDiscount().InitGoodsLevelDiscountByGoodsClassID(flag);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsClassEdit()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intClassID = int.Parse(this.Request["ClassID"]);
				int intParentID = int.Parse(this.Request["ParentID"].ToString());
				string strClassName = this.Request["ClassName"];
				string strClassRemark = (this.Request["ClassRemark"] != "") ? this.Request["ClassRemark"].ToString() : "";
				this.modelGdClass.ClassID = intClassID;
				this.modelGdClass.ClassName = strClassName;
				this.modelGdClass.ClassRemark = strClassRemark;
				this.modelGdClass.ParentID = intParentID;
				this.modelGdClass.CreateShopID = int.Parse(this.Request["ShopID"]);
				flag = this.bllGdClass.UpdateByShop(this.modelGdClass);
				if (flag > 0)
				{
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
					PubFunction.SaveSysLog(intUserID, 1, "商品分类", "编辑商品分类，分类名称：[" + this.modelGdClass.ClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsClassDel()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intClassID = int.Parse(this.Request["ClassID"]);
				int intShopID = int.Parse(this.Request["ShopID"]);
				Chain.BLL.GoodsClass bllGoodsClass = new Chain.BLL.GoodsClass();
				flag = bllGoodsClass.DeleteClassByShop(intClassID, intShopID);
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsClassSync()
		{
			int flag = 0;
			try
			{
				int intClassID = int.Parse(this.Request["ClassID"]);
				Chain.BLL.GoodsClassAuthority bllGCA = new Chain.BLL.GoodsClassAuthority();
				bllGCA.SyncGoodsClass(intClassID);
				flag = 1;
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GetIsParentID()
		{
			string strIsParent = "";
			try
			{
				int intGoodsClassID = int.Parse(this.Request["parentID"].ToString());
				DataTable dtGoodsClass = new Chain.BLL.GoodsClass().GetList(" ParentID=" + intGoodsClassID).Tables[0];
				if (dtGoodsClass.Rows.Count > 0)
				{
					strIsParent = "{\"IsParent\":\"" + 1 + "\"}";
				}
				else
				{
					strIsParent = "{\"IsParent\":\"" + 0 + "\"}";
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(strIsParent);
		}

		public void GetGoodsInfo()
		{
			string msgResponse = "";
			try
			{
				int intGoodsID = int.Parse(this.Request["goodsID"]);
				int intUserShopID = this.UserModel.UserShopID;
				int memLevelID = (this.Request["MemLevelID"] == null) ? -1 : int.Parse(this.Request["MemLevelID"]);
				int resCount;
				DataTable dtGoods = this.bllGoods.GetGoodsStockList(intUserShopID, memLevelID, intGoodsID.ToString(), 1, 1, out resCount, new string[]
				{
					""
				}).Tables[0];
				if (dtGoods != null)
				{
					msgResponse = JsonPlus.ToJson(dtGoods, "");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGoodsModelByBarCode()
		{
			string msgResponse = "";
			try
			{
				string strGoodsCode = this.Request["barCode"].ToString();
				int shopID = this.UserModel.UserShopID;
				DataTable dtGoods = this.bllNumber.GetGoodsNumber(strGoodsCode, shopID).Tables[0];
				if (dtGoods.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtGoods, "");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGoodsByCodeDelServe()
		{
			string msgResponse = "";
			try
			{
				string strGoodsCode = this.Request["barCode"].ToString();
				int shopID = this.UserModel.UserShopID;
				DataTable dtGoods = this.bllNumber.GetGoodsNumberDelServe(strGoodsCode, shopID).Tables[0];
				if (dtGoods.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtGoods, "");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGoodsNumber()
		{
			string msgResponse = "";
			try
			{
				int intGoodsID = int.Parse(this.Request["goodsID"].ToString());
				int OutShopID = this.UserModel.UserShopID;
				try
				{
					OutShopID = int.Parse(this.Request["OutshopID"].ToString());
				}
				catch (Exception e)
				{
					this.LogError(e);
				}
				int InshopID = int.Parse(this.Request["InshopID"].ToString());
				DataSet ds = this.bllNumber.CkeckInShopGoods(intGoodsID, InshopID);
				if (ds.Tables[0].Rows.Count > 0)
				{
					DataTable dtGoods = this.bllNumber.GetGoodsNumber(intGoodsID, OutShopID).Tables[0];
					if (dtGoods != null)
					{
						msgResponse = JsonPlus.ToJson(dtGoods, "");
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GoodsAddAndEdit()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intGoodsID = (this.Request["txtGoodsID"] != "") ? int.Parse(this.Request["txtGoodsID"].ToString()) : 0;
				string strGoodsNumber = this.Request["txtGoodsNumber"].ToString();
				int goodsShopID;
				if (this.Request["hdShopID"].ToString() != "")
				{
					goodsShopID = int.Parse(this.Request["hdShopID"].ToString());
				}
				else
				{
					goodsShopID = 0;
				}
				this.modelGoods.GoodsCode = this.Request["txtGoodsCode"].ToString();
				bool bolService = this.Request["chkService"] != null && this.Request["chkService"] == "on";
				this.modelGoods.Name = this.Request["txtGoodsName"].ToString();
				this.modelGoods.NameCode = this.Request["txtGoodsNameCode"].ToString();
				this.modelGoods.GoodsClassID = int.Parse(this.Request["sltGoodsClass"].ToString());
				if (this.Request["sltShopList"] != null && !string.IsNullOrEmpty(this.Request["sltShopList"].ToString()))
				{
					this.modelGoods.CreateShopID = int.Parse(this.Request["sltShopList"].ToString());
				}
				else
				{
					this.modelGoods.CreateShopID = int.Parse(this.Request["txtShopID"].ToString());
				}
				this.modelGoods.Unit = this.Request["sltjldw"].ToString();
				this.modelGoods.CommissionType = int.Parse(this.Request["sltCommissionType"].ToString());
				this.modelGoods.CommissionNumber = decimal.Parse(this.Request["txtCommissionNumber"].ToString());
				this.modelGoods.Price = decimal.Parse(this.Request["txtGoodsPrice"].ToString());
				this.modelGoods.Point = ((this.Request["txtGoodsPoint"] != "") ? int.Parse(this.Request["txtGoodsPoint"].ToString()) : -1);
				this.modelGoods.GoodsBidPrice = ((this.Request["txtGoodsBidPrice"] != "") ? decimal.Parse(this.Request["txtGoodsBidPrice"].ToString()) : 0m);
				this.modelGoods.MinPercent = decimal.Parse(this.Request["txtGoodsMinPercent"].ToString());
				string strGoodsRemark = this.Request["txtGoodsRemark"].ToString();
				bool chkSyncOtherShop = this.Request["chkSyncOtherShop"] != null && this.Request["chkSyncOtherShop"] == "on";
				bool chkSyncPartialShop = this.Request["chkSyncPartialShop"] != null && this.Request["chkSyncPartialShop"] == "on";
				strGoodsRemark = PubFunction.RemoveSpace(strGoodsRemark);
				this.modelGoods.GoodsRemark = strGoodsRemark;
				this.modelGoods.GoodsPicture = "";
				this.modelGoods.GoodsCreateTime = DateTime.Now;
				this.modelGoods.SalePercet = decimal.Parse(this.Request["txtGoodsSalePercent"].ToString());
				if (bolService)
				{
					this.modelGoods.GoodsType = 1;
				}
				else
				{
					this.modelGoods.GoodsType = 0;
				}
				if (intGoodsID == 0)
				{
					int intAdd = this.bllGoods.Add(this.modelGoods);
					if (intAdd < 0)
					{
						flag = intAdd;
					}
					if (intAdd > 0)
					{
						flag = 1;
						this.modelGoods.GoodsID = intAdd;
						this.bllNumber.InsertGoodsNumber(intAdd, this.modelGoods.CreateShopID);
						PubFunction.SaveSysLog(intUserID, 1, "商品新增", "新增商品，商品名称：[" + this.modelGoods.Name + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
				else
				{
					this.modelGoods.GoodsID = intGoodsID;
					flag = this.bllGoods.Update(this.modelGoods);
					decimal number = this.bllNumber.GetNumber(intGoodsID, goodsShopID);
					if (flag > 0 && number != decimal.Parse(strGoodsNumber))
					{
						if (this.bllNumber.UpdateGoodsNumber(intGoodsID, decimal.Parse(strGoodsNumber), goodsShopID) > 0)
						{
							this.modelGoodsLog.GoodsAccount = "BJ" + DateTime.Now.ToString("yyMMddHHmmssffff");
							this.modelGoodsLog.Type = ((decimal.Parse(strGoodsNumber) > number) ? 8 : 9);
							this.modelGoodsLog.GoodsID = intGoodsID;
							this.modelGoodsLog.TotalPrice = decimal.Parse(this.Request["txtGoodsPrice"].ToString());
							this.modelGoodsLog.GoodsNumber = 0;
							this.modelGoodsLog.Remark = "商品库存编辑";
							this.modelGoodsLog.CreateTime = DateTime.Now;
							this.modelGoodsLog.ShopID = intUserShopID;
							this.modelGoodsLog.UserID = intUserID;
							flag = this.bllGoodsLog.Add(this.modelGoodsLog);
							if (flag > 0)
							{
								this.modelGoodsDetail.GoodsLogID = flag;
								this.modelGoodsDetail.GoodsID = intGoodsID;
								this.modelGoodsDetail.GoodsInPrice = 0m;
								this.modelGoodsDetail.GoodsOutPrice = 0m;
								this.modelGoodsDetail.GoodsNumber = Math.Abs(number - decimal.Parse(strGoodsNumber));
								this.bllGoodsDetail.Add(this.modelGoodsDetail);
							}
						}
						else
						{
							flag = 0;
						}
					}
					PubFunction.SaveSysLog(intUserID, 3, "商品编辑", "编辑商品，商品名称：[" + this.modelGoods.Name + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
				if (flag > 0)
				{
					Chain.BLL.GoodsClassAuthority bllGCA = new Chain.BLL.GoodsClassAuthority();
					if (chkSyncOtherShop)
					{
						bllGCA.SyncGoodsClass(this.modelGoods.GoodsClassID);
						this.bllNumber.SyncGoods(this.modelGoods.GoodsID);
					}
					if (chkSyncPartialShop)
					{
						string SyncShopList = this.Request["SyncShop"].ToString();
						string[] arrShopList = SyncShopList.Split(new char[]
						{
							','
						});
						List<int> listShopList = new List<int>();
						string[] array = arrShopList;
						for (int i = 0; i < array.Length; i++)
						{
							string item = array[i];
							int shopId;
							if (int.TryParse(item, out shopId))
							{
								listShopList.Add(shopId);
							}
						}
						bllGCA.SyncGoodsClass(this.modelGoods.GoodsClassID, listShopList);
						this.bllNumber.SyncGoods(this.modelGoods.GoodsID, listShopList);
					}
					if (intGoodsID != 0)
					{
						DataTable dt = this.bllNumber.GetList("GoodsID=" + intGoodsID).Tables[0];
						List<int> listShopList = new List<int>();
						foreach (DataRow item2 in dt.Rows)
						{
							listShopList.Add(Convert.ToInt32(item2["ShopID"]));
						}
						if (listShopList.Count > 0)
						{
							bllGCA.SyncGoodsClass(this.modelGoods.GoodsClassID, listShopList);
						}
					}
				}
				DataRow[] drs = new Chain.BLL.MemCustomField().CustomGetList(" CustomType=2 ");
				Hashtable hash = new Hashtable();
				if (drs.Length > 0)
				{
					DataRow[] array2 = drs;
					for (int i = 0; i < array2.Length; i++)
					{
						DataRow dr = array2[i];
						hash.Add(dr["CustomField"].ToString(), this.Request["Goods_Custom_" + dr["CustomField"].ToString()]);
					}
					this.bllGoods.AddCustomField(this.modelGoods.GoodsCode, hash);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -2;
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsSync()
		{
			int flag = 0;
			try
			{
				int intGoodsID = int.Parse(this.Request["GoodsID"]);
				this.modelGoods = this.bllGoods.GetModel(intGoodsID);
				Chain.BLL.GoodsClassAuthority bllGCA = new Chain.BLL.GoodsClassAuthority();
				bllGCA.SyncGoodsClass(this.modelGoods.GoodsClassID);
				this.bllNumber.SyncGoods(intGoodsID);
				flag = 1;
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsDel()
		{
			int flag = 0;
			try
			{
				int intGoodsID = int.Parse(this.Request["GoodsID"]);
				int intShopID = int.Parse(this.Request["ShopID"]);
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				this.modelGoods = this.bllGoods.GetModel(intGoodsID);
				DataTable dtOrder = this.bllDetail.GetList(" GoodsID=" + intGoodsID).Tables[0];
				DataTable dtCount = this.bllCountDetail.GetList(" CountDetailGoodsID=" + intGoodsID).Tables[0];
				DataTable dtLog = this.bllGoodsDetail.GetList("GoodsID=" + intGoodsID).Tables[0];
				if (dtOrder.Rows.Count > 0 || dtCount.Rows.Count > 0 || dtLog.Rows.Count > 0)
				{
					flag = -2;
				}
				else if (this.bllNumber.DeleteNumber(intGoodsID, intShopID))
				{
					flag = 1;
					DataTable dt = this.bllNumber.GetShopIDListByGoods(intGoodsID).Tables[0];
					if (dt.Rows.Count <= 0)
					{
						if (this.bllGoods.Delete(intGoodsID))
						{
							flag = 1;
							PubFunction.SaveSysLog(intUserID, 2, "商品删除", "删除商品，商品名称：[" + this.modelGoods.Name + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GetGoodsProductList()
		{
			string msgResponse = "";
			try
			{
				int intSize = int.Parse(this.Request["size"].ToString());
				int intIndex = int.Parse(this.Request["index"].ToString());
				string strKey = (this.Request["key"] != "") ? this.Request["key"].ToString() : "";
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append(" 1=1 ");
				if (strKey != "")
				{
					sbWhere.AppendFormat(" and (Name like '%{0}%' or NameCode like '%{0}%' or GoodsCode='{0}')", strKey);
				}
				int resCount;
				DataTable dtGoods = this.bllGoods.GetList(intSize, intIndex, out resCount, new string[]
				{
					sbWhere.ToString()
				}).Tables[0];
				if (dtGoods != null)
				{
					string strJson = JsonPlus.ToJson(dtGoods, "GoodsID,Name,Price,MinPercent,Point,CommissionNumber,CommissionType,GoodsType");
					msgResponse = string.Concat(new object[]
					{
						"{\"RecordCount\":",
						resCount,
						",\"List\":",
						strJson,
						"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetServingProductList()
		{
			string msgResponse = "";
			try
			{
				int intSize = int.Parse(this.Request["size"].ToString());
				int intIndex = int.Parse(this.Request["index"].ToString());
				int intMemID = int.Parse(this.Request["memID"]);
				string strKey = (this.Request["key"] != "") ? this.Request["key"].ToString() : "";
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append(" Number > 0 AND  CountDetailMemID= " + intMemID);
				if (strKey != "")
				{
					sbWhere.AppendFormat(" and (Name like '%{0}%' or NameCode like '%{0}%' or GoodsCode='{0}')", strKey);
				}
				int resCount;
				DataTable dtCuont = this.bllCountDetail.GetMemCountList(intSize, intIndex, out resCount, new string[]
				{
					sbWhere.ToString()
				}).Tables[0];
				if (dtCuont != null)
				{
					string strJson = JsonPlus.ToJson(dtCuont, "");
					msgResponse = string.Concat(new object[]
					{
						"{\"RecordCount\":",
						resCount,
						",\"List\":",
						strJson,
						"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGoodsList()
		{
			string msgResponse = "";
			try
			{
				int intSize = (this.Request["size"].ToString() != "") ? int.Parse(this.Request["size"].ToString()) : 0;
				int intIndex = (this.Request["index"].ToString() != "") ? int.Parse(this.Request["index"].ToString()) : 0;
				string strKey = this.Request["key"].ToString();
				int intShopID = (this.Request["shopID"] != "") ? int.Parse(this.Request["shopID"].ToString()) : -1;
				bool bolGoodsExpense = bool.Parse(this.Request["bolGoodsExpense"].ToString());
				bool isCheckStock = bool.Parse(this.Request["IsCheckStock"]);
				int intGoodsClass = (this.Request["ClassID"] != "") ? int.Parse(this.Request["ClassID"].ToString()) : 0;
				int memLevelID = (this.Request["MemLevelID"] == null) ? -1 : int.Parse(this.Request["MemLevelID"]);
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append(" 1=1 ");
				if (strKey != "")
				{
					sbWhere.AppendFormat(" and (Name like '%{0}%' or NameCode like '%{0}%' or GoodsCode='{0}')", strKey);
				}
				if (!bolGoodsExpense)
				{
					sbWhere.Append(" and GoodsType=0");
				}
				if (intGoodsClass != 0)
				{
					sbWhere.AppendFormat(" and GoodsClassID in ({0}) ", PubFunction.GetClassID(intGoodsClass));
				}
				if (isCheckStock)
				{
					sbWhere.Append("and ((Number>0 and GoodsType=0) or GoodsType=1) ");
				}
				int resCount;
				DataTable dtGoods = this.bllGoods.GetGoodsStockList(intShopID, memLevelID, "", intSize, intIndex, out resCount, new string[]
				{
					sbWhere.ToString()
				}).Tables[0];
				if (dtGoods != null)
				{
					if (memLevelID == -1)
					{
						string strJson = JsonPlus.ToJson(dtGoods, "GoodsID,Name,Price,GoodsBidPrice,Number,GoodsCode,Point,MinPercent,SalePercet,GoodsType,NameCode,ShopName,GoodsDiscount,DiscountPrice,PointDiscount");
						msgResponse = string.Concat(new object[]
						{
							"{\"RecordCount\":",
							resCount,
							",\"List\":",
							strJson,
							"}"
						});
					}
					else
					{
						string strJson = JsonPlus.ToJson(dtGoods, "GoodsID,Name,Price,GoodsBidPrice,Number,GoodsCode,Point,MinPercent,SalePercet,GoodsType,NameCode,ShopName,GoodsDiscount,DiscountPrice,PointDiscount");
						msgResponse = string.Concat(new object[]
						{
							"{\"RecordCount\":",
							resCount,
							",\"List\":",
							strJson,
							"}"
						});
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				msgResponse = "-1";
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGoodsStockTotal()
		{
			string msgResponse = "";
			try
			{
				int intSize = (this.Request["size"].ToString() != "") ? int.Parse(this.Request["size"].ToString()) : 0;
				int intIndex = (this.Request["index"].ToString() != "") ? int.Parse(this.Request["index"].ToString()) : 0;
				string strKey = this.Request["key"].ToString();
				bool bolGoodsExpense = bool.Parse(this.Request["bolGoodsExpense"].ToString());
				int intGoodsClass = (this.Request["ClassID"] != "") ? int.Parse(this.Request["ClassID"].ToString()) : 0;
				string strShopID = this.Request["ShopID"].ToString();
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append(" 1=1 ");
				if (strKey != "")
				{
					sbWhere.AppendFormat(" and (Name like '%{0}%' or NameCode like '%{0}%' or GoodsCode='{0}')", strKey);
				}
				if (!bolGoodsExpense)
				{
					sbWhere.Append(" and GoodsType=0");
				}
				if (intGoodsClass != 0)
				{
					sbWhere.AppendFormat(" and GoodsClassID in ({0}) ", PubFunction.GetClassID(intGoodsClass));
				}
				if (strShopID != "")
				{
					sbWhere.AppendFormat(" and ShopID={0}", int.Parse(strShopID));
				}
				else
				{
					sbWhere.Append(" and ShopID>0");
				}
				int resCount;
				DataTable dtGoods = this.bllGoods.GetGoodsList(intSize, intIndex, out resCount, new string[]
				{
					sbWhere.ToString()
				}).Tables[0];
				if (dtGoods != null)
				{
					string strJson = JsonPlus.ToJson(dtGoods, "GoodsID,Name,Price,GoodsBidPrice,Number,GoodsCode,Point,MinPercent,GoodsType,NameCode,ShopName");
					msgResponse = string.Concat(new object[]
					{
						"{\"RecordCount\":",
						resCount,
						",\"List\":",
						strJson,
						"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetAllStock()
		{
			string msgResponse = "";
			try
			{
				int intGoodsID = (this.Request["goodsID"] != "") ? int.Parse(this.Request["goodsID"].ToString()) : 0;
				int intUserShopID = this.UserModel.UserShopID;
				string strSql = " GoodsID=" + intGoodsID;
				strSql = strSql + " and ShopID!=" + intUserShopID;
				if (intGoodsID != 0)
				{
					int resCount;
					DataTable dtStock = this.bllGoods.GetGoodsList(10000, 1, out resCount, new string[]
					{
						strSql.ToString()
					}).Tables[0];
					if (dtStock != null)
					{
						string strJson = JsonPlus.ToJson(dtStock, "GoodsID,Name,Number,NameCode,ShopName");
						msgResponse = string.Concat(new object[]
						{
							"{\"RecordCount\":",
							resCount,
							",\"List\":",
							strJson,
							"}"
						});
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GoodsExpense()
		{
			string flag = "0";
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int memID = int.Parse(this.Request["memID"].ToString());
				int orderID = (!string.IsNullOrEmpty(this.Request["orderID"].ToString())) ? int.Parse(this.Request["orderID"].ToString()) : 0;
				string strOrderType = this.Request["parameter[0][payType]"];
				decimal dclDiscountMoney = decimal.Parse(this.Request["parameter[0][DiscountMoney]"]);
				decimal dclCardPayMoney = (this.Request["parameter[0][CardMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CardMoney]"]) : 0m;
				decimal dclCashPayMoney = (this.Request["parameter[0][CashMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CashMoney]"]) : 0m;
				decimal dclBinkPayMoney = (this.Request["parameter[0][BinkMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][BinkMoney]"]) : 0m;
				decimal dclCouponPayMoney = (this.Request["parameter[0][CouponMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CouponMoney]"]) : 0m;
				decimal dclTotalMoney = decimal.Parse(this.Request["totalMoney"]);
				decimal dclTotalStaffMoney = decimal.Parse(this.Request["staffMoney"]);
				int intPoint = int.Parse(this.Request["point"]);
				int usePoint = int.Parse(this.Request["parameter[0][usePoint]"]);
				decimal usePointAmount = decimal.Parse(this.Request["parameter[0][usePointAmount]"]);
				string strOrderCode = this.Request["orderCode"].ToString();
				string strRemark = this.Request["remark"].ToString();
				strRemark = PubFunction.RemoveSpace(strRemark);
				int intCount = int.Parse(this.Request["count"]);
				bool print = this.Request["print"] == "true";
				bool sendSMS = this.Request["sendSMS"] == "true";
				bool staff = this.Request["staff"] == "true";
				DateTime dtExTime = DateTime.Parse(this.Request["expensetime"].ToString());
				string strStaffName = this.Request["staffName"];
				bool bolIsCard = bool.Parse(this.Request["parameter[0][IsCard]"]);
				bool bolIsCash = bool.Parse(this.Request["parameter[0][IsCash]"]);
				bool bolIsBink = bool.Parse(this.Request["parameter[0][IsBink]"]);

                string alipaymoney = this.Request["parameter[0][alipaymoney]"];
                string alipaycode = this.Request["parameter[0][alipaycode]"];

                string wxmoney = this.Request["parameter[0][wxmoney]"];
                string wxcode = this.Request["parameter[0][wxcode]"];


                if(string.IsNullOrEmpty(wxcode)==false)
                {
                   if(ChainStock.PayApi.weixin.micropay.Run("微信", wxmoney, wxcode)==false)
                   {
                       throw new Exception("微信支付失败");
                      
                   }
                }


                if (string.IsNullOrEmpty(alipaycode) == false)
                {
                    ChainStock.PayApi.alipay.Alipay alipay = new PayApi.alipay.Alipay();
                    if (alipay.Alipay_Deliver(alipaymoney, alipaycode) == false)
                    {
                        throw new Exception("支付宝支付失败");

                    }
                }


				bool bolIsEmptyBills = bool.Parse(this.Request["isEmptyBillsExpense"]);
				Chain.Model.Mem modelMem = this.bllMem.GetModel(memID);
				int intOldLevelID = modelMem.MemLevelID;
				if (!PubFunction.IsShopPoint(intUserShopID, ref intPoint))
				{
					flag = "-6";
				}
				int intOrderLogID = 0;
				if (flag != "-6")
				{
					this.modelOrderLog.OrderAccount = strOrderCode;
					this.modelOrderLog.OrderMemID = memID;
					this.modelOrderLog.OrderType = 2;
					if (strOrderType == "EmptyBills")
					{
						this.modelOrderLog.OrderType = 3;
					}
					this.modelOrderLog.OrderTotalMoney = dclTotalMoney;
					this.modelOrderLog.OrderDiscountMoney = dclDiscountMoney;
					this.modelOrderLog.OrderIsCard = bolIsCard;
					this.modelOrderLog.OrderPayCard = dclCardPayMoney;
					this.modelOrderLog.OrderIsCash = bolIsCash;
					this.modelOrderLog.OrderPayCash = dclCashPayMoney;
					this.modelOrderLog.OrderIsBink = bolIsBink;
					this.modelOrderLog.OrderPayBink = dclBinkPayMoney;
					this.modelOrderLog.OrderPayCoupon = dclCouponPayMoney;
					this.modelOrderLog.OrderPoint = intPoint;
					this.modelOrderLog.OrderRemark = strRemark;
					this.modelOrderLog.OrderPayType = 0;
					this.modelOrderLog.OrderShopID = intUserShopID;
					this.modelOrderLog.OrderUserID = intUserID;
					this.modelOrderLog.OrderCreateTime = dtExTime;
					this.modelOrderLog.OldAccount = "";
					this.modelOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
					this.modelOrderLog.UsePoint = usePoint;
					this.modelOrderLog.UsePointAmount = usePointAmount;
					if (!bolIsEmptyBills)
					{
						if (this.bllOrderLog.ExistsOrderAccount(strOrderCode))
						{
							flag = "-4";
						}
						else
						{
							intOrderLogID = this.bllOrderLog.Add(this.modelOrderLog, strOrderCode);
						}
					}
					else
					{
						this.modelOrderLog.OrderID = orderID;
						intOrderLogID = this.bllOrderLog.Update(this.modelOrderLog);
					}
				}
				int intGoodsLogID = 0;
				int IntOldGoodsLogID = 0;
				if (intOrderLogID > 0)
				{
					this.modelGoodsLog.GoodsAccount = strOrderCode;
					if (strOrderType != "EmptyBills")
					{
						this.modelGoodsLog.Type = 2;
						this.modelGoodsLog.Remark = "商品销售出库";
					}
					else
					{
						this.modelGoodsLog.Type = 3;
						this.modelGoodsLog.Remark = "商品挂单出库";
					}
					this.modelGoodsLog.TotalPrice = dclDiscountMoney;
					this.modelGoodsLog.CreateTime = dtExTime;
					this.modelGoodsLog.ShopID = intUserShopID;
					this.modelGoodsLog.UserID = intUserID;
					this.modelGoodsLog.ChangeShopID = intUserShopID;
					if (!bolIsEmptyBills)
					{
						intGoodsLogID = this.bllGoodsLog.Add(this.modelGoodsLog);
					}
					else
					{
						DataTable dtGoods = this.bllGoodsLog.GetList("GoodsAccount='" + strOrderCode + "'").Tables[0];
						if (dtGoods.Rows.Count > 0)
						{
							IntOldGoodsLogID = int.Parse(dtGoods.Rows[0]["ID"].ToString());
							this.modelGoodsLog.ID = IntOldGoodsLogID;
							if (this.bllGoodsLog.Update(this.modelGoodsLog))
							{
								intGoodsLogID = IntOldGoodsLogID;
							}
						}
					}
				}
				if (intOrderLogID > 0 && bolIsEmptyBills)
				{
					DataTable dtOrderDetail = this.bllDetail.GetList("OrderID=" + orderID).Tables[0];
					int intUpdateNumber = 0;
					for (int i = 0; i < dtOrderDetail.Rows.Count; i++)
					{
						this.modelNumber.GoodsID = int.Parse(dtOrderDetail.Rows[i]["GoodsID"].ToString());
						this.modelNumber.ShopID = intUserShopID;
						this.modelNumber.Number = int.Parse(dtOrderDetail.Rows[i]["OrderDetailNumber"].ToString());
						intUpdateNumber = this.bllNumber.UpdataGoodsNumber(this.modelNumber);
					}
					if (intUpdateNumber > 0)
					{
						this.bllDetail.DeleteDetail(orderID);
					}
					this.bllGoodsDetail.DeleteDetail(IntOldGoodsLogID);
				}
				int intOrderDetailID = 0;
				if (intGoodsLogID > 0)
				{
					for (int j = 0; j < intCount; j++)
					{
						this.modelDetail.OrderID = intOrderLogID;
						this.modelDetail.GoodsID = int.Parse(this.Request["data[" + j + "][GoodsID]"]);
						this.modelDetail.OrderDetailPrice = decimal.Parse(this.Request["data[" + j + "][Price]"]);
						this.modelDetail.OrderDetailDiscountPrice = decimal.Parse(this.Request["data[" + j + "][ExpMoney]"]);
						this.modelDetail.OrderDetailNumber = decimal.Parse(this.Request["data[" + j + "][ExpNum]"]);
						this.modelDetail.OrderDetailPoint = int.Parse(this.Request["data[" + j + "][ExpPoint]"].ToString());
						if (bolIsEmptyBills)
						{
							this.modelDetail.OrderID = orderID;
						}
						if (decimal.Parse(this.Request["data[" + j + "][ExpNum]"]) > 0m)
						{
							this.modelDetail.OrderDetailType = 0;
						}
						else
						{
							this.modelDetail.OrderDetailType = 1;
						}
						intOrderDetailID = this.bllDetail.Add(this.modelDetail);
						int intGoodsType = int.Parse(this.Request["data[" + j + "][GoodsType]"]);
						if (intGoodsType == 0)
						{
							this.modelNumber.GoodsID = this.modelDetail.GoodsID;
							this.modelNumber.ShopID = intUserShopID;
							this.modelNumber.Number = decimal.Parse((this.modelDetail.OrderDetailNumber * -1m).ToString());
							this.bllNumber.UpdataGoodsNumber(this.modelNumber);
						}
						Chain.Model.GoodsLogDetail modelGoodsLogDetail = new Chain.Model.GoodsLogDetail();
						modelGoodsLogDetail.GoodsLogID = intGoodsLogID;
						modelGoodsLogDetail.GoodsID = int.Parse(this.Request["data[" + j + "][GoodsID]"]);
						modelGoodsLogDetail.GoodsInPrice = decimal.Parse(this.Request["data[" + j + "][Price]"]);
						modelGoodsLogDetail.GoodsOutPrice = decimal.Parse((decimal.Parse(this.Request["data[" + j + "][ExpMoney]"]) / decimal.Parse(this.Request["data[" + j + "][ExpNum]"])).ToString(string.Format("F{0}", PubFunction.ProductDecimalNum)));
						modelGoodsLogDetail.GoodsNumber = decimal.Parse(this.Request["data[" + j + "][ExpNum]"]) * -1m;
						if (bolIsEmptyBills)
						{
							modelGoodsLogDetail.GoodsLogID = IntOldGoodsLogID;
						}
						this.bllGoodsDetail.Add(modelGoodsLogDetail);
						if (strOrderType != "EmptyBills")
						{
							int intStaffID = (this.Request["data[" + j + "][ExpStaffName]"] != "") ? int.Parse(this.Request["data[" + j + "][ExpStaffName]"]) : 0;
							if (intStaffID != 0)
							{
								this.modelStaffMoney.StaffID = intStaffID;
								this.modelStaffMoney.StaffTotalMoney = Math.Abs(decimal.Parse(this.Request["data[" + j + "][ExpStaffMoney]"]));
								this.modelStaffMoney.StaffOrderCode = strOrderCode;
								this.modelStaffMoney.StaffMemID = memID;
								this.modelStaffMoney.StaffGoodsID = int.Parse(this.Request["data[" + j + "][GoodsID]"]);
								this.modelStaffMoney.StaffShopID = intUserShopID;
								this.modelStaffMoney.StaffCreateTime = DateTime.Now;
								this.modelStaffMoney.StaffOrderDetailID = intOrderDetailID;
								this.modelStaffMoney.StaffType = 0;
								this.bllStaffMoney.Add(this.modelStaffMoney);
							}
						}
					}
				}
				if (intOrderDetailID > 0)
				{
					if (strOrderType == "EmptyBills")
					{
						flag = "-3";
						if (memID == 0)
						{
							string Remark = "三科挂单,订单号：[" + strOrderCode + "],备注：" + strRemark;
							PubFunction.SaveSysLog(intUserID, 4, "散客消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
						else
						{
							string Remark = string.Concat(new string[]
							{
								"会员挂单,会员卡号：[",
								modelMem.MemCard,
								"],姓名：[",
								modelMem.MemName,
								"],订单号：[",
								strOrderCode,
								"],备注：",
								strRemark
							});
							PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
					else if (memID == 0)
					{
						flag = "{\"Success\":\"" + intOrderLogID + "\",\"strUpdateMemLevel\":\"\",\"point\":0}";
						string Remark = string.Concat(new object[]
						{
							"散客商品消费,订单号：[",
							strOrderCode,
							"],消费金额：[",
							dclDiscountMoney,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "散客消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
					else
					{
						decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
						modelMem.MemConsumeMoney += dclDiscountMoney;
						modelMem.MemPoint += intPoint - usePoint;
						modelMem.MemConsumeLastTime = dtExTime;
						modelMem.MemConsumeCount++;
						int mem = this.bllMem.ExpenseUpdateMem(memID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
						if (intPoint != 0)
						{
							Chain.Model.PointLog modelPointLog = new Chain.Model.PointLog();
							modelPointLog.PointMemID = memID;
							modelPointLog.PointNumber = intPoint;
							modelPointLog.PointChangeType = 1;
							modelPointLog.PointRemark = "会员商品消费成功，消费总额：[" + dclDiscountMoney + "]";
							modelPointLog.PointShopID = intUserShopID;
							modelPointLog.PointUserID = intUserID;
							modelPointLog.PointCreateTime = dtExTime;
							modelPointLog.PointOrderCode = strOrderCode;
							this.bllPoint.Add(modelPointLog);
						}
						Chain.Model.MoneyChangeLog modelMoneyChangeLog = new Chain.Model.MoneyChangeLog();
						modelMoneyChangeLog.MoneyChangeMemID = modelMem.MemID;
						modelMoneyChangeLog.MoneyChangeUserID = intUserID;
						modelMoneyChangeLog.MoneyChangeType = 12;
						modelMoneyChangeLog.MoneyChangeAccount = strOrderCode;
						modelMoneyChangeLog.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
						modelMoneyChangeLog.MoneyChangeBalance = -dclCardPayMoney;
						modelMoneyChangeLog.MoneyChangeCash = -dclCashPayMoney;
						modelMoneyChangeLog.MoneyChangeUnionPay = -dclBinkPayMoney;
						modelMoneyChangeLog.MemMoney = modelMem.MemMoney - dclCardPayMoney;
						modelMoneyChangeLog.MoneyChangeCreateTime = DateTime.Now;
						modelMoneyChangeLog.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(modelMoneyChangeLog);
						MEMPointUpdate.MEMPointRate(modelMem, this.modelOrderLog.OrderPoint, this.modelOrderLog.OrderAccount, 1, intUserID, this.UserModel.UserShopID);
						modelMem = new Chain.BLL.Mem().GetModel(memID);
						string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
						modelMem = new Chain.BLL.Mem().GetModel(memID);
						PubFunction.SetShopPoint(intUserID, intUserShopID, intPoint, string.Concat(new object[]
						{
							"单号：[",
							strOrderCode,
							"],会员商品消费金额：[",
							dclDiscountMoney,
							"],扣除商家积分：[",
							intPoint,
							"]"
						}), 2);
						if (usePoint != 0)
						{
							PubFunction.SetShopPoint(intUserID, intUserShopID, -usePoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],会员商品消费抵用积分：[",
								usePoint,
								"]，商家回收积分：[",
								usePoint,
								"]"
							}), 4);
							Chain.Model.PointLog modelPointLog = new Chain.Model.PointLog();
							modelPointLog.PointMemID = memID;
							modelPointLog.PointNumber = usePoint;
							modelPointLog.PointChangeType = 1;
							modelPointLog.PointRemark = string.Concat(new object[]
							{
								"会员商品消费成功,抵用积分：[",
								usePoint,
								"],抵用金额：[",
								usePointAmount,
								"]"
							});
							modelPointLog.PointShopID = intUserShopID;
							modelPointLog.PointUserID = intUserID;
							modelPointLog.PointCreateTime = dtExTime;
							modelPointLog.PointOrderCode = strOrderCode;
							this.bllPoint.Add(modelPointLog);
						}
						Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
						Chain.Model.SysShop modelShop = bllSysShop.GetModel(intUserShopID);
						decimal flTotalRate = decimal.Parse(Convert.ToDouble(modelShop.TotalRate).ToString("#0.00"));
						if (flTotalRate > 0m)
						{
							int flTotalPoint = (int)(flTotalRate * intPoint);
							decimal alliancePercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.AllianceRebatePercent).ToString("#0.00"));
							decimal cardShopPercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.CardShopRebatePercent).ToString("#0.00"));
							int alliancePoint = (int)(flTotalPoint * alliancePercent);
							int cardShopPoint = (int)(flTotalPoint * cardShopPercent);
							int zbPoint = flTotalPoint - alliancePoint - cardShopPoint;
							Chain.BLL.ReturnPointLog bllReturnPoint = new Chain.BLL.ReturnPointLog();
							bllReturnPoint.Add(new Chain.Model.ReturnPointLog
							{
								OrderAccount = strOrderCode,
								MemID = memID,
								TotalPoint = flTotalPoint,
								AlliancePoint = alliancePoint,
								ZbPoint = zbPoint,
								CardShopPoint = cardShopPoint,
								Remark = "会员商品消费,商家返利积分",
								ReturnShopID = intUserShopID,
								CreateTime = DateTime.Now
							});
							PubFunction.SetShopPoint(intUserID, intUserShopID, intUserShopID, flTotalPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],会员消费得积分：[",
								intPoint,
								"],返利总比例：[",
								flTotalRate,
								"],商家扣除返利总积分：[",
								flTotalPoint,
								"]"
							}), 2);
							PubFunction.SetShopPoint(intUserID, intUserShopID, modelShop.FatherShopID, -alliancePoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],商家总返利积分：[",
								flTotalPoint,
								"],联盟商返利比例：[",
								alliancePercent,
								"],联盟商得到返利积分：[",
								alliancePoint,
								"]"
							}), 3);
							PubFunction.SetShopPoint(intUserID, intUserShopID, modelMem.MemShopID, -cardShopPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],商家总返利积分：[",
								flTotalPoint,
								"],开卡商家返利比例：[",
								cardShopPercent,
								"],开卡商家得到返利积分：[",
								cardShopPoint,
								"]"
							}), 3);
							PubFunction.SetShopPoint(intUserID, intUserShopID, 1, -zbPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],商家总返利积分：[",
								flTotalPoint,
								"],运营商得到返利积分：[",
								zbPoint,
								"]"
							}), 3);
						}
						if (sendSMS)
						{
							if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
							{
								flag = "-2";
							}
							else if (modelMem.MemMobile != "")
							{
								if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
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
									smsTemplateParameter.OldLevelID = intOldLevelID;
									modelMem = new Chain.BLL.Mem().GetModel(memID);
									smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
									smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
									smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
									string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, intUserShopID);
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
										PubFunction.SetShopSms(intUserID, this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
										{
											','
										}).Length, 4);
									}
								}
								else
								{
									flag = "-5";
								}
							}
						}
						flag = string.Concat(new object[]
						{
							"{\"Success\":\"",
							intOrderLogID,
							"\",\"strUpdateMemLevel\":\"",
							strUpdateMemLevel,
							"\",\"point\":",
							intPoint.ToString(),
							"}"
						});
						string Remark = string.Concat(new object[]
						{
							"会员商品消费,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],订单号：[",
							strOrderCode,
							"],消费金额：[",
							dclDiscountMoney,
							"],获得积分：[",
							intPoint,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-1";
			}
			this.Context.Response.Write(flag);
		}

		public void CountExpense()
		{
			int flag = 0;
			string result = "";
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int memID = int.Parse(this.Request["memID"].ToString());
				int arg_77_0 = (!string.IsNullOrEmpty(this.Request["orderID"].ToString())) ? int.Parse(this.Request["orderID"].ToString()) : 0;
				string strOrderType = this.Request["parameter[0][payType]"];
				decimal dclDiscountMoney = decimal.Parse(this.Request["parameter[0][DiscountMoney]"]);
				decimal dclCardPayMoney = (this.Request["parameter[0][CardMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CardMoney]"]) : 0m;
				decimal dclCashPayMoney = (this.Request["parameter[0][CashMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CashMoney]"]) : 0m;
				decimal dclBinkPayMoney = (this.Request["parameter[0][BinkMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][BinkMoney]"]) : 0m;
				decimal dclCouponPayMoney = (this.Request["parameter[0][CouponMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CouponMoney]"]) : 0m;
				decimal dclTotalMoney = decimal.Parse(this.Request["totalMoney"]);
				decimal dclTotalStaffMoney = decimal.Parse(this.Request["staffMoney"]);
				int intPoint = int.Parse(this.Request["point"]);
				int intNumber = int.Parse(this.Request["number"]);
				string strOrderCode = this.Request["orderCode"].ToString();
				string strRemark = this.Request["remark"].ToString();
				strRemark = PubFunction.RemoveSpace(strRemark);
				int intCount = int.Parse(this.Request["count"]);
				bool print = this.Request["print"] == "true";
				bool sendSMS = this.Request["sendSMS"] == "true";
				bool staff = this.Request["staff"] == "true";
				DateTime dtExTime = DateTime.Parse(this.Request["expensetime"].ToString());
				string strStaffName = this.Request["staffName"];
				bool bolIsCard = bool.Parse(this.Request["parameter[0][IsCard]"]);
				bool bolIsCash = bool.Parse(this.Request["parameter[0][IsCash]"]);
				bool bolIsBink = bool.Parse(this.Request["parameter[0][IsBink]"]);
				int arg_32C_0 = bool.Parse(this.Request["isEmptyBillsExpense"]) ? 1 : 0;
				bool isMemCountExpense = this.Request["isMemCountExpense"].ToString() == "1";
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				int intLevelID = modelMem.MemLevelID;
				if (memID == 0 || modelMem == null)
				{
					flag = -6;
				}
				int intOrderLogAdd = 0;
				this.modelOrderLog.OrderAccount = strOrderCode;
				this.modelOrderLog.OrderMemID = memID;
				this.modelOrderLog.OrderType = 2;
				this.modelOrderLog.OrderTotalMoney = dclTotalMoney;
				this.modelOrderLog.OrderDiscountMoney = dclDiscountMoney;
				this.modelOrderLog.OrderIsCard = bolIsCard;
				this.modelOrderLog.OrderPayCard = dclCardPayMoney;
				this.modelOrderLog.OrderIsCash = bolIsCash;
				this.modelOrderLog.OrderPayCash = dclCashPayMoney;
				this.modelOrderLog.OrderIsBink = bolIsBink;
				this.modelOrderLog.OrderPayBink = dclBinkPayMoney;
				this.modelOrderLog.OrderPayCoupon = dclCouponPayMoney;
				this.modelOrderLog.OrderPoint = intPoint;
				this.modelOrderLog.OrderRemark = strRemark;
				this.modelOrderLog.OrderPayType = 0;
				this.modelOrderLog.OrderShopID = intUserShopID;
				this.modelOrderLog.OrderUserID = intUserID;
				this.modelOrderLog.OrderCreateTime = dtExTime;
				this.modelOrderLog.OldAccount = "";
				this.modelOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
				this.modelOrderLog.OrderType = 7;
				List<string> _listarray = new List<string>();
				Chain.BLL.Goods goodsbll = new Chain.BLL.Goods();
				Chain.Model.Goods goodsmodel = new Chain.Model.Goods();
				if (this.bllOrderLog.ExistsOrderAccount(strOrderCode))
				{
					flag = -4;
				}
				else
				{
					intOrderLogAdd = this.bllOrderLog.Add(this.modelOrderLog, strOrderCode);
				}
				if (flag >= 0 && intOrderLogAdd > 0)
				{
					int intDetailAdd = 0;
					for (int i = 0; i < intCount; i++)
					{
						this.modelDetail.OrderID = intOrderLogAdd;
						this.modelDetail.GoodsID = int.Parse(this.Request["data[" + i + "][GoodsID]"]);
						goodsmodel = goodsbll.GetModel(this.modelDetail.GoodsID);
						if (goodsmodel != null)
						{
							_listarray.Add(goodsmodel.Name);
						}
						this.modelDetail.OrderDetailPrice = decimal.Parse(this.Request["data[" + i + "][Price]"]);
						this.modelDetail.OrderDetailDiscountPrice = decimal.Parse(this.Request["data[" + i + "][ExpMoney]"]);
						this.modelDetail.OrderDetailNumber = decimal.Parse(this.Request["data[" + i + "][ExpNum]"]);
						this.modelDetail.OrderDetailPoint = int.Parse(this.Request["data[" + i + "][ExpPoint]"].ToString());
						this.modelDetail.OrderDetailType = 1;
						intDetailAdd = this.bllDetail.Add(this.modelDetail);
						int intGoodsType = int.Parse(this.Request["data[" + i + "][GoodsType]"]);
						if (this.modelDetail.OrderDetailType == 1)
						{
							DataTable dtCount = this.bllCountDetail.GetList(-1, string.Concat(new object[]
							{
								" CountDetailMemID=",
								memID,
								" and CountDetailGoodsID=",
								this.modelDetail.GoodsID,
								" and CountDetailNumber>0"
							}), "CountCreateTime ASC").Tables[0];
							int orderCount = Math.Abs(int.Parse(this.modelDetail.OrderDetailNumber.ToString()));
							foreach (DataRow drCount in dtCount.Rows)
							{
								if (orderCount != 0)
								{
									int detailNumber = int.Parse(drCount["CountDetailNumber"].ToString());
									if (detailNumber > orderCount)
									{
										this.bllCountDetail.UpdateCountDetailNumber(orderCount, int.Parse(drCount["CountDetailID"].ToString()));
										orderCount = 0;
									}
									else
									{
										this.bllCountDetail.UpdateCountDetailNumber(detailNumber, int.Parse(drCount["CountDetailID"].ToString()));
										orderCount -= detailNumber;
									}
								}
							}
						}
						if (strOrderType != "EmptyBills")
						{
							int intStaffID = (this.Request["data[" + i + "][ExpStaffName]"].ToString() != "") ? int.Parse(this.Request["data[" + i + "][ExpStaffName]"]) : 0;
							if (intStaffID != 0)
							{
								this.modelStaffMoney.StaffID = intStaffID;
								this.modelStaffMoney.StaffTotalMoney = Math.Abs(decimal.Parse(this.Request["data[" + i + "][ExpStaffMoney]"]));
								this.modelStaffMoney.StaffOrderCode = strOrderCode;
								this.modelStaffMoney.StaffMemID = memID;
								this.modelStaffMoney.StaffGoodsID = int.Parse(this.Request["data[" + i + "][GoodsID]"]);
								this.modelStaffMoney.StaffShopID = intUserShopID;
								this.modelStaffMoney.StaffCreateTime = DateTime.Now;
								this.modelStaffMoney.StaffOrderDetailID = intDetailAdd;
								this.modelStaffMoney.StaffType = 4;
								this.bllStaffMoney.Add(this.modelStaffMoney);
							}
						}
					}
					if (intDetailAdd > 0 && memID != 0)
					{
						result = "{\"Success\":\"" + intOrderLogAdd + "\",\"strUpdateMemLevel\":\"\"}";
						string Remark = string.Concat(new string[]
						{
							"会员计次消费,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],订单号：[",
							strOrderCode,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "计次消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						if (sendSMS)
						{
							if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
							{
								flag = -2;
							}
							else if (modelMem.MemMobile != "")
							{
								if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
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
									foreach (string str in _listarray)
									{
										SmsTemplateParameter expr_B79 = smsTemplateParameter;
										expr_B79.CountItemsString = expr_B79.CountItemsString + str + "、";
									}
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
										PubFunction.SetShopSms(intUserID, this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
										{
											','
										}).Length, 4);
									}
								}
								else
								{
									flag = -5;
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write((flag < 0) ? flag.ToString() : result);
		}

		public void GetServiceList()
		{
			string msgResponse = "";
			try
			{
				int intSize = int.Parse(this.Request["size"].ToString());
				int intIndex = int.Parse(this.Request["index"].ToString());
				string strKey = (this.Request["key"] != "") ? this.Request["key"].ToString() : "";
				int intShopID = (this.Request["shopID"] != "") ? int.Parse(this.Request["shopID"].ToString()) : -1;
				int memLevelID = (this.Request["MemLevelID"] == null) ? -1 : int.Parse(this.Request["MemLevelID"]);
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append(" GoodsType=1 ");
				if (strKey != "")
				{
					sbWhere.AppendFormat(" and (Name like '%{0}%' or NameCode like '%{0}%' or GoodsCode='{0}')", strKey);
				}
				int resCount;
				DataTable dtGoods = this.bllGoods.GetGoodsStockList(intShopID, memLevelID, "", intSize, intIndex, out resCount, new string[]
				{
					sbWhere.ToString()
				}).Tables[0];
				if (dtGoods != null)
				{
					if (memLevelID == -1)
					{
						string strJson = JsonPlus.ToJson(dtGoods, "GoodsID,Name,Price,GoodsBidPrice,Number,GoodsCode,Point,MinPercent,SalePercet,GoodsType,NameCode,ShopName,GoodsDiscount,DiscountPrice,PointDiscount");
						msgResponse = string.Concat(new object[]
						{
							"{\"RecordCount\":",
							resCount,
							",\"List\":",
							strJson,
							"}"
						});
					}
					else
					{
						string strJson = JsonPlus.ToJson(dtGoods, "GoodsID,Name,Price,GoodsBidPrice,Number,GoodsCode,Point,MinPercent,SalePercet,GoodsType,NameCode,ShopName,GoodsDiscount,DiscountPrice,PointDiscount");
						msgResponse = string.Concat(new object[]
						{
							"{\"RecordCount\":",
							resCount,
							",\"List\":",
							strJson,
							"}"
						});
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void EmptyBillsSubmit()
		{
			string flag = "0";
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				decimal dclCardPayMoney = (this.Request["parameter[0][CardMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CardMoney]"]) : 0m;
				decimal dclCashPayMoney = (this.Request["parameter[0][CashMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CashMoney]"]) : 0m;
				decimal dclBinkPayMoney = (this.Request["parameter[0][BinkMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][BinkMoney]"]) : 0m;
				decimal dclCouponPayMoney = (this.Request["parameter[0][CouponMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CouponMoney]"]) : 0m;
				int intOrderId = int.Parse(this.Request["orderId"].ToString());
				decimal dclDiscountMoney = decimal.Parse(this.Request["totalDiscount"].ToString());
				string strOrderCode = this.Request["orderCode"].ToString();
				bool bolIsCard = bool.Parse(this.Request["parameter[0][IsCard]"]);
				bool bolIsCash = bool.Parse(this.Request["parameter[0][IsCash]"]);
				bool bolIsBink = bool.Parse(this.Request["parameter[0][IsBink]"]);
				int usePoint = int.Parse(this.Request["parameter[0][usePoint]"]);
				decimal usePointAmount = decimal.Parse(this.Request["parameter[0][usePointAmount]"]);
				this.modelOrderLog = this.bllOrderLog.GetModel(intOrderId);
				this.modelOrderLog.OrderPayType = 0;
				this.modelOrderLog.OrderDiscountMoney = dclDiscountMoney;
				this.modelOrderLog.OrderIsCard = bolIsCard;
				this.modelOrderLog.OrderPayCard = dclCardPayMoney;
				this.modelOrderLog.OrderIsCash = bolIsCash;
				this.modelOrderLog.OrderPayCash = dclCashPayMoney;
				this.modelOrderLog.OrderIsBink = bolIsBink;
				this.modelOrderLog.OrderPayBink = dclBinkPayMoney;
				this.modelOrderLog.OrderPayCoupon = dclCouponPayMoney;
				this.modelOrderLog.OrderType = 2;
				this.modelOrderLog.OrderCreateTime = DateTime.Now;
				this.modelOrderLog.UsePointAmount = usePointAmount;
				this.modelOrderLog.UsePoint = usePoint;
				int intPoint = this.modelOrderLog.OrderPoint;
				int point = this.modelOrderLog.OrderPoint;
				if (PubFunction.IsShopPoint(intUserShopID, ref point))
				{
					this.modelOrderLog.OrderPoint = point;
					int intOrderLogUpdate = this.bllOrderLog.UpdateOrderLog(this.modelOrderLog);
					if (intOrderLogUpdate > 0)
					{
						Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
						Chain.Model.Mem modelMem = bllMem.GetModel(this.modelOrderLog.OrderMemID);
						Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
						moneyChangeLogModel.MoneyChangeMemID = modelMem.MemID;
						moneyChangeLogModel.MoneyChangeUserID = intUserID;
						moneyChangeLogModel.MoneyChangeType = 13;
						moneyChangeLogModel.MoneyChangeAccount = this.modelOrderLog.OrderAccount;
						moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
						moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
						moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
						moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
						moneyChangeLogModel.MemMoney = modelMem.MemMoney - dclCardPayMoney;
						moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
						moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
						this.modelPoint.PointMemID = this.modelOrderLog.OrderMemID;
						this.modelPoint.PointNumber = this.modelOrderLog.OrderPoint;
						this.modelPoint.PointChangeType = 1;
						this.modelPoint.PointRemark = "会员商品消费成功，消费总额：" + dclDiscountMoney;
						this.modelPoint.PointShopID = intUserShopID;
						this.modelPoint.PointUserID = intUserID;
						this.modelPoint.PointCreateTime = DateTime.Now;
						this.modelPoint.PointOrderCode = strOrderCode;
						if (this.modelPoint.PointMemID > 0)
						{
							if (this.bllPoint.Add(this.modelPoint) > 0)
							{
								decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
								modelMem.MemConsumeMoney += dclDiscountMoney;
								modelMem.MemPoint += this.modelOrderLog.OrderPoint - usePoint;
								modelMem.MemConsumeLastTime = DateTime.Now;
								modelMem.MemConsumeCount++;
								int mem = bllMem.ExpenseUpdateMem(this.modelOrderLog.OrderMemID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
								MEMPointUpdate.MEMPointRate(modelMem, this.modelOrderLog.OrderPoint, this.modelOrderLog.OrderAccount, 1, intUserID, intUserShopID);
								modelMem = new Chain.BLL.Mem().GetModel(this.modelOrderLog.OrderMemID);
								string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
								PubFunction.SetShopPoint(intUserID, intUserShopID, intPoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderCode,
									"],会员商品消费金额：[",
									dclDiscountMoney,
									"],扣除商家积分：[",
									intPoint,
									"]"
								}), 2);
								if (usePoint != 0)
								{
									PubFunction.SetShopPoint(intUserID, intUserShopID, -usePoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderCode,
										"],会员商品消费抵用积分：[",
										usePoint,
										"]，商家回收积分：[",
										usePoint,
										"]"
									}), 4);
									Chain.Model.PointLog modelPointLog = new Chain.Model.PointLog();
									modelPointLog.PointMemID = this.modelOrderLog.OrderMemID;
									modelPointLog.PointNumber = usePoint;
									modelPointLog.PointChangeType = 1;
									modelPointLog.PointRemark = string.Concat(new object[]
									{
										"会员商品消费成功,抵用积分：[",
										usePoint,
										"],抵用金额：[",
										usePointAmount,
										"]"
									});
									modelPointLog.PointShopID = intUserShopID;
									modelPointLog.PointUserID = intUserID;
									modelPointLog.PointCreateTime = DateTime.Now;
									modelPointLog.PointOrderCode = strOrderCode;
									this.bllPoint.Add(modelPointLog);
								}
								Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
								Chain.Model.SysShop modelShop = bllSysShop.GetModel(intUserShopID);
								decimal flTotalRate = decimal.Parse(Convert.ToDouble(modelShop.TotalRate).ToString("#0.00"));
								if (flTotalRate > 0m)
								{
									int flTotalPoint = (int)(flTotalRate * intPoint);
									decimal alliancePercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.AllianceRebatePercent).ToString("#0.00"));
									decimal cardShopPercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.CardShopRebatePercent).ToString("#0.00"));
									int alliancePoint = (int)(flTotalPoint * alliancePercent);
									int cardShopPoint = (int)(flTotalPoint * cardShopPercent);
									int zbPoint = flTotalPoint - alliancePoint - cardShopPoint;
									Chain.BLL.ReturnPointLog bllReturnPoint = new Chain.BLL.ReturnPointLog();
									bllReturnPoint.Add(new Chain.Model.ReturnPointLog
									{
										OrderAccount = strOrderCode,
										MemID = this.modelOrderLog.OrderMemID,
										TotalPoint = flTotalPoint,
										AlliancePoint = alliancePoint,
										ZbPoint = zbPoint,
										CardShopPoint = cardShopPoint,
										Remark = "会员商品消费,商家返利积分",
										ReturnShopID = intUserShopID,
										CreateTime = DateTime.Now
									});
									PubFunction.SetShopPoint(intUserID, intUserShopID, intUserShopID, flTotalPoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderCode,
										"],会员消费得积分：[",
										intPoint,
										"],返利总比例：[",
										flTotalRate,
										"],商家扣除返利总积分：[",
										flTotalPoint,
										"]"
									}), 2);
									PubFunction.SetShopPoint(intUserID, intUserShopID, modelShop.FatherShopID, -alliancePoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderCode,
										"],商家总返利积分：[",
										flTotalPoint,
										"],联盟商返利比例：[",
										alliancePercent,
										"],联盟商得到返利积分：[",
										alliancePoint,
										"]"
									}), 3);
									PubFunction.SetShopPoint(intUserID, intUserShopID, modelMem.MemShopID, -cardShopPoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderCode,
										"],商家总返利积分：[",
										flTotalPoint,
										"],开卡商家返利比例：[",
										cardShopPercent,
										"],开卡商家得到返利积分：[",
										cardShopPoint,
										"]"
									}), 3);
									PubFunction.SetShopPoint(intUserID, intUserShopID, 1, -zbPoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderCode,
										"],商家总返利积分：[",
										flTotalPoint,
										"],运营商得到返利积分：[",
										zbPoint,
										"]"
									}), 3);
								}
								flag = string.Concat(new object[]
								{
									"{\"Success\":\"",
									intOrderLogUpdate,
									"\",\"strUpdateMemLevel\":\"",
									strUpdateMemLevel,
									"\"}"
								});
								PubFunction.SaveSysLog(intUserID, 4, "会员消费", string.Concat(new string[]
								{
									"会员挂单结算,会员卡号：[",
									modelMem.MemCard,
									"],姓名:[",
									modelMem.MemName,
									"],订单号:[",
									strOrderCode,
									"]"
								}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
							}
						}
						else
						{
							flag = intOrderLogUpdate.ToString();
						}
					}
				}
				else
				{
					flag = "-6";
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-1";
			}
			this.Context.Response.Write(flag);
		}

		public void DeleteEmptyBills()
		{
			string flag = "0";
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intMemID = int.Parse(this.Request["memID"].ToString());
				int intOrderID = int.Parse(this.Request["orderID"].ToString());
				this.modelOrderLog = this.bllOrderLog.GetModel(intOrderID);
				Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(intMemID);
				DataTable dtOrderDetail = this.bllDetail.GetList(" OrderID=" + intOrderID).Tables[0];
				this.modelGoodsLog.GoodsAccount = this.modelOrderLog.OrderAccount;
				this.modelGoodsLog.Type = 4;
				this.modelGoodsLog.TotalPrice = this.modelOrderLog.OrderDiscountMoney;
				this.modelGoodsLog.Remark = "商品挂单撤销入库";
				this.modelGoodsLog.CreateTime = DateTime.Now;
				this.modelGoodsLog.ShopID = intUserShopID;
				this.modelGoodsLog.UserID = intUserID;
				this.modelGoodsLog.ChangeShopID = this.modelOrderLog.OrderShopID;
				int intLog = this.bllGoodsLog.Add(this.modelGoodsLog);
				for (int i = 0; i < dtOrderDetail.Rows.Count; i++)
				{
					this.modelGoods = this.bllGoods.GetModel(int.Parse(dtOrderDetail.Rows[i]["GoodsID"].ToString()));
					if (float.Parse(dtOrderDetail.Rows[i]["OrderDetailNumber"].ToString()) < 0f)
					{
						int intOrderDetailNumber = Math.Abs(int.Parse(dtOrderDetail.Rows[i]["OrderDetailNumber"].ToString()));
						DataTable dtCountDetail = this.bllCountDetail.GetList(-1, string.Concat(new object[]
						{
							" CountDetailGoodsID=",
							this.modelGoods.GoodsID,
							" and CountDetailMemID=",
							intMemID
						}), "CountCreateTime").Tables[0];
						for (int j = 0; j < dtCountDetail.Rows.Count; j++)
						{
							if (int.Parse(dtCountDetail.Rows[j]["CountDetailNumber"].ToString()) + intOrderDetailNumber < int.Parse(dtCountDetail.Rows[j]["CountDetailTotalNumber"].ToString()))
							{
								intOrderDetailNumber *= -1;
								this.bllCountDetail.UpdateCountDetailNumber(intOrderDetailNumber, int.Parse(dtCountDetail.Rows[j]["CountDetailID"].ToString()));
							}
							else
							{
								int inNumber = int.Parse(dtCountDetail.Rows[j]["CountDetailTotalNumber"].ToString()) - int.Parse(dtCountDetail.Rows[j]["CountDetailNumber"].ToString());
								inNumber *= -1;
								this.bllCountDetail.UpdateCountDetailNumber(inNumber, int.Parse(dtCountDetail.Rows[j]["CountDetailID"].ToString()));
								intOrderDetailNumber -= int.Parse(dtCountDetail.Rows[j]["CountDetailNumber"].ToString());
							}
						}
					}
					else if (this.modelGoods.GoodsType == 0)
					{
						this.modelNumber.GoodsID = int.Parse(dtOrderDetail.Rows[i]["GoodsID"].ToString());
						this.modelNumber.ShopID = this.modelOrderLog.OrderShopID;
						this.modelNumber.Number = int.Parse(dtOrderDetail.Rows[i]["OrderDetailNumber"].ToString());
						this.bllNumber.UpdataGoodsNumber(this.modelNumber);
						this.modelGoodsDetail.GoodsLogID = intLog;
						this.modelGoodsDetail.GoodsID = this.modelNumber.GoodsID;
						this.modelGoodsDetail.GoodsInPrice = 0m;
						this.modelGoodsDetail.GoodsOutPrice = 0m;
						this.modelGoodsDetail.GoodsNumber = this.modelNumber.Number;
						this.bllGoodsDetail.Add(this.modelGoodsDetail);
					}
				}
				DataTable dtStaffMoney = this.bllStaffMoney.GetList(" StaffOrderCode='" + this.modelOrderLog.OrderAccount + "'").Tables[0];
				if (dtStaffMoney.Rows.Count > 0)
				{
					this.bllStaffMoney.DeleteStaff(this.modelOrderLog.OrderAccount);
				}
				int intOrderDetailDelete = this.bllDetail.DeleteDetail(intOrderID);
				if (intOrderDetailDelete > 0)
				{
					if (this.bllOrderLog.Delete(intOrderID))
					{
						if (intMemID > 0)
						{
							modelMem = new Chain.BLL.Mem().GetModel(intMemID);
							string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
							PubFunction.SaveSysLog(intUserID, 4, "会员消费", string.Concat(new string[]
							{
								"会员挂单撤销,会员卡号：[",
								modelMem.MemCard,
								"],姓名:[",
								modelMem.MemName,
								"],订单号:[",
								this.modelOrderLog.OrderAccount,
								"]"
							}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
							flag = string.Concat(new object[]
							{
								"{\"Success\":\"",
								intOrderDetailDelete,
								"\",\"strUpdateMemLevel\":\"",
								strUpdateMemLevel,
								"\"}"
							});
						}
						else
						{
							flag = intOrderDetailDelete.ToString();
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-1";
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsIn()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				string strGoodsAccount = this.Request["goodsAccount"].ToString();
				int dataCount = int.Parse(this.Request["count"].ToString());
				int intShopID = int.Parse(this.Request["shopID"].ToString());
				string strRemark = (this.Request["remark"] != "") ? this.Request["remark"].ToString() : "";
				DateTime dtCreateTime = DateTime.Parse(this.Request["time"].ToString());
				decimal dclMoney = (this.Request["totalMoney"] != "") ? decimal.Parse(this.Request["totalMoney"].ToString()) : 0m;
				this.modelGoodsLog.GoodsAccount = strGoodsAccount;
				this.modelGoodsLog.Type = 1;
				this.modelGoodsLog.TotalPrice = dclMoney;
				this.modelGoodsLog.Remark = strRemark;
				this.modelGoodsLog.CreateTime = dtCreateTime;
				this.modelGoodsLog.ShopID = intUserShopID;
				this.modelGoodsLog.UserID = intUserID;
				this.modelGoodsLog.ChangeShopID = intShopID;
				int intLog = this.bllGoodsLog.Add(this.modelGoodsLog);
				List<int> ProductsList = new List<int>();
				for (int i = 0; i < dataCount; i++)
				{
					this.modelNumber.GoodsID = int.Parse(this.Request["data[" + i + "][GoodsID]"]);
					this.modelNumber.ShopID = intShopID;
					decimal _number = 0m;
					if (decimal.TryParse(this.Request["data[" + i + "][Number]"], out _number))
					{
						if (!ProductsList.Contains(this.modelNumber.GoodsID) && _number > 0m)
						{
							ProductsList.Add(this.modelNumber.GoodsID);
							this.modelNumber.Number = _number;
							this.bllNumber.UpdataGoodsNumber(this.modelNumber);
							this.modelGoodsDetail.GoodsLogID = intLog;
							this.modelGoodsDetail.GoodsID = this.modelNumber.GoodsID;
							this.modelGoodsDetail.GoodsInPrice = decimal.Parse(this.Request["data[" + i + "][InMoney]"]);
							this.modelGoodsDetail.GoodsOutPrice = 0m;
							this.modelGoodsDetail.GoodsNumber = this.modelNumber.Number;
							flag = this.bllGoodsDetail.Add(this.modelGoodsDetail);
						}
					}
					if (flag > 0)
					{
						PubFunction.SaveSysLog(intUserID, 4, "商品入库", string.Concat(new object[]
						{
							"商品批量入库,入库单号:[",
							strGoodsAccount,
							"],入库时间:[",
							dtCreateTime,
							"]"
						}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsRevoke()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int ID = Convert.ToInt32(this.Request["ID"]);
				int InShopID = Convert.ToInt32(this.Request["InShopID"]);
				Chain.BLL.GoodsLogDetail bllGoodsLogDetail = new Chain.BLL.GoodsLogDetail();
				DataTable dtInsufficientCont = bllGoodsLogDetail.GetInsufficientCount(string.Format("GoodsLog.ID = {0}", ID)).Tables[0];
				if (Convert.ToInt32(dtInsufficientCont.Rows[0]["InsufficientCount"]) == 0)
				{
					DataTable dtLogDetail = bllGoodsLogDetail.GetList(string.Format("GoodsLogID = {0}", ID)).Tables[0];
					string strSqlUpdateNumber = "UPDATE GoodsNumber SET Number = Number - {0} WHERE GoodsID = {1} AND ShopID = {2}";
					string strOrderLog = "INSERT INTO GoodsLog(GoodsAccount,[Type],GoodsID,TotalPrice,InPrice,Outprice,GoodsNumber,InShopID,OutShopID,Remark,CreateTime,ShopID,UserID,ChangeShopID)" + string.Format(" (SELECT GoodsAccount,10,0,-1*TotalPrice,-1*InPrice,-1*Outprice,GoodsNumber,InShopID,OutShopID,Remark,GETDATE(),{0},{1} FROM GoodsLog WHERE ID = {2})", intUserShopID, intUserID, ID);
					string strOrderDetail = string.Format("INSERT INTO GoodsLogDetail(GoodsLogID,GoodsID,GoodsInPrice,GoodsOutPrice,GoodsNumber) (SELECT @@identity,GoodsID,GoodsInPrice,GoodsOutPrice,GoodsNumber FROM GoodsLogDetail WHERE GoodsLogID = {0})", ID);
					ArrayList alSql = new ArrayList();
					alSql.Add(strOrderLog);
					alSql.Add(strOrderDetail);
					foreach (DataRow dr in dtLogDetail.Rows)
					{
						alSql.Add(string.Format(strSqlUpdateNumber, dr["GoodsNumber"], dr["GoodsID"], InShopID));
					}
					alSql.Add(string.Format("UPDATE GoodsLog SET [Type] = 11  WHERE ID = {0}", ID));
					if (bllGoodsLogDetail.ExeclDataInput(alSql))
					{
						flag = 1;
					}
				}
				else
				{
					flag = -1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void EixtGetAllotDetail()
		{
			string msgResponse = "";
			try
			{
				int intAllotID = (!string.IsNullOrEmpty(this.Request["AllotID"].ToString())) ? int.Parse(this.Request["AllotID"].ToString()) : 0;
				Chain.Model.GoodsAllot ModelAllot = this.bllGoodsAllot.GetModel(intAllotID);
				if (ModelAllot.Allotstate != 2)
				{
					Chain.BLL.GoodsAllotDetail detail = new Chain.BLL.GoodsAllotDetail();
					DataTable dt = detail.AllotDetailByAllotID(intAllotID).Tables[0];
					if (dt != null && dt.Rows.Count > 0)
					{
						msgResponse = JsonPlus.ToJson(dt, "");
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GoodsAllot()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				string strGoodsAccount = this.Request["goodsAccount"].ToString();
				int intDataCount = (this.Request["count"].ToString() != "") ? int.Parse(this.Request["count"].ToString()) : 0;
				decimal intTotalNumber = 0m;
				decimal.TryParse(this.Request["totalNumber"], out intTotalNumber);
				int intOutShopID = (this.Request["outShopID"].ToString() != "") ? int.Parse(this.Request["outShopID"].ToString()) : 0;
				int intInShopID = (this.Request["inShopID"].ToString() != "") ? int.Parse(this.Request["inShopID"].ToString()) : 0;
				string strRemark = (this.Request["remark"] != "") ? this.Request["remark"].ToString() : "";
				DateTime dtCreateTime = DateTime.Parse(this.Request["time"].ToString());
				int HidAllotID = (this.Request["HidAllotID"].ToString() != "") ? int.Parse(this.Request["HidAllotID"].ToString()) : 0;
				bool isEmptyBillsExpense = bool.Parse(this.Request["isEmptyBillsExpense"]);
				if (isEmptyBillsExpense)
				{
					this.modelGoodsAllot.AllotID = HidAllotID;
					this.modelGoodsAllot.AllotAccount = strGoodsAccount;
					this.modelGoodsAllot.AllotTotalNumber = intTotalNumber;
					this.modelGoodsAllot.AllotOutShopID = intOutShopID;
					this.modelGoodsAllot.AllotInShopID = intInShopID;
					this.modelGoodsAllot.AllotCreateTime = dtCreateTime;
					this.modelGoodsAllot.AllotUserID = intUserID;
					this.modelGoodsAllot.AllotRemark = strRemark;
					this.modelGoodsAllot.Allotstate = 1;
					bool yesORno = this.bllGoodsAllot.Update(this.modelGoodsAllot);
					this.bllAllotDetail.DeleteAllorDetail(HidAllotID);
					for (int i = 0; i < intDataCount; i++)
					{
						decimal intNumber = 0m;
						int intGoodID = (this.Request["data[" + i + "][GoodsID]"] != "") ? int.Parse(this.Request["data[" + i + "][GoodsID]"]) : 0;
						decimal.TryParse(this.Request["data[" + i + "][ExpNum]"], out intNumber);
						this.modelAllotDetail.AllotDetailAllotID = HidAllotID;
						this.modelAllotDetail.AllotDetailGoodsID = intGoodID;
						this.modelAllotDetail.AllotDetailNumber = intNumber;
						this.bllAllotDetail.Add(this.modelAllotDetail);
					}
					flag = -3;
				}
				else
				{
					this.modelGoodsAllot.AllotAccount = strGoodsAccount;
					this.modelGoodsAllot.AllotTotalNumber = intTotalNumber;
					this.modelGoodsAllot.AllotOutShopID = intOutShopID;
					this.modelGoodsAllot.AllotInShopID = intInShopID;
					this.modelGoodsAllot.AllotCreateTime = dtCreateTime;
					this.modelGoodsAllot.AllotUserID = intUserID;
					this.modelGoodsAllot.AllotRemark = strRemark;
					this.modelGoodsAllot.Allotstate = 1;
					flag = this.bllGoodsAllot.Add(this.modelGoodsAllot);
					for (int i = 0; i < intDataCount; i++)
					{
						int intGoodID = (this.Request["data[" + i + "][GoodsID]"] != "") ? int.Parse(this.Request["data[" + i + "][GoodsID]"]) : 0;
						decimal intNumber = 0m;
						decimal.TryParse(this.Request["data[" + i + "][ExpNum]"], out intNumber);
						this.modelAllotDetail.AllotDetailAllotID = flag;
						this.modelAllotDetail.AllotDetailGoodsID = intGoodID;
						this.modelAllotDetail.AllotDetailNumber = intNumber;
						this.bllAllotDetail.Add(this.modelAllotDetail);
					}
					flag = -2;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void ConfirmDelivery()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intAllot = (this.Request["AllotID"].ToString() != "") ? int.Parse(this.Request["AllotID"].ToString()) : 0;
				Chain.Model.GoodsAllot ModelAllot = this.bllGoodsAllot.GetModel(intAllot);
				if (ModelAllot.Allotstate != 2)
				{
					this.modelGoodsLog.GoodsAccount = "CK" + ModelAllot.AllotAccount;
					this.modelGoodsLog.Type = 12;
					this.modelGoodsLog.Remark = "商品调拨之调出商家的明细";
					this.modelGoodsLog.CreateTime = DateTime.Now;
					this.modelGoodsLog.ShopID = intUserShopID;
					this.modelGoodsLog.UserID = intUserID;
					this.modelGoodsLog.ChangeShopID = ModelAllot.AllotOutShopID;
					int intLog = this.bllGoodsLog.Add(this.modelGoodsLog);
					DataSet ds = this.bllAllotDetail.GetAllotDetailByAllotID(ModelAllot.AllotID);
					Chain.BLL.GoodsNumber GoodNum = new Chain.BLL.GoodsNumber();
					bool YesOrNO = false;
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						YesOrNO = GoodNum.ChenkOutShopGoodsNumber(ModelAllot.AllotOutShopID, Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]), Convert.ToDecimal(ds.Tables[0].Rows[i]["AllotDetailNumber"]));
						if (!YesOrNO)
						{
							flag = -2;
							break;
						}
					}
					if (YesOrNO)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							if (!GoodNum.UPdataOutShopGoodsNumber(ModelAllot.AllotOutShopID, Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]), Convert.ToDecimal(ds.Tables[0].Rows[i]["AllotDetailNumber"])))
							{
								flag = -3;
								break;
							}
							this.modelGoodsDetail.GoodsLogID = intLog;
							this.modelGoodsDetail.GoodsID = Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]);
							this.modelGoodsDetail.GoodsInPrice = 0m;
							this.modelGoodsDetail.GoodsOutPrice = 0m;
							this.modelGoodsDetail.GoodsNumber = -Convert.ToDecimal(ds.Tables[0].Rows[i]["AllotDetailNumber"]);
							this.bllGoodsDetail.Add(this.modelGoodsDetail);
						}
						if (flag != 3)
						{
							ModelAllot.Allotstate = 2;
							this.bllGoodsAllot.Update(ModelAllot);
						}
					}
				}
				else
				{
					flag = -4;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void ConfirmReceipt()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intAllot = (this.Request["AllotID"].ToString() != "") ? int.Parse(this.Request["AllotID"].ToString()) : 0;
				Chain.Model.GoodsAllot ModelAllot = this.bllGoodsAllot.GetModel(intAllot);
				if (ModelAllot.Allotstate != 3)
				{
					this.modelGoodsLog.GoodsAccount = "RK" + ModelAllot.AllotAccount;
					this.modelGoodsLog.Type = 13;
					this.modelGoodsLog.Remark = "商品调拨之调入商家的明细";
					this.modelGoodsLog.CreateTime = DateTime.Now;
					this.modelGoodsLog.ShopID = intUserShopID;
					this.modelGoodsLog.UserID = intUserID;
					this.modelGoodsLog.ChangeShopID = ModelAllot.AllotInShopID;
					int intLog = this.bllGoodsLog.Add(this.modelGoodsLog);
					DataSet ds = this.bllAllotDetail.GetAllotDetailByAllotID(ModelAllot.AllotID);
					Chain.BLL.GoodsNumber GoodNum = new Chain.BLL.GoodsNumber();
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						if (!GoodNum.UPdataInShopGoodsNumber(ModelAllot.AllotInShopID, Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailNumber"])))
						{
							flag = -3;
							break;
						}
						this.modelGoodsDetail.GoodsLogID = intLog;
						this.modelGoodsDetail.GoodsID = Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]);
						this.modelGoodsDetail.GoodsInPrice = 0m;
						this.modelGoodsDetail.GoodsOutPrice = 0m;
						this.modelGoodsDetail.GoodsNumber = Convert.ToDecimal(ds.Tables[0].Rows[i]["AllotDetailNumber"]);
						this.bllGoodsDetail.Add(this.modelGoodsDetail);
					}
					if (flag != 3)
					{
						ModelAllot.Allotstate = 3;
						this.bllGoodsAllot.Update(ModelAllot);
					}
				}
				else
				{
					flag = -4;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void AllotRevoke()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intAllot = (this.Request["AllotID"].ToString() != "") ? int.Parse(this.Request["AllotID"].ToString()) : 0;
				Chain.Model.GoodsAllot ModelAllot = this.bllGoodsAllot.GetModel(intAllot);
				if (ModelAllot.Allotstate == 3)
				{
					DataSet ds = this.bllAllotDetail.GetAllotDetailByAllotID(ModelAllot.AllotID);
					Chain.BLL.GoodsNumber GoodNum = new Chain.BLL.GoodsNumber();
					bool YesOrNO = false;
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						YesOrNO = GoodNum.ChenkOutShopGoodsNumber(ModelAllot.AllotInShopID, Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailNumber"]));
						if (!YesOrNO)
						{
							flag = -2;
							break;
						}
					}
					if (YesOrNO)
					{
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							if (!GoodNum.UPdataInShopGoodsNumber(ModelAllot.AllotOutShopID, Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailNumber"])))
							{
								flag = -3;
								break;
							}
							if (!GoodNum.UPdataOutShopGoodsNumber(ModelAllot.AllotInShopID, Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailNumber"])))
							{
								flag = -3;
								break;
							}
						}
						if (flag != 3)
						{
							ModelAllot.Allotstate = 4;
							this.bllGoodsAllot.Update(ModelAllot);
						}
					}
					int InGoodsLogID = this.bllGoodsLog.GetIDByGoodsAccount("RK" + ModelAllot.AllotAccount);
					int OutGoodsLogID = this.bllGoodsLog.GetIDByGoodsAccount("CK" + ModelAllot.AllotAccount);
					this.bllGoodsDetail.DeleteDetail(InGoodsLogID);
					this.bllGoodsDetail.DeleteDetail(OutGoodsLogID);
					this.bllGoodsLog.Delete(InGoodsLogID);
					this.bllGoodsLog.Delete(OutGoodsLogID);
				}
				if (ModelAllot.Allotstate == 2)
				{
					DataSet ds = this.bllAllotDetail.GetAllotDetailByAllotID(ModelAllot.AllotID);
					Chain.BLL.GoodsNumber GoodNum = new Chain.BLL.GoodsNumber();
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						if (!GoodNum.UPdataInShopGoodsNumber(ModelAllot.AllotOutShopID, Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailGoodsID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["AllotDetailNumber"])))
						{
							flag = -3;
							break;
						}
					}
					ModelAllot.Allotstate = 4;
					this.bllGoodsAllot.Update(ModelAllot);
					int OutGoodsLogID = this.bllGoodsLog.GetIDByGoodsAccount("CK" + ModelAllot.AllotAccount);
					this.bllGoodsDetail.DeleteDetail(OutGoodsLogID);
					this.bllGoodsLog.Delete(OutGoodsLogID);
				}
				if (ModelAllot.Allotstate == 1)
				{
					ModelAllot.Allotstate = 4;
					this.bllGoodsAllot.Update(ModelAllot);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GoodsLogPrinting()
		{
			string msgResponse = "";
			string strGoodsLogDetail = "";
			int intID = int.Parse(this.Request["id"]);
			StringBuilder strSql = new StringBuilder();
			DataTable dtGoodsLog = this.bllGoodsLog.GetListsss(" ID=" + intID).Tables[0];
			if (dtGoodsLog != null)
			{
				string strGoodsLog = JsonPlus.ToJson(dtGoodsLog, "");
				msgResponse = "{\"strGoodsLog\":" + strGoodsLog + "}";
				DataTable dtDetail = this.bllGoodsDetail.getGoodsLogDetail(" GoodsLogDetail.GoodsLogID=" + intID).Tables[0];
				if (dtDetail != null)
				{
					strGoodsLogDetail = JsonPlus.ToJson(dtDetail, "");
				}
				msgResponse = string.Concat(new string[]
				{
					"{\"strGoodsLog\":",
					strGoodsLog,
					",\"strGoodsLogDetail\":",
					strGoodsLogDetail,
					"}"
				});
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetOrderEmptyBills()
		{
			string msgResponse = "";
			try
			{
				int intOrderID = (!string.IsNullOrEmpty(this.Request["orderID"].ToString())) ? int.Parse(this.Request["orderID"].ToString()) : 0;
				Chain.Model.OrderLog modelOrder = new Chain.BLL.OrderLog().GetModel(intOrderID);
				Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(modelOrder.OrderMemID);
				string strSql = string.Concat(new object[]
				{
					"OrderLog.OrderShopID=",
					modelOrder.OrderShopID,
					" and OrderLog.OrderType=3 and OrderLog.OrderID=",
					intOrderID
				});
				int resCount;
				DataTable dt = this.bllOrderLog.GetEmptyBillsList(10000, 1, out resCount, modelMem.MemLevelID.ToString(), new string[]
				{
					strSql
				}).Tables[0];
				if (dt != null && dt.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dt, "");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GiftAdd()
		{
			int flag = 0;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			try
			{
				Chain.Model.PointGift modelPg = new Chain.Model.PointGift();
				Chain.BLL.PointGift bllPg = new Chain.BLL.PointGift();
				modelPg.GiftName = this.Request["giftName"].ToString();
				modelPg.GiftCode = this.Request["giftCode"].ToString();
				modelPg.GiftClassID = int.Parse(this.Request["giftClassID"].ToString());
				modelPg.GiftStockNumber = int.Parse(this.Request["giftStockNumber"].ToString());
				modelPg.GiftExchangePoint = int.Parse(this.Request["giftExchangePoint"].ToString());
				modelPg.GiftShopID = intUserShopID;
				modelPg.GiftRemark = this.Request["giftRemark"].ToString();
				modelPg.GiftPhoto = this.Request["giftPhoto"].ToString();
				flag = bllPg.Add(modelPg);
				if (flag > 0)
				{
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
					PubFunction.SaveSysLog(intUserID, 1, "礼品新增", string.Concat(new object[]
					{
						"新增礼品,礼品名称：[",
						modelPg.GiftName,
						"],库存数量：[",
						modelPg.GiftStockNumber,
						"],所需积分：[",
						modelPg.GiftExchangePoint,
						"],所属商家：[",
						modelShop.ShopName,
						"]"
					}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GiftEdit()
		{
			int flag = 0;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			try
			{
				Chain.Model.PointGift modelPg = new Chain.Model.PointGift();
				Chain.BLL.PointGift bllPg = new Chain.BLL.PointGift();
				modelPg.GiftID = int.Parse(this.Request["giftID"].ToString());
				modelPg.GiftName = this.Request["giftName"].ToString();
				modelPg.GiftCode = this.Request["giftCode"].ToString();
				modelPg.GiftClassID = int.Parse(this.Request["giftClassID"].ToString());
				modelPg.GiftStockNumber = int.Parse(this.Request["giftStockNumber"].ToString());
				modelPg.GiftExchangePoint = int.Parse(this.Request["giftExchangePoint"].ToString());
				modelPg.GiftShopID = intUserShopID;
				modelPg.GiftExchangeNumber = int.Parse(this.Request["giftExchangeNumber"].ToString());
				modelPg.GiftRemark = this.Request["giftRemark"].ToString();
				modelPg.GiftPhoto = this.Request["giftPhoto"].ToString();
				flag = bllPg.Update(modelPg);
				if (flag > 0)
				{
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(modelPg.GiftShopID);
					PubFunction.SaveSysLog(intUserID, 3, "礼品编辑", string.Concat(new object[]
					{
						"编辑礼品成功,礼品名称：[",
						modelPg.GiftName,
						"],库存数量：[",
						modelPg.GiftStockNumber,
						"],所需积分：[",
						modelPg.GiftExchangePoint,
						"],所属商家：[",
						modelShop.ShopName,
						"]"
					}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GiftDel()
		{
			int flag = 0;
			try
			{
				int intGiftID = int.Parse(this.Request["GiftID"].ToString());
				Chain.BLL.PointGift bllGift = new Chain.BLL.PointGift();
				Chain.Model.PointGift modelGift = bllGift.GetModel(intGiftID);
				DataTable dt = new Chain.BLL.GiftExchangeDetail().GetList("ExchangeGiftID=" + intGiftID).Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = -4;
				}
				else if (bllGift.Delete(intGiftID))
				{
					flag = 1;
					PubFunction.SaveSysLog(this.UserModel.UserID, 2, "礼品删除", string.Concat(new object[]
					{
						"删除礼品，礼品名称：",
						modelGift.GiftName,
						",库存数量:",
						modelGift.GiftStockNumber,
						",所需积分:",
						modelGift.GiftExchangePoint,
						",所属商家:",
						PubFunction.ShopIDToName(this.UserModel.UserShopID)
					}), this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GetGiftList()
		{
			string msgResponse = "";
			try
			{
				int intSize = (this.Request["size"].ToString() != "") ? int.Parse(this.Request["size"].ToString()) : 0;
				int intIndex = (this.Request["index"].ToString() != "") ? int.Parse(this.Request["index"].ToString()) : 0;
				string key = (this.Request["key"] != null) ? this.Request["key"].ToString() : "";
				int intShopID = this.UserModel.UserShopID;
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append("GiftStockNumber > 0 ");
				if (PubFunction.curParameter.bolGiftShare)
				{
					sbWhere.Append(" AND PointGift.GiftClassID=GiftClass.GiftClassID and PointGift.GiftShopID=SysShop.ShopID ");
				}
				else
				{
					sbWhere.Append(" AND PointGift.GiftShopID=SysShop.ShopID and PointGift.GiftClassID=GiftClass.GiftClassID and GiftShopID=" + intShopID);
				}
				if (key != null && key != "")
				{
					sbWhere.AppendFormat(" and ((GiftCode like '{0}') or (GiftName like '{0}'))", key);
				}
				int resCount;
				DataTable dtGift = new Chain.BLL.PointGift().GetListSP(intSize, intIndex, true, out resCount, new string[]
				{
					sbWhere.ToString()
				}).Tables[0];
				if (dtGift != null)
				{
					msgResponse = JsonPlus.ToJson(dtGift, "GiftID,GiftName,GiftCode,GiftClassID,GiftExchangePoint,GiftStockNumber,GiftExchangeNumber");
					msgResponse = string.Concat(new object[]
					{
						"{\"RecordCount\":",
						resCount,
						",\"List\":",
						msgResponse,
						"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGiftModel()
		{
			string msgResponse = "";
			try
			{
				int intGiftID = int.Parse(this.Request["GiftID"].ToString());
				DataTable dt = new Chain.BLL.PointGift().GetItemModel(intGiftID).Tables[0];
				msgResponse = JsonPlus.ToJson(dt.Rows[0], "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GiftExchange()
		{
			string flag = "0";
			int memID = int.Parse(this.Request["memID"].ToString());
			int sumPoint = int.Parse(this.Request["sumPoint"].ToString());
			int sumNumber = int.Parse(this.Request["sumNumber"].ToString());
			int giftcount = int.Parse(this.Request["giftcount"].ToString());
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			bool sendMSM = this.Request["sendSMS"] == "true";
			string strAccount = this.Request["Account"].ToString();
			try
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				Chain.Model.PointLog modelPoint = new Chain.Model.PointLog();
				Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
				Chain.Model.GiftExchange modelGiftExchange = new Chain.Model.GiftExchange();
				Chain.BLL.GiftExchange bllGiftExchange = new Chain.BLL.GiftExchange();
				Chain.Model.GiftExchangeDetail modelDetail = new Chain.Model.GiftExchangeDetail();
				Chain.BLL.GiftExchangeDetail bllDetail = new Chain.BLL.GiftExchangeDetail();
				int levelID = modelMem.MemLevelID;
				if (modelMem.MemState != 0)
				{
					flag = "-1";
				}
				else
				{
					modelGiftExchange.MemID = memID;
					modelGiftExchange.ExchangeTelePhone = modelMem.MemTelePhone;
					modelGiftExchange.ExchangeAddress = "";
					modelGiftExchange.ExchangeAccount = strAccount;
					modelGiftExchange.ExchangeAllNumber = sumNumber;
					modelGiftExchange.ExchangeAllPoint = sumPoint;
					modelGiftExchange.ApplicationTime = DateTime.Now;
					modelGiftExchange.ExchangeStatus = 2;
					modelGiftExchange.ExchangeTime = DateTime.Now;
					modelGiftExchange.ExchangeUserID = intUserID;
					modelGiftExchange.ExchangeType = 1;
					modelGiftExchange.ShopID = this.UserModel.UserShopID;
					int intSuccess = bllGiftExchange.MainSystemAdd(modelGiftExchange);
					if (intSuccess > 0)
					{
						for (int i = 0; i < giftcount; i++)
						{
							modelDetail.ExchangeID = intSuccess;
							modelDetail.ExchangeGiftID = int.Parse(this.Request["GiftList[" + i + "][GiftID]"].ToString());
							modelDetail.ExchangeNumber = int.Parse(this.Request["GiftList[" + i + "][ExcNumber]"].ToString());
							modelDetail.Giftname = this.Request["GiftList[" + i + "][Giftname]"].ToString();
							int point = int.Parse(this.Request["GiftList[" + i + "][GiftExchangePoint]"].ToString());
							modelDetail.ExchangePoint = point * modelDetail.ExchangeNumber;
							bllDetail.Add(modelDetail);
							new Chain.BLL.PointGift().UpdateGiftNumber(modelDetail.ExchangeGiftID, modelDetail.ExchangeNumber);
						}
						modelPoint.PointMemID = modelMem.MemID;
						modelPoint.PointNumber = sumPoint;
						modelPoint.PointChangeType = 4;
						modelPoint.PointRemark = "实体店兑换礼品成功，扣减积分[" + sumPoint + "]";
						modelPoint.PointShopID = intUserShopID;
						modelPoint.PointCreateTime = DateTime.Now;
						modelPoint.PointUserID = intUserID;
						modelPoint.PointOrderCode = strAccount;
						bllPoint.Add(modelPoint);
						if (PubFunction.curParameter.bolShopPointManage)
						{
							PubFunction.SetShopPoint(intUserID, this.UserModel.UserShopID, 1, -sumPoint, "会员实体店兑换礼品成功，运营商回收积分：[" + sumPoint + "]", 4);
						}
						sumPoint *= -1;
						bllMem.UpdatePoint(memID, sumPoint);
						modelMem = new Chain.BLL.Mem().GetModel(memID);
						string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
						flag = string.Concat(new object[]
						{
							"{\"Success\":\"",
							intSuccess,
							"\",\"strUpdateMemLevel\":\"",
							strUpdateMemLevel,
							"\"}"
						});
						if (modelMem.MemMobile != "")
						{
							if (sendMSM)
							{
								if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
								{
									flag = "-2";
								}
								else if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
								{
									','
								}).Length))
								{
									SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
									smsTemplateParameter.strCardID = modelMem.MemCard;
									smsTemplateParameter.strName = modelMem.MemName;
									smsTemplateParameter.dclTempMoney = 0m;
									smsTemplateParameter.dclMoney = modelMem.MemMoney;
									smsTemplateParameter.intTempPoint = -sumPoint;
									smsTemplateParameter.intPoint = modelMem.MemPoint;
									smsTemplateParameter.OldLevelID = levelID;
									modelMem = new Chain.BLL.Mem().GetModel(memID);
									smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
									smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
									smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
									string strSendContent = SMSInfo.GetSendContent(4, smsTemplateParameter, intUserShopID);
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
								}
								else
								{
									flag = "-5";
								}
							}
						}
						PubFunction.SaveSysLog(intUserID, 4, "礼品兑换", string.Concat(new object[]
						{
							"积分兑换,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],礼品总数：[",
							sumNumber,
							"],总积分：[",
							sumNumber,
							"]"
						}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "0";
			}
			this.Context.Response.Write(flag);
		}

		public void PointDraw()
		{
			int flag = 0;
			string DrawAccount = this.Request["DrawAccount"];
			int DrawPoint = int.Parse(this.Request["DrawPoint"]);
			decimal DrawAmount = decimal.Parse(this.Request["DrawAmount"]);
			int DrawShopID = int.Parse(this.Request["DrawShopID"]);
			int DrawStatus = int.Parse(this.Request["DrawStatus"]);
			int DrawCreateUserID = int.Parse(this.Request["DrawCreateUserID"]);
			string DrawRemark = this.Request["DrawRemark"].ToString();
			DateTime DrawCreateTime = DateTime.Parse(this.Request["DrawCreateTime"]);
			try
			{
				Chain.BLL.PointDraw draw = new Chain.BLL.PointDraw();
				flag = draw.Add(new Chain.Model.PointDraw
				{
					DrawAccount = DrawAccount,
					DrawPoint = DrawPoint,
					DrawRemark = DrawRemark,
					DrawShopID = DrawShopID,
					DrawCreateTime = DrawCreateTime,
					DrawCreateUserID = DrawCreateUserID,
					DrawAmount = DrawAmount,
					DrawStatus = DrawStatus
				});
				if (flag > 0)
				{
					PubFunction.SaveSysLog(this.UserModel.UserID, 3, "积分提现", "积分提现", this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void PointDrawVerify()
		{
			int flag = 0;
			try
			{
				int DrawID = int.Parse(this.Request["DrawID"].ToString());
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.Model.PointDraw model = new Chain.Model.PointDraw();
				Chain.BLL.PointDraw draw = new Chain.BLL.PointDraw();
				model = draw.GetModel(DrawID);
				model.DrawID = DrawID;
				model.DrawStatus = 1;
				model.DrawVerifyUserID = intUserID;
				model.DrawVerifyTime = DateTime.Now;
				if (draw.Update(model))
				{
					flag = 1;
					if (PubFunction.curParameter.bolShopPointManage)
					{
						PubFunction.SetShopPoint(intUserID, intUserShopID, model.DrawShopID, model.DrawPoint, string.Concat(new object[]
						{
							"积分提现，提现金额：[",
							model.DrawAmount,
							"],提现积分：[",
							model.DrawPoint,
							"]"
						}), 6);
					}
					PubFunction.SaveSysLog(intUserID, 4, "提现审核", "审核通过", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void PointChange()
		{
			string flag = "0";
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			int intMemID = int.Parse(this.Request["memID"].ToString());
			int intPointNumber = (this.Request["pointNumber"] != null) ? int.Parse(this.Request["pointNumber"]) : 0;
			string strPointOrderCode = this.Request["pointOrderCode"].ToString();
			bool sendMSM = this.Request["sendSMS"] == "true";
			try
			{
				Chain.Model.PointLog modelPontLog = new Chain.Model.PointLog();
				Chain.BLL.PointLog bllPontLog = new Chain.BLL.PointLog();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
				int intLevelID = modelMem.MemLevelID;
				if (modelMem.MemState != 0)
				{
					flag = "-1";
				}
				else
				{
					if (!PubFunction.IsShopPoint(intUserShopID, ref intPointNumber))
					{
						flag = "-8";
						this.Context.Response.Write(flag);
						return;
					}
					string strRemark = (this.Request["remark"] != "") ? this.Request["remark"].ToString() : "无";
					string strType = this.Request["type"];
					modelPontLog.PointMemID = intMemID;
					modelPontLog.PointNumber = intPointNumber;
					modelPontLog.PointShopID = intUserShopID;
					modelPontLog.PointUserID = intUserID;
					modelPontLog.PointChangeType = 6;
					modelPontLog.PointCreateTime = DateTime.Now;
					modelPontLog.PointOrderCode = strPointOrderCode;
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
					if (intSuccess > 0)
					{
						modelMem = new Chain.BLL.Mem().GetModel(intMemID);
						string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
						if (modelMem.MemMobile != "")
						{
							if (sendMSM)
							{
								if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
								{
									intSuccess = 0;
								}
								else if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
								{
									','
								}).Length))
								{
									SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
									smsTemplateParameter.strCardID = modelMem.MemCard;
									smsTemplateParameter.strName = modelMem.MemName;
									smsTemplateParameter.intTempPoint = intPointNumber;
									smsTemplateParameter.intPoint = modelMem.MemPoint;
									smsTemplateParameter.dclTempMoney = 0m;
									smsTemplateParameter.dclMoney = modelMem.MemMoney;
									smsTemplateParameter.OldLevelID = intLevelID;
									modelMem = new Chain.BLL.Mem().GetModel(intMemID);
									smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
									smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
									smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
									string strSendContent = SMSInfo.GetSendContent(7, smsTemplateParameter, intUserShopID);
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
								}
								else
								{
									flag = "-5";
								}
							}
						}
						PubFunction.SaveSysLog(intUserID, 3, "积分变动", string.Concat(new object[]
						{
							"会员积分变动,会员卡号：[",
							modelMem.MemCard,
							"],会员姓名：[",
							modelMem.MemName,
							"],变动积分：[",
							modelPontLog.PointNumber,
							"],备注：",
							strRemark
						}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
						flag = string.Concat(new object[]
						{
							"{\"Success\":\"",
							intSuccess,
							"\",\"strUpdateMemLevel\":\"",
							strUpdateMemLevel,
							"\"}"
						});
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-3";
			}
			this.Context.Response.Write(flag);
		}

		public void GetPointRate()
		{
			string msgResponse = "";
			try
			{
				DataRow dr = new Chain.BLL.PointRate().GetDataRow();
				if (dr != null)
				{
					msgResponse = JsonPlus.ToJson(dr, "");
				}
				else
				{
					msgResponse = "";
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void PointRateEdit()
		{
			int flag = 0;
			try
			{
				int intPointRateID = (this.Request["PointRateID"].ToString() != "") ? int.Parse(this.Request["PointRateID"].ToString()) : 0;
				int intPointRateLevel = (this.Request["PointRateLevel"].ToString() != "") ? int.Parse(this.Request["PointRateLevel"].ToString()) : 0;
				bool bolPointRateType = this.Request["PointRateType"] != null && this.Request["PointRateType"] == "true";
				int decMemLevel = (this.Request["MemLevel1"].ToString() != "") ? int.Parse(this.Request["MemLevel1"].ToString()) : 0;
				int decMemLevel2 = (this.Request["MemLevel2"].ToString() != "") ? int.Parse(this.Request["MemLevel2"].ToString()) : 0;
				int decMemLevel3 = (this.Request["MemLevel3"].ToString() != "") ? int.Parse(this.Request["MemLevel3"].ToString()) : 0;
				int decMemLevel4 = (this.Request["MemLevel4"].ToString() != "") ? int.Parse(this.Request["MemLevel4"].ToString()) : 0;
				int decMemLevel5 = (this.Request["MemLevel5"].ToString() != "") ? int.Parse(this.Request["MemLevel5"].ToString()) : 0;
				int decMemLevel6 = (this.Request["MemLevel6"].ToString() != "") ? int.Parse(this.Request["MemLevel6"].ToString()) : 0;
				int decMemLevel7 = (this.Request["MemLevel7"].ToString() != "") ? int.Parse(this.Request["MemLevel7"].ToString()) : 0;
				int decMemLevel8 = (this.Request["MemLevel8"].ToString() != "") ? int.Parse(this.Request["MemLevel8"].ToString()) : 0;
				int decMemLevel9 = (this.Request["MemLevel9"].ToString() != "") ? int.Parse(this.Request["MemLevel9"].ToString()) : 0;
				int decMemLevel10 = (this.Request["MemLevel10"].ToString() != "") ? int.Parse(this.Request["MemLevel10"].ToString()) : 0;
				int decMemLevel11 = (this.Request["MemLevel11"].ToString() != "") ? int.Parse(this.Request["MemLevel11"].ToString()) : 0;
				int decMemLevel12 = (this.Request["MemLevel12"].ToString() != "") ? int.Parse(this.Request["MemLevel12"].ToString()) : 0;
				int decMemLevel13 = (this.Request["MemLevel13"].ToString() != "") ? int.Parse(this.Request["MemLevel13"].ToString()) : 0;
				int decMemLevel14 = (this.Request["MemLevel14"].ToString() != "") ? int.Parse(this.Request["MemLevel14"].ToString()) : 0;
				int decMemLevel15 = (this.Request["MemLevel15"].ToString() != "") ? int.Parse(this.Request["MemLevel15"].ToString()) : 0;
				Chain.Model.PointRate model = new Chain.Model.PointRate
				{
					PointRateID = intPointRateID,
					PointRateLevel = intPointRateLevel,
					PointRateType = bolPointRateType,
					MemLevel1 = decMemLevel,
					MemLevel2 = decMemLevel2,
					MemLevel3 = decMemLevel3,
					MemLevel4 = decMemLevel4,
					MemLevel5 = decMemLevel5,
					MemLevel6 = decMemLevel6,
					MemLevel7 = decMemLevel7,
					MemLevel8 = decMemLevel8,
					MemLevel9 = decMemLevel9,
					MemLevel10 = decMemLevel10,
					MemLevel11 = decMemLevel11,
					MemLevel12 = decMemLevel12,
					MemLevel13 = decMemLevel13,
					MemLevel14 = decMemLevel14,
					MemLevel15 = decMemLevel15
				};
				flag = new Chain.BLL.PointRate().Update(model);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(this.UserModel.UserID, 3, "积分提成", "积分提成编辑", this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void GetGiftClassModel()
		{
			string flag = "0";
			try
			{
				int intClassID = int.Parse(this.Request["ClassID"].ToString());
				DataTable dtClass = this.bllGiftClass.GetItem(intClassID);
				if (dtClass.Rows.Count != 1)
				{
					flag = "-1";
				}
				else
				{
					string strClass = JsonPlus.ToJson(dtClass.Rows[0], "");
					int intParentID = int.Parse(dtClass.Rows[0]["GiftParentID"].ToString());
					string strParent = "{}";
					if (intParentID != 0)
					{
						DataTable dtParent = this.bllGiftClass.GetItem(intParentID);
						if (dtParent.Rows.Count != 1)
						{
							flag = "-2";
						}
						else
						{
							strParent = JsonPlus.ToJson(dtParent.Rows[0], "");
						}
					}
					flag = string.Concat(new string[]
					{
						"{\"ClassModel\":",
						strClass,
						",\"ParentModel\":",
						strParent,
						"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-3";
			}
			this.Context.Response.Write(flag);
		}

		public void GiftClassDel()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intClassID = int.Parse(this.Request["ClassID"]);
				this.modelGiftClass = this.bllGiftClass.GetModel(intClassID);
				if (this.modelGiftClass.GiftParentID != 0)
				{
					DataTable dt = new Chain.BLL.PointGift().GetList("GiftClassID=" + intClassID).Tables[0];
					if (dt.Rows.Count > 0)
					{
						flag = -2;
					}
					else
					{
						this.modelGiftClass = this.bllGiftClass.GetModel(intClassID);
						if (this.bllGiftClass.Delete(intClassID))
						{
							flag = 1;
							Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
							PubFunction.SaveSysLog(intUserID, 1, "礼品分类", "删除礼品分类，分类名称：[" + this.modelGiftClass.GiftClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
				}
				else
				{
					DataTable dt = this.bllGiftClass.GetList(" GiftParentID=" + intClassID).Tables[0];
					if (dt.Rows.Count > 0)
					{
						string strClass = "";
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							strClass = strClass + dt.Rows[i]["GiftClassID"] + ",";
						}
						int length = strClass.Length;
						strClass = strClass.Substring(0, length - 1);
						DataTable dtGift = new Chain.BLL.PointGift().GetList(" GiftClassID in (" + strClass + ")").Tables[0];
						if (dtGift.Rows.Count > 0)
						{
							flag = -2;
						}
						else
						{
							this.modelGiftClass = this.bllGiftClass.GetModel(intClassID);
							if (this.bllGiftClass.Delete(intClassID))
							{
								this.bllGiftClass.DeleteByParentID(intClassID);
								flag = 1;
								Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
								PubFunction.SaveSysLog(intUserID, 1, "礼品分类", "删除礼品分类，分类名称：[" + this.modelGiftClass.GiftClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
							}
						}
					}
					else
					{
						DataTable dtGift = new Chain.BLL.PointGift().GetList(" GiftClassID = (" + intClassID + ")").Tables[0];
						if (dtGift.Rows.Count > 0)
						{
							flag = -2;
						}
						else if (this.bllGiftClass.Delete(intClassID))
						{
							this.bllGiftClass.DeleteByParentID(intClassID);
							flag = 1;
							Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
							PubFunction.SaveSysLog(intUserID, 1, "礼品分类", "删除礼品分类，分类名称：[" + this.modelGiftClass.GiftClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GiftClassAdd()
		{
			int flag = 0;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			try
			{
				string strClassName = this.Request["ClassName"];
				strClassName = PubFunction.RemoveSpace(strClassName);
				string strClassRemark = (this.Request["ClassRemark"] != "") ? this.Request["ClassRemark"].ToString() : "";
				strClassRemark = PubFunction.RemoveSpace(strClassRemark);
				this.modelGiftClass.GiftClassName = strClassName;
				this.modelGiftClass.GiftParentID = int.Parse(this.Request["ParentID"]);
				this.modelGiftClass.GiftClassRemark = strClassRemark;
				flag = this.bllGiftClass.Add(this.modelGiftClass);
				if (flag > 0)
				{
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
					PubFunction.SaveSysLog(intUserID, 1, "礼品分类", "新增礼品分类，分类名称：[" + this.modelGiftClass.GiftClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GiftClassEdit()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intClassID = int.Parse(this.Request["ClassID"].ToString());
				int intParentID = int.Parse(this.Request["ParentID"].ToString());
				string strClassName = this.Request["ClassName"].ToString();
				string strClassRemark = (this.Request["ClassRemark"] != "") ? this.Request["ClassRemark"].ToString() : "";
				this.modelGiftClass.GiftClassID = intClassID;
				this.modelGiftClass.GiftClassName = strClassName;
				this.modelGiftClass.GiftClassRemark = strClassRemark;
				this.modelGiftClass.GiftParentID = intParentID;
				flag = this.bllGiftClass.Update(this.modelGiftClass);
				if (flag > 0)
				{
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
					PubFunction.SaveSysLog(intUserID, 1, "礼品分类", "编辑礼品分类，分类名称：[" + this.modelGiftClass.GiftClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void AllowExchange()
		{
			int flag = 0;
			try
			{
				int intID = int.Parse(this.Request["ID"].ToString());
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.Model.GiftExchange modelGiftExchange = new Chain.Model.GiftExchange();
				Chain.BLL.GiftExchange bllGiftExchange = new Chain.BLL.GiftExchange();
				modelGiftExchange = bllGiftExchange.GetModel(intID);
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = new Chain.Model.Mem();
				modelMem = bllMem.GetModel(modelGiftExchange.MemID);
				Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
				Chain.Model.PointLog modelPoint = new Chain.Model.PointLog();
				Chain.BLL.PointGift bllGift = new Chain.BLL.PointGift();
				Chain.Model.PointGift modelGift = new Chain.Model.PointGift();
				Chain.BLL.GiftExchangeDetail bllExchange = new Chain.BLL.GiftExchangeDetail();
				Chain.Model.GiftExchangeDetail modelExchange = new Chain.Model.GiftExchangeDetail();
				if (modelMem.MemState != 0)
				{
					if (modelMem.MemState == 1)
					{
						flag = -3;
					}
					else if (modelMem.MemState == 2)
					{
						flag = -4;
					}
				}
				else if (modelGiftExchange.ExchangeAllPoint > modelMem.MemPoint)
				{
					flag = -1;
				}
				else
				{
					if (PubFunction.curParameter.bolShopPointManage)
					{
						PubFunction.SetShopPoint(intUserID, 1, -modelGiftExchange.ExchangeAllPoint, string.Concat(new object[]
						{
							"单号：[",
							modelGiftExchange.ExchangeAccount,
							"],会员积分兑换礼品扣除积分：[",
							modelGiftExchange.ExchangeAllPoint,
							"]，运营商回收积分：[",
							modelGiftExchange.ExchangeAllPoint,
							"]"
						}), 4);
					}
					string strWhere = string.Format(" GiftExchangeDetail.ExchangeGiftID=PointGift.GiftID and  ExchangeID={0} ", intID);
					DataTable dtExchange = bllExchange.GetList(strWhere).Tables[0];
					for (int intCount = 0; intCount < dtExchange.Rows.Count; intCount++)
					{
						int intExchangeNumber = int.Parse(dtExchange.Rows[intCount]["ExchangeNumber"].ToString());
						modelGift = bllGift.GetModel(int.Parse(dtExchange.Rows[intCount]["ExchangeGiftID"].ToString()));
						if (intExchangeNumber > modelGift.GiftStockNumber)
						{
							flag = -2;
							break;
						}
					}
					if (flag == 0)
					{
						for (int intCount = 0; intCount < dtExchange.Rows.Count; intCount++)
						{
							int intExchangeNumber = int.Parse(dtExchange.Rows[intCount]["ExchangeNumber"].ToString());
							bllGift.UpdateGiftNumber(int.Parse(dtExchange.Rows[intCount]["ExchangeGiftID"].ToString()), intExchangeNumber);
						}
						bllMem.UpdatePoint(modelGiftExchange.MemID, modelGiftExchange.ExchangeAllPoint * -1);
						modelMem = new Chain.BLL.Mem().GetModel(modelGiftExchange.MemID);
						PubFunction.UpdateMemLevel(modelMem);
						modelGiftExchange.ExchangeStatus = 2;
						modelGiftExchange.ExchangeUserID = intUserID;
						modelGiftExchange.ExchangeTime = DateTime.Now;
						if (bllGiftExchange.Update(modelGiftExchange))
						{
							flag = 1;
							modelPoint.PointMemID = modelMem.MemID;
							modelPoint.PointNumber = modelGiftExchange.ExchangeAllPoint;
							modelPoint.PointChangeType = 1;
							modelPoint.PointRemark = "微信端积分兑换礼品审核通过，扣除会员积分：[" + modelGiftExchange.ExchangeAllPoint + "]";
							modelPoint.PointShopID = intUserShopID;
							modelPoint.PointCreateTime = DateTime.Now;
							modelPoint.PointUserID = intUserID;
							modelPoint.PointOrderCode = modelGiftExchange.ExchangeAccount;
							bllPoint.Add(modelPoint);
							PubFunction.SaveSysLog(intUserID, 4, "兑换审核", string.Concat(new object[]
							{
								"审核通过,会员卡号：[",
								modelMem.MemCard,
								"],姓名：[",
								modelMem.MemName,
								"],总积分：[",
								modelGiftExchange.ExchangeAllPoint,
								"]"
							}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void NoExchange()
		{
			int flag = 0;
			try
			{
				int intID = int.Parse(this.Request["ID"].ToString());
				int intUserID = this.UserModel.UserID;
				string strExchangeRemark = this.Request["ExchangeRemark"].ToString();
				Chain.Model.GiftExchange modelGiftExchange = new Chain.Model.GiftExchange();
				Chain.BLL.GiftExchange bllGiftExchange = new Chain.BLL.GiftExchange();
				modelGiftExchange = bllGiftExchange.GetModel(intID);
				modelGiftExchange.ExchangeStatus = 3;
				modelGiftExchange.ExchangeUserID = intUserID;
				modelGiftExchange.ExchangeTime = DateTime.Now;
				modelGiftExchange.ExchangeRemark = strExchangeRemark;
				if (bllGiftExchange.Update(modelGiftExchange))
				{
					flag = 1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void Expense()
		{
			string flag = "-1";
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			int intMemID = (this.Request["memID"] != null) ? int.Parse(this.Request["memID"].ToString()) : 0;
			int intPoint = (this.Request["point"].ToString() != "") ? int.Parse(this.Request["point"].ToString()) : 0;
			bool chkNoMember = this.Request["chkNoMember"].ToString() == "true";
			int intIsTimeExpense = int.Parse(this.Request["isTimeExpense"].ToString());
			decimal dclMoney = (this.Request["money"].ToString() != "") ? decimal.Parse(this.Request["money"].ToString()) : 0m;
			decimal dclDiscountMoney = decimal.Parse(this.Request["parameter[0][DiscountMoney]"].ToString());
			decimal dclCardPayMoney = (this.Request["parameter[0][CardMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CardMoney]"]) : 0m;
			decimal dclCashPayMoney = (this.Request["parameter[0][CashMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CashMoney]"]) : 0m;
			decimal dclBinkPayMoney = (this.Request["parameter[0][BinkMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][BinkMoney]"]) : 0m;
			decimal dclCouponPayMoney = (this.Request["parameter[0][CouponMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CouponMoney]"]) : 0m;
			string strOrderAccount = (this.Request["orderAccount"].ToString() != "") ? this.Request["orderAccount"].ToString() : "";
			string strRemark = this.Request["remark"].ToString();
			bool sendMSM = this.Request["sendSMS"] == "true";
			bool bolIsCard = bool.Parse(this.Request["parameter[0][IsCard]"]);
			bool bolIsCash = bool.Parse(this.Request["parameter[0][IsCash]"]);
			bool bolIsBink = bool.Parse(this.Request["parameter[0][IsBink]"]);
			DateTime dtNow = Convert.ToDateTime(this.Request["endtime"]);
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
			int intLevelID = modelMem.MemLevelID;
			int usePoint = int.Parse(this.Request["parameter[0][usePoint]"]);
			decimal usePointAmount = decimal.Parse(this.Request["parameter[0][usePointAmount]"]);


            string alipaymoney = this.Request["parameter[0][alipaymoney]"];
            string alipaycode = this.Request["parameter[0][alipaycode]"];

            string wxmoney = this.Request["parameter[0][wxmoney]"];
            string wxcode = this.Request["parameter[0][wxcode]"];

            try
            {

                if (string.IsNullOrEmpty(wxcode) == false)
                {
                    if (ChainStock.PayApi.weixin.micropay.Run("微信", wxmoney, wxcode) == false)
                    {
                        //                  throw new Exception("微信支付失败");
                        this.Context.Response.Write("-1");
                        return;
                    }
                }


                if (string.IsNullOrEmpty(alipaycode) == false)
                {
                    ChainStock.PayApi.alipay.Alipay alipay = new PayApi.alipay.Alipay();
                    if (alipay.Alipay_Deliver(alipaymoney, alipaycode) == false)
                    {
                        //                    throw new Exception("支付宝支付失败");
                        this.Context.Response.Write("-1");
                        return;

                    }
                }

            }
            catch(Exception ex)
            {
                this.Context.Response.Write("-1");
                return;

            }


			string Remark;
			if (intMemID != 0)
			{
				Remark = string.Concat(new object[]
				{
					"会员快速消费,会员卡号：[",
					modelMem.MemCard,
					"],姓名：[",
					modelMem.MemName,
					"],订单号：[",
					strOrderAccount,
					"],消费金额：[",
					dclDiscountMoney,
					"],获得积分：[",
					intPoint,
					"],备注：",
					strRemark
				});
			}
			else
			{
				Remark = string.Concat(new object[]
				{
					"散客快速消费,订单号：[",
					strOrderAccount,
					"],消费金额：[",
					dclDiscountMoney,
					"],备注：",
					strRemark
				});
			}
			if (PubFunction.IsShopPoint(intUserShopID, ref intPoint) || chkNoMember)
			{
				try
				{
					Chain.Model.OrderLog mdOrderLog = new Chain.Model.OrderLog();
					Chain.Model.SysLog mdSysLog = new Chain.Model.SysLog();
					Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
					if (!chkNoMember)
					{
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
						mdOrderLog.OrderPayCoupon = dclCouponPayMoney;
						mdOrderLog.OrderAccount = strOrderAccount;
						mdOrderLog.OrderShopID = intUserShopID;
						mdOrderLog.OrderUserID = intUserID;
						mdOrderLog.OrderRemark = strRemark;
						mdOrderLog.OrderCreateTime = DateTime.Now;
						mdOrderLog.OrderPayType = 0;
						if (intIsTimeExpense == 1)
						{
							mdOrderLog.OrderType = 1;
						}
						mdOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
						mdOrderLog.OrderCardPoint = modelMem.MemPoint + intPoint;
						mdOrderLog.UsePoint = usePoint;
						mdOrderLog.UsePointAmount = usePointAmount;
						Chain.BLL.OrderLog bllOrder = new Chain.BLL.OrderLog();
						int intSuccess = bllOrder.Add(mdOrderLog, strOrderAccount);
						if (intSuccess > 0)
						{
							decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
							modelMem.MemConsumeMoney += mdOrderLog.OrderDiscountMoney;
							modelMem.MemPoint += mdOrderLog.OrderPoint - usePoint;
							modelMem.MemConsumeLastTime = DateTime.Now;
							modelMem.MemConsumeCount++;
							int mem = bllMem.ExpenseUpdateMem(intMemID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
							Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
							moneyChangeLogModel.MoneyChangeMemID = intMemID;
							moneyChangeLogModel.MoneyChangeUserID = intUserID;
							moneyChangeLogModel.MoneyChangeType = ((intIsTimeExpense == 1) ? 9 : 3);
							moneyChangeLogModel.MoneyChangeAccount = strOrderAccount;
							moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
							moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
							moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
							moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
							moneyChangeLogModel.MemMoney = modelMem.MemMoney - dclCardPayMoney;
							moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
							moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
							new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
							mdPoint.PointMemID = intMemID;
							mdPoint.PointNumber = intPoint;
							mdPoint.PointChangeType = 2;
							mdPoint.PointRemark = string.Concat(new object[]
							{
								"会员快速消费，消费金额：[",
								dclDiscountMoney,
								"],获得积分：[",
								intPoint,
								"]"
							});
							if (intIsTimeExpense == 1)
							{
								mdPoint.PointChangeType = 10;
								mdPoint.PointRemark = string.Concat(new object[]
								{
									"会员计时消费，消费金额：[",
									dclDiscountMoney,
									"],获得积分：[",
									intPoint,
									"]"
								});
							}
							mdPoint.PointShopID = intUserShopID;
							mdPoint.PointCreateTime = DateTime.Now;
							mdPoint.PointUserID = intUserID;
							mdPoint.PointOrderCode = strOrderAccount;
							this.bllPoint.Add(mdPoint);
							PubFunction.SetShopPoint(intUserID, intUserShopID, intPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderAccount,
								"],会员快速消费金额：[",
								dclDiscountMoney,
								"],扣除商家积分：[",
								intPoint,
								"]"
							}), 2);
							MEMPointUpdate.MEMPointRate(modelMem, mdOrderLog.OrderPoint, mdOrderLog.OrderAccount, 2, intUserID, intUserShopID);
							if (usePoint != 0)
							{
								PubFunction.SetShopPoint(intUserID, intUserShopID, -usePoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],会员快速消费抵用积分：[",
									usePoint,
									"]，商家回收积分：[",
									usePoint,
									"]"
								}), 4);
								Chain.Model.PointLog modelPointLog = new Chain.Model.PointLog();
								modelPointLog.PointMemID = intMemID;
								modelPointLog.PointNumber = usePoint;
								modelPointLog.PointChangeType = 1;
								modelPointLog.PointRemark = string.Concat(new object[]
								{
									"会员快速消费成功,抵用积分：[",
									usePoint,
									"],抵用金额：[",
									usePointAmount,
									"]"
								});
								modelPointLog.PointShopID = intUserShopID;
								modelPointLog.PointUserID = intUserID;
								modelPointLog.PointCreateTime = DateTime.Now;
								modelPointLog.PointOrderCode = strOrderAccount;
								this.bllPoint.Add(modelPointLog);
							}
							modelMem = new Chain.BLL.Mem().GetModel(intMemID);
							string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
							Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
							Chain.Model.SysShop modelShop = bllSysShop.GetModel(intUserShopID);
							decimal flTotalRate = decimal.Parse(Convert.ToDouble(modelShop.TotalRate).ToString("#0.00"));
							if (flTotalRate > 0m)
							{
								int flTotalPoint = (int)(flTotalRate * intPoint);
								decimal alliancePercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.AllianceRebatePercent).ToString("#0.00"));
								decimal cardShopPercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.CardShopRebatePercent).ToString("#0.00"));
								int alliancePoint = (int)(flTotalPoint * alliancePercent);
								int cardShopPoint = (int)(flTotalPoint * cardShopPercent);
								int zbPoint = flTotalPoint - alliancePoint - cardShopPoint;
								Chain.BLL.ReturnPointLog bllReturnPoint = new Chain.BLL.ReturnPointLog();
								bllReturnPoint.Add(new Chain.Model.ReturnPointLog
								{
									OrderAccount = strOrderAccount,
									MemID = intMemID,
									TotalPoint = flTotalPoint,
									AlliancePoint = alliancePoint,
									ZbPoint = zbPoint,
									CardShopPoint = cardShopPoint,
									Remark = "会员快速消费,商家返利积分",
									ReturnShopID = intUserShopID,
									CreateTime = DateTime.Now
								});
								PubFunction.SetShopPoint(intUserID, intUserShopID, intUserShopID, flTotalPoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],会员快速消费得积分：[",
									intPoint,
									"],返利总比例：[",
									flTotalRate,
									"],商家扣除返利总积分：[",
									flTotalPoint,
									"]"
								}), 2);
								PubFunction.SetShopPoint(intUserID, intUserShopID, modelShop.FatherShopID, -alliancePoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],商家总返利积分：[",
									flTotalPoint,
									"],联盟商返利比例：[",
									alliancePercent,
									"],联盟商得到返利积分：[",
									alliancePoint,
									"]"
								}), 3);
								PubFunction.SetShopPoint(intUserID, intUserShopID, modelMem.MemShopID, -cardShopPoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],商家总返利积分：[",
									flTotalPoint,
									"],开卡商家返利比例：[",
									cardShopPercent,
									"],开卡商家得到返利积分：[",
									cardShopPoint,
									"]"
								}), 3);
								PubFunction.SetShopPoint(intUserID, intUserShopID, 1, -zbPoint, string.Concat(new object[]
								{
									"单号：[",
									strOrderAccount,
									"],商家总返利积分：[",
									flTotalPoint,
									"],运营商得到返利积分：[",
									zbPoint,
									"]"
								}), 3);
							}
							if (intIsTimeExpense == 1)
							{
								string myremark = this.Request["myremark"].ToString().Remove(0, 53);
								PubFunction.TimeExpenseEnd(strOrderAccount, intUserID, dtNow, myremark);
							}
							flag = string.Concat(new object[]
							{
								"{\"Success\":\"",
								intSuccess,
								"\",\"strUpdateMemLevel\":\"",
								strUpdateMemLevel,
								"\",\"point\":",
								intPoint.ToString(),
								"}"
							});
							if (modelMem.MemMobile != "")
							{
								if (sendMSM)
								{
									if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
									{
										flag = "-2";
									}
									else if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
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
										smsTemplateParameter.OldLevelID = intLevelID;
										modelMem = new Chain.BLL.Mem().GetModel(intMemID);
										smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
										smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
										smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
										string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, intUserShopID);
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
										PubFunction.SaveSysLog(intUserID, 4, "会员消费-快速消费", string.Concat(new string[]
										{
											"快速消费发短信成功,会员卡号：[",
											modelMem.MemCard,
											"],姓名：[",
											modelMem.MemName,
											"]"
										}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
									}
									else
									{
										flag = "-5";
									}
								}
							}
							PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
					else
					{
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
						mdOrderLog.OrderPayCoupon = dclCouponPayMoney;
						mdOrderLog.OrderAccount = strOrderAccount;
						mdOrderLog.OrderShopID = intUserShopID;
						mdOrderLog.OrderUserID = intUserID;
						mdOrderLog.OrderRemark = strRemark;
						mdOrderLog.OrderCreateTime = DateTime.Now;
						mdOrderLog.OrderPayType = 0;
						if (intIsTimeExpense == 1)
						{
							mdOrderLog.OrderType = 1;
						}
						Chain.BLL.OrderLog bllOrder = new Chain.BLL.OrderLog();
						int intSuccess = bllOrder.Add(mdOrderLog);
						if (intIsTimeExpense == 1)
						{
							string myremark = this.Request["myremark"].ToString().Remove(0, 53);
							PubFunction.TimeExpenseEnd(strOrderAccount, intUserID, dtNow, myremark);
						}
						if (intSuccess > 0)
						{
							PubFunction.SaveSysLog(intUserID, 4, "会员管理", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
							flag = string.Concat(new object[]
							{
								"{\"Success\":\"",
								intSuccess,
								"\",\"strUpdateMemLevel\":\"\",\"point\":",
								intPoint.ToString(),
								"}"
							});
						}
					}
				}
				catch (Exception e)
				{
					this.LogError(e);
					flag = "-1";
				}
			}
			else
			{
				flag = "-6";
			}
			this.Context.Response.Write(flag);
		}

		public void Revoke()
		{
			string flag = "0";
			try
			{
				int intOrderID = int.Parse(this.Request["orderID"].ToString());
				int intMemID = int.Parse(this.Request["memID"].ToString());
				this.modelOrderLog = this.bllOrderLog.GetModel(intOrderID);
				this.modelMem = this.bllMem.GetModel(this.modelOrderLog.OrderMemID);
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				string strUpdateMemLevel = "";
				if (this.modelOrderLog.OrderType == 5)
				{
					return;
				}
				if (this.modelMem.MemID != 0)
				{
					if (this.modelMem.MemPoint < this.modelOrderLog.OrderPoint)
					{
						flag = string.Concat(new object[]
						{
							"{\"Success\":\"",
							-3,
							"\",\"strUpdateMemLevel\":\"",
							strUpdateMemLevel,
							"\"}"
						});
					}
					else
					{
						Chain.Model.OrderLog orderLog = new Chain.Model.OrderLog();
						orderLog.OrderAccount = "CD" + DateTime.Now.ToString("yyMMddHHmmssffff");
						orderLog.OrderType = 4;
						orderLog.OrderMemID = this.modelMem.MemID;
						orderLog.OrderTotalMoney = this.modelOrderLog.OrderTotalMoney * -1m;
						orderLog.OrderDiscountMoney = this.modelOrderLog.OrderDiscountMoney * -1m;
						orderLog.OrderIsCard = this.modelOrderLog.OrderIsCard;
						orderLog.OrderPayCard = this.modelOrderLog.OrderPayCard * -1m;
						orderLog.OrderIsCash = this.modelOrderLog.OrderIsCash;
						orderLog.OrderPayCash = this.modelOrderLog.OrderPayCash * -1m;
						orderLog.OrderIsBink = this.modelOrderLog.OrderIsBink;
						orderLog.OrderPayBink = this.modelOrderLog.OrderPayBink * -1m;
						orderLog.OrderPayCoupon = this.modelOrderLog.OrderPayCoupon * -1m;
						orderLog.OrderPoint = this.modelOrderLog.OrderPoint * -1;
						orderLog.OrderRemark = "会员消费撤单";
						orderLog.OrderPayType = 0;
						orderLog.OrderShopID = intUserShopID;
						orderLog.OrderCreateTime = DateTime.Now;
						orderLog.OrderUserID = intUserID;
						orderLog.OldAccount = this.modelOrderLog.OrderAccount;
						orderLog.OrderCardBalance = this.modelMem.MemMoney - orderLog.OrderPayCard;
						orderLog.OrderCardPoint = this.modelMem.MemPoint + orderLog.OrderPoint;
						int addLog = this.bllOrderLog.Add(orderLog);
						Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
						moneyChangeLogModel.MoneyChangeMemID = intMemID;
						moneyChangeLogModel.MoneyChangeUserID = intUserID;
						moneyChangeLogModel.MoneyChangeType = ((this.modelOrderLog.OrderType == 0) ? 4 : ((this.modelOrderLog.OrderType == 1) ? 10 : 11));
						moneyChangeLogModel.MoneyChangeAccount = this.modelOrderLog.OrderAccount;
						moneyChangeLogModel.MoneyChangeMoney = this.modelOrderLog.OrderPayCash + this.modelOrderLog.OrderPayCard + this.modelOrderLog.OrderPayBink;
						moneyChangeLogModel.MoneyChangeBalance = this.modelOrderLog.OrderPayCard;
						moneyChangeLogModel.MoneyChangeCash = this.modelOrderLog.OrderPayCash;
						moneyChangeLogModel.MoneyChangeUnionPay = this.modelOrderLog.OrderPayBink;
						moneyChangeLogModel.MemMoney = this.modelMem.MemMoney + this.modelOrderLog.OrderPayCard;
						moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
						moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
						Chain.BLL.ReturnPointLog bllReturnLog = new Chain.BLL.ReturnPointLog();
						Chain.Model.ReturnPointLog modelReturnLog = bllReturnLog.GetModelByOrderAccount(this.modelOrderLog.OrderAccount);
						Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(this.modelOrderLog.OrderShopID);
						if (PubFunction.curParameter.bolShopPointManage && modelReturnLog != null)
						{
							PubFunction.SetShopPoint(intUserID, intUserShopID, this.modelOrderLog.OrderShopID, -this.modelOrderLog.OrderPoint, string.Concat(new object[]
							{
								"撤销单号：[",
								orderLog.OrderAccount,
								"],会员消费撤单,返还商家消费积分：[",
								this.modelOrderLog.OrderPoint,
								"]"
							}), 7);
							PubFunction.SetShopPoint(intUserID, intUserShopID, this.modelOrderLog.OrderShopID, -modelReturnLog.TotalPoint, string.Concat(new object[]
							{
								"撤销单号：[",
								orderLog.OrderAccount,
								"],会员消费撤单,返还商家返利积分：[",
								modelReturnLog.TotalPoint,
								"]"
							}), 7);
							PubFunction.SetShopPoint(intUserID, intUserShopID, this.modelMem.MemShopID, modelReturnLog.CardShopPoint, string.Concat(new object[]
							{
								"撤销单号：[",
								orderLog.OrderAccount,
								"],会员消费撤单,扣除开卡商家返利积分：[",
								modelReturnLog.CardShopPoint,
								"]"
							}), 7);
							PubFunction.SetShopPoint(intUserID, intUserShopID, modelShop.FatherShopID, modelReturnLog.AlliancePoint, string.Concat(new object[]
							{
								"撤销单号：[",
								orderLog.OrderAccount,
								"],会员消费撤单,扣除联盟商返利积分：[",
								modelReturnLog.AlliancePoint,
								"]"
							}), 7);
							PubFunction.SetShopPoint(intUserID, intUserShopID, 1, modelReturnLog.ZbPoint, string.Concat(new object[]
							{
								"撤销单号：[",
								orderLog.OrderAccount,
								"],会员消费撤单,扣除运营商返利积分：[",
								modelReturnLog.ZbPoint,
								"]"
							}), 7);
							modelReturnLog.CardShopPoint = -modelReturnLog.CardShopPoint;
							modelReturnLog.AlliancePoint = -modelReturnLog.AlliancePoint;
							modelReturnLog.TotalPoint = -modelReturnLog.TotalPoint;
							modelReturnLog.ZbPoint = -modelReturnLog.ZbPoint;
							modelReturnLog.OrderAccount = orderLog.OrderAccount;
							modelReturnLog.Remark = "会员消费撤单,返还返利积分";
							modelReturnLog.ReturnShopID = this.modelOrderLog.OrderShopID;
							modelReturnLog.CreateTime = DateTime.Now;
							bllReturnLog.Add(modelReturnLog);
						}
						if (this.modelOrderLog.OrderPayCard != 0m)
						{
							Chain.Model.MemRecharge mdRechange = new Chain.Model.MemRecharge();
							mdRechange.RechargeMemID = this.modelMem.MemID;
							mdRechange.RechargeAccount = new CurrentParameter().strMemRechargePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
							mdRechange.RechargeMoney = this.modelOrderLog.OrderPayCard;
							mdRechange.RechargeShopID = 1;
							mdRechange.RechargeUserID = intUserID;
							mdRechange.RechargeCreateTime = DateTime.Now;
							mdRechange.RechargeIsApprove = true;
							mdRechange.RechargeRemark = "会员撤单时，余额支付的金额将充入会员卡余额";
							mdRechange.RechargeGive = this.modelOrderLog.OrderPayCard;
							mdRechange.RechargeCardBalance = this.modelMem.MemMoney + this.modelOrderLog.OrderPayCard;
							mdRechange.RechargeType = 5;
							mdRechange.RechargePoint = 0;
							new Chain.BLL.MemRecharge().Add(mdRechange);
						}
						if (addLog > 0)
						{
							if (this.modelOrderLog.OrderType == 2)
							{
								this.modelGoodsLog.GoodsAccount = this.modelOrderLog.OrderAccount;
								this.modelGoodsLog.Type = 5;
								this.modelGoodsLog.TotalPrice = this.modelOrderLog.OrderDiscountMoney;
								this.modelGoodsLog.Remark = "商品撤单入库";
								this.modelGoodsLog.CreateTime = DateTime.Now;
								this.modelGoodsLog.ShopID = intUserShopID;
								this.modelGoodsLog.UserID = intUserID;
								this.modelGoodsLog.ChangeShopID = this.modelOrderLog.OrderShopID;
								int intLog = this.bllGoodsLog.Add(this.modelGoodsLog);
								DataTable dtOrder = this.bllDetail.GetList(" OrderID=" + intOrderID).Tables[0];
								for (int i = 0; i < dtOrder.Rows.Count; i++)
								{
									this.modelGoods = this.bllGoods.GetModel(int.Parse(dtOrder.Rows[i]["GoodsID"].ToString()));
									this.modelDetail.OrderID = addLog;
									this.modelDetail.GoodsID = int.Parse(dtOrder.Rows[i]["GoodsID"].ToString());
									this.modelDetail.OrderDetailPrice = decimal.Parse(dtOrder.Rows[i]["OrderDetailPrice"].ToString());
									this.modelDetail.OrderDetailPoint = int.Parse(dtOrder.Rows[i]["OrderDetailPoint"].ToString()) * -1;
									this.modelDetail.OrderDetailDiscountPrice = decimal.Parse(dtOrder.Rows[i]["OrderDetailDiscountPrice"].ToString()) * -1m;
									this.modelDetail.OrderDetailNumber = decimal.Parse(dtOrder.Rows[i]["OrderDetailNumber"].ToString()) * -1m;
									if (this.bllDetail.Add(this.modelDetail) > 0)
									{
										if (float.Parse(dtOrder.Rows[i]["OrderDetailNumber"].ToString()) < 0f)
										{
											int intOrderDetailNumber = Math.Abs(int.Parse(dtOrder.Rows[i]["OrderDetailNumber"].ToString()));
											DataTable dtCountDetail = this.bllCountDetail.GetList(-1, string.Concat(new object[]
											{
												" CountDetailGoodsID=",
												this.modelGoods.GoodsID,
												" and CountDetailMemID=",
												this.modelOrderLog.OrderMemID
											}), "CountCreateTime").Tables[0];
											for (int j = 0; j < dtCountDetail.Rows.Count; j++)
											{
												if (intOrderDetailNumber != 0)
												{
													if (int.Parse(dtCountDetail.Rows[j]["CountDetailNumber"].ToString()) + intOrderDetailNumber <= int.Parse(dtCountDetail.Rows[j]["CountDetailTotalNumber"].ToString()))
													{
														intOrderDetailNumber *= -1;
														this.bllCountDetail.UpdateCountDetailNumber(intOrderDetailNumber, int.Parse(dtCountDetail.Rows[j]["CountDetailID"].ToString()));
														intOrderDetailNumber = 0;
													}
													else
													{
														int inNumber = int.Parse(dtCountDetail.Rows[j]["CountDetailTotalNumber"].ToString()) - int.Parse(dtCountDetail.Rows[j]["CountDetailNumber"].ToString());
														inNumber *= -1;
														this.bllCountDetail.UpdateCountDetailNumber(inNumber, int.Parse(dtCountDetail.Rows[j]["CountDetailID"].ToString()));
														intOrderDetailNumber += inNumber;
													}
													PubFunction.SaveSysLog(intUserID, 4, "会员消费撤单", string.Concat(new object[]
													{
														"会员计次撤单，计次名称[",
														this.modelGoods.Name,
														"],次数[",
														intOrderDetailNumber,
														"]"
													}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
												}
											}
										}
										else if (this.modelGoods.GoodsType == 0)
										{
											this.modelNumber.GoodsID = int.Parse(dtOrder.Rows[i]["GoodsID"].ToString());
											this.modelNumber.ShopID = this.modelOrderLog.OrderShopID;
											this.modelNumber.Number = int.Parse(dtOrder.Rows[i]["OrderDetailNumber"].ToString());
											this.bllNumber.UpdataGoodsNumber(this.modelNumber);
											this.modelGoodsDetail.GoodsLogID = intLog;
											this.modelGoodsDetail.GoodsID = this.modelNumber.GoodsID;
											this.modelGoodsDetail.GoodsInPrice = 0m;
											this.modelGoodsDetail.GoodsOutPrice = 0m;
											this.modelGoodsDetail.GoodsNumber = this.modelNumber.Number;
											this.bllGoodsDetail.Add(this.modelGoodsDetail);
											PubFunction.SaveSysLog(intUserID, 4, "会员消费撤单", string.Concat(new object[]
											{
												"会员商品撤单，商品名称[",
												this.modelGoods.Name,
												"],数量[",
												this.modelNumber.Number,
												"]"
											}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
										}
									}
								}
							}
							decimal dclMemMoney = this.modelMem.MemMoney + this.modelOrderLog.OrderPayCard;
							this.modelMem.MemConsumeMoney -= this.modelOrderLog.OrderDiscountMoney;
							this.modelMem.MemPoint -= this.modelOrderLog.OrderPoint;
							this.modelMem.MemConsumeLastTime = DateTime.Now;
							this.modelMem.MemConsumeCount--;
							int mem = this.bllMem.ExpenseUpdateMem(this.modelOrderLog.OrderMemID, dclMemMoney, this.modelMem.MemConsumeMoney, this.modelMem.MemPoint, DateTime.Now, this.modelMem.MemConsumeCount);
							DataTable dtRecommendPoint = this.bllPoint.GetList(" PointChangeType=9 and PointOrderCode='" + this.modelOrderLog.OrderAccount + "'").Tables[0];
							if (dtRecommendPoint != null)
							{
								for (int i = 0; i < dtRecommendPoint.Rows.Count; i++)
								{
									this.modelPoint.PointMemID = int.Parse(dtRecommendPoint.Rows[i]["PointMemID"].ToString());
									this.modelPoint.PointNumber = int.Parse(dtRecommendPoint.Rows[i]["PointNumber"].ToString()) * -1;
									this.modelPoint.PointChangeType = 12;
									this.modelPoint.PointRemark = "会员消费撤单推荐人积分变动";
									this.modelPoint.PointShopID = intUserShopID;
									this.modelPoint.PointCreateTime = DateTime.Now;
									this.modelPoint.PointUserID = intUserID;
									this.modelPoint.PointOrderCode = orderLog.OrderAccount;
									this.modelPoint.PointGiveMemID = intMemID;
									this.bllPoint.Add(this.modelPoint);
									this.bllPoint.MemPointRollback(int.Parse(dtRecommendPoint.Rows[i]["PointMemID"].ToString()), int.Parse(dtRecommendPoint.Rows[i]["PointNumber"].ToString()));
								}
							}
							if (mem > 0)
							{
								this.modelPoint.PointMemID = intMemID;
								this.modelPoint.PointNumber = this.modelOrderLog.OrderPoint * -1;
								if (this.modelOrderLog.OrderType == 2)
								{
									this.modelPoint.PointChangeType = 1;
								}
								else
								{
									this.modelPoint.PointChangeType = 2;
								}
								this.modelPoint.PointRemark = "会员消费撤单积分变动";
								this.modelPoint.PointShopID = intUserShopID;
								this.modelPoint.PointCreateTime = DateTime.Now;
								this.modelPoint.PointUserID = intUserID;
								this.modelPoint.PointOrderCode = orderLog.OrderAccount;
								this.modelPoint.PointGiveMemID = 0;
								this.bllPoint.Add(this.modelPoint);
							}
							DataTable dtOrderLog = this.bllOrderLog.GetList(" OldAccount='" + this.modelOrderLog.OrderAccount + "' and OrderType=6 order by OrderCreateTime desc ").Tables[0];
							if (dtOrderLog.Rows.Count == 0)
							{
								DataTable dtStaffMoney = this.bllStaffMoney.GetList(" StaffOrderCode='" + this.modelOrderLog.OrderAccount + "'").Tables[0];
								if (dtStaffMoney.Rows.Count > 0)
								{
									for (int j = 0; j < dtStaffMoney.Rows.Count; j++)
									{
										this.modelStaffMoney.StaffID = Convert.ToInt32(dtStaffMoney.Rows[j]["StaffID"].ToString());
										this.modelStaffMoney.StaffTotalMoney = Convert.ToDecimal(dtStaffMoney.Rows[j]["StaffTotalMoney"].ToString()) * -1m;
										this.modelStaffMoney.StaffOrderCode = orderLog.OrderAccount;
										this.modelStaffMoney.StaffMemID = Convert.ToInt32(dtStaffMoney.Rows[j]["StaffMemID"].ToString());
										this.modelStaffMoney.StaffGoodsID = Convert.ToInt32(dtStaffMoney.Rows[j]["StaffGoodsID"].ToString());
										this.modelStaffMoney.StaffShopID = intUserShopID;
										this.modelStaffMoney.StaffCreateTime = DateTime.Now;
										this.modelStaffMoney.StaffOrderDetailID = Convert.ToInt32(dtStaffMoney.Rows[j]["StaffOrderDetailID"].ToString());
										this.bllStaffMoney.Add(this.modelStaffMoney);
									}
								}
							}
							this.modelMem = new Chain.BLL.Mem().GetModel(this.modelOrderLog.OrderMemID);
							strUpdateMemLevel = PubFunction.UpdateMemLevel(this.modelMem);
							this.modelOrderLog.OrderType = 5;
							this.bllOrderLog.Update(this.modelOrderLog);
							PubFunction.SaveSysLog(intUserID, 4, "会员消费", string.Concat(new string[]
							{
								"会员整单撤销,会员卡号：[",
								this.modelMem.MemCard,
								"],姓名:[",
								this.modelMem.MemName,
								"],订单号:[",
								this.modelOrderLog.OrderAccount,
								"]"
							}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
							flag = string.Concat(new object[]
							{
								"{\"Success\":\"",
								mem,
								"\",\"strUpdateMemLevel\":\"",
								strUpdateMemLevel,
								"\"}"
							});
							if (this.modelMem.MemMobile != "")
							{
								if (PubFunction.curParameter.bolSms && Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
								{
									SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
									smsTemplateParameter.strCardID = this.modelMem.MemCard;
									smsTemplateParameter.strName = this.modelMem.MemName;
									smsTemplateParameter.dclTempMoney = this.modelOrderLog.OrderDiscountMoney;
									smsTemplateParameter.dclMoney = this.modelMem.MemMoney;
									smsTemplateParameter.intTempPoint = this.modelOrderLog.OrderPoint;
									smsTemplateParameter.intPoint = this.modelMem.MemPoint;
									smsTemplateParameter.OldLevelID = this.modelMem.MemLevelID;
									this.modelMem = new Chain.BLL.Mem().GetModel(intMemID);
									smsTemplateParameter.NewLevelID = this.modelMem.MemLevelID;
									smsTemplateParameter.MemBirthday = this.modelMem.MemBirthday;
									smsTemplateParameter.MemPastTime = this.modelMem.MemPastTime;
									string strSendContent = SMSInfo.GetSendContent(8, smsTemplateParameter, intUserShopID);
									SMSInfo.Send_GXSMS(false, this.modelMem.MemMobile, strSendContent, "");
									Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
									modelSms.SmsMemID = this.modelMem.MemID;
									modelSms.SmsMobile = this.modelMem.MemMobile;
									modelSms.SmsContent = strSendContent;
									modelSms.SmsTime = DateTime.Now;
									modelSms.SmsShopID = intUserShopID;
									modelSms.SmsUserID = intUserID;
									modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
									modelSms.SmsAllAmount = modelSms.SmsAmount;
									Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
									bllSms.Add(modelSms);
									PubFunction.SaveSysLog(intUserID, 4, "会员消费-消费撤单", string.Concat(new string[]
									{
										"消费撤单发短信成功,会员卡号：[",
										this.modelMem.MemCard,
										"],姓名：[",
										this.modelMem.MemName,
										"]"
									}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
								}
							}
						}
						else
						{
							flag = "{\"Success\":\"" + -1 + "\",\"strUpdateMemLevel\":\"\"}";
						}
					}
				}
				else
				{
					Chain.Model.OrderLog orderLog = new Chain.Model.OrderLog();
					orderLog.OrderAccount = "CD" + DateTime.Now.ToString("yyMMddHHmmssffff");
					orderLog.OrderType = 4;
					orderLog.OrderMemID = this.modelMem.MemID;
					orderLog.OrderTotalMoney = this.modelOrderLog.OrderTotalMoney * -1m;
					orderLog.OrderDiscountMoney = this.modelOrderLog.OrderDiscountMoney * -1m;
					orderLog.OrderIsCard = this.modelOrderLog.OrderIsCard;
					orderLog.OrderPayCard = this.modelOrderLog.OrderPayCard * -1m;
					orderLog.OrderIsCash = this.modelOrderLog.OrderIsCash;
					orderLog.OrderPayCash = this.modelOrderLog.OrderPayCash * -1m;
					orderLog.OrderIsBink = this.modelOrderLog.OrderIsBink;
					orderLog.OrderPayBink = this.modelOrderLog.OrderPayBink * -1m;
					orderLog.OrderPayCoupon = this.modelOrderLog.OrderPayCoupon * -1m;
					orderLog.OrderPoint = this.modelOrderLog.OrderPoint * -1;
					orderLog.OrderRemark = "散客消费撤单";
					orderLog.OrderPayType = 0;
					orderLog.OrderShopID = intUserShopID;
					orderLog.OrderCreateTime = DateTime.Now;
					orderLog.OrderUserID = intUserID;
					orderLog.OldAccount = this.modelOrderLog.OrderAccount;
					orderLog.OrderCardBalance = 0m;
					orderLog.OrderCardPoint = 0;
					int addLog = this.bllOrderLog.Add(orderLog);
					if (addLog > 0)
					{
						if (this.modelOrderLog.OrderType == 2)
						{
							this.modelGoodsLog.GoodsAccount = this.modelOrderLog.OrderAccount;
							this.modelGoodsLog.Type = 5;
							this.modelGoodsLog.TotalPrice = this.modelOrderLog.OrderDiscountMoney;
							this.modelGoodsLog.Remark = "商品撤单入库";
							this.modelGoodsLog.CreateTime = DateTime.Now;
							this.modelGoodsLog.ShopID = intUserShopID;
							this.modelGoodsLog.UserID = intUserID;
							this.modelGoodsLog.ChangeShopID = this.modelOrderLog.OrderShopID;
							int intLog = this.bllGoodsLog.Add(this.modelGoodsLog);
							DataTable dtOrder = this.bllDetail.GetList(" OrderID=" + intOrderID).Tables[0];
							for (int i = 0; i < dtOrder.Rows.Count; i++)
							{
								this.modelGoods = this.bllGoods.GetModel(int.Parse(dtOrder.Rows[i]["GoodsID"].ToString()));
								if (this.modelGoods.GoodsType == 0)
								{
									this.modelDetail.OrderID = addLog;
									this.modelDetail.GoodsID = int.Parse(dtOrder.Rows[i]["GoodsID"].ToString());
									this.modelDetail.OrderDetailPrice = decimal.Parse(dtOrder.Rows[i]["OrderDetailPrice"].ToString());
									this.modelDetail.OrderDetailPoint = int.Parse(dtOrder.Rows[i]["OrderDetailPoint"].ToString());
									this.modelDetail.OrderDetailDiscountPrice = decimal.Parse(dtOrder.Rows[i]["OrderDetailDiscountPrice"].ToString());
									this.modelDetail.OrderDetailNumber = decimal.Parse(dtOrder.Rows[i]["OrderDetailNumber"].ToString());
									if (this.bllDetail.Add(this.modelDetail) > 0)
									{
										this.modelNumber.GoodsID = int.Parse(dtOrder.Rows[i]["GoodsID"].ToString());
										this.modelNumber.ShopID = this.modelOrderLog.OrderShopID;
										this.modelNumber.Number = int.Parse(dtOrder.Rows[i]["OrderDetailNumber"].ToString());
										this.bllNumber.UpdataGoodsNumber(this.modelNumber);
										this.modelGoodsDetail.GoodsLogID = intLog;
										this.modelGoodsDetail.GoodsID = this.modelNumber.GoodsID;
										this.modelGoodsDetail.GoodsInPrice = 0m;
										this.modelGoodsDetail.GoodsOutPrice = 0m;
										this.modelGoodsDetail.GoodsNumber = this.modelNumber.Number;
										this.bllGoodsDetail.Add(this.modelGoodsDetail);
										PubFunction.SaveSysLog(intUserID, 4, "散客消费撤单", string.Concat(new object[]
										{
											"散客商品撤单，商品名称[",
											this.modelGoods.Name,
											"],数量[",
											this.modelNumber.Number,
											"]"
										}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
									}
									else
									{
										flag = "{\"Success\":\"" + -1 + "\",\"strUpdateMemLevel\":\"\"}";
									}
								}
							}
						}
					}
					DataTable dtStaffMoney = this.bllStaffMoney.GetList(" StaffOrderCode='" + this.modelOrderLog.OrderAccount + "'").Tables[0];
					if (dtStaffMoney.Rows.Count > 0)
					{
						for (int j = 0; j < dtStaffMoney.Rows.Count; j++)
						{
							this.modelStaffMoney.StaffID = Convert.ToInt32(dtStaffMoney.Rows[j]["StaffID"].ToString());
							this.modelStaffMoney.StaffTotalMoney = Convert.ToDecimal(dtStaffMoney.Rows[j]["StaffTotalMoney"].ToString()) * -1m;
							this.modelStaffMoney.StaffOrderCode = orderLog.OrderAccount;
							this.modelStaffMoney.StaffMemID = Convert.ToInt32(dtStaffMoney.Rows[j]["StaffMemID"].ToString());
							this.modelStaffMoney.StaffGoodsID = Convert.ToInt32(dtStaffMoney.Rows[j]["StaffGoodsID"].ToString());
							this.modelStaffMoney.StaffShopID = intUserShopID;
							this.modelStaffMoney.StaffCreateTime = DateTime.Now;
							this.modelStaffMoney.StaffOrderDetailID = Convert.ToInt32(dtStaffMoney.Rows[j]["StaffOrderDetailID"].ToString());
							this.bllStaffMoney.Add(this.modelStaffMoney);
						}
					}
					this.modelOrderLog.OrderType = 5;
					this.bllOrderLog.Update(this.modelOrderLog);
					PubFunction.SaveSysLog(intUserID, 4, "散客消费", "散客整单撤销,订单号:[" + this.modelOrderLog.OrderAccount + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
					flag = string.Concat(new object[]
					{
						"{\"Success\":\"",
						1,
						"\",\"strUpdateMemLevel\":\"",
						strUpdateMemLevel,
						"\"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "{\"Success\":\"" + -1 + "\",\"strUpdateMemLevel\":\"\"}";
			}
			this.Context.Response.Write(flag);
		}

		public void GetOrderDetail()
		{
			string order = "";
			int intOrderID = int.Parse(this.Request["orderID"].ToString());
			Chain.Model.OrderLog modelOrder = new Chain.BLL.OrderLog().GetModel(intOrderID);
			DataSet dsResults = this.bllDetail.GetDetail(intOrderID);
			DataTable dtGoods = dsResults.Tables[0];
			DataTable dtAllnumber = dsResults.Tables[1];
			if (dtGoods != null)
			{
				order = JsonPlus.ToJson(dtGoods, "");
			}
			string msgResponse = string.Concat(new object[]
			{
				"{\"order\":",
				order,
				",\"AllNumber\":",
				dtAllnumber.Rows[0]["AllNumber"],
				"}"
			});
			this.Context.Response.Write(msgResponse);
		}

		public void TimeExpenseStart()
		{
			int strFlag = 0;
			string strReturn = "系统异常，未保存数据，请再次点击保存！";
			try
			{
				int Project = Convert.ToInt32(this.Request["Project"]);
				int RulesID = new Chain.BLL.TimingProject().GetModel(Project).ProjectRulesID;
				string strToken = this.Request["Token"].ToString();
				bool bolIsMem = bool.Parse(this.Request["isMem"].ToString());
				DataTable dtTiemExpense = this.bllOrderTime.GetList(" OrderState=0 and OrderToken='" + strToken + "'").Tables[0];
				if (dtTiemExpense.Rows.Count > 0)
				{
					strFlag = -2;
					strReturn = "有尚未结束的计时消费记录，请先结束当前的计时消费项目再进行新的计时消费！";
				}
				else if (bolIsMem)
				{
					this.modelMem = this.bllMem.GetModelByMemCard(strToken);
					if (this.modelMem != null)
					{
						this.modelOrderTime.OrderMemID = this.modelMem.MemID;
						strReturn = string.Concat(new string[]
						{
							"计时消费开始，会员卡号：",
							this.modelMem.MemCard,
							",会员姓名：",
							this.modelMem.MemName,
							",计时消费开始时间：",
							DateTime.Now.ToString()
						});
						this.modelOrderTime.OrderTimeCode = PubFunction.curParameter.strTimeExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
						this.modelOrderTime.OrderToken = strToken;
						this.modelOrderTime.OrderMarchTime = DateTime.Now;
						this.modelOrderTime.OrderState = false;
						this.modelOrderTime.OrderPredictTime = ((this.Request["OrderPredictTime"] == "") ? 0m : Convert.ToDecimal(this.Request["OrderPredictTime"]));
						this.modelOrderTime.OrderOutTime = DateTime.Now;
						this.modelOrderTime.OrderShopID = this.UserModel.UserShopID;
						this.modelOrderTime.OrderStartUserID = this.UserModel.UserID;
						this.modelOrderTime.OrderCreateTime = DateTime.Now;
						this.modelOrderTime.OrderProjectID = Project;
						this.modelOrderTime.OrderRulesID = RulesID;
						strFlag = this.bllOrderTime.Add(this.modelOrderTime);
					}
					else
					{
						strFlag = -3;
						strReturn = "没有此会员卡号，请重新输入！";
					}
				}
				else
				{
					this.modelOrderTime.OrderMemID = 0;
					strReturn = "计时消费开始，散客令牌：" + strToken + ",计时开始时间：" + DateTime.Now.ToString();
					this.modelOrderTime.OrderTimeCode = PubFunction.curParameter.strTimeExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
					this.modelOrderTime.OrderToken = strToken;
					this.modelOrderTime.OrderMarchTime = DateTime.Now;
					this.modelOrderTime.OrderState = false;
					this.modelOrderTime.OrderPredictTime = ((this.Request["OrderPredictTime"] == "") ? 0m : Convert.ToDecimal(this.Request["OrderPredictTime"]));
					this.modelOrderTime.OrderOutTime = DateTime.Now;
					this.modelOrderTime.OrderShopID = this.UserModel.UserShopID;
					this.modelOrderTime.OrderStartUserID = this.UserModel.UserID;
					this.modelOrderTime.OrderCreateTime = DateTime.Now;
					this.modelOrderTime.OrderProjectID = Project;
					this.modelOrderTime.OrderRulesID = RulesID;
					strFlag = this.bllOrderTime.Add(this.modelOrderTime);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				strFlag = -1;
				strReturn = "系统错误 请与系统管理员联系！";
			}
			string msgResponse = string.Concat(new object[]
			{
				"{\"flag\":\"",
				strFlag,
				"\",\"strReturn\":\"",
				strReturn,
				"\"}"
			});
			this.Context.Response.Write(msgResponse);
		}

		public void GetTimeExpense()
		{
			string strFlag = "0";
			try
			{
				string strToken = this.Request["Token"].ToString();
				DataTable dtTiemExpense = this.bllOrderTime.GetList(" OrderState=0 and OrderToken='" + strToken + "'").Tables[0];
				if (dtTiemExpense.Rows.Count > 0)
				{
					this.modelOrderTime = this.bllOrderTime.GetModel(int.Parse(dtTiemExpense.Rows[0]["OrderTimeID"].ToString()));
					this.modelMem = this.bllMem.GetModel(this.modelOrderTime.OrderMemID);
					strFlag = "1";
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				strFlag = "-1";
			}
			string msgResponse = string.Concat(new string[]
			{
				"{\"flag\":\"",
				strFlag,
				"\",\"OrderCode\":\"",
				this.modelOrderTime.OrderTimeCode,
				"\",\"memCard\":\"",
				this.modelMem.MemCard,
				"\"}"
			});
			this.Context.Response.Write(msgResponse);
		}

		public void SecondPrinting()
		{
			string msgResponse = "";
			string strExpenseDetail = "";
			int intOrderID = int.Parse(this.Request["orderID"]);
			int intOrderType = int.Parse(this.Request["orderType"]);
			int intMemID = int.Parse(this.Request["memID"]);
			StringBuilder strSql = new StringBuilder();
			DataTable dtExpense = this.bllOrderLog.GetList(" OrderID=" + intOrderID).Tables[0];
			if (dtExpense != null)
			{
				string strExpense = JsonPlus.ToJson(dtExpense, "");
				DataTable dtMem = this.bllMem.GetList(" MemID=" + intMemID).Tables[0];
				if (dtMem != null)
				{
					string strMem = JsonPlus.ToJson(dtMem, "MemID,MemCard,MemName,MemPoint,MemMoney");
					msgResponse = string.Concat(new object[]
					{
						"{\"orderType\":",
						intOrderType,
						",\"expense\":",
						strExpense,
						",\"mem\":",
						strMem,
						"}"
					});
					if (intOrderType == 2 || intOrderType == 7)
					{
						DataTable dtDetail = this.bllDetail.GetOrderDetail(" OrderDetail.OrderID=" + intOrderID).Tables[0];
						if (dtDetail != null)
						{
							strExpenseDetail = JsonPlus.ToJson(dtDetail, "");
						}
						msgResponse = string.Concat(new object[]
						{
							"{\"orderType\":",
							intOrderType,
							",\"expense\":",
							strExpense,
							",\"mem\":",
							strMem,
							",\"expenseDetail\":",
							strExpenseDetail,
							"}"
						});
					}
				}
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetExpenseIsShow()
		{
			int flag = 0;
			int orderID = (!string.IsNullOrEmpty(this.Request["id"])) ? Convert.ToInt32(this.Request["id"].ToString()) : 0;
			if (orderID != 0)
			{
				DataTable dt = new Chain.BLL.OrderDetail().GetList(" OrderID=" + orderID).Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = 1;
				}
			}
			this.Context.Response.Write(flag);
		}

		public void GetPointIsShow()
		{
			int flag = 0;
			int pointMemID = (!string.IsNullOrEmpty(this.Request["id"])) ? Convert.ToInt32(this.Request["id"].ToString()) : 0;
			if (pointMemID != 0)
			{
				DataTable dt = new Chain.BLL.PointLog().GetList(" PointMemID=" + pointMemID + " and PointChangeType=9 ").Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = 1;
				}
			}
			this.Context.Response.Write(flag);
		}

		public void GetStaffIsShow()
		{
			int flag = 0;
			int staffID = (!string.IsNullOrEmpty(this.Request["id"])) ? Convert.ToInt32(this.Request["id"].ToString()) : 0;
			string startTime = (!string.IsNullOrEmpty(this.Request["startTime"])) ? this.Request["startTime"].ToString() : DateTime.Now.AddYears(-50).ToString("yyyy-MM-dd");
			string endTime = (!string.IsNullOrEmpty(this.Request["endTime"])) ? DateTime.Parse(this.Request["endTime"].ToString()).AddDays(1.0).ToString("yyyy-MM-dd") : DateTime.Now.AddYears(50).ToString("yyyy-MM-dd");
			if (staffID != 0)
			{
				DataTable dt = new Chain.BLL.StaffMoney().GetList(string.Concat(new object[]
				{
					" StaffID=",
					staffID,
					" and StaffCreateTime>='",
					startTime,
					"' and StaffCreateTime<='",
					endTime,
					"'"
				})).Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = 1;
				}
			}
			this.Context.Response.Write(flag);
		}

		public void GetGoodsLogIsShow()
		{
			int flag = 0;
			int goodsLogID = (!string.IsNullOrEmpty(this.Request["id"])) ? Convert.ToInt32(this.Request["id"].ToString()) : 0;
			if (goodsLogID != 0)
			{
				DataTable dt = new Chain.BLL.GoodsLogDetail().GetList(" GoodsLogID=" + goodsLogID).Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = 1;
				}
			}
			this.Context.Response.Write(flag);
		}

		public void GetGoodsLogIsShows()
		{
			int flag = 0;
			int AllotDetailAllotID = (!string.IsNullOrEmpty(this.Request["id"])) ? Convert.ToInt32(this.Request["id"].ToString()) : 0;
			if (AllotDetailAllotID != 0)
			{
				DataTable dt = new Chain.BLL.GoodsAllotDetail().GetList(" AllotDetailAllotID=" + AllotDetailAllotID).Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = 1;
				}
			}
			this.Context.Response.Write(flag);
		}

		public void SendMessage()
		{
			string strReceiver = (this.Request["strReceiver"] != null) ? this.Request["strReceiver"].ToString() : "";
			string strContent = (this.Request["strContent"] != null) ? this.Request["strContent"].ToString() : "";
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			if (strReceiver.Substring(strReceiver.Length - 1, 1) == ",")
			{
				strReceiver = strReceiver.Remove(strReceiver.LastIndexOf(","), 1);
			}
			int intFlag;
			if (int.Parse(SMSInfo.GetBalance(true).ToString()) > 0)
			{
				if (PubFunction.curParameter.bolIsSmsShopName)
				{
					if (PubFunction.curParameter.strSmsShopName != "")
					{
						strContent = strContent + "[" + PubFunction.curParameter.strSmsShopName + "]";
					}
				}
				else
				{
					Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
					Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
					modelShop = bllShop.GetModel(intUserShopID);
					if (modelShop.ShopSmsName != "")
					{
						strContent = strContent + "[" + modelShop.ShopSmsName + "]";
					}
				}
				if (!PubFunction.IsCanSendSms(this.UserModel.UserShopID, strReceiver.Split(new char[]
				{
					','
				}).Length))
				{
					intFlag = 5;
				}
				else if (SMSInfo.Send_SMS(true, strReceiver, strContent, ""))
				{
					intFlag = 1;
					Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
					modelSms.SmsMemID = 0;
					modelSms.SmsMobile = strReceiver;
					modelSms.SmsContent = strContent;
					modelSms.SmsTime = DateTime.Now;
					modelSms.SmsShopID = intUserShopID;
					modelSms.SmsUserID = intUserID;
					modelSms.SmsAmount = PubFunction.GetSmsAmount(strContent);
					modelSms.SmsAllAmount = modelSms.SmsAmount * strReceiver.Split(new char[]
					{
						','
					}).Length;
					Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
					bllSms.Add(modelSms);
					if (PubFunction.curParameter.bolShopSmsManage)
					{
						PubFunction.SetShopSms(intUserID, this.UserModel.UserShopID, strReceiver.Split(new char[]
						{
							','
						}).Length, 4);
					}
					PubFunction.SaveSysLog(intUserID, 4, "自定义短信", "自定义发送短信", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
				else
				{
					intFlag = 2;
				}
			}
			else
			{
				intFlag = 3;
			}
			this.Context.Response.Write(intFlag);
		}

		public void MemSendMessage()
		{
			string strMemReceiver = (this.Request["strMemReceiver"] != null) ? this.Request["strMemReceiver"].ToString() : "";
			string strMemContent = (this.Request["strMemContent"] != null) ? this.Request["strMemContent"].ToString() : "";
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			string[] memReceiverArray = Regex.Split(strMemReceiver, ";", RegexOptions.IgnoreCase);
			string strMemMobile = "";
			string[] array = memReceiverArray;
			for (int j = 0; j < array.Length; j++)
			{
				string i = array[j];
				if (i != "")
				{
					string[] memReceiver = Regex.Split(i, ":", RegexOptions.IgnoreCase);
					strMemMobile = strMemMobile + memReceiver[1] + ",";
				}
			}
			if (strMemMobile != "")
			{
				if (strMemMobile.Substring(strMemMobile.Length - 1, 1) == ",")
				{
					strMemMobile = strMemMobile.Remove(strMemMobile.LastIndexOf(","), 1);
				}
			}
			int intFlag;
			if (int.Parse(SMSInfo.GetBalance(true).ToString()) > 0)
			{
				if (PubFunction.curParameter.bolIsSmsShopName)
				{
					if (PubFunction.curParameter.strSmsShopName != "")
					{
						strMemContent = strMemContent + "[" + PubFunction.curParameter.strSmsShopName + "]";
					}
				}
				else
				{
					Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
					Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
					modelShop = bllShop.GetModel(intUserShopID);
					if (modelShop.ShopSmsName != "")
					{
						strMemContent = strMemContent + "[" + modelShop.ShopSmsName + "]";
					}
				}
				if (!PubFunction.IsCanSendSms(this.UserModel.UserShopID, strMemMobile.Split(new char[]
				{
					','
				}).Length))
				{
					intFlag = 5;
				}
				else if (SMSInfo.Send_SMS(true, strMemMobile, strMemContent, ""))
				{
					intFlag = 1;
					Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
					modelSms.SmsMemID = 0;
					modelSms.SmsMobile = strMemReceiver;
					modelSms.SmsContent = strMemContent;
					modelSms.SmsTime = DateTime.Now;
					modelSms.SmsShopID = intUserShopID;
					modelSms.SmsUserID = intUserID;
					modelSms.SmsAmount = PubFunction.GetSmsAmount(strMemContent);
					modelSms.SmsAllAmount = modelSms.SmsAmount * strMemMobile.Split(new char[]
					{
						','
					}).Length;
					Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
					bllSms.Add(modelSms);
					if (PubFunction.curParameter.bolShopSmsManage)
					{
						PubFunction.SetShopSms(intUserID, this.UserModel.UserShopID, strMemMobile.Split(new char[]
						{
							','
						}).Length, 4);
					}
					PubFunction.SaveSysLog(intUserID, 4, "发送短信", "给会员发送短信", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
				else
				{
					intFlag = 2;
				}
			}
			else
			{
				intFlag = 3;
			}
			this.Context.Response.Write(intFlag);
		}

		public void GetSmsTemplate()
		{
			string msgResponse = "";
			try
			{
				int intTemplateID = (this.Request["templateID"].ToString() != "") ? int.Parse(this.Request["templateID"].ToString()) : 0;
				Chain.BLL.SmsTemplate bllTemplate = new Chain.BLL.SmsTemplate();
				DataTable dtTemplate = bllTemplate.GetList("TemplateID=" + intTemplateID).Tables[0];
				if (dtTemplate != null)
				{
					msgResponse = JsonPlus.ToJson(dtTemplate, "TemplateID,TemplateName,TemplateContent");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void SmsTemplateAdd()
		{
			int flag = 0;
			try
			{
				string strTemplateName = this.Request["templateName"].ToString();
				string strTemplateContent = this.Request["templateContent"].ToString();
				int intShopID = this.UserModel.UserShopID;
				int intUserID = this.UserModel.UserID;
				Chain.Model.SmsTemplate modelTemplate = new Chain.Model.SmsTemplate();
				modelTemplate.TemplateName = strTemplateName;
				modelTemplate.TemplateContent = strTemplateContent;
				modelTemplate.TemplateShopID = intShopID;
				Chain.BLL.SmsTemplate bllTemplate = new Chain.BLL.SmsTemplate();
				flag = bllTemplate.Add(modelTemplate);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 1, "模板新增", "新增短信模板,模板名称：[" + modelTemplate.TemplateName + "]", intShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void SmsTemplateEdit()
		{
			int flag = 0;
			try
			{
				int strTemplateID = int.Parse(this.Request["templateID"].ToString());
				string strTemplateName = this.Request["templateName"].ToString();
				string strTemplateContent = this.Request["templateContent"].ToString();
				int intShopID = this.UserModel.UserShopID;
				int intUserID = this.UserModel.UserID;
				Chain.Model.SmsTemplate modelTemplate = new Chain.Model.SmsTemplate();
				modelTemplate.TemplateID = strTemplateID;
				modelTemplate.TemplateName = strTemplateName;
				modelTemplate.TemplateContent = strTemplateContent;
				Chain.BLL.SmsTemplate bllTemplate = new Chain.BLL.SmsTemplate();
				flag = bllTemplate.Update(modelTemplate);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 3, "模板编辑", "修改短信模板,模板名称：[" + modelTemplate.TemplateName + "]", intShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void SmsTemplateDelete()
		{
			int flag = 0;
			try
			{
				int intTemplateID = (this.Request["TemplateID"].ToString() != "") ? int.Parse(this.Request["TemplateID"].ToString()) : 0;
				int intShopID = this.UserModel.UserShopID;
				int intUserID = this.UserModel.UserID;
				Chain.BLL.SmsTemplate bllTemplate = new Chain.BLL.SmsTemplate();
				Chain.Model.SmsTemplate modelTemplate = new Chain.Model.SmsTemplate();
				modelTemplate = bllTemplate.GetModel(intTemplateID);
				if (bllTemplate.Delete(intTemplateID))
				{
					PubFunction.SaveSysLog(intUserID, 2, "模板删除", "删除短信模板,模板名称：[" + modelTemplate.TemplateName + "]", intShopID, DateTime.Now, PubFunction.ipAdress);
					flag = 1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void GetSmsBalance()
		{
			bool bolSMSType = !string.IsNullOrEmpty(this.Request["type"]) && bool.Parse(this.Request["type"].ToString());
			string strBalance = SMSInfo.GetBalance(bolSMSType);
			string flag = string.Concat(new object[]
			{
				"{\"smsType\":\"",
				bolSMSType,
				"\",\"number\":\"",
				strBalance,
				"\"}"
			});
			this.Context.Response.Write(flag);
		}

		public void GetMMSBalance()
		{
			string strMMSSeries = this.Request["MMSSeries"].ToString();
			string strMMSSerialPwd = this.Request["MMSSerialPwd"].ToString();
			string strBalance = WebTest.SendMessage.GetBalance(strMMSSeries, strMMSSerialPwd);
			this.Context.Response.Write(strBalance);
		}

		public void GetMemByMobile()
		{
			string modile = this.Request["mobile"];
			StringBuilder sql = new StringBuilder();
			sql.AppendFormat(" MemMobile='{0}' or MemMobile='{1}'", modile, modile.TrimStart(new char[]
			{
				'0'
			}));
			DataTable dt = new Chain.BLL.Mem().GetList(sql.ToString()).Tables[0];
			int flag;
			string RealModile;
			if (dt.Rows.Count > 0)
			{
				flag = 1;
				RealModile = dt.Rows[0]["MemMobile"].ToString();
			}
			else
			{
				flag = -1;
				RealModile = modile;
			}
			string strResponse = string.Concat(new object[]
			{
				"{\"flag\":\"",
				flag,
				"\",\"RealModile\":\"",
				RealModile,
				"\"}"
			});
			this.Context.Response.Write(strResponse);
		}

		public void CarllerMem()
		{
			int flag = 0;
			try
			{
				string strMobile = this.Request["Mobile"];
				int intMemID = (this.Request["MemID"] != "") ? int.Parse(this.Request["MemID"]) : 0;
				string strState = this.Request["State"];
				string strTime = (this.Request["Time"] != "") ? this.Request["Time"] : "00:00";
				string strRemark = (this.Request["Remark"] != "") ? this.Request["Remark"] : "无";
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.Model.ScreenPopUp modelScreenPopUp = new Chain.Model.ScreenPopUp();
				modelScreenPopUp.CallerMemID = intMemID;
				if (intMemID != 0)
				{
					modelScreenPopUp.CallerIsMem = "会员";
				}
				else
				{
					modelScreenPopUp.CallerIsMem = "非会员";
				}
				if (strState == "未接来电.....")
				{
					strState = "未接来电";
				}
				else if (strState == "本地电话机挂机.....")
				{
					strState = "已接来电";
				}
				if (strState == "来电振铃中....." && strTime == "暂未接通")
				{
					strState = "未接来电";
				}
				if (strTime == "暂未接通")
				{
					strTime = "00:00";
				}
				modelScreenPopUp.CallerMobile = strMobile;
				modelScreenPopUp.CallerState = strState;
				modelScreenPopUp.CallerDuration = strTime;
				modelScreenPopUp.CallerRemark = strRemark;
				modelScreenPopUp.CallerCreateTime = DateTime.Now;
				modelScreenPopUp.CallerUserID = intUserID;
				modelScreenPopUp.CallerShopID = intUserShopID;
				new Chain.BLL.ScreenPopUp().Add(modelScreenPopUp);
				PubFunction.SaveSysLog(intUserID, 4, "来电弹屏", modelScreenPopUp.CallerIsMem + "来电,来电时间:" + modelScreenPopUp.CallerCreateTime, intUserShopID, DateTime.Now, PubFunction.ipAdress);
				flag = 1;
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void CouponAdd()
		{
			int flag = 0;
			try
			{
				Chain.BLL.Coupon couponInfo = new Chain.BLL.Coupon();
				Chain.Model.Coupon model = new Chain.Model.Coupon();
				int spCouponID = int.Parse(this.Request["spCouponID"]);
				int CouponPredictNu = int.Parse(this.Request["CouponPredictNu"]);
				int CouponType = int.Parse(this.Request["CouponType"]);
				int CouponDayNum = int.Parse(this.Request["CouponDayNum"]);
				int CouponEffective = int.Parse(this.Request["CouPonYX"]);
				decimal CouponNumber = decimal.Parse(this.Request["CouponNumber"]);
				decimal CouponMinMoney = decimal.Parse(this.Request["CouponMinMoney"]);
				string CouponTitle = this.Request["CouponTitle"];
				string CouponContent = this.Request["CouponContent"];
				int intShopID = this.UserModel.UserShopID;
				int intUserID = this.UserModel.UserID;
				int IsGet = int.Parse(this.Request["IsGet"]);
				if (this.Request["CouPonStart"] != "")
				{
					DateTime CouponStart = DateTime.Parse(this.Request["CouPonStart"]);
					model.CouponStart = new DateTime?(CouponStart);
				}
				if (this.Request["CouPonEnd"] != "")
				{
					DateTime CouponEnd = DateTime.Parse(this.Request["CouPonEnd"]);
					model.CouponEnd = new DateTime?(CouponEnd);
				}
				model.CouponTitle = CouponTitle;
				model.IsGet = IsGet;
				model.CouponType = CouponType;
				model.CouponNumber = CouponNumber;
				model.CouponPredictNu = CouponPredictNu;
				model.CouponMinMoney = CouponMinMoney;
				model.CouponDayNum = CouponDayNum;
				model.CouponEffective = CouponEffective;
				model.CouponContent = CouponContent;
				model.CouponYF = 0;
				model.CouponSY = 0;
				model.CouponShopID = intShopID;
				flag = couponInfo.Add(model);
				if (flag > 0)
				{
					Chain.BLL.CouponList cdtl = new Chain.BLL.CouponList();
					Chain.Model.CouponList clist = new Chain.Model.CouponList();
					for (int i = 0; i < CouponPredictNu; i++)
					{
						clist.CouPon = PubFunction.CreateRandomNumber(5);
						clist.CouPonID = flag;
						cdtl.Add(clist);
					}
					PubFunction.SaveSysLog(intUserID, 1, "优惠券新增", string.Concat(new object[]
					{
						"新增电子优惠券,名称：[",
						CouponTitle,
						"],预发：",
						CouponPredictNu,
						"张"
					}), intShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GetCoupon()
		{
			string msgResponse = "";
			try
			{
				int intCouponID = (this.Request["CouponID"].ToString() != "") ? int.Parse(this.Request["CouponID"].ToString()) : 0;
				Chain.BLL.Coupon bllCoupon = new Chain.BLL.Coupon();
				DataTable dtCoupon = bllCoupon.GetList(" ID=" + intCouponID).Tables[0];
				if (dtCoupon != null)
				{
					msgResponse = JsonPlus.ToJson(dtCoupon, "ID,CouponTitle,CouponType,CouponNumber,CouponPredictNu,CouponEffective,CouponStart,CouponEnd,CouponDayNum,CouponMinMoney,CouponContent,CouponYF,CouponSY");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetCouponDetail()
		{
			string msgResponse = "";
			try
			{
				string strCoupon = this.Request["CouPon"].ToString();
				string strMemID = this.Request["MemID"].ToString();
				Chain.BLL.CouponList bllCouponList = new Chain.BLL.CouponList();
				DataTable dtCoupon = bllCouponList.GetCouponDetail(string.Concat(new string[]
				{
					"CouPonMID = ",
					strMemID,
					" AND  CouPonID=ID and CouponList.CouPonYF='True' and CouPon='",
					strCoupon,
					"'"
				})).Tables[0];
				if (dtCoupon != null)
				{
					int msgResult = 0;
					DataTable dtUse = bllCouponList.GetCouponDetail(string.Concat(new string[]
					{
						" CouPonID=ID and CouPonMID=",
						strMemID,
						" and CouPonID=",
						dtCoupon.Rows[0]["CouPonID"].ToString(),
						" and datediff(day,ConPonUseTime,getdate())=0"
					})).Tables[0];
					int intNumber = int.Parse(dtCoupon.Rows[0]["CouponDayNum"].ToString());
					if (intNumber != 0 && dtUse.Rows.Count >= intNumber)
					{
						msgResult = 1;
					}
					string msgData = JsonPlus.ToJson(dtCoupon, "ID,CouponTitle,CouponType,CouponNumber,CouponPredictNu,CouponEffective,CouponStart,CouponEnd,CouponDayNum,CouponMinMoney,CouponContent,CID,CouPonID,CouPon,CouPonYF,CouPonSY,CouPonMID,ConPonSendTime,ConPonUseTime,CouPonOrderAccount,MemName");
					msgResponse = string.Concat(new object[]
					{
						"{\"msgResult\":",
						msgResult,
						",\"msgData\":",
						msgData,
						"}"
					});
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void BindCoupon()
		{
			int flag = 0;
			try
			{
				string strMemID = this.Request["MemID"].ToString();
				int intNumber = (this.Request["Number"].ToString() != "") ? int.Parse(this.Request["Number"].ToString()) : 0;
				int intCouponID = (this.Request["CouponID"].ToString() != "") ? int.Parse(this.Request["CouponID"].ToString()) : 0;
				int intUserShopID = this.UserModel.UserShopID;
				int intUserID = this.UserModel.UserID;
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
				Chain.BLL.Coupon bllCoupon = new Chain.BLL.Coupon();
				Chain.Model.Coupon modelCoupon = new Chain.Model.Coupon();
				modelCoupon = bllCoupon.GetModel(intCouponID);
				if (modelCoupon.CouponPredictNu < intNumber)
				{
					flag = -2;
				}
				else
				{
					Chain.BLL.CouponList bllCouponList = new Chain.BLL.CouponList();
					DataTable dtCouponList = bllCouponList.GetList(intNumber, " CouPonID=" + intCouponID + " and CouPonYF='False' ", " CID ").Tables[0];
					List<Chain.Model.CouponList> listCoupon = bllCouponList.DataTableToList(dtCouponList);
					listCoupon.Sort((Chain.Model.CouponList p1, Chain.Model.CouponList p2) => Comparer<int>.Default.Compare(p1.CID, p2.CID));
					ArrayList arraySql = new ArrayList();
					string[] strMemArray = Regex.Split(strMemID, ",", RegexOptions.IgnoreCase);
					for (int i = 0; i < intNumber; i++)
					{
						if (strMemArray[i] != "")
						{
							string strSql = "update CouponList set CouPonYF='True',CouPonMID=" + strMemArray[i] + ",ConPonSendTime='" + DateTime.Now.ToString();
							strSql = strSql + "' where Coupon='" + listCoupon[i].CouPon + "';";
							arraySql.Add(strSql);
						}
					}
					if (bllCouponList.DataUpdateTran(arraySql))
					{
						modelCoupon.CouponYF += intNumber;
						bllCoupon.Update(modelCoupon);
						flag = 1;
						PubFunction.SaveSysLog(intUserID, 4, "优惠券", string.Concat(new object[]
						{
							"批量发放优惠券，发送",
							intNumber,
							"人，优惠去名称[",
							modelCoupon.CouponTitle,
							"]"
						}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void SendCoupon()
		{
			int flag = 0;
			try
			{
				string strMemID = this.Request["MemID"].ToString();
				int intNumber = (this.Request["Number"].ToString() != "") ? int.Parse(this.Request["Number"].ToString()) : 0;
				int intCouponID = (this.Request["CouponID"].ToString() != "") ? int.Parse(this.Request["CouponID"].ToString()) : 0;
				int intUserShopID = this.UserModel.UserShopID;
				int intUserID = this.UserModel.UserID;
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(intUserShopID);
				Chain.BLL.Coupon bllCoupon = new Chain.BLL.Coupon();
				Chain.Model.Coupon modelCoupon = new Chain.Model.Coupon();
				modelCoupon = bllCoupon.GetModel(intCouponID);
				if (int.Parse(SMSInfo.GetBalance(false)) < intNumber)
				{
					flag = -1;
				}
				else if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, strMemID.Split(new char[]
				{
					','
				}).Length))
				{
					if (modelCoupon.CouponPredictNu < intNumber)
					{
						flag = -2;
					}
					else
					{
						StringBuilder strCouponContent = new StringBuilder(modelCoupon.CouponContent);
						strCouponContent.Replace("{商家}", modelShop.ShopName);
						strCouponContent.Replace("{优惠券}", modelCoupon.CouponTitle);
						string strTime;
						if (modelCoupon.CouponEnd.HasValue)
						{
							strTime = DateTime.Parse(modelCoupon.CouponEnd.ToString()).ToShortDateString();
						}
						else
						{
							strTime = "永久";
						}
						strCouponContent.Replace("{有效期}", strTime);
						string strCouponMoney = Math.Round(modelCoupon.CouponMinMoney, 2).ToString();
						strCouponContent.Replace("{最低消费}", strCouponMoney);
						strCouponContent.Replace("{店面电话}", modelShop.ShopTelephone);
						if (PubFunction.curParameter.bolIsSmsShopName)
						{
							if (PubFunction.curParameter.strSmsShopName != "")
							{
								strCouponContent.Append("[" + PubFunction.curParameter.strSmsShopName + "]");
							}
						}
						else if (modelShop.ShopSmsName != "")
						{
							strCouponContent.Append("[" + modelShop.ShopSmsName + "]");
						}
						Chain.BLL.CouponList bllCouponList = new Chain.BLL.CouponList();
						DataTable dtCouponList = bllCouponList.GetList(intNumber, " CouPonID=" + intCouponID + " and CouPonYF='False' ", " CID ").Tables[0];
						List<Chain.Model.CouponList> listCoupon = bllCouponList.DataTableToList(dtCouponList);
						listCoupon.Sort((Chain.Model.CouponList p1, Chain.Model.CouponList p2) => Comparer<int>.Default.Compare(p1.CID, p2.CID));
						ArrayList arraySql = new ArrayList();
						string[] strMemArray = Regex.Split(strMemID, ",", RegexOptions.IgnoreCase);
						for (int i = 0; i < intNumber; i++)
						{
							if (strMemArray[i] != "")
							{
								StringBuilder str = new StringBuilder(strCouponContent.ToString());
								str.Replace("{券号}", listCoupon[i].CouPon);
								Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(int.Parse(strMemArray[i]));
								if (SMSInfo.Send_GXSMS(false, modelMem.MemMobile, str.ToString(), ""))
								{
									Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
									modelSms.SmsMemID = modelMem.MemID;
									modelSms.SmsMobile = modelMem.MemMobile;
									modelSms.SmsContent = str.ToString();
									modelSms.SmsTime = DateTime.Now;
									modelSms.SmsShopID = intUserShopID;
									modelSms.SmsUserID = intUserID;
									modelSms.SmsAmount = PubFunction.GetSmsAmount(str.ToString());
									modelSms.SmsAllAmount = modelSms.SmsAmount;
									Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
									bllSms.Add(modelSms);
									string strSql = "update CouponList set CouPonYF='True',CouPonMID=" + strMemArray[i] + ",ConPonSendTime='" + DateTime.Now.ToString();
									strSql = strSql + "' where Coupon='" + listCoupon[i].CouPon + "';";
									arraySql.Add(strSql);
								}
							}
						}
						if (bllCouponList.DataUpdateTran(arraySql))
						{
							modelCoupon.CouponYF += intNumber;
							bllCoupon.Update(modelCoupon);
							flag = 1;
							PubFunction.SaveSysLog(intUserID, 4, "优惠券", string.Concat(new object[]
							{
								"批量发放优惠券，发送",
								intNumber,
								"人，优惠去名称[",
								modelCoupon.CouponTitle,
								"]"
							}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
					if (PubFunction.curParameter.bolShopSmsManage)
					{
						PubFunction.SetShopSms(intUserID, this.UserModel.UserShopID, strMemID.Split(new char[]
						{
							','
						}).Length, 4);
					}
				}
				else
				{
					flag = -5;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void UpdateCouponList()
		{
			int flag = 0;
			try
			{
				string strCoupon = this.Request["Coupon"].ToString();
				string strOrderAccount = this.Request["OrderAccount"].ToString();
				Chain.BLL.CouponList bllCouponList = new Chain.BLL.CouponList();
				Chain.Model.CouponList modelCoupon = bllCouponList.GetModel(strCoupon);
				modelCoupon.CouPonSY = true;
				modelCoupon.ConPonUseTime = DateTime.Now;
				modelCoupon.CouPonOrderAccount = strOrderAccount;
				if (bllCouponList.Update(modelCoupon))
				{
					flag = 1;
					Chain.BLL.Coupon bllCoupon = new Chain.BLL.Coupon();
					Chain.Model.Coupon mdCoupon = bllCoupon.GetModel(modelCoupon.CouPonID);
					mdCoupon.CouponSY++;
					bllCoupon.Update(mdCoupon);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void GetCouponMoney()
		{
			string msgResponse = "";
			try
			{
				int intOrderID = int.Parse(this.Request["orderID"].ToString());
				this.modelOrderLog = this.bllOrderLog.GetModel(intOrderID);
				msgResponse = "{\"couponMoney\":" + this.modelOrderLog.OrderPayCoupon + "}";
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void SendEmail()
		{
			int intFlag = 0;
			string strMemEmail = (this.Request["strMemEmail"] != null) ? this.Request["strMemEmail"].ToString() : "";
			string customEmail = (this.Request["customEmail"] != null) ? this.Request["customEmail"].ToString() : "";
			string strTitle = (this.Request["strTitle"] != null) ? this.Request["strTitle"].ToString() : "";
			string strMemContent = (this.Request["strMemContent"] != null) ? this.Request["strMemContent"].ToString() : "";
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			Chain.Model.EmailLog modelEmail = new Chain.Model.EmailLog();
			Chain.BLL.EmailLog bllEmail = new Chain.BLL.EmailLog();
			try
			{
				string strEmail = "";
				if (strMemEmail != "")
				{
					string[] MemEmailArray = Regex.Split(strMemEmail, ";", RegexOptions.IgnoreCase);
					string[] array = MemEmailArray;
					for (int k = 0; k < array.Length; k++)
					{
						string i = array[k];
						if (i != "")
						{
							string[] MemEmail = Regex.Split(i, ":", RegexOptions.IgnoreCase);
							strEmail = strEmail + MemEmail[1] + ",";
						}
					}
				}
				if (customEmail != "")
				{
					if (customEmail.Substring(customEmail.Length - 1, 1) == ",")
					{
						customEmail = customEmail.Remove(customEmail.LastIndexOf(","), 1);
					}
					strEmail += customEmail;
				}
				else
				{
					strEmail = strEmail.Remove(strEmail.LastIndexOf(","), 1);
				}
				string[] Email = strEmail.Split(new char[]
				{
					','
				});
				for (int j = 0; j < Email.Length; j++)
				{
					modelEmail.EmailAdress = Email[j];
					modelEmail.EmailTitle = strTitle;
					modelEmail.EmailContent = strMemContent;
					modelEmail.EmailState = 0;
					modelEmail.EmailSendTime = DateTime.Now;
					modelEmail.EmailShopID = intUserShopID;
					modelEmail.EmailUserID = intUserID;
					modelEmail.EmailCount = 0;
					intFlag = bllEmail.Add(modelEmail);
				}
			}
			catch
			{
				intFlag = -1;
			}
			this.Context.Response.Write(intFlag);
		}

		public void EmailResend()
		{
			int intEmailID = (!string.IsNullOrEmpty(this.Request["emailID"])) ? Convert.ToInt32(this.Request["emailID"].ToString()) : 0;
			int intUserID = this.UserModel.UserID;
			int intUserShopID = this.UserModel.UserShopID;
			if (intEmailID != 0)
			{
				Chain.Model.EmailLog modelEmail = new Chain.BLL.EmailLog().GetModel(intEmailID);
				if (new Chain.BLL.EmailLog().EmailResend(intEmailID) > 0)
				{
					PubFunction.SaveSysLog(intUserID, 4, "邮件发送", "邮件重新发送成功，邮箱地址为[" + modelEmail.EmailAdress + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
		}

		public void GetRptTotal()
		{
			string flag = "0";
			try
			{
				string strUserName = "";
				DateTime time = DateTime.Now;
				DateTime time2 = DateTime.Now;
				string checkRadion = (this.Request["checkRadion"] != null) ? this.Request["checkRadion"].ToString() : "";
				string timeStart = (this.Request["txttimeStart"] != null) ? this.Request["txttimeStart"].ToString() : "";
				string timeEnd = (this.Request["txttimeEnd"] != null) ? this.Request["txttimeEnd"].ToString() : "";
				string shopID = (this.Request["ShopID"] != null) ? this.Request["ShopID"].ToString() : "";
				string userID = (this.Request["UserID"] != null) ? this.Request["UserID"].ToString() : "";
				int sysShopId = this.UserModel.UserShopID;
				int sysUserId = this.UserModel.UserID;
				if (userID == "" || userID == null)
				{
					strUserName = "所有操作员";
				}
				else
				{
					strUserName = PubFunction.UserIDTOName(int.Parse(userID));
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
				else if (checkRadion == "6")
				{
					string t = timeStart;
					if (!DateTime.TryParse(t, out time))
					{
						flag = "-1";
						return;
					}
					string t2 = timeEnd;
					if (!DateTime.TryParse(t2, out time2))
					{
						flag = "-1";
						return;
					}
					time = DateTime.Parse(timeStart);
					time2 = DateTime.Parse(timeEnd).AddSeconds(59.0);
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
				orderWhere += " and OrderType <> 3 ";
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
				DataRow[] drss = ds.Tables[1].Select(" RechargeType=2 ");
				if (drss.Length == 1)
				{
					FRechargeGiveMoney += decimal.Parse(drss[0][1].ToString());
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
				decimal payBink = 0m;
				decimal payCash = 0m;
				decimal payCard = 0m;
				decimal payCoupon = 0m;
				decimal payPoint = 0m;
				decimal zljg = 0m;
				foreach (DataRow dr in ds.Tables[2].Rows)
				{
					payCard += decimal.Parse(dr["OrderPayCard"].ToString());
					payCash += decimal.Parse(dr["OrderPayCash"].ToString());
					payBink += decimal.Parse(dr["OrderPayBink"].ToString());
					payCoupon += decimal.Parse(dr["OrderPayCoupon"].ToString());
					payPoint += decimal.Parse(dr["UsePointAmount"].ToString());
					zljg += decimal.Parse(dr["UsePointAmount"].ToString()) + (decimal.Parse(dr["OrderPayCard"].ToString()) + decimal.Parse(dr["OrderPayBink"].ToString()) + decimal.Parse(dr["OrderPayCash"].ToString()) + decimal.Parse(dr["OrderPayCoupon"].ToString())) - decimal.Parse(dr["OrderDiscountMoney"].ToString());
				}
				decimal countPayBink = 0m;
				decimal countPayCash = 0m;
				decimal countPayCard = 0m;
				decimal countPayCoupon = 0m;
				decimal jczl = 0m;
				foreach (DataRow dr in ds.Tables[3].Rows)
				{
					countPayCard += decimal.Parse(dr["CountPayCard"].ToString());
					countPayCash += decimal.Parse(dr["CountPayCash"].ToString());
					countPayBink += decimal.Parse(dr["CountPayBink"].ToString());
					countPayCoupon += decimal.Parse(dr["CountPayCoupon"].ToString());
					jczl += decimal.Parse(dr["CountPayCard"].ToString()) + decimal.Parse(dr["CountPayBink"].ToString()) + decimal.Parse(dr["CountPayCash"].ToString()) + decimal.Parse(dr["CountPayCoupon"].ToString()) - decimal.Parse(dr["CountDiscountMoney"].ToString());
				}
				decimal StorageTimingPayCard = 0m;
				decimal StorageTimingPayCash = 0m;
				decimal StorageTimingPayBink = 0m;
				decimal StorageTimingPayCoupon = 0m;
				decimal StorageTimingzlmoney = 0m;
				if (ds.Tables[4].Rows.Count > 0)
				{
					StorageTimingPayCard = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingPayCard"]);
					StorageTimingPayCash = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingPayCash"]);
					StorageTimingPayBink = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingPayBink"]);
					StorageTimingPayCoupon = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingPayCoupon"]);
					decimal StorageTimingDiscountMoney = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingDiscountMoney"]);
					StorageTimingzlmoney = StorageTimingPayCard + StorageTimingPayCash + StorageTimingPayBink + StorageTimingPayCoupon - StorageTimingDiscountMoney;
				}
				decimal expenseSumMoneys = payCash - zljg;
				decimal countSumMoneys = countPayCash - jczl;
				decimal allMoney = FRechargeMoney + expenseSumMoneys + countSumMoneys + (StorageTimingPayCash - StorageTimingzlmoney) - Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]);
				decimal doWorkallMoney = FRechargeMoney + expenseSumMoneys + countSumMoneys + (StorageTimingPayCash - StorageTimingzlmoney) - Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]);
				flag = string.Concat(new object[]
				{
					"{\"timeRadion\":\"",
					time,
					" 至 ",
					time2,
					"\",\"intNumber\":\"",
					intNumber,
					"名\",\"memSRechargeMoney\":\"",
					SRechargeMoney.ToString("#0.00"),
					"\",\"memFRechargeMoney\":\"",
					FRechargeMoney.ToString("#0.00"),
					"\",\"expenseSumMoneys\":\"",
					expenseSumMoneys.ToString("#0.00"),
					"\",\"expenseBankSumMoneys\":\"",
					FRechargeBankMoney.ToString("#0.00"),
					"\",\"payCard\":\"",
					payCard.ToString("#0.00"),
					"\",\"payBink\":\"",
					payBink.ToString("#0.00"),
					"\",\"payCoupon\":\"",
					payCoupon.ToString("#0.00"),
					"\",\"payPoint\":\"",
					payPoint.ToString("#0.00"),
					"\",\"FRechargeGiveMoney\":\"",
					FRechargeGiveMoney.ToString("#0.00"),
					"\",\"allMoney\":\"",
					allMoney.ToString("#0.00"),
					"\",\"MemDetial\":\"",
					sbMem,
					"\",\"strUserName\":\"",
					strUserName,
					"\",\"doWorkallMoney\":\"",
					doWorkallMoney,
					"\",\"countSumMoneys\":\"",
					countSumMoneys.ToString("#0.00"),
					"\",\"countPayCard\":\"",
					countPayCard.ToString("#0.00"),
					"\",\"countPayBink\":\"",
					countPayBink.ToString("#0.00"),
					"\",\"countpayCoupon\":\"",
					countPayCoupon.ToString("#0.00"),
					"\",\"StorageTimingPayCard\":\"",
					StorageTimingPayCard.ToString("f2"),
					"\",\"StorageTimingPayCash\":\"",
					StorageTimingPayCash.ToString("f2"),
					"\",\"StorageTimingPayBink\":\"",
					StorageTimingPayBink.ToString("f2"),
					"\",\"StorageTimingPayCoupon\":\"",
					StorageTimingPayCoupon.ToString("f2"),
					"\",\"AllDrawMoney\":\"",
					Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]).ToString("F2"),
					"\",\"AllDrawActualMoney\":\"",
					Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawActualMoney"]).ToString("F2"),
					"\"}"
				});
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-3";
			}
			this.Context.Response.Write(flag);
		}

		public void GetGoodsExpenseDetail()
		{
			string msgResponse = "";
			try
			{
				string strQueryGoods = this.Request["goodsName"].ToString();
				string strGoodsType = this.Request["goodsType"].ToString();
				string strShopID = this.Request["shopID"].ToString();
				string strGoodsClass = (this.Request["goodsClass"] == null) ? "" : this.Request["goodsClass"].Trim();
				int intGoodsID = (this.Request["goodsID"] != "") ? int.Parse(this.Request["goodsID"].ToString()) : 0;
				int intUserShopID = this.UserModel.UserShopID;
				string strStartTime = (!string.IsNullOrEmpty(this.Request["startTime"])) ? this.Request["startTime"].ToString() : "";
				string strEndTime = (!string.IsNullOrEmpty(this.Request["endTime"])) ? this.Request["endTime"].ToString() : "";
				string strSql = " OrderDetail.OrderID = OrderLog.OrderID and OrderLog.OrderMemID=Mem.MemID and dbo.Goods.GoodsID=dbo.OrderDetail.GoodsID ";
				int intPageSize = (!string.IsNullOrEmpty(this.Request["pageSize"])) ? Convert.ToInt32(this.Request["pageSize"].ToString()) : 0;
				int intcurrentPage = (!string.IsNullOrEmpty(this.Request["currentPage"])) ? Convert.ToInt32(this.Request["currentPage"].ToString()) : 0;
				if (strStartTime != "")
				{
					strSql = strSql + " and  OrderCreateTime>='" + strStartTime + "'";
				}
				if (strEndTime != "")
				{
					strSql = strSql + " and OrderCreateTime<'" + strEndTime + "'";
				}
				if (strQueryGoods != "")
				{
					string text = strSql;
					strSql = string.Concat(new string[]
					{
						text,
						" and (GoodsCode like '",
						strQueryGoods,
						"' or Name like '",
						strQueryGoods,
						"' or NameCode like '",
						strQueryGoods,
						"')"
					});
				}
				if (strGoodsType != "")
				{
					strSql = strSql + " and GoodsType=" + strGoodsType;
				}
				if (strGoodsClass != "")
				{
					Chain.Model.GoodsClass model = new Chain.BLL.GoodsClass().GetModel(int.Parse(strGoodsClass));
					if (model.ParentID == 0)
					{
						string strClass = int.Parse(strGoodsClass) + ",";
						DataTable dt = new Chain.BLL.GoodsClass().GetList("ParentID=" + int.Parse(strGoodsClass)).Tables[0];
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							strClass = strClass + dt.Rows[i]["ClassID"] + ",";
						}
						int length = strClass.Length;
						strClass = strClass.Trim(new char[]
						{
							','
						});
						strSql = strSql + " and GoodsClassID in (" + strClass + ")";
					}
					else
					{
						strSql = strSql + " and GoodsClassID=" + strGoodsClass;
					}
				}
				if (strShopID != "")
				{
					strSql = strSql + " and OrderShopID=" + strShopID;
				}
				if (intGoodsID != 0)
				{
					strSql = strSql + " and OrderType <> 4 and OrderType <> 5 and OrderDetail.GoodsID=" + intGoodsID;
					int resCount;
					DataTable dtDetail = this.bllDetail.GetExpenseDetail(intPageSize, intcurrentPage, out resCount, new string[]
					{
						strSql.ToString()
					}).Tables[0];
					if (dtDetail != null)
					{
						string strJson = JsonPlus.ToJson(dtDetail, "");
						msgResponse = string.Concat(new object[]
						{
							"{\"RecordCount\":",
							resCount,
							",\"List\":",
							strJson,
							"}"
						});
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetRechargeListByPage()
		{
			string flag = "";
			int userShopID = this.UserModel.UserShopID;
			try
			{
				string strQueryMem = string.IsNullOrEmpty(this.Request["memInfo"].ToString()) ? "" : this.Request["memInfo"].ToString();
				string strMemLevelID = string.IsNullOrEmpty(this.Request["memLevel"].ToString()) ? "" : this.Request["memLevel"].ToString();
				string strMemShopID = string.IsNullOrEmpty(this.Request["memShopID"].ToString()) ? "" : this.Request["memShopID"].ToString();
				string strMoneySymbols = string.IsNullOrEmpty(this.Request["moneySymbols"].ToString()) ? "" : this.Request["moneySymbols"].ToString();
				string strMoney = string.IsNullOrEmpty(this.Request["rechargeMoney"].ToString()) ? "0" : this.Request["rechargeMoney"].ToString();
				decimal dclMoney = decimal.Parse(strMoney);
				string strRechargeType = string.IsNullOrEmpty(this.Request["rechargeType"].ToString()) ? "" : this.Request["rechargeType"].ToString();
				string strRechargeAccount = string.IsNullOrEmpty(this.Request["rechargeAccount"].ToString()) ? "" : PubFunction.RemoveSpace(this.Request["rechargeAccount"].ToString());
				string strRemark = string.IsNullOrEmpty(this.Request["remark"].ToString()) ? "" : PubFunction.RemoveSpace(this.Request["remark"].ToString());
				string startTime = string.IsNullOrEmpty(this.Request["startTime"].ToString()) ? "" : this.Request["startTime"].ToString();
				string endTime = string.IsNullOrEmpty(this.Request["endTime"].ToString()) ? "" : this.Request["endTime"].ToString();
				int size = string.IsNullOrEmpty(this.Request["size"].ToString()) ? 0 : int.Parse(this.Request["size"].ToString());
				int index = string.IsNullOrEmpty(this.Request["index"].ToString()) ? 0 : int.Parse(this.Request["index"].ToString());
				StringBuilder strSql = new StringBuilder();
				strSql.Append("RechargeIsApprove=1");
				if (strQueryMem != "")
				{
					strSql.AppendFormat("and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", strQueryMem);
				}
				if (strMemLevelID != "")
				{
					strSql.AppendFormat("and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
				}
				if (strMemShopID != "")
				{
					strSql.AppendFormat("and RechargeShopID={0}", int.Parse(strMemShopID));
				}
				if (dclMoney != 0m)
				{
					strSql.AppendFormat("and RechargeMoney" + strMoneySymbols + "{0}", dclMoney);
				}
				if (startTime != "")
				{
					strSql.AppendFormat("and RechargeCreateTime>='{0}' ", startTime);
				}
				if (endTime != "")
				{
					strSql.AppendFormat("and RechargeCreateTime< '{0}'", PubFunction.TimeEndDay(endTime));
				}
				if (strRechargeType != "")
				{
					strSql.AppendFormat("and RechargeType={0} ", int.Parse(strRechargeType));
				}
				if (strRemark != "")
				{
					strSql.AppendFormat(" and RechargeRemark like '%{0}%' ", strRemark);
				}
				if (strRechargeAccount != "")
				{
					strSql.AppendFormat(" and RechargeAccount='{0}'", strRechargeAccount);
				}
				strSql.Append(" and MemRecharge.RechargeShopID = SysShop.ShopID and MemRecharge.RechargeMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemRecharge.RechargeUserID = SysUser.UserID");
				int counts;
				DataTable dbRecharge = this.bllMemRecharge.GetListSP(size, index, out counts, new string[]
				{
					PubFunction.GetShopAuthority(userShopID, "RechargeShopID", strSql.ToString())
				}).Tables[0];
				if (dbRecharge != null)
				{
					string response = JsonPlus.ToJson(dbRecharge, "RechargeAccount,MemCard,MemName,RechargeType,RechargeMoney,RechargeOrdMoney,RechargeGive,RechargeCardBalance,RechargeRemark,ShopName,RechargeCreateTime,UserName,RechargeID,RechargeMemID,RechargePoint");
					flag = string.Concat(new object[]
					{
						"{\"RechargeList\":",
						response,
						",\"RecordCount\":",
						counts,
						"}"
					});
				}
				this.Context.Response.Write(flag);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
		}

		public void GetMemRechargeListByPage()
		{
			string flag = "";
			int userShopID = this.UserModel.UserShopID;
			try
			{
				string strOrderBy = this.Request["orderBy"].ToString();
				string strQueryMem = string.IsNullOrEmpty(this.Request["memInfo"].ToString()) ? "" : this.Request["memInfo"].ToString();
				string strMemShopID = string.IsNullOrEmpty(this.Request["memShopID"].ToString()) ? "" : this.Request["memShopID"].ToString();
				string strRechargeType = string.IsNullOrEmpty(this.Request["type"].ToString()) ? "" : this.Request["type"].ToString();
				string startTime = string.IsNullOrEmpty(this.Request["startTime"]) ? "" : this.Request["startTime"];
				string endTime = string.IsNullOrEmpty(this.Request["endTime"]) ? "" : this.Request["endTime"];
				DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
				DateTime.TryParse(startTime, out date);
				startTime = date.ToString();
				date = DateTime.Now;
				DateTime.TryParse(endTime, out date);
				endTime = date.ToString();
				int size = string.IsNullOrEmpty(this.Request["size"]) ? 0 : int.Parse(this.Request["size"].ToString());
				int index = string.IsNullOrEmpty(this.Request["index"]) ? 0 : int.Parse(this.Request["index"].ToString());
				StringBuilder strSql = new StringBuilder();
				strSql.Append(string.Format("RechargeCreateTime>='{0}' AND RechargeCreateTime<='{1}' ", startTime, PubFunction.TimeEndDay(endTime)));
				if (strQueryMem != "")
				{
					strSql.AppendFormat("and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", strQueryMem);
				}
				if (strMemShopID != "")
				{
					strSql.AppendFormat("and MemShopID={0}", int.Parse(strMemShopID));
				}
				if (strRechargeType != "")
				{
					strSql.AppendFormat("and RechargeType={0} ", int.Parse(strRechargeType));
				}
				string orderBy = "RechargeMemID";
				bool isAsc = true;
				string text = strOrderBy;
				switch (text)
				{
				case "":
					orderBy = "RechargeMemID";
					isAsc = false;
					break;
				case "1":
					orderBy = "MemMoney";
					isAsc = true;
					break;
				case "2":
					orderBy = "MemMoney";
					isAsc = false;
					break;
				case "3":
					orderBy = "RechargeCount";
					isAsc = true;
					break;
				case "4":
					orderBy = "RechargeCount";
					isAsc = false;
					break;
				case "5":
					orderBy = "LastRechargeTime";
					isAsc = true;
					break;
				case "6":
					orderBy = "LastRechargeTime";
					isAsc = false;
					break;
				case "7":
					orderBy = "TotalMoney";
					isAsc = true;
					break;
				case "8":
					orderBy = "TotalMoney";
					isAsc = false;
					break;
				case "9":
					orderBy = "RechargeTotalMoney";
					isAsc = true;
					break;
				case "10":
					orderBy = "RechargeTotalMoney";
					isAsc = false;
					break;
				case "11":
					orderBy = "RechargeTotalGive";
					isAsc = true;
					break;
				case "12":
					orderBy = "RechargeTotalGive";
					isAsc = false;
					break;
				}
				string sql = PubFunction.GetShopAuthority(userShopID, "MemShopID", strSql.ToString());
				int counts;
				DataTable dbMemRecharge = this.bllMemRecharge.GetListSP1(size, index, orderBy, isAsc, out counts, new string[]
				{
					sql
				}).Tables[0];
				if (dbMemRecharge != null)
				{
					string response = JsonPlus.ToJson(dbMemRecharge, "RechargeMemID,MemName,MemCard,MemMoney,ShopName,TotalMoney,RechargeTotalMoney,RechargeTotalGive,LastRechargeTime,RechargeCount");
					flag = string.Concat(new object[]
					{
						"{\"MemRechargeList\":",
						response,
						",\"RecordCount\":",
						counts,
						"}"
					});
				}
				this.Context.Response.Write(flag);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
		}

		public void GetShopRechargeListByPage()
		{
			string flag = "";
			int userShopID = this.UserModel.UserShopID;
			try
			{
				string strShopID = string.IsNullOrEmpty(this.Request["shopID"].ToString()) ? "" : this.Request["shopID"].ToString();
				string strRechargeType = string.IsNullOrEmpty(this.Request["type"].ToString()) ? "" : this.Request["type"].ToString();
				string startTime = string.IsNullOrEmpty(this.Request["startTime"]) ? "" : this.Request["startTime"];
				string strOrderBy = this.Request["orderBy"].ToString();
				string endTime = string.IsNullOrEmpty(this.Request["endTime"]) ? "" : this.Request["endTime"];
				int size = string.IsNullOrEmpty(this.Request["size"].ToString()) ? 0 : int.Parse(this.Request["size"].ToString());
				int index = string.IsNullOrEmpty(this.Request["index"].ToString()) ? 0 : int.Parse(this.Request["index"].ToString());
				DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
				DateTime.TryParse(startTime, out date);
				startTime = date.ToString();
				date = DateTime.Now;
				DateTime.TryParse(endTime, out date);
				endTime = date.ToString();
				StringBuilder strSql = new StringBuilder();
				strSql.Append(string.Format(" RechargeCreateTime>='{0}' AND RechargeCreateTime<='{1}' ", startTime, PubFunction.TimeEndDay(endTime)));
				if (strShopID != "")
				{
					strSql.AppendFormat("and ShopID={0}", int.Parse(strShopID));
				}
				if (strRechargeType != "")
				{
					strSql.AppendFormat("and RechargeType={0} ", int.Parse(strRechargeType));
				}
				string orderBy = "RechargeMemID";
				bool isAsc = true;
				string text = strOrderBy;
				switch (text)
				{
				case "":
					orderBy = "RechargeShopID";
					isAsc = false;
					break;
				case "1":
					orderBy = "TotalMoney";
					isAsc = true;
					break;
				case "2":
					orderBy = "TotalMoney";
					isAsc = false;
					break;
				case "3":
					orderBy = "RechargeTotalMoney";
					isAsc = true;
					break;
				case "4":
					orderBy = "RechargeTotalMoney";
					isAsc = false;
					break;
				case "5":
					orderBy = "RechargeTotalGive";
					isAsc = true;
					break;
				case "6":
					orderBy = "RechargeTotalGive";
					isAsc = false;
					break;
				case "7":
					orderBy = "RechargeCount";
					isAsc = true;
					break;
				case "8":
					orderBy = "RechargeCount";
					isAsc = false;
					break;
				}
				int counts;
				DataTable dbShopRecharge = this.bllMemRecharge.GetListSP2(size, index, orderBy, isAsc, out counts, new string[]
				{
					PubFunction.GetShopAuthority(userShopID, "ShopID", strSql.ToString())
				}).Tables[0];
				if (dbShopRecharge != null)
				{
					string response = JsonPlus.ToJson(dbShopRecharge, "ShopName,TotalMoney,RechargeTotalMoney,RechargeTotalGive,TotalCount");
					flag = string.Concat(new object[]
					{
						"{\"ShopRechargeList\":",
						response,
						",\"RecordCount\":",
						counts,
						"}"
					});
				}
				this.Context.Response.Write(flag);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
		}

		public void AllianceAdd()
		{
			int flag = 0;
			try
			{
				string shopName = this.Request["shopName"].ToString();
				string shopContactMan = this.Request["shopContactMan"].ToString();
				string shopTelephone = this.Request["shopTelephone"].ToString();
				string shopAddress = this.Request["shopAddress"].ToString();
				string shopRemark = this.Request["shopRemark"].ToString();
				string shopTitle = this.Request["shopTitle"].ToString();
				string shopFoot = this.Request["shopFoot"].ToString();
				string shopSmsName = this.Request["shopSmsName"].ToString();
				int settlementInterval = string.IsNullOrEmpty(this.Request["txtSettlementInterval"].ToString()) ? 65535 : int.Parse(this.Request["txtSettlementInterval"].ToString());
				decimal shopProportion = string.IsNullOrEmpty(this.Request["txtShopProportion"].ToString()) ? 0m : decimal.Parse(this.Request["txtShopProportion"].ToString());
				int shopType = Convert.ToInt32(this.Request["shopType"].ToString());
				int sysShopId = this.UserModel.UserShopID;
				int sysUserId = this.UserModel.UserID;
				Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
				modelShop.ShopName = shopName;
				modelShop.ShopContactMan = shopContactMan;
				modelShop.ShopTelephone = shopTelephone;
				modelShop.ShopAddress = shopAddress;
				modelShop.ShopRemark = shopRemark;
				modelShop.ShopCreateTime = DateTime.Now;
				modelShop.ShopPrintTitle = shopTitle;
				modelShop.ShopPrintFoot = shopFoot;
				modelShop.ShopSmsName = shopSmsName;
				modelShop.SettlementInterval = settlementInterval;
				modelShop.ShopProportion = shopProportion;
				modelShop.ShopType = shopType;
				modelShop.IsAllianceProgram = (this.Request["isAllianceProgram"].ToString() == "1");
				modelShop.FatherShopID = Convert.ToInt32(this.Request["fatherShopID"]);
				modelShop.SmsType = Convert.ToInt32(this.Request["SmsType"]);
				modelShop.PointType = Convert.ToInt32(this.Request["PointType"]);
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				Chain.Model.SysShop mdSysShop = new Chain.BLL.SysShop().GetModel(modelShop.FatherShopID);
				modelShop.ShopState = (this.Request["isChoose"] == "0");
				flag = bllShop.Add(modelShop);
				if (flag > 0)
				{
					Chain.BLL.SysShopMemLevel bllSysShopMemLevel = new Chain.BLL.SysShopMemLevel();
					Chain.BLL.MemLevel bllLevel = new Chain.BLL.MemLevel();
					DataTable dtMemLevel = bllLevel.GetAllList().Tables[0];
					foreach (DataRow rowMemLevel in dtMemLevel.Rows)
					{
						int levelID = int.Parse(rowMemLevel["LevelID"].ToString());
						bllSysShopMemLevel.Add(new Chain.Model.SysShopMemLevel
						{
							ShopID = flag,
							MemLevelID = levelID,
							ClassDiscountPercent = bllLevel.GetModel(levelID).LevelDiscountPercent,
							ClassPointPercent = bllLevel.GetModel(levelID).LevelPointPercent,
							ClassRechargePointRate = bllLevel.GetModel(levelID).LevelRechargePointRate
						});
					}
					string strShopIDArray = "";
					DataTable dt = bllShop.GetList("ShopID>0").Tables[0];
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						strShopIDArray = strShopIDArray + dt.Rows[i]["ShopID"].ToString() + ",";
					}
					strShopIDArray = strShopIDArray.Remove(strShopIDArray.Length - 1);
					Chain.BLL.SysShopAuthority bllShopAuthority = new Chain.BLL.SysShopAuthority();
					Chain.Model.SysShopAuthority modelShopAuthority = new Chain.Model.SysShopAuthority();
					modelShopAuthority.ShopAuthorityShopID = new int?(flag);
					modelShopAuthority.ShopAuthorityData = flag.ToString();
					int shop = bllShopAuthority.Add(modelShopAuthority);
					if (shop > 0)
					{
						if (modelShop.FatherShopID != 0)
						{
							Chain.Model.SysShopAuthority modelFatherShopAuthority = bllShopAuthority.GetModel(modelShop.FatherShopID);
							modelFatherShopAuthority.ShopAuthorityData = modelFatherShopAuthority.ShopAuthorityData + "," + shop;
							bllShopAuthority.Update(modelFatherShopAuthority);
						}
						DataTable dtShopAuthority = bllShopAuthority.GetList(" ShopAuthorityShopID=1 ").Tables[0];
						if (dtShopAuthority.Rows.Count > 0)
						{
							modelShopAuthority.ShopAuthorityID = int.Parse(dtShopAuthority.Rows[0]["ShopAuthorityID"].ToString());
							modelShopAuthority.ShopAuthorityShopID = new int?(1);
							modelShopAuthority.ShopAuthorityData = strShopIDArray;
							bllShopAuthority.Update(modelShopAuthority);
						}
						else
						{
							modelShopAuthority.ShopAuthorityShopID = new int?(1);
							modelShopAuthority.ShopAuthorityData = strShopIDArray;
							int ShopAuthorityID = bllShopAuthority.Add(modelShopAuthority);
							if (ShopAuthorityID > 0)
							{
								modelShopAuthority.ShopAuthorityID = ShopAuthorityID;
								modelShopAuthority.ShopAuthorityShopID = new int?(1);
								modelShopAuthority.ShopAuthorityData = strShopIDArray;
								bllShopAuthority.Update(modelShopAuthority);
							}
						}
					}
					PubFunction.SaveSysLog(sysUserId, 1, "联盟商新增", string.Concat(new string[]
					{
						"新增联盟商,联盟商名称：[",
						modelShop.ShopName,
						"],联盟商联系人：[",
						modelShop.ShopContactMan,
						"],联系方式：[",
						modelShop.ShopTelephone,
						"]"
					}), sysShopId, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void ShopAdd()
		{
			int flag = 0;
			try
			{
				int shopType = int.Parse(this.Request["shopType"].ToString());
				string shopImageUrl = "";
				int shopProvince = 0;
				int shopCity = 0;
				int shopCounty = 0;
				decimal rechargeMaxMoney = 0m;
				bool isRecharge = false;
				bool isMainShop = false;
				if (shopType == 3)
				{
					shopImageUrl = ((this.Request["shopImageUrl"].ToString() != "") ? this.Request["shopImageUrl"].ToString() : "");
					shopProvince = ((this.Request["shopProvince"].ToString() != "") ? int.Parse(this.Request["shopProvince"].ToString()) : 0);
					shopCounty = ((this.Request["shopCounty"].ToString() != "") ? int.Parse(this.Request["shopCounty"].ToString()) : 0);
					shopCity = ((this.Request["shopCity"].ToString() != "") ? int.Parse(this.Request["shopCity"].ToString()) : 0);
					isRecharge = (this.Request["IsRecharge"].ToString() == "1");
					rechargeMaxMoney = (string.IsNullOrEmpty(this.Request["RechargeMaxMoney"].ToString()) ? 0m : decimal.Parse(this.Request["RechargeMaxMoney"].ToString()));
					isMainShop = (this.Request["IsMainShop"].ToString() == "1");
				}
				string shopName = this.Request["shopName"].ToString();
				string shopContactMan = this.Request["shopContactMan"].ToString();
				string shopTelephone = this.Request["shopTelephone"].ToString();
				string shopAddress = this.Request["shopAddress"].ToString();
				string shopRemark = this.Request["shopRemark"].ToString();
				string shopTitle = this.Request["shopTitle"].ToString();
				string shopFoot = this.Request["shopFoot"].ToString();
				string shopSmsName = this.Request["shopSmsName"].ToString();
				int settlementInterval = string.IsNullOrEmpty(this.Request["txtSettlementInterval"].ToString()) ? 65535 : int.Parse(this.Request["txtSettlementInterval"].ToString());
				decimal shopProportion = string.IsNullOrEmpty(this.Request["txtShopProportion"].ToString()) ? 0m : decimal.Parse(this.Request["txtShopProportion"].ToString());
				decimal RechargeProportion = string.IsNullOrEmpty(this.Request["RechargeProportion"].ToString()) ? 0m : decimal.Parse(this.Request["RechargeProportion"].ToString());
				decimal totalRate = decimal.Parse(this.Request["totalRate"].ToString());
				int sysShopId = this.UserModel.UserShopID;
				int sysUserId = this.UserModel.UserID;
				Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
				modelShop.ShopImageUrl = shopImageUrl;
				modelShop.ShopProvince = shopProvince;
				modelShop.ShopCity = shopCity;
				modelShop.ShopCounty = shopCounty;
				modelShop.ShopName = shopName;
				modelShop.IsMain = isMainShop;
				modelShop.ShopContactMan = shopContactMan;
				modelShop.ShopTelephone = shopTelephone;
				modelShop.ShopAddress = shopAddress;
				modelShop.ShopRemark = shopRemark;
				modelShop.ShopCreateTime = DateTime.Now;
				modelShop.ShopPrintTitle = shopTitle;
				modelShop.ShopPrintFoot = shopFoot;
				modelShop.ShopSmsName = shopSmsName;
				modelShop.SettlementInterval = settlementInterval;
				modelShop.ShopProportion = shopProportion;
				modelShop.RechargeProportion = RechargeProportion;
				modelShop.ShopType = shopType;
				modelShop.TotalRate = totalRate;
				modelShop.IsRecharge = isRecharge;
				modelShop.RechargeMaxMoney = rechargeMaxMoney;
				modelShop.IsAllianceProgram = (this.Request["isAllianceProgram"].ToString() == "1");
				modelShop.FatherShopID = Convert.ToInt32(this.Request["fatherShopID"]);
				modelShop.SmsType = Convert.ToInt32(this.Request["SmsType"]);
				modelShop.PointType = Convert.ToInt32(this.Request["PointType"]);
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				modelShop.ShopState = (this.Request["isChoose"] == "0");
				flag = bllShop.Add(modelShop);
				if (flag > 0)
				{
					Chain.BLL.SysShopMemLevel bllSysShopMemLevel = new Chain.BLL.SysShopMemLevel();
					Chain.BLL.MemLevel bllLevel = new Chain.BLL.MemLevel();
					DataTable dtMemLevel = bllLevel.GetAllList().Tables[0];
					foreach (DataRow rowMemLevel in dtMemLevel.Rows)
					{
						int levelID = int.Parse(rowMemLevel["LevelID"].ToString());
						bllSysShopMemLevel.Add(new Chain.Model.SysShopMemLevel
						{
							ShopID = flag,
							MemLevelID = levelID,
							ClassDiscountPercent = bllLevel.GetModel(levelID).LevelDiscountPercent,
							ClassPointPercent = bllLevel.GetModel(levelID).LevelPointPercent,
							ClassRechargePointRate = bllLevel.GetModel(levelID).LevelRechargePointRate
						});
					}
					string strShopIDArray = "";
					DataTable dt = bllShop.GetList("ShopID>0").Tables[0];
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						strShopIDArray = strShopIDArray + dt.Rows[i]["ShopID"].ToString() + ",";
					}
					strShopIDArray = strShopIDArray.Remove(strShopIDArray.Length - 1);
					Chain.BLL.SysShopAuthority bllShopAuthority = new Chain.BLL.SysShopAuthority();
					Chain.Model.SysShopAuthority modelShopAuthority = new Chain.Model.SysShopAuthority();
					modelShopAuthority.ShopAuthorityShopID = new int?(flag);
					modelShopAuthority.ShopAuthorityData = flag.ToString();
					int shop = bllShopAuthority.Add(modelShopAuthority);
					if (shop > 0)
					{
						if (modelShop.FatherShopID != 0)
						{
							Chain.Model.SysShopAuthority modelFatherShopAuthority = bllShopAuthority.GetModel(modelShop.FatherShopID);
							modelFatherShopAuthority.ShopAuthorityData = modelFatherShopAuthority.ShopAuthorityData + "," + shop;
							bllShopAuthority.Update(modelFatherShopAuthority);
						}
						DataTable dtShopAuthority = bllShopAuthority.GetList(" ShopAuthorityShopID=1 ").Tables[0];
						if (dtShopAuthority.Rows.Count > 0)
						{
							modelShopAuthority.ShopAuthorityID = int.Parse(dtShopAuthority.Rows[0]["ShopAuthorityID"].ToString());
							modelShopAuthority.ShopAuthorityShopID = new int?(1);
							modelShopAuthority.ShopAuthorityData = strShopIDArray;
							bllShopAuthority.Update(modelShopAuthority);
						}
						else
						{
							modelShopAuthority.ShopAuthorityShopID = new int?(1);
							modelShopAuthority.ShopAuthorityData = strShopIDArray;
							int ShopAuthorityID = bllShopAuthority.Add(modelShopAuthority);
							if (ShopAuthorityID > 0)
							{
								modelShopAuthority.ShopAuthorityID = ShopAuthorityID;
								modelShopAuthority.ShopAuthorityShopID = new int?(1);
								modelShopAuthority.ShopAuthorityData = strShopIDArray;
								bllShopAuthority.Update(modelShopAuthority);
							}
						}
					}
					PubFunction.SaveSysLog(sysUserId, 1, "商家新增", string.Concat(new string[]
					{
						"新增商家,商家名称：[",
						modelShop.ShopName,
						"],商家联系人：[",
						modelShop.ShopContactMan,
						"],联系方式：[",
						modelShop.ShopTelephone,
						"]"
					}), sysShopId, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void ShopEdit()
		{
			int flag = 0;
			try
			{
				string shopType = this.Request["shopType"].ToString();
				string shopImageUrl = "";
				decimal RechargeProportion = 0m;
				decimal rechargeMaxMoney = 0m;
				bool isRecharge = false;
				bool isMainShop = false;
				if (shopType == "3")
				{
					shopImageUrl = ((this.Request["shopImageUrl"].ToString() != "") ? this.Request["shopImageUrl"].ToString() : "");
					isMainShop = (this.Request["IsMainShop"].ToString() == "1");
					RechargeProportion = decimal.Parse(this.Request["RechargeProportion"].ToString());
					isRecharge = (this.Request["IsRecharge"].ToString() == "1");
					rechargeMaxMoney = (string.IsNullOrEmpty(this.Request["RechargeMaxMoney"].ToString()) ? 0m : decimal.Parse(this.Request["RechargeMaxMoney"].ToString()));
				}
				int shopProvince = (this.Request["shopProvince"].ToString() != "") ? int.Parse(this.Request["shopProvince"].ToString()) : 0;
				int shopCounty = (this.Request["shopCounty"].ToString() != "") ? int.Parse(this.Request["shopCounty"].ToString()) : 0;
				int shopCity = (this.Request["shopCity"].ToString() != "") ? int.Parse(this.Request["shopCity"].ToString()) : 0;
				int shopID = int.Parse(this.Request["shopID"].ToString());
				string shopName = this.Request["shopName"].ToString();
				string shopContactMan = this.Request["shopContactMan"].ToString();
				string shopTelephone = this.Request["shopTelephone"].ToString();
				string shopAddress = this.Request["shopAddress"].ToString();
				string shopRemark = this.Request["shopRemark"].ToString();
				string shopTitle = this.Request["shopTitle"].ToString();
				string shopFoot = this.Request["shopFoot"].ToString();
				string shopSmsName = this.Request["shopSmsName"].ToString();
				int sysShopId = this.UserModel.UserShopID;
				int sysUserId = this.UserModel.UserID;
				int settlementInterval = int.Parse(this.Request["txtSettlementInterval"].ToString());
				decimal shopProportion = decimal.Parse(this.Request["txtShopProportion"].ToString());
				decimal totalRate = decimal.Parse(this.Request["totalRate"].ToString());
				Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
				modelShop.IsMain = isMainShop;
				modelShop.IsRecharge = isRecharge;
				modelShop.RechargeMaxMoney = rechargeMaxMoney;
				modelShop.ShopImageUrl = shopImageUrl;
				modelShop.ShopProvince = shopProvince;
				modelShop.ShopCity = shopCity;
				modelShop.ShopCounty = shopCounty;
				modelShop.ShopID = shopID;
				modelShop.ShopName = shopName;
				modelShop.ShopContactMan = shopContactMan;
				modelShop.ShopTelephone = shopTelephone;
				modelShop.ShopAddress = shopAddress;
				modelShop.ShopRemark = shopRemark;
				modelShop.ShopPrintTitle = shopTitle;
				modelShop.ShopPrintFoot = shopFoot;
				modelShop.ShopSmsName = shopSmsName;
				modelShop.ShopProportion = shopProportion;
				modelShop.RechargeProportion = RechargeProportion;
				modelShop.SettlementInterval = settlementInterval;
				modelShop.SmsType = Convert.ToInt32(this.Request["SmsType"]);
				modelShop.PointType = Convert.ToInt32(this.Request["PointType"]);
				modelShop.TotalRate = totalRate;
				Chain.Model.SysShop sysShop = new Chain.BLL.SysShop().GetModel(shopID);
				modelShop.PointCount = sysShop.PointCount;
				modelShop.SmsCount = sysShop.SmsCount;
				Chain.Model.SysShop mdSysShop = new Chain.Model.SysShop();
				if (!sysShop.IsAllianceProgram && sysShop.FatherShopID != 0)
				{
					int fatherShopID = Convert.ToInt32(this.Request["FatherShopID"]);
					mdSysShop = new Chain.BLL.SysShop().GetModel(fatherShopID);
				}
				if (!sysShop.IsAllianceProgram && sysShop.FatherShopID != 0 && ((shopID > 1 && mdSysShop.PointType < modelShop.PointType) || (shopID > 1 && mdSysShop.SmsType < modelShop.SmsType)))
				{
					flag = -5;
				}
				else
				{
					if (this.Request["isChoose"] == "0")
					{
						if (sysShopId == shopID)
						{
							flag = -2;
						}
						else
						{
							modelShop.ShopState = true;
							new Chain.BLL.SysUser().UpdateUserLock(modelShop.ShopID, 1);
						}
					}
					else
					{
						modelShop.ShopState = false;
						new Chain.BLL.SysUser().UpdateUserLock(modelShop.ShopID, 0);
					}
					if (flag != -2)
					{
						Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
						flag = bllShop.Update(modelShop);
						if (1 == flag && sysShop.IsAllianceProgram)
						{
							DataTable dt = bllShop.GetList(" FatherShopID = " + sysShop.ShopID.ToString()).Tables[0];
							for (int i = 0; i < dt.Rows.Count; i++)
							{
								if (Convert.ToInt32(dt.Rows[i]["SmsType"]) > modelShop.SmsType || Convert.ToInt32(dt.Rows[i]["PointType"]) > modelShop.PointType)
								{
									dt.Rows[i]["SmsType"] = ((Convert.ToInt32(dt.Rows[i]["SmsType"]) <= modelShop.SmsType) ? dt.Rows[i]["SmsType"] : modelShop.SmsType);
									dt.Rows[i]["PointType"] = ((Convert.ToInt32(dt.Rows[i]["PointType"]) <= modelShop.PointType) ? dt.Rows[i]["PointType"] : modelShop.PointType);
									bllShop.Update(Convert.ToInt32(dt.Rows[i]["ShopID"]), Convert.ToInt32(dt.Rows[i]["SmsType"]), Convert.ToInt32(dt.Rows[i]["PointType"]));
								}
							}
						}
					}
					if (flag > 0)
					{
						PubFunction.SaveSysLog(sysUserId, 3, "商家编辑", string.Concat(new string[]
						{
							"商家修改,商家名称：[",
							modelShop.ShopName,
							"],商家联系人：[",
							modelShop.ShopContactMan,
							"],联系方式：[",
							modelShop.ShopTelephone,
							"]"
						}), sysShopId, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GetShopInfo()
		{
			string msgResponse = "";
			try
			{
				int shopID = int.Parse(this.Request["shopID"]);
				DataTable dt = new Chain.BLL.SysShop().GetList(" ShopID=" + shopID).Tables[0];
				msgResponse = JsonPlus.ToJson(dt.Rows[0], "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void SetShopSms()
		{
			int flag = 0;
			try
			{
				int ShopID = Convert.ToInt32(this.Request["shopID"]);
				int type = Convert.ToInt32(this.Request["type"]);
				int count = Convert.ToInt32(this.Request["count"]);
				int shopType = Convert.ToInt32(this.Request["shopType"]);
				Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
				Chain.Model.SysShop modelSysShop = new Chain.Model.SysShop();
				modelSysShop = bllSysShop.GetModel(ShopID);
				if (modelSysShop.SmsCount < count && type == 1)
				{
					flag = 1;
				}
				else if (PubFunction.IsCanSendSms(modelSysShop.FatherShopID, count) || type != 0)
				{
					string strGiveShopName = bllSysShop.GetShopNamebyShopID(this.UserModel.UserShopID);
					string czlx;
					if (type == 0)
					{
						modelSysShop.SmsCount += count;
						czlx = "充值";
					}
					else
					{
						modelSysShop.SmsCount -= count;
						czlx = "扣除";
					}
					bllSysShop.Update(modelSysShop);
					Chain.BLL.SysShopCmsLog bllSysShopCmsLog = new Chain.BLL.SysShopCmsLog();
					Chain.Model.SysShopCmsLog modelSysShopCmsLog = new Chain.Model.SysShopCmsLog();
					modelSysShopCmsLog.Count = ((type == 0) ? count : (-1 * count));
					modelSysShopCmsLog.CreateTime = DateTime.Now;
					modelSysShopCmsLog.OutShopID = ShopID;
					modelSysShopCmsLog.ShopID = this.UserModel.UserShopID;
					modelSysShopCmsLog.ShopCmsAccount = "DX" + DateTime.Now.ToString("yyMMddHHmmssffff");
					modelSysShopCmsLog.UserID = this.UserModel.UserID;
					if (shopType == 2)
					{
						if (czlx == "充值")
						{
							modelSysShopCmsLog.ShopCmsType = 0;
							modelSysShopCmsLog.Remark = string.Concat(new object[]
							{
								strGiveShopName,
								"给",
								modelSysShop.ShopName,
								"充值短信：[",
								count,
								"]"
							});
						}
						if (czlx == "扣除")
						{
							modelSysShopCmsLog.ShopCmsType = 1;
							modelSysShopCmsLog.Remark = string.Concat(new object[]
							{
								strGiveShopName,
								"给",
								modelSysShop.ShopName,
								"扣除短信：[",
								count,
								"]"
							});
						}
					}
					if (shopType == 3)
					{
						if (czlx == "充值")
						{
							modelSysShopCmsLog.ShopCmsType = 2;
							modelSysShopCmsLog.Remark = string.Concat(new object[]
							{
								strGiveShopName,
								"给",
								modelSysShop.ShopName,
								"充值短信：[",
								count,
								"]"
							});
						}
						if (czlx == "扣除")
						{
							modelSysShopCmsLog.ShopCmsType = 3;
							modelSysShopCmsLog.Remark = string.Concat(new object[]
							{
								strGiveShopName,
								"给",
								modelSysShop.ShopName,
								"扣除短信：[",
								count,
								"]"
							});
						}
					}
					bllSysShopCmsLog.Add(modelSysShopCmsLog);
					PubFunction.SaveSysLog(this.UserModel.UserID, 1, "商家短信变动", string.Concat(new string[]
					{
						"商家：[",
						modelSysShop.ShopName,
						"]",
						czlx,
						count.ToString(),
						"短信"
					}), this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
					flag = 2;
					if (PubFunction.curParameter.bolShopSmsManage)
					{
						if (shopType == 3 || shopType == 2)
						{
							if (type == 0)
							{
								PubFunction.SetShopSmsNew(this.UserModel.UserID, modelSysShop.FatherShopID, this.UserModel.UserShopID, count, modelSysShopCmsLog.ShopCmsType, string.Concat(new string[]
								{
									strGiveShopName,
									"给",
									modelSysShop.ShopName,
									czlx,
									"短信：[",
									count.ToString(),
									"]"
								}));
							}
							else
							{
								PubFunction.SetShopSmsNew(this.UserModel.UserID, modelSysShop.FatherShopID, this.UserModel.UserShopID, -count, modelSysShopCmsLog.ShopCmsType, string.Concat(new string[]
								{
									strGiveShopName,
									"给",
									modelSysShop.ShopName,
									czlx,
									"短信：[",
									count.ToString(),
									"]"
								}));
							}
						}
					}
				}
				else
				{
					flag = -5;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void SetShopPonit()
		{
			int flag = 0;
			try
			{
				int ShopID = Convert.ToInt32(this.Request["shopID"]);
				int type = Convert.ToInt32(this.Request["type"]);
				string shopName = this.Request["shopName"].ToString();
				int shopType = Convert.ToInt32(this.Request["shopType"]);
				int count = Convert.ToInt32(this.Request["count"]);
				Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
				Chain.Model.SysShop modelSysShop = new Chain.Model.SysShop();
				modelSysShop = bllSysShop.GetModel(ShopID);
				if (modelSysShop.PointCount < count && type == 1)
				{
					flag = 1;
				}
				else if (type != 0 || PubFunction.IsShopPoint(modelSysShop.FatherShopID, ref count))
				{
					string czlx;
					if (type == 0)
					{
						modelSysShop.PointCount += count;
						czlx = "充值";
					}
					else
					{
						modelSysShop.PointCount -= count;
						czlx = "扣除";
					}
					bllSysShop.Update(modelSysShop);
					Chain.BLL.SysShopPointLog bllSysShopPointLog = new Chain.BLL.SysShopPointLog();
					Chain.Model.SysShopPointLog modelSysShopPointLog = new Chain.Model.SysShopPointLog();
					if (type == 0)
					{
						modelSysShopPointLog.Count = count;
					}
					else
					{
						modelSysShopPointLog.Count = -count;
					}
					modelSysShopPointLog.CreateTime = DateTime.Now;
					modelSysShopPointLog.OutShopID = ShopID;
					modelSysShopPointLog.ShopID = this.UserModel.UserShopID;
					modelSysShopPointLog.ShopPointAccount = "JF" + DateTime.Now.ToString("yyMMddHHmmssffff");
					if (czlx == "充值")
					{
						modelSysShopPointLog.ShopPointType = 0;
					}
					if (czlx == "扣除")
					{
						modelSysShopPointLog.ShopPointType = 1;
					}
					modelSysShopPointLog.UserID = this.UserModel.UserID;
					string strGiveShopName = bllSysShop.GetShopNamebyShopID(this.UserModel.UserShopID);
					modelSysShopPointLog.Remark = string.Concat(new string[]
					{
						strGiveShopName,
						"给",
						shopName,
						czlx,
						"积分：[",
						count.ToString(),
						"]"
					});
					bllSysShopPointLog.Add(modelSysShopPointLog);
					PubFunction.SaveSysLog(this.UserModel.UserID, 1, shopName + "积分变动", string.Concat(new string[]
					{
						shopName,
						"：[",
						modelSysShop.ShopName,
						"]",
						czlx,
						count.ToString(),
						"积分"
					}), this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
					flag = 2;
					if (PubFunction.curParameter.bolShopPointManage)
					{
						if (shopType == 3 || shopType == 2)
						{
							if (type == 0)
							{
								PubFunction.SetShopPoint(this.UserModel.UserID, this.UserModel.UserShopID, modelSysShop.FatherShopID, count, string.Concat(new string[]
								{
									strGiveShopName,
									"给",
									shopName,
									czlx,
									"积分：[",
									count.ToString(),
									"]"
								}), modelSysShopPointLog.ShopPointType);
							}
							else
							{
								PubFunction.SetShopPoint(this.UserModel.UserID, this.UserModel.UserShopID, modelSysShop.FatherShopID, -count, string.Concat(new string[]
								{
									strGiveShopName,
									"给",
									shopName,
									czlx,
									"积分：[",
									count.ToString(),
									"]"
								}), modelSysShopPointLog.ShopPointType);
							}
						}
					}
				}
				else
				{
					flag = -5;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void GetSubShopList()
		{
			Chain.BLL.SysShop shopBll = new Chain.BLL.SysShop();
			string msgResponse = "";
			if (!string.IsNullOrEmpty(this.Request["shopid"].ToString()))
			{
				int parentShopID = int.Parse(this.Request["ShopID"]);
				string strWhere = " FatherShopID = " + parentShopID;
				try
				{
					DataTable dtSubShop = shopBll.GetList(strWhere).Tables[0];
					dtSubShop.Columns.Add("HasChildShop");
					for (int i = 0; i < dtSubShop.Rows.Count; i++)
					{
						if (shopBll.GetRecordCount(" FatherShopID = " + dtSubShop.Rows[i]["ShopID"].ToString()) != 0)
						{
							dtSubShop.Rows[i]["HasChildShop"] = 1;
						}
						else
						{
							dtSubShop.Rows[i]["HasChildShop"] = 0;
						}
						dtSubShop.Rows[i]["ShopProportion"] = string.Format("{0:N2}", dtSubShop.Rows[i]["ShopProportion"]);
					}
					if (dtSubShop != null)
					{
						msgResponse = JsonPlus.ToJson(dtSubShop, "ShopID,ShopName,ShopTelephone,ShopContactMan,ShopAreaID,ShopAddress,ShopRemark,ShopCreateTime,ShopState,ShopPrintTitle,ShopPrintFoot,ShopSmsName,SettlementInterval,ShopProportion,IsAllianceProgram,FatherShopID,PointCount,SmsCount,PointType,SmsType,HasChildShop");
					}
				}
				catch (Exception e)
				{
					this.LogError(e);
				}
				this.Context.Response.Write(msgResponse);
			}
		}

		public void GroupdDel()
		{
			int flag = 0;
			try
			{
				int intGroupID = int.Parse(this.Request["groupID"]);
				Chain.BLL.SysGroup group = new Chain.BLL.SysGroup();
				Chain.Model.SysGroup modelGroup = new Chain.BLL.SysGroup().GetModel(intGroupID);
				Chain.BLL.SysGroupAuthority groupAuthority = new Chain.BLL.SysGroupAuthority();
				DataTable dt = new Chain.BLL.SysUser().GetList(" UserGroupID=" + intGroupID).Tables[0];
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				if (dt.Rows.Count > 0)
				{
					flag = -2;
				}
				else
				{
					group.DeleteGroup(modelGroup);
					Chain.Model.SysLog modelLog = new Chain.Model.SysLog();
					modelLog.LogShopID = intUserShopID;
					modelLog.LogUserID = intUserID;
					modelLog.LogActionID = 8;
					modelLog.LogCreateTime = DateTime.Now;
					modelLog.LogDetail = "删除角色成功，角色名称：" + modelGroup.GroupName;
					new Chain.BLL.SysLog().Add(modelLog);
					flag = 1;
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GetGroupListByShopAndGroupType()
		{
			string msgResponse = "";
			try
			{
				int ShopID = int.Parse(this.Request["shopID"]);
				int GroupID = int.Parse(this.Request["groupID"]);
				int GroupType = int.Parse(this.Request["GroupType"]);
				string strSql = string.Format(" (IsPublic = '1' and GroupType=" + GroupType + ")", new object[0]);
				DataTable dtGroup = new Chain.BLL.SysGroup().GetList(strSql).Tables[0];
				if (dtGroup != null)
				{
					msgResponse = JsonPlus.ToJson(dtGroup, "GroupID,GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGroupListByShopAndGroup()
		{
			string msgResponse = "";
			try
			{
				int ShopID = int.Parse(this.Request["shopID"]);
				int GroupID = int.Parse(this.Request["groupID"]);
				string strSql = string.Format(" (IsPublic = 'true' or CreateUserID in (select UserID from sysUser where UserShopID={0})) ", ShopID);
				object obj = strSql;
				strSql = string.Concat(new object[]
				{
					obj,
					" and (ParentIDStr like '%/",
					GroupID,
					"/%' or GroupID = ",
					GroupID,
					")"
				});
				DataTable dtGroup = new Chain.BLL.SysGroup().GetList(strSql).Tables[0];
				if (dtGroup != null)
				{
					msgResponse = JsonPlus.ToJson(dtGroup, "GroupID,GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGroupListByGroup()
		{
			string msgResponse = "";
			try
			{
				int GroupID = int.Parse(this.Request["groupID"]);
				string strSql = string.Format(" (IsPublic = '1' and GroupType='1')", new object[0]);
				object obj = strSql;
				strSql = string.Concat(new object[]
				{
					obj,
					" and (ParentIDStr like '%/",
					GroupID,
					"/%' or GroupID = ",
					GroupID,
					")"
				});
				DataTable dtGroup = new Chain.BLL.SysGroup().GetList(strSql).Tables[0];
				if (dtGroup != null)
				{
					msgResponse = JsonPlus.ToJson(dtGroup, "GroupID,GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void ShopAuthority()
		{
			int flag = 0;
			try
			{
				string ShopID = this.Request["ShopID"].ToString();
				string authority = this.Request["List"].ToString();
				Chain.BLL.SysShopAuthority bllShopAuthority = new Chain.BLL.SysShopAuthority();
				Chain.Model.SysShopAuthority modelShopAuthority = bllShopAuthority.GetModelList(" ShopAuthorityShopID = " + ShopID)[0];
				authority = ShopID + "," + authority;
				modelShopAuthority.ShopAuthorityData = authority.Remove(authority.Length - 1, 1);
				if (bllShopAuthority.Update(modelShopAuthority))
				{
					flag = 1;
				}
			}
			catch
			{
				flag = 2;
			}
			this.Context.Response.Write(flag);
		}

		public void UserAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.SysUser modelUser = new Chain.Model.SysUser();
				Chain.Model.SysLog modelLog = new Chain.Model.SysLog();
				Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
				modelUser.UserName = this.Request["userName"].ToString();
				modelUser.UserAccount = this.Request["userAccount"].ToString();
				modelUser.UserTelephone = this.Request["userTel"].ToString();
				modelUser.UserNumber = this.Request["userNumber"].ToString();
				modelUser.UserPassword = DESEncrypt.Encrypt(this.Request["userPassword"].ToString());
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				if (this.Request["isChoose"] == "0")
				{
					modelUser.UserLock = true;
				}
				else
				{
					modelUser.UserLock = false;
				}
				modelUser.UserShopID = int.Parse(this.Request["userShopID"]);
				modelUser.UserGroupID = int.Parse(this.Request["authority"]);
				modelUser.UserRemark = this.Request["userRemark"];
				modelUser.UserCreateTime = DateTime.Now;
				flag = bllUser.Add(modelUser);
				if (flag > 0)
				{
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(modelUser.UserShopID);
					Chain.Model.SysGroup modelGroup = new Chain.BLL.SysGroup().GetModel(modelUser.UserGroupID);
					PubFunction.SaveSysLog(intUserID, 1, "用户新增", string.Concat(new string[]
					{
						"新增用户,用户名称：[",
						modelUser.UserName,
						"],用户所属商家：[",
						modelShop.ShopName,
						"],所属权限组：[",
						modelGroup.GroupName,
						"]"
					}), this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void UserEdit()
		{
			int flag = 0;
			try
			{
				string strPwd = this.Request["userPassword"].ToString();
				Chain.Model.SysUser modelUser = new Chain.Model.SysUser();
				Chain.Model.SysLog modelLog = new Chain.Model.SysLog();
				Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelUser = bllUser.GetModel(int.Parse(this.Request["userID"].ToString()));
				if (this.Request["isChoose"] == "0")
				{
					if (modelUser.UserID != intUserID)
					{
						modelUser.UserLock = true;
					}
					else
					{
						flag = -2;
					}
				}
				else
				{
					modelUser.UserLock = false;
				}
				if (flag >= 0)
				{
					modelUser.UserName = this.Request["userName"].ToString();
					modelUser.UserAccount = this.Request["userAccount"].ToString();
					modelUser.UserTelephone = this.Request["userTel"].ToString();
					modelUser.UserNumber = this.Request["userNumber"].ToString();
					modelUser.UserShopID = int.Parse(this.Request["userShopID"]);
					modelUser.UserGroupID = int.Parse(this.Request["authority"]);
					modelUser.UserRemark = this.Request["userRemark"];
					modelUser.UserCreateTime = DateTime.Now;
					flag = bllUser.Update(modelUser);
				}
				if (flag > 0)
				{
					flag = 1;
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(modelUser.UserShopID);
					Chain.Model.SysGroup modelGroup = new Chain.BLL.SysGroup().GetModel(modelUser.UserGroupID);
					PubFunction.SaveSysLog(intUserID, 3, "用户编辑", string.Concat(new string[]
					{
						"用户编辑,用户名称：[",
						modelUser.UserName,
						"],用户所属商家：[",
						modelShop.ShopName,
						"],所属权限组：[",
						modelGroup.GroupName,
						"]"
					}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void ChangeUserPwd()
		{
			int flag = 0;
			int UserID = Convert.ToInt32(this.Request["userID"]);
			string newPassWord = this.Request["userPassword"].ToString();
			try
			{
				Chain.BLL.SysUser bllSysUser = new Chain.BLL.SysUser();
				Chain.Model.SysUser mdSysUser = new Chain.Model.SysUser();
				mdSysUser = bllSysUser.GetModel(UserID);
				mdSysUser.UserPassword = DESEncrypt.Encrypt(newPassWord);
				flag = bllSysUser.Update(mdSysUser);
				PubFunction.SaveSysLog(UserID, 3, "用户修改密码", "用户修改密码,用户名称：" + mdSysUser.UserName, this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch
			{
				flag = -2;
			}
			this.Context.Response.Write(flag);
		}

		public void GetUserInfo()
		{
			string msgResponse = "";
			try
			{
				int userID = int.Parse(this.Request["userID"]);
				DataTable dt = new Chain.BLL.SysUser().GetList(" UserID=" + userID).Tables[0];
				msgResponse = JsonPlus.ToJson(dt.Rows[0], "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void UserDel()
		{
			string msgResponse = "0";
			try
			{
				int UserID = int.Parse(this.Request["UserID"].ToString());
				Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
				DataTable dtIsCanDel = bllUser.GetIsCanDelUser(UserID).Tables[0];
				if (dtIsCanDel.Rows.Count > 0)
				{
					if (dtIsCanDel.Rows[0]["strResultsList"].ToString() != "")
					{
						msgResponse = dtIsCanDel.Rows[0]["strResultsList"].ToString();
					}
				}
				Chain.Model.SysUser modelUser = bllUser.GetModel(UserID);
				if (msgResponse == "0")
				{
					if (bllUser.Delete(UserID))
					{
						msgResponse = "1";
						PubFunction.SaveSysLog(this.UserModel.UserID, 2, "用户删除", "删除用户,字段名称:" + modelUser.UserName, this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch
			{
				msgResponse = "-3";
			}
			msgResponse = "{\"result\":\"" + msgResponse + "\"}";
			this.Context.Response.Write(msgResponse);
		}

		public void GetStaffList()
		{
			string msgResponse = "";
			try
			{
				string strQuery = this.Request["txtQuery"].ToString();
				string strShopID = this.Request["ShopID"].ToString();
				string strClassID = this.Request["ClassID"].ToString();
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append(" Staff.StaffClassID=StaffClass.ClassID and  StaffClass.ClassShopID=SysShop.ShopID ");
				if (strQuery != "")
				{
					sbWhere.AppendFormat(" and ((StaffName like '{0}') or (StaffNumber like '{0}') or (StaffMobile like '{0}')) ", strQuery);
				}
				if (strShopID != "")
				{
					sbWhere.AppendFormat(" and ClassShopID={0} ", strShopID);
				}
				if (strClassID != "")
				{
					sbWhere.AppendFormat(" and ClassID={0} ", strClassID);
				}
				Chain.BLL.Staff bllStaff = new Chain.BLL.Staff();
				DataTable dtStaff = bllStaff.GetList(PubFunction.GetShopAuthority(this.UserModel.UserShopID, "ClassShopID", sbWhere.ToString())).Tables[0];
				if (dtStaff.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtStaff, "StaffID,StaffNumber,StaffName,StaffSex,StaffMobile,StaffAddress,ClassName,ShopName,StaffRemark");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetStaff()
		{
			string msgResponse = "";
			try
			{
				int intStaffID = (this.Request["StaffID"].ToString() != "") ? int.Parse(this.Request["StaffID"].ToString()) : 0;
				Chain.BLL.Staff bllStaff = new Chain.BLL.Staff();
				DataTable dtStaff = bllStaff.GetList(" StaffID=" + intStaffID + " and Staff.StaffClassID=StaffClass.ClassID and  StaffClass.ClassShopID=SysShop.ShopID").Tables[0];
				if (dtStaff.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtStaff, "StaffID,StaffNumber,StaffName,StaffSex,StaffMobile,StaffAddress,StaffClassID,ClassName,ShopName,StaffRemark");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetSltStaff()
		{
			StringBuilder sbHtml = new StringBuilder();
			int intUserShopID = this.UserModel.UserShopID;
			string strShopAuth = " SysShop.ShopID = StaffClass.ClassShopID and StaffClass.ClassID=Staff.StaffClassID ";
			strShopAuth = strShopAuth + " and ClassShopID=" + intUserShopID;
			Chain.BLL.Staff bllStaff = new Chain.BLL.Staff();
			DataTable dtStaff = bllStaff.GetList(strShopAuth).Tables[0];
			DataView dvStaff = dtStaff.DefaultView;
			dvStaff.Sort = " StaffClassID ASC";
			dtStaff = dvStaff.ToTable();
			sbHtml.Append("<select id='sltStaff' style='width:70px;'>");
			for (int i = 0; i < dtStaff.Rows.Count; i++)
			{
				sbHtml.Append(string.Concat(new object[]
				{
					"<option value=",
					dtStaff.Rows[i]["ClassPercent"],
					">",
					dtStaff.Rows[i]["StaffName"],
					"</option>"
				}));
			}
			sbHtml.Append("</select>");
			string strMsg = "{\"sltStaffDetail\":\"" + sbHtml + "\"}";
			this.Context.Response.Write(strMsg);
		}

		public void StaffAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.Staff modelStaff = new Chain.Model.Staff();
				int intStaffClassID = (this.Request["StaffClass"].ToString() != "") ? int.Parse(this.Request["StaffClass"].ToString()) : 0;
				bool bolStaffSex = this.Request["StaffSex"].ToString() == "0";
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelStaff.StaffNumber = this.Request["StaffNumber"].ToString();
				modelStaff.StaffName = this.Request["StaffName"].ToString();
				modelStaff.StaffSex = bolStaffSex;
				modelStaff.StaffClassID = intStaffClassID;
				modelStaff.StaffMobile = this.Request["StaffMobile"].ToString();
				modelStaff.StaffAddress = this.Request["StaffAddress"].ToString();
				modelStaff.StaffRemark = this.Request["StaffRemark"].ToString();
				Chain.BLL.Staff bllStaff = new Chain.BLL.Staff();
				flag = bllStaff.Add(modelStaff);
				PubFunction.SaveSysLog(intUserID, 1, "员工新增", "新增员工，员工姓名：[" + modelStaff.StaffName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch
			{
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void StaffEdit()
		{
			int flag = 0;
			try
			{
				Chain.Model.Staff modelStaff = new Chain.Model.Staff();
				int intStaffID = (this.Request["StaffID"].ToString() != "") ? int.Parse(this.Request["StaffID"].ToString()) : 0;
				int intStaffClassID = (this.Request["StaffClass"].ToString() != "") ? int.Parse(this.Request["StaffClass"].ToString()) : 0;
				bool bolStaffSex = this.Request["StaffSex"].ToString() == "0";
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelStaff.StaffID = intStaffID;
				modelStaff.StaffNumber = this.Request["StaffNumber"].ToString();
				modelStaff.StaffName = this.Request["StaffName"].ToString();
				modelStaff.StaffSex = bolStaffSex;
				modelStaff.StaffClassID = intStaffClassID;
				modelStaff.StaffMobile = this.Request["StaffMobile"].ToString();
				modelStaff.StaffAddress = this.Request["StaffAddress"].ToString();
				modelStaff.StaffRemark = this.Request["StaffRemark"].ToString();
				Chain.BLL.Staff bllStaff = new Chain.BLL.Staff();
				flag = bllStaff.Update(modelStaff);
				PubFunction.SaveSysLog(intUserID, 3, "员工编辑", "编辑员工，员工姓名：[" + modelStaff.StaffName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch
			{
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void StaffDelete()
		{
			int flag = 0;
			try
			{
				int intStaffID = (this.Request["StaffID"].ToString() != "") ? int.Parse(this.Request["StaffID"].ToString()) : 0;
				Chain.BLL.Staff bllStaff = new Chain.BLL.Staff();
				Chain.Model.Staff modelStaff = bllStaff.GetModel(intStaffID);
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				if (bllStaff.Delete(intStaffID))
				{
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 2, "员工删除", "删除员工，员工姓名：[" + modelStaff.StaffName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GetStaffClass()
		{
			string msgResponse = "";
			try
			{
				int intClassID = (this.Request["ClassID"].ToString() != "") ? int.Parse(this.Request["ClassID"].ToString()) : 0;
				Chain.BLL.StaffClass bllClass = new Chain.BLL.StaffClass();
				DataTable dtClass = bllClass.GetList(" ClassID=" + intClassID + " and StaffClass.ClassShopID=SysShop.ShopID").Tables[0];
				if (dtClass.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtClass, "ClassID,ClassName,ClassType,ClassPercent,ClassShopID,ClassRemark,ShopName");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void StaffClassAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.StaffClass modelClass = new Chain.Model.StaffClass();
				int intClassShopID = (this.Request["ClassShopID"].ToString() != "") ? int.Parse(this.Request["ClassShopID"].ToString()) : 0;
				bool bolClassType = this.Request["ClassType"].ToString() == "0";
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelClass.ClassName = this.Request["ClassName"].ToString();
				modelClass.ClassType = bolClassType;
				modelClass.ClassPercent = ((this.Request["ClassPercent"].ToString() != "") ? decimal.Parse(this.Request["ClassPercent"].ToString()) : 0m);
				modelClass.ClassShopID = intClassShopID;
				modelClass.ClassRemark = this.Request["ClassRemark"].ToString();
				Chain.BLL.StaffClass bllClass = new Chain.BLL.StaffClass();
				flag = bllClass.Add(modelClass);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 1, "部门新增", "新增部门，部门名称：[" + modelClass.ClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -2;
			}
			this.Context.Response.Write(flag);
		}

		public void StaffClassEdit()
		{
			int flag = 0;
			try
			{
				Chain.Model.StaffClass modelClass = new Chain.Model.StaffClass();
				int intClassID = (this.Request["ClassID"].ToString() != "") ? int.Parse(this.Request["ClassID"].ToString()) : 0;
				int intClassShopID = (this.Request["ClassShopID"].ToString() != "") ? int.Parse(this.Request["ClassShopID"].ToString()) : 0;
				bool bolClassType = this.Request["ClassType"].ToString() == "0";
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelClass.ClassID = intClassID;
				modelClass.ClassName = this.Request["ClassName"].ToString();
				modelClass.ClassType = bolClassType;
				modelClass.ClassPercent = ((this.Request["ClassPercent"].ToString() != "") ? decimal.Parse(this.Request["ClassPercent"].ToString()) : 0m);
				modelClass.ClassShopID = intClassShopID;
				modelClass.ClassRemark = this.Request["ClassRemark"].ToString();
				Chain.BLL.StaffClass bllClass = new Chain.BLL.StaffClass();
				flag = bllClass.Update(modelClass);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 3, "部门编辑", "编辑部门，部门名称：[" + modelClass.ClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -2;
			}
			this.Context.Response.Write(flag);
		}

		public void StaffClassDelete()
		{
			int flag = 0;
			try
			{
				int intClassID = (this.Request["ClassID"].ToString() != "") ? int.Parse(this.Request["ClassID"].ToString()) : 0;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.BLL.StaffClass bllClass = new Chain.BLL.StaffClass();
				Chain.Model.StaffClass modelClass = bllClass.GetModel(intClassID);
				flag = bllClass.Delete(intClassID);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 2, "部门删除", "删除部门，部门名称：[" + modelClass.ClassName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void NoticeAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.SysNotice modelNotice = new Chain.Model.SysNotice();
				modelNotice.SysNoticeCode = this.Request["NoticeCode"];
				modelNotice.SysNoticeTime = DateTime.Parse(DateTime.Now.ToShortDateString());
				modelNotice.SysNotieceName = this.Request["RelaseName"];
				modelNotice.SysNoticeTitle = this.Request["NoticeTitle"];
				string strDetail = this.Request["NoticeDetail"];
				modelNotice.SysNoticeDetail = strDetail;
				flag = new Chain.BLL.SysNotice().Add(modelNotice);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(this.UserModel.UserID, 1, "公告新增", string.Concat(new string[]
					{
						"系统公告新增,发布人：[",
						modelNotice.SysNotieceName,
						"],公告标题：[",
						modelNotice.SysNoticeTitle,
						"]"
					}), this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				Chain.BLL.SysError.Add(e, PubFunction.ipAdress);
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void NoticeEdit()
		{
			int flag = 0;
			try
			{
				Chain.Model.SysNotice modelNotice = new Chain.Model.SysNotice();
				modelNotice.SysNoticeID = int.Parse(this.Request["NoticeID"]);
				modelNotice.SysNoticeCode = this.Request["NoticeCode"];
				modelNotice.SysNoticeTime = DateTime.Parse(DateTime.Now.ToShortDateString());
				modelNotice.SysNotieceName = this.Request["RelaseName"];
				modelNotice.SysNoticeTitle = this.Request["NoticeTitle"];
				string strDetail = this.Request["NoticeDetail"];
				modelNotice.SysNoticeDetail = strDetail;
				flag = new Chain.BLL.SysNotice().Update(modelNotice);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(this.UserModel.UserID, 3, "公告编辑", string.Concat(new string[]
					{
						"系统公告编辑,发布人：[",
						modelNotice.SysNotieceName,
						"],公告标题：[",
						modelNotice.SysNoticeTitle,
						"]"
					}), this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GetNoticeInfo()
		{
			string msgResponse = "";
			try
			{
				int NoticeID = int.Parse(this.Request["NoticeID"]);
				DataTable dt = new Chain.BLL.SysNotice().GetList(" SysNoticeID=" + NoticeID).Tables[0];
				msgResponse = JsonPlus.ToJson(dt.Rows[0], "");
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void NoticeDel()
		{
			int flag = 0;
			try
			{
				int NoticeID = int.Parse(this.Request["NoticeID"].ToString());
				Chain.BLL.SysNotice bllNotice = new Chain.BLL.SysNotice();
				Chain.Model.SysNotice modelNotice = bllNotice.GetModel(NoticeID);
				if (bllNotice.Delete(NoticeID))
				{
					flag = 1;
					PubFunction.SaveSysLog(this.UserModel.UserID, 2, "公告删除", "删除系统公告,公告更改人:" + modelNotice.SysNotieceName, this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void CustomFieldAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.MemCustomField modelMemCustom = new Chain.Model.MemCustomField();
				string CustomFieldType = this.Request["CustomFieldType"].ToString();
				modelMemCustom.CustomType = int.Parse(this.Request["CustomType"]);
				modelMemCustom.CustomFieldName = this.Request["CustomName"].ToString();
				modelMemCustom.CustomField = this.Request["CustomCode"].ToString();
				modelMemCustom.CustomFieldType = CustomFieldType;
				modelMemCustom.CustomFieldIsNull = (this.Request["isNull"] == "true");
				modelMemCustom.CustomFieldIsShow = (this.Request["isShow"] == "true");
				modelMemCustom.CustomFieldCreateTime = DateTime.Now;
				modelMemCustom.CustomFieldShopID = this.UserModel.UserShopID;
				modelMemCustom.CustomFieldUserID = this.UserModel.UserID;
				modelMemCustom.CustomFieldInfo = this.Request["CustomInfo"].ToString();
				flag = new Chain.BLL.MemCustomField().Add(modelMemCustom);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(modelMemCustom.CustomFieldUserID, 1, "自定义属性新增", string.Concat(new string[]
					{
						"新增自定义属性,属性名称:[",
						modelMemCustom.CustomFieldName,
						"] ,代码:[",
						modelMemCustom.CustomField,
						"]"
					}), modelMemCustom.CustomFieldShopID, modelMemCustom.CustomFieldCreateTime, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void CustomFieldEdit()
		{
			int flag = 0;
			try
			{
				Chain.Model.MemCustomField modelMemCustom = new Chain.Model.MemCustomField();
				modelMemCustom.CustomFieldID = int.Parse(this.Request["CustomFieldID"]);
				modelMemCustom.CustomType = int.Parse(this.Request["CustomType"]);
				modelMemCustom.CustomFieldName = this.Request["CustomName"].ToString();
				modelMemCustom.CustomField = this.Request["CustomCode"].ToString();
				modelMemCustom.CustomFieldType = this.Request["CustomFieldType"].ToString();
				modelMemCustom.CustomFieldIsNull = (this.Request["isNull"] == "true");
				modelMemCustom.CustomFieldIsShow = (this.Request["isShow"] == "true");
				modelMemCustom.CustomFieldCreateTime = DateTime.Now;
				modelMemCustom.CustomFieldShopID = this.UserModel.UserShopID;
				modelMemCustom.CustomFieldUserID = this.UserModel.UserID;
				modelMemCustom.CustomFieldInfo = this.Request["CustomInfo"].ToString();
				flag = new Chain.BLL.MemCustomField().Update(modelMemCustom);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(modelMemCustom.CustomFieldUserID, 3, "自定义属性编辑", string.Concat(new string[]
					{
						"自定义属性编辑,属性名称:[",
						modelMemCustom.CustomFieldName,
						"] ,代码:[",
						modelMemCustom.CustomField,
						"]"
					}), modelMemCustom.CustomFieldShopID, modelMemCustom.CustomFieldCreateTime, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GetCustomField()
		{
			string msgResponse = "";
			try
			{
				int CustomFieldID = int.Parse(this.Request["CustomFieldID"]);
				DataTable dt = new Chain.BLL.MemCustomField().GetList(" CustomFieldID=" + CustomFieldID).Tables[0];
				msgResponse = JsonPlus.ToJson(dt.Rows[0], "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void CustomFieldDel()
		{
			int flag = 0;
			try
			{
				int CustomFieldID = int.Parse(this.Request["CustomFieldID"].ToString());
				Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
				Chain.Model.MemCustomField modelCustomField = bllCustomField.GetModel(CustomFieldID);
				DataTable dt = new DataTable();
				if (modelCustomField.CustomType == 1)
				{
					dt = new Chain.BLL.Mem().GetList(string.Concat(new string[]
					{
						"len(",
						modelCustomField.CustomField,
						")!=0 and ",
						modelCustomField.CustomField,
						" is not null"
					})).Tables[0];
				}
				else
				{
					dt = new Chain.BLL.Goods().GetList(string.Concat(new string[]
					{
						"len(",
						modelCustomField.CustomField,
						")!=0 and ",
						modelCustomField.CustomField,
						" is not null"
					})).Tables[0];
				}
				if (dt.Rows.Count > 0)
				{
					flag = -2;
				}
				else if (bllCustomField.Delete(CustomFieldID))
				{
					flag = 1;
					PubFunction.SaveSysLog(this.UserModel.UserID, 2, "自定义属性删除", "删除自定义属性,字段名称:" + modelCustomField.CustomFieldName, this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void CustomRemindAdd()
		{
			try
			{
				string CustomReminder = this.Request["CustomReminder"].ToString();
				CustomReminder = CustomReminder.Remove(CustomReminder.LastIndexOf(","), 1);
				Chain.Model.SysCustomRemind modelRemind = new Chain.Model.SysCustomRemind();
				modelRemind.CustomRemindTitle = this.Request["CustomRemindTitle"].ToString();
				modelRemind.CustomRemindDetail = this.Request["CustomRemindDetail"].ToString();
				modelRemind.CustomReminder = CustomReminder;
				modelRemind.CustomRemindTime = DateTime.Parse(this.Request["CustomRemindTime"].ToString());
				modelRemind.CustomRemindCreateTime = DateTime.Now;
				modelRemind.CustomRemindShopID = this.UserModel.UserShopID;
				modelRemind.CustomRemindUserID = this.UserModel.UserID;
				int flag = new Chain.BLL.SysCustomRemind().Add(modelRemind);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(modelRemind.CustomRemindUserID, 1, "自定义提醒新增", string.Concat(new object[]
					{
						"新增自定义提醒,标题:[",
						modelRemind.CustomRemindTitle,
						"] ,创建时间:[",
						modelRemind.CustomRemindCreateTime,
						"]"
					}), modelRemind.CustomRemindShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
			}
		}

		public void CustomRemindDel()
		{
			int flag = 0;
			try
			{
				int CustomRemindID = int.Parse(this.Request["CustomRemindID"]);
				Chain.BLL.SysCustomRemind bllRemind = new Chain.BLL.SysCustomRemind();
				Chain.Model.SysCustomRemind modelRemind = bllRemind.GetModel(CustomRemindID);
				if (bllRemind.Delete(CustomRemindID))
				{
					flag = 1;
					PubFunction.SaveSysLog(this.UserModel.UserID, 2, "自定义提醒删除", "删除自定义提醒,标题:" + modelRemind.CustomRemindTitle, this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void DataBakUp()
		{
			string bakUpName = this.Request["bakUpName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
			Chain.BLL.SysParameter par = new Chain.BLL.SysParameter();
			string fileName = this.Server.MapPath("../AppData/DataBase/");
			if (!Directory.Exists(fileName))
			{
				Directory.CreateDirectory(fileName);
			}
			Regex regx = new Regex("\\\\$");
			this.Context.Response.Write(par.DataBakUp(regx.Replace(fileName.Trim(), "") + "\\" + bakUpName + ".bak"));
		}

		public void DataBakUpDel()
		{
			string bakUpName = this.Request["bakUpName"].ToString();
			string basePath;
			if (bakUpName.Contains("Aoto"))
			{
				basePath = this.Server.MapPath("../AppData/AutoDataBase/");
			}
			else
			{
				basePath = this.Server.MapPath("../AppData/DataBase/");
			}
			if (File.Exists(basePath + bakUpName))
			{
				File.Delete(basePath + bakUpName);
			}
			this.Context.Response.Write(1);
		}

		public void ReductionDataBakUp()
		{
			string bakUpName = this.Request["bakUpName"].ToString();
			string basePath;
			if (bakUpName.Contains("Aoto"))
			{
				basePath = this.Server.MapPath("../AppData/AutoDataBase/");
			}
			else
			{
				basePath = this.Server.MapPath("../AppData/DataBase/");
			}
			if (File.Exists(basePath + bakUpName))
			{
				Chain.BLL.SysParameter par = new Chain.BLL.SysParameter();
				par.ReductionDataBakUp(basePath + bakUpName);
			}
			this.Context.Response.Write(1);
		}

		public void CleanSysLog()
		{
			try
			{
				Chain.BLL.SysLog bllSysLog = new Chain.BLL.SysLog();
				int intCleanTime = int.Parse(this.Request["cleanTime"].ToString());
				int intCleanSys = bllSysLog.CleadSysLog(intCleanTime);
				if (intCleanSys > 0)
				{
					PubFunction.SaveSysLog(this.UserModel.UserID, 2, "系统日志", "系统日志清理", this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
		}

		public void DBInit()
		{
			int flag = 0;
			try
			{
				string strRemark = "";
				string strType = this.Request["type"].ToString();
				ArrayList arr = new ArrayList();
				Chain.BLL.SysLog bllSysLog = new Chain.BLL.SysLog();
				string text = strType;
				if (text != null)
				{
					if (!(text == "empty"))
					{
						if (text == "Restore")
						{
							//string[] strRestore = new string[1];
							//strRestore = Regex.Split(Resource.TestData, "\\r\\n");
							strRemark = "系统数据还原到测试数据";
							//string[] array = strRestore;
							//for (int i = 0; i < array.Length; i++)
							//{
							//	string str = array[i];
							//	if (str.IndexOf("--") != 0 && str.Trim() != "")
							//	{
							//		arr.Add(str.Trim());
							//	}
							//}
                            arr.Add("exec Proc_TestData");
						}
					}
					else
					{
						//string[] strEmpty = new string[1];
						//strEmpty = Regex.Split(Resource.ClearTable, "\\r\\n");
						strRemark = "系统数据全部清空";
                        /*	string[] array = strEmpty;
                            for (int i = 0; i < array.Length; i++)
                            {
                                string str = array[i];
                                if (str.IndexOf("--") != 0 && str.Trim() != "")
                                {
                                    arr.Add(str.Trim());
                                }
                            }*/
                        arr.Add("exec Proc_DBinit");
					}
				}
				if (bllSysLog.DataBaseInit(arr))
				{
					flag = 1;
					PubFunction.SaveSysLog(this.UserModel.UserID, 4, "系统数据", strRemark, this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GetMessageListPage()
		{
			string flag = "";
			string pageSize = (this.Request["size"] != null) ? this.Request["size"].ToString() : "";
			string pageIndex = (this.Request["index"] != null) ? this.Request["index"].ToString() : "";
			int memID = (this.Request["memID"] != null) ? int.Parse(this.Request["memID"].ToString()) : 0;
			string strSql = " MessageMemID=" + memID;
			strSql += " and [Message].MessageMemID = Mem.MemID ";
			int resCount;
			DataTable dt = this.bllMessage.GetListSP(int.Parse(pageSize), int.Parse(pageIndex), out resCount, new string[]
			{
				strSql.ToString()
			}).Tables[0];
			if (dt != null)
			{
				string jsonStr = JsonPlus.ToJson(dt, "");
				flag = string.Concat(new object[]
				{
					"{\"RecordCount\":",
					resCount,
					",\"List\":",
					jsonStr,
					"}"
				});
			}
			this.Context.Response.Write(flag);
		}

		public void MemMessageDel()
		{
			int flag = 0;
			try
			{
				int intMemID = int.Parse(this.Request["memID"]);
				string arg_4D_0 = (this.Request["memName"] != "") ? this.Request["memName"] : "";
				this.modelMem = this.bllMem.GetModel(intMemID);
				if (this.bllMessage.MessageDel(intMemID) > 0)
				{
					flag = 1;
					PubFunction.SaveSysLog(this.UserModel.UserID, 2, "会员留言", "卡号：[" + this.modelMem.MemCard + "]的会员留言全部删除", this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void MessageReply()
		{
			int flag = 0;
			try
			{
				int intMessageID = (this.Request["messageID"] != null) ? int.Parse(this.Request["messageID"].ToString()) : 0;
				string strContent = (this.Request["content"] != null) ? this.Request["content"].ToString() : "";
				this.modelMessage.MessageID = intMessageID;
				this.modelMessage.MessageReplyContent = strContent;
				this.modelMessage.MessageReplyTime = DateTime.Now;
				this.modelMessage.MessageReplyUserID = this.UserModel.UserID;
				this.modelMessage.MessageIsReply = 1;
				if (this.bllMessage.Reply(this.modelMessage))
				{
					flag = 1;
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void MessageDel()
		{
			int flag = 0;
			try
			{
				int intMessageID = int.Parse(this.Request["messageID"].ToString());
				this.modelMessage = this.bllMessage.GetModel(intMessageID);
				this.modelMem = this.bllMem.GetModel(this.modelMessage.MessageMemID);
				if (this.bllMessage.Delete(intMessageID))
				{
					flag = 1;
					PubFunction.SaveSysLog(this.UserModel.UserID, 2, "会员留言", "卡号：[" + this.modelMem.MemCard + "]的会员留言删除", this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GetShopList()
		{
			string msgResponse = "";
			try
			{
				string strShopAuth = " ShopID>0 and ShopType=3 ";
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				int intShopID = int.Parse(this.Request["shopid"].ToString());
				int fid = int.Parse(this.Request["fid"].ToString());
				Chain.Model.SysShop modelShop = bllShop.GetModel(intShopID);
				if (fid == -1 && modelShop.ShopType == 1)
				{
					object obj = strShopAuth;
					strShopAuth = string.Concat(new object[]
					{
						obj,
						" and (( IsMain=0 and  FatherShopID=",
						fid,
						") or ( IsMain=1 ))"
					});
				}
				else
				{
					object obj = strShopAuth;
					strShopAuth = string.Concat(new object[]
					{
						obj,
						" and (( IsMain=0 and  FatherShopID=",
						fid,
						") )"
					});
				}
				DataTable dtShopAuth = PubFunction.GetShopAuthority(intShopID);
				if (dtShopAuth.Rows.Count > 0)
				{
					strShopAuth = strShopAuth + " and ShopID in (" + dtShopAuth.Rows[0]["ShopAuthorityData"].ToString() + ") and ShopState='false'";
				}
				DataTable dt = bllShop.GetList(strShopAuth).Tables[0];
				if (dt.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dt, "");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetNextName()
		{
			string msgResponse = "";
			try
			{
				int intPID = int.Parse(this.Request["pid"].ToString());
				DataTable dt = new Chain.BLL.SysArea().GetList("PID=" + intPID).Tables[0];
				if (dt.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dt, "");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void MemInfoExpense()
		{
			string flag = "";
			string key = this.Request["key"].ToString();
			int intSize = int.Parse(this.Request["size"].ToString());
			int intIndex = int.Parse(this.Request["index"].ToString());
			int intUserShopID = this.UserModel.UserShopID;
			StringBuilder strSql = new StringBuilder();
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			strSql.AppendFormat("(MemCard='{0}' or MemName ='{0}' or  MemMobile='{0}') ", key);
			strSql.Append(" and OrderType!=3 and OrderType!=4 and OrderType!=5 and OrderType!=6 ");
			strSql.Append("  and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderMemID = Mem.MemID  and OrderLog.OrderUserID = SysUser.UserID");
			int counts;
			DataTable dtMemExpense = this.bllOrderLog.GetListSP(intSize, intIndex, out counts, new string[]
			{
				PubFunction.GetShopAuthority(intUserShopID, "OrderShopID", strSql.ToString())
			}).Tables[0];
			if (dtMemExpense != null)
			{
				string Response = JsonPlus.ToJson(dtMemExpense, "MemCard,MemName,OrderAccount,OrderTotalMoney,OrderDiscountMoney,OrderPoint,OrderCreateTime,ShopName,UserName,OrderType,OrderID");
				flag = string.Concat(new object[]
				{
					"{\"MemInfoExpense\":",
					Response,
					",\"RecordCount\":",
					counts,
					"}"
				});
			}
			this.Context.Response.Write(flag);
		}

		public void GetExpenseDetail()
		{
			string flag = "";
			int intOrderID = int.Parse(this.Request["orderID"].ToString());
			string strSql = " OrderDetail.OrderID=OrderLog.OrderID and OrderDetail.GoodsID=Goods.GoodsID";
			strSql = strSql + " and OrderDetail.OrderID=" + intOrderID;
			Chain.BLL.OrderDetail bllOrderDetail = new Chain.BLL.OrderDetail();
			DataTable dtDetail = bllOrderDetail.GetListSP(strSql).Tables[0];
			if (dtDetail != null)
			{
				string Response = JsonPlus.ToJson(dtDetail, "");
				flag = "{\"MemInfoExpenseDeatil\":" + Response + "}";
			}
			this.Context.Response.Write(flag);
		}

		public void MemInfoRecharge()
		{
			string flag = "";
			string key = this.Request["key"].ToString();
			int intUserShopID = this.UserModel.UserShopID;
			StringBuilder strSql = new StringBuilder();
			Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();
			strSql.AppendFormat("RechargeIsApprove=1 AND (MemCard='{0}' or MemName ='{0}' or  MemMobile='{0}') ", key);
			strSql.Append(" and MemRecharge.RechargeShopID = SysShop.ShopID and MemRecharge.RechargeMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemRecharge.RechargeUserID = SysUser.UserID");
			int counts = 0;
			DataTable dtMemRecharge = bllMemRecharge.GetListSP(50, 1, out counts, new string[]
			{
				PubFunction.GetShopAuthority(intUserShopID, "RechargeShopID", strSql.ToString())
			}).Tables[0];
			if (dtMemRecharge != null)
			{
				string Response = JsonPlus.ToJson(dtMemRecharge, "MemCard,MemName,RechargeAccount,RechargeMoney,RechargeRemark,RechargeCreateTime,ShopName,UserName");
				flag = "{\"MemInfoRecharge\":" + Response + "}";
			}
			this.Context.Response.Write(flag);
		}

		public void MemInfoRechargeCount()
		{
			string flag = "";
			try
			{
				string key = this.Request["key"].ToString();
				int intSize = int.Parse(this.Request["size"].ToString());
				int intIndex = int.Parse(this.Request["index"].ToString());
				int intUserShopID = this.UserModel.UserShopID;
				StringBuilder strSql = new StringBuilder();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				strSql.AppendFormat("(MemCard='{0}' or MemName ='{0}' or  MemMobile='{0}') ", key);
				strSql.Append("  and MemCount.CountShopID = SysShop.ShopID and MemCount.CountMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemCount.CountUserID = SysUser.UserID ");
				int counts;
				DataTable dtMemRechargeCount = new Chain.BLL.MemCount().GetListSP(intSize, intIndex, out counts, new string[]
				{
					PubFunction.GetShopAuthority(intUserShopID, "CountShopID", strSql.ToString())
				}).Tables[0];
				if (dtMemRechargeCount != null)
				{
					string Response = JsonPlus.ToJson(dtMemRechargeCount, "MemCard,MemName,ShopName,CountID,CountMemID,CountAccount,CountTotalMoney,CountDiscountMoney,CountPayCard,CountPayCash,CountPayCoupon,CountPayType,CountPoint,CountRemark,CountShopID,CountCreateTime,CountUserID,UserName");
					flag = string.Concat(new object[]
					{
						"{\"MemInfoRechargeCount\":",
						Response,
						",\"RecordCount\":",
						counts,
						"}"
					});
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void GetRechargeCountDetail()
		{
			string flag = "";
			try
			{
				int intCountID = int.Parse(this.Request["CountID"].ToString());
				string strSql = " MemCountDetail.CountDetailCountID=" + intCountID;
				DataTable dtDetail = new Chain.BLL.MemCountDetail().GetList(strSql).Tables[0];
				if (dtDetail != null)
				{
					string Response = JsonPlus.ToJson(dtDetail, "");
					flag = "{\"MemInfoRechargeCountDeatil\":" + Response + "}";
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void MemInfoExchangeGift()
		{
			string flag = "";
			string key = this.Request["key"].ToString();
			int intUserShopID = this.UserModel.UserShopID;
			StringBuilder strSql = new StringBuilder();
			Chain.BLL.PointExchange bllExchange = new Chain.BLL.PointExchange();
            strSql.AppendFormat("(MemCard='{0}' or GiftExchange.MemName ='{0}' or  MemMobile='{0}') ", key);
			strSql.Append(" and GiftExchange.ExchangeStatus=2 and GiftExchange.MemID=Mem.MemID and GiftExchange.ExchangeUserID = SysUser.UserID and Mem.MemShopID = SysShop.ShopID ");
			int counts = 0;
			DataTable dtMemExchangeGift = bllExchange.GetListSP(50, 1, out counts, new string[]
			{
				PubFunction.GetShopAuthority(intUserShopID, "MemShopID", strSql.ToString())
			}).Tables[0];
			if (dtMemExchangeGift != null)
			{
				string Response = JsonPlus.ToJson(dtMemExchangeGift, "ExchangeAccount,MemCard,MemName,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ExchangeTime,ShopName,UserName,ExchangeType,ExchangeID,ExchangeStatus");
				flag = "{\"MemInfoExchangeGift\":" + Response + "}";
			}
			this.Context.Response.Write(flag);
		}

		public void MemInfoCoupon()
		{
			string Response = "";
			try
			{
				string key = this.Request["key"].ToString();
				int intUserShopID = this.UserModel.UserShopID;
				StringBuilder strSql = new StringBuilder();
				strSql.AppendFormat("(MemCard='{0}' or MemName ='{0}' or  MemMobile='{0}') ", key);
				strSql.AppendFormat(" and Mem.MemID=CouponList.CouPonMID ", new object[0]);
				int counts = 0;
				DataTable dtCoupon = new Chain.BLL.CouponList().GetMemInfoListSP(1000, 1, out counts, new string[]
				{
					PubFunction.GetShopAuthority(intUserShopID, "MemShopID", strSql.ToString())
				}).Tables[0];
				if (dtCoupon != null)
				{
					Response = JsonPlus.ToJson(dtCoupon, "CID,CouPonID,CouPon,CouPonYF,CouPonSY,CouPonMID,ConPonSendTime,ConPonUseTime,CouPonOrderAccount");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(Response);
		}

		public void MemInfoMobile()
		{
			string Response = "";
			try
			{
				string key = this.Request["key"].ToString();
				int intUserShopID = this.UserModel.UserShopID;
				StringBuilder strSql = new StringBuilder();
				strSql.AppendFormat("(MemCard='{0}' or MemName ='{0}' or  MemMobile='{0}') ", key);
				int counts = 0;
				DataTable dtCoupon = new Chain.BLL.ScreenPopUp().GetScreenPopUpList(1000, 1, out counts, new string[]
				{
					PubFunction.GetShopAuthority(intUserShopID, "CallerShopID", strSql.ToString())
				}).Tables[0];
				if (dtCoupon != null)
				{
					Response = JsonPlus.ToJson(dtCoupon, "CallerID,CallerMemID,CallerIsMem,CallerState,CallerDuration,CallerRemark,CallerCreateTime,MemMobile,CallerMobile,ShopID,UserID,MemID,MemCard,MemName,UserName,ShopName");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(Response);
		}

		public void GetMemBirthday()
		{
			string msgResponse = "";
			int day = (this.Request["day"] != null) ? int.Parse(this.Request["day"]) : 0;
			int intUserShopID = this.UserModel.UserShopID;
			DataTable dt = new Chain.BLL.Mem().GetBirthdayList(day, intUserShopID).Tables[0];
			if (dt != null)
			{
				msgResponse = JsonPlus.ToJson(dt, "MemID,MemCard,MemName,MemMobile,MemBirthday,LevelName,MemPoint,ShopName");
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetMemPointReset()
		{
			string msgResponse = "";
			int time = (this.Request["time"] != null) ? int.Parse(this.Request["time"]) : 0;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("MemID<>''");
			if (time >= 0)
			{
				strSql.AppendFormat(" and MemPoint>0 and  DATEDIFF(day,isnull(MemConsumeLastTime,MemCreateTime),getdate()) >= '{0}' and MemShopID = '{1}' ", time, this.UserModel.UserShopID);
			}
			DataTable dt = new Chain.BLL.Mem().GetMemPointReset(strSql.ToString(), 1).Tables[0];
			if (dt != null)
			{
				msgResponse = JsonPlus.ToJson(dt, "MemID,MemCard,MemName,LevelName,MemPoint,MemMobile,MemPastTime,MemConsumeLastTime,MemConsumeCount,ShopName");
			}
			this.Context.Response.Write(msgResponse);
		}

		public void MemPointClear()
		{
			int flag = 0;
			int MemID = (this.Request["MemID"] != null) ? int.Parse(this.Request["MemID"].ToString()) : 0;
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			if (bllMem.ClearMemberPoint(MemID))
			{
				flag = 1;
			}
			this.Context.Response.Write(flag);
		}

		public void GetCustomRemindList()
		{
			string msgResponse = "";
			int intUserID = this.UserModel.UserID;
			DataTable dt = new Chain.BLL.SysCustomRemind().GetList(" CustomReminder like '%" + PubFunction.UserIDTOName(intUserID) + "%' and DATEDIFF(day,CustomRemindTime,getdate())<=0 ").Tables[0];
			if (dt != null)
			{
				msgResponse = JsonPlus.ToJson(dt, "CustomRemindTitle,CustomRemindDetail,CustomRemindTime,CustomRemindCreateTime,ShopName,UserName");
			}
			this.Context.Response.Write(msgResponse);
		}

		public void UserPwdEdit()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				string oldPwd = this.Request["oldPwd"];
				string newPwd = this.Request["newPwd"];
				string newRePwd = this.Request["newRePwd"];
				Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
				Chain.Model.SysUser modelUser = bllUser.GetModel(intUserID);
				oldPwd = DESEncrypt.Encrypt(oldPwd);
				newPwd = DESEncrypt.Encrypt(newPwd);
				flag = bllUser.UpdateUserPwd(intUserID, oldPwd, newPwd);
				if (flag == 1)
				{
					PubFunction.SaveSysLog(intUserID, 3, "用户密码编辑", "编辑用户密码,用户名称：[" + modelUser.UserName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -3;
			}
			this.Context.Response.Write(flag);
		}

		public void GetCommonData()
		{
			StringBuilder sb = new StringBuilder();
			DataTable dtLevel = new Chain.BLL.MemLevel().GetList("").Tables[0];
			foreach (DataRow dr in dtLevel.Rows)
			{
				if (sb.ToString() != "")
				{
					sb.Append(",");
				}
				sb.Append(string.Concat(new string[]
				{
					"{\"LevelID\":",
					dr["LevelID"].ToString(),
					",\"LevelName\":\"",
					dr["LevelName"].ToString(),
					"\",\"Point\":\"",
					dr["LevelPoint"].ToString(),
					"\",\"Percent\":\"",
					dr["LevelDiscountPercent"].ToString(),
					"\"}"
				}));
			}
			string flag = sb.ToString();
			this.Context.Response.Write(flag);
		}

		public void CheckOnlineUser()
		{
			int flag = -2;
			if (PubFunction.curParameter.RestrainOnlineNumber > 0)
			{
				string ip = (this.Request.Headers["x-forwarded-for"] == null) ? this.Request.ServerVariables["remote_addr"].ToString() : this.Request.UserHostAddress;
				flag = OnlineBiz.IsValid(this.UserModel.UserID, ip, this.Request.UserAgent);
				if (flag == 0)
				{
					Dictionary<int, OnlineMember> _onlineMember = OnlineBiz._onlineMember;
				}
				if (flag != 1)
				{
					this.Context.Session.Clear();
				}
			}
			this.Context.Response.Write(flag);
		}

		public void CheckHardwareID()
		{
			string Safety = this.Request["Safety"].ToString();
			Hashtable HardwareID = (Hashtable)this.Context.Application.Get("HardwareID");
			if (HardwareID.ContainsValue(Safety))
			{
				this.Context.Response.Write(0);
			}
			else
			{
				this.Context.Response.Write(1);
			}
		}

		public void GetUserName()
		{
			string flag = "";
			try
			{
				string usernumber = (this.Request["Number"].Length < 0) ? "" : this.Request["Number"].ToString();
				Chain.BLL.SysUser bllsysuserwork = new Chain.BLL.SysUser();
				DataTable dtuser = bllsysuserwork.GetList(string.Format(" UserNumber = '{0}'", usernumber)).Tables[0];
				flag = JsonPlus.ToJson(dtuser, "");
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void AddUserWork()
		{
			int flag = 0;
			try
			{
				Chain.BLL.SysUser blluser = new Chain.BLL.SysUser();
				Chain.Model.SysUserWork muserwork = new Chain.Model.SysUserWork();
				Chain.BLL.SysUserWork blluserwork = new Chain.BLL.SysUserWork();
				muserwork.UserID = Convert.ToInt32(this.Request["hdUserID"].ToString());
				muserwork.StartTime = Convert.ToDateTime(this.Request["hdStartTime"]);
				muserwork.EedTime = DateTime.Now;
				muserwork.AddNewUser = Convert.ToInt32(this.Request["lblMemNumber"].ToString().Replace("名", ""));
				muserwork.CardMoney = Convert.ToDecimal(this.Request["lblCardMoney"].ToString().Replace("￥", ""));
				muserwork.ExpenseSumMoneys = Convert.ToDecimal(this.Request["lblExpenseSumMoneys"].ToString().Replace("￥", ""));
				muserwork.ExpenseBinkMoneys = Convert.ToDecimal(this.Request["lblExpenseBinkMoneys"].ToString().Replace("￥", ""));
				muserwork.ExpenseCouponMoneys = Convert.ToDecimal(this.Request["lblExpenseCouponMoneys"].ToString().Replace("￥", ""));
				muserwork.SRechargeMoney = Convert.ToDecimal(this.Request["lblSRechargeMoney"].ToString().Replace("￥", ""));
				muserwork.FRechargeMoney = Convert.ToDecimal(this.Request["lblFRechargeMoney"].ToString().Replace("￥", ""));
				muserwork.RechargeBank = Convert.ToDecimal(this.Request["lblRechargeBank"].ToString().Replace("￥", ""));
				muserwork.FRechargeGiveMoney = Convert.ToDecimal(this.Request["lblFRechargeGiveMoney"].ToString().Replace("￥", ""));
				muserwork.AllMoneys = Convert.ToDecimal(this.Request["hdyjjg"]);
				muserwork.sjMoneys = Convert.ToDecimal(this.Request["txtsjMoneys"]);
				muserwork.HandoverUserID = Convert.ToInt32(this.Request["hdjjid"]);
				muserwork.Arrearage = Convert.ToDecimal(this.Request["hdye"]);
				muserwork.Ispay = false;
				flag = blluserwork.Add(muserwork);
				if (flag > 0)
				{
					DbHelperSQL.ExecuteSql(string.Format("update SysUserWork set ispay = '1' where HandoverUserID = '{0}'", Convert.ToInt32(this.Request["hdUserID"].ToString())));
					flag = 1;
					PubFunction.SaveSysLog(Convert.ToInt32(this.Request["hdUserID"].ToString()), 1, "管理员换班", "管理员换班,接班人：[" + blluser.GetModel(Convert.ToInt32(this.Request["hdjjid"])).UserName + "]", Convert.ToInt32(this.Request["hdShopID"]), DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void CheckCustomFieldRemind()
		{
			DataTable dt = new Chain.BLL.SysCustomRemind().GetCustomField().Tables[0];
			if (dt.Rows.Count > 0)
			{
				string msgResponse = JsonPlus.ToJson(dt, "CustomRemindDetail");
				this.Context.Response.Write(msgResponse);
			}
		}

		public void GetGoodsStockOut()
		{
			string flag = "";
			try
			{
				int number = Convert.ToInt32(this.Request["myday"]);
				DataTable dtGoods = new Chain.BLL.Goods().GetStockRemind(string.Format("Number < = '{0}' and GoodsType = '0' and ShopID = '{1}'", number, this.UserModel.UserShopID)).Tables[0];
				flag = JsonPlus.ToJson(dtGoods, "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void MemPassTime()
		{
			string flag = "";
			try
			{
				DataTable dtMemPastTime = this.bllMem.GetMemPastTime(string.Format(" and DATEDIFF(day,getdate(),MemPastTime) <= '{0}' ", this.Request["myday"].ToString()), this.UserModel.UserShopID).Tables[0];
				flag = JsonPlus.ToJson(dtMemPastTime, "");
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void UpdateWebchatConfig()
		{
			bool WeiXinType = !(this.Request["WeiXinType"].ToString() == "0");
			bool WeiXinVerified = !(this.Request["WeiXinVerified"].ToString() == "0");
			string WeiXinToken = this.Request["WeiXinToken"];
			string WeiXinEncodingAESKey = this.Request["WeiXinEncodingAESKey"];
			string WeiXinShopName = this.Request["WeiXinShopName"];
			string WeiXinSalutatory = this.Request["WeiXinSalutatory"];
			string WeiXinAppID = this.Request["WeiXinAppID"];
			string WeiXinAppSecret = this.Request["WeiXinAppSecret"];
			bool WeiXinSMSVcode = bool.Parse(this.Request["WeiXinSMSVcode"]);
			int SignInPoint = int.Parse(this.Request["SignInPoint"]);
			int Pay = int.Parse(this.Request["Pay"]);
			string MchId = this.Request["MchId"];
			string Api = this.Request["Api"];
			decimal Xiane = (this.Request["Xiane"] != "") ? decimal.Parse(this.Request["Xiane"]) : 0m;
			Chain.BLL.SysParameter bllParameter = new Chain.BLL.SysParameter();
			Chain.Model.SysParameter modelParameter = bllParameter.GetModel(1);
			if (modelParameter != null)
			{
				modelParameter.Pay = Pay;
				if (WeiXinType && WeiXinVerified && Pay == 1)
				{
					modelParameter.MchId = MchId;
					modelParameter.Api = Api;
					modelParameter.Xiane = Xiane;
				}
				else
				{
					modelParameter.MchId = "";
					modelParameter.Api = "";
					modelParameter.Xiane = 0m;
				}
				modelParameter.WeiXinType = WeiXinType;
				modelParameter.WeiXinVerified = WeiXinVerified;
				modelParameter.WeiXinToken = WeiXinToken;
				modelParameter.WeiXinEncodingAESKey = WeiXinEncodingAESKey;
				modelParameter.WeiXinShopName = WeiXinShopName;
				modelParameter.WeiXinSalutatory = WeiXinSalutatory;
				modelParameter.WeiXinAppID = WeiXinAppID;
				modelParameter.WeiXinAppSecret = WeiXinAppSecret;
				modelParameter.WeiXinSMSVcode = WeiXinSMSVcode;
				modelParameter.SignInPoint = SignInPoint;
				if (bllParameter.Update(modelParameter))
				{
					PubFunction pub = new PubFunction();
					PubFunction.curParameter = pub.LoadSysParameter();
					PubFunction.SaveSysLog(this.UserModel.UserID, 3, "微信参数设置", "微信参数设置", this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
					this.Response.Write("1");
				}
			}
		}

		public void GetAccessToken()
		{
			string errorCode = string.Empty;
			string errorMassage = string.Empty;
			string appId = this.Request["appId"].ToString();
			string appSecret = this.Request["appSecret"].ToString();
            string partern = this.Request["partern"].ToString();
            string wxpaykey = this.Request["wxpaykey"].ToString();
            string accessToken = AccessToken.Get(appId, appSecret, out errorCode, out errorMassage);
			if (!string.IsNullOrEmpty(accessToken))
			{
				this.Response.Write(accessToken);
			}
			else
			{
				this.Response.Write("0");
			}
		}

		public void TextRuleAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.WeiXinRule ruleModel = new Chain.Model.WeiXinRule();
				ruleModel.RuleNUmber = this.Request["RuleNUmber"];
				ruleModel.RuleNewsType = "text";
				ruleModel.RuleDesc = this.Request["RuleDesc"];
				ruleModel.RuleContent = this.Request["RuleContent"];
				ruleModel.RuleUserID = this.UserModel.UserID;
				ruleModel.RuleCreateTime = DateTime.Now;
				int ruleNumber = 0;
				if (int.TryParse(this.Request["RuleNUmber"], out ruleNumber) && ruleNumber >= 1 && ruleNumber <= 3)
				{
					flag = -1;
				}
				if (new Chain.BLL.WeiXinRule().GetList("RuleNUmber='" + ruleModel.RuleNUmber + "' ").Tables[0].Rows.Count > 0)
				{
					flag = -1;
				}
				if (flag != -1)
				{
					flag = new Chain.BLL.WeiXinRule().Add(ruleModel);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void TextRuleDel()
		{
			int flag = 0;
			try
			{
				int RuleID = int.Parse(this.Request["RuleID"]);
				Chain.BLL.WeiXinMenu WeiXinMenuBll = new Chain.BLL.WeiXinMenu();
				if (WeiXinMenuBll.GetUseCountByRuleID(RuleID) > 0)
				{
					flag = -1;
				}
				else
				{
					flag = (new Chain.BLL.WeiXinRule().Delete(RuleID) ? 1 : 0);
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void TextRuleEdit()
		{
			int flag = 0;
			try
			{
				Chain.Model.WeiXinRule ruleModel = new Chain.Model.WeiXinRule();
				Chain.BLL.WeiXinRule WeiXinRuleBll = new Chain.BLL.WeiXinRule();
				ruleModel.RuleID = int.Parse(this.Request["RuleID"]);
				ruleModel.RuleNUmber = this.Request["RuleNUmber"];
				ruleModel.RuleNewsType = "text";
				ruleModel.RuleDesc = this.Request["RuleDesc"];
				ruleModel.RuleContent = this.Request["RuleContent"];
				ruleModel.RuleUserID = this.UserModel.UserID;
				ruleModel.RuleCreateTime = DateTime.Now;
				string sqlWhere = string.Concat(new object[]
				{
					"RuleID != ",
					ruleModel.RuleID,
					" and RuleNUmber='",
					ruleModel.RuleNUmber,
					"' "
				});
				if (WeiXinRuleBll.GetList(sqlWhere).Tables[0].Rows.Count > 0)
				{
					flag = -1;
				}
				else
				{
					Chain.Model.WeiXinRule WeiXinRuleOldModel = WeiXinRuleBll.GetModel(ruleModel.RuleID);
					flag = (WeiXinRuleBll.Update(ruleModel) ? 1 : 0);
					if (WeiXinRuleOldModel.RuleNUmber != ruleModel.RuleNUmber)
					{
						new Chain.BLL.WeiXinMenu().UpdateMenuKey(WeiXinRuleOldModel.RuleNUmber, ruleModel.RuleNUmber);
					}
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void GetTextRuleByID()
		{
			string flag = "";
			try
			{
				int RuleID = int.Parse(this.Request["RuleID"]);
				DataTable textRuleTable = new Chain.BLL.WeiXinRule().GetList("RuleID=" + RuleID + " ").Tables[0];
				flag = JsonPlus.ToJson(textRuleTable, "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void NewsRuleAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.WeiXinRule ruleModel = new Chain.Model.WeiXinRule();
				ruleModel.RuleNUmber = this.Request["RuleNUmber"];
				ruleModel.RuleNewsType = "news";
				ruleModel.RuleUserID = this.UserModel.UserID;
				ruleModel.RuleCreateTime = DateTime.Now;
				ruleModel.RuleDesc = this.Request["RuleDesc"];
				int ruleNumber = 0;
				if (int.TryParse(this.Request["RuleNUmber"], out ruleNumber) && ruleNumber >= 1 && ruleNumber <= 3)
				{
					flag = -1;
				}
				if (new Chain.BLL.WeiXinRule().GetList("RuleNUmber='" + ruleModel.RuleNUmber + "' ").Tables[0].Rows.Count > 0)
				{
					flag = -1;
				}
				if (flag != -1)
				{
					flag = new Chain.BLL.WeiXinRule().Add(ruleModel);
					if (flag > 0)
					{
						Chain.Model.WeiXinNews newsModel = new Chain.Model.WeiXinNews();
						newsModel.NewsRuleID = flag;
						newsModel.NewsTitle = this.Request["NewsTitle"];
						newsModel.NewsDesc = this.Request["NewsDesc"];
						newsModel.NewsUrlFirst = this.Request["NewsUrlFirst"];
						newsModel.NewsUrlSecond = this.Request["NewsUrlSecond"];
						newsModel.NewsLinkContent = this.Request["NewsLinkContent"];
						newsModel.NewsCreateTime = DateTime.Now;
						flag = new Chain.BLL.WeiXinNews().Add(newsModel);
						newsModel.NewsID = flag;
						newsModel.NewsUrlSecond = string.Concat(new object[]
						{
							"http://",
							PubFunction.curParameter.strDoMain,
							"/WeiXin/WeiXinNewsLink.aspx?NewsID=",
							flag
						});
						flag = (new Chain.BLL.WeiXinNews().Update(newsModel) ? 1 : 0);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void NewsAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.WeiXinNews newsModel = new Chain.Model.WeiXinNews();
				newsModel.NewsRuleID = int.Parse(this.Request["NewsRuleID"]);
				newsModel.NewsTitle = this.Request["NewsTitle"];
				newsModel.NewsDesc = this.Request["NewsDesc"];
				newsModel.NewsUrlFirst = this.Request["NewsUrlFirst"];
				newsModel.NewsUrlSecond = this.Request["NewsUrlSecond"];
				newsModel.NewsLinkContent = this.Request["NewsLinkContent"];
				newsModel.NewsLinkContent = newsModel.NewsLinkContent;
				newsModel.NewsCreateTime = DateTime.Now;
				flag = new Chain.BLL.WeiXinNews().Add(newsModel);
				newsModel.NewsID = flag;
				newsModel.NewsUrlSecond = string.Concat(new object[]
				{
					"http://",
					PubFunction.curParameter.strDoMain,
					"/WeiXin/WeiXinNewsLink.aspx?NewsID=",
					flag
				});
				flag = (new Chain.BLL.WeiXinNews().Update(newsModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void GetNewsRuleByID()
		{
			string flag = "";
			try
			{
				int NewsID = int.Parse(this.Request["NewsID"]);
				DataTable newsRuleTable = new Chain.BLL.WeiXinNews().GetList("NewsID=" + NewsID + " ").Tables[0];
				newsRuleTable.Rows[0]["NewsLinkContent"] = newsRuleTable.Rows[0]["NewsLinkContent"].ToString();
				flag = JsonPlus.ToJson(newsRuleTable, "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void NewsRuleDel()
		{
			int flag = 0;
			try
			{
				int RuleID = int.Parse(this.Request["RuleID"]);
				Chain.BLL.WeiXinMenu WeiXinMenuBll = new Chain.BLL.WeiXinMenu();
				if (WeiXinMenuBll.GetUseCountByRuleID(RuleID) > 0)
				{
					flag = -1;
				}
				else
				{
					List<Chain.Model.WeiXinNews> listNewsModel = new Chain.BLL.WeiXinNews().GetModelList("NewsRuleID=" + RuleID);
					if (listNewsModel != null)
					{
						flag = 1;
						foreach (Chain.Model.WeiXinNews item in listNewsModel)
						{
							string fileName = Path.GetFileName(item.NewsUrlFirst);
							string fullPath = this.Context.Server.MapPath("~/Upload/WeiXin/Images/" + fileName);
							if (File.Exists(fullPath))
							{
								File.Delete(fullPath);
							}
							flag = (new Chain.BLL.WeiXinNews().Delete(item.NewsID) ? 1 : 0);
						}
						flag = (new Chain.BLL.WeiXinRule().Delete(RuleID) ? 1 : 0);
					}
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void NewsDel()
		{
			int flag = 0;
			try
			{
				int NewsID = int.Parse(this.Request["NewsID"]);
				Chain.Model.WeiXinNews newsModel = new Chain.BLL.WeiXinNews().GetModel(NewsID);
				if (newsModel != null)
				{
					flag = 1;
					string fileName = Path.GetFileName(newsModel.NewsUrlFirst);
					string fullPath = this.Context.Server.MapPath("~/Upload/WeiXin/Images/" + fileName);
					if (File.Exists(fullPath))
					{
						File.Delete(fullPath);
					}
					flag = (new Chain.BLL.WeiXinNews().Delete(newsModel.NewsID) ? 1 : 0);
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void NewsRuleEdit()
		{
			int flag = 0;
			try
			{
				Chain.BLL.WeiXinRule WeiXinRuleBll = new Chain.BLL.WeiXinRule();
				Chain.Model.WeiXinRule ruleModel = new Chain.Model.WeiXinRule();
				ruleModel.RuleID = int.Parse(this.Request["RuleID"]);
				ruleModel.RuleNUmber = this.Request["RuleNUmber"];
				ruleModel.RuleNewsType = "news";
				ruleModel.RuleUserID = this.UserModel.UserID;
				ruleModel.RuleCreateTime = DateTime.Now;
				ruleModel.RuleDesc = this.Request["RuleDesc"];
				Chain.Model.WeiXinRule WeiXinRuleOldModel = WeiXinRuleBll.GetModel(ruleModel.RuleID);
				string sqlWhere = string.Concat(new object[]
				{
					"RuleID != ",
					ruleModel.RuleID,
					" and RuleNUmber='",
					ruleModel.RuleNUmber,
					"' "
				});
				if (WeiXinRuleBll.GetList(sqlWhere).Tables[0].Rows.Count > 0)
				{
					flag = -1;
				}
				else
				{
					flag = (WeiXinRuleBll.Update(ruleModel) ? 1 : 0);
					if (WeiXinRuleOldModel.RuleNUmber != ruleModel.RuleNUmber)
					{
						new Chain.BLL.WeiXinMenu().UpdateMenuKey(WeiXinRuleOldModel.RuleNUmber, ruleModel.RuleNUmber);
					}
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void NewsEdit()
		{
			int flag = 0;
			try
			{
				Chain.Model.WeiXinNews newsModel = new Chain.Model.WeiXinNews();
				newsModel.NewsID = int.Parse(this.Request["NewsID"]);
				newsModel.NewsRuleID = int.Parse(this.Request["NewsRuleID"]);
				newsModel.NewsTitle = this.Request["NewsTitle"];
				newsModel.NewsDesc = this.Request["NewsDesc"];
				newsModel.NewsUrlFirst = this.Request["NewsUrlFirst"];
				newsModel.NewsUrlSecond = string.Concat(new object[]
				{
					"http://",
					PubFunction.curParameter.strDoMain,
					"/WeiXin/WeiXinNewsLink.aspx?NewsID=",
					newsModel.NewsID
				});
				newsModel.NewsLinkContent = this.Request["NewsLinkContent"];
				newsModel.NewsLinkContent = newsModel.NewsLinkContent;
				newsModel.NewsCreateTime = DateTime.Now;
				flag = (new Chain.BLL.WeiXinNews().Update(newsModel) ? 1 : 0);
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void GetMemChartIndex()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string ShopID = (this.Request["ShopID"] == null) ? "" : this.Request["ShopID"].ToString();
			string Interval = "";
			string Mydata = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "新增会员数统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "MemShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND MemShopID = '{0}' ", ShopID);
			}
			try
			{
				DataTable dtMemTable = new Chain.BLL.Mem().GetDataByTime(StartTime, EndTime, strwhere).Tables[0];
				foreach (DataRow dr in dtMemTable.Rows)
				{
					Mydata = Mydata + dr["Number"].ToString() + ",";
				}
				Mydata = Mydata.Remove(Mydata.Length - 1);
				while (StartTime <= EndTime)
				{
					Interval = Interval + StartTime.Day.ToString() + ",";
					StartTime = StartTime.AddDays(1.0);
				}
				Interval = Interval.Remove(Interval.Length - 1);
				string msgResponse = string.Concat(new object[]
				{
					"{\"Success\":\"",
					1,
					"\",\"Interval\":\"",
					Interval,
					"\",\"Mydata\":\"",
					Mydata,
					"\",\"title\":\"",
					title,
					"\"}"
				});
				this.Context.Response.Write(msgResponse);
			}
			catch (Exception e)
			{
				this.LogError(e);
				this.Context.Response.Write("");
			}
		}

		public void GetMemChart()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string ShopID = this.Request["ShopID"].ToString();
			string Interval = "";
			string Mydata = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "新增会员数统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "MemShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND MemShopID = '{0}' ", ShopID);
			}
			try
			{
				DataTable dtMemTable = new Chain.BLL.Mem().GetDataByTime(StartTime, EndTime, strwhere).Tables[0];
				foreach (DataRow dr in dtMemTable.Rows)
				{
					Mydata = Mydata + dr["Number"].ToString() + ",";
				}
				Mydata = Mydata.Remove(Mydata.Length - 1);
				while (StartTime <= EndTime)
				{
					Interval = Interval + StartTime.ToString("yyyy-MM-dd") + ",";
					StartTime = StartTime.AddDays(1.0);
				}
				Interval = Interval.Remove(Interval.Length - 1);
				string msgResponse = string.Concat(new object[]
				{
					"{\"Success\":\"",
					1,
					"\",\"Interval\":\"",
					Interval,
					"\",\"Mydata\":\"",
					Mydata,
					"\",\"title\":\"",
					title,
					"\"}"
				});
				this.Context.Response.Write(msgResponse);
			}
			catch
			{
				this.Context.Response.Write("");
			}
		}

		public void GetDrawMoney()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string ShopID = this.Request["ShopID"].ToString();
			string Interval = "";
			string Mydata = "";
			string PracticalMoney = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "会员提现统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "DrawMoneyShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND DrawMoneyShopID = '{0}' ", ShopID);
			}
			try
			{
				DataTable dtMemTable = new Chain.BLL.MemDrawMoney().GetDataByTime(StartTime, EndTime, strwhere).Tables[0];
				foreach (DataRow dr in dtMemTable.Rows)
				{
					Mydata = Mydata + dr["AllMoney"].ToString() + ",";
					PracticalMoney = PracticalMoney + dr["PracticalMoney"].ToString() + ",";
				}
				Mydata = Mydata.Remove(Mydata.Length - 1);
				PracticalMoney = PracticalMoney.Remove(PracticalMoney.Length - 1);
				while (StartTime <= EndTime)
				{
					Interval = Interval + StartTime.ToString("yyyy-MM-dd") + ",";
					StartTime = StartTime.AddDays(1.0);
				}
				Interval = Interval.Remove(Interval.Length - 1);
				string msgResponse = string.Concat(new object[]
				{
					"{\"Success\":\"",
					1,
					"\",\"Interval\":\"",
					Interval,
					"\",\"Mydata\":\"",
					Mydata,
					"\",\"PracticalMoney\":\"",
					PracticalMoney,
					"\",\"title\":\"",
					title,
					"\"}"
				});
				this.Context.Response.Write(msgResponse);
			}
			catch
			{
				this.Context.Response.Write("");
			}
		}

		public void GetRecharge()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string ShopID = this.Request["ShopID"].ToString();
			string Interval = "";
			string Mydata = "";
			string MyCshcz = "";
			string MyPayBink = "";
			string MyPayCash = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "会员充值统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "RechargeShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND RechargeShopID = '{0}' ", ShopID);
			}
			try
			{
				DataTable dtRecharge = new Chain.BLL.MemRecharge().GetRechargeByTime(StartTime, EndTime, strwhere).Tables[0];
				foreach (DataRow dr in dtRecharge.Rows)
				{
					Mydata = Mydata + dr["AllMoney"].ToString() + ",";
					MyCshcz = MyCshcz + dr["Cshcz"].ToString() + ",";
					MyPayBink = MyPayBink + dr["PayBink"].ToString() + ",";
					MyPayCash = MyPayCash + dr["PayCash"].ToString() + ",";
				}
				Mydata = Mydata.Remove(Mydata.Length - 1);
				MyCshcz = MyCshcz.Remove(MyCshcz.Length - 1);
				MyPayBink = MyPayBink.Remove(MyPayBink.Length - 1);
				MyPayCash = MyPayCash.Remove(MyPayCash.Length - 1);
				while (StartTime <= EndTime)
				{
					Interval = Interval + StartTime.ToString("yyyy-MM-dd") + ",";
					StartTime = StartTime.AddDays(1.0);
				}
				Interval = Interval.Remove(Interval.Length - 1);
				string msgResponse = string.Concat(new object[]
				{
					"{\"Success\":\"",
					1,
					"\",\"Interval\":\"",
					Interval,
					"\",\"Mydata\":\"",
					Mydata,
					"\",\"MyCshcz\":\"",
					MyCshcz,
					"\",\"MyPayBink\":\"",
					MyPayBink,
					"\",\"MyPayCash\":\"",
					MyPayCash,
					"\",\"title\":\"",
					title,
					"\"}"
				});
				this.Context.Response.Write(msgResponse);
			}
			catch
			{
				this.Context.Response.Write("");
			}
		}

		public void GetPointChart()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string ShopID = this.Request["ShopID"].ToString();
			string Interval = "";
			string NewPoint = "";
			string CutPoint = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "积分变动统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "PointShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND PointShopID = '{0}' ", ShopID);
			}
			try
			{
				DataTable dtRecharge = new Chain.BLL.PointLog().GetPointByTime(StartTime, EndTime, strwhere).Tables[0];
				foreach (DataRow dr in dtRecharge.Rows)
				{
					NewPoint = NewPoint + dr["NewPoint"].ToString() + ",";
					CutPoint = CutPoint + dr["CutPoint"].ToString() + ",";
				}
				NewPoint = NewPoint.Remove(NewPoint.Length - 1);
				CutPoint = CutPoint.Remove(CutPoint.Length - 1);
				while (StartTime <= EndTime)
				{
					Interval = Interval + StartTime.ToString("yyyy-MM-dd") + ",";
					StartTime = StartTime.AddDays(1.0);
				}
				Interval = Interval.Remove(Interval.Length - 1);
				string msgResponse = string.Concat(new object[]
				{
					"{\"Success\":\"",
					1,
					"\",\"Interval\":\"",
					Interval,
					"\",\"NewPoint\":\"",
					NewPoint,
					"\",\"CutPoint\":\"",
					CutPoint,
					"\",\"title\":\"",
					title,
					"\"}"
				});
				this.Context.Response.Write(msgResponse);
			}
			catch
			{
				this.Context.Response.Write("");
			}
		}

		public void GetOrderChart()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string strMemCard = this.Request["MemCard"].ToString();
			string ShopID = this.Request["ShopID"].ToString();
			string Interval = "";
			string TotalMoney = "";
			string TotalDiscount = "";
			string Coupon = "";
			string Point = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "会员消费统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "OrderShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND OrderShopID = '{0}' ", ShopID);
			}
			if (strMemCard != "")
			{
				DataTable dt = new Chain.BLL.Mem().GetList(string.Format("MemCard='{0}' or MemName ='{0}' or  MemMobile='{0}' or MemCardNumber like '%{0}%' ", strMemCard)).Tables[0];
				if (dt.Rows.Count > 0)
				{
					strwhere += string.Format(" AND OrderMemID = {0}", dt.Rows[0]["MemID"]);
				}
			}
			try
			{
				DataTable dtRecharge = new Chain.BLL.OrderLog().GetOrderByTime(StartTime, EndTime, strwhere).Tables[0];
				foreach (DataRow dr in dtRecharge.Rows)
				{
					TotalMoney = TotalMoney + dr["TotalMoney"].ToString() + ",";
					TotalDiscount = TotalDiscount + dr["TotalDiscount"].ToString() + ",";
					Coupon = Coupon + dr["Coupon"].ToString() + ",";
					Point = Point + dr["Point"].ToString() + ",";
				}
				TotalMoney = TotalMoney.Remove(TotalMoney.Length - 1);
				TotalDiscount = TotalDiscount.Remove(TotalDiscount.Length - 1);
				Coupon = Coupon.Remove(Coupon.Length - 1);
				Point = Point.Remove(Point.Length - 1);
				while (StartTime <= EndTime)
				{
					Interval = Interval + StartTime.ToString("yyyy-MM-dd") + ",";
					StartTime = StartTime.AddDays(1.0);
				}
				Interval = Interval.Remove(Interval.Length - 1);
				string msgResponse = string.Concat(new object[]
				{
					"{\"Success\":\"",
					1,
					"\",\"Interval\":\"",
					Interval,
					"\",\"TotalMoney\":\"",
					TotalMoney,
					"\",\"TotalDiscount\":\"",
					TotalDiscount,
					"\",\"Coupon\":\"",
					Coupon,
					"\",\"Point\":\"",
					Point,
					"\",\"title\":\"",
					title,
					"\"}"
				});
				this.Context.Response.Write(msgResponse);
			}
			catch
			{
				this.Context.Response.Write("");
			}
		}

		public void GetStaffMoneyChart()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string ShopID = this.Request["ShopID"].ToString();
			string Interval = "";
			string Mydata = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "员工提成统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "ShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND ShopID = '{0}' ", ShopID);
			}
			try
			{
				DataTable dtMemTable = new Chain.BLL.StaffMoney().GetStaffMoneyByTime(StartTime, EndTime, strwhere).Tables[0];
				foreach (DataRow dr in dtMemTable.Rows)
				{
					Mydata = Mydata + dr["AllMoney"].ToString() + ",";
				}
				Mydata = Mydata.Remove(Mydata.Length - 1);
				while (StartTime <= EndTime)
				{
					Interval = Interval + StartTime.ToString("yyyy-MM-dd") + ",";
					StartTime = StartTime.AddDays(1.0);
				}
				Interval = Interval.Remove(Interval.Length - 1);
				string msgResponse = string.Concat(new object[]
				{
					"{\"Success\":\"",
					1,
					"\",\"Interval\":\"",
					Interval,
					"\",\"Mydata\":\"",
					Mydata,
					"\",\"title\":\"",
					title,
					"\"}"
				});
				this.Context.Response.Write(msgResponse);
			}
			catch
			{
				this.Context.Response.Write("");
			}
		}

		public void GetGiftExchangeChart()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string ShopID = this.Request["ShopID"].ToString();
			string Interval = "";
			string AllPoint = "";
			string MainPoint = "";
			string SelfPoint = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "积分兑换统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "UserShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND UserShopID = '{0}' ", ShopID);
			}
			try
			{
				DataTable dtRecharge = new Chain.BLL.GiftExchange().GetGiftExchangeByTime(StartTime, EndTime, strwhere).Tables[0];
				foreach (DataRow dr in dtRecharge.Rows)
				{
					AllPoint = AllPoint + dr["AllPoint"].ToString() + ",";
					MainPoint = MainPoint + dr["MainPoint"].ToString() + ",";
					SelfPoint = SelfPoint + dr["SelfPoint"].ToString() + ",";
				}
				AllPoint = AllPoint.Remove(AllPoint.Length - 1);
				MainPoint = MainPoint.Remove(MainPoint.Length - 1);
				SelfPoint = SelfPoint.Remove(SelfPoint.Length - 1);
				while (StartTime <= EndTime)
				{
					Interval = Interval + StartTime.ToString("yyyy-MM-dd") + ",";
					StartTime = StartTime.AddDays(1.0);
				}
				Interval = Interval.Remove(Interval.Length - 1);
				string msgResponse = string.Concat(new object[]
				{
					"{\"Success\":\"",
					1,
					"\",\"Interval\":\"",
					Interval,
					"\",\"AllPoint\":\"",
					AllPoint,
					"\",\"MainPoint\":\"",
					MainPoint,
					"\",\"SelfPoint\":\"",
					SelfPoint,
					"\",\"title\":\"",
					title,
					"\"}"
				});
				this.Context.Response.Write(msgResponse);
			}
			catch
			{
				this.Context.Response.Write("");
			}
		}

		public void GetGoodsExpenseChart()
		{
			DateTime StartTime = Convert.ToDateTime(this.Request["StartTime"]).Date;
			DateTime EndTime = Convert.ToDateTime(this.Request["EndTime"]).Date;
			string GoodsCode = this.Request["GoodsCode"].ToString();
			string ShopID = this.Request["ShopID"].ToString();
			string Interval = "";
			string Mydata = "";
			string GeneralMoney = "";
			string ServeMoney = "";
			string title = StartTime.ToString("yyyy-MM-dd") + "到" + EndTime.ToString("yyyy-MM-dd") + "商品消费统计";
			string strwhere = PubFunction.GetShopAuthority(this.UserModel.UserShopID, "OrderShopID", " and 1=1 ");
			if (!string.IsNullOrEmpty(ShopID))
			{
				strwhere += string.Format(" AND OrderShopID = '{0}' ", ShopID);
			}
			try
			{
				Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
				string msgResponse;
				if (!string.IsNullOrEmpty(GoodsCode) && !bllGoods.Exists(GoodsCode))
				{
					msgResponse = "0";
				}
				else
				{
					DataTable dtMemTable = new Chain.BLL.OrderLog().GetGoodsExpense(StartTime, EndTime, GoodsCode, strwhere).Tables[0];
					foreach (DataRow dr in dtMemTable.Rows)
					{
						Mydata = Mydata + dr["AllMoney"].ToString() + ",";
						GeneralMoney = GeneralMoney + dr["GeneralMoney"].ToString() + ",";
						ServeMoney = ServeMoney + dr["ServeMoney"].ToString() + ",";
					}
					Mydata = Mydata.Remove(Mydata.Length - 1);
					GeneralMoney = GeneralMoney.Remove(GeneralMoney.Length - 1);
					ServeMoney = ServeMoney.Remove(ServeMoney.Length - 1);
					while (StartTime <= EndTime)
					{
						Interval = Interval + StartTime.ToString("yyyy-MM-dd") + ",";
						StartTime = StartTime.AddDays(1.0);
					}
					Interval = Interval.Remove(Interval.Length - 1);
					msgResponse = string.Concat(new object[]
					{
						"{\"Success\":\"",
						1,
						"\",\"Interval\":\"",
						Interval,
						"\",\"Mydata\":\"",
						Mydata,
						"\",\"GeneralMoney\":\"",
						GeneralMoney,
						"\",\"ServeMoney\":\"",
						ServeMoney,
						"\",\"title\":\"",
						title,
						"\"}"
					});
				}
				this.Context.Response.Write(msgResponse);
			}
			catch
			{
				this.Context.Response.Write("");
			}
		}

		public void GetGoodsClassDiscount()
		{
			string msgResponse = "";
			try
			{
				int classDiscountID = (this.Request["ClassDiscountID"] == null) ? 0 : int.Parse(this.Request["ClassDiscountID"].Trim());
				DataTable dt = new Chain.BLL.GoodsClassDiscount().GetListByClassDiscountID(classDiscountID).Tables[0];
				if (dt != null)
				{
					msgResponse = JsonPlus.ToJson(dt, "LevelPoint,ClassDiscountPercent,ClassPointPercent,ClassName,ClassDiscountID");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(msgResponse);
		}

		public void UpdateGoodsClassDiscount()
		{
			int flag = -1;
			try
			{
				int ClassDiscountID = int.Parse(this.Request["ClassDiscountID"]);
				decimal ClassDiscountPercent = decimal.Parse(this.Request["ClassDiscountPercent"]) / 100m;
				decimal ClassPointPercent;
				if (decimal.Parse(this.Request["ClassPointPercent"]) != 0m)
				{
					ClassPointPercent = 1m / decimal.Parse(this.Request["ClassPointPercent"]);
				}
				else
				{
					ClassPointPercent = 0m;
				}
				Chain.BLL.GoodsClassDiscount GoodsClassDiscountBll = new Chain.BLL.GoodsClassDiscount();
				Chain.Model.GoodsClassDiscount goodsClassDiscountModel = GoodsClassDiscountBll.GetModel(ClassDiscountID);
				goodsClassDiscountModel.ClassDiscountPercent = ClassDiscountPercent;
				goodsClassDiscountModel.ClassPointPercent = ClassPointPercent;
				GoodsClassDiscountBll.Update(goodsClassDiscountModel);
				flag = 0;
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void CleanSysError()
		{
			try
			{
				Chain.BLL.SysError bllSysError = new Chain.BLL.SysError();
				int intCleanTime = int.Parse(this.Request["cleanTime"].ToString());
				int intCleanSys = bllSysError.CleadSysError(intCleanTime);
				if (intCleanSys > 0)
				{
					PubFunction.SaveSysLog(this.UserModel.UserID, 2, "系统日志", "系统日志清理", this.UserModel.UserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
		}

		public void GetSysError()
		{
			string msgResponse = "";
			try
			{
				Chain.BLL.SysError bllSysError = new Chain.BLL.SysError();
				int id = Convert.ToInt32(this.Request["ID"]);
				msgResponse = bllSysError.GetModel(id).ErrorContent;
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void AgainPrintMemRecharge()
		{
			string flag = string.Empty;
			try
			{
				int rechargeID = int.Parse(this.Context.Request["rechargeID"]);
				DataTable dt = new Chain.BLL.MemRecharge().AgainPrint(rechargeID);
				flag = JsonPlus.ToJson(dt, "RechargeMoney,RechargeGive,MemCard,MemName,RechargeCreateTime,UserName,RechargeAccount,MemMoney,RechargeCardBalance");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void AgainPrintPointID()
		{
			string flag = string.Empty;
			try
			{
				int PointID = int.Parse(this.Context.Request["PointID"]);
				Chain.BLL.PointLog pointLog = new Chain.BLL.PointLog();
				DataTable dt = pointLog.AgainPrint(PointID);
				flag = JsonPlus.ToJson(dt, "MemCard,MemName,PointChangeType,PointNumber,MemPoint,UserName,PointCreateTime,PointRemark");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void AgainPrintGiftExchange()
		{
			string flag = string.Empty;
			try
			{
				int rechargeID = int.Parse(this.Context.Request["RechargeID"]);
				if (!string.IsNullOrEmpty(this.Context.Request["GiftExchange"]))
				{
					Chain.BLL.GiftExchange giftExchangeBlL = new Chain.BLL.GiftExchange();
					DataTable dt = giftExchangeBlL.AgainPrint(rechargeID);
					flag = JsonPlus.ToJson(dt, "MemCard,MemName,ExchangeAllPoint,ExchangeAllNumber,MemPoint,UserName,ExchangeTime,ExchangeAccount");
					this.Context.Response.Write(flag);
				}
				else if (!string.IsNullOrEmpty(this.Context.Request["GiftExchangeDetail"]))
				{
					Chain.BLL.GiftExchangeDetail giftExchangeDetail = new Chain.BLL.GiftExchangeDetail();
					DataTable dt = giftExchangeDetail.AgainPrint(rechargeID);
					flag = JsonPlus.ToJson(dt, "GiftName,GiftCode,ExchangeNumber,GiftExchangePoint,ExchangePoint");
					this.Context.Response.Write(flag);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
		}

		public void SendCards()
		{
			string msgResponse = "0";
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				ulong StartCard = Convert.ToUInt64(this.Request["cardstart"]);
				ulong EndCard = Convert.ToUInt64(this.Request["cardend"]);
				int len = this.Request["cardstart"].ToString().Length;
				int memstate = Convert.ToInt32(this.Request["memstate"]);
				string remark = this.Request["remark"].ToString();
				string[] strDelNumber = (this.Request["delcard"].ToString() != "") ? this.Request["delcard"].Split(new char[]
				{
					','
				}) : new string[0];
				string pwd = this.Request["pwd"].ToString();
				int memlevel = (this.Request["memlevel"] == "") ? 0 : Convert.ToInt32(this.Request["memlevel"]);
				int mempoint = (this.Request["mempoint"] == "") ? 0 : Convert.ToInt32(this.Request["mempoint"]);
				decimal memmoney = (this.Request["memmoney"] == "") ? 0m : Convert.ToDecimal(this.Request["memmoney"]);
				string newCard = string.Empty;
				ArrayList allStrs = new ArrayList();
				string strDateNow = DateTime.Now.ToString();
				string strSql = string.Concat(new object[]
				{
					"INSERT [Mem] ( [MemCard] , [MemPassword] , [MemName] , [MemSex] , [MemIdentityCard] , [MemMobile] , [MemPhoto] , [MemBirthdayType] , [MemBirthday] , [MemIsPast] , [MemPastTime] , [MemPoint] , [MemPointAutomatic] , [MemMoney] , [MemEmail] , [MemAddress] , [MemState] , [MemRecommendID] , [MemLevelID] , [MemShopID] , [MemCreateTime] , [MemRemark] , [MemUserID] , [MemTelePhone] , [MemQRCode] , [MemProvince] , [MemCity] , [MemCounty] , [MemVillage] , [MemCardNumber] ) VALUES (  '{0}' , '",
					DESEncrypt.Encrypt(pwd),
					"' , '{0}' , 1 , '' , '' , '' , 1 , '1900-01-01 00:00:00.000' , 0 , '2900-01-01 00:00:00.000' , ",
					mempoint,
					" , 1 , ",
					memmoney,
					" , '' , '' , ",
					memstate,
					" , 0 , ",
					memlevel,
					" , ",
					intUserShopID,
					" , GETDATE() , '",
					remark,
					"' , ",
					intUserID,
					" , '' , '' , '' , '' , '' , '' , '' )"
				});
				string strPointLog = string.Concat(new object[]
				{
					"INSERT INTO PointLog (PointMemID,PointNumber,PointChangeType,PointRemark,PointShopID,PointCreateTime,PointUserID,PointOrderCode,PointGiveMemID) VALUES(IDENT_CURRENT('Mem'),",
					mempoint,
					",5,'会员批量发卡，初始化会员余额',",
					intUserShopID,
					",GETDATE(),",
					intUserID,
					",'",
					PubFunction.curParameter.strMemPointChangePrefix,
					DateTime.Now.ToString("yyMMddHHmmssffff"),
					"'+CONVERT(VARCHAR(200),@@identity),0)"
				});
				string strMemRecharge = string.Concat(new object[]
				{
					"INSERT INTO MemRecharge(RechargeMemID,RechargeAccount,RechargeType,RechargeMoney,RechargeGive,RechargeRemark,RechargeShopID,RechargeCreateTime,RechargeUserID,RechargeCardBalance,RechargeIsApprove) VALUES(IDENT_CURRENT('Mem'),'",
					PubFunction.curParameter.strMemRechargePrefix,
					DateTime.Now.ToString("yyMMddHHmmssffff"),
					"'+CONVERT(VARCHAR(200),IDENT_CURRENT('Mem')),1,",
					memmoney,
					",0,'批量发卡，初始化会员余额',",
					intUserShopID,
					",GETDATE(),",
					intUserID,
					",",
					memmoney,
					",1)"
				});
				string strMoneyChangeLog = string.Concat(new object[]
				{
					"INSERT INTO MoneyChangeLog(MoneyChangeMemID,MoneyChangeUserID,MoneyChangeType,MoneyChangeAccount,MoneyChangeMoney,MoneyChangeCash,MoneyChangeBalance,MoneyChangeUnionPay,MemMoney,MoneyChangeGiveMoney,MoneyChangeCreateTime) VALUES(IDENT_CURRENT('Mem'),",
					intUserID,
					",5,'",
					PubFunction.curParameter.strMemRechargePrefix,
					DateTime.Now.ToString("yyMMddHHmmssffff"),
					"'+CONVERT(VARCHAR(200),IDENT_CURRENT('Mem')),",
					memmoney,
					",",
					memmoney,
					",0,0,",
					memmoney,
					",0,GETDATE())"
				});
				while (StartCard <= EndCard)
				{
					bool isExist = false;
					newCard = StartCard.ToString();
					int differ = len - newCard.Length;
					for (int i = 0; i < differ; i++)
					{
						newCard = "0" + newCard;
					}
					for (int i = 0; i < strDelNumber.Length; i++)
					{
						if (newCard.IndexOf(strDelNumber[i].ToString()) != -1)
						{
							isExist = true;
							break;
						}
					}
					if (!PubFunction.curParameter.bolIsSendCard)
					{
						goto IL_4F2;
					}
					if (PubFunction.IsCanRegisterCard(intUserShopID, newCard, ""))
					{
						goto IL_4F2;
					}
					IL_56A:
					StartCard += 1uL;
					continue;
					IL_4F2:
					if (isExist || this.bllMem.ExistsMemCard(newCard))
					{
						goto IL_56A;
					}
					allStrs.Add(string.Format(strSql, newCard));
					if (mempoint > 0)
					{
						allStrs.Add(strPointLog);
					}
					if (memmoney > 0m)
					{
						allStrs.Add(strMemRecharge);
						allStrs.Add(strMoneyChangeLog);
					}
					goto IL_56A;
				}
				if (this.bllMem.ExeclDataInput(allStrs) && allStrs.Count > 0)
				{
					msgResponse = "1";
					PubFunction.SaveSysLog(intUserID, 1, "批量发卡", string.Concat(new object[]
					{
						"批量发卡起始卡号：[",
						StartCard,
						"],结束卡号：[",
						EndCard,
						"]",
						remark
					}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetPinYin()
		{
			string msgResponse = "";
			try
			{
				string strchinese = this.Request["strpinyin"].ToString();
				msgResponse = StringPlus.ConvertToFirstPinYin(strchinese);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			msgResponse = "{\"result\":\"" + msgResponse + "\"}";
			this.Context.Response.Write(msgResponse);
		}

		public void GetSysParameter()
		{
			string msgResponse = "";
			msgResponse = msgResponse + "\"Sms\":" + PubFunction.curParameter.bolSms.ToString().ToLower() + ",";
			msgResponse = msgResponse + "\"WeiXinType\":" + PubFunction.curParameter.bolWeiXinType.ToString().ToLower() + ",";
			object obj = msgResponse;
			msgResponse = string.Concat(new object[]
			{
				obj,
				"\"RestrainOnlineNumber\":",
				PubFunction.curParameter.RestrainOnlineNumber,
				","
			});
			msgResponse = msgResponse + "\"WeiXinVerified\":" + PubFunction.curParameter.bolWeiXinVerified.ToString().ToLower() + ",";
			msgResponse = msgResponse + "\"PointNumStr\":\"" + PubFunction.curParameter.PointNumStr + "\",";
			obj = msgResponse;
			msgResponse = string.Concat(new object[]
			{
				obj,
				"\"PrintPreview\":\"",
				PubFunction.curParameter.PrintPreview,
				"\","
			});
			obj = msgResponse;
			msgResponse = string.Concat(new object[]
			{
				obj,
				"\"PrintPaperType\":\"",
				PubFunction.curParameter.PrintPaperType,
				"\""
			});
			msgResponse = "{" + msgResponse + "}";
			this.Context.Response.Write(msgResponse);
		}

		public void GetRules()
		{
			string msgResponse = "";
			try
			{
				Chain.BLL.Timingrules bllTimingrules = new Chain.BLL.Timingrules();
				DataTable dtTimingrules = bllTimingrules.GetList(string.Format("RulesID = {0}", this.Request["RulesID"].ToString())).Tables[0];
				msgResponse = JsonPlus.ToJson(dtTimingrules.Rows[0], "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void RulesAdd()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				string RulesName = this.Request["RulesName"].ToString();
				int RulesInterval = Convert.ToInt32(this.Request["RulesInterval"]);
				decimal RulesUnitPrice = Convert.ToDecimal(this.Request["RulesUnitPrice"]);
				int RulesExceedTime = Convert.ToInt32(this.Request["RulesExceedTime"]);
				Chain.BLL.Timingrules bllTimingrules = new Chain.BLL.Timingrules();
				flag = bllTimingrules.Add(new Chain.Model.Timingrules
				{
					RulesAddTime = DateTime.Now,
					RulesExceedTime = RulesExceedTime,
					RulesInterval = RulesInterval,
					RulesName = RulesName,
					RulesShopID = intUserShopID,
					RulesUnitPrice = RulesUnitPrice,
					RulesUserID = intUserID,
					RulesRemark = this.Request["RulesRemark"].ToString()
				});
				PubFunction.SaveSysLog(intUserID, 1, "添加计时消费规则", "添加计时消费规则,规则名称：[" + RulesName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void RulesEdit()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int RulesID = Convert.ToInt32(this.Request["RulesID"]);
				string RulesName = this.Request["RulesName"].ToString();
				int RulesInterval = Convert.ToInt32(this.Request["RulesInterval"]);
				decimal RulesUnitPrice = Convert.ToDecimal(this.Request["RulesUnitPrice"]);
				int RulesExceedTime = Convert.ToInt32(this.Request["RulesExceedTime"]);
				Chain.BLL.Timingrules bllTimingrules = new Chain.BLL.Timingrules();
				Chain.Model.Timingrules mdTimingrules = bllTimingrules.GetModel(RulesID);
				mdTimingrules.RulesExceedTime = RulesExceedTime;
				mdTimingrules.RulesInterval = RulesInterval;
				mdTimingrules.RulesName = RulesName;
				mdTimingrules.RulesUnitPrice = RulesUnitPrice;
				mdTimingrules.RulesRemark = this.Request["RulesRemark"].ToString();
				if (bllTimingrules.Update(mdTimingrules))
				{
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 3, "修改计时消费规则", "修改计时消费规则,规则名称：[" + RulesName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void DelRules()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int RulesID = Convert.ToInt32(this.Request["RulesID"]);
				Chain.BLL.Timingrules bllTimingrules = new Chain.BLL.Timingrules();
				Chain.Model.Timingrules mdTimingrules = bllTimingrules.GetModel(RulesID);
				if (bllTimingrules.Delete(RulesID))
				{
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 2, "删除计时消费规则", "删除计时消费规则,规则名称：[" + mdTimingrules.RulesName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
				else
				{
					flag = -1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void CategoryEdit()
		{
			int flag = 0;
			try
			{
				int CategoryID = Convert.ToInt32(this.Request["CategoryID"]);
				string CategoryName = this.Request["CategoryName"].ToString();
				string CategoryrRemark = this.Request["CategoryrRemark"].ToString();
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.BLL.TimingCategory bllTimingCategory = new Chain.BLL.TimingCategory();
				Chain.Model.TimingCategory mdTimingCategory = bllTimingCategory.GetModel(CategoryID);
				mdTimingCategory.CategoryName = CategoryName;
				mdTimingCategory.CategoryrRemark = CategoryrRemark;
				if (bllTimingCategory.Update(mdTimingCategory))
				{
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 3, "修改计时服务类别", "修改计时服务类别,类别名称：[" + CategoryName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void CategoryAdd()
		{
			int flag = 0;
			try
			{
				string CategoryName = this.Request["CategoryName"].ToString();
				int CategoryFatherID = Convert.ToInt32(this.Request["CategoryFatherID"]);
				string CategoryrRemark = this.Request["CategoryrRemark"].ToString();
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.BLL.TimingCategory bllTimingCategory = new Chain.BLL.TimingCategory();
				flag = bllTimingCategory.Add(new Chain.Model.TimingCategory
				{
					CategoryFatherID = CategoryFatherID,
					CategoryName = CategoryName,
					CategoryrRemark = CategoryrRemark,
					CategoryShopID = intUserShopID,
					CategoryUserID = intUserID
				});
				PubFunction.SaveSysLog(intUserID, 1, "添加计时服务类别", "添加计时服务类别,类别名称：[" + CategoryName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void GetCategory()
		{
			string msgResponse = "";
			try
			{
				Chain.BLL.TimingCategory bllTimingCategory = new Chain.BLL.TimingCategory();
				DataTable dtTimingCategory = bllTimingCategory.GetList(string.Format("CategoryID = {0}", this.Request["CategoryID"].ToString())).Tables[0];
				msgResponse = JsonPlus.ToJson(dtTimingCategory.Rows[0], "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void DelCategory()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int CategoryID = Convert.ToInt32(this.Request["CategoryID"]);
				Chain.BLL.TimingCategory bllTimingCategory = new Chain.BLL.TimingCategory();
				Chain.Model.TimingCategory mdTimingCategory = bllTimingCategory.GetModel(CategoryID);
				if (bllTimingCategory.Delete(CategoryID))
				{
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 1, "删除计时服务类别", "删除计时服务类别,类别名称：[" + mdTimingCategory.CategoryName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
				else
				{
					flag = -1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void TimingProjectAdd()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				string ProjectName = this.Request["ProjectName"].ToString();
				int ProjectRulesID = Convert.ToInt32(this.Request["ProjectRulesID"]);
				string ProjectRemark = this.Request["ProjectRemark"].ToString();
				Chain.BLL.TimingProject bllTimingProject = new Chain.BLL.TimingProject();
				flag = bllTimingProject.Add(new Chain.Model.TimingProject
				{
					ProjectAddTime = DateTime.Now,
					ProjectName = ProjectName,
					ProjectRemark = ProjectRemark,
					ProjectRulesID = ProjectRulesID,
					ProjectShopID = intUserShopID,
					ProjectUserID = intUserID
				});
				PubFunction.SaveSysLog(intUserID, 1, "添加计时服务", "添加计时服务,类别名称：[" + ProjectName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void TimingProjectEdit()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				string ProjectName = this.Request["ProjectName"].ToString();
				int ProjectCategoryID = Convert.ToInt32(this.Request["ProjectCategoryID"]);
				int ProjectRulesID = Convert.ToInt32(this.Request["ProjectRulesID"]);
				string ProjectRemark = this.Request["ProjectRemark"].ToString();
				Chain.BLL.TimingProject bllTimingProject = new Chain.BLL.TimingProject();
				Chain.Model.TimingProject mdTimingProject = bllTimingProject.GetModel(Convert.ToInt32(this.Request["ProjectID"]));
				mdTimingProject.ProjectName = ProjectName;
				mdTimingProject.ProjectCategoryID = ProjectCategoryID;
				mdTimingProject.ProjectRulesID = ProjectRulesID;
				mdTimingProject.ProjectRemark = ProjectRemark;
				if (bllTimingProject.Update(mdTimingProject))
				{
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 3, "修改计时服务", "修改计时服务,类别名称：[" + ProjectName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void GetTimingProject()
		{
			string msgResponse = "";
			try
			{
				DataTable dtTimingProject = new Chain.BLL.TimingProject().GetList(string.Format("ProjectID = {0}", this.Request["ProjectID"])).Tables[0];
				msgResponse = JsonPlus.ToJson(dtTimingProject.Rows[0], "");
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void DelTimingProject()
		{
			int flag = 0;
			try
			{
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int ProjectID = Convert.ToInt32(this.Request["ProjectID"]);
				Chain.BLL.TimingProject bllTimingProject = new Chain.BLL.TimingProject();
				Chain.Model.TimingProject mdTimingProject = bllTimingProject.GetModel(ProjectID);
				if (bllTimingProject.Delete(ProjectID))
				{
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 2, "删除计时服务", "删除计时服务,类别名称：[" + mdTimingProject.ProjectName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
				else
				{
					flag = -1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void GetPrjectByPage()
		{
			string msgResponse = "";
			int counts = 0;
			try
			{
				int MemID = (this.Request["memID"] == null) ? 0 : Convert.ToInt32(this.Request["memID"]);
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intSize = Convert.ToInt32(this.Request["size"]);
				int intIndex = Convert.ToInt32(this.Request["index"]);
				string ProjectName = this.Request["ProjectName"].ToString();
				string strSql = string.Format(" TimingProject.ProjectRulesID = Timingrules.RulesID AND ProjectUserID = SysUser.UserID AND ProjectShopID = {0}", intUserShopID);
				if (ProjectName != "")
				{
					strSql += string.Format(" AND ProjectName LIKE '%{0}%'", ProjectName);
				}
				Chain.BLL.TimingProject bllTimingProject = new Chain.BLL.TimingProject();
				DataTable dtTimingProject = bllTimingProject.GetListSP(MemID, intSize, intIndex, out counts, new string[]
				{
					strSql
				}).Tables[0];
				msgResponse = JsonPlus.ToJson(dtTimingProject, "");
				msgResponse = string.Concat(new object[]
				{
					"{\"RecordCount\":",
					counts,
					",\"List\":",
					msgResponse,
					"}"
				});
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void MemStorageTiming()
		{
			string flag = "0";
			try
			{
				int StorageTimingProjectID = Convert.ToInt32(this.Request["ProjectID"]);
				int intMemID = (this.Request["MemID"].ToString() != "") ? int.Parse(this.Request["MemID"].ToString()) : 0;
				decimal dclTotalMoney = (this.Request["Money"].ToString() != "") ? decimal.Parse(this.Request["Money"].ToString()) : 0m;
				decimal dclDiscountMoney = (this.Request["DiscountMoney"].ToString() != "") ? decimal.Parse(this.Request["DiscountMoney"].ToString()) : 0m;
				int intPoint = (this.Request["Point"].ToString() != "") ? int.Parse(this.Request["Point"].ToString()) : 0;
				decimal dclCardPayMoney = (this.Request["parameter[0][CardMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CardMoney]"]) : 0m;
				decimal dclCashPayMoney = (this.Request["parameter[0][CashMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CashMoney]"]) : 0m;
				decimal dclBinkPayMoney = (this.Request["parameter[0][BinkMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][BinkMoney]"]) : 0m;
				decimal dclCouponPayMoney = (this.Request["parameter[0][CouponMoney]"] != "0") ? decimal.Parse(this.Request["parameter[0][CouponMoney]"]) : 0m;
				string strOrderAccount = this.Request["orderAccount"].ToString();
				string strRemark = (this.Request["Remark"].ToString() != "") ? this.Request["Remark"].ToString() : "";
				bool IsMSM = this.Request["sendSMS"] == "true";
				bool bolIsCard = bool.Parse(this.Request["parameter[0][IsCard]"]);
				bool bolIsCash = bool.Parse(this.Request["parameter[0][IsCash]"]);
				bool bolIsBink = bool.Parse(this.Request["parameter[0][IsBink]"]);
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int usePoint = int.Parse(this.Request["parameter[0][usePoint]"]);
				decimal usePointAmount = decimal.Parse(this.Request["parameter[0][usePointAmount]"]);
				int Alltime = Convert.ToInt32(this.Request["ExpenTime"]);
				Chain.Model.MemStorageTiming mdMemStorageTiming = new Chain.Model.MemStorageTiming();
				mdMemStorageTiming.StorageResidueTime = Alltime;
				mdMemStorageTiming.StorageTimingAccount = strOrderAccount;
				mdMemStorageTiming.StorageTimingCreateTime = DateTime.Now;
				mdMemStorageTiming.StorageTimingDiscountMoney = dclDiscountMoney;
				mdMemStorageTiming.StorageTimingIsBink = bolIsBink;
				mdMemStorageTiming.StorageTimingIsCard = bolIsCard;
				mdMemStorageTiming.StorageTimingIsCash = bolIsCash;
				mdMemStorageTiming.StorageTimingMemID = intMemID;
				mdMemStorageTiming.StorageTimingPayBink = dclBinkPayMoney;
				mdMemStorageTiming.StorageTimingPayCard = dclCardPayMoney;
				mdMemStorageTiming.StorageTimingPayCash = dclCashPayMoney;
				mdMemStorageTiming.StorageTimingPayCoupon = dclCouponPayMoney;
				mdMemStorageTiming.StorageTimingPayType = 0;
				mdMemStorageTiming.StorageTimingPoint = intPoint;
				mdMemStorageTiming.StorageTimingRemark = strRemark;
				mdMemStorageTiming.StorageTimingShopID = intUserShopID;
				mdMemStorageTiming.StorageTimingTotalMoney = dclTotalMoney;
				mdMemStorageTiming.StorageTimingUserID = intUserID;
				mdMemStorageTiming.StorageTotalTime = Alltime;
				mdMemStorageTiming.StorageTimingProjectID = StorageTimingProjectID;
				Chain.BLL.MemStorageTiming bllMemStorageTiming = new Chain.BLL.MemStorageTiming();
				int intCountID = bllMemStorageTiming.Add(mdMemStorageTiming);
				if (intCountID > 0)
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
					int intLevelID = modelMem.MemLevelID;
					this.modelPoint.PointMemID = intMemID;
					this.modelPoint.PointNumber = intPoint;
					this.modelPoint.PointChangeType = 14;
					this.modelPoint.PointRemark = string.Concat(new object[]
					{
						"会员充时成功，消费总额：[",
						dclDiscountMoney,
						"],获得积分:[",
						intPoint,
						"]"
					});
					this.modelPoint.PointShopID = intUserShopID;
					this.modelPoint.PointUserID = intUserID;
					this.modelPoint.PointCreateTime = DateTime.Now;
					this.modelPoint.PointOrderCode = strOrderAccount;
					if (this.bllPoint.Add(this.modelPoint) > 0)
					{
						PubFunction.SetShopPoint(intUserID, intUserShopID, intPoint, string.Concat(new object[]
						{
							"单号：[",
							strOrderAccount,
							"],会员充时金额：[",
							dclDiscountMoney,
							"],扣除商家积分：[",
							intPoint,
							"]"
						}), 2);
						decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
						modelMem.MemPoint += intPoint - usePoint;
						int mem = bllMem.MemCountUpdateMem(intMemID, dclMemMoney, modelMem.MemPoint);
						MEMPointUpdate.MEMPointRate(modelMem, intPoint, strOrderAccount, 14, intUserID, intUserShopID);
						modelMem = new Chain.BLL.Mem().GetModel(intMemID);
						string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
						if (usePoint != 0)
						{
							PubFunction.SetShopPoint(intUserID, intUserShopID, -usePoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderAccount,
								"],会员充时抵用积分：[",
								usePoint,
								"]，商家回收积分：[",
								usePoint,
								"]"
							}), 4);
							Chain.Model.PointLog modelPointLog = new Chain.Model.PointLog();
							modelPointLog.PointMemID = intMemID;
							modelPointLog.PointNumber = usePoint;
							modelPointLog.PointChangeType = 1;
							modelPointLog.PointRemark = string.Concat(new object[]
							{
								"会员充时成功,抵用积分：[",
								usePoint,
								"],抵用金额：[",
								usePointAmount,
								"]"
							});
							modelPointLog.PointShopID = intUserShopID;
							modelPointLog.PointUserID = intUserID;
							modelPointLog.PointCreateTime = DateTime.Now;
							modelPointLog.PointOrderCode = strOrderAccount;
							this.bllPoint.Add(modelPointLog);
						}
						Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
						Chain.Model.SysShop modelShop = bllSysShop.GetModel(intUserShopID);
						decimal flTotalRate = decimal.Parse(Convert.ToDouble(modelShop.TotalRate).ToString("#0.00"));
						if (flTotalRate > 0m)
						{
							int flTotalPoint = (int)(flTotalRate * intPoint);
							decimal alliancePercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.AllianceRebatePercent).ToString("#0.00"));
							decimal cardShopPercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.CardShopRebatePercent).ToString("#0.00"));
							int alliancePoint = (int)(flTotalPoint * alliancePercent);
							int cardShopPoint = (int)(flTotalPoint * cardShopPercent);
							int zbPoint = flTotalPoint - alliancePoint - cardShopPoint;
							Chain.BLL.ReturnPointLog bllReturnPoint = new Chain.BLL.ReturnPointLog();
							bllReturnPoint.Add(new Chain.Model.ReturnPointLog
							{
								OrderAccount = strOrderAccount,
								MemID = intMemID,
								TotalPoint = flTotalPoint,
								AlliancePoint = alliancePoint,
								ZbPoint = zbPoint,
								CardShopPoint = cardShopPoint,
								Remark = "会员充时,商家返利积分",
								ReturnShopID = intUserShopID,
								CreateTime = DateTime.Now
							});
							PubFunction.SetShopPoint(intUserID, intUserShopID, intUserShopID, flTotalPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderAccount,
								"],会员充时得积分：[",
								intPoint,
								"],返利总比例：[",
								flTotalRate,
								"],商家扣除返利总积分：[",
								flTotalPoint,
								"]"
							}), 2);
							PubFunction.SetShopPoint(intUserID, intUserShopID, modelShop.FatherShopID, -alliancePoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderAccount,
								"],商家总返利积分：[",
								flTotalPoint,
								"],联盟商返利比例：[",
								alliancePercent,
								"],联盟商得到返利积分：[",
								alliancePoint,
								"]"
							}), 3);
							PubFunction.SetShopPoint(intUserID, intUserShopID, modelMem.MemShopID, -cardShopPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderAccount,
								"],商家总返利积分：[",
								flTotalPoint,
								"],开卡商家返利比例：[",
								cardShopPercent,
								"],开卡商家得到返利积分：[",
								cardShopPoint,
								"]"
							}), 3);
							PubFunction.SetShopPoint(intUserID, intUserShopID, 1, -zbPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderAccount,
								"],商家总返利积分：[",
								flTotalPoint,
								"],运营商得到返利积分：[",
								zbPoint,
								"]"
							}), 3);
						}
						Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
						moneyChangeLogModel.MoneyChangeMemID = intMemID;
						moneyChangeLogModel.MoneyChangeUserID = intUserID;
						moneyChangeLogModel.MoneyChangeType = 15;
						moneyChangeLogModel.MoneyChangeAccount = strOrderAccount;
						moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
						moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
						moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
						moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
						moneyChangeLogModel.MemMoney = modelMem.MemMoney;
						moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
						moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
						flag = "{\"strUpdateMemLevel\":\"" + strUpdateMemLevel + "\"}";
						if (modelMem.MemMobile != "")
						{
							if (IsMSM)
							{
								if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
								{
									flag = "-2";
								}
								else if (modelMem.MemMobile != "")
								{
									SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
									smsTemplateParameter.strCardID = modelMem.MemCard;
									smsTemplateParameter.strName = modelMem.MemName;
									smsTemplateParameter.dclTempMoney = dclDiscountMoney;
									smsTemplateParameter.dclMoney = modelMem.MemMoney;
									smsTemplateParameter.intTempPoint = intPoint;
									smsTemplateParameter.intPoint = modelMem.MemPoint;
									smsTemplateParameter.OldLevelID = intLevelID;
									modelMem = new Chain.BLL.Mem().GetModel(intMemID);
									smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
									smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
									smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
									string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, intUserShopID);
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
								}
							}
						}
						string Remark = string.Concat(new object[]
						{
							"会员充时,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],订单号：[",
							strOrderAccount,
							"],消费金额：[",
							dclDiscountMoney,
							"],获得积分：[",
							intPoint,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "会员充时", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch
			{
				flag = "-1";
			}
			this.Context.Response.Write(flag);
		}

		public void GetProjectState()
		{
			string flag = "0";
			try
			{
				int project = Convert.ToInt32(this.Request["project"]);
				Chain.BLL.OrderTime bllOrderTime = new Chain.BLL.OrderTime();
				if (bllOrderTime.isUse(project))
				{
					flag = "1";
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void EditModule()
		{
			int flag = 0;
			try
			{
				int MerchantID = int.Parse(this.Request["MerchantID"]);
				string MerchantDesc = this.Request["MerchantDesc"];
				string MerchantRemark = StringPlus.HtmlEncode(this.Request["MerchantRemark"]);
				Chain.BLL.MerchantSite MerchantSiteBll = new Chain.BLL.MerchantSite();
				Chain.Model.MerchantSite MerchantSiteModel = MerchantSiteBll.GetModel(MerchantID);
				MerchantSiteModel.MerchantDesc = MerchantDesc;
				MerchantSiteModel.MerchantRemark = MerchantRemark;
				flag = (MerchantSiteBll.Update(MerchantSiteModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void AddProduct()
		{
			int flag = 0;
			try
			{
				Chain.Model.ProductCenter ProductCenterModel = new Chain.Model.ProductCenter();
				Chain.BLL.ProductCenter ProductCenterBll = new Chain.BLL.ProductCenter();
				string ProductName = this.Request["ProductName"];
				string ProductPhoto = "../Upload/MicroWebsite/MicroWebsiteProductCenter/" + this.Request["ProductPhoto"];
				string ProductDesc = this.Request["ProductDesc"];
				string ProductRemark = this.Request["ProductRemark"];
				int ClassID = (this.Request["ClassID"].ToString() != "") ? int.Parse(this.Request["ClassID"].ToString()) : 0;
				ProductCenterModel.ProductName = ProductName;
				ProductCenterModel.ProductPhoto = ProductPhoto;
				ProductCenterModel.ProductDesc = ProductDesc;
				ProductCenterModel.ProductCreateTime = new DateTime?(DateTime.Now);
				ProductCenterModel.CreateUserID = this.UserModel.UserID;
				ProductCenterModel.ProductRemark = ProductRemark;
				ProductCenterModel.ClassID = ClassID;
				flag = ProductCenterBll.Add(ProductCenterModel);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelProduct()
		{
			int flag = 0;
			try
			{
				int ProductID = int.Parse(this.Request["ProductID"]);
				flag = (new Chain.BLL.ProductCenter().Delete(ProductID) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditProduct()
		{
			int flag = 0;
			try
			{
				Chain.BLL.ProductCenter ProductCenterBll = new Chain.BLL.ProductCenter();
				int ProductID = int.Parse(this.Request["ProductID"]);
				string ProductName = this.Request["ProductName"];
				string ProductPhoto = "../Upload/MicroWebsite/MicroWebsiteProductCenter/" + Path.GetFileName(this.Request["ProductPhoto"]);
				string ProductDesc = this.Request["ProductDesc"];
				string ProductRemark = this.Request["ProductRemark"];
				int ClassID = (this.Request["ClassID"].ToString() != "") ? int.Parse(this.Request["ClassID"].ToString()) : 0;
				Chain.Model.ProductCenter ProductCenterModel = ProductCenterBll.GetModel(ProductID);
				ProductCenterModel.ProductName = ProductName;
				ProductCenterModel.ProductPhoto = ProductPhoto;
				ProductCenterModel.ProductDesc = ProductDesc;
				ProductCenterModel.ProductRemark = ProductRemark;
				ProductCenterModel.ClassID = ClassID;
				flag = (ProductCenterBll.Update(ProductCenterModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void AddSymbol()
		{
			int flag = 0;
			try
			{
				Chain.Model.SymbolShow SymbolShowModel = new Chain.Model.SymbolShow();
				Chain.BLL.SymbolShow SymbolShowBll = new Chain.BLL.SymbolShow();
				string SymbolTitle = this.Request["SymbolTitle"];
				string SymbolPhoto = "../Upload/MicroWebsite/MicroWebsiteSymbol/" + this.Request["SymbolPhoto"];
				string SymbolDesc = this.Request["SymbolDesc"];
				SymbolShowModel.SymbolTitle = SymbolTitle;
				SymbolShowModel.SymbolPhoto = SymbolPhoto;
				SymbolShowModel.SymbolDesc = SymbolDesc;
				SymbolShowModel.SymbolTime = DateTime.Now;
				flag = SymbolShowBll.Add(SymbolShowModel);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelSymbol()
		{
			int flag = 0;
			try
			{
				int SymbolID = int.Parse(this.Request["SymbolID"]);
				flag = (new Chain.BLL.SymbolShow().Delete(SymbolID) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditSymbol()
		{
			int flag = 0;
			try
			{
				Chain.BLL.SymbolShow SymbolShowBll = new Chain.BLL.SymbolShow();
				int SymbolID = int.Parse(this.Request["SymbolID"]);
				string SymbolTitle = this.Request["SymbolTitle"];
				string SymbolPhoto = "../Upload/MicroWebsite/MicroWebsiteSymbol/" + Path.GetFileName(this.Request["SymbolPhoto"]);
				string SymbolDesc = this.Request["SymbolDesc"];
				Chain.Model.SymbolShow SymbolShowModel = SymbolShowBll.GetModel(SymbolID);
				SymbolShowModel.SymbolTitle = SymbolTitle;
				SymbolShowModel.SymbolPhoto = SymbolPhoto;
				SymbolShowModel.SymbolDesc = SymbolDesc;
				flag = (SymbolShowBll.Update(SymbolShowModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void AddPromotions()
		{
			int flag = 0;
			try
			{
				string PromotionsTitle = StringPlus.HtmlEncode(this.Request["PromotionsTitle"]);
				string PromotionsDesc = this.Request["PromotionsDesc"];
				string PromotionsRemark = this.Request["PromotionsRemark"];
				string PromotionsPhoto = "../Upload/MicroWebsite/MicroWebsitePromotions/" + this.Request["PromotionsPhoto"];
				int PromotionsType = int.Parse(this.Request["PromotionsType"]);
				int PromotionsMemLevel = int.Parse(this.Request["PromotionsMemLevel"]);
				DateTime dtStart = DateTime.Parse((this.Request["PromotionsStart"] == "") ? "1900-1-1" : this.Request["PromotionsStart"]);
				DateTime dtEndt = DateTime.Parse((this.Request["PromotionsEnd"] == "") ? "1900-1-1" : this.Request["PromotionsEnd"]);
				string ShopList = this.Request["ShopList"];
				Chain.BLL.Promotions PromotionsBll = new Chain.BLL.Promotions();
				flag = PromotionsBll.Add(new Chain.Model.Promotions
				{
					PromotionsTitle = PromotionsTitle,
					PromotionsType = PromotionsType,
					PromotionsStart = dtStart,
					PromotionsEnd = dtEndt,
					PromotionsMemLevel = PromotionsMemLevel,
					PromotionsTime = DateTime.Now,
					CreateUserID = this.UserModel.UserID,
					PromotionsDesc = PromotionsDesc,
					PromotionsRemark = PromotionsRemark,
					PromotionsPhoto = PromotionsPhoto,
					ShopList = ShopList
				});
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelPromotions()
		{
			int flag = 0;
			try
			{
				int PromotionsID = int.Parse(this.Request["PromotionsID"]);
				flag = (new Chain.BLL.Promotions().Delete(PromotionsID) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditPromotions()
		{
			int flag = 0;
			try
			{
				string ShopList = this.Request["ShopList"];
				int PromotionsID = int.Parse(this.Request["PromotionsID"]);
				string PromotionsDesc = this.Request["PromotionsDesc"];
				string PromotionsRemark = this.Request["PromotionsRemark"];
				string PromotionsPhoto = "../Upload/MicroWebsite/MicroWebsitePromotions/" + Path.GetFileName(this.Request["PromotionsPhoto"]);
				string PromotionsTitle = this.Request["PromotionsTitle"];
				int PromotionsType = int.Parse(this.Request["PromotionsType"]);
				int PromotionsMemLevel = int.Parse(this.Request["PromotionsMemLevel"]);
				DateTime dtStart = DateTime.Parse((this.Request["PromotionsStart"] == "") ? "1900-1-1" : this.Request["PromotionsStart"]);
				DateTime dtEndt = DateTime.Parse((this.Request["PromotionsEnd"] == "") ? "1900-1-1" : this.Request["PromotionsEnd"]);
				Chain.BLL.Promotions PromotionsBll = new Chain.BLL.Promotions();
				Chain.Model.Promotions PromotionsModel = PromotionsBll.GetModel(PromotionsID);
				PromotionsModel.PromotionsTitle = PromotionsTitle;
				PromotionsModel.PromotionsType = PromotionsType;
				PromotionsModel.PromotionsStart = dtStart;
				PromotionsModel.PromotionsEnd = dtEndt;
				PromotionsModel.PromotionsMemLevel = PromotionsMemLevel;
				PromotionsModel.PromotionsTime = DateTime.Now;
				PromotionsModel.PromotionsDesc = PromotionsDesc;
				PromotionsModel.PromotionsRemark = PromotionsRemark;
				PromotionsModel.PromotionsPhoto = PromotionsPhoto;
				PromotionsModel.ShopList = ShopList;
				flag = (PromotionsBll.Update(PromotionsModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void AddMemberExplanation()
		{
			int flag = 0;
			try
			{
				Chain.BLL.MemberExplanation MemberExplanationBll = new Chain.BLL.MemberExplanation();
				Chain.Model.MemberExplanation MemberExplanationModel = new Chain.Model.MemberExplanation();
				string MemberExplanationDesc = this.Request["MemberExplanationDesc"];
				MemberExplanationModel.MemberExplanationTime = DateTime.Now;
				MemberExplanationModel.MemberExplanationDesc = MemberExplanationDesc;
				flag = MemberExplanationBll.Add(MemberExplanationModel);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditMemberExplanation()
		{
			int flag = 0;
			try
			{
				int MemberExplanationID = int.Parse(this.Request["MemberExplanationID"]);
				string MemberExplanationDesc = this.Request["MemberExplanationDesc"];
				Chain.BLL.MemberExplanation MemberExplanationBll = new Chain.BLL.MemberExplanation();
				Chain.Model.MemberExplanation MemberExplanationModel = MemberExplanationBll.GetModel(MemberExplanationID);
				MemberExplanationModel.MemberExplanationTime = DateTime.Now;
				MemberExplanationModel.MemberExplanationDesc = MemberExplanationDesc;
				flag = (MemberExplanationBll.Update(MemberExplanationModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelMemberExplanation()
		{
			int flag = 0;
			try
			{
				int MemberExplanationID = int.Parse(this.Request["MemberExplanationID"]);
				Chain.BLL.MemberExplanation MemberExplanationBll = new Chain.BLL.MemberExplanation();
				flag = (MemberExplanationBll.Delete(MemberExplanationID) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void MicroWebsiteAllowExchange()
		{
			int flag = 0;
			try
			{
				int intID = int.Parse(this.Request["ID"].ToString());
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.Model.MicroWebsiteGiftExchange MicroWebsiteGiftExchangeModel = new Chain.Model.MicroWebsiteGiftExchange();
				Chain.BLL.MicroWebsiteGiftExchange MicroWebsiteGiftExchangeBll = new Chain.BLL.MicroWebsiteGiftExchange();
				MicroWebsiteGiftExchangeModel = MicroWebsiteGiftExchangeBll.GetModel(intID);
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = new Chain.Model.Mem();
				modelMem = bllMem.GetModel(MicroWebsiteGiftExchangeModel.MemID);
				Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
				Chain.Model.PointLog modelPoint = new Chain.Model.PointLog();
				if (modelMem.MemState != 0)
				{
					if (modelMem.MemState == 1)
					{
						flag = -3;
					}
					else if (modelMem.MemState == 2)
					{
						flag = -4;
					}
				}
				else if (MicroWebsiteGiftExchangeModel.ExchangeAllPoint > modelMem.MemPoint)
				{
					flag = -1;
				}
				else
				{
					bllMem.UpdatePoint(MicroWebsiteGiftExchangeModel.MemID, MicroWebsiteGiftExchangeModel.ExchangeAllPoint * -1);
					modelMem = new Chain.BLL.Mem().GetModel(MicroWebsiteGiftExchangeModel.MemID);
					PubFunction.UpdateMemLevel(modelMem);
					MicroWebsiteGiftExchangeModel.ExchangeStatus = 2;
					MicroWebsiteGiftExchangeModel.ExchangeUserID = intUserID;
					MicroWebsiteGiftExchangeModel.ExchangeTime = DateTime.Now;
					if (MicroWebsiteGiftExchangeBll.Update(MicroWebsiteGiftExchangeModel))
					{
						flag = 1;
						modelPoint.PointMemID = modelMem.MemID;
						modelPoint.PointNumber = MicroWebsiteGiftExchangeModel.ExchangeAllPoint;
						modelPoint.PointChangeType = 4;
						modelPoint.PointRemark = "微网站兑换审核通过，扣减积分[" + MicroWebsiteGiftExchangeModel.ExchangeAllPoint + "]";
						modelPoint.PointShopID = intUserShopID;
						modelPoint.PointCreateTime = DateTime.Now;
						modelPoint.PointUserID = intUserID;
						modelPoint.PointOrderCode = MicroWebsiteGiftExchangeModel.ExchangeAccount;
						bllPoint.Add(modelPoint);
						PubFunction.SaveSysLog(intUserID, 4, "兑换审核", string.Concat(new object[]
						{
							"审核通过,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],总积分：[",
							MicroWebsiteGiftExchangeModel.ExchangeAllPoint,
							"]"
						}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void MicroWebsiteNoExchange()
		{
			int flag = 0;
			try
			{
				int intID = int.Parse(this.Request["ID"].ToString());
				int intUserID = this.UserModel.UserID;
				string strExchangeRemark = this.Request["ExchangeRemark"].ToString();
				Chain.Model.MicroWebsiteGiftExchange MicroWebsiteGiftExchangeModel = new Chain.Model.MicroWebsiteGiftExchange();
				Chain.BLL.MicroWebsiteGiftExchange MicroWebsiteGiftExchangeBll = new Chain.BLL.MicroWebsiteGiftExchange();
				MicroWebsiteGiftExchangeModel = MicroWebsiteGiftExchangeBll.GetModel(intID);
				MicroWebsiteGiftExchangeModel.ExchangeStatus = 3;
				MicroWebsiteGiftExchangeModel.ExchangeUserID = intUserID;
				MicroWebsiteGiftExchangeModel.ExchangeTime = DateTime.Now;
				MicroWebsiteGiftExchangeModel.ExchangeRemark = strExchangeRemark;
				if (MicroWebsiteGiftExchangeBll.Update(MicroWebsiteGiftExchangeModel))
				{
					flag = 1;
				}
			}
			catch
			{
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void AddOrEditGoodsClass()
		{
			int flag = 0;
			try
			{
				Chain.BLL.MicroWebsiteGoodsClass bllClass = new Chain.BLL.MicroWebsiteGoodsClass();
				Chain.Model.MicroWebsiteGoodsClass modelClass = new Chain.Model.MicroWebsiteGoodsClass();
				string strClassName = this.Request["GoodsClassName"].ToString();
				string strClassRemark = this.Request["GoodsClassRemark"].ToString();
				int arg_71_0 = (!string.IsNullOrEmpty(this.Request["ClassShopID"])) ? int.Parse(this.Request["ClassShopID"].ToString()) : 0;
				int intClassID = (!string.IsNullOrEmpty(this.Request["GoodsClassID"])) ? int.Parse(this.Request["GoodsClassID"].ToString()) : 0;
				modelClass.MicroGoodsClassName = strClassName;
				modelClass.MicroGoodsClassRemark = strClassRemark;
				modelClass.MicroGoodsClassShopID = 1;
				if (intClassID == 0)
				{
					flag = bllClass.Add(modelClass);
				}
				else
				{
					modelClass.MicroGoodsClassID = intClassID;
					if (bllClass.Update(modelClass))
					{
						flag = 1;
					}
				}
			}
			catch
			{
				flag = -1;
			}
			this.Response.Write(flag);
		}

		public void DelGoodsClass()
		{
			int flag = 0;
			try
			{
				Chain.BLL.MicroWebsiteGoodsClass bllClass = new Chain.BLL.MicroWebsiteGoodsClass();
				Chain.Model.MicroWebsiteGoodsClass modelClass = new Chain.Model.MicroWebsiteGoodsClass();
				int intClass = (!string.IsNullOrEmpty(this.Request["classID"])) ? int.Parse(this.Request["classID"].ToString()) : 0;
				if (intClass != 0)
				{
					DataTable dtClass = new Chain.BLL.MicroWebsiteGoods().GetList(" MicroGoodsClassID=" + intClass).Tables[0];
					if (dtClass.Rows.Count > 0)
					{
						flag = -1;
					}
					else if (bllClass.Delete(intClass))
					{
						flag = 1;
					}
					else
					{
						flag = -2;
					}
				}
			}
			catch
			{
				flag = -2;
			}
			this.Response.Write(flag);
		}

		public void MicroGoodsAddAndEdit()
		{
			int flag = 0;
			try
			{
				Chain.Model.MicroWebsiteGoods modelGoods = new Chain.Model.MicroWebsiteGoods();
				Chain.BLL.MicroWebsiteGoods bllGoods = new Chain.BLL.MicroWebsiteGoods();
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				int intGoodsID = (this.Request["txtGoodsID"] != "") ? int.Parse(this.Request["txtGoodsID"].ToString()) : 0;
				modelGoods.MicroGoodsCode = this.Request["txtGoodsCode"].ToString();
				modelGoods.MicroGoodsName = this.Request["txtGoodsName"].ToString();
				modelGoods.MicroGoodsClassID = ((this.Request["sltGoodsClass"].ToString() != "") ? int.Parse(this.Request["sltGoodsClass"].ToString()) : 0);
				modelGoods.MicroGoodsShopID = 1;
				modelGoods.MicroPrice = ((this.Request["txtMicroPrice"] != "") ? decimal.Parse(this.Request["txtMicroPrice"].ToString()) : 0m);
				modelGoods.MicroPoint = ((this.Request["txtGoodsPoint"] != "") ? int.Parse(this.Request["txtGoodsPoint"].ToString()) : -1);
				modelGoods.MicroGoodsBidPrice = ((this.Request["txtGoodsBidPrice"] != "") ? decimal.Parse(this.Request["txtGoodsBidPrice"].ToString()) : 0m);
				modelGoods.MicroSalePrice = ((this.Request["txtMicroSalePrice"] != "") ? decimal.Parse(this.Request["txtMicroSalePrice"].ToString()) : 0m);
				string strGoodsRemark = this.Request["txtGoodsRemark"].ToString();
				strGoodsRemark = PubFunction.RemoveSpace(strGoodsRemark);
				modelGoods.MicroGoodsRemark = strGoodsRemark;
				modelGoods.MicroGoodsCreateTime = DateTime.Now;
				if (intGoodsID == 0)
				{
					modelGoods.MicroGoodsPicture = "../Upload/MicroWebsite/MicroGoods/" + this.Request["txtUpdateGoodsName"];
					flag = bllGoods.Add(modelGoods);
				}
				else
				{
					modelGoods.MicroGoodsPicture = "../Upload/MicroWebsite/MicroGoods/" + Path.GetFileName(this.Request["txtUpdateGoodsName"]);
					modelGoods.MicroGoodsID = intGoodsID;
					flag = bllGoods.Update(modelGoods);
				}
				PubFunction.SaveSysLog(intUserID, 4, "商品管理", string.Concat(new object[]
				{
					"商家网站",
					(intGoodsID == 0) ? "商品新增" : "商品编辑",
					",商品名称:[",
					modelGoods.MicroGoodsName,
					"],零售金额:[",
					modelGoods.MicroPrice,
					"]"
				}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			catch
			{
				flag = -2;
			}
			this.Response.Write(flag);
		}

		public void GetMicroGoodsInfo()
		{
			string msgResponse = "";
			try
			{
				int intGoodsID = int.Parse(this.Request["goodsID"]);
				int intUserShopID = this.UserModel.UserShopID;
				DataTable dtGoods = new Chain.BLL.MicroWebsiteGoods().GetList(" MicroGoodsID=" + intGoodsID).Tables[0];
				if (dtGoods != null)
				{
					msgResponse = JsonPlus.ToJson(dtGoods, "");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void DelMicroGoods()
		{
			int flag = 0;
			try
			{
				int GoodsID = int.Parse(this.Request["goodsID"]);
				if (new Chain.BLL.MicroWebsiteGoods().Delete(GoodsID))
				{
					flag = 1;
				}
			}
			catch
			{
				flag = 0;
			}
			this.Response.Write(flag);
		}

		public void MicroWebsiteGoodsAllowExchange()
		{
			int flag = 0;
			try
			{
				int MicroOrderID = Convert.ToInt32(this.Request["MicroOrderID"]);
				bool sendSMS = this.Request["sendSMS"] == "true";
				Chain.BLL.MicroWebsiteOrderLog MicroWebsiteOrderLogBll = new Chain.BLL.MicroWebsiteOrderLog();
				Chain.Model.MicroWebsiteOrderLog MicroWebsiteOrderLogModel = MicroWebsiteOrderLogBll.GetModel(MicroOrderID);
				int MemID = MicroWebsiteOrderLogModel.MicroOrderMemID;
				Chain.Model.Mem MemModel = new Chain.BLL.Mem().GetModel(MemID);
				if (MemModel.MemState != 0)
				{
					if (this.modelMem.MemState == 1)
					{
						flag = -3;
					}
					else if (this.modelMem.MemState == 2)
					{
						flag = -4;
					}
				}
				else
				{
					MicroWebsiteOrderLogModel.MicroOrderPassCreateTime = DateTime.Now;
					MicroWebsiteOrderLogModel.MicroOrderStatus = 2;
					MicroWebsiteOrderLogModel.MicroOrderUserID = this.UserModel.UserShopID;
					MicroWebsiteOrderLogBll.Update(MicroWebsiteOrderLogModel);
					flag = 1;
					if (!string.IsNullOrEmpty(MemModel.MemMobile) && sendSMS && Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
					{
						SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
						smsTemplateParameter.strCardID = MemModel.MemCard;
						smsTemplateParameter.strName = MemModel.MemName;
						smsTemplateParameter.dclTempMoney = MicroWebsiteOrderLogModel.MicroOrderDiscountMoney;
						smsTemplateParameter.dclMoney = MemModel.MemMoney;
						smsTemplateParameter.intTempPoint = MicroWebsiteOrderLogModel.MicroOrderPoint;
						smsTemplateParameter.intPoint = MemModel.MemPoint;
						smsTemplateParameter.NewLevelID = (smsTemplateParameter.OldLevelID = MemModel.MemLevelID);
						smsTemplateParameter.MemBirthday = this.modelMem.MemBirthday;
						smsTemplateParameter.MemPastTime = this.modelMem.MemPastTime;
						string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, this.UserModel.UserShopID);
						SMSInfo.Send_GXSMS(false, this.modelMem.MemMobile, strSendContent, "");
						Chain.BLL.SmsLog SmsLogBll = new Chain.BLL.SmsLog();
						Chain.Model.SmsLog SmsLogModel = new Chain.Model.SmsLog
						{
							SmsMemID = MemModel.MemID,
							SmsMobile = MemModel.MemMobile,
							SmsContent = strSendContent,
							SmsShopID = this.UserModel.UserShopID,
							SmsTime = DateTime.Now,
							SmsUserID = this.UserModel.UserID,
							SmsAmount = PubFunction.GetSmsAmount(strSendContent),
							SmsAllAmount = PubFunction.GetSmsAmount(strSendContent)
						};
						SmsLogBll.Add(SmsLogModel);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void MicroWebsiteGoodsNoExchange()
		{
			int flag = 0;
			try
			{
				int MicroOrderID = Convert.ToInt32(this.Request["MicroOrderID"]);
				string MicroOrderMark = this.Request["MicroOrderMark"];
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				bool sendSMS = this.Request["sendSMS"] == "true";
				Chain.BLL.MicroWebsiteOrderLog MicroWebsiteOrderLogBll = new Chain.BLL.MicroWebsiteOrderLog();
				Chain.Model.MicroWebsiteOrderLog MicroWebsiteOrderLogModel = MicroWebsiteOrderLogBll.GetModel(MicroOrderID);
				Chain.BLL.Mem MemBll = new Chain.BLL.Mem();
				Chain.Model.Mem MemModel = MemBll.GetModel(MicroWebsiteOrderLogModel.MicroOrderMemID);
				MicroWebsiteOrderLogModel.MicroOrderStatus = 3;
				MicroWebsiteOrderLogModel.MicroOrderRemark = MicroOrderMark;
				MicroWebsiteOrderLogModel.MicroOrderUserID = intUserID;
				flag = (MicroWebsiteOrderLogBll.Update(MicroWebsiteOrderLogModel) ? 1 : 0);
				int point = (MicroWebsiteOrderLogModel.MicroOrderPoint > MemModel.MemPoint) ? 0 : (MemModel.MemPoint - MicroWebsiteOrderLogModel.MicroOrderPoint);
				MemBll.ExpenseUpdateMem(MemModel.MemID, MemModel.MemMoney + MicroWebsiteOrderLogModel.MicroOrderDiscountMoney, MemModel.MemConsumeMoney - MicroWebsiteOrderLogModel.MicroOrderDiscountMoney, point, DateTime.Now, MemModel.MemConsumeCount - 1);
				Chain.BLL.PointLog PointLogBll = new Chain.BLL.PointLog();
				Chain.Model.PointLog PointLogModel = new Chain.Model.PointLog
				{
					PointMemID = MemModel.MemID,
					PointNumber = MicroWebsiteOrderLogModel.MicroOrderPoint,
					PointChangeType = 1,
					PointRemark = "会员微网站商品退回",
					PointShopID = intUserShopID,
					PointCreateTime = DateTime.Now,
					PointUserID = intUserID,
					PointGiveMemID = 0,
					PointOrderCode = MicroWebsiteOrderLogModel.MicroOrderAccount
				};
				PointLogBll.Add(PointLogModel);
				Chain.Model.MoneyChangeLog MoneyChangeLogModel = new Chain.Model.MoneyChangeLog
				{
					MemMoney = MemModel.MemMoney + MicroWebsiteOrderLogModel.MicroOrderDiscountMoney,
					MoneyChangeAccount = MicroWebsiteOrderLogModel.MicroOrderAccount,
					MoneyChangeBalance = MicroWebsiteOrderLogModel.MicroOrderDiscountMoney,
					MoneyChangeCash = 0m,
					MoneyChangeCreateTime = DateTime.Now,
					MoneyChangeGiveMoney = 0m,
					MoneyChangeMemID = MemModel.MemID,
					MoneyChangeMoney = MicroWebsiteOrderLogModel.MicroOrderDiscountMoney,
					MoneyChangeType = 11,
					MoneyChangeUnionPay = 0m,
					MoneyChangeUserID = intUserID
				};
				new Chain.BLL.MoneyChangeLog().Add(MoneyChangeLogModel);
				DataTable dtRecommendPoint = PointLogBll.GetList(" PointChangeType=9 and PointOrderCode='" + MicroWebsiteOrderLogModel.MicroOrderAccount + "'").Tables[0];
				if (dtRecommendPoint != null)
				{
					for (int i = 0; i < dtRecommendPoint.Rows.Count; i++)
					{
						Chain.Model.PointLog PointLogModelG = new Chain.Model.PointLog
						{
							PointMemID = MemModel.MemID,
							PointNumber = MicroWebsiteOrderLogModel.MicroOrderPoint * -1,
							PointChangeType = 12,
							PointRemark = "会员消费撤单推荐人积分变动",
							PointShopID = intUserShopID,
							PointCreateTime = DateTime.Now,
							PointUserID = intUserID,
							PointGiveMemID = MemModel.MemID,
							PointOrderCode = MicroWebsiteOrderLogModel.MicroOrderAccount
						};
						PointLogBll.Add(PointLogModelG);
						PointLogBll.MemPointRollback(int.Parse(dtRecommendPoint.Rows[i]["PointMemID"].ToString()), int.Parse(dtRecommendPoint.Rows[i]["PointNumber"].ToString()));
					}
				}
				if (!string.IsNullOrEmpty(MemModel.MemMobile) && sendSMS && Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
				{
					SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
					smsTemplateParameter.strCardID = MemModel.MemCard;
					smsTemplateParameter.strName = MemModel.MemName;
					smsTemplateParameter.dclTempMoney = MicroWebsiteOrderLogModel.MicroOrderDiscountMoney;
					smsTemplateParameter.dclMoney = MemModel.MemMoney;
					smsTemplateParameter.intTempPoint = MicroWebsiteOrderLogModel.MicroOrderPoint;
					smsTemplateParameter.intPoint = MemModel.MemPoint;
					smsTemplateParameter.OldLevelID = (smsTemplateParameter.NewLevelID = MemModel.MemLevelID);
					smsTemplateParameter.MemBirthday = this.modelMem.MemBirthday;
					smsTemplateParameter.MemPastTime = this.modelMem.MemPastTime;
					string strSendContent = SMSInfo.GetSendContent(8, smsTemplateParameter, intUserShopID);
					strSendContent = strSendContent + "撤单原因是：未通过审核；备注：" + MicroOrderMark;
					SMSInfo.Send_GXSMS(false, this.modelMem.MemMobile, strSendContent, "");
					Chain.BLL.SmsLog SmsLogBll = new Chain.BLL.SmsLog();
					Chain.Model.SmsLog SmsLogModel = new Chain.Model.SmsLog
					{
						SmsMemID = MemModel.MemID,
						SmsMobile = MemModel.MemMobile,
						SmsContent = strSendContent,
						SmsShopID = intUserShopID,
						SmsTime = DateTime.Now,
						SmsUserID = intUserID,
						SmsAmount = PubFunction.GetSmsAmount(strSendContent),
						SmsAllAmount = PubFunction.GetSmsAmount(strSendContent)
					};
					SmsLogBll.Add(SmsLogModel);
				}
			}
			catch (Exception)
			{
			}
			this.Response.Write(flag);
		}

		public void MemOpinion()
		{
			int flag = 0;
			try
			{
				string ProposalContent = this.Request["ProposalContent"];
				int MemID = Convert.ToInt32(this.Request["MemID"]);
				string MemMobile = this.Request["MemMobile"];
				Chain.BLL.Proposal ProposalBll = new Chain.BLL.Proposal();
				flag = ProposalBll.Add(new Chain.Model.Proposal
				{
					ProposalContent = ProposalContent,
					MemID = new int?(MemID),
					MemMobile = MemMobile,
					ProposalTime = new DateTime?(DateTime.Now)
				});
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void UpdateMemInfo()
		{
			int flag = 0;
			try
			{
				int MemID = Convert.ToInt32(this.Request["MemID"]);
				string MemName = this.Request["MemName"];
				bool MemSex = this.Request["MemSex"] == "1";
				DateTime MemBirthday = Convert.ToDateTime((this.Request["MemBirthday"] == "") ? "1900-01-01" : this.Request["MemBirthday"]);
				string MemMobile = this.Request["MemMobile"];
				string MemEmail = this.Request["MemEmail"];
				string MemAddress = this.Request["MemAddress"];
				Chain.BLL.Mem MemBll = new Chain.BLL.Mem();
				Chain.Model.Mem MemModel = MemBll.GetModel(MemID);
				if (MemBirthday.ToString("yyyy-MM-dd") != "1900-1-1" && MemModel.MemBirthday.ToString("yyyy-MM-dd") != MemBirthday.ToString("yyyy-MM-dd") && MemModel.MemBirthday.ToString("MM-dd") != MemBirthday.ToString("MM-dd"))
				{
					Chain.BLL.MemberSMSRemindLog MemberSMSRemindLogBll = new Chain.BLL.MemberSMSRemindLog();
					MemberSMSRemindLogBll.DeleteMemByMemID(this.modelMem.MemID);
				}
				MemModel.MemName = MemName;
				MemModel.MemSex = MemSex;
				MemModel.MemBirthday = MemBirthday;
				MemModel.MemMobile = MemMobile;
				MemModel.MemEmail = MemEmail;
				MemModel.MemAddress = MemAddress;
				flag = MemBll.Update(MemModel);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void WeiXinLogin()
		{
			int flag = 1;
			Chain.BLL.Mem WXMem = new Chain.BLL.Mem();
			try
			{
				string WeiXinCard = this.Request["uid"].ToString();
				string passwords = this.Request["pwd"].ToString();
				string password = DESEncrypt.Encrypt(passwords);
				string Yanzheng = this.Request["Yanzheng"].ToString();
				ValCodeModel modeValCode = (ValCodeModel)this.Session["ValCode"];
				if (Yanzheng == modeValCode.valCode)
				{
					using (DataTable dt = WXMem.WeiXinLogin(WeiXinCard, password).Tables[0])
					{
						if (dt.Rows.Count > 0)
						{
							string MemState = dt.Rows[0]["MemState"].ToString();
							DateTime stime = DateTime.Now;
							DateTime s_t = stime;
							DateTime.TryParse(dt.Rows[0]["MemPastTime"].ToString(), out stime);
							if (MemState != "0")
							{
								flag = 4;
							}
							else if (s_t < stime)
							{
								this.Response.Cookies.Add(new HttpCookie("uid", dt.Rows[0]["MemID"].ToString()));
								flag = 2;
							}
							else
							{
								flag = 5;
							}
						}
					}
				}
				else
				{
					flag = 3;
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void WX_Register()
		{
			int flag = -2;
			Chain.BLL.Mem WXMem = new Chain.BLL.Mem();
			try
			{
				string MemName = this.Request["MemName"].ToString();
				string telnumber = this.Request["telnumber"].ToString();
				string password = DESEncrypt.Encrypt(telnumber.Substring(5, 6));
				string Yanzheng = this.Request["YanZhengMa"].ToString();
				string referrerMemId = this.Request["referrerMemId"].ToString();
				string memWeiXinCard = this.Request["OpenID"].ToString();
				string txtRecommended = this.Request["txtRecommended"].ToString();
				ValCodeModel modeValCode = (ValCodeModel)this.Session["ValCode"];
				if (Yanzheng == modeValCode.valCode)
				{
					List<Chain.Model.Mem> listMem = new Chain.BLL.Mem().GetModelList(string.Format(" MemWeiXinCard='{0}' and MemWeiXinCard is not null and MemWeiXinCard<>''", memWeiXinCard));
					if (listMem.Count > 0)
					{
						flag = -6;
					}
					else
					{
						int MemShopID = 1;
						Chain.Model.MicroWebsiteSceneStr modelMicroWebsiteSceneStr = new Chain.Model.MicroWebsiteSceneStr();
						Chain.BLL.MicroWebsiteSceneStr bllMicroWebsiteSceneStr = new Chain.BLL.MicroWebsiteSceneStr();
						modelMicroWebsiteSceneStr = bllMicroWebsiteSceneStr.GetModel(memWeiXinCard);
						if (modelMicroWebsiteSceneStr != null)
						{
							MemShopID = modelMicroWebsiteSceneStr.SceneStr;
						}
						if (txtRecommended != "")
						{
							int memId = this.GetMemID(txtRecommended);
							if (memId > 0)
							{
								flag = WXMem.WeiXinRegister(MemName, password, telnumber, memId.ToString(), memWeiXinCard, MemShopID);
								Chain.Model.Mem modelMems = new Chain.BLL.Mem().GetModel(Convert.ToInt32(flag));
								this.MemRecommendPoint(modelMems, MemShopID, memId);
								this.Response.Cookies.Add(new HttpCookie("uid", flag.ToString()));
							}
							else
							{
								flag = memId;
							}
						}
						else
						{
							flag = WXMem.WeiXinRegister(MemName, password, telnumber, referrerMemId, memWeiXinCard, MemShopID);
							if (Convert.ToInt32(referrerMemId) != 0)
							{
								Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(flag);
								this.MemRecommendPoint(modelMem, MemShopID, Convert.ToInt32(referrerMemId));
							}
							this.Response.Cookies.Add(new HttpCookie("uid", flag.ToString()));
						}
					}
				}
				else
				{
					flag = -3;
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public int GetMemID(string memCard)
		{
			int memID = -4;
			try
			{
				List<Chain.Model.Mem> listMem = new Chain.BLL.Mem().GetModelList(string.Format(" MemCard='{0}'", memCard));
				if (listMem.Count > 0)
				{
					memID = listMem[0].MemID;
				}
			}
			catch
			{
				memID = -5;
			}
			return memID;
		}

		public void Change_Pwd()
		{
			int flag = 0;
			int memID = -1;
			int.TryParse(this.Request["uid"], out memID);
			string oldPwd = (!string.IsNullOrEmpty(this.Request["spwd"])) ? this.Request["spwd"] : "";
			string newPwd = (!string.IsNullOrEmpty(this.Request["npwd"])) ? this.Request["npwd"] : "";
			string dnewPwd = (!string.IsNullOrEmpty(this.Request["dnpwd"])) ? this.Request["dnpwd"] : "";
			string remark = "微信系统密码修改";
			ValCodeModel modeValCode = (ValCodeModel)this.Session["ValCode"];
			if (modeValCode == null)
			{
				flag = -5;
			}
			else if (modeValCode.CodeFailure)
			{
				flag = -6;
			}
			else
			{
				string rcode = string.IsNullOrEmpty(this.Request["rcode"]) ? "" : this.Request["rcode"];
				if (rcode.Length != 4)
				{
					flag = -7;
				}
				else if (modeValCode.valCode != rcode)
				{
					flag = -8;
				}
				else
				{
					bool isOldPwd = true;
					if (memID > 0)
					{
						Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
						Chain.Model.Mem modelMem = bllMem.GetModel(memID);
						if (modelMem != null)
						{
							oldPwd = DESEncrypt.Encrypt(oldPwd);
							newPwd = DESEncrypt.Encrypt(newPwd);
							dnewPwd = DESEncrypt.Encrypt(dnewPwd);
							if (newPwd != dnewPwd)
							{
								flag = -3;
							}
							else if (newPwd == oldPwd)
							{
								flag = -2;
							}
							else
							{
								try
								{
									flag = bllMem.UpdateMemPwd(isOldPwd, modelMem.MemID, newPwd, oldPwd);
									if (flag > 0)
									{
										PubFunction.SaveSysLog(1, 3, "微信系统密码修改", string.Concat(new string[]
										{
											"微信修改会员密码,会员卡号：[",
											modelMem.MemCard,
											"],会员姓名：[",
											modelMem.MemName,
											"],备注：",
											remark
										}), 1, DateTime.Now, PubFunction.ipAdress);
									}
								}
								catch
								{
									flag = -4;
								}
							}
						}
						else
						{
							this.Response.Cookies.Add(new HttpCookie("uid", ""));
							flag = -9;
						}
					}
				}
			}
			this.Context.Response.Write(flag);
		}

		public void RequestCoupon()
		{
			int flag = 0;
			try
			{
				int ID = Convert.ToInt32(this.Request["ID"]);
				int MemID = Convert.ToInt32(this.Request["MemID"]);
				Chain.BLL.Coupon CouponBll = new Chain.BLL.Coupon();
				Chain.Model.Coupon CouponModel = CouponBll.GetModel(ID);
				if (CouponModel.CouponYF >= CouponModel.CouponPredictNu)
				{
					flag = -1;
				}
				else
				{
					Chain.BLL.CouponList CouponListBll = new Chain.BLL.CouponList();
					string strWhere = string.Format("CouPonID={0} and CouPonYF='False'", ID);
					DataRow row = CouponListBll.GetList(1, strWhere, " CID ").Tables[0].Rows[0];
					string strSql = string.Format("update CouponList set CouPonYF='True',CouPonMID={0},ConPonSendTime='{1}' where CID={2}", MemID, DateTime.Now.ToString(), row["CID"]);
					if (CouponListBll.DataUpdateTran(new ArrayList
					{
						strSql
					}))
					{
						CouponModel.CouponYF++;
						flag = (CouponBll.Update(CouponModel) ? 1 : 0);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void GiftApplication()
		{
			int flag = 0;
			try
			{
				int MemID = Convert.ToInt32(this.Request["MemID"]);
				string MemName = this.Request["MemName"];
				string ExchangeTelePhone = this.Request["ExchangeTelePhone"];
				string ExchangeAddress = this.Request["ExchangeAddress"];
				int ExchangeAllPoint = Convert.ToInt32(this.Request["ExchangeAllPoint"]);
				string giftlist = HttpUtility.UrlDecode(this.Request.Cookies["giftlist"].Value).Trim();
				string[] keys = giftlist.Split(new char[]
				{
					'|'
				}, StringSplitOptions.RemoveEmptyEntries);
				int ExchangeAllNumber = 0;
				string[] array = keys;
				for (int i = 0; i < array.Length; i++)
				{
					string item = array[i];
					ExchangeAllNumber += Convert.ToInt32(item.Split(new char[]
					{
						','
					})[1]);
				}
				Chain.BLL.MicroWebsiteGiftExchange MicroWebsiteGiftExchangeBll = new Chain.BLL.MicroWebsiteGiftExchange();
				flag = MicroWebsiteGiftExchangeBll.Add(new Chain.Model.MicroWebsiteGiftExchange
				{
					MemID = MemID,
					ExchangeTelePhone = ExchangeTelePhone,
					ExchangeAddress = ExchangeAddress,
					ExchangeAccount = PubFunction.curParameter.strGiftExchangePrefix + DateTime.Now.ToString("yyMMddHHmmssffff"),
					ExchangeAllNumber = ExchangeAllNumber,
					ExchangeAllPoint = ExchangeAllPoint,
					ApplicationTime = DateTime.Now,
					ExchangeStatus = 1,
					ExchangeType = 4,
					ShopID = 1,
					MemName = MemName
				});
				Chain.BLL.PointGift PointGiftBll = new Chain.BLL.PointGift();
				Chain.Model.MicroWebsiteGiftExchangeDetail MicroWebsiteGiftExchangeDetailModel = new Chain.Model.MicroWebsiteGiftExchangeDetail();
				Chain.BLL.MicroWebsiteGiftExchangeDetail MicroWebsiteGiftExchangeDetailBll = new Chain.BLL.MicroWebsiteGiftExchangeDetail();
				array = keys;
				for (int i = 0; i < array.Length; i++)
				{
					string item = array[i];
					int GiftID = Convert.ToInt32(item.Split(new char[]
					{
						','
					})[0]);
					int ConvertNumber = Convert.ToInt32(item.Split(new char[]
					{
						','
					})[1]);
					Chain.Model.PointGift PointGiftModel = PointGiftBll.GetModel(GiftID);
					MicroWebsiteGiftExchangeDetailModel.ExchangeID = flag;
					MicroWebsiteGiftExchangeDetailModel.ExchangeGiftID = GiftID;
					MicroWebsiteGiftExchangeDetailModel.ExchangeNumber = ConvertNumber;
					MicroWebsiteGiftExchangeDetailModel.ExchangePoint = ConvertNumber * PointGiftModel.GiftExchangePoint;
					MicroWebsiteGiftExchangeDetailModel.GiftName = PointGiftModel.GiftName;
					MicroWebsiteGiftExchangeDetailBll.Add(MicroWebsiteGiftExchangeDetailModel);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void GoodApplication()
		{
			int flag = 0;
			try
			{
				int MemID = Convert.ToInt32(this.Request["MemID"]);
				string MemName = this.Request["MemName"];
				string ExchangeTelePhone = this.Request["ExchangeTelePhone"];
				string ExchangeAddress = this.Request["ExchangeAddress"];
				decimal MicroOrderDiscountMoney = Convert.ToDecimal(this.Request["MicroOrderDiscountMoney"]);
				int MicroOrderPoint = Convert.ToInt32(this.Request["MicroOrderPoint"]);
				string goodlist = HttpUtility.UrlDecode(this.Request.Cookies["goodlist"].Value).Trim();
				string[] keys = goodlist.Split(new char[]
				{
					'|'
				}, StringSplitOptions.RemoveEmptyEntries);
				Chain.BLL.Mem MemBll = new Chain.BLL.Mem();
				Chain.Model.Mem MemModel = MemBll.GetModel(MemID);
				Chain.BLL.MicroWebsiteOrderLog MicroWebsiteOrderLogBll = new Chain.BLL.MicroWebsiteOrderLog();
				flag = MicroWebsiteOrderLogBll.Add(new Chain.Model.MicroWebsiteOrderLog
				{
					MicroOrderAccount = PubFunction.curParameter.strGoodsExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff"),
					MicroOrderType = 0,
					MicroOrderMemID = MemID,
					MicroOrderTotalMoney = MicroOrderDiscountMoney,
					MicroOrderDiscountMoney = MicroOrderDiscountMoney,
					MicroOrderIsCard = false,
					MicroOrderPayCard = 0m,
					MicroOrderIsCash = false,
					MicroOrderPayCash = 0m,
					MicroOrderIsBink = false,
					MicroOrderPayBink = 0m,
					MicroOrderPayCoupon = 0m,
					MicroOrderPoint = MicroOrderPoint,
					MicroOrderRemark = "",
					MicroOrderShopID = 1,
					MicroOrderUserID = 0,
					MicroOrderCreateTime = DateTime.Now,
					MicroOldAccount = "",
					MicroOrderCardBalance = MemModel.MemMoney,
					MicroOrderCardPoint = MemModel.MemPoint,
					MicroOrderName = MemName,
					MicroOrderMobile = ExchangeTelePhone,
					MicroOrderAdress = ExchangeAddress,
					MicroOrderStatus = 1,
					MicroOrderPassCreateTime = DateTime.Now,
					MicroOrderMark = "",
					MicroOrderPayCreateTime = DateTime.Now
				});
				if (flag > 0)
				{
					Chain.BLL.MicroWebsiteOrderLogDetail MicroWebsiteOrderLogDetailBll = new Chain.BLL.MicroWebsiteOrderLogDetail();
					Chain.Model.MicroWebsiteOrderLogDetail MicroWebsiteOrderLogDetailModel = new Chain.Model.MicroWebsiteOrderLogDetail();
					MicroWebsiteOrderLogDetailModel.MicroOrderID = flag;
					Chain.BLL.MicroWebsiteGoods MicroWebsiteGoodsBll = new Chain.BLL.MicroWebsiteGoods();
					Chain.Model.MicroWebsiteGoods MicroWebsiteGoodsModel = new Chain.Model.MicroWebsiteGoods();
					for (int i = 0; i < keys.Length; i++)
					{
						int MicroGoodsID = Convert.ToInt32(keys[i].Split(new char[]
						{
							','
						})[0]);
						int MicroOrderDetailNumber = Convert.ToInt32(keys[i].Split(new char[]
						{
							','
						})[1]);
						MicroWebsiteGoodsModel = MicroWebsiteGoodsBll.GetModel(MicroGoodsID);
						MicroWebsiteOrderLogDetailModel.MicroGoodsID = MicroGoodsID;
						MicroWebsiteOrderLogDetailModel.MicroOrderDetailPrice = MicroWebsiteGoodsModel.MicroSalePrice;
						MicroWebsiteOrderLogDetailModel.MicroOrderDetailPoint = MicroWebsiteOrderLogDetailModel.MicroOrderDetailPoint;
						MicroWebsiteOrderLogDetailModel.MicroOrderDetailDiscountPrice = MicroWebsiteOrderLogDetailModel.MicroOrderDetailDiscountPrice;
						MicroWebsiteOrderLogDetailModel.MicroOrderDetailNumber = MicroOrderDetailNumber;
						MicroWebsiteOrderLogDetailBll.Add(MicroWebsiteOrderLogDetailModel);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void GoodPay()
		{
			int flag = 0;
			int MemID = Convert.ToInt32(this.Request["MemID"]);
			int MicroOrderID = Convert.ToInt32(this.Request["MicroOrderID"]);
			decimal payMoney = Convert.ToDecimal(this.Request["payMoney"]);
			Chain.BLL.Mem MemBll = new Chain.BLL.Mem();
			Chain.Model.Mem MemModel = MemBll.GetModel(MemID);
			if (payMoney > MemModel.MemMoney)
			{
				flag = -1;
			}
			else
			{
				Chain.BLL.MicroWebsiteOrderLog MicroWebsiteOrderLogBll = new Chain.BLL.MicroWebsiteOrderLog();
				Chain.Model.MicroWebsiteOrderLog MicroWebsiteOrderLogModel = MicroWebsiteOrderLogBll.GetModel(MicroOrderID);
				if (MicroWebsiteOrderLogModel.MicroOrderStatus == 4)
				{
					flag = -2;
				}
				else
				{
					MicroWebsiteOrderLogModel.MicroOrderIsCard = true;
					MicroWebsiteOrderLogModel.MicroOrderPayCard = payMoney;
					MicroWebsiteOrderLogModel.MicroOrderStatus = 4;
					MicroWebsiteOrderLogModel.MicroOrderPayCreateTime = DateTime.Now;
					MicroWebsiteOrderLogModel.MicroOrderCardBalance -= payMoney;
					if (MicroWebsiteOrderLogBll.Update(MicroWebsiteOrderLogModel))
					{
						MemBll.ExpenseUpdateMem(MemModel.MemID, MemModel.MemMoney - payMoney, MemModel.MemConsumeMoney + payMoney, MemModel.MemPoint + MicroWebsiteOrderLogModel.MicroOrderPoint, DateTime.Now, MemModel.MemConsumeCount + 1);
						PubFunction.UpdateMemLevel(MemModel);
						MEMPointUpdate.MEMPointRate(MemModel, MicroWebsiteOrderLogModel.MicroOrderPoint, MicroWebsiteOrderLogModel.MicroOrderAccount, 1, 1, 1);
						Chain.Model.PointLog PointLogModel = new Chain.Model.PointLog
						{
							PointMemID = MemID,
							PointNumber = MicroWebsiteOrderLogModel.MicroOrderPoint,
							PointChangeType = 1,
							PointRemark = "会员微网站商品消费成功，消费总额：[" + payMoney + "]",
							PointShopID = 1,
							PointCreateTime = DateTime.Now,
							PointUserID = 1,
							PointGiveMemID = 0,
							PointOrderCode = MicroWebsiteOrderLogModel.MicroOrderAccount
						};
						new Chain.BLL.PointLog().Add(PointLogModel);
						Chain.Model.MoneyChangeLog MoneyChangeLogModel = new Chain.Model.MoneyChangeLog
						{
							MemMoney = MemModel.MemMoney - payMoney,
							MoneyChangeAccount = MicroWebsiteOrderLogModel.MicroOrderAccount,
							MoneyChangeBalance = -payMoney,
							MoneyChangeCash = 0m,
							MoneyChangeCreateTime = DateTime.Now,
							MoneyChangeGiveMoney = 0m,
							MoneyChangeMemID = MemID,
							MoneyChangeMoney = -payMoney,
							MoneyChangeType = 12,
							MoneyChangeUnionPay = 0m,
							MoneyChangeUserID = 1
						};
						new Chain.BLL.MoneyChangeLog().Add(MoneyChangeLogModel);
						flag = 1;
					}
				}
			}
			this.Response.Write(flag);
		}

		public void AddMenu()
		{
			int flag = 0;
			try
			{
				string MenuName = this.Request["MenuName"];
				int MenuType = Convert.ToInt32(this.Request["MenuType"]);
				string MenuKey = this.Request["MenuKey"];
				string MenuUrl = this.Request["MenuUrl"];
				int parentMenuID = Convert.ToInt32(this.Request["parentMenuID"]);
				if (MenuType == 1)
				{
					MenuUrl = "";
				}
				else
				{
					MenuKey = "";
				}
				Chain.BLL.WeiXinMenu WeiXinMenuBll = new Chain.BLL.WeiXinMenu();
				Chain.Model.WeiXinMenu WeiXinMenuModel = new Chain.Model.WeiXinMenu
				{
					MenuKey = MenuKey,
					MenuName = MenuName,
					MenuType = MenuType,
					MenuUrl = MenuUrl,
					parentMenuID = parentMenuID
				};
				flag = WeiXinMenuBll.Add(WeiXinMenuModel);
			}
			catch (Exception)
			{
			}
			this.Response.Write(flag);
		}

		public void GetMenuFirstList()
		{
			int MenuID = Convert.ToInt32(this.Request["MenuID"]);
			StringBuilder sbStr = new StringBuilder();
			sbStr.Append("<select id=\"sltMenuSecondType\" class=\"selectWidth\" style=\"width: 150px;\">");
			DataTable dt = new Chain.BLL.WeiXinMenu().GetMenuParentInfo().Tables[0];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (Convert.ToInt32(dt.Rows[i]["MenuID"]) == MenuID)
				{
					sbStr.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", dt.Rows[i]["MenuID"], dt.Rows[i]["MenuName"]);
				}
				else if (Convert.ToInt32(dt.Rows[i]["childNum"]) != 5)
				{
					sbStr.AppendFormat("<option value=\"{0}\">{1}</option>", dt.Rows[i]["MenuID"], dt.Rows[i]["MenuName"]);
				}
			}
			sbStr.Append("</select>");
			this.Response.Write(sbStr.ToString());
		}

		public void UpdateMenuBySystem()
		{
			int flag = 0;
			try
			{
				int MenuID = Convert.ToInt32(this.Request["MenuID"]);
				int parentMenuID = Convert.ToInt32(this.Request["parentMenuID"]);
				Chain.BLL.WeiXinMenu WeiXinMenuBll = new Chain.BLL.WeiXinMenu();
				Chain.Model.WeiXinMenu WeiXinMenuModel = WeiXinMenuBll.GetModel(MenuID);
				WeiXinMenuModel.parentMenuID = parentMenuID;
				flag = (WeiXinMenuBll.Update(WeiXinMenuModel) ? 1 : 0);
			}
			catch (Exception)
			{
			}
			this.Response.Write(flag);
		}

		public void UpdataMenuFirst()
		{
			int flag = 0;
			try
			{
				int MenuID = Convert.ToInt32(this.Request["MenuID"]);
				string MenuName = this.Request["MenuName"];
				Chain.BLL.WeiXinMenu WeiXinMenuBll = new Chain.BLL.WeiXinMenu();
				Chain.Model.WeiXinMenu WeiXinMenuModel = WeiXinMenuBll.GetModel(MenuID);
				WeiXinMenuModel.MenuName = MenuName;
				flag = (WeiXinMenuBll.Update(WeiXinMenuModel) ? 1 : 0);
			}
			catch (Exception)
			{
			}
			this.Response.Write(flag);
		}

		public void GetMenuList()
		{
			int MenuID = Convert.ToInt32(this.Request["MenuID"]);
			Chain.BLL.WeiXinMenu WeiXinMenuBll = new Chain.BLL.WeiXinMenu();
			DataTable dt = WeiXinMenuBll.GetList("MenuID=" + MenuID).Tables[0];
			string flag = JsonPlus.ToJson(dt, "MenuID,MenuName,MenuType,MenuKey,MenuUrl,parentMenuID");
			this.Response.Write(flag);
		}

		public void EditMenu()
		{
			int flag = 0;
			try
			{
				string MenuName = this.Request["MenuName"];
				int MenuType = Convert.ToInt32(this.Request["MenuType"]);
				string MenuKey = this.Request["MenuKey"];
				string MenuUrl = this.Request["MenuUrl"];
				int parentMenuID = Convert.ToInt32(this.Request["parentMenuID"]);
				int MenuID = Convert.ToInt32(this.Request["MenuID"]);
				if (MenuType == 1)
				{
					MenuUrl = "";
				}
				else
				{
					MenuKey = "";
				}
				Chain.BLL.WeiXinMenu WeiXinMenuBll = new Chain.BLL.WeiXinMenu();
				Chain.Model.WeiXinMenu WeiXinMenuModel = new Chain.Model.WeiXinMenu
				{
					MenuKey = MenuKey,
					MenuName = MenuName,
					MenuType = MenuType,
					MenuUrl = MenuUrl,
					parentMenuID = parentMenuID,
					MenuID = MenuID
				};
				flag = (WeiXinMenuBll.Update(WeiXinMenuModel) ? 1 : 0);
			}
			catch (Exception)
			{
			}
			this.Response.Write(flag);
		}

		public void DelMenu()
		{
			int flag = 0;
			try
			{
				int MenuID = Convert.ToInt32(this.Request["MenuID"]);
				Chain.BLL.WeiXinMenu WeiXinMenuBll = new Chain.BLL.WeiXinMenu();
				flag = (WeiXinMenuBll.Delete(MenuID) ? 1 : 0);
			}
			catch (Exception)
			{
			}
			this.Response.Write(flag);
		}

		public void CreateMenu()
		{
			int flag = 0;
			try
			{
				string intGoodsID = this.Request["CreateMenu"];
				if (string.IsNullOrEmpty(PubFunction.curParameter.strWeiXinAppID))
				{
					flag = 1;
				}
				else if (string.IsNullOrEmpty(PubFunction.curParameter.strWeiXinAppSecret))
				{
					flag = 2;
				}
				else
				{
					string intShopID = PubFunction.curParameter.strWeiXinAppID;
					string intUserID = PubFunction.curParameter.strWeiXinAppSecret;
					string intUserShopID = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", intShopID, intUserID);
					string dtCount = new WebClient
					{
						Encoding = Encoding.UTF8
					}.DownloadString(intUserShopID);
					string dtLog = string.Empty;
					string dt = "{\"access_token\":\"(.+)\",\"expires_in\":7200}";
					Match match = Regex.Match(dtCount, dt);
					if (match.Success)
					{
						dtLog = match.Groups[1].Value;
					}
					else
					{
						flag = 3;
					}
					if (!string.IsNullOrEmpty(dtLog))
					{
						Chain.BLL.WeiXinMenu menu = new Chain.BLL.WeiXinMenu();
						DataTable dtMenu = menu.GetList("").Tables[0];
						string s = ResponseSendStr.Menu(dtMenu);
						intUserShopID = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", dtLog);
						using (MemoryStream stream = new MemoryStream())
						{
							byte[] bytes = Encoding.UTF8.GetBytes(s);
							stream.Write(bytes, 0, bytes.Length);
							HttpWebRequest request = (HttpWebRequest)WebRequest.Create(intUserShopID);
							request.Method = "POST";
							request.ContentLength = stream.Length;
							request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
							request.KeepAlive = true;
							request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
							if (stream != null)
							{
								stream.Position = 0L;
								Stream requestStream = request.GetRequestStream();
								byte[] buffer2 = new byte[1024];
								int count;
								while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
								{
									requestStream.Write(bytes, 0, count);
								}
								stream.Close();
							}
							HttpWebResponse response = (HttpWebResponse)request.GetResponse();
							using (Stream stream2 = response.GetResponseStream())
							{
								using (StreamReader reader = new StreamReader(stream2, Encoding.GetEncoding("utf-8")))
								{
									if (reader.ReadToEnd() == "{\"errcode\":0,\"errmsg\":\"ok\"}")
									{
										flag = 4;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Chain.BLL.SysError.Add(e, PubFunction.ipAdress);
			}
			this.Response.Write(flag);
		}

		public void ShopBuyCard()
		{
			int flag = 0;
			try
			{
				string IsAllianceProgram = this.Request["IsAllianceProgram"].ToString();
				string StartCardNumber = this.Request["StartCardNumber"].ToString();
				string EndCardNumber = this.Request["EndCardNumber"].ToString();
				int ShopID = int.Parse(this.Request["ShopID"].ToString());
				int BuyType = int.Parse(this.Request["BuyType"].ToString());
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(ShopID);
				if (IsAllianceProgram == "False" && !modelShop.IsMain)
				{
					if (!PubFunction.isInAllianceBuyCard(StartCardNumber, EndCardNumber, ShopID))
					{
						flag = -2;
					}
					else if (PubFunction.IsCanBuyCard(this.Request["StartCardNumber"].ToString(), this.Request["EndCardNumber"].ToString(), IsAllianceProgram, ShopID))
					{
						Chain.BLL.SysShopBuyCard bllSysShopBuyCard = new Chain.BLL.SysShopBuyCard();
						flag = bllSysShopBuyCard.Add(new Chain.Model.SysShopBuyCard
						{
							BuyCardMoney = (string.IsNullOrEmpty(this.Request["BuyCardMoney"].ToString()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.Request["BuyCardMoney"])),
							BuyCardShopid = Convert.ToInt32(this.Request["ShopID"]),
							BuyCardTime = DateTime.Now,
							EndCardNumber = this.Request["EndCardNumber"].ToString(),
							Remark = this.Request["Remark"].ToString(),
							StartCardNumber = this.Request["StartCardNumber"].ToString(),
							UserID = this.UserModel.UserID,
							BuyType = BuyType
						});
					}
					else
					{
						flag = -1;
					}
				}
				else if (PubFunction.IsCanBuyCard(this.Request["StartCardNumber"].ToString(), this.Request["EndCardNumber"].ToString(), IsAllianceProgram, ShopID))
				{
					Chain.BLL.SysShopBuyCard bllSysShopBuyCard = new Chain.BLL.SysShopBuyCard();
					flag = bllSysShopBuyCard.Add(new Chain.Model.SysShopBuyCard
					{
						BuyCardMoney = (string.IsNullOrEmpty(this.Request["BuyCardMoney"].ToString()) ? Convert.ToDecimal(0) : Convert.ToDecimal(this.Request["BuyCardMoney"])),
						BuyCardShopid = Convert.ToInt32(this.Request["ShopID"]),
						BuyCardTime = DateTime.Now,
						EndCardNumber = this.Request["EndCardNumber"].ToString(),
						Remark = this.Request["Remark"].ToString(),
						StartCardNumber = this.Request["StartCardNumber"].ToString(),
						UserID = this.UserModel.UserID,
						BuyType = BuyType
					});
				}
				else
				{
					flag = -1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(flag);
		}

		public void GetShopSettlement()
		{
			string flag = "";
			string id = this.Request["ID"].ToString();
			DataTable dt = new Chain.BLL.SysShopSettlement().GetList(string.Format(" id = '{0}'", id)).Tables[0];
			try
			{
				flag = JsonPlus.ToJson(dt.Rows[0], "");
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void GetShopPointSettlement()
		{
			string flag = "";
			string id = this.Request["ID"].ToString();
			DataTable dt = new Chain.BLL.SysShopPointSettlement().GetList(string.Format(" id = '{0}'", id)).Tables[0];
			try
			{
				flag = JsonPlus.ToJson(dt.Rows[0], "");
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void SetShopPointSettlement()
		{
			int flag = 0;
			try
			{
				int id = Convert.ToInt32(this.Request["ID"]);
				Chain.Model.SysShopPointSettlement mdSysShopSettlement = new Chain.Model.SysShopPointSettlement();
				Chain.BLL.SysShopPointSettlement bllSysShopSettlement = new Chain.BLL.SysShopPointSettlement();
				mdSysShopSettlement.ID = id;
				mdSysShopSettlement.IsFinish = true;
				mdSysShopSettlement.UserID = this.UserModel.UserID;
				mdSysShopSettlement.Remark = this.Request["Remark"].ToString();
				mdSysShopSettlement.FinishTime = DateTime.Now;
				bllSysShopSettlement.Update(mdSysShopSettlement);
				flag = 2;
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void SetShopSettlement()
		{
			int flag = 0;
			try
			{
				int id = Convert.ToInt32(this.Request["ID"]);
				Chain.Model.SysShopSettlement mdSysShopSettlement = new Chain.Model.SysShopSettlement();
				Chain.BLL.SysShopSettlement bllSysShopSettlement = new Chain.BLL.SysShopSettlement();
				mdSysShopSettlement = bllSysShopSettlement.GetModel(id);
				mdSysShopSettlement.IsFinish = true;
				mdSysShopSettlement.UserID = this.UserModel.UserID;
				mdSysShopSettlement.Remark = this.Request["Remark"].ToString();
				mdSysShopSettlement.FinishTime = DateTime.Now;
				bllSysShopSettlement.Update(mdSysShopSettlement);
				flag = 2;
			}
			catch
			{
			}
			this.Context.Response.Write(flag);
		}

		public void ForeverTicketUrl()
		{
			string shopid = this.Request["shopid"];
			string errorCode;
			string errorMessage;
			string accessToken = AccessToken.Get(PubFunction.curParameter.strWeiXinAppID, PubFunction.curParameter.strWeiXinAppSecret, out errorCode, out errorMessage);
			string dataTemplate = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"{scene_str}\"}}}";
			dataTemplate = dataTemplate.Replace("{scene_str}", shopid);
			string templateUrl = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";
			templateUrl = string.Format(templateUrl, accessToken);
			HttpRequestHelper hrm = new HttpRequestHelper();
			string reponseText = hrm.Reqeust(templateUrl, dataTemplate);
			string str = "[" + reponseText + "]";
			JavaScriptArray javascript = (JavaScriptArray)JavaScriptConvert.DeserializeObject(str);
			JavaScriptObject obj = (JavaScriptObject)javascript[0];
			if (obj["url"] != null)
			{
				string value = obj["url"].ToString();
				this.Context.Response.Write("\"" + value + "\"");
			}
			else
			{
				this.Context.Response.Write("\"暂未开通此项功能！\"");
			}
		}

		public void SMSverificationcode()
		{
			string flag = "0";
			try
			{
				string Mobile = this.Request["Mobile"].ToString();
				Chain.Model.Mem modelMem = new Chain.Model.Mem();
				modelMem = new Chain.BLL.Mem().GetModelByMemCard(Mobile);
				if (modelMem != null)
				{
					flag = "3";
				}
				else
				{
					Random ran = new Random();
					int RandKey = ran.Next(100000, 999999);
					bool tf = SMSInfo.Send_GXSMS(false, Mobile, PubFunction.ShopIDToName(1) + " 注册手机验证码:" + RandKey.ToString() + "(如非本人操作,请忽略.)", "");
					if (tf)
					{
						flag = RandKey.ToString();
					}
					else
					{
						flag = "2";
					}
				}
			}
			catch
			{
				flag = "0";
			}
			this.Context.Response.Write(flag);
		}

		public void SpecialAdd()
		{
			int flag = 0;
			try
			{
				string specialname = this.Request["txtSpecialName"];
				decimal specialrecharge = Convert.ToDecimal(this.Request["txtMoney"]);
				decimal specialgive = Convert.ToDecimal(this.Request["txtGiveMoney"]);
				string sremark = this.Request["txtSpecialRemark"];
				int specialuser = Convert.ToInt32(this.Request["SpecialUSerID"]);
				string type = this.Request["Type"];
				string startTime = this.Request["txtStartTime"];
				string endTime = this.Request["txtEndTime"];
				string week = this.Request["txtWeek"];
				string month = this.Request["txtMonth"];
				int intType = 0;
				string text = type;
				if (text != null)
				{
					if (!(text == "Date"))
					{
						if (!(text == "Week"))
						{
							if (!(text == "Month"))
							{
								if (text == "Birthday")
								{
									intType = 4;
								}
							}
							else
							{
								intType = 3;
							}
						}
						else
						{
							intType = 2;
						}
					}
					else
					{
						intType = 1;
					}
				}
				Chain.Model.Special mds = new Chain.Model.Special();
				Chain.BLL.Special blls = new Chain.BLL.Special();
				mds.SpecialUser = specialuser;
				mds.SpecialName = specialname;
				mds.SpecialRecharge = specialrecharge;
				mds.SpecialGive = specialgive;
				mds.SpecialTime = DateTime.Now;
				mds.Sremark = sremark;
				mds.Type = intType;
				mds.StartTime = ((startTime != "") ? Convert.ToDateTime(startTime) : DateTime.Now);
				mds.EndTime = ((endTime != "") ? Convert.ToDateTime(endTime) : DateTime.Now);
				mds.Week = week;
				mds.Month = month;
				flag = blls.Add(mds);
			}
			catch (Exception e_1DA)
			{
				flag = -1;
				throw;
			}
			this.Context.Response.Write(flag);
		}

		public void SpecialList()
		{
			string msgResponse = "";
			try
			{
				int id = int.Parse(this.Request["SpecialID"]);
				Chain.BLL.Special blls = new Chain.BLL.Special();
				DataTable dts = blls.GetItemAll(id).Tables[0];
				if (dts != null)
				{
					msgResponse = JsonPlus.ToJson(dts, "SpecialID,SpecialName,SpecialRecharge,SpecialGive,Sremark,SpecialUser,SpecialTime");
				}
			}
			catch (Exception e_52)
			{
				throw;
			}
			this.Context.Response.Write(msgResponse);
		}

		public void SpecialEdit()
		{
			int flag = 0;
			try
			{
				int id = int.Parse(this.Request["txtSpecialID"]);
				string specialname = this.Request["txtSpecialName"];
				decimal specialrecharge = Convert.ToDecimal(this.Request["txtMoney"]);
				decimal specialgive = Convert.ToDecimal(this.Request["txtGiveMoney"]);
				string sremark = this.Request["txtSpecialRemark"];
				int specialuser = Convert.ToInt32(this.Request["SpecialUSerID"]);
				string type = this.Request["Type"];
				string startTime = this.Request["txtStartTime"];
				string endTime = this.Request["txtEndTime"];
				string week = this.Request["txtWeek"];
				string month = this.Request["txtMonth"];
				int intType = 0;
				string text = type;
				if (text != null)
				{
					if (!(text == "Date"))
					{
						if (!(text == "Week"))
						{
							if (!(text == "Month"))
							{
								if (text == "Birthday")
								{
									intType = 4;
								}
							}
							else
							{
								intType = 3;
							}
						}
						else
						{
							intType = 2;
						}
					}
					else
					{
						intType = 1;
					}
				}
				Chain.Model.Special mds = new Chain.Model.Special();
				Chain.BLL.Special blls = new Chain.BLL.Special();
				mds.SpecialID = id;
				mds.SpecialUser = specialuser;
				mds.SpecialName = specialname;
				mds.SpecialRecharge = specialrecharge;
				mds.SpecialGive = specialgive;
				mds.SpecialTime = DateTime.Now;
				mds.Sremark = sremark;
				mds.Type = intType;
				mds.StartTime = ((startTime != "") ? Convert.ToDateTime(startTime) : DateTime.Now);
				mds.EndTime = ((endTime != "") ? Convert.ToDateTime(endTime) : DateTime.Now);
				mds.Week = week;
				mds.Month = month;
				flag = blls.Update(mds);
			}
			catch (Exception e_1FB)
			{
				flag = -1;
				throw;
			}
			this.Context.Response.Write(flag);
		}

		public void SpecialDelete()
		{
			string msgResponse = "0";
			try
			{
				int SpecialID = (this.Request["SpecialID"].ToString() != "") ? int.Parse(this.Request["SpecialID"].ToString()) : 0;
				Chain.BLL.Special blls = new Chain.BLL.Special();
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				if (blls.Delete(SpecialID))
				{
					msgResponse = "1";
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				msgResponse = "0";
			}
			msgResponse = "{\"result\":\"" + msgResponse + "\"}";
			this.Context.Response.Write(msgResponse);
		}

		public void BindSpecialselect()
		{
			string msgResponse = "";
			DataTable dt = new DataTable();
			try
			{
				int memID = (int)Convert.ToInt16(this.Request["MemID"]);
				Chain.Model.Mem memModel = new Chain.BLL.Mem().GetModel(memID);
				DataTable dtSpecial = new Chain.BLL.Special().GetList("SpecialID!=0").Tables[0];
				dt.Columns.Add("Name");
				dt.Columns.Add("ID");
				foreach (DataRow drSpecial in dtSpecial.Rows)
				{
					string text = drSpecial["Type"].ToString();
					if (text != null)
					{
						if (!(text == "1"))
						{
							if (!(text == "2"))
							{
								if (!(text == "3"))
								{
									if (text == "4")
									{
										DateTime arg_303_0 = memModel.MemBirthday;
										bool flag = 1 == 0;
										if (memModel.MemBirthday.Month == DateTime.Now.Month && memModel.MemBirthday.Day == DateTime.Now.Day)
										{
											DataRow dr = dt.NewRow();
											object[] objs = new object[]
											{
												drSpecial["SpecialName"].ToString(),
												drSpecial["SpecialID"].ToString()
											};
											dr.ItemArray = objs;
											dt.Rows.Add(dr);
										}
									}
								}
								else
								{
									string day = "|" + Convert.ToInt32(DateTime.Now.Day).ToString() + "|";
									string strMonth = "|" + drSpecial["Month"].ToString() + "|";
									if (strMonth.IndexOf(day) > -1)
									{
										DataRow dr = dt.NewRow();
										object[] objs = new object[]
										{
											drSpecial["SpecialName"].ToString(),
											drSpecial["SpecialID"].ToString()
										};
										dr.ItemArray = objs;
										dt.Rows.Add(dr);
									}
								}
							}
							else
							{
								int week = Convert.ToInt32(DateTime.Now.DayOfWeek);
								if (drSpecial["Week"].ToString().IndexOf(week.ToString()) > -1)
								{
									DataRow dr = dt.NewRow();
									object[] objs = new object[]
									{
										drSpecial["SpecialName"].ToString(),
										drSpecial["SpecialID"].ToString()
									};
									dr.ItemArray = objs;
									dt.Rows.Add(dr);
								}
							}
						}
						else if (DateTime.Parse(drSpecial["StartTime"].ToString()) < DateTime.Now && DateTime.Now < DateTime.Parse(drSpecial["EndTime"].ToString()))
						{
							DataRow dr = dt.NewRow();
							object[] objs = new object[]
							{
								drSpecial["SpecialName"].ToString(),
								drSpecial["SpecialID"].ToString()
							};
							dr.ItemArray = objs;
							dt.Rows.Add(dr);
						}
					}
				}
				msgResponse = JsonPlus.ToJson(dt, "Name,ID");
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void TransferMoney()
		{
			int flag = 0;
			try
			{
				string transferToMemCard = this.Request["transferToMemCard"];
				decimal money = Convert.ToDecimal(this.Request["money"]);
				string password = this.Request["password"];
				int transferFromMemID = (int)Convert.ToInt16(this.Request["transferFromMemID"]);
				string transferRemark = this.Request["transferRemark"];
				string transferTime = this.Request["transferTime"];
				string account = this.Request["account"];
				password = ((password == "#") ? DESEncrypt.Encrypt("") : DESEncrypt.Encrypt(this.Request["password"].ToString()));
				Chain.BLL.Mem MemBll = new Chain.BLL.Mem();
				Chain.Model.Mem transferFromMemModel = MemBll.GetModel(transferFromMemID);
				if (MemBll.CheckMemPwd(transferFromMemModel.MemCard, password) == null)
				{
					flag = -1;
					this.Context.Response.Write(flag);
					return;
				}
				Chain.Model.Mem transferToMemModel = MemBll.GetModel(transferToMemCard);
				if (transferToMemModel == null)
				{
					flag = -2;
					this.Context.Response.Write(flag);
					return;
				}
				Chain.Model.MemTransferLog modelMemTransferLog = new Chain.Model.MemTransferLog();
				Chain.BLL.MemTransferLog bllMemTransferLog = new Chain.BLL.MemTransferLog();
				modelMemTransferLog.TransferAccount = account;
				modelMemTransferLog.TransferCreateTime = Convert.ToDateTime(transferTime);
				modelMemTransferLog.TransferFromMemID = transferFromMemID;
				modelMemTransferLog.TransferToMemID = transferToMemModel.MemID;
				modelMemTransferLog.TransferRemark = transferRemark;
				modelMemTransferLog.UserID = this.UserModel.UserID;
				modelMemTransferLog.TransferMoney = money;
				flag = bllMemTransferLog.Add(modelMemTransferLog);
				MemBll.UpdateMoney(transferFromMemModel.MemID, -money);
				MemBll.UpdateMoney(transferToMemModel.MemID, money);
				Chain.BLL.MoneyChangeLog bllMoneyChangeLog = new Chain.BLL.MoneyChangeLog();
				Chain.Model.MoneyChangeLog modelMoneyChangeLog = new Chain.Model.MoneyChangeLog();
				modelMoneyChangeLog.MoneyChangeMemID = transferFromMemModel.MemID;
				modelMoneyChangeLog.MoneyChangeUserID = this.UserModel.UserID;
				modelMoneyChangeLog.MoneyChangeType = 16;
				modelMoneyChangeLog.MoneyChangeAccount = account;
				modelMoneyChangeLog.MoneyChangeMoney = money;
				modelMoneyChangeLog.MoneyChangeCash = 0m;
				modelMoneyChangeLog.MoneyChangeBalance = -money;
				modelMoneyChangeLog.MoneyChangeUnionPay = 0m;
				modelMoneyChangeLog.MemMoney = transferFromMemModel.MemMoney - money;
				modelMoneyChangeLog.MoneyChangeGiveMoney = 0m;
				modelMoneyChangeLog.MoneyChangeCreateTime = Convert.ToDateTime(transferTime);
				bllMoneyChangeLog.Add(modelMoneyChangeLog);
				modelMoneyChangeLog.MoneyChangeMemID = transferToMemModel.MemID;
				modelMoneyChangeLog.MoneyChangeUserID = this.UserModel.UserID;
				modelMoneyChangeLog.MoneyChangeType = 17;
				modelMoneyChangeLog.MoneyChangeAccount = account;
				modelMoneyChangeLog.MoneyChangeMoney = money;
				modelMoneyChangeLog.MoneyChangeCash = 0m;
				modelMoneyChangeLog.MoneyChangeBalance = money;
				modelMoneyChangeLog.MoneyChangeUnionPay = 0m;
				modelMoneyChangeLog.MemMoney = transferToMemModel.MemMoney + money;
				modelMoneyChangeLog.MoneyChangeGiveMoney = 0m;
				modelMoneyChangeLog.MoneyChangeCreateTime = Convert.ToDateTime(transferTime);
				bllMoneyChangeLog.Add(modelMoneyChangeLog);
			}
			catch (Exception e_32C)
			{
				flag = -3;
				throw;
			}
			this.Context.Response.Write(flag);
		}

		public void AddSubMem()
		{
			int flag = 0;
			try
			{
				string subCardNumber = this.Request["subCardNumber"];
				string subName = this.Request["subName"];
				string subMemMobile = this.Request["subMemMobile"];
				string MemCard = this.Request["MemCard"];
				int MemID = Convert.ToInt32(this.Request["MemID"]);
				string isUsed = this.Request["isUsed"];
				Chain.Model.SubMem modelSubMem = new Chain.Model.SubMem();
				Chain.BLL.SubMem bllSubMem = new Chain.BLL.SubMem();
				if (bllSubMem.IsHasMemCard(subCardNumber, this.UserModel.UserShopID))
				{
					flag = -2;
					this.Context.Response.Write(flag);
					return;
				}
				if (bllSubMem.IsHasMemCard(subMemMobile, this.UserModel.UserShopID))
				{
					flag = -3;
					this.Context.Response.Write(flag);
					return;
				}
				modelSubMem.SubCardNumber = subCardNumber;
				modelSubMem.SubName = subName;
				modelSubMem.SubMemMobile = subMemMobile;
				modelSubMem.MemCard = MemCard;
				modelSubMem.MemID = MemID;
				modelSubMem.IsUsed = Convert.ToBoolean(isUsed);
				flag = bllSubMem.Add(modelSubMem);
			}
			catch (Exception e_12A)
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GetSubMemList()
		{
			string msgResponse = "";
			try
			{
				int memID = int.Parse(this.Request["memID"].ToString());
				Chain.BLL.SubMem bllSubMem = new Chain.BLL.SubMem();
				DataTable dtSubMem = bllSubMem.GetList(" MemID = " + memID).Tables[0];
				if (dtSubMem.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtSubMem, "ID,SubCardNumber,MemID,MemCard,SubName,SubMemMobile,IsUsed");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void SubMemDelete()
		{
			try
			{
				int ID = int.Parse(this.Request["ID"].ToString());
				Chain.BLL.SubMem bllSubMem = new Chain.BLL.SubMem();
				int arg_32_0 = bllSubMem.Delete(ID) ? 1 : 0;
			}
			catch
			{
			}
		}

		public void SubMemInfo()
		{
			string msgResponse = "";
			try
			{
				int ID = (this.Request["ID"].ToString() != "") ? int.Parse(this.Request["ID"].ToString()) : 0;
				Chain.BLL.SubMem bllSubMem = new Chain.BLL.SubMem();
				DataTable dtSubMem = bllSubMem.GetList(" ID=" + ID).Tables[0];
				if (dtSubMem.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtSubMem, "ID,SubCardNumber,MemID,MemCard,SubName,SubMemMobile,IsUsed");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void EditSubMem()
		{
			int flag = 0;
			try
			{
				int SubMemID = Convert.ToInt32(this.Request["SubMemID"]);
				string subCardNumber = this.Request["subCardNumber"];
				string subName = this.Request["subName"];
				string subMemMobile = this.Request["subMemMobile"];
				string MemCard = this.Request["MemCard"];
				int MemID = Convert.ToInt32(this.Request["MemID"]);
				string isUsed = this.Request["isUsed"];
				Chain.Model.SubMem modelSubMem = new Chain.Model.SubMem();
				Chain.BLL.SubMem bllSubMem = new Chain.BLL.SubMem();
				modelSubMem = bllSubMem.GetModel(SubMemID);
				if (modelSubMem.SubCardNumber != subCardNumber)
				{
					if (bllSubMem.IsHasMemCard(subCardNumber, this.UserModel.UserShopID))
					{
						flag = -2;
						this.Context.Response.Write(flag);
						return;
					}
				}
				if (modelSubMem.SubMemMobile != subMemMobile)
				{
					if (bllSubMem.IsHasMemCard(subMemMobile, this.UserModel.UserShopID))
					{
						flag = -3;
						this.Context.Response.Write(flag);
						return;
					}
				}
				modelSubMem.SubCardNumber = subCardNumber;
				modelSubMem.SubName = subName;
				modelSubMem.SubMemMobile = subMemMobile;
				modelSubMem.IsUsed = Convert.ToBoolean(isUsed);
				if (bllSubMem.Update(modelSubMem))
				{
					flag = 1;
				}
			}
			catch (Exception e_176)
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void ShopBuyCardDelete()
		{
			int flag = 0;
			try
			{
				int ByCardID = (this.Request["BuyCardID"].ToString() != "") ? int.Parse(this.Request["BuyCardID"].ToString()) : 0;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.BLL.SysShopBuyCard bllClass = new Chain.BLL.SysShopBuyCard();
				Chain.Model.SysShopBuyCard modelClass = bllClass.GetModel(ByCardID);
				if (bllClass.Delete(ByCardID))
				{
					flag = 1;
					PubFunction.SaveSysLog(intUserID, 2, "购卡撤销", "购卡撤销", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void AddNews()
		{
			int flag = 0;
			try
			{
				Chain.Model.News NewsModel = new Chain.Model.News();
				Chain.BLL.News NewsBll = new Chain.BLL.News();
				string NewsName = this.Request["NewsName"];
				string NewsPhoto = "../Upload/MicroWebsite/MicroWebsiteNews/" + this.Request["NewsPhoto"];
				string NewsDesc = this.Request["NewsDesc"];
				string NewsRemark = this.Request["NewsRemark"];
				int IsRecommend = (this.Request["IsRecommend"] != null && this.Request["IsRecommend"] == "on") ? 1 : 0;
				NewsModel.NewsName = NewsName;
				NewsModel.NewsPhoto = NewsPhoto;
				NewsModel.NewsDesc = NewsDesc;
				NewsModel.NewsCreateTime = DateTime.Now;
				NewsModel.CreateUserID = this.UserModel.UserID;
				NewsModel.NewsRemark = NewsRemark;
				NewsModel.IsRecommend = IsRecommend;
				flag = NewsBll.Add(NewsModel);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelNews()
		{
			int flag = 0;
			try
			{
				int NewsID = int.Parse(this.Request["NewsID"]);
				flag = (new Chain.BLL.News().Delete(NewsID) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditNews()
		{
			int flag = 0;
			try
			{
				Chain.BLL.News NewsBll = new Chain.BLL.News();
				int NewsID = int.Parse(this.Request["NewsID"]);
				string NewsName = this.Request["NewsName"];
				string NewsPhoto = "../Upload/MicroWebsite/MicroWebsiteNews/" + Path.GetFileName(this.Request["NewsPhoto"]);
				string NewsDesc = this.Request["NewsDesc"];
				string NewsRemark = this.Request["NewsRemark"];
				int IsRecommend = (this.Request["IsRecommend"] != null && this.Request["IsRecommend"] == "on") ? 1 : 0;
				Chain.Model.News NewsModel = NewsBll.GetModel(NewsID);
				NewsModel.NewsName = NewsName;
				NewsModel.NewsPhoto = NewsPhoto;
				NewsModel.NewsDesc = NewsDesc;
				NewsModel.NewsRemark = NewsRemark;
				NewsModel.IsRecommend = IsRecommend;
				flag = (NewsBll.Update(NewsModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void AddAlbum()
		{
			int flag = 0;
			try
			{
				Chain.Model.Album AlbumModel = new Chain.Model.Album();
				Chain.BLL.Album AlbumBll = new Chain.BLL.Album();
				string AlbumTitle = this.Request["AlbumTitle"];
				string AlbumPhoto = "../Upload/MicroWebsite/MicroWebsiteAlbum/" + this.Request["AlbumPhoto"];
				string AlbumDesc = this.Request["AlbumDesc"];
				AlbumModel.AlbumName = AlbumTitle;
				AlbumModel.AlbumPhoto = AlbumPhoto;
				AlbumModel.AlbumDesc = AlbumDesc;
				AlbumModel.AlbumCreateTime = DateTime.Now;
				AlbumModel.CreateUserID = this.UserModel.UserID;
				flag = AlbumBll.Add(AlbumModel);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelAlbum()
		{
			int flag = 0;
			try
			{
				int AlbumID = int.Parse(this.Request["AlbumID"]);
				flag = (new Chain.BLL.Album().Delete(AlbumID) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditAlbum()
		{
			int flag = 0;
			try
			{
				Chain.BLL.Album AlbumBll = new Chain.BLL.Album();
				int AlbumID = int.Parse(this.Request["AlbumID"]);
				string AlbumName = this.Request["AlbumTitle"];
				string AlbumPhoto = "../Upload/MicroWebsite/MicroWebsiteAlbum/" + Path.GetFileName(this.Request["AlbumPhoto"]);
				string AlbumDesc = this.Request["AlbumDesc"];
				Chain.Model.Album AlbumModel = AlbumBll.GetModel(AlbumID);
				AlbumModel.AlbumName = AlbumName;
				AlbumModel.AlbumPhoto = AlbumPhoto;
				AlbumModel.AlbumDesc = AlbumDesc;
				flag = (AlbumBll.Update(AlbumModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void AddPhoto()
		{
			int flag = 0;
			try
			{
				Chain.Model.Photo PhotoModel = new Chain.Model.Photo();
				Chain.BLL.Photo PhotoBll = new Chain.BLL.Photo();
				string PhotoTitle = this.Request["PhotoTitle"];
				string PhotoPhoto = "../Upload/MicroWebsite/MicroWebsitePhoto/" + this.Request["PhotoPhoto"];
				string PhotoDesc = this.Request["PhotoDesc"];
				int AlbumID = int.Parse(this.Request["AlbumID"]);
				PhotoModel.PhotoName = PhotoTitle;
				PhotoModel.PhotoPhoto = PhotoPhoto;
				PhotoModel.PhotoDesc = PhotoDesc;
				PhotoModel.PhotoCreateTime = DateTime.Now;
				PhotoModel.CreateUserID = this.UserModel.UserID;
				PhotoModel.AlbumID = AlbumID;
				flag = PhotoBll.Add(PhotoModel);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelPhoto()
		{
			int flag = 0;
			try
			{
				int PhotoID = int.Parse(this.Request["PhotoID"]);
				flag = (new Chain.BLL.Photo().Delete(PhotoID) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditPhoto()
		{
			int flag = 0;
			try
			{
				Chain.BLL.Photo PhotoBll = new Chain.BLL.Photo();
				int AlbumID = int.Parse(this.Request["AlbumID"]);
				int PhotoID = int.Parse(this.Request["PhotoID"]);
				string PhotoName = this.Request["PhotoTitle"];
				string PhotoPhoto = "../Upload/MicroWebsite/MicroWebsitePhoto/" + Path.GetFileName(this.Request["PhotoPhoto"]);
				string PhotoDesc = this.Request["PhotoDesc"];
				Chain.Model.Photo PhotoModel = PhotoBll.GetModel(PhotoID);
				PhotoModel.PhotoName = PhotoName;
				PhotoModel.PhotoPhoto = PhotoPhoto;
				PhotoModel.PhotoDesc = PhotoDesc;
				PhotoModel.AlbumID = AlbumID;
				flag = (PhotoBll.Update(PhotoModel) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void OnlineAsk()
		{
			int flag = 0;
			try
			{
				Chain.BLL.OnlineMessage bllProposal = new Chain.BLL.OnlineMessage();
				int MemID = int.Parse(this.Request["MemID"]);
				string MemCard = this.Request["MemCard"];
				string message = this.Request["message"];
				flag = bllProposal.Add(new Chain.Model.OnlineMessage
				{
					MemID = new int?(MemID),
					MemCard = MemCard,
					MessageContent = message,
					MessageTime = new DateTime?(DateTime.Now),
					MessageType = 0,
					IsShow = 1
				});
			}
			catch (Exception e_9B)
			{
				flag = 0;
			}
			this.Response.Write(flag);
		}

		public void MessageReplyNew()
		{
			int flag = 0;
			try
			{
				Chain.BLL.OnlineMessage bllMessage = new Chain.BLL.OnlineMessage();
				int intMessageID = (this.Request["messageID"] != null) ? int.Parse(this.Request["messageID"].ToString()) : 0;
				string strContent = (this.Request["content"] != null) ? this.Request["content"].ToString() : "";
				Chain.Model.OnlineMessage modelMessage = bllMessage.GetModel(intMessageID);
				modelMessage.MessageID = intMessageID;
				modelMessage.ReplyContent = strContent;
				modelMessage.ReplyTime = DateTime.Now;
				modelMessage.ReplyUserID = this.UserModel.UserID;
				modelMessage.IsReply = 1;
				modelMessage.IsShow = 0;
				if (bllMessage.Reply(modelMessage))
				{
					flag = bllMessage.Add(new Chain.Model.OnlineMessage
					{
						MemID = modelMessage.MemID,
						MemCard = modelMessage.MemCard,
						MessageContent = strContent,
						MessageTime = new DateTime?(DateTime.Now),
						MessageType = 1
					});
					flag = 1;
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GetReplyInfo()
		{
			string msgResponse = "";
			try
			{
				string MemCard = this.Request["MemCard"];
				Chain.BLL.OnlineMessage bllOnlineMessage = new Chain.BLL.OnlineMessage();
				DataTable dt = bllOnlineMessage.GetList(" MemCard='" + MemCard + "' and IsShow=0  and MessageType=1").Tables[0];
				if (dt.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dt, "MessageID,MessageContent,MemID,MemCard,MessageTime,ReplyContent,MessageType");
					bllOnlineMessage.UpdateShowStatus(MemCard);
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetNewMessageCount()
		{
			int count = 0;
			try
			{
				Chain.BLL.OnlineMessage bllProposal = new Chain.BLL.OnlineMessage();
				count = bllProposal.GetRecordCount("IsReply=0 and MessageType=0");
			}
			catch
			{
			}
			this.Context.Response.Write(count);
		}

		public void AddOrEditProductClass()
		{
			int flag = 0;
			try
			{
				Chain.BLL.ProductClass bllClass = new Chain.BLL.ProductClass();
				Chain.Model.ProductClass modelClass = new Chain.Model.ProductClass();
				string strClassName = this.Request["ClassName"].ToString();
				string strClassRemark = this.Request["ClassRemark"].ToString();
				int intClassID = (!string.IsNullOrEmpty(this.Request["ClassID"])) ? int.Parse(this.Request["ClassID"].ToString()) : 0;
				modelClass.ClassName = strClassName;
				modelClass.ClassRemark = strClassRemark;
				if (intClassID == 0)
				{
					flag = bllClass.Add(modelClass);
				}
				else
				{
					modelClass.ClassID = intClassID;
					if (bllClass.Update(modelClass))
					{
						flag = 1;
					}
				}
			}
			catch
			{
				flag = -1;
			}
			this.Response.Write(flag);
		}

		public void DelProductClass()
		{
			int flag = 0;
			try
			{
				Chain.BLL.ProductClass bllClass = new Chain.BLL.ProductClass();
				Chain.Model.ProductClass modelClass = new Chain.Model.ProductClass();
				int intClass = (!string.IsNullOrEmpty(this.Request["classID"])) ? int.Parse(this.Request["classID"].ToString()) : 0;
				if (intClass != 0)
				{
					DataTable dtClass = new Chain.BLL.ProductCenter().GetList(" ClassID=" + intClass).Tables[0];
					if (dtClass.Rows.Count > 0)
					{
						flag = -1;
					}
					else if (bllClass.Delete(intClass))
					{
						flag = 1;
					}
					else
					{
						flag = -2;
					}
				}
			}
			catch
			{
				flag = -2;
			}
			this.Response.Write(flag);
		}

		public void Wx_MemLogin()
		{
			int flag = -1;
			Chain.BLL.Mem WXMem = new Chain.BLL.Mem();
			try
			{
				string memcard = this.Request["memcard"].ToString();
				string passwords = this.Request["pwd"].ToString();
				string password = DESEncrypt.Encrypt(passwords);
				using (DataTable dt = WXMem.WeiXinLogin(memcard, password).Tables[0])
				{
					if (dt.Rows.Count > 0)
					{
						string MemState = dt.Rows[0]["MemState"].ToString();
						DateTime stime = DateTime.Now;
						DateTime s_t = stime;
						DateTime.TryParse(dt.Rows[0]["MemPastTime"].ToString(), out stime);
						if (MemState != "0")
						{
							flag = 2;
						}
						else if (s_t < stime)
						{
							this.Session["MemID"] = dt.Rows[0]["MemID"].ToString();
							flag = 1;
						}
						else
						{
							flag = 3;
						}
					}
				}
			}
			catch
			{
				flag = -1;
			}
			this.Response.Write(flag);
		}

		public void Wx_MemLoginOut()
		{
			int flag = -1;
			Chain.BLL.Mem WXMem = new Chain.BLL.Mem();
			try
			{
				this.Session["MemID"] = null;
				flag = 1;
			}
			catch
			{
				flag = -1;
			}
			this.Response.Write(flag);
		}

		public void WX_SendSMSCode()
		{
			int flag = 0;
			Chain.BLL.Mem WXMem = new Chain.BLL.Mem();
			try
			{
				string mobile = this.Request["mobile"].ToString();
				int code = this.GetMobileCode();
				string strSendContent = "您的短信验证码为：" + code + "，1分钟内有效，请尽快完成操作。";
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
				modelShop = bllShop.GetModel(1);
         
				if (modelShop.ShopSmsName != "")
				{
                    //TODO 1
					strSendContent = strSendContent + "【" + modelShop.ShopSmsName + "】";
                    if (SMSInfo.Send_SMS(false, mobile, strSendContent, ""))
                    {
                        flag = code;
                    }
                   
				}
				
			}
			catch (Exception ex)
			{
				flag = 0;
			}
			this.Response.Write(flag);
		}

		private int GetMobileCode()
		{
			Random rad = new Random();
			return rad.Next(100000, 1000000);
		}

		public void IsExitRegistMobile()
		{
			int MemID = -1;
			try
			{
				string mobile = this.Request["mobile"].ToString();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				MemID = bllMem.GetMemIDByWhere(" MemMobile='" + mobile + "'");
			}
			catch
			{
				MemID = -1;
			}
			this.Context.Response.Write(MemID);
		}

		public void Wx_UpdateMemPwd()
		{
			int flag = 0;
			try
			{
				int memid = int.Parse(this.Request["memid"].ToString());
				string pwd = this.Request["pwd"].ToString();
				pwd = DESEncrypt.Encrypt(pwd);
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				flag = bllMem.UpdateMemPwd(memid, pwd);
			}
			catch
			{
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void Wx_MemRegister()
		{
			int flag = 0;
			Chain.BLL.Mem WXMem = new Chain.BLL.Mem();
			try
			{
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				string memname = this.Request["memname"].ToString();
				string mobile = this.Request["mobile"].ToString();
				string pwd = this.Request["pwd"].ToString();
				string password = DESEncrypt.Encrypt(pwd);
                int shopid = int.Parse(this.Request["shopid"].ToString()); //bllShop.GetShopIDByWhere(" IsMain=1");
				Chain.BLL.SysShopBuyCard bllBuyCard = new Chain.BLL.SysShopBuyCard();
				DataTable dtCard = bllBuyCard.GetList("BuyCardShopID=" + shopid + " and BuyType=1").Tables[0];
				string MemCard = "0";
				for (int i = 0; i < dtCard.Rows.Count; i++)
				{
					string startNum = dtCard.Rows[i]["StartCardNumber"].ToString();
					string endNum = dtCard.Rows[i]["EndCardNumber"].ToString();
					MemCard = bllBuyCard.GetCarNum(startNum, endNum);
					if (MemCard != "0")
					{
						break;
					}
				}
				if (MemCard == "0")
				{
					bllBuyCard.Add(new Chain.Model.SysShopBuyCard
					{
						StartCardNumber = "999990000",
						EndCardNumber = "999999999",
						BuyCardMoney = 0m,
						BuyCardTime = DateTime.Now,
						Remark = "系统自动生成购卡记录",
						UserID = 1,
						BuyCardShopid = shopid,
						BuyType = 1
					});
					MemCard = "999990000";
				}
				flag = WXMem.WeiXinRegister(MemCard, memname, password, mobile, "0", "", shopid, "");
			}
			catch
			{
				flag = 0;
			}
			this.Response.Write(flag);
		}

		public void UpdateMemWeiXinCard()
		{
			int flag = 0;
			try
			{
				int memid = int.Parse(this.Request["memid"].ToString());
				string openid = this.Request["openid"].ToString();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				flag = bllMem.UpdateMemWeiXinCard(memid, openid);
			}
			catch
			{
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void Wx_GiftExchange()
		{
			string flag = "0";
			int memID = int.Parse(this.Request["memid"].ToString());
			string memname = this.Request["memname"].ToString();
			string mobile = this.Request["mobile"].ToString();
			string address = this.Request["address"].ToString();
			int sumPoint = int.Parse(this.Request["sumPoint"].ToString());
			int sumNumber = int.Parse(this.Request["sumNumber"].ToString());
			int giftcount = int.Parse(this.Request["giftcount"].ToString());
			int addressID = int.Parse(this.Request["addressID"].ToString());
			string strAccount = this.Request["orderAccount"].ToString();
			try
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				Chain.Model.GiftExchange modelGiftExchange = new Chain.Model.GiftExchange();
				Chain.BLL.GiftExchange bllGiftExchange = new Chain.BLL.GiftExchange();
				Chain.Model.GiftExchangeDetail modelDetail = new Chain.Model.GiftExchangeDetail();
				Chain.BLL.GiftExchangeDetail bllDetail = new Chain.BLL.GiftExchangeDetail();
				int levelID = modelMem.MemLevelID;
				if (modelMem.MemState != 0)
				{
					flag = "-1";
				}
				else
				{
					bool isNoStock = false;
					for (int i = 0; i < giftcount; i++)
					{
						int giftid = int.Parse(this.Request["GiftList[" + i + "][id]"].ToString());
						Chain.BLL.PointGift bllPointGift = new Chain.BLL.PointGift();
						if (bllPointGift.GetStockNumber(giftid) <= 0)
						{
							isNoStock = true;
							flag = "-2";
							break;
						}
					}
					if (!isNoStock)
					{
						modelGiftExchange.MemID = memID;
						modelGiftExchange.ExchangeTelePhone = mobile;
						modelGiftExchange.ExchangeAddress = address;
						modelGiftExchange.ExchangeAccount = strAccount;
						modelGiftExchange.ExchangeAllNumber = sumNumber;
						modelGiftExchange.ExchangeAllPoint = sumPoint;
						modelGiftExchange.ApplicationTime = DateTime.Now;
						modelGiftExchange.ExchangeStatus = 1;
						modelGiftExchange.ExchangeTime = DateTime.Now;
						modelGiftExchange.ExchangeType = 3;
						modelGiftExchange.AddressID = addressID;
						modelGiftExchange.MemName = memname;
						int intSuccess = bllGiftExchange.Add(modelGiftExchange);
						flag = intSuccess.ToString();
						if (intSuccess > 0)
						{
							for (int i = 0; i < giftcount; i++)
							{
								modelDetail.ExchangeID = intSuccess;
								modelDetail.ExchangeGiftID = int.Parse(this.Request["GiftList[" + i + "][id]"].ToString());
								modelDetail.ExchangeNumber = int.Parse(this.Request["GiftList[" + i + "][count]"].ToString());
								modelDetail.Giftname = this.Request["GiftList[" + i + "][name]"].ToString();
								int point = int.Parse(this.Request["GiftList[" + i + "][point]"].ToString());
								modelDetail.ExchangePoint = point * modelDetail.ExchangeNumber;
								bllDetail.Add(modelDetail);
							}
							flag = "1";
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "0";
			}
			this.Context.Response.Write(flag);
		}

		public void AddMemAddress()
		{
			int flag = 0;
			try
			{
				Chain.Model.MemAddress modelMemAddress = new Chain.Model.MemAddress();
				Chain.BLL.MemAddress bllMemAddress = new Chain.BLL.MemAddress();
				int memid = int.Parse(this.Request["memid"]);
				string mobile = this.Request["mobile"];
				string memname = this.Request["memname"];
				string province = this.Request["province"];
				string city = this.Request["city"];
				string county = this.Request["county"];
				string village = this.Request["village"];
				string address = this.Request["address"];
				int isDefault = int.Parse(this.Request["isDefault"].ToString());
				modelMemAddress.MemDetailAddress = address;
				modelMemAddress.MemID = memid;
				modelMemAddress.MemMobile = mobile;
				modelMemAddress.MemName = memname;
				modelMemAddress.MemProvince = province;
				modelMemAddress.MemCity = city;
				modelMemAddress.MemCounty = county;
				modelMemAddress.MemVillage = village;
				modelMemAddress.IsDefault = isDefault;
				int count = bllMemAddress.GetRecordCount("MemID=" + memid);
				if (count == 0)
				{
					modelMemAddress.IsDefault = 1;
				}
				if (isDefault == 1)
				{
					bllMemAddress.UpdateDefaultAddressByMemID(memid, 0);
				}
				flag = bllMemAddress.Add(modelMemAddress);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelMemAddress()
		{
			int flag = 0;
			try
			{
				int id = int.Parse(this.Request["id"].ToString());
				flag = (new Chain.BLL.MemAddress().Delete(id) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditMemAddress()
		{
			int flag = 0;
			try
			{
				int memid = int.Parse(this.Request["memid"].ToString());
				string mobile = this.Request["mobile"];
				string memname = this.Request["memname"];
				string province = this.Request["province"];
				string city = this.Request["city"];
				string county = this.Request["county"];
				string village = this.Request["village"];
				string address = this.Request["address"];
				int id = int.Parse(this.Request["id"].ToString());
				Chain.BLL.MemAddress bllMemAddress = new Chain.BLL.MemAddress();
				Chain.Model.MemAddress modelMemAddress = bllMemAddress.GetModel(id);
				int isDefault = int.Parse(this.Request["isDefault"].ToString());
				modelMemAddress.MemDetailAddress = address;
				modelMemAddress.MemID = memid;
				modelMemAddress.MemMobile = mobile;
				modelMemAddress.MemName = memname;
				modelMemAddress.MemProvince = province;
				modelMemAddress.MemCity = city;
				modelMemAddress.MemCounty = county;
				modelMemAddress.MemVillage = village;
				modelMemAddress.ID = id;
				modelMemAddress.IsDefault = isDefault;
				if (isDefault == 1)
				{
					bllMemAddress.UpdateDefaultAddressByMemID(memid, 0);
				}
				flag = (new Chain.BLL.MemAddress().Update(modelMemAddress) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void SetMemDefaultAddress()
		{
			int flag = 0;
			try
			{
				int id = int.Parse(this.Request["id"].ToString());
				int memid = int.Parse(this.Request["memid"].ToString());
				Chain.BLL.MemAddress bllMemAddress = new Chain.BLL.MemAddress();
				bllMemAddress.UpdateDefaultAddressByMemID(memid, 0);
				flag = bllMemAddress.UpdateDefaultAddressByID(id, memid, 1);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void Wx_UpdateMemInfo()
		{
			int flag = 0;
			try
			{
				string imageShow = this.Request["imgShow"];
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				int memid = int.Parse(this.Request["memid"]);
				Chain.Model.Mem modelMem = bllMem.GetModel(memid);
				string mobile = this.Request["mobile"];
				string memname = this.Request["memname"];
				string province = this.Request["province"];
				string city = this.Request["city"];
				string county = this.Request["county"];
				string village = this.Request["village"];
				string address = this.Request["address"];
				string identityCard = this.Request["identityCard"];
				bool sex = this.Request["sex"] == "1";
				string birthday = this.Request["birthday"];
				string email = this.Request["email"];
				modelMem.MemAddress = address;
				modelMem.MemID = memid;
				modelMem.MemMobile = mobile;
				modelMem.MemName = memname;
				modelMem.MemProvince = province;
				modelMem.MemCity = city;
				modelMem.MemCounty = county;
				modelMem.MemVillage = village;
				modelMem.MemIdentityCard = identityCard;
				modelMem.MemEmail = email;
				modelMem.MemSex = sex;
				modelMem.MemPhoto = null;
				modelMem.MemBirthday = DateTime.Parse(birthday);
				modelMem.MemPhoto = imageShow;
				flag = bllMem.Update(modelMem);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		private void SaveImg(HttpPostedFile sourse, string savePath, string saveName)
		{
			try
			{
				sourse.SaveAs(this.Server.MapPath(savePath) + saveName);
			}
			catch (Exception e)
			{
				Chain.Model.SysError err = new Chain.Model.SysError();
				err.UserID = 0;
				err.ShopID = 0;
				err.ErrorTime = DateTime.Now;
				err.ErrorContent = e.ToString();
				err.Ipaddress = PubFunction.ipAdress;
				new Chain.BLL.SysError().Add(err);
				throw e;
			}
		}

		public void Wx_GetMemCoupon()
		{
			int flag = 0;
			try
			{
				int memid = int.Parse(this.Request["memid"]);
				int CouPonID = int.Parse(this.Request["CouPonID"]);
				Chain.BLL.CouponList bllCouponList = new Chain.BLL.CouponList();
				int cid = bllCouponList.GetMemCouponID(CouPonID);
				if (cid != 0)
				{
					if (bllCouponList.SendCoupon(memid, cid))
					{
						Chain.BLL.Coupon bllCoupon = new Chain.BLL.Coupon();
						Chain.Model.Coupon modelCoupon = bllCoupon.GetModel(CouPonID);
						modelCoupon.CouponYF++;
						bllCoupon.Update(modelCoupon);
						flag = 1;
					}
				}
				else
				{
					flag = -1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}


        public void Wx_getSHop()
        {
            int flag = 0;
            try
            {
                //TODO 11

                Chain.BLL.SysShop Shop = new Chain.BLL.SysShop();
                string strSql = " ShopID>0 and ShopType=3 ";
                DataTable db = Shop.GetList(strSql).Tables[0];

                this.Context.Response.Write(JsonPlus.ToJson(db, ""));
                return;

            }
            catch 
            {
                flag = 0;
            }

            this.Context.Response.Write(flag.ToString());
        }

		public void Membersrecharge()
		{
			try
			{
				int memid = int.Parse(this.Request["memid"]);
				int jf = int.Parse(decimal.Parse(this.Request["point"]).ToString("#0"));
				string openid = this.Request["openid"];
				string timestamp = this.Request["timestamps"];
				Chain.Model.SysParameter parameter = new Chain.BLL.SysParameter().GetModel(1);
				string systemDomain = parameter.SystemDomain;
				string appId = parameter.WeiXinAppID;
				decimal je = decimal.Parse(this.Request["money"]);
				decimal giveMoney = decimal.Parse(this.Request["givemoney"]);
				string total_fee = (je * 100m).ToString("#0");
				string nonceStr = this.Request["nonceStrs"];
				string signType = "MD5";
				string mchid = parameter.MchId;
				string key = parameter.Api;
				string spbill_create_ip = this.Request.UserHostAddress;
				string body = "会员通过微信支付充值";
				DateTime now = DateTime.Parse(this.Request["now"]);
				string out_trade_no = parameter.MemRechargePrefix + now.ToString("yyMMddHHmmssffff") + memid;
				string time_expire = now.AddMinutes(10.0).ToString("yyyyMMddHHmmss");
				string time_start = now.ToString("yyyyMMddHHmmss");
				string trade_type = "JSAPI";
				string attach = string.Format("{0},{1},{2},{3},{4}", new object[]
				{
					"Membersrecharge",
					memid,
					total_fee,
					jf,
					giveMoney
				});
				string notify_url = string.Format("http://{0}/mobile/member/ResultNotifyPage.aspx", systemDomain);
				Sign sign = new Sign();
				string unitesign = sign.UniteSign(appId, attach, body, mchid, nonceStr, notify_url, openid, out_trade_no, spbill_create_ip, time_expire, time_start, total_fee, trade_type, key);
				if (unitesign == "")
				{
					this.Context.Response.Write(this.membersrecharge("", ""));
				}
				else
				{
					Pay pay = new Pay();
					string prepay_id = string.Format("prepay_id={0}", pay.UnitePay(appId, attach, body, mchid, nonceStr, notify_url, openid, out_trade_no, spbill_create_ip, time_expire, time_start, total_fee, trade_type, unitesign));
					if (prepay_id.Equals("prepay_id="))
					{
						this.Context.Response.Write(this.membersrecharge("", ""));
					}
					else
					{
						string paySign = sign.ChooseWXPaySign(appId, nonceStr, prepay_id, signType, timestamp, key);
						if (paySign == "")
						{
							this.Context.Response.Write(this.membersrecharge("", ""));
						}
						else
						{
							this.Context.Response.Write(this.membersrecharge(prepay_id, paySign));
						}
					}
				}
			}
			catch
			{
				this.Context.Response.Write(this.membersrecharge("", ""));
			}
		}

		private string membersrecharge(string prepay_id = "", string paySign = "")
		{
			string msgResponse;
			if (prepay_id == "" && paySign == "")
			{
				msgResponse = "{\"prepay_id\":\"\",\"paySign\":\"\"}";
			}
			else
			{
				msgResponse = string.Concat(new string[]
				{
					"{\"prepay_id\":\"",
					prepay_id,
					"\",\"paySign\":\"",
					paySign,
					"\"}"
				});
			}
			return msgResponse;
		}

		public void AddRechargeRule()
		{
			int flag = 0;
			try
			{
				decimal RechargeMoney = decimal.Parse(this.Request["RechargeMoney"]);
				decimal GiveMoney = decimal.Parse(this.Request["GiveMoney"]);
				string RuleDesc = this.Request["RuleDesc"].ToString();
				Chain.BLL.RechargeRule bllRule = new Chain.BLL.RechargeRule();
				flag = bllRule.Add(new Chain.Model.RechargeRule
				{
					RechargeMoney = RechargeMoney,
					GiveMoney = GiveMoney,
					CreateTime = DateTime.Now,
					CreateUserID = this.UserModel.UserID,
					RuleDesc = RuleDesc
				});
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void DelRechargeRule()
		{
			int flag = 0;
			try
			{
				int RuleID = int.Parse(this.Request["RuleID"]);
				flag = (new Chain.BLL.RechargeRule().Delete(RuleID) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void EditRechargeRule()
		{
			int flag = 0;
			try
			{
				decimal RechargeMoney = decimal.Parse(this.Request["RechargeMoney"]);
				decimal GiveMoney = decimal.Parse(this.Request["GiveMoney"]);
				int RuleID = int.Parse(this.Request["RuleID"]);
				string RuleDesc = this.Request["RuleDesc"].ToString();
				Chain.BLL.RechargeRule bllRule = new Chain.BLL.RechargeRule();
				Chain.Model.RechargeRule modelRule = bllRule.GetModel(RuleID);
				modelRule.RechargeMoney = RechargeMoney;
				modelRule.GiveMoney = GiveMoney;
				modelRule.RuleDesc = RuleDesc;
				flag = (bllRule.Update(modelRule) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void GetRechargeRule()
		{
			string msgResponse = "";
			try
			{
				int RuleID = int.Parse(this.Request["RuleID"]);
				DataTable dtRule = new Chain.BLL.RechargeRule().GetList(" RuleID=" + RuleID).Tables[0];
				if (dtRule != null)
				{
					msgResponse = JsonPlus.ToJson(dtRule, "");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetGiveMoney()
		{
			decimal givemoney = 0m;
			try
			{
				decimal RechargeMoney = decimal.Parse(this.Request["RechargeMoney"]);
				DataTable dtRule = new Chain.BLL.RechargeRule().GetList(" RechargeMoney<=" + RechargeMoney).Tables[0];
				if (dtRule.Rows.Count >= 0)
				{
					givemoney = decimal.Parse(dtRule.Rows[0]["GiveMoney"].ToString());
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(givemoney);
		}

		public void GetMemName()
		{
			string memcard = this.Request["memcard"];
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			DataTable dtMem = bllMem.GetList(string.Concat(new string[]
			{
				" MemCard='",
				memcard,
				"' or MemMobile='",
				memcard,
				"'"
			})).Tables[0];
			string msgResponse;
			if (dtMem.Rows.Count > 0)
			{
				string memname = dtMem.Rows[0]["MemName"].ToString();
				int memid = int.Parse(dtMem.Rows[0]["MemID"].ToString());
				msgResponse = string.Concat(new object[]
				{
					"{\"memid\":\"",
					memid,
					"\",\"memname\":\"",
					memname,
					"\"}"
				});
			}
			else
			{
				msgResponse = "{\"memid\":\"" + 0 + "\",\"memname\":\"\"}";
			}
			this.Context.Response.Write(msgResponse);
		}

		public void Wx_GiveMemMoney()
		{
			int flag = 0;
			try
			{
				decimal money = Convert.ToDecimal(this.Request["money"]);
				decimal totalMoney = Convert.ToDecimal(this.Request["totalMoney"]);
				decimal elseMoney = Convert.ToDecimal(this.Request["elseMoney"]);
				int TransferToMemID = int.Parse(this.Request["giveMemID"]);
				int transferFromMemID = int.Parse(this.Request["memid"]);
				string transferRemark = this.Request["remark"];
				string account = "ZZ" + DateTime.Now.ToString("yyMMddHHmmssffff");
				Chain.BLL.Mem MemBll = new Chain.BLL.Mem();
				Chain.Model.Mem transferFromMemModel = MemBll.GetModel(transferFromMemID);
				Chain.Model.Mem transferToMemModel = MemBll.GetModel(TransferToMemID);
				Chain.Model.MemTransferLog modelMemTransferLog = new Chain.Model.MemTransferLog();
				Chain.BLL.MemTransferLog bllMemTransferLog = new Chain.BLL.MemTransferLog();
				modelMemTransferLog.TransferAccount = account;
				modelMemTransferLog.TransferCreateTime = DateTime.Now;
				modelMemTransferLog.TransferFromMemID = transferFromMemID;
				modelMemTransferLog.TransferToMemID = transferToMemModel.MemID;
				modelMemTransferLog.TransferRemark = transferRemark;
				modelMemTransferLog.UserID = 1;
				modelMemTransferLog.TransferMoney = money;
				modelMemTransferLog.ElseMoney = elseMoney;
				modelMemTransferLog.TotalMoney = totalMoney;
				flag = bllMemTransferLog.Add(modelMemTransferLog);
				MemBll.UpdateMoney(transferFromMemModel.MemID, -totalMoney);
				MemBll.UpdateMoney(transferToMemModel.MemID, money);
				Chain.BLL.MoneyChangeLog bllMoneyChangeLog = new Chain.BLL.MoneyChangeLog();
				Chain.Model.MoneyChangeLog modelMoneyChangeLog = new Chain.Model.MoneyChangeLog();
				modelMoneyChangeLog.MoneyChangeMemID = transferFromMemModel.MemID;
				modelMoneyChangeLog.MoneyChangeUserID = 1;
				modelMoneyChangeLog.MoneyChangeType = 16;
				modelMoneyChangeLog.MoneyChangeAccount = account;
				modelMoneyChangeLog.MoneyChangeMoney = totalMoney;
				modelMoneyChangeLog.MoneyChangeCash = 0m;
				modelMoneyChangeLog.MoneyChangeBalance = -totalMoney;
				modelMoneyChangeLog.MoneyChangeUnionPay = 0m;
				modelMoneyChangeLog.MemMoney = transferFromMemModel.MemMoney - totalMoney;
				modelMoneyChangeLog.MoneyChangeGiveMoney = 0m;
				modelMoneyChangeLog.MoneyChangeCreateTime = DateTime.Now;
				bllMoneyChangeLog.Add(modelMoneyChangeLog);
				modelMoneyChangeLog.MoneyChangeMemID = transferToMemModel.MemID;
				modelMoneyChangeLog.MoneyChangeUserID = 1;
				modelMoneyChangeLog.MoneyChangeType = 16;
				modelMoneyChangeLog.MoneyChangeAccount = account;
				modelMoneyChangeLog.MoneyChangeMoney = money;
				modelMoneyChangeLog.MoneyChangeCash = 0m;
				modelMoneyChangeLog.MoneyChangeBalance = money;
				modelMoneyChangeLog.MoneyChangeUnionPay = 0m;
				modelMoneyChangeLog.MemMoney = transferToMemModel.MemMoney + money;
				modelMoneyChangeLog.MoneyChangeGiveMoney = 0m;
				modelMoneyChangeLog.MoneyChangeCreateTime = DateTime.Now;
				bllMoneyChangeLog.Add(modelMoneyChangeLog);
			}
			catch (Exception e_29B)
			{
				flag = 0;
				throw;
			}
			this.Context.Response.Write(flag);
		}

		private int GetPrizeCode(int RotateID)
		{
			Random rad = new Random();
			int value = rad.Next(100000, 1000000);
			if (new Chain.BLL.SysRotatePrizeLog().ExistsPrizeCode(RotateID, value.ToString()))
			{
				value = this.GetPrizeCode(RotateID);
			}
			return value;
		}

		public void SysRotatePrizeLogAdd()
		{
			string flag = "0";
			try
			{
				Chain.Model.SysRotatePrizeLog modelBill = new Chain.Model.SysRotatePrizeLog();
				int intUserID = 1;
				int intUserShopID = 1;
				modelBill.PrizeLevel = this.Request["PrizeLevel"].ToString();
				modelBill.PrizeAccount = "ZJ" + DateTime.Now.ToString("yyMMddHHmmssffff");
				modelBill.RotateID = int.Parse(this.Request["RotateID"].ToString());
				Chain.BLL.SysRotatePrizeLog bllBill = new Chain.BLL.SysRotatePrizeLog();
				Chain.BLL.SysRotate blSysRotate = new Chain.BLL.SysRotate();
				modelBill.MemID = int.Parse(this.Request["MemID"].ToString());
				int wincount = bllBill.GetRecordCount(string.Concat(new object[]
				{
					" PrizeLevel<>'未中奖' and RotateID=",
					modelBill.RotateID,
					" and MemID=",
					modelBill.MemID
				}));
				Chain.Model.Mem modelMem = this.bllMem.GetModel(modelBill.MemID);
				Chain.Model.SysRotate modelRotate = blSysRotate.GetModel(modelBill.RotateID);
				string prizename = "未中奖";
				if (modelRotate.IsWinOne == 1 && wincount >= 1)
				{
					modelBill.PrizeLevel = "未中奖";
				}
				string prizeLevel = modelBill.PrizeLevel;
				if (prizeLevel != null)
				{
					if (!(prizeLevel == "一等奖"))
					{
						if (!(prizeLevel == "二等奖"))
						{
							if (!(prizeLevel == "三等奖"))
							{
								if (!(prizeLevel == "四等奖"))
								{
									if (!(prizeLevel == "五等奖"))
									{
										if (prizeLevel == "六等奖")
										{
											if (modelRotate.SixPrizeWinCount >= modelRotate.SixPrizeCount)
											{
												modelBill.PrizeLevel = "未中奖";
											}
										}
									}
									else if (modelRotate.FivePrizeWinCount >= modelRotate.FivePrizeCount)
									{
										modelBill.PrizeLevel = "未中奖";
									}
								}
								else if (modelRotate.FourPrizeWinCount >= modelRotate.FourPrizeCount)
								{
									modelBill.PrizeLevel = "未中奖";
								}
							}
							else if (modelRotate.ThreePrizeWinCount >= modelRotate.ThreePrizeCount)
							{
								modelBill.PrizeLevel = "未中奖";
							}
						}
						else if (modelRotate.TwoPrizeWinCount >= modelRotate.TwoPrizeCount)
						{
							modelBill.PrizeLevel = "未中奖";
						}
					}
					else if (modelRotate.OnePrizeWinCount >= modelRotate.OnePrizeCount)
					{
						modelBill.PrizeLevel = "未中奖";
					}
				}
				string prizecode = this.GetPrizeCode(modelBill.RotateID).ToString();
				if (modelBill.PrizeLevel == "未中奖")
				{
					modelBill.PrizeCode = "";
				}
				else
				{
					modelBill.PrizeCode = prizecode;
				}
				modelBill.CreateTime = DateTime.Now;
				modelBill.PrizeStatus = 0;
				int intPrizeLog = bllBill.Add(modelBill);
				if (intPrizeLog > 0)
				{
					Chain.Model.SysRotate modelSysRotate = blSysRotate.GetModel(modelBill.RotateID);
					prizeLevel = modelBill.PrizeLevel;
					if (prizeLevel != null)
					{
						if (!(prizeLevel == "一等奖"))
						{
							if (!(prizeLevel == "二等奖"))
							{
								if (!(prizeLevel == "三等奖"))
								{
									if (!(prizeLevel == "四等奖"))
									{
										if (!(prizeLevel == "五等奖"))
										{
											if (prizeLevel == "六等奖")
											{
												prizename = modelSysRotate.SixName;
												string winname = modelSysRotate.SixPrizeName;
												modelSysRotate.SixPrizeWinCount++;
											}
										}
										else
										{
											prizename = modelSysRotate.FiveName;
											string winname = modelSysRotate.FivePrizeName;
											modelSysRotate.FivePrizeWinCount++;
										}
									}
									else
									{
										prizename = modelSysRotate.FourName;
										string winname = modelSysRotate.FourPrizeName;
										modelSysRotate.FourPrizeWinCount++;
									}
								}
								else
								{
									prizename = modelSysRotate.ThreeName;
									string winname = modelSysRotate.ThreePrizeName;
									modelSysRotate.ThreePrizeWinCount++;
								}
							}
							else
							{
								prizename = modelSysRotate.TwoName;
								string winname = modelSysRotate.TwoPrizeName;
								modelSysRotate.TwoPrizeWinCount++;
							}
						}
						else
						{
							prizename = modelSysRotate.OneName;
							string winname = modelSysRotate.OnePrizeName;
							modelSysRotate.OnePrizeWinCount++;
						}
					}
					if (modelBill.PrizeLevel != "未中奖")
					{
						blSysRotate.UpdateWinCount(modelSysRotate);
					}
					flag = string.Concat(new string[]
					{
						"{\"prizecode\":\"",
						prizecode,
						"\",\"prizename\":\"",
						prizename,
						"\"}"
					});
					PubFunction.SaveSysLog(intUserID, 1, "抽奖记录新增", "新增抽奖记录", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-1";
			}
			this.Context.Response.Write(flag);
		}

		public void SysRotatePrizeLogUpdate()
		{
			int flag = 0;
			try
			{
				Chain.BLL.SysRotatePrizeLog bllSysRotatePrizeLog = new Chain.BLL.SysRotatePrizeLog();
				int PrizeLogID = int.Parse(this.Request["PrizeLogID"]);
				Chain.Model.SysRotatePrizeLog modelSysRotatePrizeLog = bllSysRotatePrizeLog.GetModel(PrizeLogID);
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelSysRotatePrizeLog.GiveUserID = intUserID;
				modelSysRotatePrizeLog.GiveTime = DateTime.Now;
				modelSysRotatePrizeLog.PrizeStatus = 1;
				Chain.BLL.SysRotate blSysRotate = new Chain.BLL.SysRotate();
				flag = bllSysRotatePrizeLog.Update(modelSysRotatePrizeLog);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 1, "发放奖品", "发放奖品", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void SysRotateAdd()
		{
			int flag = 0;
			try
			{
				Chain.BLL.SysRotate blSysRotate = new Chain.BLL.SysRotate();
				Chain.Model.SysRotate modelSysRotate = new Chain.Model.SysRotate();
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelSysRotate.RotateName = this.Request["RotateName"].ToString();
				modelSysRotate.RotateRemark = this.Request["RotateRemark"].ToString();
				modelSysRotate.StartTime = DateTime.Parse(this.Request["StartTime"].ToString());
				modelSysRotate.EndTime = DateTime.Parse(this.Request["EndTime"].ToString());
				DataTable dt = blSysRotate.GetList(string.Concat(new string[]
				{
					" (StartTime>='",
					modelSysRotate.StartTime.ToShortDateString(),
					"' and StartTime<='",
					modelSysRotate.EndTime.ToShortDateString(),
					"') or (EndTime>='",
					modelSysRotate.StartTime.ToShortDateString(),
					"' and EndTime<='",
					modelSysRotate.EndTime.ToShortDateString(),
					"')"
				})).Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = -2;
				}
				else
				{
					modelSysRotate.RotateCount = 0;
					modelSysRotate.PersonTotalCount = 0;
					modelSysRotate.PersonDayCount = 0;
					modelSysRotate.OnePrizeName = this.Request["OnePrizeName"].ToString();
					modelSysRotate.OnePrizeCount = int.Parse(this.Request["OnePrizeCount"].ToString());
					modelSysRotate.TwoPrizeName = this.Request["TwoPrizeName"].ToString();
					modelSysRotate.TwoPrizeCount = int.Parse(this.Request["TwoPrizeCount"].ToString());
					modelSysRotate.ThreePrizeName = this.Request["ThreePrizeName"].ToString();
					modelSysRotate.ThreePrizeCount = int.Parse(this.Request["ThreePrizeCount"].ToString());
					modelSysRotate.FourPrizeName = this.Request["FourPrizeName"].ToString();
					modelSysRotate.FourPrizeCount = int.Parse(this.Request["FourPrizeCount"].ToString());
					modelSysRotate.FivePrizeName = this.Request["FivePrizeName"].ToString();
					modelSysRotate.FivePrizeCount = int.Parse(this.Request["FivePrizeCount"].ToString());
					modelSysRotate.SixPrizeName = this.Request["SixPrizeName"].ToString();
					modelSysRotate.SixPrizeCount = int.Parse(this.Request["SixPrizeCount"].ToString());
					modelSysRotate.OneRate = decimal.Parse(this.Request["OneRate"].ToString());
					modelSysRotate.TwoRate = decimal.Parse(this.Request["TwoRate"].ToString());
					modelSysRotate.ThreeRate = decimal.Parse(this.Request["ThreeRate"].ToString());
					modelSysRotate.FourRate = decimal.Parse(this.Request["FourRate"].ToString());
					modelSysRotate.FiveRate = decimal.Parse(this.Request["FiveRate"].ToString());
					modelSysRotate.SixRate = decimal.Parse(this.Request["SixRate"].ToString());
					modelSysRotate.RotateRegion = this.Request["RotateRegion"].ToString();
					modelSysRotate.CreateTime = DateTime.Now;
					modelSysRotate.CreateUserID = intUserID;
					modelSysRotate.OnePrizeWinCount = 0;
					modelSysRotate.TwoPrizeWinCount = 0;
					modelSysRotate.ThreePrizeWinCount = 0;
					modelSysRotate.FourPrizeWinCount = 0;
					modelSysRotate.FivePrizeWinCount = 0;
					modelSysRotate.SixPrizeWinCount = 0;
					flag = blSysRotate.Add(modelSysRotate);
					if (flag > 0)
					{
						PubFunction.SaveSysLog(intUserID, 1, "大转盘活动新增", "新增大转盘活动，大转盘活动名称：[" + modelSysRotate.RotateName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void SysRotateEdit()
		{
			int flag = 0;
			try
			{
				Chain.BLL.SysRotate blSysRotate = new Chain.BLL.SysRotate();
				Chain.Model.SysRotate modelSysRotate = new Chain.Model.SysRotate();
				int intRotateID = (this.Request["RotateID"].ToString() != "") ? int.Parse(this.Request["RotateID"].ToString()) : 0;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelSysRotate.RotateID = intRotateID;
				modelSysRotate = blSysRotate.GetModel(intRotateID);
				modelSysRotate.RotateName = this.Request["RotateName"].ToString();
				modelSysRotate.RotateRemark = this.Request["RotateRemark"].ToString();
				modelSysRotate.StartTime = DateTime.Parse(this.Request["StartTime"].ToString());
				modelSysRotate.EndTime = DateTime.Parse(this.Request["EndTime"].ToString());
				DataTable dt = blSysRotate.GetList(string.Concat(new object[]
				{
					"((StartTime>='",
					modelSysRotate.StartTime.ToShortDateString(),
					"' and StartTime<='",
					modelSysRotate.EndTime.ToShortDateString(),
					"') or (EndTime>='",
					modelSysRotate.StartTime.ToShortDateString(),
					"' and EndTime<='",
					modelSysRotate.EndTime.ToShortDateString(),
					"')) and RotateID<>",
					intRotateID
				})).Tables[0];
				if (dt.Rows.Count > 0)
				{
					flag = -2;
				}
				else
				{
					modelSysRotate.RotateCount = 0;
					modelSysRotate.PersonTotalCount = 0;
					modelSysRotate.PersonDayCount = 0;
					modelSysRotate.OnePrizeName = this.Request["OnePrizeName"].ToString();
					modelSysRotate.OnePrizeCount = int.Parse(this.Request["OnePrizeCount"].ToString());
					modelSysRotate.TwoPrizeName = this.Request["TwoPrizeName"].ToString();
					modelSysRotate.TwoPrizeCount = int.Parse(this.Request["TwoPrizeCount"].ToString());
					modelSysRotate.ThreePrizeName = this.Request["ThreePrizeName"].ToString();
					modelSysRotate.ThreePrizeCount = int.Parse(this.Request["ThreePrizeCount"].ToString());
					modelSysRotate.FourPrizeName = this.Request["FourPrizeName"].ToString();
					modelSysRotate.FourPrizeCount = int.Parse(this.Request["FourPrizeCount"].ToString());
					modelSysRotate.FivePrizeName = this.Request["FivePrizeName"].ToString();
					modelSysRotate.FivePrizeCount = int.Parse(this.Request["FivePrizeCount"].ToString());
					modelSysRotate.SixPrizeName = this.Request["SixPrizeName"].ToString();
					modelSysRotate.SixPrizeCount = int.Parse(this.Request["SixPrizeCount"].ToString());
					modelSysRotate.CreateTime = DateTime.Now;
					modelSysRotate.CreateUserID = intUserID;
					modelSysRotate.OneRate = decimal.Parse(this.Request["OneRate"].ToString());
					modelSysRotate.TwoRate = decimal.Parse(this.Request["TwoRate"].ToString());
					modelSysRotate.ThreeRate = decimal.Parse(this.Request["ThreeRate"].ToString());
					modelSysRotate.FourRate = decimal.Parse(this.Request["FourRate"].ToString());
					modelSysRotate.FiveRate = decimal.Parse(this.Request["FiveRate"].ToString());
					modelSysRotate.SixRate = decimal.Parse(this.Request["SixRate"].ToString());
					flag = blSysRotate.Update(modelSysRotate);
					if (flag > 0)
					{
						PubFunction.SaveSysLog(intUserID, 1, "大转盘活动编辑", "编辑大转盘活动，大转盘活动名称：[" + modelSysRotate.RotateName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void SysRotateDelete()
		{
			int flag = 0;
			try
			{
				int intRotateID = (this.Request["RotateID"].ToString() != "") ? int.Parse(this.Request["RotateID"].ToString()) : 0;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.BLL.SysRotate bllBill = new Chain.BLL.SysRotate();
				Chain.Model.SysRotate modelBill = bllBill.GetModel(intRotateID);
				flag = bllBill.Delete(intRotateID);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 2, "大转盘活动删除", "删除大转盘活动，大转盘活动名称：[" + modelBill.RotateName + "]", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -1;
			}
			this.Context.Response.Write(flag);
		}

		public void GetSysRotateInfo()
		{
			string msgResponse = "";
			try
			{
				int RotateID = (this.Request["RotateID"].ToString() != "") ? int.Parse(this.Request["RotateID"].ToString()) : 0;
				Chain.BLL.SysRotate bllSysRotate = new Chain.BLL.SysRotate();
				DataTable dtSysRotate = bllSysRotate.GetList(" RotateID=" + RotateID).Tables[0];
				if (dtSysRotate.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtSysRotate, "IsWinOne,OneMobile,TwoMobile,ThreeMobile,FourMobile,FiveMobile,SixMobile,ImageUrl,RotateRegion,OneRate,TwoRate,ThreeRate,FourRate,FiveRate,SixRate,CreateTime,CreateUserID,OnePrizeWinCount,TwoPrizeWinCount,ThreePrizeWinCount,FourPrizeWinCount,FivePrizeWinCount,SixPrizeWinCount,RotateID,RotateName,StartTime,EndTime,RotateRemark,RotateCount,PersonTotalCount,PersonDayCount,OnePrizeName,OnePrizeCount,TwoPrizeName,TwoPrizeCount,ThreePrizeName,ThreePrizeCount,FourPrizeName,FourPrizeCount,FivePrizeName,FivePrizeCount,SixPrizeName,SixPrizeCount");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void GetSysRotateCount()
		{
			string msgResponse = "";
			try
			{
				int ID = (this.Request["ID"].ToString() != "") ? int.Parse(this.Request["ID"].ToString()) : 0;
				Chain.BLL.SysRotateCount bllStaff = new Chain.BLL.SysRotateCount();
				DataTable dtStaff = bllStaff.GetList(" ID=" + ID).Tables[0];
				if (dtStaff.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtStaff, "ID,CostAmount,RotateCount,StartTime,EndTime");
				}
			}
			catch
			{
			}
			this.Context.Response.Write(msgResponse);
		}

		public void SysRotateCountAdd()
		{
			int flag = 0;
			try
			{
				Chain.Model.SysRotateCount modelClass = new Chain.Model.SysRotateCount();
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelClass.CostAmount = decimal.Parse(this.Request["CostAmount"].ToString());
				modelClass.RotateCount = int.Parse(this.Request["RotateCount"].ToString());
				modelClass.StartTime = DateTime.Parse(this.Request["StartTime"].ToString());
				modelClass.EndTime = DateTime.Parse(this.Request["EndTime"].ToString());
				modelClass.RotateID = int.Parse(this.Request["RotateID"].ToString());
				Chain.BLL.SysRotateCount bllClass = new Chain.BLL.SysRotateCount();
				flag = bllClass.Add(modelClass);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 3, "转盘活动规则新增", "新增转盘活动规则", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -2;
			}
			this.Context.Response.Write(flag);
		}

		public void SysRotateCountEdit()
		{
			int flag = 0;
			try
			{
				Chain.Model.SysRotateCount modelClass = new Chain.Model.SysRotateCount();
				int ID = (this.Request["ID"].ToString() != "") ? int.Parse(this.Request["ID"].ToString()) : 0;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				modelClass.CostAmount = decimal.Parse(this.Request["CostAmount"].ToString());
				modelClass.RotateCount = int.Parse(this.Request["RotateCount"].ToString());
				modelClass.StartTime = DateTime.Parse(this.Request["StartTime"].ToString());
				modelClass.EndTime = DateTime.Parse(this.Request["EndTime"].ToString());
				modelClass.RotateID = int.Parse(this.Request["RotateID"].ToString());
				modelClass.ID = ID;
				Chain.BLL.SysRotateCount bllClass = new Chain.BLL.SysRotateCount();
				flag = bllClass.Update(modelClass);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 3, "转盘活动规则编辑", "编辑转盘活动规则", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = -2;
			}
			this.Context.Response.Write(flag);
		}

		public void SysRotateCountDelete()
		{
			int flag = 0;
			try
			{
				int ID = (this.Request["ID"].ToString() != "") ? int.Parse(this.Request["ID"].ToString()) : 0;
				int intUserID = this.UserModel.UserID;
				int intUserShopID = this.UserModel.UserShopID;
				Chain.BLL.SysRotateCount bllClass = new Chain.BLL.SysRotateCount();
				flag = bllClass.Delete(ID);
				if (flag > 0)
				{
					PubFunction.SaveSysLog(intUserID, 3, "转盘活动规则删除", "删除转盘活动规则", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
			}
			catch
			{
				flag = 0;
			}
			this.Context.Response.Write(flag);
		}

		public void GetMoneyRegion()
		{
			string MoneyRegion = this.Request["MoneyRegion"].ToString();
			Chain.BLL.WeiXinMoney bllWeiXinMoney = new Chain.BLL.WeiXinMoney();
			string moneyID = bllWeiXinMoney.GetMoneyIDByMoneyRegion(MoneyRegion);
			if (moneyID != "")
			{
				Chain.Model.WeiXinMoney modelWeiXinMoney = bllWeiXinMoney.GetModel(int.Parse(moneyID));
				DateTime now = DateTime.Now;
				if (modelWeiXinMoney.StartTime > now)
				{
					this.Context.Response.Write("-1");
				}
				else if (modelWeiXinMoney.EndTime < now)
				{
					this.Context.Response.Write("-2");
				}
				else
				{
					this.Context.Response.Write(moneyID);
				}
			}
			else
			{
				this.Context.Response.Write("0");
			}
		}

		public void GetRotateRegion()
		{
			string RotateRegion = this.Request["RotateRegion"].ToString();
			Chain.BLL.SysRotate bllSysRotate = new Chain.BLL.SysRotate();
			string rotateID = bllSysRotate.GetRotateIDByRotateRegion(RotateRegion);
			if (rotateID != "")
			{
				Chain.Model.SysRotate modelSysRotate = bllSysRotate.GetModel(int.Parse(rotateID));
				DateTime now = DateTime.Now;
				if (modelSysRotate.StartTime > now)
				{
					this.Context.Response.Write("-1");
				}
				else if (modelSysRotate.EndTime < now)
				{
					this.Context.Response.Write("-2");
				}
				else
				{
					this.Context.Response.Write(rotateID);
				}
			}
			else
			{
				this.Context.Response.Write("0");
			}
		}

		public void GetMemRotateCount()
		{
			int resultcount = 0;
			int memtotalcount = 0;
			int RotateID = int.Parse(this.Request["RotateID"].ToString());
			int MemID = int.Parse(this.Request["MemID"].ToString());
			Chain.BLL.SysRotateCount bllSysRotateCount = new Chain.BLL.SysRotateCount();
			DataTable dt = bllSysRotateCount.GetList(" RotateID=" + RotateID).Tables[0];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string starttime = dt.Rows[i]["StartTime"].ToString();
				string endtime = dt.Rows[i]["EndTime"].ToString();
				int count = int.Parse(dt.Rows[i]["RotateCount"].ToString());
				decimal costamount = decimal.Parse(dt.Rows[i]["CostAmount"].ToString());
				decimal amount = bllSysRotateCount.GetMemCountCostAmount(starttime, endtime, MemID) + bllSysRotateCount.GetMemOrderLogCostAmount(starttime, endtime, MemID) + bllSysRotateCount.GetMemStorageTimingCostAmount(starttime, endtime, MemID);
				if (amount >= costamount)
				{
					memtotalcount = count;
					break;
				}
			}
			Chain.BLL.SysRotatePrizeLog bllSysRotatePrizeLog = new Chain.BLL.SysRotatePrizeLog();
			int hasWinCount = bllSysRotatePrizeLog.GetRecordCount(string.Concat(new object[]
			{
				"MemID=",
				MemID,
				" and RotateID=",
				RotateID
			}));
			if (memtotalcount != 0)
			{
				resultcount = memtotalcount - hasWinCount;
			}
			this.Context.Response.Write(resultcount);
		}

		public void GetWinCount()
		{
			int MemID = int.Parse(this.Request["MemID"].ToString());
			int RotateID = int.Parse(this.Request["RotateID"].ToString());
			Chain.BLL.SysRotatePrizeLog bllSysRotatePrizeLog = new Chain.BLL.SysRotatePrizeLog();
			int hasWinCount = bllSysRotatePrizeLog.GetRecordCount(string.Concat(new object[]
			{
				"MemID=",
				MemID,
				" and PrizeLevel<>'未中奖' and RotateID=",
				RotateID
			}));
			Chain.BLL.SysRotateCount bllSysRotateCount = new Chain.BLL.SysRotateCount();
			DataTable dt = bllSysRotateCount.GetList(" RotateID=" + RotateID).Tables[0];
			int count = 0;
			int totalcount = 0;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string starttime = DateTime.Parse(dt.Rows[i]["StartTime"].ToString()).ToString("yyyy-MM-dd");
				string endtime = DateTime.Parse(dt.Rows[i]["endTime"].ToString()).ToString("yyyy-MM-dd");
				count = int.Parse(dt.Rows[i]["RotateCount"].ToString());
				decimal costamount = decimal.Parse(dt.Rows[i]["CostAmount"].ToString());
				decimal amount = bllSysRotateCount.GetMemCountCostAmount(starttime, endtime, MemID);
				decimal amount2 = bllSysRotateCount.GetMemOrderLogCostAmount(starttime, endtime, MemID);
				decimal amount3 = bllSysRotateCount.GetMemStorageTimingCostAmount(starttime, endtime, MemID);
				decimal amount4 = amount + amount2 + amount3;
				if (amount4 >= costamount)
				{
					totalcount = count;
					break;
				}
			}
			int getcount = bllSysRotatePrizeLog.GetRecordCount(string.Concat(new object[]
			{
				"MemID=",
				MemID,
				"  and RotateID=",
				RotateID
			}));
			int noUserCount = totalcount - getcount;
			string flag = string.Concat(new object[]
			{
				"{\"hasWinCount\":\"",
				hasWinCount,
				"\",\"noUseCount\":\"",
				noUserCount,
				"\",\"count\":\"",
				count,
				"\",\"getcount\":\"",
				getcount,
				"\"}"
			});
			this.Context.Response.Write(flag);
		}

		public void Wx_MemSign()
		{
			int flag = 0;
			try
			{
				int memid = int.Parse(this.Request["memid"].ToString());
				int point = int.Parse(this.Request["point"].ToString());
				Chain.BLL.MemSign bllSign = new Chain.BLL.MemSign();
				int count = bllSign.GetRecordCount("MemID=" + memid + " and convert(char(10),SignTime,121)=convert(char(10),getdate(),121)");
				if (count == 0)
				{
					if (bllSign.Add(new Chain.Model.MemSign
					{
						MemID = memid,
						SignTime = DateTime.Now,
						GivePoint = point
					}) > 0)
					{
						Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
						bllMem.UpdatePoint(memid, point);
						if (PubFunction.curParameter.bolShopPointManage)
						{
							PubFunction.SetShopPoint(1, 1, point, "会员微信签到送积分", 2);
						}
						Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
						bllPoint.Add(new Chain.Model.PointLog
						{
							PointMemID = memid,
							PointNumber = point,
							PointChangeType = 16,
							PointRemark = "会员微信签到送积分",
							PointShopID = 1,
							PointCreateTime = DateTime.Now,
							PointUserID = 1,
							PointOrderCode = ""
						});
						flag = 1;
					}
				}
				else
				{
					flag = -1;
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Response.Write(flag);
		}

		public void WeiXinShopLogin()
		{
			int flag = 1;
			Chain.BLL.SysUser WXUser = new Chain.BLL.SysUser();
			Chain.Model.SysUser WXUserModel = new Chain.Model.SysUser();
			try
			{
				string UserAccount = this.Request["userid"].ToString();
				string passwords = this.Request["pwd"].ToString();
				string password = DESEncrypt.Encrypt(passwords);
				string Yanzheng = this.Request["Yanzheng"].ToString();
				ValCodeModel modeValCode = (ValCodeModel)this.Session["ValCode"];
				if (Yanzheng == modeValCode.valCode)
				{
					WXUserModel = WXUser.GetModel(UserAccount);
					if (password == WXUserModel.UserPassword)
					{
						this.Session["userid"] = WXUserModel.UserID.ToString();
						this.Response.Cookies.Add(new HttpCookie("userid", WXUserModel.UserID.ToString()));
						flag = 2;
					}
					else
					{
						flag = 5;
					}
				}
				else
				{
					flag = 3;
				}
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void WxGetDiscountMoney()
		{
			decimal lastMoney = 0m;
			decimal lastPoint = 0m;
			try
			{
				decimal money = decimal.Parse(this.Request["money"].ToString());
				int shopid = int.Parse(this.Request["shopid"].ToString());
				int levelid = int.Parse(this.Request["levelid"].ToString());
				Chain.Model.SysShopMemLevel ModelLevel = new Chain.BLL.SysShopMemLevel().GetModel(levelid, shopid);
				lastMoney = money * ModelLevel.ClassDiscountPercent;
				lastPoint = lastMoney / ModelLevel.ClassPointPercent;
			}
			catch
			{
			}
			string s = string.Concat(new object[]
			{
				"{\"Money\":\"",
				lastMoney.ToString("0.00"),
				"\",\"Point\":\"",
				Math.Floor(lastPoint),
				"\"}"
			});
			this.Context.Response.Write(s);
		}

		public void Wx_GetGoodsInfo()
		{
			string result = "0";
			try
			{
				decimal pointPercent = 0m;
				decimal discount = 0m;
				int shopID = int.Parse(this.Request["shopid"].ToString());
				int MemID = int.Parse(this.Request["memid"].ToString());
				int GoodsID = int.Parse(this.Request["goodsid"].ToString());
				if (MemID != 0)
				{
					Chain.Model.Goods modelGoods = new Chain.BLL.Goods().GetModel(GoodsID);
					Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
					DataTable dt = new Chain.BLL.GoodsClassDiscount().GetListByClassID(modelGoods.GoodsClassID).Tables[0];
					if (dt.Rows.Count > 0)
					{
						pointPercent = decimal.Parse(dt.Rows[0]["ClassPointPercent"].ToString());
						discount = decimal.Parse(dt.Rows[0]["ClassDiscountPercent"].ToString());
					}
				}
				result = string.Concat(new object[]
				{
					"{\"pointPercent\":\"",
					pointPercent,
					"\",\"discount\":\"",
					discount,
					"\"}"
				});
			}
			catch
			{
				result = "0";
			}
			this.Response.Write(result);
		}

		public void SendCode()
		{
			string memCard = (this.Request["memCard"] != null) ? this.Request["memCard"].ToString() : "";
			string strMemContent = (this.Request["strMemContent"] != null) ? this.Request["strMemContent"].ToString() : "";
			Chain.Model.Mem memModel = new Chain.BLL.Mem().GetModel(memCard);
			string strMemReceiver = memModel.MemMobile;
			int intUserID = 1;
			int intUserShopID = 1;
			string[] memReceiverArray = Regex.Split(strMemReceiver, ";", RegexOptions.IgnoreCase);
			string strMemMobile = "";
			string[] array = memReceiverArray;
			for (int j = 0; j < array.Length; j++)
			{
				string i = array[j];
				if (i != "")
				{
					string[] memReceiver = Regex.Split(i, ":", RegexOptions.IgnoreCase);
					strMemMobile = strMemMobile + memReceiver[0] + ",";
				}
			}
			if (strMemMobile != "")
			{
				if (strMemMobile.Substring(strMemMobile.Length - 1, 1) == ",")
				{
					strMemMobile = strMemMobile.Remove(strMemMobile.LastIndexOf(","), 1);
				}
			}
			int intFlag;
			if (int.Parse(SMSInfo.GetBalance(false).ToString()) > 0)
			{
				if (PubFunction.curParameter.strSmsShopName != "")
				{
					strMemContent = strMemContent + "【" + PubFunction.curParameter.strSmsShopName + "】";
				}
				if (!PubFunction.IsCanSendSms(1, strMemMobile.Split(new char[]
				{
					','
				}).Length))
				{
					intFlag = 5;
				}
				else if (SMSInfo.Send_SMS(false, strMemMobile, strMemContent, ""))
				{
					intFlag = 1;
					Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
					modelSms.SmsMemID = 0;
					modelSms.SmsMobile = strMemReceiver;
					modelSms.SmsContent = strMemContent;
					modelSms.SmsTime = DateTime.Now;
					modelSms.SmsShopID = intUserShopID;
					modelSms.SmsUserID = intUserID;
					modelSms.SmsAmount = PubFunction.GetSmsAmount(strMemContent);
					modelSms.SmsAllAmount = modelSms.SmsAmount * strMemMobile.Split(new char[]
					{
						','
					}).Length;
					Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
					bllSms.Add(modelSms);
					if (PubFunction.curParameter.bolShopSmsManage)
					{
						PubFunction.SetShopSms(intUserID, 1, strMemMobile.Split(new char[]
						{
							','
						}).Length, 2);
					}
					PubFunction.SaveSysLog(intUserID, 4, "发送短信", "给会员发送短信", intUserShopID, DateTime.Now, PubFunction.ipAdress);
				}
				else
				{
					intFlag = 2;
				}
			}
			else
			{
				intFlag = 3;
			}
			this.Context.Response.Write(intFlag);
		}

		public void GetIntroduceMem()
		{
			string memCrad = this.Request["introduceMemCard"].ToString();
			Chain.BLL.Mem MemBll = new Chain.BLL.Mem();
			Chain.Model.Mem MemModel = MemBll.GetModel(memCrad);
			string flag;
			if (MemModel != null)
			{
				flag = string.Concat(new object[]
				{
					"姓名：",
					MemModel.MemName,
					"；余额：",
					MemModel.MemMoney
				});
			}
			else
			{
				flag = "";
			}
			this.Response.Write(flag);
		}

		public void GetWxRptTotal()
		{
			string flag = "0";
			try
			{
				string strUserName = "";
				DateTime time = DateTime.Now;
				DateTime time2 = DateTime.Now;
				string checkRadion = (this.Request["type"] != null) ? this.Request["type"].ToString() : "";
				string shopID = (this.Request["shopid"] != null) ? this.Request["shopid"].ToString() : "";
				int sysShopId = int.Parse(shopID);
				if (shopID != "")
				{
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(int.Parse(shopID));
					if (modelShop.ShopType == 1 || modelShop.ShopType == 2)
					{
						shopID = "";
					}
				}
				string userID = "";
				if (userID == "" || userID == null)
				{
					strUserName = "所有操作员";
				}
				else
				{
					strUserName = PubFunction.UserIDTOName(int.Parse(userID));
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
				orderWhere += " and OrderType <> 3 ";
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
				DataRow[] drss = ds.Tables[1].Select(" RechargeType=2 ");
				if (drss.Length == 1)
				{
					FRechargeGiveMoney += decimal.Parse(drss[0][1].ToString());
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
				decimal FRechargeWebMoney = 0m;
				drs = ds.Tables[1].Select(" RechargeType=5 ");
				if (drs.Length == 1)
				{
					FRechargeWebMoney += decimal.Parse(drs[0][0].ToString());
				}
				decimal WXCZ = 0m;
				drs = ds.Tables[1].Select(" RechargeType=6 ");
				if (drs.Length == 1)
				{
					WXCZ += decimal.Parse(drs[0][0].ToString());
				}
				decimal payBink = 0m;
				decimal payCash = 0m;
				decimal payCard = 0m;
				decimal payCoupon = 0m;
				decimal payPoint = 0m;
				decimal zljg = 0m;
				foreach (DataRow dr in ds.Tables[2].Rows)
				{
					payCard += decimal.Parse(dr["OrderPayCard"].ToString());
					payCash += decimal.Parse(dr["OrderPayCash"].ToString());
					payBink += decimal.Parse(dr["OrderPayBink"].ToString());
					payPoint += decimal.Parse(dr["UsePointAmount"].ToString());
					zljg += decimal.Parse(dr["UsePointAmount"].ToString()) + decimal.Parse(dr["OrderPayCard"].ToString()) + decimal.Parse(dr["OrderPayBink"].ToString()) + decimal.Parse(dr["OrderPayCash"].ToString()) + decimal.Parse(dr["OrderPayCoupon"].ToString()) - decimal.Parse(dr["OrderDiscountMoney"].ToString());
				}
				decimal countPayBink = 0m;
				decimal countPayCash = 0m;
				decimal countPayCard = 0m;
				decimal countPayCoupon = 0m;
				decimal jczl = 0m;
				foreach (DataRow dr in ds.Tables[3].Rows)
				{
					countPayCard += decimal.Parse(dr["CountPayCard"].ToString());
					countPayCash += decimal.Parse(dr["CountPayCash"].ToString());
					countPayBink += decimal.Parse(dr["CountPayBink"].ToString());
					countPayCoupon += decimal.Parse(dr["CountPayCoupon"].ToString());
					jczl += decimal.Parse(dr["CountPayCard"].ToString()) + decimal.Parse(dr["CountPayBink"].ToString()) + decimal.Parse(dr["CountPayCash"].ToString()) + decimal.Parse(dr["CountPayCoupon"].ToString()) - decimal.Parse(dr["CountDiscountMoney"].ToString());
				}
				decimal StorageTimingPayCard = 0m;
				decimal StorageTimingPayCash = 0m;
				decimal StorageTimingPayBink = 0m;
				decimal StorageTimingPayCoupon = 0m;
				decimal StorageTimingzlmoney = 0m;
				if (ds.Tables[4].Rows.Count > 0)
				{
					StorageTimingPayCard = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingPayCard"]);
					StorageTimingPayCash = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingPayCash"]);
					StorageTimingPayBink = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingPayBink"]);
					StorageTimingPayCoupon = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingPayCoupon"]);
					decimal StorageTimingDiscountMoney = Convert.ToDecimal(ds.Tables[4].Rows[0]["StorageTimingDiscountMoney"]);
					StorageTimingzlmoney = StorageTimingPayCard + StorageTimingPayCash + StorageTimingPayBink;
				}
				decimal expenseSumMoneys = payCash - zljg;
				decimal countSumMoneys = countPayCash - jczl;
				decimal allMoney = FRechargeMoney + expenseSumMoneys + countSumMoneys + (StorageTimingPayCash - StorageTimingzlmoney) - Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]);
				decimal doWorkallMoney = FRechargeMoney + expenseSumMoneys + countSumMoneys + (StorageTimingPayCash - StorageTimingzlmoney) - Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]);
				flag = string.Concat(new object[]
				{
					"{\"timeRadion\":\"",
					time.ToString("yyyy-MM-dd"),
					" 至 ",
					time2.ToString("yyyy-MM-dd"),
					"\",\"intNumber\":\"",
					intNumber,
					"\",\"memSRechargeMoney\":\"",
					SRechargeMoney.ToString("#0.00"),
					"\",\"memFRechargeMoney\":\"",
					FRechargeMoney.ToString("#0.00"),
					"\",\"expenseSumMoneys\":\"",
					expenseSumMoneys.ToString("#0.00"),
					"\",\"expenseBankSumMoneys\":\"",
					FRechargeBankMoney.ToString("#0.00"),
					"\",\"FRechargeWebMoney\":\"",
					FRechargeWebMoney.ToString("#0.00"),
					"\",\"WXCZ\":\"",
					WXCZ.ToString("#0.00"),
					"\",\"payCard\":\"",
					payCard.ToString("#0.00"),
					"\",\"payBink\":\"",
					payBink.ToString("#0.00"),
					"\",\"payCoupon\":\"",
					payCoupon.ToString("#0.00"),
					"\",\"payPoint\":\"",
					payPoint.ToString("#0.00"),
					"\",\"FRechargeGiveMoney\":\"",
					FRechargeGiveMoney.ToString("#0.00"),
					"\",\"allMoney\":\"",
					allMoney.ToString("#0.00"),
					"\",\"MemDetial\":\"",
					sbMem,
					"\",\"strUserName\":\"",
					strUserName,
					"\",\"doWorkallMoney\":\"",
					doWorkallMoney,
					"\",\"countSumMoneys\":\"",
					countSumMoneys.ToString("#0.00"),
					"\",\"countPayCard\":\"",
					countPayCard.ToString("#0.00"),
					"\",\"countPayBink\":\"",
					countPayBink.ToString("#0.00"),
					"\",\"countpayCoupon\":\"",
					countPayCoupon.ToString("#0.00"),
					"\",\"StorageTimingPayCard\":\"",
					StorageTimingPayCard.ToString("f2"),
					"\",\"StorageTimingPayCash\":\"",
					StorageTimingPayCash.ToString("f2"),
					"\",\"StorageTimingPayBink\":\"",
					StorageTimingPayBink.ToString("f2"),
					"\",\"StorageTimingPayCoupon\":\"",
					StorageTimingPayCoupon.ToString("f2"),
					"\",\"AllDrawMoney\":\"",
					Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]).ToString("F2"),
					"\",\"AllDrawActualMoney\":\"",
					Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawActualMoney"]).ToString("F2"),
					"\"}"
				});
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-3";
			}
			this.Context.Response.Write(flag);
		}

		public void GetWXMem()
		{
			string msgResponse = "";
			try
			{
				string memCard = (this.Request["memCard"].ToString() != "") ? this.Request["memCard"].ToString() : "";
				int shopid = int.Parse(this.Request["shopid"].ToString());
				StringBuilder strSql = new StringBuilder();
				string strCustomField = "";
				DataTable dt = new Chain.BLL.MemCustomField().GetList(" CustomType=1").Tables[0];
				foreach (DataRow dr in dt.Rows)
				{
					strCustomField = strCustomField + dr["CustomField"].ToString() + ",";
				}
				strSql.Append(" 1=1 ");
				if (memCard != "")
				{
					strSql.AppendFormat(" and (MemCard='{0}' or MemMobile='{0}' or MemCardNumber='{0}')", memCard);
				}
				strSql.Append(" and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID=SysUser.UserID");
				strSql.Append(" and Mem.MemID>0 AND dbo.SysShopMemLevel.MemLevelID=Mem.MemLevelID");
				strSql.AppendFormat(" AND dbo.SysShopMemLevel.ShopID='{0}' ", shopid);
				int counts = 0;
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				DataTable dtMem = bllMem.GetListSP(20, 1, out counts, new string[]
				{
					strSql.ToString()
				}).Tables[0];
				for (int i = 0; i < dtMem.Rows.Count; i++)
				{
					dtMem.Rows[i]["MemRemark"] = StringPlus.HtmlDecode(dtMem.Rows[i]["MemRemark"].ToString());
				}
				if (dtMem.Rows.Count > 0)
				{
					msgResponse = JsonPlus.ToJson(dtMem, "MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemCreateTime,MemRemark,MemLevelID,MemShopID,MemUserID,ShopName," + strCustomField + "LevelID,LevelName,LevelPoint,ClassDiscountPercent,ClassPointPercent,UserName,MemTelePhone,MemQRCode,MemProvinceName,MemCityName,MemCountyName,MemVillageName,MemCardNumber,MemCountNumber,ClassRechargePointRate");
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void Wx_GoodsExpense()
		{
			string flag = "0";
			try
			{
				int intUserID = int.Parse(this.Request["userID"].ToString());
				int intUserShopID = int.Parse(this.Request["shopID"].ToString());
				int memID = (this.Request["memid"] != "") ? int.Parse(this.Request["memid"].ToString()) : 0;
				decimal dclDiscountMoney = decimal.Parse(this.Request["totalMoney"]);
				decimal dclBinkPayMoney = 0m;
				decimal dclCouponPayMoney = 0m;
				decimal dclTotalMoney = decimal.Parse(this.Request["totalMoney"]);
				int intPoint = int.Parse(this.Request["point"]);
				int usePoint = 0;
				decimal usePointAmount = 0m;
				string strOrderCode = this.Request["orderAccount"].ToString();
				string strRemark = this.Request["remark"].ToString();
				strRemark = PubFunction.RemoveSpace(strRemark);
				int intCount = int.Parse(this.Request["count"]);
				int payType = int.Parse(this.Request["payType"].ToString());
				bool bolIsCard = false;
				bool bolIsCash = false;
				bool bolIsBink = false;
				decimal dclCardPayMoney = 0m;
				decimal dclCashPayMoney = 0m;
				bool bolIsEmptyBills = false;
				if (payType == 0)
				{
					bolIsCard = true;
					dclCardPayMoney = dclTotalMoney;
				}
				else
				{
					bolIsCash = true;
					dclCashPayMoney = dclTotalMoney;
				}
				Chain.Model.Mem modelMem = this.bllMem.GetModel(memID);
				int intOldLevelID = modelMem.MemLevelID;
				if (modelMem.MemMoney < dclCardPayMoney && payType == 0)
				{
					flag = "-7";
					this.Context.Response.Write(flag);
					return;
				}
				if (!PubFunction.IsShopPoint(intUserShopID, ref intPoint))
				{
					flag = "-6";
				}
				int intOrderLogID = 0;
				if (flag != "-6")
				{
					this.modelOrderLog.OrderAccount = strOrderCode;
					this.modelOrderLog.OrderMemID = memID;
					this.modelOrderLog.OrderType = 2;
					this.modelOrderLog.OrderTotalMoney = dclTotalMoney;
					this.modelOrderLog.OrderDiscountMoney = dclDiscountMoney;
					this.modelOrderLog.OrderIsCard = bolIsCard;
					this.modelOrderLog.OrderPayCard = dclCardPayMoney;
					this.modelOrderLog.OrderIsCash = bolIsCash;
					this.modelOrderLog.OrderPayCash = dclCashPayMoney;
					this.modelOrderLog.OrderIsBink = bolIsBink;
					this.modelOrderLog.OrderPayBink = dclBinkPayMoney;
					this.modelOrderLog.OrderPayCoupon = dclCouponPayMoney;
					this.modelOrderLog.OrderPoint = intPoint;
					this.modelOrderLog.OrderRemark = strRemark;
					this.modelOrderLog.OrderPayType = 0;
					this.modelOrderLog.OrderShopID = intUserShopID;
					this.modelOrderLog.OrderUserID = intUserID;
					this.modelOrderLog.OrderCreateTime = DateTime.Now;
					this.modelOrderLog.OldAccount = "";
					this.modelOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
					this.modelOrderLog.UsePoint = usePoint;
					this.modelOrderLog.UsePointAmount = usePointAmount;
					if (this.bllOrderLog.ExistsOrderAccount(strOrderCode))
					{
						flag = "-4";
					}
					else
					{
						intOrderLogID = this.bllOrderLog.Add(this.modelOrderLog, strOrderCode);
					}
				}
				int intGoodsLogID = 0;
				if (intOrderLogID > 0)
				{
					this.modelGoodsLog.GoodsAccount = strOrderCode;
					this.modelGoodsLog.Type = 2;
					this.modelGoodsLog.Remark = "商品销售出库";
					this.modelGoodsLog.TotalPrice = dclDiscountMoney;
					this.modelGoodsLog.CreateTime = DateTime.Now;
					this.modelGoodsLog.ShopID = intUserShopID;
					this.modelGoodsLog.UserID = intUserID;
					this.modelGoodsLog.ChangeShopID = intUserShopID;
					if (!bolIsEmptyBills)
					{
						intGoodsLogID = this.bllGoodsLog.Add(this.modelGoodsLog);
					}
					else
					{
						DataTable dtGoods = this.bllGoodsLog.GetList("GoodsAccount='" + strOrderCode + "'").Tables[0];
						if (dtGoods.Rows.Count > 0)
						{
							int IntOldGoodsLogID = int.Parse(dtGoods.Rows[0]["ID"].ToString());
							this.modelGoodsLog.ID = IntOldGoodsLogID;
							if (this.bllGoodsLog.Update(this.modelGoodsLog))
							{
								intGoodsLogID = IntOldGoodsLogID;
							}
						}
					}
				}
				int intOrderDetailID = 0;
				if (intGoodsLogID > 0)
				{
					for (int i = 0; i < intCount; i++)
					{
						this.modelDetail.OrderID = intOrderLogID;
						this.modelDetail.GoodsID = int.Parse(this.Request["data[" + i + "][goodsid]"]);
						this.modelDetail.OrderDetailPrice = decimal.Parse(this.Request["data[" + i + "][price]"]);
						this.modelDetail.OrderDetailDiscountPrice = decimal.Parse(this.Request["data[" + i + "][discountmoney]"]);
						this.modelDetail.OrderDetailNumber = decimal.Parse(this.Request["data[" + i + "][count]"]);
						this.modelDetail.OrderDetailPoint = int.Parse(this.Request["data[" + i + "][point]"].ToString());
						this.modelDetail.OrderDetailType = 0;
						intOrderDetailID = this.bllDetail.Add(this.modelDetail);
						int intGoodsType = int.Parse(this.Request["data[" + i + "][goodsType]"]);
						if (intGoodsType == 0)
						{
							this.modelNumber.GoodsID = this.modelDetail.GoodsID;
							this.modelNumber.ShopID = intUserShopID;
							this.modelNumber.Number = decimal.Parse((this.modelDetail.OrderDetailNumber * -1m).ToString());
							this.bllNumber.UpdataGoodsNumber(this.modelNumber);
						}
						Chain.Model.GoodsLogDetail modelGoodsLogDetail = new Chain.Model.GoodsLogDetail();
						modelGoodsLogDetail.GoodsLogID = intGoodsLogID;
						modelGoodsLogDetail.GoodsID = int.Parse(this.Request["data[" + i + "][goodsid]"]);
						modelGoodsLogDetail.GoodsInPrice = decimal.Parse(this.Request["data[" + i + "][price]"]);
						modelGoodsLogDetail.GoodsOutPrice = decimal.Parse(this.Request["data[" + i + "][price]"]) * decimal.Parse(this.Request["data[" + i + "][discount]"]);
						modelGoodsLogDetail.GoodsNumber = decimal.Parse(this.Request["data[" + i + "][count]"]) * -1m;
						this.bllGoodsDetail.Add(modelGoodsLogDetail);
					}
				}
				if (intOrderDetailID > 0)
				{
					if (memID == 0)
					{
						flag = "{\"Success\":\"" + intOrderLogID + "\",\"strUpdateMemLevel\":\"\",\"point\":0}";
						string Remark = string.Concat(new object[]
						{
							"散客商品消费,订单号：[",
							strOrderCode,
							"],消费金额：[",
							dclDiscountMoney,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "散客消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
					else
					{
						decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
						modelMem.MemConsumeMoney += dclDiscountMoney;
						modelMem.MemPoint += intPoint - usePoint;
						modelMem.MemConsumeLastTime = DateTime.Now;
						modelMem.MemConsumeCount++;
						int mem = this.bllMem.ExpenseUpdateMem(memID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
						if (intPoint != 0)
						{
							Chain.Model.PointLog modelPointLog = new Chain.Model.PointLog();
							modelPointLog.PointMemID = memID;
							modelPointLog.PointNumber = intPoint;
							modelPointLog.PointChangeType = 1;
							modelPointLog.PointRemark = "会员商品消费成功，消费总额：[" + dclDiscountMoney + "]";
							modelPointLog.PointShopID = intUserShopID;
							modelPointLog.PointUserID = intUserID;
							modelPointLog.PointCreateTime = DateTime.Now;
							modelPointLog.PointOrderCode = strOrderCode;
							this.bllPoint.Add(modelPointLog);
						}
						Chain.Model.MoneyChangeLog modelMoneyChangeLog = new Chain.Model.MoneyChangeLog();
						modelMoneyChangeLog.MoneyChangeMemID = modelMem.MemID;
						modelMoneyChangeLog.MoneyChangeUserID = intUserID;
						modelMoneyChangeLog.MoneyChangeType = 12;
						modelMoneyChangeLog.MoneyChangeAccount = strOrderCode;
						modelMoneyChangeLog.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
						modelMoneyChangeLog.MoneyChangeBalance = -dclCardPayMoney;
						modelMoneyChangeLog.MoneyChangeCash = -dclCashPayMoney;
						modelMoneyChangeLog.MoneyChangeUnionPay = -dclBinkPayMoney;
						modelMoneyChangeLog.MemMoney = modelMem.MemMoney - dclCardPayMoney;
						modelMoneyChangeLog.MoneyChangeCreateTime = DateTime.Now;
						modelMoneyChangeLog.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(modelMoneyChangeLog);
						MEMPointUpdate.MEMPointRate(modelMem, this.modelOrderLog.OrderPoint, this.modelOrderLog.OrderAccount, 1, intUserID, intUserShopID);
						modelMem = new Chain.BLL.Mem().GetModel(memID);
						string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
						modelMem = new Chain.BLL.Mem().GetModel(memID);
						PubFunction.SetShopPoint(intUserID, intUserShopID, intPoint, string.Concat(new object[]
						{
							"单号：[",
							strOrderCode,
							"],会员商品消费金额：[",
							dclDiscountMoney,
							"],扣除商家积分：[",
							intPoint,
							"]"
						}), 2);
						if (usePoint != 0)
						{
							PubFunction.SetShopPoint(intUserID, intUserShopID, -usePoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],会员商品消费抵用积分：[",
								usePoint,
								"]，商家回收积分：[",
								usePoint,
								"]"
							}), 4);
							Chain.Model.PointLog modelPointLog = new Chain.Model.PointLog();
							modelPointLog.PointMemID = memID;
							modelPointLog.PointNumber = usePoint;
							modelPointLog.PointChangeType = 1;
							modelPointLog.PointRemark = string.Concat(new object[]
							{
								"会员商品消费成功,抵用积分：[",
								usePoint,
								"],抵用金额：[",
								usePointAmount,
								"]"
							});
							modelPointLog.PointShopID = intUserShopID;
							modelPointLog.PointUserID = intUserID;
							modelPointLog.PointCreateTime = DateTime.Now;
							modelPointLog.PointOrderCode = strOrderCode;
							this.bllPoint.Add(modelPointLog);
						}
						Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
						Chain.Model.SysShop modelShop = bllSysShop.GetModel(intUserShopID);
						decimal flTotalRate = decimal.Parse(Convert.ToDouble(modelShop.TotalRate).ToString("#0.00"));
						if (flTotalRate > 0m)
						{
							int flTotalPoint = (int)(flTotalRate * intPoint);
							decimal alliancePercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.AllianceRebatePercent).ToString("#0.00"));
							decimal cardShopPercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.CardShopRebatePercent).ToString("#0.00"));
							int alliancePoint = (int)(flTotalPoint * alliancePercent);
							int cardShopPoint = (int)(flTotalPoint * cardShopPercent);
							int zbPoint = flTotalPoint - alliancePoint - cardShopPoint;
							Chain.BLL.ReturnPointLog bllReturnPoint = new Chain.BLL.ReturnPointLog();
							bllReturnPoint.Add(new Chain.Model.ReturnPointLog
							{
								OrderAccount = strOrderCode,
								MemID = memID,
								TotalPoint = flTotalPoint,
								AlliancePoint = alliancePoint,
								ZbPoint = zbPoint,
								CardShopPoint = cardShopPoint,
								Remark = "会员商品消费,商家返利积分",
								ReturnShopID = intUserShopID,
								CreateTime = DateTime.Now
							});
							PubFunction.SetShopPoint(intUserID, intUserShopID, intUserShopID, flTotalPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],会员消费得积分：[",
								intPoint,
								"],返利总比例：[",
								flTotalRate,
								"],商家扣除返利总积分：[",
								flTotalPoint,
								"]"
							}), 2);
							PubFunction.SetShopPoint(intUserID, intUserShopID, modelShop.FatherShopID, -alliancePoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],商家总返利积分：[",
								flTotalPoint,
								"],联盟商返利比例：[",
								alliancePercent,
								"],联盟商得到返利积分：[",
								alliancePoint,
								"]"
							}), 3);
							PubFunction.SetShopPoint(intUserID, intUserShopID, modelMem.MemShopID, -cardShopPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],商家总返利积分：[",
								flTotalPoint,
								"],开卡商家返利比例：[",
								cardShopPercent,
								"],开卡商家得到返利积分：[",
								cardShopPoint,
								"]"
							}), 3);
							PubFunction.SetShopPoint(intUserID, intUserShopID, 1, -zbPoint, string.Concat(new object[]
							{
								"单号：[",
								strOrderCode,
								"],商家总返利积分：[",
								flTotalPoint,
								"],运营商得到返利积分：[",
								zbPoint,
								"]"
							}), 3);
						}
						flag = string.Concat(new object[]
						{
							"{\"Success\":\"",
							intOrderLogID,
							"\",\"strUpdateMemLevel\":\"",
							strUpdateMemLevel,
							"\",\"point\":",
							intPoint.ToString(),
							"}"
						});
						string Remark = string.Concat(new object[]
						{
							"会员商品消费,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],订单号：[",
							strOrderCode,
							"],消费金额：[",
							dclDiscountMoney,
							"],获得积分：[",
							intPoint,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "-1";
			}
			this.Context.Response.Write(flag);
		}

		public void Wx_GoodsExpenseOld()
		{
			string flag = "0";
			int isMem = int.Parse(this.Request["isMem"].ToString());
			int memID = 0;
			if (isMem == 1)
			{
				memID = int.Parse(this.Request["memid"].ToString());
			}
			int totalMoney = int.Parse(this.Request["totalMoney"].ToString());
			int sumNumber = int.Parse(this.Request["sumNumber"].ToString());
			int intCount = int.Parse(this.Request["count"].ToString());
			string strOrderCode = this.Request["orderAccount"].ToString();
			int intUserID = int.Parse(this.Request["userID"].ToString());
			int intUserShopID = int.Parse(this.Request["shopID"].ToString());
			int payType = int.Parse(this.Request["payType"].ToString());
			int orderID = 0;
			string strRemark = "";
			int GoodID = int.Parse(this.Request["GoodsList[0][goodsid]"].ToString());
			decimal discount = decimal.Parse(this.Request["GoodsList[0][discountmoney]"].ToString());
			decimal totalMoneyNoDiscount = 0m;
			for (int i = 0; i < intCount; i++)
			{
				decimal price = decimal.Parse(this.Request["GoodsList[" + i + "][price]"].ToString());
				int count = int.Parse(this.Request["GoodsList[" + i + "][count]"].ToString());
				totalMoneyNoDiscount += price * count;
			}
			try
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				Chain.BLL.GoodsLog bllGoodsLog = new Chain.BLL.GoodsLog();
				Chain.Model.GoodsLog modelGoodsLog = new Chain.Model.GoodsLog();
				Chain.BLL.GoodsNumber bllNumber = new Chain.BLL.GoodsNumber();
				Chain.Model.GoodsNumber modelNumber = new Chain.Model.GoodsNumber();
				Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
				Chain.Model.OrderLog modelOrderLog = new Chain.Model.OrderLog();
				Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
				Chain.Model.OrderDetail modelDetail = new Chain.Model.OrderDetail();
				Chain.BLL.OrderDetail bllDetail = new Chain.BLL.OrderDetail();
				Chain.BLL.GoodsLogDetail bllGoodsDetail = new Chain.BLL.GoodsLogDetail();
				int levelID = modelMem.MemLevelID;
				int intOrderLogID = 0;
				if (modelMem.MemState != 0)
				{
					flag = "-1";
				}
				else
				{
					modelOrderLog.OrderAccount = strOrderCode;
					modelOrderLog.OrderMemID = memID;
					modelOrderLog.OrderType = 2;
					modelOrderLog.OrderTotalMoney = totalMoneyNoDiscount;
					modelOrderLog.OrderDiscountMoney = totalMoney;
					if (payType == 0)
					{
						modelOrderLog.OrderIsCard = true;
						modelOrderLog.OrderPayCard = totalMoney;
					}
					else
					{
						modelOrderLog.OrderIsCard = false;
						modelOrderLog.OrderPayCard = 0m;
					}
					if (payType == 1)
					{
						modelOrderLog.OrderIsCash = true;
						modelOrderLog.OrderPayCash = totalMoney;
					}
					else
					{
						modelOrderLog.OrderIsCash = false;
						modelOrderLog.OrderPayCash = 0m;
					}
					modelOrderLog.OrderIsBink = false;
					modelOrderLog.OrderPayBink = 0m;
					modelOrderLog.OrderPayCoupon = 0m;
					modelOrderLog.OrderPoint = totalMoney;
					modelOrderLog.OrderRemark = "";
					modelOrderLog.OrderPayType = payType;
					modelOrderLog.OrderShopID = intUserShopID;
					modelOrderLog.OrderUserID = intUserID;
					modelOrderLog.OrderCreateTime = DateTime.Now;
					modelOrderLog.OldAccount = "";
					if (memID > 0)
					{
						modelOrderLog.OrderCardBalance = modelMem.MemMoney - totalMoney;
						modelOrderLog.OrderCardPoint = modelMem.MemPoint + totalMoney;
					}
					else
					{
						modelOrderLog.OrderCardBalance = 0m;
						modelOrderLog.OrderCardPoint = 0;
					}
					modelOrderLog.UsePoint = 0;
					if (bllOrderLog.ExistsOrderAccount(strOrderCode))
					{
						flag = "-4";
					}
					else
					{
						intOrderLogID = bllOrderLog.Add(modelOrderLog, strOrderCode);
					}
				}
				int intGoodsLogID = 0;
				int IntOldGoodsLogID = 0;
				if (intOrderLogID > 0)
				{
					modelGoodsLog.GoodsAccount = strOrderCode;
					modelGoodsLog.Type = 2;
					modelGoodsLog.Remark = "商品消费出库";
					modelGoodsLog.TotalPrice = totalMoney;
					modelGoodsLog.CreateTime = DateTime.Now;
					modelGoodsLog.ShopID = intUserShopID;
					modelGoodsLog.UserID = intUserID;
					modelGoodsLog.ChangeShopID = intUserShopID;
					intGoodsLogID = bllGoodsLog.Add(modelGoodsLog);
				}
				if (intOrderLogID > 0)
				{
					DataTable dtOrderDetail = bllDetail.GetList("OrderID=" + orderID).Tables[0];
					int intUpdateNumber = 0;
					for (int j = 0; j < dtOrderDetail.Rows.Count; j++)
					{
						modelNumber.GoodsID = int.Parse(dtOrderDetail.Rows[j]["GoodsID"].ToString());
						modelNumber.ShopID = intUserShopID;
						modelNumber.Number = int.Parse(dtOrderDetail.Rows[j]["OrderDetailNumber"].ToString());
						intUpdateNumber = bllNumber.UpdataGoodsNumber(modelNumber);
					}
					if (intUpdateNumber > 0)
					{
						bllDetail.DeleteDetail(orderID);
					}
					bllGoodsDetail.DeleteDetail(IntOldGoodsLogID);
				}
				int intOrderDetailID = 0;
				if (intGoodsLogID > 0)
				{
					for (int i = 0; i < intCount; i++)
					{
						modelDetail.OrderID = intOrderLogID;
						modelDetail.GoodsID = int.Parse(this.Request["GoodsList[" + i + "][goodsid]"]);
						modelDetail.OrderDetailPrice = decimal.Parse(this.Request["GoodsList[" + i + "][price]"]);
						modelDetail.OrderDetailDiscountPrice = decimal.Parse(this.Request["GoodsList[" + i + "][discountmoney]"]);
						modelDetail.OrderDetailNumber = decimal.Parse(this.Request["GoodsList[" + i + "][count]"]);
						modelDetail.OrderDetailPoint = (int)Math.Floor(decimal.Parse(this.Request["GoodsList[" + i + "][discountmoney]"].ToString()));
						if (decimal.Parse(this.Request["GoodsList[" + i + "][count]"]) > 0m)
						{
							modelDetail.OrderDetailType = 0;
						}
						else
						{
							modelDetail.OrderDetailType = 1;
						}
						intOrderDetailID = bllDetail.Add(modelDetail);
						modelNumber.GoodsID = modelDetail.GoodsID;
						modelNumber.ShopID = intUserShopID;
						modelNumber.Number = decimal.Parse((modelDetail.OrderDetailNumber * -1m).ToString());
						bllNumber.UpdataGoodsNumber(modelNumber);
						bllGoodsDetail.Add(new Chain.Model.GoodsLogDetail
						{
							GoodsLogID = intGoodsLogID,
							GoodsID = int.Parse(this.Request["GoodsList[" + i + "][goodsid]"]),
							GoodsInPrice = decimal.Parse(this.Request["GoodsList[" + i + "][price]"]),
							GoodsOutPrice = decimal.Parse(this.Request["GoodsList[" + i + "][discountmoney]"].ToString()),
							GoodsNumber = decimal.Parse(this.Request["GoodsList[" + i + "][count]"]) * -1m,
						
						});
					}
				}
				if (intOrderDetailID > 0)
				{
					if (memID == 0)
					{
						flag = "{\"Success\":\"" + intOrderLogID + "\",\"strUpdateMemLevel\":\"\",\"point\":0}";
						string Remark = string.Concat(new object[]
						{
							"散客商品消费,订单号：[",
							strOrderCode,
							"],消费金额：[",
							totalMoney,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "散客消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
					else
					{
						decimal dclMemMoney = modelMem.MemMoney;
						if (payType == 0)
						{
							dclMemMoney = modelMem.MemMoney - totalMoney;
						}
						modelMem.MemConsumeMoney += totalMoney;
						modelMem.MemPoint += totalMoney;
						modelMem.MemConsumeLastTime = DateTime.Now;
						modelMem.MemConsumeCount++;
						int mem = bllMem.ExpenseUpdateMem(memID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
						if (totalMoney != 0)
						{
							bllPoint.Add(new Chain.Model.PointLog
							{
								PointMemID = memID,
								PointNumber = totalMoney,
								PointChangeType = 1,
								PointRemark = "会员商品消费成功，消费总额：[" + totalMoney + "]",
								PointShopID = intUserShopID,
								PointUserID = intUserID,
								PointCreateTime = DateTime.Now,
								PointOrderCode = strOrderCode
							});
						}
						Chain.Model.MoneyChangeLog modelMoneyChangeLog = new Chain.Model.MoneyChangeLog();
						modelMoneyChangeLog.MoneyChangeMemID = modelMem.MemID;
						modelMoneyChangeLog.MoneyChangeUserID = intUserID;
						modelMoneyChangeLog.MoneyChangeType = 12;
						modelMoneyChangeLog.MoneyChangeAccount = strOrderCode;
						modelMoneyChangeLog.MoneyChangeMoney = totalMoney;
						if (payType == 0)
						{
							modelMoneyChangeLog.MoneyChangeBalance = -totalMoney;
							modelMoneyChangeLog.MoneyChangeCash = 0m;
							modelMoneyChangeLog.MemMoney = modelMem.MemMoney - totalMoney;
						}
						else
						{
							modelMoneyChangeLog.MoneyChangeBalance = 0m;
							modelMoneyChangeLog.MoneyChangeCash = -totalMoney;
							modelMoneyChangeLog.MemMoney = modelMem.MemMoney;
						}
						modelMoneyChangeLog.MoneyChangeUnionPay = 0m;
						modelMoneyChangeLog.MoneyChangeCreateTime = DateTime.Now;
						modelMoneyChangeLog.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(modelMoneyChangeLog);
						MEMPointUpdate.MEMPointRate(modelMem, modelOrderLog.OrderPoint, modelOrderLog.OrderAccount, 1, intUserID, intUserShopID);
						modelMem = new Chain.BLL.Mem().GetModel(memID);
						string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
						modelMem = new Chain.BLL.Mem().GetModel(memID);
						flag = string.Concat(new object[]
						{
							"{\"Success\":\"",
							intOrderLogID,
							"\",\"strUpdateMemLevel\":\"",
							strUpdateMemLevel,
							"\",\"point\":",
							totalMoney.ToString(),
							"}"
						});
						string Remark = string.Concat(new object[]
						{
							"会员商品消费,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],订单号：[",
							strOrderCode,
							"],消费金额：[",
							totalMoney,
							"],获得积分：[",
							totalMoney,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = "0";
			}
			this.Context.Response.Write(flag);
		}

		public void WX_Expense()
		{
			string flag = "-1";
			Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
			string payType = this.Request["payType"].ToString();
			int isMember = int.Parse(this.Request["ismember"].ToString());
			int intMemID = 0;
			if (isMember == 1)
			{
				intMemID = ((this.Request["memID"] != null) ? int.Parse(this.Request["memID"].ToString()) : 0);
			}
			int intUserID = (this.Request["userid"].ToString() != "") ? int.Parse(this.Request["userid"].ToString()) : 0;
			int intUserShopID = (this.Request["shopid"].ToString() != "") ? int.Parse(this.Request["shopid"].ToString()) : 0;
			int intPoint = (this.Request["point"].ToString() != "") ? int.Parse(this.Request["point"].ToString()) : 0;
			decimal dclMoney = (this.Request["money"].ToString() != "") ? decimal.Parse(this.Request["money"].ToString()) : 0m;
			decimal dclDiscountMoney = (this.Request["discountmoney"].ToString() != "") ? decimal.Parse(this.Request["discountmoney"].ToString()) : 0m;
			string strOrderAccount = (this.Request["OrderAccount"].ToString() != "") ? this.Request["OrderAccount"].ToString() : "";
			bool sendMSM = false;
			bool bolIsCard = false;
			bool bolIsCash = false;
			bool bolIsBink = false;
			decimal dclCardPayMoney = 0m;
			decimal dclBinkPayMoney = 0m;
			decimal dclCouponPayMoney = 0m;
			decimal dclCashPayMoney = 0m;
			if (payType == "cash")
			{
				bolIsCash = true;
				dclCashPayMoney = dclDiscountMoney;
			}
			if (payType == "balance")
			{
				bolIsCard = true;
				dclCardPayMoney = dclDiscountMoney;
			}
			DateTime dtNow = DateTime.Now;
			string strRemark = "";
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
			if (isMember == 1 && payType == "balance" && modelMem.MemMoney < dclDiscountMoney)
			{
				flag = "-3";
				this.Context.Response.Write(flag);
			}
			else
			{
				int intLevelID = modelMem.MemLevelID;
				string Remark;
				if (intMemID != 0)
				{
					Remark = string.Concat(new object[]
					{
						"会员商家中心快速消费,会员卡号：[",
						modelMem.MemCard,
						"],姓名：[",
						modelMem.MemName,
						"],订单号：[",
						strOrderAccount,
						"],消费金额：[",
						dclDiscountMoney,
						"],获得积分：[",
						intPoint,
						"],备注：",
						strRemark
					});
				}
				else
				{
					Remark = string.Concat(new object[]
					{
						"散客商家中心快速消费,订单号：[",
						strOrderAccount,
						"],消费金额：[",
						dclDiscountMoney,
						"],备注：",
						strRemark
					});
				}
				if (PubFunction.IsShopPoint(intUserShopID, ref intPoint) || isMember == 0)
				{
					try
					{
						Chain.Model.OrderLog mdOrderLog = new Chain.Model.OrderLog();
						Chain.Model.SysLog mdSysLog = new Chain.Model.SysLog();
						Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
						if (isMember == 1)
						{
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
							mdOrderLog.OrderPayCoupon = dclCouponPayMoney;
							mdOrderLog.OrderAccount = strOrderAccount;
							mdOrderLog.OrderShopID = intUserShopID;
							mdOrderLog.OrderUserID = intUserID;
							mdOrderLog.OrderRemark = strRemark;
							mdOrderLog.OrderCreateTime = dtNow;
							mdOrderLog.OrderPayType = 0;
							mdOrderLog.UsePoint = 0;
							mdOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
							mdOrderLog.OrderCardPoint = modelMem.MemPoint + intPoint;
							Chain.BLL.OrderLog bllOrder = new Chain.BLL.OrderLog();
							int intSuccess = bllOrder.Add(mdOrderLog, strOrderAccount);
							if (intSuccess > 0)
							{
								decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
								modelMem.MemConsumeMoney += mdOrderLog.OrderDiscountMoney;
								modelMem.MemPoint += mdOrderLog.OrderPoint;
								modelMem.MemConsumeLastTime = DateTime.Now;
								modelMem.MemConsumeCount++;
								int mem = bllMem.ExpenseUpdateMem(intMemID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
								Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
								moneyChangeLogModel.MoneyChangeMemID = intMemID;
								moneyChangeLogModel.MoneyChangeUserID = intUserID;
								moneyChangeLogModel.MoneyChangeType = 3;
								moneyChangeLogModel.MoneyChangeAccount = strOrderAccount;
								moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
								moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
								moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
								moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
								moneyChangeLogModel.MemMoney = modelMem.MemMoney - dclCardPayMoney;
								moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
								moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
								new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
								bllPoint.Add(new Chain.Model.PointLog
								{
									PointMemID = intMemID,
									PointNumber = intPoint,
									PointChangeType = 2,
									PointRemark = string.Concat(new object[]
									{
										"会员快速消费，消费金额：[",
										dclDiscountMoney,
										"],获得积分：[",
										intPoint,
										"]"
									}),
									PointShopID = intUserShopID,
									PointCreateTime = DateTime.Now,
									PointUserID = intUserID,
									PointOrderCode = strOrderAccount
								});
								if (PubFunction.curParameter.bolShopPointManage)
								{
									PubFunction.SetShopPoint(intUserID, intUserShopID, intPoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderAccount,
										"],会员快速消费金额：[",
										dclDiscountMoney,
										"],扣除商家积分：[",
										intPoint,
										"]"
									}), 2);
								}
								MEMPointUpdate.MEMPointRate(modelMem, mdOrderLog.OrderPoint, mdOrderLog.OrderAccount, 2, intUserID, intUserShopID);
								Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
								Chain.Model.SysShop modelShop = bllSysShop.GetModel(intUserShopID);
								decimal flTotalRate = decimal.Parse(Convert.ToDouble(modelShop.TotalRate).ToString("#0.00"));
								if (flTotalRate > 0m)
								{
									int flTotalPoint = (int)(flTotalRate * intPoint);
									decimal alliancePercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.AllianceRebatePercent).ToString("#0.00"));
									decimal cardShopPercent = decimal.Parse(Convert.ToDouble(PubFunction.curParameter.CardShopRebatePercent).ToString("#0.00"));
									int alliancePoint = (int)(flTotalPoint * alliancePercent);
									int cardShopPoint = (int)(flTotalPoint * cardShopPercent);
									int zbPoint = flTotalPoint - alliancePoint - cardShopPoint;
									Chain.BLL.ReturnPointLog bllReturnPoint = new Chain.BLL.ReturnPointLog();
									bllReturnPoint.Add(new Chain.Model.ReturnPointLog
									{
										OrderAccount = strOrderAccount,
										MemID = intMemID,
										TotalPoint = flTotalPoint,
										AlliancePoint = alliancePoint,
										ZbPoint = zbPoint,
										CardShopPoint = cardShopPoint,
										Remark = "会员快速消费,商家返利积分",
										ReturnShopID = intUserShopID,
										CreateTime = DateTime.Now
									});
									PubFunction.SetShopPoint(intUserID, intUserShopID, intUserShopID, flTotalPoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderAccount,
										"],会员快速消费得积分：[",
										intPoint,
										"],返利总比例：[",
										flTotalRate,
										"],商家扣除返利总积分：[",
										flTotalPoint,
										"]"
									}), 2);
									PubFunction.SetShopPoint(intUserID, intUserShopID, modelShop.FatherShopID, -alliancePoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderAccount,
										"],商家总返利积分：[",
										flTotalPoint,
										"],联盟商返利比例：[",
										alliancePercent,
										"],联盟商得到返利积分：[",
										alliancePoint,
										"]"
									}), 3);
									PubFunction.SetShopPoint(intUserID, intUserShopID, modelMem.MemShopID, -cardShopPoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderAccount,
										"],商家总返利积分：[",
										flTotalPoint,
										"],开卡商家返利比例：[",
										cardShopPercent,
										"],开卡商家得到返利积分：[",
										cardShopPoint,
										"]"
									}), 3);
									PubFunction.SetShopPoint(intUserID, intUserShopID, 1, -zbPoint, string.Concat(new object[]
									{
										"单号：[",
										strOrderAccount,
										"],商家总返利积分：[",
										flTotalPoint,
										"],运营商得到返利积分：[",
										zbPoint,
										"]"
									}), 3);
								}
								modelMem = new Chain.BLL.Mem().GetModel(intMemID);
								string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
								flag = string.Concat(new object[]
								{
									"{\"Success\":\"",
									intSuccess,
									"\",\"strUpdateMemLevel\":\"",
									strUpdateMemLevel,
									"\",\"point\":",
									intPoint.ToString(),
									"}"
								});
								if (modelMem.MemMobile != "")
								{
									if (sendMSM)
									{
										if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
										{
											flag = "-2";
										}
										else if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
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
											smsTemplateParameter.OldLevelID = intLevelID;
											modelMem = new Chain.BLL.Mem().GetModel(intMemID);
											smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
											smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
											smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
											string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, intUserShopID);
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
											PubFunction.SaveSysLog(intUserID, 4, "会员消费-快速消费", string.Concat(new string[]
											{
												"快速消费发短信成功,会员卡号：[",
												modelMem.MemCard,
												"],姓名：[",
												modelMem.MemName,
												"]"
											}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
										}
										else
										{
											flag = "-5";
										}
									}
								}
								PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
							}
						}
						else
						{
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
							mdOrderLog.OrderPayCoupon = dclCouponPayMoney;
							mdOrderLog.OrderAccount = strOrderAccount;
							mdOrderLog.OrderShopID = intUserShopID;
							mdOrderLog.OrderUserID = intUserID;
							mdOrderLog.OrderRemark = strRemark;
							mdOrderLog.OrderPayType = 0;
							mdOrderLog.OrderCreateTime = dtNow;
							Chain.BLL.OrderLog bllOrder = new Chain.BLL.OrderLog();
							int intSuccess = bllOrder.Add(mdOrderLog);
							if (intSuccess > 0)
							{
								PubFunction.SaveSysLog(intUserID, 4, "会员管理", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
								flag = string.Concat(new object[]
								{
									"{\"Success\":\"",
									intSuccess,
									"\",\"strUpdateMemLevel\":\"\",\"point\":",
									intPoint.ToString(),
									"}"
								});
							}
						}
					}
					catch (Exception e)
					{
						this.LogError(e);
						flag = "-1";
					}
				}
				else
				{
					flag = "-6";
				}
				this.Context.Response.Write(flag);
			}
		}

		public void GetWx_MemCountProductList()
		{
			string msgResponse = "";
			try
			{
				Chain.BLL.MemCountDetail bllCountDetail = new Chain.BLL.MemCountDetail();
				int intMemID = int.Parse(this.Request["memID"]);
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append("  Number>0 and  CountDetailMemID= " + intMemID);
			}
			catch (Exception e)
			{
				this.LogError(e);
			}
			this.Context.Response.Write(msgResponse);
		}

		public void WX_CountExpense()
		{
			int flag = 0;
			string result = "";
			try
			{
				Chain.BLL.MemCountDetail bllCountDetail = new Chain.BLL.MemCountDetail();
				Chain.Model.OrderLog modelOrderLog = new Chain.Model.OrderLog();
				int intUserID = int.Parse(this.Request["userid"].ToString());
				int intUserShopID = int.Parse(this.Request["shopid"].ToString());
				int memID = int.Parse(this.Request["memID"].ToString());
				decimal dclDiscountMoney = 0m;
				decimal dclCardPayMoney = 0m;
				decimal dclCashPayMoney = 0m;
				decimal dclBinkPayMoney = 0m;
				decimal dclCouponPayMoney = 0m;
				decimal dclTotalMoney = 0m;
				int intPoint = 0;
				int intNumber = int.Parse(this.Request["number"]);
				string strOrderCode = this.Request["OrderAccount"].ToString();
				string strRemark = "";
				strRemark = PubFunction.RemoveSpace(strRemark);
				int intCount = int.Parse(this.Request["count"]);
				bool sendSMS = false;
				DateTime dtExTime = DateTime.Now;
				bool bolIsCard = false;
				bool bolIsCash = false;
				bool bolIsBink = false;
				Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
				Chain.Model.OrderDetail modelDetail = new Chain.Model.OrderDetail();
				Chain.BLL.OrderDetail bllDetail = new Chain.BLL.OrderDetail();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				int intLevelID = modelMem.MemLevelID;
				if (memID == 0 || modelMem == null)
				{
					flag = -6;
				}
				int intOrderLogAdd = 0;
				modelOrderLog.OrderAccount = strOrderCode;
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
				modelOrderLog.OrderPayCoupon = dclCouponPayMoney;
				modelOrderLog.OrderPoint = intPoint;
				modelOrderLog.OrderRemark = strRemark;
				modelOrderLog.OrderPayType = 0;
				modelOrderLog.OrderShopID = intUserShopID;
				modelOrderLog.OrderUserID = intUserID;
				modelOrderLog.OrderCreateTime = DateTime.Now;
				modelOrderLog.OldAccount = "";
				modelOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
				modelOrderLog.OrderType = 7;
				List<string> _listarray = new List<string>();
				Chain.BLL.Goods goodsbll = new Chain.BLL.Goods();
				Chain.Model.Goods goodsmodel = new Chain.Model.Goods();
				if (bllOrderLog.ExistsOrderAccount(strOrderCode))
				{
					flag = -4;
				}
				else
				{
					intOrderLogAdd = bllOrderLog.Add(modelOrderLog, strOrderCode);
				}
				if (flag >= 0 && intOrderLogAdd > 0)
				{
					int intDetailAdd = 0;
					for (int i = 0; i < intCount; i++)
					{
						modelDetail.OrderID = intOrderLogAdd;
						modelDetail.GoodsID = int.Parse(this.Request["data[" + i + "][id]"]);
						goodsmodel = goodsbll.GetModel(modelDetail.GoodsID);
						modelDetail.OrderDetailPrice = 0m;
						modelDetail.OrderDetailDiscountPrice = 0m;
						modelDetail.OrderDetailNumber = -int.Parse(this.Request["data[" + i + "][count]"]);
						modelDetail.OrderDetailPoint = 0;
						modelDetail.OrderDetailType = 1;
						intDetailAdd = bllDetail.Add(modelDetail);
						int orderItemCount = 0;
						int sySumCount = 0;
						if (modelDetail.OrderDetailType == 1)
						{
							DataTable dtCount = bllCountDetail.GetList(-1, string.Concat(new object[]
							{
								" CountDetailMemID=",
								memID,
								" and CountDetailGoodsID=",
								modelDetail.GoodsID,
								" and CountDetailNumber>0"
							}), "CountCreateTime ASC").Tables[0];
							int orderCount = Math.Abs(int.Parse(modelDetail.OrderDetailNumber.ToString()));
							sySumCount = int.Parse(dtCount.Compute("Sum(CountDetailNumber)", "").ToString());
							foreach (DataRow drCount in dtCount.Rows)
							{
								if (orderCount != 0)
								{
									int detailNumber = int.Parse(drCount["CountDetailNumber"].ToString());
									if (detailNumber > orderCount)
									{
										bllCountDetail.UpdateCountDetailNumber(orderCount, int.Parse(drCount["CountDetailID"].ToString()));
										orderItemCount += orderCount;
										orderCount = 0;
									}
									else
									{
										bllCountDetail.UpdateCountDetailNumber(detailNumber, int.Parse(drCount["CountDetailID"].ToString()));
										orderCount -= detailNumber;
										orderItemCount += orderCount;
									}
								}
							}
						}
						_listarray.Add(string.Concat(new object[]
						{
							goodsmodel.Name,
							modelDetail.OrderDetailNumber,
							"次|余",
							sySumCount - orderItemCount,
							"次"
						}));
						orderItemCount = 0;
					}
					if (intDetailAdd > 0 && memID != 0)
					{
						result = "{\"Success\":\"" + intOrderLogAdd + "\",\"strUpdateMemLevel\":\"\"}";
						string Remark = string.Concat(new string[]
						{
							"会员计次消费,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],订单号：[",
							strOrderCode,
							"],备注：",
							strRemark
						});
						PubFunction.SaveSysLog(intUserID, 4, "计次消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						if (sendSMS)
						{
							if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
							{
								flag = -2;
							}
							else if (modelMem.MemMobile != "")
							{
								if (PubFunction.IsCanSendSms(this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
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
									foreach (string str in _listarray)
									{
										SmsTemplateParameter expr_70B = smsTemplateParameter;
										expr_70B.CountItemsString = expr_70B.CountItemsString + str + "、";
									}
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
										PubFunction.SetShopSms(intUserID, this.UserModel.UserShopID, modelMem.MemMobile.Split(new char[]
										{
											','
										}).Length, 2);
									}
								}
								else
								{
									flag = -5;
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				this.LogError(e);
				flag = -1;
			}
			this.Context.Response.Write((flag < 0) ? flag.ToString() : result);
		}

		public void Wx_UserLoginOut()
		{
			int flag = -1;
			try
			{
				this.Session["userid"] = null;
				flag = 1;
			}
			catch
			{
				flag = -1;
			}
			this.Response.Write(flag);
		}
	}
}
