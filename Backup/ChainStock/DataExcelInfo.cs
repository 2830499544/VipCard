using Chain.BLL;
using Chain.Common;
using Chain.Common.DEncrypt;
using Chain.Model;
using NickLee.Common.ExcelLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public static class DataExcelInfo
{
	private static Regex regx = new Regex("\\s");

	public static void SysRotatePrizeLogExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='11' style='text-align:center; '><h2><strong>中奖记录列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>中奖单号</td><td>会员姓名</td><td>会员卡号</td><td>会员等级</td><td>所属店铺</td><td>中奖等级</td><td>中奖时间</td><td>获得奖品</td><td>兑换码</td><td>奖品领取状态</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:160px'>" + dt.Rows[i]["PrizeAccount"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "</td>");
			str.Append("<td style='width:150px'>" + ((dt.Rows[i]["MemCard"].ToString() == "0") ? "无卡号" : dt.Rows[i]["MemCard"].ToString()) + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["LevelName"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["PrizeLevel"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["CreateTime"].ToString() + "</td>");
			Chain.BLL.SysRotate bllSysRotate = new Chain.BLL.SysRotate();
			Chain.Model.SysRotate modelSysRotate = bllSysRotate.GetModel(int.Parse(dt.Rows[i]["RotateID"].ToString()));
			string result = "";
			string text = dt.Rows[i]["PrizeLevel"].ToString();
			if (text != null)
			{
				if (!(text == "一等奖"))
				{
					if (!(text == "二等奖"))
					{
						if (!(text == "三等奖"))
						{
							if (!(text == "四等奖"))
							{
								if (!(text == "五等奖"))
								{
									if (text == "六等奖")
									{
										result = modelSysRotate.SixPrizeName;
									}
								}
								else
								{
									result = modelSysRotate.FivePrizeName;
								}
							}
							else
							{
								result = modelSysRotate.FourPrizeName;
							}
						}
						else
						{
							result = modelSysRotate.ThreePrizeName;
						}
					}
					else
					{
						result = modelSysRotate.TwoPrizeName;
					}
				}
				else
				{
					result = modelSysRotate.OnePrizeName;
				}
			}
			str.Append("<td style='width:150px'>" + result + "</td>");
			str.Append("<td style='width:160px'>" + dt.Rows[i]["PrizeCode"] + "</td>");
			string status;
			if (dt.Rows[i]["PrizeStatus"].ToString() == "0")
			{
				status = "待领取";
			}
			else
			{
				status = "已领取";
			}
			str.Append("<td style='width:160px'>" + status + "</td>");
			str.Append("</tr>");
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "中奖记录列表.xls", 100, Encoding.UTF8);
	}

	public static void WeiXinGiveMoneyExport(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='6' style='text-align:center; '><h2><strong>红包领取明细</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='6' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>会员卡号</td><td>会员姓名</td><td>所属店铺</td><td>领取金额</td><td>领取时间</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + decimal.Parse(dt.Rows[i]["GiveMoney"].ToString()).ToString("#0.00") + "</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["GiveTime"].ToString() + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["GiveMoney"].ToString());
		}
		str.Append("<tr><td colspan='6'><h4>红包领取总金额:" + dclTotal.ToString("0.00") + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "红包领取明细.xls", 100, Encoding.UTF8);
	}

	public static void MemTransferMoney(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>会员转账记录列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>转账单号</td><td>转款会员</td><td>转款会员卡号</td><td>收款会员</td><td>收款会员卡号</td><td>转账金额</td><td>转账时间</td><td>备注</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["TransferAccount"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TransferFromMemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TransferFromMemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TransferToMemName"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TransferToMemCard"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TransferMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["TransferCreateTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TransferRemark"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["TransferMoney"].ToString());
		}
		str.Append("<tr><td colspan='10'><h4>会员转账总金额:" + dclTotal.ToString("0.00") + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员转账记录列表.xls", 100, Encoding.UTF8);
	}

	public static void ExportExcel(string str, string filename, int max, Encoding encoding)
	{
		GridView gv = new GridView();
		HttpResponse response = HttpContext.Current.Response;
		foreach (DataControlField field in gv.Columns)
		{
			if (field != null)
			{
				field.SortExpression = null;
			}
		}
		if (max > 0)
		{
			gv.PageSize = max;
		}
		Page page = new Page();
		HtmlForm child = new HtmlForm();
		page.EnableEventValidation = false;
		page.Controls.Add(child);
		response.Clear();
		response.Buffer = true;
		response.Charset = encoding.WebName;
		response.ContentEncoding = encoding;
		response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename, encoding));
		response.ContentType = "application/ms-excel";
		StringWriter writer = new StringWriter();
		HtmlTextWriter writer2 = new HtmlTextWriter(writer);
		page.RenderControl(writer2);
		response.Output.Write(str);
		response.Flush();
		response.End();
	}

	public static void MemReportExcel(DataTable dt, string Master)
	{
		List<FieldModel> FieldList = new List<FieldModel>();
		FieldList.Add(new FieldModel("会员卡号", "MemCard", "string", false));
		FieldList.Add(new FieldModel("姓名", "MemName", "string", true));
		FieldList.Add(new FieldModel("性别", "MemSex", "int", false));
		FieldList.Add(new FieldModel("身份证号码", "MemIdentityCard", "string", true));
		FieldList.Add(new FieldModel("手机号码", "MemMobile", "string", true));
		FieldList.Add(new FieldModel("固定电话", "CardID", "string", true));
		FieldList.Add(new FieldModel("生日", "MemBirthday", "datetime", true));
		FieldList.Add(new FieldModel("积分", "MemPoint", "int", false));
		FieldList.Add(new FieldModel("余额", "MemMoney", "decimal", false));
		FieldList.Add(new FieldModel("历史消费金额", "MemConsumeMoney", "decimal", false));
		FieldList.Add(new FieldModel("电子邮箱", "MemEmail", "string", true));
		FieldList.Add(new FieldModel("地址", "MemAddress", "string", true));
		FieldList.Add(new FieldModel("会员等级", "MemLevelID", "int", false));
		FieldList.Add(new FieldModel("开卡商家", "MemShopID", "int", false));
		FieldList.Add(new FieldModel("办卡日期", "MemCreateTime", "datetime", false));
		FieldList.Add(new FieldModel("备注", "MemRemark", "string", true));
		FieldList.Add(new FieldModel("卡面号码", "MemCardNumber", "string", true));
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataRow[] drCustomField = bllCustomField.GetList(" CustomType=1 ").Tables[0].Select();
		if (drCustomField.Length > 0)
		{
			List<string> _list = new List<string>();
			_list.Add("会员卡号");
			_list.Add("姓名");
			_list.Add("性别");
			_list.Add("身份证号码");
			_list.Add("手机号码");
			_list.Add("固定电话");
			_list.Add("生日");
			_list.Add("积分");
			_list.Add("余额");
			_list.Add("历史消费金额");
			_list.Add("电子邮箱");
			_list.Add("地址");
			_list.Add("会员等级ID");
			_list.Add("开卡商家ID");
			_list.Add("办卡日期");
			_list.Add("备注");
			_list.Add("卡面号码");
			string CustomColumn = string.Empty;
			DataRow[] array = drCustomField;
			for (int k = 0; k < array.Length; k++)
			{
				DataRow dr = array[k];
				int CustomColumn_no = 1;
				CustomColumn = DataExcelInfo.regx.Replace(dr["CustomFieldName"].ToString(), "");
				while (_list.Contains(CustomColumn))
				{
					CustomColumn = string.Format("{0}_{1}", DataExcelInfo.regx.Replace(dr["CustomFieldName"].ToString(), ""), CustomColumn_no);
					CustomColumn_no++;
				}
				_list.Add(CustomColumn);
				FieldList.Add(new FieldModel(CustomColumn, dr["CustomField"].ToString(), dr["CustomFieldType"].ToString(), bool.Parse(dr["CustomFieldIsNull"].ToString())));
			}
		}
		ExcelFile excel = new ExcelFile();
		ExcelWorksheet sheet = excel.Worksheets.Add("会员列表");
		sheet.Cells.GetSubrangeAbsolute(0, 0, 2, FieldList.Count - 1).Merged = true;
		sheet.Cells[0, 0].Style.Font.Size = 500;
		sheet.Cells[0, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.Font.Weight = 800;
		sheet.Cells[0, 0].Value = "会员列表";
		sheet.Cells.GetSubrangeAbsolute(3, 0, 3, FieldList.Count - 1).Merged = true;
		sheet.Cells[3, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[3, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Cells[3, 0].Value = string.Concat(new object[]
		{
			"制表人:",
			Master,
			"                           制表时间:",
			DateTime.Now
		});
		sheet.Rows[3].Height = 400;
		int row = 4;
		int col = 0;
		sheet.Rows[row].Height = 400;
		sheet.Rows[row].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Rows[row].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Rows[row].Style.Font.Color = Color.Red;
		foreach (FieldModel model in FieldList)
		{
			if (!model.FieldIsNull)
			{
				model.FiledName = "* " + model.FiledName;
			}
			sheet.Cells[row, col].Value = model.FiledName;
			sheet.Cells[row, col].Style.Font.Size = 200;
			sheet.Cells[row, col].Style.Font.Weight = 800;
			if (model.FieldType == "string" || model.FieldType == "datetime" || model.FieldType == "text")
			{
				sheet.Columns[col].Width = 5000;
			}
			else if (model.FieldType == "int")
			{
				sheet.Columns[col].Width = 3000;
			}
			else
			{
				sheet.Columns[col].Width = 4000;
			}
			col++;
		}
		int myRows = 5;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			sheet.Cells[myRows, 0].Value = dt.Rows[i]["MemCard"];
			sheet.Cells[myRows, 0].Style.WrapText = true;
			sheet.Cells[myRows, 1].Value = dt.Rows[i]["MemName"];
			sheet.Cells[myRows, 2].Value = ((dt.Rows[i]["MemSex"].ToString() == "True") ? "男" : "女");
			sheet.Cells[myRows, 3].Value = dt.Rows[i]["MemIdentityCard"];
			sheet.Cells[myRows, 4].Value = dt.Rows[i]["MemMobile"];
			sheet.Cells[myRows, 5].Value = dt.Rows[i]["MemTelePhone"];
			DateTime time = DateTime.Parse("1900-01-01");
			string strBirthday;
			if (!DateTime.TryParse(dt.Rows[i]["MemBirthday"].ToString().Trim(), out time))
			{
				strBirthday = "未设置";
			}
			else
			{
				strBirthday = time.ToString("yyyy-MM-dd");
			}
			sheet.Cells[myRows, 6].Value = strBirthday;
			sheet.Cells[myRows, 7].Value = dt.Rows[i]["MemPoint"];
			sheet.Cells[myRows, 8].Value = dt.Rows[i]["MemMoney"];
			sheet.Cells[myRows, 9].Value = ((dt.Rows[i]["MemConsumeMoney"].ToString() == "") ? "0.00" : dt.Rows[i]["MemConsumeMoney"]);
			sheet.Cells[myRows, 10].Value = dt.Rows[i]["MemEmail"];
			sheet.Cells[myRows, 11].Value = dt.Rows[i]["MemAddress"];
			sheet.Cells[myRows, 12].Value = dt.Rows[i]["LevelName"];
			sheet.Cells[myRows, 13].Value = dt.Rows[i]["ShopName"];
			sheet.Cells[myRows, 14].Value = Convert.ToDateTime(dt.Rows[i]["MemCreateTime"].ToString()).ToString("yyyy-MM-dd");
			sheet.Cells[myRows, 15].Value = dt.Rows[i]["MemRemark"];
			sheet.Cells[myRows, 16].Value = dt.Rows[i]["MemCardNumber"].ToString();
			int mycolumns = 17;
			for (int j = 0; j < drCustomField.Length; j++)
			{
				sheet.Cells[myRows, mycolumns].Value = dt.Rows[i][drCustomField[j]["CustomField"].ToString()];
				mycolumns++;
			}
			myRows++;
		}
		try
		{
			string filePath = HttpContext.Current.Server.MapPath("~/Upload/Template/");
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			if (File.Exists(filePath + "MembersList.xls"))
			{
				File.Delete(filePath + "MembersList.xls");
			}
			excel.SaveXls(filePath + "MembersList.xls");
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		HttpResponse response = HttpContext.Current.Response;
		response.ContentType = "application/x-zip-compressed";
		response.AddHeader("Content-Disposition", "attachment;filename=MembersList.xls");
		string filename = HttpContext.Current.Server.MapPath("~/Upload/Template/MembersList.xls");
		response.TransmitFile(filename);
	}

	public static void MemTemplateExcel()
	{
		List<FieldModel> FieldList = new List<FieldModel>();
		FieldList.Add(new FieldModel("会员卡号", "MemCard", "string", false));
		FieldList.Add(new FieldModel("姓名", "MemName", "string", true));
		FieldList.Add(new FieldModel("性别", "MemSex", "int", false));
		FieldList.Add(new FieldModel("身份证号码", "MemIdentityCard", "string", true));
		FieldList.Add(new FieldModel("手机号码", "MemMobile", "string", true));
		FieldList.Add(new FieldModel("固定电话", "CardID", "string", true));
		FieldList.Add(new FieldModel("生日", "MemBirthday", "datetime", true));
		FieldList.Add(new FieldModel("积分", "MemPoint", "int", false));
		FieldList.Add(new FieldModel("余额", "MemMoney", "decimal", false));
		FieldList.Add(new FieldModel("历史消费金额", "MemConsumeMoney", "decimal", false));
		FieldList.Add(new FieldModel("电子邮箱", "MemEmail", "string", true));
		FieldList.Add(new FieldModel("地址", "MemAddress", "string", true));
		FieldList.Add(new FieldModel("会员等级ID", "MemLevelID", "int", false));
		FieldList.Add(new FieldModel("开卡商家ID", "MemShopID", "int", false));
		FieldList.Add(new FieldModel("办卡日期", "MemCreateTime", "datetime", false));
		FieldList.Add(new FieldModel("备注", "MemRemark", "string", true));
		FieldList.Add(new FieldModel("卡面号码", "MemCardNumber", "string", true));
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataRow[] drCustomField = bllCustomField.GetList(" CustomType=1 ").Tables[0].Select();
		if (drCustomField.Length > 0)
		{
			List<string> _list = new List<string>();
			_list.Add("会员卡号");
			_list.Add("姓名");
			_list.Add("性别");
			_list.Add("身份证号码");
			_list.Add("手机号码");
			_list.Add("固定电话");
			_list.Add("生日");
			_list.Add("积分");
			_list.Add("余额");
			_list.Add("历史消费金额");
			_list.Add("电子邮箱");
			_list.Add("地址");
			_list.Add("会员等级ID");
			_list.Add("开卡商家ID");
			_list.Add("办卡日期");
			_list.Add("备注");
			_list.Add("卡面号码");
			string CustomColumn = string.Empty;
			DataRow[] array = drCustomField;
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dr = array[i];
				int CustomColumn_no = 1;
				CustomColumn = DataExcelInfo.regx.Replace(dr["CustomFieldName"].ToString(), "");
				while (_list.Contains(CustomColumn))
				{
					CustomColumn = string.Format("{0}_{1}", DataExcelInfo.regx.Replace(dr["CustomFieldName"].ToString(), ""), CustomColumn_no);
					CustomColumn_no++;
				}
				_list.Add(CustomColumn);
				FieldList.Add(new FieldModel(CustomColumn, dr["CustomField"].ToString(), dr["CustomFieldType"].ToString(), bool.Parse(dr["CustomFieldIsNull"].ToString())));
			}
		}
		ExcelFile excel = new ExcelFile();
		ExcelWorksheet sheet = excel.Worksheets.Add("会员列表");
		sheet.Cells.GetSubrangeAbsolute(0, 0, 2, FieldList.Count - 1).Merged = true;
		sheet.Cells[0, 0].Style.Font.Size = 500;
		sheet.Cells[0, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.Font.Weight = 800;
		sheet.Cells[0, 0].Value = "会员信息批量录入模板";
		StringBuilder sb = new StringBuilder();
		sb.Append("相关数据字典：（★★请严格按照相关格式填写，以免导入错误★★）");
		sb.Append("\n         列名带有\"*\"是必填列;");
		sb.Append("\n         会员卡号：会员卡号长度为3~20位,且只能数字或者英文字母;");
		sb.Append("\n         性别：填写“男”或者“女”;");
		sb.Append("\n         手机号码：只能是11位数字的标准手机号码;");
		sb.Append("\n         固定电话：最好填写为“区号+电话号码”，例：075529755361;");
		sb.Append("\n         生日：必须填写为“年-月-日”，例：1990-12-27，如果未留请填写：1900-1-1;");
		sb.Append("\n         “积分”和“余额”不能为空, 如果为空请填写“0”;");
		Chain.BLL.MemLevel bllLevel = new Chain.BLL.MemLevel();
		DataTable dtLevel = bllLevel.GetList("").Tables[0];
		sb.Append("\n         会员等级ID：");
		foreach (DataRow dr in dtLevel.Rows)
		{
			
			sb.Append(dr["LevelID"].ToString() + "-" + dr["LevelName"].ToString() + "  ");
		}
		Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
		DataTable dtShop = bllShop.GetList("ShopID>0").Tables[0];
		sb.Append(";\n         开卡商家ID：");
		foreach (DataRow dr in dtShop.Rows)
		{
			
			sb.Append(dr["ShopID"].ToString() + "-" + dr["ShopName"].ToString() + "  ");
		}
		sb.Append(";\n         办卡日期：必须填写为“年-月-日”，例：2013-1-27;");
		sheet.Cells.GetSubrangeAbsolute(3, 0, 3, FieldList.Count - 1).Merged = true;
		sheet.Cells[3, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[3, 0].Value = sb.ToString();
		sheet.Rows[3].Height = 3000;
		int row = 4;
		int col = 0;
		sheet.Rows[row].Height = 400;
		sheet.Rows[row].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Rows[row].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Rows[row].Style.Font.Color = Color.Red;
		foreach (FieldModel model in FieldList)
		{
			if (!model.FieldIsNull)
			{
				model.FiledName = "* " + model.FiledName;
			}
			sheet.Cells[row, col].Value = model.FiledName;
			sheet.Cells[row, col].Style.Font.Size = 200;
			sheet.Cells[row, col].Style.Font.Weight = 800;
			if (model.FieldType == "string" || model.FieldType == "datetime" || model.FieldType == "text")
			{
				sheet.Columns[col].Width = 5000;
			}
			else if (model.FieldType == "int")
			{
				sheet.Columns[col].Width = 3000;
			}
			else
			{
				sheet.Columns[col].Width = 4000;
			}
			col++;
		}
		sheet.Cells[5, 0].Value = "200020";
		sheet.Cells[5, 1].Value = "林俊杰";
		sheet.Cells[5, 2].Value = "男";
		sheet.Cells[5, 3].Value = "";
		sheet.Cells[5, 4].Value = "";
		sheet.Cells[5, 5].Value = "";
		sheet.Cells[5, 6].Value = "1990-12-27";
		sheet.Cells[5, 7].Value = 0;
		sheet.Cells[5, 8].Value = 0;
		sheet.Cells[5, 9].Value = 0;
		sheet.Cells[5, 10].Value = "";
		sheet.Cells[5, 11].Value = "";
		sheet.Cells[5, 12].Value = 0;
		sheet.Cells[5, 13].Value = 1;
		sheet.Cells[5, 14].Value = "2013-05-15";
		sheet.Cells[5, 15].Value = "";
		sheet.Cells[5, 16].Value = "";
		try
		{
			string filePath = HttpContext.Current.Server.MapPath("~/Upload/Template/");
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			if (File.Exists(filePath + "Members.xls"))
			{
				File.Delete(filePath + "Members.xls");
			}
			excel.SaveXls(filePath + "Members.xls");
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		HttpResponse response = HttpContext.Current.Response;
		response.ContentType = "application/x-zip-compressed";
		response.AddHeader("Content-Disposition", "attachment;filename=Members.xls");
		string filename = HttpContext.Current.Server.MapPath("~/Upload/Template/Members.xls");
		response.TransmitFile(filename);
	}

	public static DataTable CheckMemDataExcel(string strSavePath, ref string esr)
	{
		Dictionary<string, SqlMapModel> SqlMap = new Dictionary<string, SqlMapModel>();
		DataTable dtMem = new DataTable();
		SqlMap.Add("会员卡号", new SqlMapModel("MemCard", typeof(string)));
		SqlMap.Add("姓名", new SqlMapModel("MemName", typeof(string)));
		SqlMap.Add("性别", new SqlMapModel("MemSex", typeof(int)));
		SqlMap.Add("身份证号码", new SqlMapModel("MemIdentityCard", typeof(string)));
		SqlMap.Add("手机号码", new SqlMapModel("MemMobile", typeof(string)));
		SqlMap.Add("固定电话", new SqlMapModel("MemTelePhone", typeof(string)));
		SqlMap.Add("生日", new SqlMapModel("MemBirthday", typeof(DateTime)));
		SqlMap.Add("积分", new SqlMapModel("MemPoint", typeof(int)));
		SqlMap.Add("余额", new SqlMapModel("MemMoney", typeof(float)));
		SqlMap.Add("历史消费金额", new SqlMapModel("MemConsumeMoney", typeof(float)));
		SqlMap.Add("电子邮箱", new SqlMapModel("MemEmail", typeof(string)));
		SqlMap.Add("地址", new SqlMapModel("MemAddress", typeof(string)));
		SqlMap.Add("会员等级ID", new SqlMapModel("MemLevelID", typeof(int)));
		SqlMap.Add("开卡商家ID", new SqlMapModel("MemShopID", typeof(int)));
		SqlMap.Add("办卡日期", new SqlMapModel("MemCreateTime", typeof(DateTime)));
		SqlMap.Add("备注", new SqlMapModel("MemRemark", typeof(string)));
		SqlMap.Add("卡面号码", new SqlMapModel("MemCardNumber", typeof(string)));
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataRow[] drCustomField = bllCustomField.GetList(" CustomType=1 ").Tables[0].Select();
		string CustomColumn = string.Empty;
		DataRow[] array = drCustomField;
		for (int j = 0; j < array.Length; j++)
		{
			DataRow dr = array[j];
			int CustomColumn_no = 1;
			CustomColumn = DataExcelInfo.regx.Replace(dr["CustomFieldName"].ToString(), "");
			while (SqlMap.ContainsKey(CustomColumn))
			{
				CustomColumn = string.Format("{0}_{1}", DataExcelInfo.regx.Replace(dr["CustomFieldName"].ToString(), ""), CustomColumn_no);
				CustomColumn_no++;
			}
			SqlMap.Add(CustomColumn, new SqlMapModel(DataExcelInfo.regx.Replace(dr["CustomField"].ToString(), ""), typeof(string)));
		}
		try
		{
			dtMem = DataExcelInOfNPOI.getExcelDataTable(strSavePath, SqlMap, ref esr);
		}
		catch (Exception err)
		{
			string i = err.ToString();
		}
		return dtMem;
	}

	public static bool MemImport(DataTable dtMem, bool bolPwd, int intUserID)
	{
		StringBuilder strSqlColumn = new StringBuilder();
		StringBuilder strSqlValue = new StringBuilder();
		strSqlColumn.Append("Insert into Mem(MemCard,MemName,MemSex,MemIdentityCard,MemMobile,MemTelePhone,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemMoney,MemConsumeMoney,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemCardNumber,MemUserID");
		strSqlValue.Append(" values ('{0}','{1}',{2},'{3}','{4}','{5}',1,'{6}',0,'2900-1-1',{7},{8},{9},'{10}','{11}',0,0,{12},{13},'{14}','{15}','{16}'," + intUserID);
		DataTable dthyid = new Chain.BLL.MemLevel().GetAllList().Tables[0];
		DataTable dtdpid = new Chain.BLL.SysShop().GetAllList().Tables[0];
		strSqlColumn.Append(",MemPassword");
		if (bolPwd)
		{
			strSqlValue.Append(",'" + DESEncrypt.Encrypt("123456") + "'");
		}
		else
		{
			strSqlValue.Append(",'" + DESEncrypt.Encrypt("") + "'");
		}
		int intNumber = 16;
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataRow[] drCustomField = bllCustomField.GetList("  CustomType=1 ").Tables[0].Select();
		if (drCustomField != null)
		{
			DataRow[] array = drCustomField;
			for (int k = 0; k < array.Length; k++)
			{
				DataRow dr = array[k];
				intNumber++;
				strSqlColumn.Append("," + dr["CustomField"].ToString());
				strSqlValue.Append(",'{" + intNumber + "}'");
			}
		}
		strSqlColumn.Append(")");
		strSqlValue.Append(")");
		bool result;
		if (intNumber + 2 != dtMem.Columns.Count)
		{
			result = false;
		}
		else
		{
			ArrayList sqlArray = new ArrayList();
			ArrayList sqlArr = new ArrayList();
			ArrayList sqlPointArr = new ArrayList();
			for (int i = 0; i < dtMem.Rows.Count; i++)
			{
				string strAllSql = strSqlColumn.ToString() + strSqlValue.ToString();
				foreach (DataColumn dc in dtMem.Columns)
				{
					if (dtMem.Rows[i].IsNull(0) || dtMem.Rows[i][0].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{0}", dtMem.Rows[i][0].ToString());
					strAllSql = strAllSql.Replace("{1}", dtMem.Rows[i][1].ToString());
					if (dtMem.Rows[i][2].ToString() == "女")
					{
						strAllSql = strAllSql.Replace("{2}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{2}", "1");
					}
					strAllSql = strAllSql.Replace("{3}", dtMem.Rows[i][3].ToString());
					strAllSql = strAllSql.Replace("{4}", dtMem.Rows[i][4].ToString());
					strAllSql = strAllSql.Replace("{5}", dtMem.Rows[i][5].ToString());
					if (dtMem.Rows[i][6].ToString() == "")
					{
						strAllSql = strAllSql.Replace("'{6}'", "1900-01-01");
					}
					else
					{
						if (dtMem.Rows[i][6].ToString() == "未设置")
						{
							dtMem.Rows[i][6] = "1900-01-01";
						}
						strAllSql = strAllSql.Replace("{6}", DateTime.Parse(dtMem.Rows[i][6].ToString()).ToShortDateString());
					}
					if (dtMem.Rows[i][7].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{7}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{7}", dtMem.Rows[i][7].ToString());
					}
					if (dtMem.Rows[i][8].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{8}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{8}", dtMem.Rows[i][8].ToString());
					}
					if (dtMem.Rows[i][9].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{9}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{9}", dtMem.Rows[i][9].ToString());
					}
					strAllSql = strAllSql.Replace("{10}", dtMem.Rows[i][10].ToString());
					strAllSql = strAllSql.Replace("{11}", dtMem.Rows[i][11].ToString());
					if (dtMem.Rows[i][12].ToString() == "")
					{
						result = false;
						return result;
					}
					int myID;
					DataRow[] drlevel;
					if (int.TryParse(dtMem.Rows[i][12].ToString(), out myID))
					{
						drlevel = dthyid.Select(string.Format(" LevelID = '{0}'", dtMem.Rows[i][12].ToString()));
						if (drlevel.Length <= 0)
						{
							drlevel = dthyid.Select(string.Format(" LevelName = '{0}'", dtMem.Rows[i][12].ToString()));
						}
					}
					else
					{
						drlevel = dthyid.Select(string.Format(" LevelName = '{0}'", dtMem.Rows[i][12].ToString()));
					}
					if (drlevel.Length <= 0)
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{12}", drlevel[0]["LevelID"].ToString());
					if (dtMem.Rows[i][13].ToString() == "")
					{
						result = false;
						return result;
					}
					string ShopID = DataExcelInfo.GetShopID(dtMem.Rows[i][13].ToString(), dtdpid);
					if (!(ShopID != ""))
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{13}", ShopID);
					if (dtMem.Rows[i][14].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{14}", DateTime.Parse(dtMem.Rows[i][14].ToString()).ToShortDateString());
					strAllSql = strAllSql.Replace("{15}", dtMem.Rows[i][15].ToString());
					strAllSql = strAllSql.Replace("{16}", dtMem.Rows[i][16].ToString());
					intNumber = 16;
					if (drCustomField != null)
					{
						DataRow[] array = drCustomField;
						for (int k = 0; k < array.Length; k++)
						{
							DataRow dr = array[k];
							intNumber++;
							strAllSql = strAllSql.Replace("{" + intNumber + "}", dtMem.Rows[i][intNumber].ToString());
						}
					}
				}
				sqlArray.Add(strAllSql);
			}
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			bllMem.ExeclDataInput(sqlArray);
			StringBuilder strReacharSql = new StringBuilder();
			strReacharSql.Append("Insert into MemRecharge(RechargeMemID,RechargeAccount,RechargeType,RechargeMoney,RechargeGive,RechargeRemark,RechargeShopID,RechargeCreateTime,RechargeUserID,RechargeIsApprove)");
			strReacharSql.Append(string.Concat(new object[]
			{
				" values({0},'",
				PubFunction.curParameter.strMemRechargePrefix,
				DateTime.Now.ToString("yyMMddHHmmssffff"),
				"',1,{1},0,'{2}',{3},'",
				DateTime.Now,
				"',",
				intUserID,
				",1)"
			}));
			StringBuilder strPointSql = new StringBuilder();
			strPointSql.Append("Insert into PointLog(PointMemID,PointNumber,PointChangeType,PointRemark,PointShopID,PointCreateTime,PointUserID,PointOrderCode)");
			strPointSql.Append(string.Concat(new object[]
			{
				" values({0},{1},5,'{2}',{3},'",
				DateTime.Now,
				"',",
				intUserID,
				",'",
				PubFunction.curParameter.strMemPointChangePrefix,
				DateTime.Now.ToString("yyMMddHHmmssffff"),
				"')"
			}));
			Chain.Model.SysUser modelUser = new Chain.BLL.SysUser().GetModel(intUserID);
			int j = 0;
			while (j < dtMem.Rows.Count)
			{
				if (dtMem.Rows[j][7].ToString() != "" && int.Parse(dtMem.Rows[j][7].ToString()) > 0)
				{
					try
					{
						string ShopID = DataExcelInfo.GetShopID(dtMem.Rows[j][13].ToString(), dtdpid);
						if (string.IsNullOrEmpty(ShopID))
						{
							goto IL_CE2;
						}
						string strSql = strPointSql.ToString();
						Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModelByMemCard(dtMem.Rows[j][0].ToString());
						strSql = strSql.Replace("{0}", modelMem.MemID.ToString());
						strSql = strSql.Replace("{1}", dtMem.Rows[j][7].ToString());
						strSql = strSql.Replace("{2}", "会员导入增加积分，积分变动：" + dtMem.Rows[j][7].ToString());
						strSql = strSql.Replace("{3}", ShopID);
						sqlArr.Add(strSql.ToString());
					}
					catch
					{
					}
					goto IL_B81;
				}
				goto IL_B81;
				IL_CE2:
				j++;
				continue;
				IL_B81:
				if (dtMem.Rows[j][8].ToString() != "" && Convert.ToDecimal(dtMem.Rows[j][8].ToString()) > 0m)
				{
					try
					{
						string ShopID = DataExcelInfo.GetShopID(dtMem.Rows[j][13].ToString(), dtdpid);
						if (!string.IsNullOrEmpty(ShopID))
						{
							string strSql = strReacharSql.ToString();
							Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModelByMemCard(dtMem.Rows[j][0].ToString());
							strSql = strSql.Replace("{0}", modelMem.MemID.ToString());
							strSql = strSql.Replace("{1}", dtMem.Rows[j][8].ToString());
							strSql = strSql.Replace("{2}", "会员导入金额充值：充值金额：" + dtMem.Rows[j][8].ToString());
							strSql = strSql.Replace("{3}", ShopID);
							sqlArr.Add(strSql.ToString());
						}
					}
					catch
					{
					}
				}
				goto IL_CE2;
			}
			result = bllMem.ExeclDataInput(sqlArr);
		}
		return result;
	}

	public static string GetShopID(string strShopID, DataTable dtdpid)
	{
		string ShopID = "";
		if (strShopID != "")
		{
			int myID;
			DataRow[] drSysShop;
			if (int.TryParse(strShopID, out myID))
			{
				drSysShop = dtdpid.Select(string.Format(" ShopID = '{0}' ", strShopID));
				if (drSysShop.Length <= 0)
				{
					drSysShop = dtdpid.Select(string.Format(" ShopName = '{0}' ", strShopID));
				}
			}
			else
			{
				drSysShop = dtdpid.Select(string.Format(" ShopName = '{0}' ", strShopID));
			}
			if (drSysShop.Length > 0)
			{
				ShopID = drSysShop[0]["ShopID"].ToString();
			}
		}
		return ShopID;
	}

	public static void GoodsTemplateExcel(int ShopID)
	{
		List<FieldModel> FieldList = new List<FieldModel>();
		FieldList.Add(new FieldModel("商品编码", "GoodsCode", "string", false));
		FieldList.Add(new FieldModel("商品名称", "Name", "string", false));
		FieldList.Add(new FieldModel("商品简码", "NameCode", "string", true));
		FieldList.Add(new FieldModel("商品分类ID", "GoodsClassID", "int", false));
		FieldList.Add(new FieldModel("计量单位", "Unit", "string", false));
		FieldList.Add(new FieldModel("参考进价", "GoodsBidPrice", "decimal", true));
		FieldList.Add(new FieldModel("零售单价", "Price", "decimal", false));
		FieldList.Add(new FieldModel("商品积分", "Point", "int", true));
		FieldList.Add(new FieldModel("商品类型", "GoodsType", "int", false));
		FieldList.Add(new FieldModel("最低折扣", "MinPercent", "decimal", true));
		FieldList.Add(new FieldModel("提成类型", "CommissionType", "int", true));
		FieldList.Add(new FieldModel("提成金额(比例)", "CommissionNumber", "decimal", true));
		FieldList.Add(new FieldModel("商品简介", "GoodsRemark", "string", true));
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataRow[] drCustomField = bllCustomField.GetList(" CustomType=2 ").Tables[0].Select();
		DataRow[] array = drCustomField;
		for (int i = 0; i < array.Length; i++)
		{
			DataRow dr = array[i];
			FieldList.Add(new FieldModel(dr["CustomFieldName"].ToString(), dr["CustomField"].ToString(), dr["CustomFieldType"].ToString(), bool.Parse(dr["CustomFieldIsNull"].ToString())));
		}
		ExcelFile excel = new ExcelFile();
		ExcelWorksheet sheet = excel.Worksheets.Add("商品列表");
		sheet.Cells.GetSubrangeAbsolute(0, 0, 2, FieldList.Count - 1).Merged = true;
		sheet.Cells[0, 0].Style.Font.Size = 500;
		sheet.Cells[0, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.Font.Weight = 800;
		sheet.Cells[0, 0].Value = "商品信息批量录入模板";
		StringBuilder sb = new StringBuilder();
		sb.Append("相关数据字典：（★★请严格按照相关格式填写，以免导入错误★★）");
		sb.Append("\n         列名带有\"*\"是必填列;");
		sb.Append("\n         商品编码：只能是长度为5～25位字符(数字、字母、字母数字组合);");
		DataTable dtGoodsClass = new Chain.BLL.GoodsClass().GetListByShopID(ShopID).Tables[0];
		sb.Append("\n         商品分类ID：");
		foreach (DataRow dr in dtGoodsClass.Rows)
		{
		
			if (int.Parse(dr["ParentID"].ToString()) != 0)
			{
				sb.Append(dr["ClassID"].ToString() + "-" + dr["ClassName"].ToString() + " ");
			}
			else
			{
				DataTable dtClass = new Chain.BLL.GoodsClass().GetList("ParentID=" + dr["ClassID"]).Tables[0];
				if (dtClass.Rows.Count == 0)
				{
					sb.Append(dr["ClassID"].ToString() + "-" + dr["ClassName"].ToString() + " ");
				}
			}
		}
		DataTable dtGoodsClassAll = new Chain.BLL.GoodsClass().GetList(" ParentID=0").Tables[0];
		sb.Append("\n         商品积分：填写0或正整数");
		sb.Append("\n         商品类型：0-普通商品 1-服务项目;");
		sb.Append("\n         最低折扣：填写0～1之间的小数;");
		sb.Append("\n         提成类型：1-按固定比例提成  2-按固定金额提成;");
		sb.Append("\n         提成金额(比例)： 当'提成类型ID'为1时，'提成金额(比例)'填0～1的小数; 当'提成类型ID'为2时，'提成金额(比例)'填整数;");
		sheet.Cells.GetSubrangeAbsolute(3, 0, 3, FieldList.Count - 1).Merged = true;
		sheet.Cells[3, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[3, 0].Value = sb.ToString();
		sheet.Rows[3].Height = 2500;
		int row = 4;
		int col = 0;
		sheet.Rows[row].Height = 400;
		sheet.Rows[row].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Rows[row].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Rows[row].Style.Font.Color = Color.Red;
		foreach (FieldModel model in FieldList)
		{
			if (!model.FieldIsNull)
			{
				model.FiledName = "* " + model.FiledName;
			}
			sheet.Cells[row, col].Value = model.FiledName;
			sheet.Cells[row, col].Style.Font.Size = 200;
			sheet.Cells[row, col].Style.Font.Weight = 800;
			if (model.FieldType == "string" || model.FieldType == "datetime" || model.FieldType == "text")
			{
				sheet.Columns[col].Width = 5000;
			}
			else if (model.FieldType == "int")
			{
				sheet.Columns[col].Width = 3000;
			}
			else
			{
				sheet.Columns[col].Width = 4000;
			}
			col++;
		}
		try
		{
			string filePath = HttpContext.Current.Server.MapPath("~/Upload/Template/");
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			if (File.Exists(filePath + "Goods.xls"))
			{
				File.Delete(filePath + "Goods.xls");
			}
			excel.SaveXls(filePath + "Goods.xls");
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		HttpResponse response = HttpContext.Current.Response;
		response.ContentType = "application/x-zip-compressed";
		response.AddHeader("Content-Disposition", "attachment;filename=Goods.xls");
		string filename = HttpContext.Current.Server.MapPath("~/Upload/Template/Goods.xls");
		response.TransmitFile(filename);
	}

	public static void GoodsNumberTemplateExcel(int UserShopID)
	{
		List<FieldModel> FieldList = new List<FieldModel>();
		FieldList.Add(new FieldModel("商品编码", "GoodsCode", "string", false));
		FieldList.Add(new FieldModel("商品名称", "Name", "string", false));
		FieldList.Add(new FieldModel("商品库存", "Number", "int", false));
		ExcelFile excel = new ExcelFile();
		ExcelWorksheet sheet = excel.Worksheets.Add("库存列表");
		sheet.Cells.GetSubrangeAbsolute(0, 0, 2, FieldList.Count - 1).Merged = true;
		sheet.Cells[0, 0].Style.Font.Size = 500;
		sheet.Cells[0, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.Font.Weight = 800;
		sheet.Cells[0, 0].Value = "商品库存批量录入模板";
		StringBuilder sb = new StringBuilder();
		sb.Append("相关数据字典：（★★请严格按照相关格式填写，以免导入错误★★）");
		sb.Append("\n         列名带有\"*\"是必填列;");
		sb.Append("\n         商品编码：只能是长度为5～25位数字;");
		if (PubFunction.ProductDecimalNum == 0)
		{
			sb.Append("\n         商品库存：填写0或正整数");
		}
		else
		{
			sb.AppendFormat("\n         商品库存：填写0或正整数或大于0的小数位不超{0}位的小数", PubFunction.ProductDecimalNum);
		}
		Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
		DataTable dtShop = bllShop.GetList("ShopID>0").Tables[0];
		sheet.Cells.GetSubrangeAbsolute(3, 0, 3, FieldList.Count - 1).Merged = true;
		sheet.Cells[3, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[3, 0].Value = sb.ToString();
		sheet.Rows[3].Height = 2500;
		int row = 4;
		int col = 0;
		sheet.Rows[row].Height = 400;
		sheet.Rows[row].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Rows[row].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Rows[row].Style.Font.Color = Color.Red;
		foreach (FieldModel model in FieldList)
		{
			if (!model.FieldIsNull)
			{
				model.FiledName = "* " + model.FiledName;
			}
			sheet.Cells[row, col].Value = model.FiledName;
			sheet.Cells[row, col].Style.Font.Size = 200;
			sheet.Cells[row, col].Style.Font.Weight = 800;
			if (model.FieldType == "string" || model.FieldType == "datetime" || model.FieldType == "text")
			{
				sheet.Columns[col].Width = 5000;
			}
			else if (model.FieldType == "int")
			{
				sheet.Columns[col].Width = 3000;
			}
			else
			{
				sheet.Columns[col].Width = 4000;
			}
			col++;
		}
		Chain.BLL.GoodsNumber BLLGoodsNumber = new Chain.BLL.GoodsNumber();
		DataSet ds = BLLGoodsNumber.GetAllGoodsidByShopID(UserShopID);
		string GoodsID = "";
		if (ds.Tables[0].Rows.Count > 0)
		{
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				GoodsID = GoodsID + ds.Tables[0].Rows[i][0].ToString() + ",";
			}
			if (GoodsID.Substring(GoodsID.Length - 1, 1) == ",")
			{
				GoodsID = GoodsID.Remove(GoodsID.LastIndexOf(","), 1);
			}
			string sqlWhere = "GoodsID  in (" + GoodsID + ")  and  GoodsType=0 ";
			DataTable dt = new Chain.BLL.Goods().GetList(sqlWhere).Tables[0];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				sheet.Cells[5 + i, 0].Value = dt.Rows[i]["GoodsCode"];
				sheet.Cells[5 + i, 1].Value = dt.Rows[i]["Name"];
			}
		}
		try
		{
			string filePath = HttpContext.Current.Server.MapPath("~/Upload/Template/");
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			if (File.Exists(filePath + "GoodsNumber.xls"))
			{
				File.Delete(filePath + "GoodsNumber.xls");
			}
			excel.SaveXls(filePath + "GoodsNumber.xls");
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		HttpResponse response = HttpContext.Current.Response;
		response.ContentType = "application/x-zip-compressed";
		response.AddHeader("Content-Disposition", "attachment;filename=GoodsNumber.xls");
		string filename = HttpContext.Current.Server.MapPath("~/Upload/Template/GoodsNumber.xls");
		response.TransmitFile(filename);
	}

	public static DataTable CheckGoodsDataExcel(string strSavePath)
	{
		DataTable dtGoods = new DataTable();
		Dictionary<string, SqlMapModel> SqlMap = new Dictionary<string, SqlMapModel>();
		SqlMap.Add("商品编码", new SqlMapModel("GoodsCode", typeof(string)));
		SqlMap.Add("商品名称", new SqlMapModel("Name", typeof(string)));
		SqlMap.Add("商品简码", new SqlMapModel("NameCode", typeof(string)));
		SqlMap.Add("商品分类ID", new SqlMapModel("GoodsClassID", typeof(int)));
		SqlMap.Add("计量单位", new SqlMapModel("Unit", typeof(string)));
		SqlMap.Add("参考进价", new SqlMapModel("GoodsBidPrice", typeof(float)));
		SqlMap.Add("零售单价", new SqlMapModel("Price", typeof(float)));
		SqlMap.Add("商品积分", new SqlMapModel("Point", typeof(int)));
		SqlMap.Add("商品类型", new SqlMapModel("GoodsType", typeof(string)));
		SqlMap.Add("最低折扣", new SqlMapModel("MinPercent", typeof(float)));
		SqlMap.Add("提成类型", new SqlMapModel("CommissionType", typeof(string)));
		SqlMap.Add("提成金额(比例)", new SqlMapModel("CommissionNumber", typeof(float)));
		SqlMap.Add("商品简介", new SqlMapModel("GoodsRemark", typeof(string)));
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataTable dtCustomField = bllCustomField.GetList(" CustomType=2 ").Tables[0];
		Regex regx = new Regex("\\s");
		foreach (DataRow dr in dtCustomField.Rows)
		{
			SqlMap.Add(regx.Replace(dr["CustomFieldName"].ToString(), ""), new SqlMapModel(regx.Replace(dr["CustomField"].ToString(), ""), typeof(string)));
		}
		DataTable result;
		try
		{
			dtGoods = (result = DataExcelInOfNPOI.getExcelDataTable(strSavePath, SqlMap));
			return result;
		}
		catch
		{
		}
		result = dtGoods;
		return result;
	}

	public static DataTable CheckGoodsNumDataExcel(string strSavePath)
	{
		DataTable dtGoods = new DataTable();
		Dictionary<string, SqlMapModel> SqlMap = new Dictionary<string, SqlMapModel>();
		SqlMap.Add("商品编码", new SqlMapModel("GoodsCode", typeof(string)));
		SqlMap.Add("商品名称", new SqlMapModel("Name", typeof(string)));
		SqlMap.Add("商品库存", new SqlMapModel("Number", typeof(int)));
		DataTable result;
		try
		{
			dtGoods = (result = DataExcelInOfNPOI.getExcelDataTable(strSavePath, SqlMap));
			return result;
		}
		catch
		{
		}
		result = dtGoods;
		return result;
	}

	public static bool GoodsImport(DataTable dtGoods, int intUserID, int ShopID)
	{
		StringBuilder strSqlColumn = new StringBuilder();
		StringBuilder strSqlValue = new StringBuilder();
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataRow[] drCustomField = bllCustomField.GetList(" CustomType=2 ").Tables[0].Select();
		strSqlColumn.Append("Insert into Goods(GoodsCode,GoodsClassID,Name,NameCode,Unit,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsCreateTime,CreateShopID,GoodsNumber,SalePercet,GoodsSaleNumber,GoodsPicture");
		strSqlValue.Append(string.Concat(new object[]
		{
			" values ('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},'{12}','",
			DateTime.Now.ToString(),
			"',",
			ShopID,
			",0,0,0,''"
		}));
		int intNumber = 12;
		if (drCustomField != null)
		{
			DataRow[] array = drCustomField;
			for (int j = 0; j < array.Length; j++)
			{
				DataRow dr = array[j];
				intNumber++;
				strSqlColumn.Append("," + dr["CustomField"].ToString());
				strSqlValue.Append(",'{" + intNumber + "}'");
			}
		}
		strSqlColumn.Append(")");
		strSqlValue.Append(")");
		bool result;
		if (intNumber + 2 != dtGoods.Columns.Count)
		{
			result = false;
		}
		else
		{
			ArrayList sqlArray = new ArrayList();
			for (int i = 0; i < dtGoods.Rows.Count; i++)
			{
				string strAllSql = strSqlColumn.ToString() + strSqlValue.ToString();
				foreach (DataColumn dc in dtGoods.Columns)
				{
					if (dtGoods.Rows[i].IsNull(0) || dtGoods.Rows[i][0].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{0}", dtGoods.Rows[i][0].ToString());
					if (dtGoods.Rows[i].IsNull(3) || dtGoods.Rows[i][3].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{1}", dtGoods.Rows[i][3].ToString());
					if (dtGoods.Rows[i].IsNull(1) || dtGoods.Rows[i][1].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{2}", dtGoods.Rows[i][1].ToString());
					if (dtGoods.Rows[i].IsNull(2) || dtGoods.Rows[i][2].ToString() == "")
					{
						string GoodsCode = StringPlus.ConvertToFirstPinYin(dtGoods.Rows[i][1].ToString());
						strAllSql = strAllSql.Replace("{3}", GoodsCode);
					}
					else
					{
						strAllSql = strAllSql.Replace("{3}", dtGoods.Rows[i][2].ToString());
					}
					if (dtGoods.Rows[i].IsNull(4) || dtGoods.Rows[i][4].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{4}", dtGoods.Rows[i][4].ToString());
					if (dtGoods.Rows[i].IsNull(6) || dtGoods.Rows[i][6].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{5}", dtGoods.Rows[i][6].ToString());
					if (dtGoods.Rows[i].IsNull(10) || dtGoods.Rows[i][10].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{6}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{6}", dtGoods.Rows[i][10].ToString());
					}
					if (dtGoods.Rows[i].IsNull(11) || dtGoods.Rows[i][11].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{7}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{7}", dtGoods.Rows[i][11].ToString());
					}
					if (dtGoods.Rows[i].IsNull(7) || dtGoods.Rows[i][7].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{8}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{8}", dtGoods.Rows[i][7].ToString());
					}
					if (dtGoods.Rows[i].IsNull(9) || dtGoods.Rows[i][9].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{9}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{9}", dtGoods.Rows[i][9].ToString());
					}
					if (dtGoods.Rows[i].IsNull(8) || dtGoods.Rows[i][8].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{10}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{10}", dtGoods.Rows[i][8].ToString());
					}
					if (dtGoods.Rows[i].IsNull(5) || dtGoods.Rows[i][5].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{11}", "0");
					}
					else
					{
						strAllSql = strAllSql.Replace("{11}", dtGoods.Rows[i][5].ToString());
					}
					if (dtGoods.Rows[i].IsNull(12) || dtGoods.Rows[i][12].ToString() == "")
					{
						strAllSql = strAllSql.Replace("{12}", " ");
					}
					else
					{
						strAllSql = strAllSql.Replace("{12}", dtGoods.Rows[i][12].ToString());
					}
					intNumber = 12;
					if (drCustomField != null)
					{
						DataRow[] array = drCustomField;
						for (int j = 0; j < array.Length; j++)
						{
							DataRow dr = array[j];
							intNumber++;
							strAllSql = strAllSql.Replace("{" + intNumber + "}", dtGoods.Rows[i][intNumber].ToString());
						}
					}
				}
				sqlArray.Add(strAllSql);
			}
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			result = bllGoods.ExeclDataInput(sqlArray);
		}
		return result;
	}

	public static bool GoodsImportNums(DataTable dtGoods, int intUserID, int ShopID)
	{
		StringBuilder strSqlColumn = new StringBuilder();
		StringBuilder strSqlValue = new StringBuilder();
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataRow[] drCustomField = bllCustomField.GetList(" CustomType=2 ").Tables[0].Select();
		strSqlColumn.Append("Update GoodsNumber set Number=Number+{2} where GoodsID={0} and ShopID={1}");
		int intNumber = 3;
		bool result;
		if (intNumber + 1 != dtGoods.Columns.Count)
		{
			result = false;
		}
		else
		{
			ArrayList sqlArray = new ArrayList();
			Chain.BLL.Goods BLLgoods = new Chain.BLL.Goods();
			Chain.Model.GoodsLog modelGoodsLog = new Chain.Model.GoodsLog();
			Chain.BLL.GoodsLog bllGoodsLog = new Chain.BLL.GoodsLog();
			Chain.Model.GoodsNumber modelNumber = new Chain.Model.GoodsNumber();
			Chain.BLL.GoodsNumber bllNumber = new Chain.BLL.GoodsNumber();
			Chain.Model.GoodsLogDetail modelGoodsDetail = new Chain.Model.GoodsLogDetail();
			Chain.BLL.GoodsLogDetail bllGoodsDetail = new Chain.BLL.GoodsLogDetail();
			string Account = "PLDR" + DateTime.Now.ToString("yyMMddHHmmssffff");
			DateTime GoodsLogCreateTime = DateTime.Now;
			modelGoodsLog.GoodsAccount = Account;
			modelGoodsLog.Type = 14;
			modelGoodsLog.TotalPrice = 0m;
			modelGoodsLog.Remark = "商品库存批量导入";
			modelGoodsLog.CreateTime = GoodsLogCreateTime;
			modelGoodsLog.ShopID = ShopID;
			modelGoodsLog.UserID = intUserID;
			modelGoodsLog.ChangeShopID = ShopID;
			int intLog = bllGoodsLog.Add(modelGoodsLog);
			if (intLog > 0)
			{
				PubFunction.SaveSysLog(intUserID, 4, "商品入库", string.Concat(new object[]
				{
					"商品批量入库,入库单号:[",
					Account,
					"],入库时间:[",
					GoodsLogCreateTime,
					"]"
				}), intUserID, DateTime.Now, PubFunction.ipAdress);
			}
			for (int i = 0; i < dtGoods.Rows.Count; i++)
			{
				modelGoodsDetail.GoodsLogID = intLog;
				modelGoodsDetail.GoodsID = BLLgoods.GetGoodsID(dtGoods.Rows[i]["GoodsCode"].ToString());
				modelGoodsDetail.GoodsInPrice = 0m;
				modelGoodsDetail.GoodsOutPrice = 0m;
				modelGoodsDetail.GoodsNumber = Convert.ToDecimal(dtGoods.Rows[i]["Number"]);
				int flag = bllGoodsDetail.Add(modelGoodsDetail);
			}
			for (int i = 0; i < dtGoods.Rows.Count; i++)
			{
				string strAllSql = strSqlColumn.ToString() + strSqlValue.ToString();
				foreach (DataColumn dc in dtGoods.Columns)
				{
					if (dtGoods.Rows[i].IsNull(0) || dtGoods.Rows[i][0].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{0}", PubFunction.GoodsCodeToGoodsID(dtGoods.Rows[i][0].ToString()).ToString());
					strAllSql = strAllSql.Replace("{1}", ShopID.ToString());
					if (dtGoods.Rows[i].IsNull(2) || dtGoods.Rows[i][2].ToString() == "")
					{
						result = false;
						return result;
					}
					strAllSql = strAllSql.Replace("{2}", dtGoods.Rows[i][2].ToString());
				}
				sqlArray.Add(strAllSql);
			}
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			result = bllGoods.ExeclDataInput(sqlArray);
		}
		return result;
	}

	public static void GoodsNumberImport(DataTable dtGoods, int intShopID)
	{
		for (int i = 0; i < dtGoods.Rows.Count; i++)
		{
			Chain.BLL.GoodsNumber bllNumber = new Chain.BLL.GoodsNumber();
			bllNumber.InsertGoodsNumber(PubFunction.GoodsCodeToGoodsID(dtGoods.Rows[i][0].ToString()), intShopID);
		}
	}

	public static void RptMemReportExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>会员信息统计列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>会员卡号</td><td>会员姓名</td><td>手机号码</td><td>会员金额</td><td>账户积分</td><td>会员等级</td><td>商家</td><td>日期</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemMobile"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemPoint"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["LevelName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCreateTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
		}
		str.Append("<tr><td colspan='10'><h4>会员总数:" + dt.Rows.Count + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员信息统计列表.xls", 100, Encoding.UTF8);
	}

	public static void RechargeReportExcel(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='9' style='text-align:center; '><h2><strong>充值统计列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='9' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>充值单号</td><td>会员卡号</td><td>会员姓名</td><td>充值类型</td><td>充值金额</td><td>卡内余额</td><td>备注</td><td>商家</td><td>日期</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["RechargeAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + DataExcelInfo.GetRechargeType(dt.Rows[i]["RechargeType"].ToString()) + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["RechargeMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["RechargeCardBalance"].ToString() + "</td>");
			str.Append("<td style='width:400px'>" + dt.Rows[i]["RechargeRemark"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["RechargeCreateTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["RechargeMoney"].ToString());
		}
		str.Append("<tr><td colspan='9'><h4>充值总金额:" + dclTotal.ToString("0.00") + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "充值统计列表.xls", 100, Encoding.UTF8);
	}

	public static void MemRechargeReportExcel(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>会员充值统计列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>会员卡号</td><td>会员姓名</td><td>充值总额（含赠送金额）</td><td>充值总金额</td><td>赠送总金额</td><td>账户余额</td><td>总充值次数</td><td>最后充值时间</td><td>会员所属商家</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["TotalMoney"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["RechargeTotalMoney"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["RechargeTotalGive"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemMoney"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["RechargeCount"] + "</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["LastRechargeTime"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["TotalMoney"].ToString());
		}
		str.Append("<tr><td colspan='10'><h4>会员充值总金额（含赠送金额）:" + dclTotal.ToString("0.00") + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员充值统计列表.xls", 100, Encoding.UTF8);
	}

	public static void ShopRechargeReportExcel(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='6' style='text-align:center; '><h2><strong>商家充值统计列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='6' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>商家名称</td><td>充值总额（含赠送金额）</td><td>充值总金额</td><td>赠送总金额</td><td>充值总次数</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["TotalMoney"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["RechargeTotalMoney"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["RechargeTotalGive"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TotalCount"] + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["TotalMoney"].ToString());
		}
		str.Append("<tr><td colspan='6'><h4>商家充值总金额(含赠送金额):" + dclTotal.ToString("0.00") + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "商家充值统计列表.xls", 100, Encoding.UTF8);
	}

	public static string GetRechargeType(string strType)
	{
		string strRechargeType = "现金充值";
		if (strType != null)
		{
			if (!(strType == "1"))
			{
				if (!(strType == "2"))
				{
					if (!(strType == "3"))
					{
						if (strType == "4")
						{
							strRechargeType = "网上充值";
						}
					}
					else
					{
						strRechargeType = "银联充值";
					}
				}
				else
				{
					strRechargeType = "现金充值";
				}
			}
			else
			{
				strRechargeType = "初始充值";
			}
		}
		return strRechargeType;
	}

	public static void PointChangeReportExcel(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='8' style='text-align:center; '><h2><strong>会员积分变动列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>会员卡号</td><td>会员姓名</td><td>积分</td><td>备注</td><td>商家</td><td>日期</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["PointNumber"].ToString() + "</td>");
			str.Append("<td style='width:400px'>" + dt.Rows[i]["PointRemark"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["PointCreateTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["PointNumber"].ToString());
		}
		str.Append("<tr><td colspan='8'><h4>变动总积分:" + dclTotal.ToString() + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员积分变动列表.xls", 100, Encoding.UTF8);
	}

	public static void AllianceListShopReportExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='8' style='text-align:center; '><h2><strong>联盟商营业统计报表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>联盟商名称</td><td>积分余额</td><td>短信余额</td><td>是否锁定</td><td>注册会员</td><td>注册商家</td><td>加盟时间</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["PointCount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["SmsCount"].ToString() + "</td>");
			if (bool.Parse(dt.Rows[i]["ShopState"].ToString()))
			{
				str.Append("<td style='width:100px'>是</td>");
			}
			else
			{
				str.Append("<td style='width:100px'>否</td>");
			}
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemCount"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopCount"].ToString() + "</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["ShopCreateTime"] + "</td>");
			str.Append("</tr>");
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "联盟商营业统计报表.xls", 100, Encoding.UTF8);
	}

	private static string BindShopName(object shopid)
	{
		Chain.BLL.SysShop Shop = new Chain.BLL.SysShop();
		string shopname = "";
		if (shopid != null)
		{
			shopname = Shop.GetShopNameByShopid(shopid.ToString());
		}
		return shopname;
	}

	public static void ShopListShopReportExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>商家营业统计报表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>商家名称</td><td>所属联盟商</td><td>积分余额</td><td>会员总消费次数</td><td>会员消费总金额</td><td>本店开卡会员在本店消费次数</td><td>本店开卡会员在本店消费总额</td><td>本店开卡会员消费总次数</td><td>本店开卡会员消费总额</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "&nbsp;&nbsp;&nbsp;</td>");
			string lmsName = DataExcelInfo.BindShopName(dt.Rows[i]["FatherShopID"]);
			str.Append("<td style='width:150px'>" + lmsName + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["PointCount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TotalNumber"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TotalMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemNumber"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemMoney"].ToString() + "</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["MemTotalNumber"] + "</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["MemTotalMoney"] + "</td>");
			str.Append("</tr>");
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "商家营业统计报表.xls", 100, Encoding.UTF8);
	}

	public static void PointExchangeReportExcel(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>积分兑换列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>会员卡号</td><td>会员姓名</td><td>礼品名称</td><td>礼品简码</td><td>数量</td><td>总积分</td><td>商家</td><td>日期</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + i + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["GiftName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["GiftCode"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ExchangeNumber"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ExchangeTotalPoint"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ExchangeTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["ExchangeTotalPoint"].ToString());
		}
		str.Append("<tr><td colspan='10'><h4>兑换总积分:" + dclTotal.ToString() + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "积分兑换列表.xls", 100, Encoding.UTF8);
	}

	public static void RptExpenseReportExcel(DataTable dt, string Master)
	{
		decimal dclTotalMoney = 0m;
		decimal dclDiscountMoney = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='11' style='text-align:center; '><h2><strong>快速消费列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>帐单号</td><td>会员卡号</td><td>会员姓名</td><td>总金额</td><td>折扣后金额</td><td>积分</td><td>备注</td><td>商家</td><td>日期</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + i + "</td>");
			str.Append("<td style='width:200px;vnd.ms-excel.numberformat:@'>" + dt.Rows[i]["OrderAccount"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["OrderTotalMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["OrderDiscountMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["OrderPoint"].ToString() + "</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["OrderRemark"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["OrderCreateTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			dclTotalMoney += decimal.Parse(dt.Rows[i]["OrderTotalMoney"].ToString());
			dclDiscountMoney += decimal.Parse(dt.Rows[i]["OrderDiscountMoney"].ToString());
		}
		str.Append("<tr><td colspan='11'><h4>消费总金额:" + dclTotalMoney.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;");
		str.Append("折扣后消费总金额:" + dclDiscountMoney.ToString() + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "快速消费列表.xls", 100, Encoding.UTF8);
	}

	public static void MemExpenseReportExcel(DataTable dt, string Master, string strSql)
	{
		decimal dclTotalMoney = 0m;
		int intNumber = dt.Rows.Count;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>会员消费列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>会员卡号</td><td>会员姓名</td><td>实际支付总额</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["DiscountMoney"].ToString() + "</td>");
			str.Append("</tr>");
			if (decimal.Parse(dt.Rows[i]["DiscountMoney"].ToString()) > 0m)
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				string strWhere = " MemID=" + dt.Rows[i]["MemID"].ToString() + " and";
				strWhere += strSql;
				DataTable dtDetail = bllMem.GetMemExpenseDetail(strWhere).Tables[0];
				str.Append("<tr style='color:blue;text-align:center;'><td></td><td>订单编号</td><td>消费类型</td><td>消费总额</td><td>折后总价</td><td>优惠券金额</td><td>获得积分</td><td>卡内余额</td><td>卡内积分</td><td>消费时间</td></tr>");
				for (int j = 0; j < dtDetail.Rows.Count; j++)
				{
					int b = j % 2;
					if (b == 0)
					{
						style = "style='color: blue; background-color: white;'";
					}
					str.Append("<tr " + style + " >");
					str.Append("<td></td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
					str.Append("<td style='width:100px'>" + DataExcelInfo.GetOrderTypeName(int.Parse(dtDetail.Rows[j]["OrderType"].ToString())) + "&nbsp;&nbsp;&nbsp;</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["OrderTotalMoney"] + "</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["OrderDiscountMoney"].ToString() + "</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["OrderPayCoupon"] + "</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["OrderPoint"].ToString() + "</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["OrderCardBalance"].ToString() + "</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["OrderCardPoint"].ToString() + "</td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderCreateTime"] + "</td>");
					str.Append("</tr>");
					dclTotalMoney += decimal.Parse(dtDetail.Rows[j]["OrderDiscountMoney"].ToString());
				}
			}
		}
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' ><h4>消费总人数：",
			intNumber,
			"&nbsp;&nbsp;&nbsp;&nbsp;会员消费总金额：",
			dclTotalMoney.ToString("0.00"),
			"</h4></td></tr>"
		}));
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员消费列表.xls", 100, Encoding.UTF8);
	}

	public static void PointRankReportExcel(DataTable dt, string Master)
	{
		int intTotal = 0;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='8' style='text-align:center; '><h2><strong>积分排行列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>积分</td><td>会员卡号</td><td>会员姓名</td><td>消费总金额</td><td>会员储存金额</td>><td>会员等级</td><td>所属商家</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemPoint"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemConsumeMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["LevelName"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("</tr>");
			intTotal += int.Parse(dt.Rows[i]["MemPoint"].ToString());
		}
		str.Append("<tr><td colspan='8'><h4>总积分:" + intTotal.ToString() + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "积分排行列表.xls", 100, Encoding.UTF8);
	}

	public static void ShopReportExcel(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='11' style='text-align:center; '><h2><strong>商家汇总统计</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td>序号</td><td>商家名称</td><td>消费应收总金额</td><td>消费实收总金额</td><td>初始充值总金额</td><td>常规充值总金额</td><td>消费得积分</td><td>消费使用积分</td><td>变动总积分</td>><td>会员总数量</td><td>会员账户总金额</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["sumOrderTotalMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["sumOrderDiscountMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["sumSRechargeMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["sumFRechargeMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["sumOrderPoint"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["sumUsePoint"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["sumPointNumber"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemCount"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemMoney"].ToString() + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["sumOrderDiscountMoney"].ToString());
		}
		str.Append("<tr><td colspan='11'><h4>所有商家总营业额:" + dclTotal.ToString() + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "商家汇总统计.xls", 100, Encoding.UTF8);
	}

	public static void GoodsListExcel(DataTable dt, string Master)
	{
		List<FieldModel> FieldList = new List<FieldModel>();
		FieldList.Add(new FieldModel("商品编码", "GoodsCode", "string", false));
		FieldList.Add(new FieldModel("商品名称", "Name", "string", false));
		FieldList.Add(new FieldModel("商品简码", "NameCode", "string", true));
		FieldList.Add(new FieldModel("商品分类", "GoodsClassID", "int", false));
		FieldList.Add(new FieldModel("计量单位", "Unit", "string", false));
		FieldList.Add(new FieldModel("参考进价", "GoodsBidPrice", "string", true));
		FieldList.Add(new FieldModel("零售单价", "Price", "string", false));
		FieldList.Add(new FieldModel("商品积分", "Point", "int", true));
		FieldList.Add(new FieldModel("商品类型", "GoodsType", "int", false));
		FieldList.Add(new FieldModel("最低折扣", "MinPercent", "decimal", true));
		FieldList.Add(new FieldModel("提成类型", "CommissionType", "int", true));
		FieldList.Add(new FieldModel("提成金额(比例)", "CommissionNumber", "decimal", true));
		FieldList.Add(new FieldModel("商品简介", "GoodsRemark", "string", true));
		Chain.BLL.MemCustomField bllCustomField = new Chain.BLL.MemCustomField();
		DataRow[] drCustomField = bllCustomField.GetList(" CustomType=2 ").Tables[0].Select();
		if (drCustomField.Length > 0)
		{
			List<string> _list = new List<string>();
			_list.Add("商品编码");
			_list.Add("商品名称");
			_list.Add("商品简码");
			_list.Add("商品分类");
			_list.Add("计量单位");
			_list.Add("参考进价");
			_list.Add("零售单价");
			_list.Add("商品积分");
			_list.Add("商品类型");
			_list.Add("最低折扣");
			_list.Add("提成类型");
			_list.Add("提成金额(比例)");
			_list.Add("商品简介");
			string CustomColumn = string.Empty;
			DataRow[] array = drCustomField;
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dr = array[i];
				int CustomColumn_no = 1;
				CustomColumn = DataExcelInfo.regx.Replace(dr["CustomFieldName"].ToString(), "");
				while (_list.Contains(CustomColumn))
				{
					CustomColumn = string.Format("{0}_{1}", DataExcelInfo.regx.Replace(dr["CustomFieldName"].ToString(), ""), CustomColumn_no);
					CustomColumn_no++;
				}
				_list.Add(CustomColumn);
				FieldList.Add(new FieldModel(CustomColumn, dr["CustomField"].ToString(), dr["CustomFieldType"].ToString(), bool.Parse(dr["CustomFieldIsNull"].ToString())));
			}
		}
		ExcelFile excel = new ExcelFile();
		ExcelWorksheet sheet = excel.Worksheets.Add("商品列表");
		sheet.Cells.GetSubrangeAbsolute(0, 0, 2, FieldList.Count - 1).Merged = true;
		sheet.Cells[0, 0].Style.Font.Size = 500;
		sheet.Cells[0, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[0, 0].Style.Font.Weight = 800;
		sheet.Cells[0, 0].Value = "商品统计";
		sheet.Cells.GetSubrangeAbsolute(3, 0, 3, FieldList.Count - 1).Merged = true;
		sheet.Cells[3, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Cells[3, 0].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Cells[3, 0].Value = string.Concat(new object[]
		{
			"制表人:",
			Master,
			"                          制表时间:",
			DateTime.Now
		});
		sheet.Rows[3].Height = 400;
		int row = 4;
		int col = 0;
		sheet.Rows[row].Height = 400;
		sheet.Rows[row].Style.VerticalAlignment = VerticalAlignmentStyle.Center;
		sheet.Rows[row].Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
		sheet.Rows[row].Style.Font.Color = Color.Red;
		foreach (FieldModel model in FieldList)
		{
			if (!model.FieldIsNull)
			{
				model.FiledName = "* " + model.FiledName;
			}
			sheet.Cells[row, col].Value = model.FiledName;
			sheet.Cells[row, col].Style.Font.Size = 200;
			sheet.Cells[row, col].Style.Font.Weight = 800;
			if (model.FieldType == "string" || model.FieldType == "datetime" || model.FieldType == "text")
			{
				sheet.Columns[col].Width = 5000;
			}
			else if (model.FieldType == "int")
			{
				sheet.Columns[col].Width = 3000;
			}
			else
			{
				sheet.Columns[col].Width = 4000;
			}
			col++;
		}
		int Rows = 5;
		foreach (DataRow drRow in dt.Rows)
		{
			sheet.Cells[Rows, 0].Value = drRow["GoodsCode"];
			sheet.Cells[Rows, 0].Style.WrapText = true;
			sheet.Cells[Rows, 1].Value = drRow["Name"];
			sheet.Cells[Rows, 2].Value = drRow["NameCode"];
			sheet.Cells[Rows, 3].Value = new Chain.BLL.GoodsClass().GetModel(Convert.ToInt32(drRow["GoodsClassID"])).ClassName;
			sheet.Cells[Rows, 4].Value = drRow["Unit"];
			sheet.Cells[Rows, 5].Value = drRow["GoodsBidPrice"];
			sheet.Cells[Rows, 6].Value = drRow["Price"];
			sheet.Cells[Rows, 7].Value = drRow["Point"];
			sheet.Cells[Rows, 8].Value = ((drRow["GoodsType"].ToString() == "0") ? "普通商品" : "服务项目");
			sheet.Cells[Rows, 9].Value = drRow["MinPercent"].ToString();
			sheet.Cells[Rows, 10].Value = ((drRow["CommissionType"].ToString() == "1") ? "按固定比例提成" : "按固定金额提成");
			sheet.Cells[Rows, 11].Value = drRow["CommissionNumber"];
			sheet.Cells[Rows, 12].Value = drRow["GoodsRemark"];
			int mycolumns = 13;
			DataRow[] array = drCustomField;
			for (int i = 0; i < array.Length; i++)
			{
				DataRow drmyrow = array[i];
				sheet.Cells[Rows, mycolumns].Value = drRow[drmyrow["CustomField"].ToString()];
				mycolumns++;
			}
			Rows++;
		}
		try
		{
			string filePath = HttpContext.Current.Server.MapPath("~/Upload/Template/");
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			if (File.Exists(filePath + "GoodsList.xls"))
			{
				File.Delete(filePath + "GoodsList.xls");
			}
			excel.SaveXls(filePath + "GoodsList.xls");
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
		HttpResponse response = HttpContext.Current.Response;
		response.ContentType = "application/x-zip-compressed";
		response.AddHeader("Content-Disposition", "attachment;filename=GoodsList.xls");
		string filename = HttpContext.Current.Server.MapPath("~/Upload/Template/GoodsList.xls");
		response.TransmitFile(filename);
	}

	public static string GetGoodsTypeName(int goodsType)
	{
		string strGoodsType = "";
		switch (goodsType)
		{
		case 0:
			strGoodsType = "普通商品";
			break;
		case 1:
			strGoodsType = "服务项目";
			break;
		}
		return strGoodsType;
	}

	public static void GoodsLogExcel(DataTable dt, string Master, string strAgent)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='9' style='text-align:center; '><h2><strong>入库出库明细</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='9' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>单号</td><td>出入库方式</td><td>总价格</td><td>变更商家</td><td>备注</td><td>时间</td><td>操作商家</td><td>操作人员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:180px'>" + dt.Rows[i]["GoodsAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + DataExcelInfo.GetGoodsType(int.Parse(dt.Rows[i]["Type"].ToString())) + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TotalPrice"] + "</td>");
			str.Append("<td style='width:130px'>" + dt.Rows[i]["ChangeShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["Remark"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["CreateTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			string strSql = "  GoodsLogDetail.GoodsID=Goods.GoodsID ";
			strSql = strSql + "  and GoodsLogDetail.GoodsLogID=" + dt.Rows[i]["ID"];
			if (strAgent != "")
			{
				strSql += strAgent;
			}
			Chain.BLL.GoodsLogDetail bllDetail = new Chain.BLL.GoodsLogDetail();
			DataTable dtDetail = bllDetail.GetListSP(strSql).Tables[0];
			str.Append("<tr style='color:blue;text-align:center;'><td></td><td></td><td>商品编码</td><td>商品名称</td><td>商品入库单价</td><td>商品出库单价</td><td>商品数量</td></tr>");
			for (int j = 0; j < dtDetail.Rows.Count; j++)
			{
				int b = j % 2;
				if (b == 0)
				{
					style = "style='color: blue; background-color: white;'";
				}
				str.Append("<tr " + style + " >");
				str.Append("<td></td>");
				str.Append("<td></td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["GoodsCode"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["Name"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["GoodsInPrice"].ToString() + "</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["GoodsOutPrice"].ToString() + "</td>");
				str.Append("<td style='width:150px'>" + Math.Abs(int.Parse(dtDetail.Rows[j]["GoodsNumber"].ToString())) + "</td>");
				str.Append("</tr>");
			}
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "商品入库出库统计.xls", 100, Encoding.UTF8);
	}

	public static string GetGoodsType(int intGoodsType)
	{
		string strGoodsType = "";
		switch (intGoodsType)
		{
		case 0:
			strGoodsType = "初始化商品数据";
			break;
		case 1:
			strGoodsType = "商品入库";
			break;
		case 2:
			strGoodsType = "商品销售出库";
			break;
		case 3:
			strGoodsType = "商品挂单出库";
			break;
		case 4:
			strGoodsType = "撤销订单入库";
			break;
		case 5:
			strGoodsType = "商品退货入库";
			break;
		case 6:
			strGoodsType = "商品调拨";
			break;
		case 7:
			strGoodsType = "商品退货入库";
			break;
		case 8:
			strGoodsType = "商品编辑库存入库";
			break;
		case 9:
			strGoodsType = "商品编辑库存出库";
			break;
		case 10:
			strGoodsType = "商品入库撤单";
			break;
		case 11:
			strGoodsType = "商品入库(已撤单)";
			break;
		case 12:
			strGoodsType = "商品调拨出库";
			break;
		case 13:
			strGoodsType = "商品调拨入库";
			break;
		case 14:
			strGoodsType = "商品库存批量导入";
			break;
		}
		return strGoodsType;
	}

	public static void EmptyBillsExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='11' style='text-align:center; '><h2><strong>消费挂单明细</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>订单编号</td><td>会员卡号</td><td>会员姓名</td><td>应付金额</td><td>实付金额</td><td>应获积分</td><td>挂单备注</td><td>挂单商家</td><td>操作人员</td><td>挂单时间</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["OrderAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemCard"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["OrderTotalMoney"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["OrderDiscountMoney"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["OrderPoint"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["OrderRemark"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["OrderCreateTime"] + "</td>");
			str.Append("</tr>");
			string strSql = " OrderDetail.OrderID=OrderLog.OrderID and OrderDetail.GoodsID=Goods.GoodsID ";
			strSql = strSql + " and OrderDetail.OrderID=" + dt.Rows[i]["OrderID"];
			Chain.BLL.OrderDetail bllOrderDetail = new Chain.BLL.OrderDetail();
			DataTable dtDetail = bllOrderDetail.GetListSP(strSql).Tables[0];
			str.Append("<tr style='color:blue;text-align:center;'><td></td><td></td><td>商品编码</td><td>商品名称</td><td>商品类型</td><td>商品单价</td><td>商品数量</td><td>商品积分</td><td>折后总价</td></tr>");
			for (int j = 0; j < dtDetail.Rows.Count; j++)
			{
				int b = j % 2;
				if (b == 0)
				{
					style = "style='color: blue; background-color: white;'";
				}
				str.Append("<tr " + style + " >");
				str.Append("<td></td>");
				str.Append("<td></td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["GoodsCode"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["Name"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:100px'>" + ((int.Parse(dtDetail.Rows[j]["GoodsType"].ToString()) == 0) ? "普通商品" : "服务商品") + "</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderDetailPrice"].ToString() + "</td>");
				str.Append("<td style='width:150px'>" + Math.Abs(int.Parse(dtDetail.Rows[j]["OrderDetailNumber"].ToString())) + "</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderDetailPoint"].ToString() + "</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderDetailDiscountPrice"].ToString() + "</td>");
				str.Append("</tr>");
			}
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "消费挂单明细统计.xls", 100, Encoding.UTF8);
	}

	public static void ExpenseHistory(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		decimal dclCoupon = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='15' style='text-align:center; '><h2><strong>消费历史记录列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='15' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:#000000; font-size:18px;text-align:center;'><td >序号</td><td>订单编号</td><td>会员姓名</td><td>会员卡号</td><td>消费类型</td><td>应付金额</td><td>实付金额</td><td>优惠券金额</td><td>获得积分</td><td>卡内余额</td><td>消费备注</td><td>消费时间</td><td>消费商家</td><td>操作人员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " style='color:#333333; font-size:14px;' >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:160px'>" + dt.Rows[i]["OrderAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + DataExcelInfo.GetOrderTypeName(int.Parse(dt.Rows[i]["OrderType"].ToString())) + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["OrderTotalMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["OrderDiscountMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["OrderPayCoupon"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["OrderPoint"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["OrderCardBalance"] + "</td>");
			str.Append("<td style='width:400px'>" + dt.Rows[i]["OrderRemark"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["OrderCreateTime"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			if (int.Parse(dt.Rows[i]["OrderType"].ToString()) == 2 || int.Parse(dt.Rows[i]["OrderType"].ToString()) == 4 || int.Parse(dt.Rows[i]["OrderType"].ToString()) == 6 || int.Parse(dt.Rows[i]["OrderType"].ToString()) == 7)
			{
				string strSql = " OrderDetail.OrderID=OrderLog.OrderID and OrderDetail.GoodsID=Goods.GoodsID";
				strSql = strSql + " and OrderDetail.OrderID=" + dt.Rows[i]["OrderID"];
				Chain.BLL.OrderDetail bllOrderDetail = new Chain.BLL.OrderDetail();
				DataTable dtDetail = bllOrderDetail.GetListSP(strSql).Tables[0];
				if (dtDetail.Rows.Count > 0)
				{
					str.Append("<tr style='color:blue;text-align:center;'><td></td><td></td><td>商品编码</td><td>商品名称</td><td>商品类型</td><td>商品单价</td><td>商品数量</td><td>获得积分</td><td>实付金额</td></tr>");
					for (int j = 0; j < dtDetail.Rows.Count; j++)
					{
						int b = j % 2;
						if (b == 0)
						{
							style = "style='color: blue; background-color: white;'";
						}
						str.Append("<tr " + style + " >");
						str.Append("<td></td>");
						str.Append("<td></td>");
						str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["GoodsCode"] + "&nbsp;&nbsp;&nbsp;</td>");
						str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["Name"] + "&nbsp;&nbsp;&nbsp;</td>");
						str.Append("<td style='width:100px'>" + DataExcelInfo.GetGoodsTypeName(int.Parse(dtDetail.Rows[j]["GoodsType"].ToString())) + "</td>");
						str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["OrderDetailPrice"].ToString() + "</td>");
						str.Append("<td style='width:150px'>" + Math.Abs(decimal.Parse(dtDetail.Rows[j]["OrderDetailNumber"].ToString())) + "</td>");
						str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderDetailPoint"].ToString() + "</td>");
						str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderDetailDiscountPrice"] + "</td>");
						str.Append("</tr>");
					}
				}
			}
			dclTotal += decimal.Parse(dt.Rows[i]["OrderDiscountMoney"].ToString());
			dclCoupon += decimal.Parse(dt.Rows[i]["OrderPayCoupon"].ToString());
		}
		str.Append(string.Concat(new string[]
		{
			"<tr><td colspan='7'><h4>消费总金额:",
			dclTotal.ToString("0.00"),
			"</h4></td><td colspan='8'><h4>会员使用优惠券总金额:",
			dclCoupon.ToString("0.00"),
			"</h4></td></tr>"
		}));
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员消费历史记录列表.xls", 100, Encoding.UTF8);
	}

	public static string GetOrderTypeName(int orderType)
	{
		string strOrderType = "";
		switch (orderType)
		{
		case 0:
			strOrderType = "快速消费";
			break;
		case 1:
			strOrderType = "计时消费";
			break;
		case 2:
			strOrderType = "商品消费";
			break;
		case 3:
			strOrderType = "商品挂单";
			break;
		case 4:
			strOrderType = "消费撤单";
			break;
		case 6:
			strOrderType = "消费退货";
			break;
		case 7:
			strOrderType = "计次消费";
			break;
		}
		return strOrderType;
	}

	public static void PointRateExcel(DataTable dt, string Master, string strWhere)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>积分提成列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>会员卡号</td><td>会员姓名</td><td>会员等级</td><td>会员积分</td><td>提成积分</td><td>所属商家</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["LEVELNAME"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemPoint"].ToString() + "</td>");
			str.Append("<td style='width:400px'>" + dt.Rows[i]["RatePointCount"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			if (int.Parse(dt.Rows[i]["RatePointCount"].ToString()) > 0)
			{
				StringBuilder strwhere = new StringBuilder();
				Chain.BLL.PointRate bllPointRate = new Chain.BLL.PointRate();
				int memID = Convert.ToInt32(dt.Rows[i]["MEMID"]);
				DataTable dtDetail = bllPointRate.GetMemDetailByMemCard(memID, strwhere.ToString()).Tables[0];
				str.Append("<tr style='color:blue;text-align:center;'><td></td><td></td><td>会员名称</td><td>会员卡号</td><td>提成积分</td><td>详情</td><td>订单编号</td><td>时间</td></tr>");
				for (int j = 0; j < dtDetail.Rows.Count; j++)
				{
					int b = j % 2;
					if (b == 0)
					{
						style = "style='color: blue; background-color: white;'";
					}
					str.Append("<tr " + style + " >");
					str.Append("<td></td>");
					str.Append("<td></td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["MEMNAME"] + "&nbsp;&nbsp;&nbsp;</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["MEMCARD"] + "&nbsp;&nbsp;&nbsp;</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["POINTNUMBER"] + "</td>");
					str.Append("<td style='width:400px'>" + dtDetail.Rows[j]["POINTREMARK"].ToString() + "</td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["PointOrderCode"].ToString() + "</td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["POINTCREATETIME"] + "</td>");
					str.Append("</tr>");
				}
			}
			dclTotal += decimal.Parse(dt.Rows[i]["RatePointCount"].ToString());
		}
		str.Append("<tr><td colspan='8'><h4>提成总积分:" + dclTotal.ToString() + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员积分提成列表.xls", 100, Encoding.UTF8);
	}

	public static void MemCount(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		decimal dclDiscountMoney = 0m;
		int intPoint = 0;
		int totalCount = 0;
		int RemainCount = 0;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='11' style='text-align:center; '><h2><strong>会员充次记录列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>充次单号</td><td>会员卡号</td><td>会员姓名</td><td>应付金额</td><td>实付金额</td><td>所得积分</td><td>充次备注</td><td>充次时间</td><td>充次商家</td><td>操作人员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["CountAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemCard"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["CountTotalMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["CountDiscountMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["CountPoint"] + "</td>");
			str.Append("<td style='width:400px'>" + dt.Rows[i]["CountRemark"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["CountCreateTime"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			string strSql = " CountDetailCountID=" + dt.Rows[i]["CountID"].ToString();
			Chain.BLL.MemCountDetail bllDetail = new Chain.BLL.MemCountDetail();
			DataTable dtDetail = bllDetail.GetList(strSql).Tables[0];
			str.Append("<tr style='color:blue;text-align:center;'><td></td><td></td><td>服务项目</td><td>计次数量</td><td>实付金额</td><td>所得积分</td></tr>");
			for (int j = 0; j < dtDetail.Rows.Count; j++)
			{
				int b = j % 2;
				if (b == 0)
				{
					style = "style='color: blue; background-color: white;'";
				}
				str.Append("<tr " + style + " >");
				str.Append("<td></td>");
				str.Append("<td></td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["Name"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["CountDetailTotalNumber"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["CountDetailDiscountMoney"].ToString() + "</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["CountDetailPoint"].ToString() + "</td>");
				str.Append("</tr>");
				totalCount += int.Parse(dtDetail.Rows[j]["CountDetailTotalNumber"].ToString());
				RemainCount += int.Parse(dtDetail.Rows[j]["CountDetailNumber"].ToString());
			}
			dclTotal += decimal.Parse(dt.Rows[i]["CountTotalMoney"].ToString());
			dclDiscountMoney += decimal.Parse(dt.Rows[i]["CountDiscountMoney"].ToString());
			intPoint += int.Parse(dt.Rows[i]["CountPoint"].ToString());
		}
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11'><h4>会员充次总金额:",
			dclTotal.ToString("0.00"),
			"&nbsp;&nbsp;&nbsp;&nbsp;折后总金额:",
			dclDiscountMoney.ToString("0.00"),
			"&nbsp;&nbsp;&nbsp;&nbsp;充次总积分:",
			intPoint,
			"&nbsp;&nbsp;&nbsp;&nbsp;充次总次数:",
			totalCount,
			"&nbsp;&nbsp;&nbsp;&nbsp;剩余总次数:",
			RemainCount,
			"</h4></td></tr>"
		}));
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员充次记录列表.xls", 100, Encoding.UTF8);
	}

	public static void MemDrawMoney(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		decimal dclDiscount = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>会员提现记录列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>提现单号</td><td>会员卡号</td><td>会员姓名</td><td>提现金额</td><td>实际提现金额</td><td>操作详情</td><td>提现日期</td><td>所属商家</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["DrawMoneyAccount"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["DrawMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["DrawActualMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["DrawMoneyRemark"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["DrawMoneyCreateTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["DrawActualMoney"].ToString());
			dclDiscount += decimal.Parse(dt.Rows[i]["DrawMoney"].ToString());
		}
		str.Append(string.Concat(new string[]
		{
			"<tr><td colspan='10'><h4>会员提现总金额:",
			dclTotal.ToString("0.00"),
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;会员实际提现总金额:",
			dclDiscount.ToString("0.00"),
			"</h4></td></tr>"
		}));
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员提现记录列表.xls", 100, Encoding.UTF8);
	}

	public static void Coupon(DataTable dt, string Master, Chain.Model.Coupon model)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='7' style='text-align:center; '><h2><strong>优惠券详情</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='7' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='7'><h4>优惠券名称：<b>",
			model.CouponTitle,
			"</b>&nbsp;优惠券类型：<b>",
			(model.CouponType > 0) ? "折扣券" : "代金券",
			"</b>&nbsp;",
			(model.CouponType > 0) ? "折扣比例：" : "优惠金额：",
			"<b>",
			model.CouponNumber,
			"</b>&nbsp;预发数量：<b>",
			model.CouponPredictNu,
			"</b>&nbsp;单日限用：<b>",
			model.CouponDayNum,
			"</b>&nbsp;最低消费：<b>",
			model.CouponMinMoney,
			"</b>&nbsp;有效期限：<b>",
			(model.CouponEffective > 0) ? (model.CouponStart + "至" + model.CouponEnd) : "永久有效",
			"</b>&nbsp;</h4></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>优惠券号</td><td>发送状态</td><td>使用状态</td><td>发送对象</td><td>发送时间</td><td>使用时间</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td>" + (i + 1) + "</td>");
			str.Append("<td style='text-align:center; '>" + dt.Rows[i]["CouPon"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='text-align:center; '>" + (bool.Parse(dt.Rows[i]["CouPonYF"].ToString()) ? "是" : "否") + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='text-align:center; '>" + (bool.Parse(dt.Rows[i]["CouPonSY"].ToString()) ? "是" : "否") + "</td>");
			str.Append("<td style='text-align:center; '>" + dt.Rows[i]["MemCard"].ToString() + "</td>");
			str.Append("<td style='text-align:center; '>" + dt.Rows[i]["ConPonSendTime"].ToString() + "</td>");
			str.Append("<td style='text-align:center; '>" + dt.Rows[i]["ConPonUseTime"].ToString() + "</td>");
			str.Append("</tr>");
		}
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='4'><h4>已发数量：<b>",
			model.CouponYF,
			"</b></h4></td><td colspan='3'><h4>已用数量：<b>",
			model.CouponSY,
			"</h4></td></tr>"
		}));
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "优惠券详情.xls", 100, Encoding.UTF8);
	}

	public static void StaffMoney(DataTable dt, string Master, string strDetail)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='7' style='text-align:center; '><h2><strong>员工提成记录列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='7' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>员工名称</td><td>员工编号</td><td>员工手机</td><td>提成总额</td><td>所属部门</td><td>所属商家</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["StaffName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["StaffNumber"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["StaffMobile"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["TotalMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ClassName"] + "</td>");
			str.Append("<td style='width:400px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("</tr>");
			if (decimal.Parse(dt.Rows[i]["TotalMoney"].ToString()) > 0m)
			{
				string strSql = " StaffID=" + dt.Rows[i]["StaffID"].ToString();
				strSql += strDetail;
				Chain.BLL.StaffMoney bllStaff = new Chain.BLL.StaffMoney();
				DataTable dtDetail = bllStaff.GetListSP(strSql).Tables[0];
				str.Append("<tr style='color:blue;text-align:center;'><td></td><td></td><td>提成账单</td><td>提成金额</td><td>提成商家</td><td>提成时间</td></tr>");
				for (int j = 0; j < dtDetail.Rows.Count; j++)
				{
					int b = j % 2;
					if (b == 0)
					{
						style = "style='color: blue; background-color: white;'";
					}
					str.Append("<tr " + style + " >");
					str.Append("<td></td>");
					str.Append("<td></td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["StaffOrderCode"] + "&nbsp;&nbsp;&nbsp;</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["TotalMoney"].ToString() + "&nbsp;&nbsp;&nbsp;</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["ShopName"] + "</td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["StaffCreateTime"].ToString() + "</td>");
					str.Append("</tr>");
				}
			}
			dclTotal += decimal.Parse(dt.Rows[i]["TotalMoney"].ToString());
		}
		str.Append("<tr><td colspan='7'><h4>员工提成总金额:" + dclTotal.ToString("0.00") + "</td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "员工提成记录列表.xls", 100, Encoding.UTF8);
	}

	public static void RptGiftExchangeExcel(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		int intNumber = 0;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='11' style='text-align:center; '><h2><strong>会员礼品兑换历史记录列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>兑换单号</td><td>会员卡号</td><td>会员姓名</td><td>兑换数量</td><td>消费积分</td><td>兑换类型</td><td>申请时间</td><td>审核时间</td><td>商家</td><td>操作员</tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ExchangeAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ExchangeAllNumber"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ExchangeAllPoint"] + "</td>");
			str.Append("<td style='width:100px'>" + ((Convert.ToInt32(dt.Rows[i]["ExchangeType"]) == 1) ? "主系统兑换" : "自助平台兑换") + "</td>");
			str.Append("<td style='width:150px'>" + Convert.ToDateTime(dt.Rows[i]["ApplicationTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
			str.Append("<td style='width:150px'>" + Convert.ToDateTime(dt.Rows[i]["ExchangeTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			dclTotal += decimal.Parse(dt.Rows[i]["ExchangeAllPoint"].ToString());
			intNumber += int.Parse(dt.Rows[i]["ExchangeAllNumber"].ToString());
			DataTable dtGiftExchangeDetail = new Chain.BLL.GiftExchangeDetail().GetGiftExchangeDetailByExchangeID(Convert.ToInt32(dt.Rows[i]["ExchangeID"]));
			for (int j = 0; j < dtGiftExchangeDetail.Rows.Count; j++)
			{
				int b = j % 2;
				if (b == 0)
				{
					style = "style='color: blue; background-color: white;'";
				}
				str.Append("<tr style='color:blue;text-align:center;'><td></td><td>序号</td><td>名称</td><td>兑换数量</td><td>积分</td><td>积分小计</td><td></td><td></td><td></td><td></td><td></td></tr>");
				str.Append("<tr " + style + " >");
				str.Append("<td></td>");
				str.Append("<td style='width:150px'>" + (j + 1) + "</td>");
				str.Append("<td style='width:150px'>" + dtGiftExchangeDetail.Rows[j]["GiftName"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:150px'>" + dtGiftExchangeDetail.Rows[j]["ExchangeNumber"] + "</td>");
				str.Append("<td style='width:150px'>" + Convert.ToInt32(dtGiftExchangeDetail.Rows[j]["ExchangePoint"]) / Convert.ToInt32(dtGiftExchangeDetail.Rows[j]["ExchangeNumber"]) + "</td>");
				str.Append("<td style='width:150px'>" + dtGiftExchangeDetail.Rows[j]["ExchangePoint"] + "</td>");
				str.Append("<td></td><td></td><td></td><td></td><td></td></tr>");
			}
		}
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11'><h4>兑换总数量：",
			intNumber,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;会员兑换总积分:",
			dclTotal.ToString(),
			"</h4></td></tr>"
		}));
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员礼品兑换历史记录列表.xls", 100, Encoding.UTF8);
	}

	public static void GoodsExpensExcel(DataTable dt, string Master, string strWhere)
	{
		decimal dclTotal = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='8' style='text-align:center; '><h2><strong>商品销售统计</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>商品编码</td><td>商品名称</td><td>所属分类</td><td>商品类型</td><td>商品单价</td><td>销售数量</td><td>销售总金额</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["GoodsCode"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["Name"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ClassName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + DataExcelInfo.GetGoodsExpenseTypeName(int.Parse(dt.Rows[i]["GoodsType"].ToString())) + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["Price"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["totalNumber"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["totalMoney"] + "</td>");
			str.Append("</tr>");
			if (decimal.Parse(dt.Rows[i]["totalMoney"].ToString()) > 0m)
			{
				string strSql = "and OrderDetail.GoodsID=" + dt.Rows[i]["GoodsID"].ToString();
				strSql += strWhere;
				Chain.BLL.OrderDetail bllDetail = new Chain.BLL.OrderDetail();
				DataTable dtDetail = bllDetail.GetGoodsExpenseDetail(strSql).Tables[0];
				str.Append("<tr style='color:blue;text-align:center;'><td></td><td>订单编号</td><td>会员卡号</td><td>会员姓名</td><td>消费数量</td><td>商品总金额</td><td>消费时间</td></tr>");
				for (int j = 0; j < dtDetail.Rows.Count; j++)
				{
					int b = j % 2;
					if (b == 0)
					{
						style = "style='color: blue; background-color: white;'";
					}
					str.Append("<tr " + style + " >");
					str.Append("<td></td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["MemCard"].ToString() + "&nbsp;&nbsp;&nbsp;</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["MemName"] + "</td>");
					str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["OrderDetailNumber"] + "</td>");
					str.Append("<td style='width:100px'>" + decimal.Parse(dtDetail.Rows[j]["OrderDetailDiscountPrice"].ToString()).ToString("0.00") + "</td>");
					str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["OrderCreateTime"].ToString() + "</td>");
					str.Append("</tr>");
				}
			}
			dclTotal += decimal.Parse(dt.Rows[i]["totalMoney"].ToString());
		}
		str.Append("<tr><td colspan='8'><h4>商品消费总金额:" + dclTotal.ToString("0.00") + "</td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "商品销售记录列表.xls", 100, Encoding.UTF8);
	}

	public static string GetGoodsExpenseTypeName(int goodsType)
	{
		string strGoodsType = "";
		switch (goodsType)
		{
		case 0:
			strGoodsType = "普通商品";
			break;
		case 1:
			strGoodsType = "服务项目";
			break;
		}
		if (goodsType < 0)
		{
			strGoodsType = "会员计次";
		}
		return strGoodsType;
	}

	public static void RptMemDrainReportExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='12' style='text-align:center; '><h2><strong>会员流失统计列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='12' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>会员卡号</td><td>会员姓名</td><td>手机号码</td><td>会员金额</td><td>账户积分</td><td>会员等级</td><td>累计消费</td><td>上次消费日期</td><td>商家</td><td>办卡日期</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + i + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemMobile"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemPoint"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["LevelName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemConsumeMoney"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemConsumeLastTime"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCreateTime"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
		}
		str.Append("<tr><td colspan='12'><h4>会员总数:" + dt.Rows.Count + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员流失统计列表.xls", 100, Encoding.UTF8);
	}

	public static void PointUserWorkExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='8' style='text-align:center; '><h2><strong>员工换班列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>交班人</td><td>接班人</td><td>交班时间</td><td>新增会员数</td><td>应收总金额</td>><td>实收总金额</td><td>转入余额</td><td>是否结余</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + DataExcelInfo.GetUserNameByUserID(dt.Rows[i]["HandoverUserID"].ToString()) + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["EedTime"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["AddNewUser"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["AllMoneys"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["sjMoneys"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["Arrearage"] + "</td>");
			str.Append("<td style='width:150px'>" + DataExcelInfo.GetIspay(dt.Rows[i]["Ispay"].ToString()) + "</td>");
			str.Append("</tr>");
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "换班列表.xls", 100, Encoding.UTF8);
	}

	public static string GetIspay(string ispay)
	{
		string pay = "是";
		if (ispay == "1")
		{
			pay = "否";
		}
		return pay;
	}

	public static string GetUserNameByUserID(string userid)
	{
		return new Chain.BLL.SysUser().GetUserNameByUserID(userid);
	}

	public static void MemStorageTimingExcel(DataTable dt, string Master)
	{
		decimal dclTotalMoney = 0m;
		decimal dclDiscountMoney = 0m;
		int intPoint = 0;
		int totalTime = 0;
		int RemainTime = 0;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='13' style='text-align:center; '><h2><strong>会员充时报表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='13' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>充时单号</td><td>会员卡号</td><td>会员姓名</td><td>服务名称</td><td>充值时长</td><td>应付金额</td><td>实付金额</td><td>所得积分</td><td>充时备注</td><td>充时商家</td><td>充时时间</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["StorageTimingAccount"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["ProjectName"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["StorageTotalTime"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["StorageTimingTotalMoney"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["StorageTimingDiscountMoney"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["StorageTimingPoint"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["StorageTimingRemark"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["StorageTimingCreateTime"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["UserName"] + "</td>");
			str.Append("</tr>");
			dclTotalMoney += decimal.Parse(dt.Rows[i]["StorageTimingTotalMoney"].ToString());
			dclDiscountMoney += decimal.Parse(dt.Rows[i]["StorageTimingDiscountMoney"].ToString());
			intPoint += int.Parse(dt.Rows[i]["StorageTimingPoint"].ToString());
			totalTime += int.Parse(dt.Rows[i]["StorageTotalTime"].ToString());
			RemainTime += int.Parse(dt.Rows[i]["StorageResidueTime"].ToString());
		}
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='11'><h4>会员充时总金额:",
			dclTotalMoney.ToString("0.00"),
			"&nbsp;&nbsp;&nbsp;&nbsp;折后总金额:",
			dclDiscountMoney.ToString("0.00"),
			"&nbsp;&nbsp;&nbsp;&nbsp;充时总积分:",
			intPoint,
			"&nbsp;&nbsp;&nbsp;&nbsp;充时总时间:",
			totalTime,
			"&nbsp;&nbsp;&nbsp;&nbsp;剩余总时间:",
			RemainTime,
			"</h4></td></tr>"
		}));
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员充时.xls", 100, Encoding.UTF8);
	}

	public static void MoneyChangeLogReportExcel(DataTable dt, string Master)
	{
		decimal moneyChangeMoney = 0m;
		decimal moneyChangeCash = 0m;
		decimal moneyChangeBalance = 0m;
		decimal moneyChangeUnionPay = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='14' style='text-align:center; '><h2><strong>会员金额变动统计</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='14' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>订单编号</td><td>变动类型</td><td>会员卡号</td><td>卡内余额</td><td>会员姓名</td><td>变动金额</td><td>金额变动</td><td>余额变动</td><td>银联变动</td><td>赠送金额</td><td>变动时间</td><td>商家</td><td>操作员</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:200px'>" + dt.Rows[i]["MoneyChangeAccount"].ToString() + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + DataExcelInfo.GetExchangeType(dt.Rows[i]["MoneyChangeType"]) + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MoneyChangeMoney"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MoneyChangeCash"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MoneyChangeBalance"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MoneyChangeUnionPay"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MoneyChangeGiveMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MoneyChangeCreateTime"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["UserName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("</tr>");
			moneyChangeMoney += Convert.ToDecimal(dt.Rows[i]["MoneyChangeMoney"]);
			moneyChangeCash += Convert.ToDecimal(dt.Rows[i]["MoneyChangeCash"]);
			moneyChangeBalance += Convert.ToDecimal(dt.Rows[i]["MoneyChangeBalance"]);
			moneyChangeUnionPay += Convert.ToDecimal(dt.Rows[i]["MoneyChangeUnionPay"]);
		}
		str.Append("<tr><td colspan='14'><h4>变动金额统计:" + moneyChangeMoney.ToString("0.00") + "&nbsp;&nbsp;&nbsp;&nbsp;");
		str.Append("现金变动统计:" + moneyChangeCash.ToString("0.00") + "&nbsp;&nbsp;&nbsp;&nbsp;");
		str.Append("余额变动统计:" + moneyChangeBalance.ToString("0.00") + "&nbsp;&nbsp;&nbsp;&nbsp;");
		str.Append("银联变动统计:" + moneyChangeUnionPay.ToString("0.00") + "</h4></td></tr>");
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员金额变动统计.xls", 100, Encoding.UTF8);
	}

	private static string GetExchangeType(object obj)
	{
		string result;
		switch (Convert.ToInt32(obj))
		{
		case 1:
			result = "会员充值";
			break;
		case 2:
			result = "充值撤单";
			break;
		case 3:
			result = "快速消费";
			break;
		case 4:
			result = "快速消费撤单";
			break;
		case 5:
			result = "初始充值";
			break;
		case 6:
			result = "会员导入";
			break;
		case 7:
			result = "账户提现";
			break;
		case 8:
			result = "会员充次";
			break;
		case 9:
			result = "计时消费";
			break;
		case 10:
			result = "计时消费撤单";
			break;
		case 11:
			result = "商品消费撤单";
			break;
		case 12:
			result = "商品消费";
			break;
		case 13:
			result = "挂单结算";
			break;
		case 14:
			result = "商品退货";
			break;
		default:
			result = "未知类型";
			break;
		}
		return result;
	}

	public static void GoodsStockTotalExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='8' style='text-align:center; '><h2><strong>商品库存汇总列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>商品编码</td><td>商品名称</td><td>参考价</td><td>销售价</td><td>商品简码</td><td>库存数量</td><td>所属商家</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["GoodsCode"].ToString() + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["Name"].ToString() + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["GoodsBidPrice"] + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["Price"].ToString() + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["NameCode"].ToString() + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["Number"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("</tr>");
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "商品库存汇总列表.xls", 100, Encoding.UTF8);
	}

	public static void MoneyOpinionExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='8' style='text-align:center; '><h2><strong>会员反馈</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td>序号</td><td>会员姓名</td><td>会员性别</td><td>会员卡号</td><td>意见详情</td><td>所属商家</td><td>会员电话</td><td>反馈时间</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + (Convert.ToBoolean(dt.Rows[i]["MemSex"]) ? "男" : "女") + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemCard"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ProposalContent"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ShopName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MemMobile"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["ProposalTime"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("</tr>");
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "会员反馈.xls", 100, Encoding.UTF8);
	}

	public static void MicroGoodsListExcel(DataTable dt, string Master)
	{
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='8' style='text-align:center; '><h2><strong>会员反馈</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='8' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td>序号</td><td>商品编号</td><td>商品名称</td><td>商品售价</td><td>商品原价</td><td>积分数量</td><td>所属分类</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroGoodsCode"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroGoodsName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroPrice"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroSalePrice"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroPoint"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroGoodsClassName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("</tr>");
		}
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "商品列表.xls", 100, Encoding.UTF8);
	}

	public static void MicroExpenseHistory(DataTable dt, string Master)
	{
		decimal dclTotal = 0m;
		decimal dclCoupon = 0m;
		StringBuilder str = new StringBuilder();
		str.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset={0}\"/>", Encoding.UTF8);
		str.Append("<table cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' bordercolor='#cccccc'>");
		str.Append("<tr><td colspan='10' style='text-align:center; '><h2><strong>消费记录列表</h2></td></tr>");
		str.Append(string.Concat(new object[]
		{
			"<tr><td colspan='10' style='text-align:center; '><strong>   制表人:",
			Master,
			"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 制表时间:",
			DateTime.Now,
			"</strong></td></tr>"
		}));
		str.Append("<tr style='color:red;text-align:center;'><td >序号</td><td>账单号</td><td>状态</td><td>会员姓名</td><td>会员卡号</td><td>消费金额</td><td>折扣总价</td><td>优惠券金额</td><td>获得积分</td><td>卡内余额</td><td>消费备注</td><td>消费时间</td></tr>");
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			string style = "style='color: rgb(51, 51, 51); background-color: rgb(247, 246, 243);'";
			int a = i % 2;
			if (a == 0)
			{
				style = "style='color: rgb(40, 71, 117); background-color: white;'";
			}
			str.Append("<tr " + style + " >");
			str.Append("<td style='width:100px'>" + (i + 1) + "</td>");
			str.Append("<td style='width:160px'>" + dt.Rows[i]["MicroOrderAccount"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:160px'>" + DataExcelInfo.GetGoodsTypeStr(dt.Rows[i]["MicroOrderStatus"]) + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MemName"] + "&nbsp;&nbsp;&nbsp;</td>");
			str.Append("<td style='width:150px'>" + ((dt.Rows[i]["MemCard"].ToString() == "0") ? "无卡号" : dt.Rows[i]["MemCard"].ToString()) + "</td>");
			str.Append("<td style='width:100px'>" + dt.Rows[i]["MicroOrderTotalMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroOrderDiscountMoney"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroOrderPayCoupon"].ToString() + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroOrderPoint"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroOrderCardBalance"] + "</td>");
			str.Append("<td style='width:400px'>" + dt.Rows[i]["MicroOrderRemark"] + "</td>");
			str.Append("<td style='width:150px'>" + dt.Rows[i]["MicroOrderCreateTime"] + "</td>");
			str.Append("</tr>");
			string strSql = " MicroWebsiteOrderLogDetail.MicroOrderID=MicroWebsiteOrderLog.MicroOrderID and MicroWebsiteOrderLogDetail.MicroGoodsID=MicroWebsiteGoods.MicroGoodsID";
			strSql = strSql + " and MicroWebsiteOrderLogDetail.MicroOrderID=" + dt.Rows[i]["MicroOrderID"];
			DataTable dtDetail = new Chain.BLL.MicroWebsiteOrderLogDetail().GetListSP(strSql).Tables[0];
			str.Append("<tr style='color:blue;text-align:center;'><td></td><td></td><td>商品编码</td><td>商品名称</td><td>商品原价</td><td>销售数量</td><td>获得积分</td><td>折后总价</td></tr>");
			for (int j = 0; j < dtDetail.Rows.Count; j++)
			{
				int b = j % 2;
				if (b == 0)
				{
					style = "style='color: blue; background-color: white;'";
				}
				str.Append("<tr " + style + " >");
				str.Append("<td></td>");
				str.Append("<td></td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["MicroGoodsCode"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["MicroGoodsName"] + "&nbsp;&nbsp;&nbsp;</td>");
				str.Append("<td style='width:100px'>" + dtDetail.Rows[j]["MicroSalePrice"].ToString() + "</td>");
				str.Append("<td style='width:150px'>" + Math.Abs(int.Parse(dtDetail.Rows[j]["MicroOrderDetailNumber"].ToString())) + "</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["MicroOrderDetailPoint"].ToString() + "</td>");
				str.Append("<td style='width:150px'>" + dtDetail.Rows[j]["MicroOrderDetailDiscountPrice"] + "</td>");
				str.Append("</tr>");
			}
			dclTotal += decimal.Parse(dt.Rows[i]["MicroOrderDiscountMoney"].ToString());
			dclCoupon += decimal.Parse(dt.Rows[i]["MicroOrderPayCoupon"].ToString());
		}
		str.Append(string.Concat(new string[]
		{
			"<tr><td colspan='7'><h4>会员现金消费总金额:",
			dclTotal.ToString("0.00"),
			"</h4></td><td colspan='8'><h4>会员使用优惠券总金额:",
			dclCoupon.ToString("0.00"),
			"</h4></td></tr>"
		}));
		str.Append("</table>");
		DataExcelInfo.ExportExcel(str.ToString(), "消费记录列表.xls", 100, Encoding.UTF8);
	}

	public static string GetGoodsTypeStr(object objMicroOrderStatus)
	{
		string result;
		switch (Convert.ToInt32(objMicroOrderStatus))
		{
		case 1:
			result = "未支付";
			break;
		case 2:
			result = "已审核";
			break;
		case 3:
			result = "已退回";
			break;
		case 4:
			result = "已支付";
			break;
		default:
			result = "未知类型";
			break;
		}
		return result;
	}
}
