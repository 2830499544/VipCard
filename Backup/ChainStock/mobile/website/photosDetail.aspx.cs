using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class photosDetail : Page
	{
		protected HtmlInputHidden txtAlbumID;

		protected Repeater rptPhoto;

		protected HtmlAnchor morePhoto;

		protected Repeater rptPhotoDetail;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				Photo bllPhoto = new Photo();
				int AlbumID = 0;
				if (base.Request.QueryString["AlbumID"] != null)
				{
					AlbumID = int.Parse(base.Request.QueryString["AlbumID"]);
				}
				this.txtAlbumID.Value = AlbumID.ToString();
				DataTable dt;
				if (base.Request.QueryString["type"] == "all")
				{
					dt = bllPhoto.GetList(" AlbumID=" + AlbumID).Tables[0];
				}
				else
				{
					dt = bllPhoto.GetList(30, " AlbumID=" + AlbumID, "PhotoCreateTime").Tables[0];
				}
				this.rptPhoto.DataSource = dt;
				this.rptPhoto.DataBind();
				this.rptPhotoDetail.DataSource = dt;
				this.rptPhotoDetail.DataBind();
				if (dt.Rows.Count < 30 || base.Request.QueryString["type"] == "all")
				{
					this.morePhoto.Attributes.Add("style", "display:none");
				}
			}
		}
	}
}
