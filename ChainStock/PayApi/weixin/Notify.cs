using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ChainStock.PayApi.weixin
{
    /// <summary>
    /// 回调处理基类
    /// 主要负责接收微信支付后台发送过来的数据，对数据进行签名验证
    /// 子类在此类基础上进行派生并重写自己的回调处理过程
    /// </summary>
    public class Notify
    {
        public Page page {get;set;}
        public Notify(Page page)
        {
            this.page = page;
        }

        /// <summary>
        /// 接收从微信支付后台发送过来的数据并验证签名
        /// </summary>
        /// <returns>微信支付后台返回的数据</returns>
        public WxPayData GetNotifyData()
        {
            //接收从微信后台POST过来的数据
            System.IO.Stream s = page.Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

           // builder.Append("<xml><appid><![CDATA[wx974321cff4ed2795]]></appid> <attach><![CDATA[test]]></attach> <bank_type><![CDATA[CFT]]></bank_type> <cash_fee><![CDATA[1]]></cash_fee> <fee_type><![CDATA[CNY]]></fee_type> <is_subscribe><![CDATA[Y]]></is_subscribe> <mch_id><![CDATA[1269348401]]></mch_id> <nonce_str><![CDATA[2ac04f91646340a08af4ebd2a22f2cff]]></nonce_str> <openid><![CDATA[okwG7wtInBHAYP_nzFocG-Gq6rLo]]></openid> <out_trade_no><![CDATA[wxcz201706262216376267]]></out_trade_no> <result_code><![CDATA[SUCCESS]]></result_code> <return_code><![CDATA[SUCCESS]]></return_code> <sign><![CDATA[27BAC6DC3BF7ECB53BA749EA26ACDD62]]></sign> <time_end><![CDATA[20170626221645]]></time_end> <total_fee>1</total_fee> <trade_type><![CDATA[JSAPI]]></trade_type> <transaction_id><![CDATA[4005352001201706267574911286]]></transaction_id> </xml>");
            //<xml><appid><![CDATA[wx974321cff4ed2795]]></appid> <attach><![CDATA[test]]></attach> <bank_type><![CDATA[CFT]]></bank_type> <cash_fee><![CDATA[1]]></cash_fee> <fee_type><![CDATA[CNY]]></fee_type> <is_subscribe><![CDATA[Y]]></is_subscribe> <mch_id><![CDATA[1269348401]]></mch_id> <nonce_str><![CDATA[bf9359dfa5b643c2904fe2fd82fb211c]]></nonce_str> <openid><![CDATA[okwG7wtInBHAYP_nzFocG-Gq6rLo]]></openid> <out_trade_no><![CDATA[wxcz201706262246000781]]></out_trade_no> <result_code><![CDATA[SUCCESS]]></result_code> <return_code><![CDATA[SUCCESS]]></return_code> <sign><![CDATA[3C7A689E770021749BA394DA2514B600]]></sign> <time_end><![CDATA[20170626224614]]></time_end> <total_fee>1</total_fee> <trade_type><![CDATA[JSAPI]]></trade_type> <transaction_id><![CDATA[4005352001201706267579539359]]></transaction_id> </xml>

            Log.Info(this.GetType().ToString(), "Receive data from WeChat : " + builder.ToString());

            //转换数据格式并验证签名
            WxPayData data = new WxPayData();

            try
            {
                data.FromXml(builder.ToString());
            }
            catch(WxPayException ex)
            {
                //若签名错误，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            Log.Info(this.GetType().ToString(), "Check sign success");
          //  Chain.IDAL.WeiXinLog lg = new Chain.IDAL.WeiXinLog();
         //   lg.AddWeixinRec(builder.ToString());

            return data;
        }

        //派生类需要重写这个方法，进行不同的回调处理
        public virtual void ProcessNotify()
        {

        }
    }
}