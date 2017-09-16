using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class Album : PageBase
	{
		private Chain.BLL.Album SymbolShowBll = new Chain.BLL.Album();

		protected HtmlHead Head1;

		protected HtmlForm frmAlbumShow;

		protected Literal ltlTitle;

		protected HtmlTextArea txtAlbumRemark;

		protected HtmlImage imgAlbumPhoto;

		protected HtmlInputButton btnAlbumAdd;

		protected Repeater rptAlbum;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Get_ParameterList();
			}
		}

		private void Get_ParameterList()
		{
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = this.SymbolShowBll.GetAlbumInfo(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				"Album.CreateUserID=SysUser.UserID"
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.rptAlbum.DataSource = db;
			this.rptAlbum.DataBind();
			PageBase.BindSerialRepeater(this.rptAlbum, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
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

		public string GetPhotoCount(object albumID)
		{
			string result = "0";
			if (albumID != null)
			{
				int AlbumID = int.Parse(albumID.ToString());
				Chain.BLL.Photo bllPhoto = new Chain.BLL.Photo();
				result = bllPhoto.GetRecordCount("AlbumID=" + AlbumID).ToString();
			}
			return result;
		}
	}
}
