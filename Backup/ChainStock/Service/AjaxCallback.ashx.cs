using Chain.BLL;
using Chain.Common;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ChainStock.Service
{
	public class AjaxCallback : IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			context.Response.Write("Hello World");
		}

		public void GetLevel()
		{
			try
			{
				HttpContext.Current.Response.ContentType = "application/json;charset=utf-8";
				string jsonCallBackFunName = string.Empty;
				jsonCallBackFunName = HttpContext.Current.Request.Params["callback"].ToString();
				Chain.BLL.MemLevel bll = new Chain.BLL.MemLevel();
				List<Chain.Model.MemLevel> listLevel = bll.GetLevelList("");
				StringBuilder sb = new StringBuilder();
				string[] strList = new string[listLevel.Count];
				for (int i = 0; i < listLevel.Count; i++)
				{
					strList[i] = JsonPlus.ToJson(listLevel[i]);
					if (0 == i)
					{
						sb.AppendFormat("{0}\":{1}", "{\"" + i, strList[i] + "}");
					}
					else
					{
						sb.AppendFormat(",{0}\":{1}", "{\"" + i, strList[i] + "}");
					}
				}
				if (listLevel.Count > 1)
				{
					HttpContext.Current.Response.Write(jsonCallBackFunName + "([" + sb.ToString() + "])");
				}
				else
				{
					HttpContext.Current.Response.Write(jsonCallBackFunName + "(" + sb.ToString() + ")");
				}
			}
			catch
			{
			}
		}
	}
}
