using System;
using System.Collections.Generic;
using System.Web;

namespace ChainStock.PayApi.weixin
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}