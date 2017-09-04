using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class SysRotateList : PageBase
	{
		private SysRotate PromotionsBll = new SysRotate();

		protected HtmlForm frmPromotions;

		protected Literal ltlTitle;

		protected HtmlSelect sltPromotionsLevel;

		protected Button btnSysRotateAdd;

		protected Repeater gvSysRotateList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindPreferentialObject();
				this.Get_ParameterList();
			}
		}

		protected string BindStatus(object StartTime, object EndTime)
		{
			string result = "";
			DateTime startTime = DateTime.Parse(StartTime.ToString());
			DateTime endTime = DateTime.Parse(EndTime.ToString());
			DateTime currentTime = DateTime.Now;
			if (currentTime >= startTime && currentTime <= endTime)
			{
				result = "正在进行中";
			}
			if (currentTime < startTime)
			{
				result = "<span style='color:gray'>活动未开始</span>";
			}
			if (currentTime > endTime)
			{
				result = "<span style='color:gray'>活动已结束</span>";
			}
			return result;
		}

		protected void btnSysRotateAdd_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("SysRotateInfo.aspx");
		}

		protected int BindCount(object rotateID)
		{
			Chain.BLL.SysRotatePrizeLog bllSysRotatePrizeLog = new Chain.BLL.SysRotatePrizeLog();
			return bllSysRotatePrizeLog.GetRotateCount(int.Parse(rotateID.ToString()));
		}

		private void Get_ParameterList()
		{
			int Counts;
			DataTable dt = this.PromotionsBll.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				"SysRotate.CreateUserID=SysUser.UserID"
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvSysRotateList.DataSource = dt;
			this.gvSysRotateList.DataBind();
			PageBase.BindSerialRepeater(this.gvSysRotateList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		public void BindPreferentialObject()
		{
			DataTable dt = new MemLevel().GetList("").Tables[0];
			this.sltPromotionsLevel.Items.Add(new ListItem("=== 所有会员 ===", "-1"));
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				this.sltPromotionsLevel.Items.Add(new ListItem(dt.Rows[i]["LevelName"].ToString(), dt.Rows[i]["LevelID"].ToString()));
			}
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
