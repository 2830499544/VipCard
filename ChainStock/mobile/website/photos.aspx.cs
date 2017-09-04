using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class photos : Page
	{
		protected Repeater rptAlbum;

		protected HtmlAnchor moreAlbum;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				Album bllAlbum = new Album();
				DataTable dt;
				if (base.Request.QueryString["type"] == "all")
				{
					dt = bllAlbum.GetList(" 1=1 ").Tables[0];
				}
				else
				{
					dt = bllAlbum.GetList(20, " 1=1 ", "AlbumCreateTime").Tables[0];
				}
				this.rptAlbum.DataSource = dt;
				this.rptAlbum.DataBind();
				if (dt.Rows.Count < 20 || base.Request.QueryString["type"] == "all")
				{
					this.moreAlbum.Attributes.Add("style", "display:none");
				}
			}
		}

		public string GetPhotoCount(object albumID)
		{
			string result = "0";
			if (albumID != null)
			{
				int AlbumID = int.Parse(albumID.ToString());
				Photo bllPhoto = new Photo();
				result = bllPhoto.GetRecordCount("AlbumID=" + AlbumID).ToString();
			}
			return result;
		}
	}
}
