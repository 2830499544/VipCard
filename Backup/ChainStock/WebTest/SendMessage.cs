using QRCode;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace WebTest
{
	public static class SendMessage
	{
		private static Encoding encode = Encoding.GetEncoding("gb2312");

		public static string SendMMSToWG(string id, string pwd, string Mobile, string Subject, string Content)
		{
			string result = "";
			string subject = HexConvert.StringToHexString(Subject, SendMessage.encode);
			string d = Convert.ToString(1);
			string tt = "txt";
			string file = HttpContext.Current.Server.MapPath("");
			string tv = HexConvert.GetHexString(HexConvert.FileToBytes(file + "\\Readme.txt"));
			string pt = "jpg";
			string pv = HexConvert.GetHexString(QRCodeImage.imageToByte(Content));
			string url = "http://118.144.76.79:8080/mmsServer/sendMms";
			string postdata = string.Concat(new string[]
			{
				"id=",
				id,
				"&pwd=",
				pwd,
				"&subject=",
				subject,
				"&d1=",
				d,
				"&tt1=",
				tt,
				"&tv1=",
				tv,
				"&pt1=",
				pt,
				"&pv1=",
				pv,
				"&mt1=&mv1="
			});
			string mms_id = SendMessage.PostSend(url, postdata);
			if (mms_id != null & int.Parse(mms_id) > 0)
			{
				url = "http://118.144.76.79:8080/mmsServer/sendMobile";
				postdata = string.Concat(new string[]
				{
					"id=",
					id,
					"&pwd=",
					pwd,
					"&yw=10690029yd&mobile=",
					Mobile,
					"&mms_id=",
					mms_id
				});
				result = SendMessage.PostSend(url, postdata);
			}
			return result;
		}

		public static string PostSend(string url, string postdate)
		{
			HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
			myHttpWebRequest.Method = "POST";
			Stream myRequestStream = myHttpWebRequest.GetRequestStream();
			StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
			myStreamWriter.Write(postdate);
			myStreamWriter.Flush();
			myStreamWriter.Close();
			myRequestStream.Close();
			HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
			Stream myResponseStream = myHttpWebResponse.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
			string outdata = myStreamReader.ReadToEnd();
			myStreamReader.Close();
			myResponseStream.Close();
			return outdata;
		}

		public static string GetBalance(string strUserID, string strUserPwd)
		{
			string url = "http://118.144.76.79:8888/mmsServer2/queryBalance";
			string postdata = "user_id=" + strUserID + "&user_pwd=" + strUserPwd;
			string result = SendMessage.PostSend(url, postdata);
			string text = result;
			if (text != null)
			{
				if (text == "-2")
				{
					result = "-1";
					return result;
				}
				if (text == "null#")
				{
					result = "0";
					return result;
				}
			}
			result = result.Substring(result.Length - 1, 1);
			return result;
		}
	}
}
