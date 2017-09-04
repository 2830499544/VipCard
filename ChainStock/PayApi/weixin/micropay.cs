using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WxPayAPI;

namespace ChainStock.PayApi.weixin
{
    public class micropay
    {
        /**
  * 刷卡支付完整业务流程逻辑
  * @param body 商品描述
  * @param total_fee 总金额
  * @param auth_code 支付授权码
  * @throws WxPayException
  * @return 刷卡支付结果
  */
        public static bool Run(string body, string total_fee, string auth_code)
        {
            WxPayData data = new WxPayData();
            data.SetValue("auth_code", auth_code);//授权码
            data.SetValue("body", "微信");//商品描述
            data.SetValue("total_fee", int.Parse((float.Parse(total_fee)*100).ToString()));//总金额
            data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());//产生随机的商户订单号

            WxPayData result = Micropay(data, 10);//提交被扫支付，接收返回结果

            //如果提交被扫支付接口调用失败，则抛异常
            if (!result.IsSet("return_code") || result.GetValue("return_code").ToString() == "FAIL")
            {
                string returnMsg = result.IsSet("return_msg") ? result.GetValue("return_msg").ToString() : "";
                Log.Error("MicroPay", "Micropay API interface call failure, result : " + result.ToXml());
                throw new WxPayException("Micropay API interface call failure, return_msg : " + returnMsg);
            }

            //签名验证
            result.CheckSign();
            Log.Debug("MicroPay", "Micropay response check sign success");

            //刷卡支付直接成功
            if (result.GetValue("return_code").ToString() == "SUCCESS" &&
                result.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }

            /******************************************************************
             * 剩下的都是接口调用成功，业务失败的情况
             * ****************************************************************/
            //1）业务结果明确失败
            if (result.GetValue("err_code").ToString() != "USERPAYING" &&
            result.GetValue("err_code").ToString() != "SYSTEMERROR")
            {
                return false;
            }

            //2）不能确定是否失败，需查单
            //用商户订单号去查单
            string out_trade_no = data.GetValue("out_trade_no").ToString();

            //确认支付是否成功,每隔一段时间查询一次订单，共查询10次
            int queryTimes = 10;//查询次数计数器
            while (queryTimes-- > 0)
            {
                int succResult = 0;//查询结果
                WxPayData queryResult = Query(out_trade_no, out succResult);
                //如果需要继续查询，则等待2s后继续
                if (succResult == 2)
                {
                    Thread.Sleep(2000);
                    continue;
                }
                //查询成功,返回订单查询接口返回的数据
                else if (succResult == 1)
                {
                    return true;
                }
                //订单交易失败，直接返回刷卡支付接口返回的结果，失败原因会在err_code中描述
                else
                {
                    return false;
                }
            }

            //确认失败，则撤销订单
            if (!Cancel(out_trade_no))
            {
                return false;
            }

            return false;
        }



