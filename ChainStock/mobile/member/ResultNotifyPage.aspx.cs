using Chain.BLL;
using Chain.Model;
using Chain.Wechat;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace ChainStock.mobile.member
{
	public class ResultNotifyPage : Page
	{
		protected HtmlForm form1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				try
				{
					SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
					Stream s = base.Request.InputStream;
					byte[] buffer = new byte[1024];
					StringBuilder builder = new StringBuilder();
					int count;
					while ((count = s.Read(buffer, 0, 1024)) > 0)
					{
						builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
					}
					s.Flush();
					s.Close();
					s.Dispose();
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.LoadXml(builder.ToString());
					XmlNode xmlNode = xmlDoc.FirstChild;
					XmlNodeList nodes = xmlNode.ChildNodes;
					foreach (XmlNode xn in nodes)
					{
						XmlElement xe = (XmlElement)xn;
						m_values[xe.Name] = xe.InnerText;
					}
					if (!(m_values["return_code"].ToString() != "SUCCESS"))
					{
						string out_trade_no = m_values["out_trade_no"].ToString();
						string appid = m_values["appid"].ToString();
						string mch_id = m_values["mch_id"].ToString();
						string transaction_id = m_values["transaction_id"].ToString();
						string nonce_str = m_values["nonce_str"].ToString();
						string sign = m_values["sign"].ToString();
						string time_end = m_values["time_end"].ToString();
						string attach = m_values["attach"].ToString();
						string[] sz = attach.Split(new char[]
						{
							','
						});
						string type = sz[0];
						string text = type;
						if (text != null)
						{
							if (text == "Membersrecharge")
							{
								Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();
								DataSet ds = bllMemRecharge.GetList(string.Format(" RechargeAccount='{0}'", out_trade_no));
								if (ds.Tables[0].Rows.Count > 0)
								{
									base.Response.Write(this.ToXml("SUCCESS", ""));
									base.Response.End();
								}
								else
								{
									CheckOrder checkorder = new CheckOrder();
									Sign signss = new Sign();
									Chain.Model.SysParameter modelSysParameter = new Chain.BLL.SysParameter().GetModel(1);
									string ordertrackingsign = signss.OrderTrackingSign(appid, mch_id, nonce_str, out_trade_no, transaction_id, modelSysParameter.Api);
									XmlNode xmNode = checkorder.GetCheckOrder(appid, mch_id, transaction_id, out_trade_no, nonce_str, ordertrackingsign);
									string trade_state = xmNode["trade_state"].InnerText.ToUpper();
									if (!(trade_state != "SUCCESS"))
									{
										int total_fee = Convert.ToInt32(sz[2]);
										int total_fees = Convert.ToInt32(m_values["total_fee"]);
										if (total_fee == total_fees)
										{
											text = type;
											if (text != null)
											{
												if (!(text == "Membersrecharge"))
												{
													if (text == "ShopMembersRecharge")
													{
														base.Response.Write(this.ToXml("SUCCESS", ""));
														base.Response.End();
													}
												}
												else
												{
													this.Membersrecharge(sz[1], sz[2], sz[3], sz[4], out_trade_no, time_end);
												}
											}
										}
									}
								}
							}
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private void Membersrecharge(string memid, string total_fee, string jf, string GiveMoney, string out_trade_no, string time_end)
		{
			try
			{
				int id = int.Parse(memid);
				int jifen = int.Parse(jf);
				decimal givemoney = decimal.Parse(GiveMoney);
				decimal totalfee = decimal.Parse(total_fee) / 100m;
				time_end = string.Format("{0}-{1}-{2} {3}:{4}:{5}", new object[]
				{
					time_end.Substring(0, 4),
					time_end.Substring(4, 2),
					time_end.Substring(6, 2),
					time_end.Substring(8, 2),
					time_end.Substring(10, 2),
					time_end.Substring(12, 2)
				});
				DateTime timeend = DateTime.Parse(time_end);
				Chain.Model.Mem mem = new Chain.Model.Mem();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				mem = bllMem.GetModel(id);
				mem.MemPoint += jifen;
				mem.MemMoney += totalfee + givemoney;
				bllMem.Update(mem);
				Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();
				bllMemRecharge.Add(new Chain.Model.MemRecharge
				{
					RechargeMemID = id,
					RechargeAccount = out_trade_no,
					RechargeMoney = totalfee,
					RechargeShopID = 1,
					RechargeUserID = 1,
					RechargeCreateTime = timeend,
					RechargeIsApprove = true,
					RechargeRemark = "会员微信充值",
					RechargePoint = jifen,
					RechargeType = 6,
					RechargeGive = givemoney,
					RechargeCardBalance = mem.MemMoney
				});
				Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
				moneyChangeLogModel.MoneyChangeMemID = id;
				moneyChangeLogModel.MoneyChangeUserID = 1;
				moneyChangeLogModel.MoneyChangeType = 1;
				moneyChangeLogModel.MoneyChangeAccount = out_trade_no;
				moneyChangeLogModel.MoneyChangeMoney = totalfee + givemoney;
				moneyChangeLogModel.MemMoney = mem.MemMoney;
				moneyChangeLogModel.MoneyChangeCreateTime = timeend;
				moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
				new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
				if (jifen > 0)
				{
					Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
					mdPoint.PointMemID = id;
					mdPoint.PointNumber = jifen;
					mdPoint.PointChangeType = 15;
					mdPoint.PointRemark = string.Concat(new object[]
					{
						"会员充值，充值金额：[",
						totalfee,
						"],获得积分：[",
						jifen,
						"]"
					});
					mdPoint.PointShopID = 1;
					mdPoint.PointCreateTime = timeend;
					mdPoint.PointUserID = 1;
					mdPoint.PointOrderCode = out_trade_no;
					new Chain.BLL.PointLog().Add(mdPoint);
					MEMPointUpdate.MEMPointRate(mem, jifen, out_trade_no, 15, 1, 1);
					PubFunction.UpdateMemLevel(mem);
				}
			}
			catch
			{
				return;
			}
			base.Response.Write(this.ToXml("SUCCESS", ""));
			base.Response.End();
		}

		private string ToXml(string return_code, string return_msg)
		{
			StringBuilder strSB = new StringBuilder();
			strSB.Append("<xml>");
			strSB.AppendFormat("<return_code>{0}</return_code>", return_code);
			strSB.AppendFormat("<return_msg>{0}</return_msg>", return_code);
			strSB.Append("</xml>");
			return strSB.ToString();
		}
	}
}
