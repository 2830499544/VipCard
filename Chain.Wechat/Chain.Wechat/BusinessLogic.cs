using Chain.BLL;
using Chain.Common.DEncrypt;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
using Web.Common;

namespace Chain.Wechat
{
	public class BusinessLogic
	{
		public string GetWelcomeText()
		{
			StringBuilder temp = new StringBuilder();
			Chain.Model.SysParameter modelSysPara = new Chain.BLL.SysParameter().GetModel(1);
			Chain.BLL.WeiXinRule bll = new Chain.BLL.WeiXinRule();
			temp.Append("\n" + modelSysPara.WeiXinSalutatory + "\n");
			temp.Append("回复\"会员\" 申请/出示会员卡\n");
			temp.Append("回复\"绑定\" 已有会员转微信会员卡\n");
			temp.Append("回复\"签到\" 进行会员签到并获得积分\n");
			DataTable dt = bll.Attention();
			foreach (DataRow row in dt.Rows)
			{
				temp.Append(row["RuleDesc"].ToString() + "\n");
			}
			return temp.ToString();
		}

		public string GetMemCardResponse(IRequest req, Chain.Model.Mem mem, string title)
		{
			this.DrawImage(mem);
			Chain.Model.SysParameter modelSysPara = new Chain.BLL.SysParameter().GetModel(1);
			if (string.IsNullOrEmpty(title))
			{
				title = string.Format("您的{0}微信会员卡", modelSysPara.WeiXinShopName);
			}
			string imageUrl = string.Format("http://{0}/Upload/WeiXin/Images/{1}.jpg?random={2}", modelSysPara.SystemDomain, mem.MemWeiXinCard, Guid.NewGuid());
//			string url = string.Format("http://{0}/ReceptionPage/index.aspx?MemWeiXinCard={1}&random={2}", modelSysPara.SystemDomain, mem.MemWeiXinCard, Guid.NewGuid());
            string url = string.Format("http://{0}/mobile/member/index.aspx", modelSysPara.SystemDomain);
			NewsResponseItem item = new NewsResponseItem(title, "", imageUrl, url);
			return new NewsResponse(req, new List<NewsResponseItem>
			{
				item
			}).Result();
		}

		public string GetResponseByRule(IRequest req, string content)
		{
			Chain.BLL.WeiXinRule bllRule = new Chain.BLL.WeiXinRule();
			Chain.Model.WeiXinRule ruleModel = bllRule.GetModelByNewsRuleID(content);
			string result;
			if (ruleModel != null)
			{
				if (ruleModel.RuleNewsType == "text")
				{
					result = new TextResponse(req, ruleModel.RuleContent).Result();
					return result;
				}
				if (ruleModel.RuleNewsType == "news")
				{
					List<Chain.Model.WeiXinNews> newsList = new Chain.BLL.WeiXinNews().GetModelList("NewsRuleID=" + ruleModel.RuleID);
					List<NewsResponseItem> list = new List<NewsResponseItem>();
					foreach (Chain.Model.WeiXinNews newsRule in newsList)
					{
						NewsResponseItem item = new NewsResponseItem(newsRule.NewsTitle, newsRule.NewsDesc, newsRule.NewsUrlFirst, newsRule.NewsUrlSecond);
						list.Add(item);
					}
					result = new NewsResponse(req, list).Result();
					return result;
				}
			}
			result = string.Empty;
			return result;
		}

		public int MemRegister(string mobile, string openID)
		{
			Chain.Model.Mem model = new Chain.Model.Mem();
			model.MemCard = mobile;
			string pwd = mobile.Substring(5, 6);
			model.MemPassword = DESEncrypt.Encrypt(pwd);
			model.MemName = "";
			model.MemSex = true;
			model.MemIdentityCard = "";
			model.MemMobile = mobile;
			model.MemPhoto = "";
			model.MemBirthdayType = true;
			model.MemBirthday = Convert.ToDateTime("1900-1-1 0:00:00");
			model.MemIsPast = false;
			model.MemPastTime = Convert.ToDateTime("2900-1-1 0:00:00");
			model.MemPoint = 0;
			model.MemPointAutomatic = true;
			model.MemMoney = 0m;
			model.MemEmail = "";
			model.MemAddress = "";
			model.MemState = 0;
			model.MemRecommendID = 0;
			model.MemLevelID = 0;
			Chain.Model.MicroWebsiteSceneStr websitescenestr = new Chain.Model.MicroWebsiteSceneStr();
			Chain.BLL.MicroWebsiteSceneStr bllwebsitescenestr = new Chain.BLL.MicroWebsiteSceneStr();
			websitescenestr = bllwebsitescenestr.GetModel(openID);
			if (websitescenestr != null)
			{
				model.MemShopID = websitescenestr.SceneStr;
			}
			else
			{
				model.MemShopID = 1;
			}
			model.MemWeiXinCards = openID;
			model.MemAttention = 1;
			model.MemCreateTime = DateTime.Now;
			model.MemRemark = "";
			model.MemUserID = 1;
			model.MemTelePhone = "";
			model.MemWeiXinCard = openID;
			model.MemQRCode = "";
			model.MemProvince = "";
			model.MemCity = "";
			model.MemCounty = "";
			model.MemVillage = "";
			return new Chain.BLL.Mem().Add(model);
		}

