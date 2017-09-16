using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class Opinion : PageBase
	{
		protected HtmlForm frmOpinion;

		protected Literal ltlTitle;

		protected Button btnOpinionSearch;

		protected Button btnOpinionExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtStaffStartTime;

		protected HtmlInputText txtStaffEndTime;

		protected Repeater gvOpinion;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		private Chain.BLL.OnlineMessage ProposalBll = new Chain.BLL.OnlineMessage();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtStaffStartTime.Value = DateTime.Now.ToString("yyyy-MM") + "-01";
				this.txtStaffEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void Get_ParameterList(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = this.ProposalBll.GetProposalInfo(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvOpinion.DataSource = db;
			this.gvOpinion.DataBind();
			PageBase.BindSerialRepeater(this.gvOpinion, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		public string GetMemName(object memid)
		{
			string result = "";
			int MemID = int.Parse(memid.ToString());
			if (MemID == 0)
			{
				result = "游客";
			}
			else
			{
				Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
				if (modelMem != null)
				{
					result = modelMem.MemName;
				}
			}
			return result;
		}

		private string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value.Trim();
			string startTime = this.txtStaffStartTime.Value;
			string endTime = this.txtStaffEndTime.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" MessageType=0 ");
			if (!string.IsNullOrEmpty(strQueryMem))
			{
				strSql.AppendFormat("and (MemCard ='{0}' ) ", strQueryMem);
			}
			if (!string.IsNullOrEmpty(startTime))
			{
				strSql.AppendFormat("and MessageTime>='{0}' ", startTime);
			}
			if (!string.IsNullOrEmpty(endTime))
			{
				strSql.AppendFormat("and MessageTime<='{0}' ", Convert.ToDateTime(endTime).AddDays(1.0).ToString());
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnOpinionSearch_Click(object sender, EventArgs e)
		{
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnOpinionExcel_Click(object sender, EventArgs e)
		{
			string strSql = this.QueryCondition();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and OnlineMessage.MemID=Mem.MemID and Mem.MemShopID=SysShop.ShopID";
			DataTable db = this.ProposalBll.GetProposalInfo(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MoneyOpinionExcel(db, this._UserName);
		}
	}
}
