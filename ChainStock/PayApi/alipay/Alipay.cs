using Com.Alipay;
using Com.Alipay.Business;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChainStock.PayApi.alipay
{
    public class Alipay
    {

        private LogHelper log = new LogHelper(AppDomain.CurrentDomain.BaseDirectory + "/PayApi/alipay/log/log.txt");

     //   static string curpath = HttpRuntime.AppDomainAppPath.ToString()+@"PayApi\alipay\keys\";

        IAlipayTradeService serviceClient;

        private AlipayTradePayContentBuilder BuildPayContent(string authcode,string money)
        {
            string out_trade_no = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();

            //扫码枪扫描到的用户手机钱包中的付款条码
            AlipayTradePayContentBuilder builder = new AlipayTradePayContentBuilder();

            //收款账号
            builder.seller_id = Config.pid;
            //订单编号
            builder.out_trade_no = out_trade_no;
            //支付场景，无需修改
            builder.scene = "bar_code";
            //支付授权码,付款码
            builder.auth_code = authcode;
            //订单总金额
            builder.total_amount = money;
            //参与优惠计算的金额
            //builder.discountable_amount = "";
            //不参与优惠计算的金额
            //builder.undiscountable_amount = "";
            //订单名称
            builder.subject = money;
            //自定义超时时间
            builder.timeout_express = "2m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "test store id";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = "test";


            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();

            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = "304";
            goods.goods_name = "goods#name";
            goods.price = "0.01";
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;

            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;

            return builder;
            

        }



        /// <summary>
        /// 提交支付请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool Alipay_Deliver(string money,string authcode)
        {

            serviceClient = F2FBiz.CreateClientInstance(Config.serverUrl, Config.appId, Config.getMerchantPriveteKeyStr(), Config.version,
                             Config.sign_type, Config.getMerchantPublicKeyStr(), Config.charset);
            //  BtnAlipay0.Enabled = false;
            AlipayTradePayContentBuilder builder = BuildPayContent(authcode,money);
            string out_trade_no = builder.out_trade_no;

            AlipayF2FPayResult payResult = serviceClient.tradePay(builder);

            switch (payResult.Status)
            {
                case ResultEnum.SUCCESS:
                    // DoSuccessProcess(payResult);
                    return true;
                case ResultEnum.FAILED:
                    return false;
                    //                    DoFailedProcess(payResult);
                case ResultEnum.UNKNOWN:
                    // result = "网络异常，请检查网络配置后，更换外部订单号重试";
                    return false;
            }
            return false;
            //  Response.Redirect("result.aspx?Text=" + result);
        }
    }
}