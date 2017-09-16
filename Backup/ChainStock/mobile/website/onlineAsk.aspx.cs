using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class onlineAsk : Page
	{
		protected Repeater rptOnlinAsk;

		protected HtmlTextArea txtMessage;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtMemCard;

		private Chain.BLL.OnlineMessage bllProposal = new Chain.BLL.OnlineMessage();

		private static char[] constant = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z'
		};

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				int MemID = 0;
				string MemCard;
				if (this.Session["MemID"] != null)
				{
					MemID = int.Parse(this.Session["MemID"].ToString());
					Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
					MemCard = modelMem.MemCard;
				}
				else if (this.Session["MemCard"] != null)
				{
					MemCard = this.Session["MemCard"].ToString();
				}
				else
				{
					MemCard = "wxid_" + onlineAsk.GenerateRandom(10);
					this.Session["MemCard"] = MemCard;
				}
				this.txtMemID.Value = MemID.ToString();
				this.txtMemCard.Value = MemCard;
				this.rptOnlinAskBind(MemCard);
				this.bllProposal.UpdateShowStatus(MemCard);
			}
		}

		protected void rptOnlinAsk_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				HtmlGenericControl divReply = (HtmlGenericControl)e.Item.FindControl("divReply");
				HtmlGenericControl divMessage = (HtmlGenericControl)e.Item.FindControl("divMessage");
				if (dr["MessageType"].ToString() == "0")
				{
					divReply.Attributes.Add("style", "display:none");
				}
				else
				{
					divMessage.Attributes.Add("style", "display:none");
				}
			}
		}

		private void rptOnlinAskBind(string MemCard)
		{
			DataTable dt = this.bllProposal.GetList("MemCard='" + MemCard + "'  order by MessageTime asc").Tables[0];
			this.rptOnlinAsk.DataSource = dt;
			this.rptOnlinAsk.DataBind();
		}

		public static string GenerateRandom(int Length)
		{
			StringBuilder newRandom = new StringBuilder(36);
			Random rd = new Random();
			for (int i = 0; i < Length; i++)
			{
				newRandom.Append(onlineAsk.constant[rd.Next(36)]);
			}
			return newRandom.ToString();
		}
	}
}
