using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class UserInfo : PageBase
	{
		private Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();

		private Chain.Model.SysShop modelShop = new Chain.Model.SysShop();

		private Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();

		private Chain.Model.SysUser modelUser = new Chain.Model.SysUser();

		private Chain.BLL.SysGroup bllGroup = new Chain.BLL.SysGroup();

		private Chain.Model.SysGroup modelGroup = new Chain.Model.SysGroup();

		protected HtmlForm frmUserInfo;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected HtmlInputText txtUserAccount;

		protected HtmlInputText txtUserName;

		protected HtmlInputHidden txtUserID;

		protected HtmlInputPassword txtPwd;

		protected HtmlInputHidden txtPassword;

		protected HtmlInputPassword txtRepwd;

		protected HtmlInputText txtUserTel;

		protected HtmlSelect sltShop;

		protected HtmlInputText txtShopName;

		protected HtmlSelect sltGroupID;

		protected HtmlInputText txtGroupName;

		protected HtmlInputRadioButton radChooseYes;

		protected HtmlInputRadioButton radChooseNo;

		protected HtmlTextArea txtUserRemark;

		protected HiddenField HidUid;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, false);
				PubFunction.BindAuthoritySelelct(this._UserShopID, this._UserGroupID, this.sltGroupID, false);
			}
		}
	}
}
