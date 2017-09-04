using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class Photo : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmPhotoShow;

		protected Literal ltlTitle;

		protected HtmlSelect sltAlbumID;

		protected HtmlTextArea txtPhotoRemark;

		protected HtmlImage imgPhotoPhoto;

		protected HtmlInputButton btnPhotoAdd;

		protected Repeater rptPhoto;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		private Chain.BLL.Photo SymbolShowBll = new Chain.BLL.Photo();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Get_ParameterList();
				this.BindAlbum(this.sltAlbumID);
			}
		}

		public void BindAlbum(HtmlSelect select)
		{
			select.Items.Clear();
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
			DataTable dtGoodsClass = new Chain.BLL.Album().GetList("").Tables[0];
			foreach (DataRow dr in dtGoodsClass.Rows)
			{
				select.Items.Add(new ListItem(dr["AlbumName"].ToString(), dr["AlbumID"].ToString()));
			}
		}

		private void Get_ParameterList()
		{
			string strSql = " Album.AlbumID=Photo.AlbumID and Photo.CreateUserID=SysUser.UserID";
			if (base.Request.QueryString["AlbumID"] != null)
			{
				strSql = strSql + " and Photo.AlbumID=" + base.Request.QueryString["AlbumID"];
			}
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = this.SymbolShowBll.GetPhotoInfo(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptPhoto.DataSource = db;
			this.rptPhoto.DataBind();
			PageBase.BindSerialRepeater(this.rptPhoto, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList();
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList();
		}
	}
}
