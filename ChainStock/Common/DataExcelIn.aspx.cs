using Chain.BLL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Common
{
	public class DataExcelIn : PageBase
	{
		private static object _syncObj = new object();

		private string strSavePath = "";

		protected HtmlForm frmDataExcelIn;

		protected Literal ltMainIndex;

		protected Literal ltlTitle;

		protected Button btnMemTemplate;

		protected FileUpload fileUploadMem;

		protected Button btnMemCheck;

		protected Button btnMemImport;

		protected HtmlInputHidden strPath;

		protected Label lblError;

		protected GridView gvErrorMem;

		public bool bolMainIndex
		{
			get
			{
				return this.ltMainIndex.Text == "1";
			}
			set
			{
				this.ltMainIndex.Text = (value ? "1" : "0");
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.bolMainIndex = false;
			}
		}

		protected void btnMemTemplate_Click(object sender, EventArgs e)
		{
			DataExcelInfo.MemTemplateExcel();
		}

		protected void btnMemCheck_Click(object sender, EventArgs e)
		{
			lock (DataExcelIn._syncObj)
			{
				string strUploadPath = this.fileUploadMem.FileName;
				this.strSavePath = base.Server.MapPath("~/Upload/Members/Members" + DateTime.Now.ToString("yyMMddHHmmssffff") + ".xls");
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
						if (!Directory.Exists(base.Server.MapPath("~/Upload/Members")))
						{
							Directory.CreateDirectory(base.Server.MapPath("~/Upload/Members"));
						}
						if (File.Exists(this.strSavePath))
						{
							File.Delete(this.strSavePath);
						}
						this.fileUploadMem.PostedFile.SaveAs(this.strSavePath);
					}
					if (File.Exists(this.strSavePath))
					{
						string err = "";
						DataTable dtMem = DataExcelInfo.CheckMemDataExcel(this.strSavePath, ref err);
						if (!string.IsNullOrEmpty(err))
						{
							File.Delete(this.strSavePath);
							base.OutputWarn(err);
						}
						else
						{
							DataTable dtMemCopy = this.CheckData(dtMem);
							this.Get_ParameterList(dtMemCopy);
							if (dtMemCopy.Rows.Count <= 0 && dtMem.Rows.Count > 0)
							{
								this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'您提交的会员数据检验通过，请导入会员数据。',close: function () { }});</script>");
								this.bolMainIndex = true;
							}
							else
							{
								File.Delete(this.strSavePath);
							}
						}
					}
					else
					{
						File.Delete(this.strSavePath);
						base.OutputWarn("会员数据有错误，请改正。");
					}
				}
			}
		}

		protected void btnMemImport_Click(object sender, EventArgs e)
		{
			lock (DataExcelIn._syncObj)
			{
				this.strSavePath = this.strPath.Value;
				if (File.Exists(this.strSavePath))
				{
					if (this.bolMainIndex)
					{
						string errstr = "";
						DataTable dtMem = DataExcelInfo.CheckMemDataExcel(this.strSavePath, ref errstr);
						if (!string.IsNullOrEmpty(errstr))
						{
							File.Delete(this.strSavePath);
							base.OutputWarn(errstr);
						}
						else if (DataExcelInfo.MemImport(dtMem, this.curParameter.bolPwd, this._UserID))
						{
							base.OutputWarn("恭喜您，导入会员成功！");
							Chain.BLL.Mem memBll = new Chain.BLL.Mem();
							Chain.Model.Mem memModel = new Chain.Model.Mem();
							for (int rowIndex = 0; rowIndex < dtMem.Rows.Count; rowIndex++)
							{
								decimal memMoney = Convert.ToDecimal(dtMem.Rows[rowIndex]["MemMoney"]);
								if (memMoney != 0m)
								{
									memModel = memBll.GetModelByMemCard(dtMem.Rows[rowIndex]["MemCard"].ToString());
									Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
									moneyChangeLogModel.MoneyChangeMemID = memModel.MemID;
									moneyChangeLogModel.MoneyChangeUserID = this._UserID;
									moneyChangeLogModel.MoneyChangeType = 6;
									moneyChangeLogModel.MoneyChangeAccount = "";
									moneyChangeLogModel.MoneyChangeMoney = memModel.MemMoney;
									moneyChangeLogModel.MemMoney = memModel.MemMoney;
									moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
									moneyChangeLogModel.MoneyChangeGiveMoney = memModel.MemMoney;
									new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
									Chain.Model.MemRecharge modelRecharge = new Chain.Model.MemRecharge();
									modelRecharge.RechargeMemID = memModel.MemID;
									modelRecharge.RechargeType = 1;
									modelRecharge.RechargeMoney = 0m;
									modelRecharge.RechargeGive = memModel.MemMoney;
									modelRecharge.RechargeRemark = "会员登记赠送金额,初始金额：" + memModel.MemMoney.ToString();
									modelRecharge.RechargeShopID = this._UserShopID;
									modelRecharge.RechargeCreateTime = DateTime.Now;
									modelRecharge.RechargeAccount = PubFunction.curParameter.strMemRechargePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
									modelRecharge.RechargeUserID = this._UserID;
									modelRecharge.RechargeCardBalance = memModel.MemMoney;
									modelRecharge.RechargeIsApprove = true;
									Chain.BLL.MemRecharge bllRecharge = new Chain.BLL.MemRecharge();
									bllRecharge.Add(modelRecharge);
								}
							}
							this.strSavePath = "";
							this.bolMainIndex = false;
						}
						else
						{
							File.Delete(this.strSavePath);
							base.OutputWarn("导入会员失败,请再次检验并改正会员数据！");
						}
					}
					else
					{
						File.Delete(this.strSavePath);
						base.OutputWarn("您导入的会员数据还没有检验成功，请通过检验再导入会员数据。");
					}
				}
				else
				{
					base.OutputWarn("您还没有检验会员数据，请先检验数据");
				}
			}
		}

		private void Get_ParameterList(DataTable dtMem)
		{
			try
			{
				Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
				List<Chain.Model.MemCustomField> fieldlist = bllCustom.GetModelList("  CustomType=1  ");
				this.GetCustomFields(fieldlist);
				this.gvErrorMem.DataSource = dtMem;
				this.gvErrorMem.DataBind();
			}
			catch
			{
				base.OutputWarn("Excel表中数据格式有严重错误，请改正数据再验证。");
			}
		}

		public void GetCustomFields(List<Chain.Model.MemCustomField> fieldlist)
		{
			DataControlFieldCollection drc = this.gvErrorMem.Columns.CloneFields();
			this.gvErrorMem.Columns.RemoveAt(drc.Count - 1);
			int fieldCount = this.gvErrorMem.Columns.Count - 1;
			foreach (Chain.Model.MemCustomField CField in fieldlist)
			{
				BoundField field = new BoundField();
				field.HeaderText = CField.CustomFieldName;
				field.DataField = CField.CustomField;
				field.ReadOnly = true;
				field.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
				field.ItemStyle.Width = 40;
				this.gvErrorMem.Columns.Insert(fieldCount + 1, field);
				fieldCount++;
			}
			this.gvErrorMem.Columns.Insert(fieldCount + 1, drc[drc.Count - 1]);
		}

		private DataTable CheckData(DataTable dtMem)
		{
			DataTable dtMemCopy = dtMem.Copy();
			dtMemCopy.Clear();
			if (dtMem.Rows.Count <= 0)
			{
				File.Delete(this.strSavePath);
				base.OutputWarn("检验的Excel表格还没有会员数据，请填写会员数据再检验。");
			}
			else
			{
				for (int i = 0; i < dtMem.Rows.Count; i++)
				{
					bool bolIndex = false;
					string strError = "";
					if (dtMem.Rows[i].IsNull(0) || dtMem.Rows[i]["MemCard"].ToString() == "")
					{
						strError += "会员卡号不能为空,";
						bolIndex = true;
					}
					if (dtMem.Rows[i]["MemCard"].ToString().Length < 4 || dtMem.Rows[i]["MemCard"].ToString().Length > 20)
					{
						strError += "会员卡号长度为3~20位,";
						bolIndex = true;
					}
					if (!dtMem.Rows[i].IsNull("MemCardNumber") && dtMem.Rows[i]["MemCardNumber"].ToString() != "" && (dtMem.Rows[i]["MemCardNumber"].ToString().Length < 4 || dtMem.Rows[i]["MemCardNumber"].ToString().Length > 20))
					{
						strError += "卡面号码长度为3~20位,";
						bolIndex = true;
					}
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					int exists = bllMem.Exists(0, dtMem.Rows[i][0].ToString(), dtMem.Rows[i][4].ToString(), dtMem.Rows[i][16].ToString(), Convert.ToInt32( dtMem.Rows[i][13] ));
					int num = exists;
					if (num != -6)
					{
						switch (num)
						{
						case -2:
							strError += "手机号码已存在系统,";
							bolIndex = true;
							break;
						case -1:
							strError += "卡号已存在系统,";
							bolIndex = true;
							break;
						}
					}
					else
					{
						strError += "卡面号码已存在系统,";
						bolIndex = true;
					}
					bool bolCardIndex = false;
					bool bolMobileIndex = false;
					bool bolMemCardNumber = false;
					for (int j = 0; j < i; j++)
					{
						if (dtMem.Rows[j][0].ToString() == dtMem.Rows[i][0].ToString())
						{
							bolCardIndex = true;
						}
						if (dtMem.Rows[j][4].ToString() == dtMem.Rows[i][4].ToString() && dtMem.Rows[i][4].ToString() != "")
						{
							bolMobileIndex = true;
						}
						if (dtMem.Rows[j][16].ToString() == dtMem.Rows[i][16].ToString() && dtMem.Rows[i][16].ToString() != "")
						{
							bolMemCardNumber = true;
						}
					}
					if (bolCardIndex)
					{
						strError += "卡号在Excel表重复出现,";
						bolIndex = true;
					}
					if (bolMobileIndex)
					{
						strError += "手机号码在Excel表重复出现,";
						bolIndex = true;
					}
					if (bolMemCardNumber)
					{
						strError += "卡面号码在Excel表重复出现,";
						bolIndex = true;
					}
					if (dtMem.Rows[i].IsNull(2) || dtMem.Rows[i][2].ToString() == "")
					{
						strError += "性别不能为空,";
						bolIndex = true;
					}
					else if (dtMem.Rows[i][2].ToString() != "男" && dtMem.Rows[i][2].ToString() != "女")
					{
						strError += "性别的格式不正确,";
						bolIndex = true;
					}
					if (dtMem.Rows[i][3].ToString() != "")
					{
						Regex regex = new Regex("(\\d{15}$|^\\d{18}$|^\\d{17}(\\d|X|x))");
						if (!regex.IsMatch(dtMem.Rows[i][3].ToString()))
						{
							strError += "身份证的格式不正确,";
							bolIndex = true;
						}
					}
					try
					{
						if (dtMem.Rows[i][6].ToString() == "未设置")
						{
							dtMem.Rows[i][6] = "1900-01-01";
						}
						if (dtMem.Rows[i][6].ToString() != "")
						{
							DateTime.Parse(dtMem.Rows[i][6].ToString());
						}
					}
					catch
					{
						strError += "生日的格式不正确,";
						bolIndex = true;
					}
					if (dtMem.Rows[i].IsNull(7) || dtMem.Rows[i][7].ToString() == "")
					{
						strError += "积分不能为空,";
						bolIndex = true;
					}
					if (dtMem.Rows[i].IsNull(8) || dtMem.Rows[i][8].ToString() == "")
					{
						strError += "余额不能为空,";
						bolIndex = true;
					}
					if (dtMem.Rows[i].IsNull(9) || dtMem.Rows[i][9].ToString() == "")
					{
						strError += "历史消费金额不能为空,";
						bolIndex = true;
					}
					else
					{
						try
						{
							Convert.ToDecimal(dtMem.Rows[i][9].ToString());
						}
						catch
						{
							strError += "历史消费金额的格式不正确,";
							bolIndex = true;
						}
					}
					if (dtMem.Rows[i].IsNull(12) || dtMem.Rows[i][12].ToString() == "")
					{
						strError += "会员等级不能为空,";
						bolIndex = true;
					}
					if (dtMem.Rows[i].IsNull(13) || dtMem.Rows[i][13].ToString() == "")
					{
						strError += "开卡商家不能为空,";
						bolIndex = true;
					}
					else if (dtMem.Rows[i][13].ToString().Trim() == "0")
					{
						strError += "开卡商家输入有误,";
						bolIndex = true;
					}
					if (dtMem.Rows[i].IsNull(14) || dtMem.Rows[i][14].ToString() == "")
					{
						strError += "办卡日期不能为空,";
						bolIndex = true;
					}
					else
					{
						try
						{
							DateTime dtime = DateTime.Parse(dtMem.Rows[i][14].ToString());
							dtMem.Rows[i][14] = dtime.ToShortDateString();
						}
						catch
						{
							strError += "办卡时期的格式不正确,";
							bolIndex = true;
						}
					}
					Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
					DataRow[] drCustomField = bllCustomField.GetList(" CustomType=1 ").Tables[0].Select();
					DataRow[] array = drCustomField;
					for (int k = 0; k < array.Length; k++)
					{
						DataRow dr = array[k];
						string strCustomField = dr["CustomField"].ToString();
						string strItem = dtMem.Rows[i][strCustomField].ToString();
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
						dtMem.Rows[i]["Error"] = strError;
						dtMemCopy.Rows.Add(dtMem.Rows[i].ItemArray);
					}
				}
			}
			return dtMemCopy;
		}
	}
}