        /**
     * 提交被扫支付API
     * 收银员使用扫码设备读取微信用户刷卡授权码以后，二维码或条码信息传送至商户收银台，
     * 由商户收银台或者商户后台调用该接口发起支付。
     * @param WxPayData inputObj 提交给被扫支付API的参数
     * @param int timeOut 超时时间
     * @throws WxPayException
     * @return 成功时返回调用结果，其他抛异常
     */
        public static WxPayData Micropay(WxPayData inputObj, int timeOut = 10)
        {
            string url = "https://api.mch.weixin.qq.com/pay/micropay";
            //检测必填参数
            if (!inputObj.IsSet("body"))
            {
                throw new WxPayException("提交被扫支付API接口中，缺少必填参数body！");
            }
            else if (!inputObj.IsSet("out_trade_no"))
            {
                throw new WxPayException("提交被扫支付API接口中，缺少必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new WxPayException("提交被扫支付API接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("auth_code"))
            {
                throw new WxPayException("提交被扫支付API接口中，缺少必填参数auth_code！");
            }
          //  , PubFunction.curParameter.strWeiXinAppSecret
            inputObj.SetValue("spbill_create_ip", WxPayConfig.IP);//终端ip
            inputObj.SetValue("appid",  PubFunction.curParameter.strWeiXinAppID);//公众账号ID
            inputObj.SetValue("mch_id", PubFunction.curParameter.strMchid);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();

            var start = DateTime.Now;//请求开始时间

            Log.Debug("WxPayApi", "MicroPay request : " + xml);
            string response = HttpService.Post(xml, url, false, timeOut);//调用HTTP通信接口以提交数据到API
            Log.Debug("WxPayApi", "MicroPay response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

          //  ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }


        public static WxPayData OrderQuery(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            inputObj.SetValue("appid", PubFunction.curParameter.strWeiXinAppID);//公众账号ID
            inputObj.SetValue("mch_id", PubFunction.curParameter.strMchid);//商户号
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            Log.Debug("WxPayApi", "OrderQuery request : " + xml);
            string response = HttpService.Post(xml, url, false, timeOut);//调用HTTP通信接口提交数据
            Log.Debug("WxPayApi", "OrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

          //  ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }
        /**
      * 
      * 查询订单情况
      * @param string out_trade_no  商户订单号
      * @param int succCode         查询订单结果：0表示订单不成功，1表示订单成功，2表示继续查询
      * @return 订单查询接口返回的数据，参见协议接口
      */
        public static WxPayData Query(string out_trade_no, out int succCode)
        {
            WxPayData queryOrderInput = new WxPayData();
            queryOrderInput.SetValue("out_trade_no", out_trade_no);
           
            WxPayData result = OrderQuery(queryOrderInput,6);

            if (result.GetValue("return_code").ToString() == "SUCCESS"
                && result.GetValue("result_code").ToString() == "SUCCESS")
            {
                //支付成功
                if (result.GetValue("trade_state").ToString() == "SUCCESS")
                {
                    succCode = 1;
                    return result;
                }
                //用户支付中，需要继续查询
                else if (result.GetValue("trade_state").ToString() == "USERPAYING")
                {
                    succCode = 2;
                    return result;
                }
            }

            //如果返回错误码为“此交易订单号不存在”则直接认定失败
            if (result.GetValue("err_code").ToString() == "ORDERNOTEXIST")
            {
                succCode = 0;
            }
            else
            {
                //如果是系统错误，则后续继续
                succCode = 2;
            }
            return result;
        }

        /**
     * 
     * 撤销订单API接口
     * @param WxPayData inputObj 提交给撤销订单API接口的参数，out_trade_no和transaction_id必填一个
     * @param int timeOut 接口超时时间
     * @throws WxPayException
     * @return 成功时返回API调用结果，其他抛异常
     */
        public static WxPayData Reverse(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("撤销订单API接口中，参数out_trade_no和transaction_id必须填写一个！");
            }

            inputObj.SetValue("appid", PubFunction.curParameter.strWeiXinAppID);//公众账号ID
            inputObj.SetValue("mch_id", PubFunction.curParameter.strMchid);//商户号
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();

            var start = DateTime.Now;//请求开始时间

            Log.Debug("WxPayApi", "Reverse request : " + xml);

            string response = HttpService.Post(xml, url, true, timeOut);

            Log.Debug("WxPayApi", "Reverse response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            WxPayData result = new WxPayData();
            result.FromXml(response);

           // ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }



        /**
        * 
        * 撤销订单，如果失败会重复调用10次
        * @param string out_trade_no 商户订单号
        * @param depth 调用次数，这里用递归深度表示
        * @return false表示撤销失败，true表示撤销成功
        */
        public static bool Cancel(string out_trade_no, int depth = 0)
        {
            if (depth > 10)
            {
                return false;
            }

            WxPayData reverseInput = new WxPayData();
            reverseInput.SetValue("out_trade_no", out_trade_no);
            WxPayData result = Reverse(reverseInput);

            //接口调用失败
            if (result.GetValue("return_code").ToString() != "SUCCESS")
            {
                return false;
            }

            //如果结果为success且不需要重新调用撤销，则表示撤销成功
            if (result.GetValue("result_code").ToString() != "SUCCESS" && result.GetValue("recall").ToString() == "N")
            {
                return true;
            }
            else if (result.GetValue("recall").ToString() == "Y")
            {
                return Cancel(out_trade_no, ++depth);
            }
            return false;
        }

    }
}