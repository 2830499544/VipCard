using Chain.BLL;
using Chain.Model;
using ChainStock.PayApi.weixin;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class RechargeResultNotifyPage : Page
	{
		protected HtmlForm form1;

		protected void Page_Load(object sender, EventArgs e)
		{
			WxPayData notifyData = new Notify(this.Page).GetNotifyData();
			if (!notifyData.IsSet("transaction_id"))
			{
				WxPayData res = new WxPayData();
				res.SetValue("return_code", "FAIL");
				res.SetValue("return_msg", "支付结果中微信订单号不存在");
				Log.Error(base.GetType().ToString(), "The Pay result is error : " + res.ToXml());
				this.Page.Response.Write(res.ToXml());
				this.Page.Response.End();
			}
			string transaction_id = notifyData.GetValue("transaction_id").ToString();
			string out_trade_no = notifyData.GetValue("out_trade_no").ToString();
			if (!this.QueryOrder(transaction_id))
			{
				WxPayData res = new WxPayData();
				res.SetValue("return_code", "FAIL");
				res.SetValue("return_msg", "订单查询失败");
				Log.Error(base.GetType().ToString(), "Order query failure : " + res.ToXml());
				this.Page.Response.Write(res.ToXml());
				this.Page.Response.End();
			}
			//memid,money,givemoney,rechargeaccount,point
			else
            {


				string attach = notifyData.GetValue("attach").ToString();
                
                 Chain.IDAL.WeiXinLog lg = new Chain.IDAL.WeiXinLog();
                 lg.AddWeixinRec(attach);

				string[] data = attach.Split(new char[]
				{
					','
				});
				Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
				int intUserID = 1;
				int intUserShopID = 1;
				int intMemID = int.Parse(data[0]);
				decimal money = decimal.Parse(data[1]);
				decimal giveMoney = decimal.Parse(data[2]);
				string rechargeAccount = data[3];
				int point = int.Parse(data[4]);
				int count = new Chain.BLL.MemRecharge().GetRecordCount("RechargeAccount='" + rechargeAccount + "'");
				if (count <= 0)
				{
					if (money + giveMoney <= 0m)
					{
						this.Context.Response.Write("-6");
					}
					else
					{
						string strRemark = "无";
						DateTime createTime = DateTime.Now;
						Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
						Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
						Chain.BLL.PointLog bllPoingLog = new Chain.BLL.PointLog();
						Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
						string Remark = string.Concat(new object[]
						{
							"会员微信充值,充值金额：[",
							money.ToString(),
							"],赠送：[",
							giveMoney,
							"],备注：",
							strRemark
						});
						decimal sumMoney = money + giveMoney;
						Chain.Model.MemRecharge mdRechange = new Chain.Model.MemRecharge();
						mdRechange.RechargeMemID = intMemID;
						mdRechange.RechargeAccount = rechargeAccount;
						mdRechange.RechargeMoney = money + giveMoney;
						mdRechange.RechargeShopID = intUserShopID;
						mdRechange.RechargeUserID = intUserID;
						mdRechange.RechargeCreateTime = createTime;
						mdRechange.RechargeIsApprove = true;
						mdRechange.RechargeRemark = strRemark;
						mdRechange.RechargePoint = point;
						mdRechange.RechargeType = 6;
						mdRechange.RechargeGive = giveMoney;
						mdRechange.RechargeCardBalance = modelMem.MemMoney + sumMoney;
						int flag = new Chain.BLL.MemRecharge().Add(mdRechange);
						modelMem.MemMoney += sumMoney;
						modelMem.MemPoint += point;
						bllMem.Update(modelMem);
						Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
						moneyChangeLogModel.MoneyChangeMemID = modelMem.MemID;
						moneyChangeLogModel.MoneyChangeUserID = intUserID;
						moneyChangeLogModel.MoneyChangeType = 1;
						moneyChangeLogModel.MoneyChangeAccount = rechargeAccount;
						moneyChangeLogModel.MoneyChangeMoney = sumMoney;
						moneyChangeLogModel.MemMoney = modelMem.MemMoney;
						moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
						moneyChangeLogModel.MoneyChangeGiveMoney = giveMoney;
						new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
						mdPoint.PointMemID = modelMem.MemID;
						mdPoint.PointNumber = point;
						mdPoint.PointChangeType = 15;
						mdPoint.PointRemark = string.Concat(new object[]
						{
							"会员充值，充值金额：[",
							money,
							"],获得积分：[",
							point,
							"]"
						});
						mdPoint.PointShopID = intUserShopID;
						mdPoint.PointCreateTime = DateTime.Now;
						mdPoint.PointUserID = intUserID;
						mdPoint.PointOrderCode = rechargeAccount;
						bllPoint.Add(mdPoint);
						if (PubFunction.curParameter.bolShopPointManage)
						{
							PubFunction.SetShopPoint(intUserID, intUserShopID, point, "会员充值扣除店铺积分", 2);
						}
						MEMPointUpdate.MEMPointRate(modelMem, point, rechargeAccount, 15, intUserID, intUserShopID);
						modelMem = new Chain.BLL.Mem().GetModel(modelMem.MemID);
						PubFunction.UpdateMemLevel(modelMem);
						WxPayData res = new WxPayData();
						res.SetValue("return_code", "SUCCESS");
						res.SetValue("return_msg", "OK");
						Log.Info(base.GetType().ToString(), "order query success : " + res.ToXml());
						this.Page.Response.Write(res.ToXml());
						this.Page.Response.End();
					}
				}
			}
		}

		private bool QueryOrder(string transaction_id)
		{
			WxPayData req = new WxPayData();
			req.SetValue("transaction_id", transaction_id);
			WxPayData res = micropay.OrderQuery(req, 6);
			return res.GetValue("return_code").ToString() == "SUCCESS" && res.GetValue("result_code").ToString() == "SUCCESS";
		}
	}
}
