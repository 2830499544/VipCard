using Chain.BLL;
using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class EditLevel : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmSetLevel;

		protected Literal ltlTitle;

		protected HtmlInputText txtLevelName;

		protected HtmlInputHidden txtLevelID;

		protected HtmlInputText txtLevelPoint;

		protected HtmlInputCheckBox chkLevellLock;

		protected HtmlInputText txtLevelRechargePointRate;

		protected HtmlInputText txtLevelDiscountPercent;

		protected HtmlInputText txtLevelPointPercent;

		protected HtmlGenericControl divEnableGoodsSet;

		protected Repeater rptShopClassLevel;

		protected Button btnSetLevel;

		private Chain.BLL.GoodsClass bllGoodsClass = new Chain.BLL.GoodsClass();

		private Chain.BLL.MemLevel bllMemLevel = new Chain.BLL.MemLevel();

		private Chain.BLL.GoodsClassDiscount bllGoodsClassDiscount = new Chain.BLL.GoodsClassDiscount();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.bindData();
				this.divEnableGoodsSet.Visible = PubFunction.curParameter.EnableGoods;
			}
		}

		private void bindData()
		{
			DataTable dtClass = this.bllGoodsClass.GetListByShopID(this._UserShopID).Tables[0];
			DataTable dtResult = this.GetTreeList(dtClass);
			this.rptShopClassLevel.DataSource = dtResult;
			this.rptShopClassLevel.DataBind();
			if (!string.IsNullOrEmpty(base.Request["MemLevelID"]))
			{
				this.txtLevelID.Value = base.Request["MemLevelID"].ToString();
				int MemLevelID = Convert.ToInt32(base.Request["MemLevelID"]);
				if (MemLevelID == 0)
				{
					this.txtLevelPoint.Attributes.Add("readonly", "true");
					this.txtLevelPoint.Style.Add("background-color", "#eee");
				}
				if (this._UserShopID != 1)
				{
					this.txtLevelPoint.Attributes.Add("disabled", "disabled");
					this.txtLevelName.Attributes.Add("disabled", "disabled");
					this.chkLevellLock.Attributes.Add("disabled", "disabled");
				}
				DataTable dtMemLevel = this.bllMemLevel.GetLists(string.Concat(new object[]
				{
					" SysShopMemLevel.ShopID=",
					this._UserShopID,
					" and  SysShopMemLevel.MemLevelID=",
					MemLevelID,
					" "
				})).Tables[0];
				this.txtLevelName.Value = dtMemLevel.Rows[0]["LevelName"].ToString();
				this.txtLevelPoint.Value = dtMemLevel.Rows[0]["LevelPoint"].ToString();
				this.txtLevelDiscountPercent.Value = (Convert.ToDouble(dtMemLevel.Rows[0]["ClassDiscountPercent"]) * 100.0).ToString();
				this.txtLevelPointPercent.Value = ((Convert.ToDouble(dtMemLevel.Rows[0]["ClassPointPercent"]).ToString() == "0") ? "0" : Convert.ToDouble(dtMemLevel.Rows[0]["ClassPointPercent"]).ToString());
				this.txtLevelRechargePointRate.Value = ((Convert.ToDouble(dtMemLevel.Rows[0]["ClassRechargePointRate"]).ToString() == "0") ? "0" : Convert.ToDouble(dtMemLevel.Rows[0]["ClassRechargePointRate"]).ToString());
				this.chkLevellLock.Checked = (bool)dtMemLevel.Rows[0]["LevellLock"];
				DataTable dtGoodsClassDiscount = this.bllGoodsClassDiscount.GetList(MemLevelID, this._UserShopID).Tables[0];
				for (int i = 0; i < this.rptShopClassLevel.Items.Count; i++)
				{
					string ClassID = ((Literal)this.rptShopClassLevel.Items[i].FindControl("ltClassID")).Text;
					DataRow[] drs = dtGoodsClassDiscount.Select(string.Format(" GoodsClassID = '{0}' AND MemLevelID = '{1}'", ClassID, MemLevelID));
					((HtmlInputText)this.rptShopClassLevel.Items[i].FindControl("txtDiscountPercent")).Value = (Convert.ToDouble(drs[0]["ClassDiscountPercent"]) * 100.0).ToString();
					((HtmlInputText)this.rptShopClassLevel.Items[i].FindControl("txtPointPercent")).Value = ((drs[0]["ClassPointPercent"].ToString() == "0") ? "0" : Convert.ToDecimal(drs[0]["ClassPointPercent"]).ToString());
					((Literal)this.rptShopClassLevel.Items[i].FindControl("ltClassDiscountID")).Text = drs[0]["ClassDiscountID"].ToString();
				}
			}
		}

		protected DataTable GetTreeList(DataTable dtSource)
		{
			DataTable dt = new DataTable();
			for (int i = 0; i < dtSource.Columns.Count; i++)
			{
				dt.Columns.Add(new DataColumn(dtSource.Columns[i].ColumnName));
			}
			DataRow[] dr = dtSource.Select(" ParentID=0");
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					dt.Rows[dt.Rows.Count - 1][j] = dr[i][j].ToString();
				}
				string strClassID = dr[i]["ClassID"].ToString();
				this.CreateTreeItem(dtSource, dt, strClassID, 1);
			}
			return dt;
		}

		protected void CreateTreeItem(DataTable dtSource, DataTable dt, string strClassID, int level)
		{
			DataRow[] dr = dtSource.Select(" ParentID=" + strClassID);
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					string temp = dr[i][j].ToString();
					dt.Rows[dt.Rows.Count - 1][j] = temp;
				}
				string strCurrentID = dr[i]["ClassID"].ToString();
				this.CreateTreeItem(dtSource, dt, strCurrentID, level + 1);
			}
		}

		protected void btnSetLevel_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(base.Request["MemLevelID"]))
			{
				Chain.Model.MemLevel mdMemLevel = new Chain.Model.MemLevel();
				Chain.BLL.SysShopMemLevel ShopMemLevelBLL = new Chain.BLL.SysShopMemLevel();
				Chain.Model.SysShopMemLevel ModelShopMemLevel = new Chain.Model.SysShopMemLevel();
				mdMemLevel.LevelDiscountPercent = Convert.ToDecimal(this.txtLevelDiscountPercent.Value.Trim()) / 100m;
				if (this.txtLevelPointPercent.Value != "" && this.txtLevelPointPercent.Value != "0")
				{
					mdMemLevel.LevelPointPercent = ((Convert.ToDecimal(this.txtLevelPointPercent.Value.Trim()) == 0m) ? 0m : Convert.ToDecimal(this.txtLevelPointPercent.Value.Trim()));
				}
				else
				{
					base.MessagePageShowError("快速消费-积分比率输入错误，必须为大于等于0的正整数！");
				}
				mdMemLevel.LevelName = this.txtLevelName.Value.Trim();
				mdMemLevel.LevelPoint = Convert.ToInt32(this.txtLevelPoint.Value.Trim());
				mdMemLevel.LevellLock = this.chkLevellLock.Checked;
				int flag = this.bllMemLevel.Add(mdMemLevel);
				if (flag > 0)
				{
					Chain.BLL.SysShop shop = new Chain.BLL.SysShop();
					for (int i = 1; i < shop.GetMaxId(); i++)
					{
						ModelShopMemLevel.ShopID = i;
						ModelShopMemLevel.MemLevelID = this.bllMemLevel.GetMaxId() - 1;
						ModelShopMemLevel.ClassDiscountPercent = Convert.ToDecimal(this.txtLevelDiscountPercent.Value.Trim()) / 100m;
						ModelShopMemLevel.ClassPointPercent = ((Convert.ToDecimal(this.txtLevelPointPercent.Value.Trim()) == 0m) ? 0m : Convert.ToDecimal(this.txtLevelPointPercent.Value.Trim()));
						ModelShopMemLevel.ClassRechargePointRate = ((Convert.ToDecimal(this.txtLevelRechargePointRate.Value.Trim()) == 0m) ? 0m : Convert.ToDecimal(this.txtLevelRechargePointRate.Value.Trim()));
						int dsasd = ShopMemLevelBLL.Add(ModelShopMemLevel);
					}
					ArrayList sqlArray = new ArrayList();
					string strSqlColumn = "INSERT INTO GoodsClassDiscount (GoodsClassID,MemLevelID,ClassDiscountPercent,ClassPointPercent,DiscountShopID) VALUES ('{0}','{1}','{2}','{3}','{4}')";
					string strAllSql = string.Empty;
					string GoodsClassID = string.Empty;
					int MemLevelID = flag;
					string strClassDiscountPercent = string.Empty;
					decimal decClassDiscountPercent = 100m;
					string ClassDiscountPercent = string.Empty;
					string strClassPointPercent = string.Empty;
					decimal decClassPointPercent = 1m;
					string ClassPointPercent = string.Empty;
					for (int i = 0; i < this.rptShopClassLevel.Items.Count; i++)
					{
						strAllSql = strSqlColumn;
						GoodsClassID = ((Literal)this.rptShopClassLevel.Items[i].FindControl("ltClassID")).Text;
						strClassDiscountPercent = ((HtmlInputText)this.rptShopClassLevel.Items[i].FindControl("txtDiscountPercent")).Value.Trim();
						if (decimal.TryParse(strClassDiscountPercent, out decClassDiscountPercent))
						{
							ClassDiscountPercent = (decClassDiscountPercent / 100m).ToString();
						}
						else
						{
							ClassDiscountPercent = "1.00";
						}
						strClassPointPercent = ((HtmlInputText)this.rptShopClassLevel.Items[i].FindControl("txtPointPercent")).Value.Trim();
						if (decimal.TryParse(strClassPointPercent, out decClassPointPercent))
						{
							ClassPointPercent = ((strClassPointPercent == "0") ? "0" : decClassPointPercent.ToString());
						}
						else
						{
							ClassPointPercent = ((strClassPointPercent == "0") ? "0" : "1");
						}
						strAllSql = string.Format(strAllSql, new object[]
						{
							GoodsClassID,
							MemLevelID,
							ClassDiscountPercent,
							ClassPointPercent,
							this._UserShopID
						});
						sqlArray.Add(strAllSql);
					}
					if (this.bllGoodsClassDiscount.AddList(sqlArray))
					{
						string strSql = "INSERT INTO GoodsClassDiscount (GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) ";
						strSql += string.Format("(SELECT GoodsClass.ClassID,{0},ShopID,1,1 FROM GoodsClass inner join GoodsClassAuthority on GoodsClass.ClassID = GoodsClassAuthority.ClassID WHERE ShopID <> {1})", MemLevelID, this._UserShopID);
						DbHelperSQL.ExecuteSql(strSql);
						this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'保存成功',close: function () { location.href = 'SetLevel.aspx?PID=5'; }});</script>");
					}
					else
					{
						this.bllMemLevel.Delete(flag);
						base.MessagePageShowError("保存失败，请重试！");
					}
				}
				else
				{
					string strError = "保存失败，请重试！";
					if (flag == -1)
					{
						strError = "已存在所需积分的会员等级，请重新输入！";
					}
					base.MessagePageShowError(strError);
				}
			}
			else
			{
				int MemLevelID = Convert.ToInt32(base.Request["MemLevelID"]);
				int ShopMemLevelID = Convert.ToInt32(base.Request["ShopMemLevelID"]);
				Chain.Model.MemLevel mdMemLevel = this.bllMemLevel.GetModel(MemLevelID);
				mdMemLevel.LevelName = this.txtLevelName.Value.Trim();
				mdMemLevel.LevelPoint = Convert.ToInt32(this.txtLevelPoint.Value.Trim());
				mdMemLevel.LevellLock = this.chkLevellLock.Checked;
				int flag = this.bllMemLevel.Update(mdMemLevel);
				Chain.BLL.SysShopMemLevel shopmemleverBll = new Chain.BLL.SysShopMemLevel();
				Chain.Model.SysShopMemLevel ModelMemLevel = shopmemleverBll.GetModel(ShopMemLevelID);
				ModelMemLevel.ClassDiscountPercent = Convert.ToDecimal(this.txtLevelDiscountPercent.Value.Trim()) / 100m;
				ModelMemLevel.ClassPointPercent = ((Convert.ToDecimal(this.txtLevelPointPercent.Value.Trim()) == 0m) ? 0m : Convert.ToDecimal(this.txtLevelPointPercent.Value.Trim()));
				ModelMemLevel.ClassRechargePointRate = ((Convert.ToDecimal(this.txtLevelRechargePointRate.Value.Trim()) == 0m) ? 0m : Convert.ToDecimal(this.txtLevelRechargePointRate.Value.Trim()));
				bool o = shopmemleverBll.Update(ModelMemLevel);
				if (o && flag > 0)
				{
					ArrayList sqlArray = new ArrayList();
					string strSqlColumn = "UPDATE GoodsClassDiscount SET ClassDiscountPercent = '{0}',ClassPointPercent = '{1}' WHERE ClassDiscountID = '{2}'";
					string strAllSql = string.Empty;
					string ClassDiscountPercent = string.Empty;
					string ClassPointPercent = string.Empty;
					string ClassDiscountID = string.Empty;
					for (int i = 0; i < this.rptShopClassLevel.Items.Count; i++)
					{
						ClassDiscountID = ((Literal)this.rptShopClassLevel.Items[i].FindControl("ltClassDiscountID")).Text;
						ClassDiscountPercent = (Convert.ToDecimal(((HtmlInputText)this.rptShopClassLevel.Items[i].FindControl("txtDiscountPercent")).Value.Trim()) / 100m).ToString();
						ClassPointPercent = ((HtmlInputText)this.rptShopClassLevel.Items[i].FindControl("txtPointPercent")).Value.Trim();
						ClassPointPercent = ((ClassPointPercent == "0") ? "0" : Convert.ToDecimal(ClassPointPercent).ToString());
						strAllSql = string.Format(strSqlColumn, ClassDiscountPercent, ClassPointPercent, ClassDiscountID);
						sqlArray.Add(strAllSql);
					}
					if (this.bllGoodsClassDiscount.AddList(sqlArray))
					{
						this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'保存成功',close: function () { location.href = 'SetLevel.aspx?PID=5'; }});</script>");
					}
					else
					{
						base.MessagePageShowError("保存失败，请重试！");
					}
				}
				else
				{
					string strError = "保存失败，请重试！";
					if (flag == -1)
					{
						strError = "已存在所需积分的会员等级，请重新输入！";
					}
					base.MessagePageShowError(strError);
				}
			}
		}
	}
}
