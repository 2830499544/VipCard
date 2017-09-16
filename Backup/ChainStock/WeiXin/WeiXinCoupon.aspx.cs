using Chain.BLL;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.WeiXin
{
	public class WeiXinCoupon : Page
	{
		protected Repeater Rpt_WeiXinCoupon;

		protected HtmlGenericControl noProduct;

		protected string MemWeiXinCard = string.Empty;

		protected string rc = PubFunction.GetDataTimeString();

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			string CouponID = base.Request["CouponID"];
			if (!string.IsNullOrEmpty(this.MemWeiXinCard))
			{
				this.CouPonDataBind();
				if (!string.IsNullOrEmpty(CouponID))
				{
					this.SendCoupon();
				}
			}
		}

		protected void Rpt_WeiXinCoupon_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView row = (DataRowView)e.Item.DataItem;
				string CouponType = row["CouponType"].ToString();
				string CouponStart = row["CouponStart"].ToString();
				string CouponEnd = row["CouponEnd"].ToString();
				string CouponMinMoney = row["CouponMinMoney"].ToString();
				string CouponNumber = row["CouponNumber"].ToString();
				string ID = row["ID"].ToString();
				HtmlGenericControl spDesc = (HtmlGenericControl)e.Item.FindControl("spDesc");
				HtmlGenericControl spTime = (HtmlGenericControl)e.Item.FindControl("spTime");
				if (CouponType == "0")
				{
					if (CouponStart == "")
					{
						spTime.InnerText = "永久有效";
						spTime.Style.Add("color", "Red");
					}
					else
					{
						spTime.InnerText = DateTime.Parse(CouponStart).ToString("yyyy-MM-dd") + "至" + DateTime.Parse(CouponEnd).ToString("yyyy-MM-dd");
					}
					if (CouponMinMoney == "0")
					{
						spDesc.InnerText = "优惠￥" + decimal.Parse(CouponNumber).ToString("F2");
					}
					else
					{
						spDesc.InnerText = "最低消费￥" + decimal.Parse(CouponMinMoney).ToString("F2") + "可优惠￥" + decimal.Parse(CouponNumber).ToString("F2");
					}
				}
				else if (CouponType == "1")
				{
					if (CouponStart == "")
					{
						spTime.InnerText = "永久有效";
						spTime.Style.Add("color", "Red");
					}
					else
					{
						spTime.InnerText = DateTime.Parse(CouponStart).ToString("yyyy-MM-dd") + "至" + DateTime.Parse(CouponEnd).ToString("yyyy-MM-dd");
					}
					spDesc.InnerText = string.Concat(new string[]
					{
						"最低消费￥",
						decimal.Parse(CouponMinMoney).ToString("F2"),
						"可打",
						CouponNumber,
						"折"
					});
				}
				Chain.Model.Mem mem = new Chain.BLL.Mem().GetMemByWeiXinCard(this.MemWeiXinCard);
				if (mem != null)
				{
					int MemID = mem.MemID;
					StringBuilder strWhere = new StringBuilder();
					strWhere.Append("CouPonID = " + ID);
					strWhere.Append(" and CouPonMID = " + MemID);
					strWhere.Append(" and CouPonSY = 0 ");
					DataTable dt = new Chain.BLL.CouponList().GetList(1, strWhere.ToString(), "CID").Tables[0];
					HtmlGenericControl spCouPon = (HtmlGenericControl)e.Item.FindControl("spCouPon");
					if (dt.Rows.Count > 0)
					{
						spCouPon.InnerHtml = "优惠券号：<span style='color:Red'>" + dt.Rows[0]["CouPon"].ToString() + "</span>";
					}
					else
					{
						int CouponPredictNu = int.Parse(row["CouponPredictNu"].ToString());
						int CouponYF = int.Parse(row["CouponYF"].ToString());
						if (CouponYF >= CouponPredictNu)
						{
							spCouPon.InnerText = "该优惠券已发完";
							spCouPon.Style.Add("color", "Red");
						}
						else
						{
							spCouPon.InnerHtml = "<span style='color:Red'>点击申请</span>";
							HtmlAnchor application = (HtmlAnchor)e.Item.FindControl("application");
							application.HRef = "WeiXinCoupon.aspx?MemWeiXinCard=" + this.MemWeiXinCard + "&CouPonID=" + ID;
						}
					}
				}
			}
		}

		protected void CouPonDataBind()
		{
			DataTable dt = new Chain.BLL.Coupon().GetList("CouponEnd >= getDate() or CouponStart is null").Tables[0];
			if (dt.Rows.Count > 0)
			{
				this.Rpt_WeiXinCoupon.DataSource = dt;
				this.Rpt_WeiXinCoupon.DataBind();
			}
			else
			{
				this.noProduct.Style.Add("display", "\"\"");
			}
		}

		protected void SendCoupon()
		{
			int flag = 0;
			try
			{
				string strMemID = new Chain.BLL.Mem().GetMemByWeiXinCard(this.MemWeiXinCard).MemID.ToString();
				int intNumber = 1;
				int intCouponID = int.Parse(base.Request["CouponID"].ToString());
				Chain.BLL.Coupon bllCoupon = new Chain.BLL.Coupon();
				Chain.Model.Coupon modelCoupon = new Chain.Model.Coupon();
				modelCoupon = bllCoupon.GetModel(intCouponID);
				if (modelCoupon.CouponPredictNu - modelCoupon.CouponYF < intNumber)
				{
					flag = 1;
				}
				else
				{
					Chain.BLL.CouponList bllCouponList = new Chain.BLL.CouponList();
					DataRow row = bllCouponList.GetList(intNumber, " CouPonID=" + intCouponID + " and CouPonYF='False' ", " CID ").Tables[0].Rows[0];
					string strSql = string.Concat(new string[]
					{
						"update CouponList set CouPonYF='True',CouPonMID=",
						strMemID,
						",ConPonSendTime='",
						DateTime.Now.ToString(),
						"'"
					});
					object obj = strSql;
					strSql = string.Concat(new object[]
					{
						obj,
						" where Coupon = '",
						row["CouPon"],
						"'"
					});
					if (bllCouponList.DataUpdateTran(new ArrayList
					{
						strSql
					}))
					{
						modelCoupon.CouponYF += intNumber;
						bllCoupon.Update(modelCoupon);
					}
					flag = 2;
				}
			}
			catch
			{
				flag = -1;
			}
			finally
			{
				base.Response.Redirect(string.Concat(new object[]
				{
					"WeiXinCoupon.aspx?MemWeiXinCard=",
					this.MemWeiXinCard,
					"&flag=",
					flag
				}));
			}
		}

		protected void ShowMsg()
		{
		}
	}
}
