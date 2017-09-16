using Chain.BLL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.StockManage
{
	public class GoodsExcelIn : PageBase
	{
		protected HtmlInputHidden hdMainIndex;

		protected HtmlForm frmGoodsExcelIn;

		protected Literal ltlTitle;

		protected Button btnGoodsTemplate;

		protected Button btnGoodsNumberTemplate;

		protected HtmlInputHidden strPath;

		protected FileUpload fileUploadGoods;

		protected Button btnGoodsCheck;

		protected Button btnGoodsImport;

		protected FileUpload fileUploadGoodsNumber;

		protected Button btnGoodsNumberCheck;

		protected Button btnGoodsNumberImport;

		protected HtmlInputHidden path;

		protected GridView gvErrorGoods;

		protected GridView GridView1;

		private static object _syncObj = new object();

		private string strSavePath = "";

		public bool bolMainIndex
		{
			get
			{
				return this.hdMainIndex.Attributes["bolMainIndex"].ToString() == "1";
			}
			set
			{
				this.hdMainIndex.Attributes.Add("bolMainIndex", value ? "1" : "0");
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.bolMainIndex = false;
			}
		}

		protected void btnGoodsTemplate_Click(object sender, EventArgs e)
		{
			DataExcelInfo.GoodsTemplateExcel(this._UserShopID);
		}

		protected void btnGoodsCheck_Click(object sender, EventArgs e)
		{
			lock (GoodsExcelIn._syncObj)
			{
				string strUploadPath = this.fileUploadGoods.FileName;
				this.strSavePath = base.Server.MapPath("~/Upload/Goods/Goods" + DateTime.Now.ToString("yyMMddHHmmssffff") + ".xls");
				this.strPath.Value = this.strSavePath;
				if (strUploadPath == "")
				{
					base.OutputWarn("请先选择Excel文件。");
				}
				else
				{
					string strFileFix = Path.GetExtension(strUploadPath).ToLower();
					if (strFileFix != ".xls")
					{
						base.OutputWarn("选择文件的文件格式错误，请重新选择Excel文件。");
					}
					else
					{
						if (!Directory.Exists(base.Server.MapPath("~/Upload/Goods")))
						{
							Directory.CreateDirectory(base.Server.MapPath("~/Upload/Goods"));
						}
						if (File.Exists(this.strSavePath))
						{
							File.Delete(this.strSavePath);
						}
						this.fileUploadGoods.PostedFile.SaveAs(this.strSavePath);
					}
					if (File.Exists(this.strSavePath))
					{
						DataTable dtGoods = DataExcelInfo.CheckGoodsDataExcel(this.strSavePath);
						DataTable dtGoodsCopy = this.CheckGoodsData(dtGoods, false);
						this.GridView1.Visible = false;
						this.gvErrorGoods.Visible = true;
						this.Get_ParameterList(dtGoodsCopy);
						if (dtGoodsCopy.Rows.Count <= 0 && dtGoods.Rows.Count > 0)
						{
							this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'您提交的商品数据检验通过，请导入商品信息。',close: function () {  }});</script>");
							this.bolMainIndex = true;
						}
						else
						{
							File.Delete(this.strSavePath);
						}
					}
					else
					{
						File.Delete(this.strSavePath);
						base.OutputWarn("商品数据有错误，请改正。");
					}
				}
			}
		}

		protected void btnGoodsImport_Click(object sender, EventArgs e)
		{
			lock (GoodsExcelIn._syncObj)
			{
				this.strSavePath = this.strPath.Value;
				if (File.Exists(this.strSavePath))
				{
					if (this.bolMainIndex)
					{
						DataTable dtGoods = DataExcelInfo.CheckGoodsDataExcel(this.strSavePath);
						DataTable dtGoodsCopy = this.CheckGoodsData(dtGoods, true);
						if (DataExcelInfo.GoodsImport(dtGoodsCopy, this._UserID, this._UserShopID))
						{
							base.OutputWarn("恭喜您，导入商品成功！");
							DataExcelInfo.GoodsNumberImport(dtGoods, this._UserShopID);
							this.strSavePath = "";
							this.bolMainIndex = false;
						}
						else
						{
							File.Delete(this.strSavePath);
							base.OutputWarn("导入商品失败,请再次检验并改正商品数据！");
						}
					}
					else
					{
						File.Delete(this.strSavePath);
						base.OutputWarn("您导入的商品数据还没有检验成功，请通过检验再导入商品数据。");
					}
				}
				else
				{
					base.OutputWarn("您还没有检验商品数据，请先检验数据");
				}
			}
		}

		private DataTable CheckGoodsData(DataTable dtGoods, bool istable)
		{
			DataTable dtGoodsCopy = dtGoods.Copy();
			dtGoodsCopy.Clear();
			if (dtGoods.Rows.Count <= 0)
			{
				File.Delete(this.strSavePath);
				base.OutputWarn("检验的Excel表格还没有商品数据，请填写商品数据再检验。");
			}
			else
			{
				DataTable dtspid = new Chain.BLL.GoodsClass().GetAllList().Tables[0];
				for (int i = 0; i < dtGoods.Rows.Count; i++)
				{
					bool bolIndex = false;
					string strError = "";
					if (dtGoods.Rows[i].IsNull(0) || dtGoods.Rows[i]["GoodsCode"].ToString() == "")
					{
						strError += "商品编码不能为空,";
						bolIndex = true;
					}
					if (dtGoods.Rows[i]["GoodsCode"].ToString().Length < 5 || dtGoods.Rows[i]["GoodsCode"].ToString().Length > 25)
					{
						strError += "商品编码必须是5~25位数字,";
						bolIndex = true;
					}
					Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
					if (bllGoods.Exists(dtGoods.Rows[i][0].ToString(), this._UserShopID))
					{
						strError += "商品编码已存在系统,";
						bolIndex = true;
					}
					for (int j = 0; j < i; j++)
					{
						if (dtGoods.Rows[j][0].ToString() == dtGoods.Rows[i][0].ToString())
						{
							strError += "商品编码在Excel表重复出现,";
							bolIndex = true;
						}
					}
					if (dtGoods.Rows[i].IsNull(1) || dtGoods.Rows[i]["Name"].ToString() == "")
					{
						strError += "商品名称不能为空,";
						bolIndex = true;
					}
					if (dtGoods.Rows[i].IsNull(3) || dtGoods.Rows[i]["GoodsClassID"].ToString() == "")
					{
						strError += "商品分类ID不能为空,";
						bolIndex = true;
					}
					else
					{
						int myid;
						DataRow[] drGoodsClass;
						if (int.TryParse(dtGoods.Rows[i]["GoodsClassID"].ToString(), out myid))
						{
							drGoodsClass = dtspid.Select(string.Format(" ClassID = '{0}'", dtGoods.Rows[i]["GoodsClassID"].ToString()));
							if (drGoodsClass.Length <= 0)
							{
								drGoodsClass = dtspid.Select(string.Format(" ClassName = '{0}'", dtGoods.Rows[i]["GoodsClassID"].ToString()));
							}
						}
						else
						{
							drGoodsClass = dtspid.Select(string.Format(" ClassName = '{0}'", dtGoods.Rows[i]["GoodsClassID"].ToString()));
						}
						if (drGoodsClass.Length > 0)
						{
							dtGoods.Rows[i]["GoodsClassID"] = drGoodsClass[0]["ClassID"];
						}
						else
						{
							strError += "商品分类未找到,";
							bolIndex = true;
						}
					}
					if (dtGoods.Rows[i].IsNull(4) || dtGoods.Rows[i]["Unit"].ToString() == "")
					{
						strError += "计量单位不能为空,";
						bolIndex = true;
					}
					decimal yzsz;
					if (!dtGoods.Rows[i].IsNull(5) && dtGoods.Rows[i]["GoodsBidPrice"].ToString() != "")
					{
						if (!decimal.TryParse(dtGoods.Rows[i]["GoodsBidPrice"].ToString(), out yzsz))
						{
							strError += "参考进价只能是整数或小数,";
							bolIndex = true;
						}
					}
					if (dtGoods.Rows[i].IsNull(6) || dtGoods.Rows[i]["Price"].ToString() == "")
					{
						strError += "零售单价不能为空,";
						bolIndex = true;
					}
					else if (!decimal.TryParse(dtGoods.Rows[i]["Price"].ToString(), out yzsz))
					{
						strError += "零售单价只能是整数或小数,";
						bolIndex = true;
					}
					if (!dtGoods.Rows[i].IsNull(7) && dtGoods.Rows[i]["Point"].ToString() != "")
					{
						int myid;
						if (!int.TryParse(dtGoods.Rows[i]["Point"].ToString(), out myid))
						{
							strError += "商品积分只能是整数,";
							bolIndex = true;
						}
					}
					if (dtGoods.Rows[i].IsNull(8) || dtGoods.Rows[i]["GoodsType"].ToString() == "")
					{
						strError += "商品类型不能为空,";
						bolIndex = true;
					}
					else
					{
						string intType = dtGoods.Rows[i]["GoodsType"].ToString();
						if (intType != "0" && intType != "1" && intType != "普通商品" && intType != "服务项目")
						{
							strError += "商品类型只能是0(普通商品)或1(服务项目)，";
							bolIndex = true;
						}
						else if (intType == "普通商品" || intType == "服务项目")
						{
							if (intType == "服务项目")
							{
								dtGoods.Rows[i]["GoodsType"] = "1";
							}
							else
							{
								dtGoods.Rows[i]["GoodsType"] = "0";
							}
						}
					}
					if (!dtGoods.Rows[i].IsNull(9) && dtGoods.Rows[i]["MinPercent"].ToString() != "")
					{
						if (!decimal.TryParse(dtGoods.Rows[i]["MinPercent"].ToString(), out yzsz))
						{
							strError += "最低折扣只能是0～1的小数,";
							bolIndex = true;
						}
						else if (decimal.Parse(dtGoods.Rows[i]["MinPercent"].ToString()) > 1m)
						{
							strError += "最低折扣只能是0～1的小数,";
							bolIndex = true;
						}
					}
					if (!dtGoods.Rows[i].IsNull(10) && dtGoods.Rows[i]["CommissionType"].ToString() != "")
					{
						string intType = dtGoods.Rows[i]["CommissionType"].ToString();
						if (intType != "1" && intType != "2" && intType != "按固定比例提成" && intType != "按固定金额提成")
						{
							strError += "提成类型只能是1(按固定比例提成)或2(按固定金额提成),";
							bolIndex = true;
						}
						else if (intType == "按固定比例提成" || intType == "按固定金额提成")
						{
							if (intType == "按固定比例提成")
							{
								dtGoods.Rows[i]["CommissionType"] = "1";
							}
							else
							{
								dtGoods.Rows[i]["CommissionType"] = "2";
							}
						}
					}
					if (!dtGoods.Rows[i].IsNull(11) && dtGoods.Rows[i]["CommissionNumber"].ToString() != "")
					{
						if (!decimal.TryParse(dtGoods.Rows[i]["CommissionNumber"].ToString(), out yzsz))
						{
							strError += "提成金额(比例)只能是数字,";
							bolIndex = true;
						}
						else if (decimal.Parse(dtGoods.Rows[i]["CommissionNumber"].ToString()) < 0m)
						{
							strError += "提成金额(比例)只能是大于0的数字,";
							bolIndex = true;
						}
					}
					Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
					DataRow[] drCustomField = bllCustomField.GetList("  CustomType=2 ").Tables[0].Select();
					DataRow[] array = drCustomField;
					for (int k = 0; k < array.Length; k++)
					{
						DataRow dr = array[k];
						string strCustomField = dr["CustomField"].ToString();
						string strItem = dtGoods.Rows[i][strCustomField].ToString();
						if (!bool.Parse(dr["CustomFieldIsNull"].ToString()) && strItem == "")
						{
							strError = strError + dr["CustomFieldName"].ToString() + "不能为空,";
							bolIndex = true;
						}
					}
					if (bolIndex)
					{
						if (strError.Substring(strError.Length - 1, 1) == ",")
						{
							strError = strError.Remove(strError.Length - 1);
						}
						dtGoods.Rows[i]["Error"] = strError;
						dtGoodsCopy.Rows.Add(dtGoods.Rows[i].ItemArray);
					}
					if (istable)
					{
						dtGoodsCopy = dtGoods;
					}
				}
			}
			return dtGoodsCopy;
		}

		protected void btnGoodsNumberTemplate_Click(object sender, EventArgs e)
		{
			DataExcelInfo.GoodsNumberTemplateExcel(this._UserShopID);
		}

		protected void btnGoodsNumberImport_Click(object sender, EventArgs e)
		{
			this.strSavePath = this.path.Value;
			if (File.Exists(this.strSavePath))
			{
				if (this.bolMainIndex)
				{
					DataTable dtGoods = DataExcelInfo.CheckGoodsNumDataExcel(this.strSavePath);
					DataTable dtGoodsCopy = this.CheckGoodsNumData(dtGoods, true, this._UserShopID);
					if (DataExcelInfo.GoodsImportNums(dtGoodsCopy, this._UserID, this._UserShopID))
					{
						base.OutputWarn("恭喜您，导入商品库存成功！");
						this.strSavePath = "";
						this.bolMainIndex = false;
					}
					else
					{
						File.Delete(this.strSavePath);
						base.OutputWarn("导入商品库存失败,请再次检验并改正商品数据！");
					}
				}
				else
				{
					File.Delete(this.strSavePath);
					base.OutputWarn("您导入的商品库存数据还没有检验成功，请通过检验再导入商品数据。");
				}
			}
			else
			{
				base.OutputWarn("您还没有检验商品库存数据，请先检验数据");
			}
		}

		protected void btnGoodsNumberCheck_Click(object sender, EventArgs e)
		{
			string strUploadPath = this.fileUploadGoodsNumber.FileName;
			this.strSavePath = base.Server.MapPath("~/Upload/Goods/GoodsNumber" + DateTime.Now.ToString("yyMMddHHmmssffff") + ".xls");
			this.path.Value = this.strSavePath;
			if (strUploadPath == "")
			{
				base.OutputWarn("请先选择Excel文件。");
			}
			else
			{
				string strFileFix = Path.GetExtension(strUploadPath).ToLower();
				if (strFileFix != ".xls")
				{
					base.OutputWarn("选择文件的文件格式错误，请重新选择Excel文件。");
				}
				else
				{
					if (!Directory.Exists(base.Server.MapPath("~/Upload/Goods")))
					{
						Directory.CreateDirectory(base.Server.MapPath("~/Upload/Goods"));
					}
					if (File.Exists(this.strSavePath))
					{
						File.Delete(this.strSavePath);
					}
					this.fileUploadGoodsNumber.PostedFile.SaveAs(this.strSavePath);
				}
				if (File.Exists(this.strSavePath))
				{
					DataTable dtGoods = DataExcelInfo.CheckGoodsNumDataExcel(this.strSavePath);
					DataTable dtGoodsCopy = this.CheckGoodsNumData(dtGoods, false, this._UserShopID);
					this.GridView1.Visible = true;
					this.gvErrorGoods.Visible = false;
					this.Get_ParameterList(dtGoodsCopy);
					if (dtGoodsCopy.Rows.Count <= 0 && dtGoods.Rows.Count > 0)
					{
						this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'您提交的商品库存数据检验通过，请导入商品库存信息。',close: function () {  }});</script>");
						this.bolMainIndex = true;
					}
				}
				else
				{
					File.Delete(this.strSavePath);
					base.OutputWarn("商品库存数据有错误，请改正。");
				}
			}
		}

		private DataTable CheckGoodsNumData(DataTable dtGoods, bool istable, int ThisShopID)
		{
			DataTable dtGoodsCopy = dtGoods.Copy();
			dtGoodsCopy.Clear();
			if (dtGoods.Rows.Count <= 0)
			{
				File.Delete(this.strSavePath);
				base.OutputWarn("检验的Excel表格还没有商品数据，请填写商品数据再检验。");
			}
			else
			{
				DataTable dtspid = new Chain.BLL.GoodsClass().GetAllList().Tables[0];
				for (int i = 0; i < dtGoods.Rows.Count; i++)
				{
					bool bolIndex = false;
					string strError = "";
					if (dtGoods.Rows[i].IsNull(0) || dtGoods.Rows[i]["GoodsCode"].ToString() == "")
					{
						strError += "商品编码不能为空,";
						bolIndex = true;
					}
					if (dtGoods.Rows[i]["GoodsCode"].ToString().Length < 5 || dtGoods.Rows[i]["GoodsCode"].ToString().Length > 25)
					{
						strError += "商品编码必须是5~25位数字,";
						bolIndex = true;
					}
					Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
					if (!bllGoods.Exists(dtGoods.Rows[i][0].ToString()))
					{
						strError += "商品编码不存在系统,";
						bolIndex = true;
					}
					DataRow[] row = dtGoods.Select(" GoodsCode='" + dtGoods.Rows[i][0].ToString() + "'");
					if (row.Length > 1)
					{
						strError += "商品编码在Excel表重复出现,";
						bolIndex = true;
					}
					if (dtGoods.Rows[i].IsNull(1) || dtGoods.Rows[i]["Name"].ToString() == "")
					{
						strError += "商品名称不能为空,";
						bolIndex = true;
					}
					if (!dtGoods.Rows[i].IsNull(2) && dtGoods.Rows[i]["Number"].ToString() != "")
					{
						if (PubFunction.ProductDecimalNum == 0)
						{
							int myid;
							if (!int.TryParse(dtGoods.Rows[i]["Number"].ToString(), out myid))
							{
								strError += "商品库存只能是整数,";
								bolIndex = true;
							}
						}
						else
						{
							string num = dtGoods.Rows[i]["Number"].ToString().Trim();
							if (Regex.IsMatch(num, "^\\d+\\.$"))
							{
								num += "0";
							}
							if (num.IndexOf(".") > -1 && !Regex.IsMatch(num, "^\\d+\\.\\d{1," + PubFunction.ProductDecimalNum.ToString() + "}$"))
							{
								strError += string.Format("商品库存只能填写0或正整数或大于0的小数位不超{0}位的小数", PubFunction.ProductDecimalNum);
								bolIndex = true;
							}
						}
					}
					if (bolIndex)
					{
						if (strError.Substring(strError.Length - 1, 1) == ",")
						{
							strError = strError.Remove(strError.Length - 1);
						}
						dtGoods.Rows[i]["Error"] = strError;
						dtGoodsCopy.Rows.Add(dtGoods.Rows[i].ItemArray);
					}
					if (istable)
					{
						dtGoodsCopy = dtGoods;
					}
				}
			}
			return dtGoodsCopy;
		}

		private void Get_ParameterList(DataTable dtGoods)
		{
			try
			{
				Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
				List<Chain.Model.MemCustomField> fieldlist = bllCustom.GetModelList(" CustomType=2 ");
				this.GetCustomFields(fieldlist);
				if (this.GridView1.Visible)
				{
					this.GridView1.DataSource = dtGoods;
					this.GridView1.DataBind();
				}
				else
				{
					this.gvErrorGoods.DataSource = dtGoods;
					this.gvErrorGoods.DataBind();
				}
			}
			catch
			{
				base.OutputWarn("Excel表中数据格式有严重错误，请改正数据再验证。");
			}
		}

		public void GetCustomFields(List<Chain.Model.MemCustomField> fieldlist)
		{
			DataControlFieldCollection drc = this.gvErrorGoods.Columns.CloneFields();
			this.gvErrorGoods.Columns.RemoveAt(drc.Count - 1);
			int fieldCount = this.gvErrorGoods.Columns.Count - 1;
			foreach (Chain.Model.MemCustomField CField in fieldlist)
			{
				BoundField field = new BoundField();
				field.HeaderText = CField.CustomFieldName;
				field.DataField = CField.CustomField;
				field.ReadOnly = true;
				field.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
				field.ItemStyle.Width = 40;
				this.gvErrorGoods.Columns.Insert(fieldCount + 1, field);
				fieldCount++;
			}
			this.gvErrorGoods.Columns.Insert(fieldCount + 1, drc[drc.Count - 1]);
		}
	}
}
