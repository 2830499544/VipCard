namespace ChainStock.MemExpense
{
    using Chain.BLL;
    using Chain.Model;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class TimeList : PageBase
    {
        protected HtmlInputButton btnAddTiming;
        protected Button btnQueryTiming;
        protected DropDownList drpPageSize;
        protected HtmlForm frmTiemExpense;
        protected Repeater gvTimeExpense;
        protected Label lblPrintFoot;
        protected Label lblPrintTitle;
        protected Literal ltlTitle;
        protected AspNetPager NetPagerParameter;
        protected HtmlInputText txtQueryTiming;

        private void BindTimeExpense(string strWhere)
        {
            Chain.BLL.OrderTime bllTime = new Chain.BLL.OrderTime();
            int Counts = this.NetPagerParameter.RecordCount;
            string strSql = "OrderShopID=" + base._UserShopID;
            strSql = PubFunction.GetShopAuthority(base._UserShopID, "OrderShopID", strSql);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql = strWhere + " and " + strSql;
            }
            DataTable db = bllTime.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[] { strSql }).Tables[0];
            this.NetPagerParameter.RecordCount = Counts;
            this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[] { this.NetPagerParameter.CurrentPageIndex, this.NetPagerParameter.PageCount, this.NetPagerParameter.RecordCount, this.NetPagerParameter.PageSize });
            this.gvTimeExpense.DataSource = db;
            this.gvTimeExpense.DataBind();
        }

        protected void btnQueryTiming_Click(object sender, EventArgs e)
        {
            this.NetPagerParameter.CurrentPageIndex = 1;
            this.BindTimeExpense(this.QueryCondition());
        }

        protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.NetPagerParameter.CurrentPageIndex = 1;
            this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
            this.BindTimeExpense("");
        }

        public string GetState(bool OrderState, decimal OrderPredictTime, DateTime dtStartTime)
        {
            string strresult = "";
            if (OrderState)
            {
                return strresult;
            }
            if (OrderPredictTime <= 0M)
            {
                return strresult;
            }
            int mymin = Convert.ToInt32(OrderPredictTime);
            int minutes = 0;
            TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(dtStartTime));
            minutes = (((ts.Days * 0x18) * 60) + (ts.Hours * 60)) + ts.Minutes;
            if (minutes > mymin)
            {
                return string.Format("<span style='color:red;'>已超时长：{0}分钟</span>", minutes - mymin);
            }
            return string.Format("<span style='color:#4aadef;'>剩余时长：{0}分钟</span>", mymin - minutes);
        }

        protected void gvTimeExpense_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SettleAccounts")
            {
                Chain.BLL.OrderTime bllOrderTime = new Chain.BLL.OrderTime();
                Chain.Model.OrderTime mdOrderTime = new Chain.Model.OrderTime();
                Chain.BLL.MemStorageTiming bllMemStorageTiming = new Chain.BLL.MemStorageTiming();
                Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
                Chain.Model.Mem mdMem = new Chain.Model.Mem();
                int OrderTimeID = Convert.ToInt32(((Literal) e.Item.FindControl("ltOrderTimeID")).Text);
                mdOrderTime = bllOrderTime.GetModel(OrderTimeID);
                mdMem = bllMem.GetModel(mdOrderTime.OrderMemID);
                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(mdOrderTime.OrderMarchTime));
                DataTable dtMemCount = bllMemStorageTiming.GetAllTimeByMem(mdOrderTime.OrderMemID, mdOrderTime.OrderProjectID).Tables[0];
                int minute = ts.Minutes + ((ts.Hours + (ts.Days * 0x18)) * 60);
                int mytime = Convert.ToInt32(dtMemCount.Rows[0]["AllTime"]);
                Chain.Model.Timingrules mdTimingrules = new Chain.BLL.Timingrules().GetModel(mdOrderTime.OrderRulesID);
                int micount = minute / mdTimingrules.RulesInterval;
                int passtime = minute % mdTimingrules.RulesInterval;
                if (micount > 0)
                {
                    if (passtime <= mdTimingrules.RulesExceedTime)
                    {
                        minute -= passtime;
                    }
                    else
                    {
                        minute = (minute - passtime) + mdTimingrules.RulesInterval;
                    }
                }
                else
                {
                    minute = mdTimingrules.RulesInterval;
                }
                if (minute > mytime)
                {
                    base.Response.Redirect(string.Format("Expense.aspx?PID=17&MemCard={0}&IsMem={1}&OrderCode={2}", mdMem.MemCard, (mdOrderTime.OrderMemID > 0) ? "true" : "false", mdOrderTime.OrderTimeCode));
                }
                else
                {
                    PubFunction.SubTractCont(minute, mdOrderTime.OrderMemID, mdOrderTime.OrderProjectID);
                    PubFunction.TimeExpenseEnd(mdOrderTime.OrderTimeCode, base._UserID, DateTime.Now, string.Concat(new object[] { "消费时长：", ts.Hours + (ts.Days * 0x18), "小时 ", ts.Minutes, "分钟 ", ts.Seconds, "秒<br>总共：", minute, "分钟消耗剩余充时时长", minute, "分钟" }));
                    PubFunction.SaveSysLog(base._UserID, 4, "会员消费", string.Concat(new object[] { "会员计时消费:会员卡号", mdMem.MemCard, " 消费时长：", minute, "分钟" }), base._UserShopID, DateTime.Now, PubFunction.ipAdress);
                    string GoPrint = "location.href = location.href;";
                    if (base.curParameter.bolAutoPrint)
                    {
                        GoPrint = string.Format("TimeExpenPrint('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}');", new object[] { base._UserName, mdMem.MemCard, mdMem.MemName, mdMem.MemPoint, ((Literal) e.Item.FindControl("ltProjectName")).Text, minute, mytime - minute, mdOrderTime.OrderTimeCode, DateTime.Now.ToString() });
                    }
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'结算成功',close: function () { " + GoPrint + " }});</script>");
                }
            }
        }

        protected void gvTimeExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(e.Row.Cells[1].Text) == 0)
                {
                    e.Row.Cells[1].Text = "散客消费";
                }
                else
                {
                    e.Row.Cells[1].Text = "会员消费";
                }
                if (bool.Parse(e.Row.Cells[6].Text))
                {
                    e.Row.Cells[6].Text = "消费结束";
                    e.Row.Cells[6].ForeColor = Color.Red;
                    e.Row.Cells[9].Text = "已结算";
                    e.Row.Cells[9].ForeColor = Color.Red;
                }
                else
                {
                    e.Row.Cells[6].Text = "正在消费";
                    e.Row.Cells[7].Text = "";
                }
            }
        }

        protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
        {
            this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
            this.BindTimeExpense("");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindTimeExpense("");
                PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, base._UserShopID);
            }
        }

        protected string QueryCondition()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("1=1");
            string strQueryTiming = this.txtQueryTiming.Value;
            if (strQueryTiming != "")
            {
                strSql.AppendFormat(" and OrderToken = '{0}' ", strQueryTiming);
            }
            return strSql.ToString();
        }
    }
}

