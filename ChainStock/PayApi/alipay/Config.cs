using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Config
/// </summary>
namespace ChainStock.PayApi.alipay
{
    public class Config
    {

        public static string appId = PubFunction.curParameter.AlipayAppId;
        public static string serverUrl = "https://openapi.alipay.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";
        public static string pid = PubFunction.curParameter.PartnerID;


         public static string charset = "utf-8";//"utf-8";
         public static string sign_type = "RSA";
         public static string version = "1.0";
     

        public Config()
        {
            //
        }

        public static string getMerchantPublicKeyStr()
        {
            return PubFunction.curParameter.AlipayPublicKey;
        }

        public static string getMerchantPriveteKeyStr()
        {
            return PubFunction.curParameter.AlipayPrivateKey;
        }

    }
}