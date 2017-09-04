using Chain.BLL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class rechargeOnline : Page
	{
		protected HtmlGenericControl spMemCard;

		protected HtmlGenericControl spMemName;

		protected HtmlInputHidden txtPointRate;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtMaxMoney;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = MemID.ToString();
					this.BindMemInfo(MemID);
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
				Chain.Model.SysParameter parameter = new Chain.BLL.SysParameter().GetModel(1);
				this.txtMaxMoney.Value = parameter.Xiane.ToString();
			}
		}

		private void BindMemInfo(int MemID)
		{
			Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
			this.spMemCard.InnerHtml = modelMem.MemCard;
			this.spMemName.InnerHtml = modelMem.MemName.ToString();
			List<Chain.Model.SysShopMemLevel> list = new Chain.BLL.SysShopMemLevel().GetModelList(string.Format(" ShopID=1 and MemLeveLID={0} ", modelMem.MemLevelID));
			if (list.Count > 0)
			{
				Chain.Model.MemLevel mdlMemLevel = new Chain.BLL.MemLevel().GetModel(modelMem.MemLevelID);
				if (list[0].ClassRechargePointRate > 0m)
				{
					this.txtPointRate.Value = list[0].ClassRechargePointRate.ToString();
				}
				else
				{
					this.txtPointRate.Value = "0";
				}
			}
		}
	}
}
