using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class StaffList : PageBase
	{
		protected HtmlForm frmStaffList;

		protected Literal ltlTitle;

		protected HtmlInputText txtStaffNumber;

		protected HtmlInputText txtStaffName;

		protected HtmlInputHidden txtStaffID;

		protected HtmlSelect sltStaffSex;

		protected HtmlInputText txtStaffMobile;

		protected HtmlSelect sltStaffClass;

		protected HtmlInputText txtStaffAddress;

		protected HtmlTextArea txtStaffRemark;

		protected HtmlInputButton btnStaffAdd;

		protected HtmlInputText txtQuery;

		protected HtmlInputButton btnStaffQuery;

		protected HtmlGenericControl StaffList_ClassTree;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				string strTree = this.BindStaff("StaffListTree");
				this.StaffList_ClassTree.InnerHtml = strTree;
				PubFunction.BindStaffClass(this._UserShopID, this.sltStaffClass, true);
			}
		}

		private string BindStaff(string strCallbackFun)
		{
			StringBuilder sbHtml = new StringBuilder();
			DataTable dtStaffClass = new StaffClass().GetList(" SysShop.ShopID=StaffClass.ClassShopID ").Tables[0];
			sbHtml.Append("<ul class=\"mktree\" id=\"treeStaff\">");
			sbHtml.Append("<li><a href=\"javascript:void(0);\" onclick=\"javascript:" + strCallbackFun + "();\" style='font-size:14px;color:#303030;font-weight:bolder;'><img src='/images/ico/open.png'/>&nbsp;所有商家</a></li>");
			if (this._UserShopID == 1)
			{
				sbHtml.Append("<ul>");
				DataTable dtShop = new SysShop().GetList("ShopID>0").Tables[0];
				foreach (DataRow dr in dtShop.Rows)
				{
					sbHtml.Append(string.Concat(new object[]
					{
						"<li><font style='font-size:14px;color:#4f4e4e;'>&nbsp;&nbsp;&nbsp;&nbsp;<a href='#' class='mytrees' onclick='bindtree(",
						dr["ShopID"].ToString(),
						")'><img id='img",
						dr["ShopID"].ToString(),
						"' src='/images/ico/close.png'/></a></font><a style='font-size:14px;color:#4f4e4e;' href=\"javascript:void(0);\" onclick=\"javascript:",
						strCallbackFun,
						"(",
						dr["ShopID"],
						",true);\">&nbsp;商家：",
						dr["ShopName"],
						"</a>"
					}));
					StaffList.CreateStaffClass(sbHtml, dtStaffClass, strCallbackFun, int.Parse(dr["ShopID"].ToString()));
				}
				sbHtml.Append("</ul>");
			}
			else
			{
				StaffList.CreateStaffClass(sbHtml, dtStaffClass, strCallbackFun, this._UserShopID);
			}
			return sbHtml.ToString();
		}

		private static void CreateStaffClass(StringBuilder sbHtml, DataTable dtStaffClass, string strCallbackFun, int intShopID)
		{
			DataRow[] drf = dtStaffClass.Select("ClassShopID=" + intShopID);
			DataRow[] array = drf;
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dr = array[i];
				sbHtml.Append(string.Concat(new object[]
				{
					"<ul class='selected",
					intShopID.ToString(),
					"' style='display:none;'><li style='padding-left:70px;'><img src='/images/ico/dot.gif'/>&nbsp;<a class='mytrees' style='font-size:12px;color:#4f4e4e;' href=\"javascript:void(0);\" onclick=\"javascript:",
					strCallbackFun,
					"(",
					dr["ClassID"],
					",false);\">",
					dr["ClassName"],
					"</a>"
				}));
				sbHtml.Append("</li></ul>");
			}
		}
	}
}
