using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Gift
{
	public class GiftList : PageBase
	{
		protected HtmlForm frmGiftList;

		protected Literal ltlTitle;

		protected HtmlInputText txtGiftName;

		protected HtmlInputHidden txtGiftID;

		protected HtmlInputText txtGiftCode;

		protected HtmlSelect sltGiftClass;

		protected HtmlInputText txtGiftStockNumber;

		protected HtmlInputHidden txtGiftExchangeNumber;

		protected HtmlInputText txtGiftExchangePoint;

		protected HtmlTextArea txtGiftRemark;

		protected HtmlImage imgGiftPhoto;

		protected HtmlInputHidden txtGiftPhoto;

		protected HtmlInputButton btnGiftAdd;

		protected Repeater gvwGiftList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindGiftClass(this.sltGiftClass);
				this.BindGiftList();
			}
		}

		private void BindGiftList()
		{
			

            Chain.BLL.PointGift PointGiftBll = new Chain.BLL.PointGift();

			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = string.Empty;
			if (PubFunction.curParameter.bolGiftShare)
			{
				strSql = "PointGift.GiftClassID=GiftClass.GiftClassID and PointGift.GiftShopID=SysShop.ShopID ";
			}
			else
			{
				strSql = "PointGift.GiftShopID=SysShop.ShopID and PointGift.GiftClassID=GiftClass.GiftClassID and GiftShopID=" + this._UserShopID;
			}
            DataTable db = PointGiftBll.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, true, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvwGiftList.DataSource = db;
			this.gvwGiftList.DataBind();
			PageBase.BindSerialRepeater(this.gvwGiftList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected string GetPhoto(string strPhoto)
		{
			string result;
			if (strPhoto == "")
			{
				result = "../images/Gift/nogift.jpg";
			}
			else
			{
				result = strPhoto;
			}
			return result;
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindGiftList();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindGiftList();
		}

		protected void btnShopSelect_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindGiftList();
		}
	}
}
