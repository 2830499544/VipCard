using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Chain.Wechat
{
	public class HttpRequestHelper
	{
		public string Reqeust(string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
			string responseText = reader.ReadToEnd();
			reader.Close();
			return responseText;
		}

		public string Reqeust(string url, string postText, bool isUseCert, string SSLCERT_PATH, string SSLCERT_PASSWORD)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			if (isUseCert)
			{
				string path = HttpContext.Current.Request.PhysicalApplicationPath + SSLCERT_PATH;
				X509Certificate2 cert = new X509Certificate2(path, SSLCERT_PASSWORD, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
				request.ClientCertificates.Add(cert);
			}
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			byte[] payload = Encoding.UTF8.GetBytes(postText);
			request.ContentLength = (long)payload.Length;
			Stream writer = request.GetRequestStream();
			writer.Write(payload, 0, payload.Length);
			writer.Close();
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
			string responseText = reader.ReadToEnd();
			reader.Close();
			return responseText;
		}

		public string Reqeust(string url, string postText)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			byte[] payload = Encoding.UTF8.GetBytes(postText);
			request.ContentLength = (long)payload.Length;
			Stream writer = request.GetRequestStream();
			writer.Write(payload, 0, payload.Length);
			writer.Close();
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
			string responseText = reader.ReadToEnd();
			reader.Close();
			return responseText;
		}
	}
}
