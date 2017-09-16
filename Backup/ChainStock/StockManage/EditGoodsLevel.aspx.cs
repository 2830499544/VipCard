using Chain.BLL;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.StockManage
{
	public class EditGoodsLevel : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmGoodsClass;

		protected Literal ltlTitle;

		protected Repeater rptSyncShopList;

		protected HtmlSelect sltGoodsClass;

		protected HtmlGenericControl lblShowSync;

		protected HtmlInputCheckBox chkSyncOtherShop;

		protected HtmlGenericControl lblShowSyncPartial;

		protected HtmlInputCheckBox chkSyncPartialShop;

		protected HtmlSelect sltShop;

		protected HtmlInputText txtClassName;

		protected HtmlInputHidden txtClassID;

		protected HtmlTextArea txtGoodsClassRemark;

		protected Repeater rptLevelList;

		protected Button btnGoodsClassSave;

		private Chain.BLL.GoodsClass bllGoodsClass = new Chain.BLL.GoodsClass();

		private Chain.BLL.GoodsClassDiscount bllGoodsClassDiscount = new Chain.BLL.GoodsClassDiscount();

		private Chain.BLL.MemLevel bllMemLevel = new Chain.BLL.MemLevel();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.bindGoodsClass();
				this.bindSyncShopList();
				this.bindData();
			}
		}

		private void bindGoodsClass()
		{
			DataTable dtClass = this.bllGoodsClass.GetListByShopID(this._UserShopID).Tables[0];
			DataRow[] dtGoodsClass = dtClass.Select("ParentID = 0");
			this.sltGoodsClass.Items.Add(new ListItem("根类别", "0"));
			DataRow[] array = dtGoodsClass;
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dr = array[i];
				this.sltGoodsClass.Items.Add(new ListItem(dr["ClassName"].ToString(), dr["ClassID"].ToString()));
			}
		}

		private void bindSyncShopList()
		{
			Chain.BLL.SysShop bllSS = new Chain.BLL.SysShop();
			string sqlStr = "ShopID>0 and ShopID<>" + this._UserShopID;
			sqlStr = PubFunction.GetShopAuthority(this._UserShopID, "ShopID", sqlStr);
			DataTable dt = bllSS.GetList(sqlStr).Tables[0];
			if (dt.Rows.Count > 0)
			{
				this.rptSyncShopList.DataSource = dt;
				this.rptSyncShopList.DataBind();
			}
			else
			{
				this.lblShowSync.Visible = false;
				this.lblShowSyncPartial.Visible = false;
			}
		}

		private void bindData()
		{
			if (!string.IsNullOrEmpty(base.Request["ParentID"]))
			{
				string ParentID = base.Request["ParentID"].ToString();
				this.sltGoodsClass.Value = ParentID;
			}
			PubFunction.BindShopSelect(this._UserShopID, this.sltShop, this._UserShopID, true);
			DataTable dtMemLevel = this.bllMemLevel.GetList("").Tables[0];
			this.rptLevelList.DataSource = dtMemLevel;
			this.rptLevelList.DataBind();
			if (!string.IsNullOrEmpty(base.Request["ClassID"]))
			{
				this.sltGoodsClass.Attributes.Add("disabled", "disabled");
				this.sltGoodsClass.Attributes.Add("readonly", "true");
				Chain.Model.GoodsClass mdGoodsClass = this.bllGoodsClass.GetModel(Convert.ToInt32(base.Request["ClassID"]));
				this.txtClassName.Value = mdGoodsClass.ClassName;
				this.txtClassID.Value = mdGoodsClass.ClassID.ToString();
				this.txtGoodsClassRemark.Value = mdGoodsClass.ClassRemark;
				this.sltGoodsClass.Value = mdGoodsClass.ParentID.ToString();
				this.sltShop.Value = mdGoodsClass.CreateShopID.ToString();
				DataTable dtGoodsClass = this.bllGoodsClassDiscount.GetList(string.Format(" GoodsClassID = '{0}' and DiscountShopID = '{1}'", base.Request["ClassID"], this._UserShopID)).Tables[0];
				for (int i = 0; i < this.rptLevelList.Items.Count; i++)
				{
					DataRow[] drs = dtGoodsClass.Select(string.Format(" MemLevelID = '{0}'", ((Literal)this.rptLevelList.Items[i].FindControl("ltLevelID")).Text));
					if (drs.Length > 0)
					{
						((HtmlInputText)this.rptLevelList.Items[i].FindControl("txtDiscountPercent")).Value = (Convert.ToDouble(drs[0]["ClassDiscountPercent"]) * 100.0).ToString();
						((HtmlInputText)this.rptLevelList.Items[i].FindControl("txtPointPercent")).Value = ((drs[0]["ClassPointPercent"].ToString() == "0") ? "0" : Convert.ToDecimal(drs[0]["ClassPointPercent"]).ToString());
						((Literal)this.rptLevelList.Items[i].FindControl("ltClassDiscountID")).Text = drs[0]["ClassDiscountID"].ToString();
					}
				}
			}
		}

		protected void btnGoodsClassSave_Click(object sender, EventArgs e)
		{
			Chain.BLL.GoodsClassAuthority bllGCA = new Chain.BLL.GoodsClassAuthority();
			if (string.IsNullOrEmpty(base.Request["ClassID"]))
			{
				Chain.Model.GoodsClass mdGoodsClass = new Chain.Model.GoodsClass();
				mdGoodsClass.ClassName = this.txtClassName.Value;
				mdGoodsClass.ClassRemark = this.txtGoodsClassRemark.Value;
				mdGoodsClass.ParentID = Convert.ToInt32(this.sltGoodsClass.Value);
				mdGoodsClass.CreateShopID = Convert.ToInt32(this.sltShop.Value);
				int flag = this.bllGoodsClass.Add(mdGoodsClass);
				int modelGCA = bllGCA.Add(new Chain.Model.GoodsClassAuthority
				{
					ClassID = flag,
					ShopID = Convert.ToInt32(this.sltShop.Value)
				});
				if (flag > 0 && modelGCA > 0)
				{
					ArrayList sqlArray = new ArrayList();
					string strSqlColumn = "INSERT INTO GoodsClassDiscount (GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) VALUES ('{0}','{1}','{2}','{3}','{4}')";
					string GoodsClassID = flag.ToString();
					mdGoodsClass.ClassID = flag;
					for (int i = 0; i < this.rptLevelList.Items.Count; i++)
					{
						string MemLevelID = ((Literal)this.rptLevelList.Items[i].FindControl("ltLevelID")).Text;
						string ClassDiscountPercent = (Convert.ToDecimal(((HtmlInputText)this.rptLevelList.Items[i].FindControl("txtDiscountPercent")).Value.Trim()) / 100m).ToString();
						string ClassPointPercent = ((HtmlInputText)this.rptLevelList.Items[i].FindControl("txtPointPercent")).Value.Trim();
						ClassPointPercent = ((ClassPointPercent == "0") ? "0" : Convert.ToDecimal(ClassPointPercent).ToString());
						string strSql = string.Format(strSqlColumn, new object[]
						{
							GoodsClassID,
							MemLevelID,
							this._UserShopID,
							ClassDiscountPercent,
							ClassPointPercent
						});
						sqlArray.Add(strSql);
					}
					if (this.bllGoodsClassDiscount.AddList(sqlArray))
					{
						this.lblShowSync.Attributes.Add("style", "display:none");
						this.lblShowSyncPartial.Attributes.Add("style", "display:none");
						this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'保存成功',close: function () { location.href = 'SetGoodsLevel.aspx?PID=73'; }});</script>");
					}
					else
					{
						this.bllGoodsClass.Delete(flag);
						base.OutputWarn("保存失败，请重试！");
					}
				}
				else
				{
					switch (flag)
					{
					case -1:
						base.OutputWarn("类别名称已存在，请重试！");
						break;
					case 0:
						base.OutputWarn("保存失败，请重试！");
						break;
					default:
						base.OutputWarn("保存失败，请重试！");
						break;
					}
				}
			}
			else
			{
				Chain.Model.GoodsClass mdGoodsClass = this.bllGoodsClass.GetModel(Convert.ToInt32(base.Request["ClassID"]));
				mdGoodsClass.ClassName = this.txtClassName.Value;
				mdGoodsClass.ClassRemark = this.txtGoodsClassRemark.Value;
				int flag = this.bllGoodsClass.UpdateByShop(mdGoodsClass);
				if (flag > 0)
				{
					ArrayList sqlArray = new ArrayList();
					string strSqlColumn = "UPDATE GoodsClassDiscount SET ClassDiscountPercent = '{0}',ClassPointPercent = '{1}' WHERE ClassDiscountID = '{2}'";
					string strAllSql = string.Empty;
					string ClassDiscountPercent = string.Empty;
					string ClassPointPercent = string.Empty;
					string ClassDiscountID = string.Empty;
					for (int i = 0; i < this.rptLevelList.Items.Count; i++)
					{
						ClassDiscountID = ((Literal)this.rptLevelList.Items[i].FindControl("ltClassDiscountID")).Text;
						ClassDiscountPercent = (Convert.ToDecimal(((HtmlInputText)this.rptLevelList.Items[i].FindControl("txtDiscountPercent")).Value.Trim()) / 100m).ToString();
						ClassPointPercent = ((HtmlInputText)this.rptLevelList.Items[i].FindControl("txtPointPercent")).Value.Trim();
						ClassPointPercent = ((ClassPointPercent == "0") ? "0" : Convert.ToDecimal(ClassPointPercent).ToString());
						strAllSql = string.Format(strSqlColumn, ClassDiscountPercent, ClassPointPercent, ClassDiscountID);
						sqlArray.Add(strAllSql);
					}
					if (this.bllGoodsClassDiscount.AddList(sqlArray))
					{
						this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'保存成功',close: function () { location.href = 'SetGoodsLevel.aspx?PID=73';  }});</script>");
					}
					else
					{
						base.OutputWarn("保存失败，请重试！");
					}
				}
				else
				{
					switch (flag)
					{
					case -1:
						base.OutputWarn("类别名称已存在，请重试！");
						break;
					case 0:
						base.OutputWarn("保存失败，请重试！");
						break;
					default:
						base.OutputWarn("保存失败，请重试！");
						break;
					}
				}
			}
		}

		public string GetDiscountPercent(string DiscountPercent)
		{
			return (Convert.ToDouble(DiscountPercent) * 100.0).ToString();
		}

		public string GetPointPercent(string PointPercent)
		{
			return (PointPercent == "0") ? "0" : Math.Round(1.0 / Convert.ToDouble(PointPercent)).ToString();
		}
	}
}