		public int MemSign(Chain.Model.Mem member)
		{
			Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
			Chain.Model.SysParameter modelSysPara = new Chain.BLL.SysParameter().GetModel(1);
			int result;
			if (member == null)
			{
				result = -1;
			}
			else if (modelSysPara.SignInPoint <= 0)
			{
				result = -2;
			}
			else if (bllPoint.IsSignedToday(member.MemID))
			{
				result = -3;
			}
			else
			{
				bllPoint.Add(new Chain.Model.PointLog
				{
					PointMemID = member.MemID,
					PointNumber = modelSysPara.SignInPoint,
					PointChangeType = 16,
					PointRemark = "会员微信签到送积分",
					PointShopID = member.MemShopID,
					PointCreateTime = DateTime.Now,
					PointUserID = member.MemUserID,
					PointOrderCode = ""
				});
				Chain.BLL.SysParameter bllSysParameter = new Chain.BLL.SysParameter();
				Chain.Model.SysParameter modelSysParameter = bllSysParameter.GetModel(1);
				if (modelSysParameter.ShopPointManage)
				{
					this.SetShopPoint(member.MemUserID, member.MemShopID, modelSysPara.SignInPoint, string.Format("会员：{0} 微信签到送积分,扣除{1}", member.MemCard, modelSysPara.SignInPoint), 1, true);
				}
				int intSuccess = new Chain.BLL.Mem().UpdatePoint(member.MemID, modelSysPara.SignInPoint);
				result = intSuccess;
			}
			return result;
		}

		public void SetShopPoint(int UserID, int ShopID, int Point, string Remark, int Type, bool isPointSame)
		{
			if (Point != 0)
			{
				if (ShopID > 1)
				{
					Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
					Chain.Model.SysShop mdSysShop = new Chain.Model.SysShop();
					mdSysShop = bllSysShop.GetModel(ShopID);
					if (isPointSame)
					{
						if (mdSysShop.PointType != 1 || Point <= mdSysShop.PointCount)
						{
							mdSysShop.PointCount -= Point;
							bllSysShop.Update(mdSysShop);
							Chain.BLL.SysShopPointLog bllSysShopPointLog = new Chain.BLL.SysShopPointLog();
							bllSysShopPointLog.Add(new Chain.Model.SysShopPointLog
							{
								Count = -1 * Point,
								CreateTime = DateTime.Now,
								OutShopID = ShopID,
								ShopID = ShopID,
								ShopPointAccount = "JF" + DateTime.Now.ToString("yyMMddHHmmssffff"),
								ShopPointType = Type,
								UserID = UserID,
								Remark = Remark
							});
						}
					}
				}
			}
		}

		public void DrawImage(Chain.Model.Mem member)
		{
			this.DrawImage(member, "~/Upload/WeiXin/Images/bg.jpg", "~/Upload/WeiXin/Images/" + member.MemWeiXinCard + ".jpg");
			this.DrawImage(member, "~/Upload/WeiXin/Images/memCard.jpg", "~/Upload/WeiXin/Images/" + member.MemCard + "-MemCard.jpg");
		}

		public void DrawImage(Chain.Model.Mem member, string sourceUrl, string targetUrl)
		{
			Bitmap smallWeiXin = new Bitmap(130, 130);
			Image weixinImg = QRCodeImage.CreateQRCode(member.MemCard);
			Graphics g = Graphics.FromImage(smallWeiXin);
			g.DrawImage(weixinImg, new Point(-35, -35));
			string savePath = HttpContext.Current.Server.MapPath(targetUrl);
			Bitmap bigWeiXin = new Bitmap(200, 200);
			g = Graphics.FromImage(bigWeiXin);
			g.DrawImage(smallWeiXin, new Rectangle(0, 0, 200, 200), new Rectangle(0, 0, 130, 130), GraphicsUnit.Pixel);
			string bg = HttpContext.Current.Server.MapPath(sourceUrl);
			Image bgImg = Image.FromFile(bg, true);
			Bitmap bmp = new Bitmap(bgImg.Width, bgImg.Height, PixelFormat.Format32bppArgb);
			g = Graphics.FromImage(bmp);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.DrawImage(bgImg, new Rectangle(0, 0, bgImg.Width, bgImg.Height), new Rectangle(0, 0, bgImg.Width, bgImg.Height), GraphicsUnit.Pixel);
			g.DrawImage(bigWeiXin, new RectangleF(50f, 60f, 200f, 200f), new RectangleF(0f, 0f, 200f, 200f), GraphicsUnit.Pixel);
			g.DrawString("NO:" + member.MemCard, new Font("微软雅黑", 34f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 70f));
			g.DrawString("积分:" + member.MemPoint, new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 115f));
			g.DrawString("余额:" + member.MemMoney.ToString("F2"), new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 160f));
			string pastStr = (member.MemPastTime.ToString("yyyy-MM-dd") == "2900-01-01") ? "有效期:永久有效" : ("有效期:" + member.MemPastTime.ToString("yyyy-MM-dd"));
			g.DrawString(pastStr, new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 205f));
			bmp.Save(savePath, ImageFormat.Jpeg);
		}
	}
}
