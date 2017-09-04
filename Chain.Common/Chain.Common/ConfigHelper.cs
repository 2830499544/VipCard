using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Xml;

namespace Chain.Common
{
	public class ConfigHelper
	{
		private static string docName = string.Empty;

		private static XmlNode node = null;

		private static int _configType = 0;

		private static int ConfigType
		{
			get
			{
				return ConfigHelper._configType;
			}
			set
			{
				ConfigHelper._configType = value;
			}
		}

		public static bool SetValue(string key, string value)
		{
			XmlDocument xmlDocument = new XmlDocument();
			ConfigHelper.loadConfigDoc(xmlDocument);
			ConfigHelper.node = xmlDocument.SelectSingleNode("//appSettings");
			if (ConfigHelper.node == null)
			{
				throw new InvalidOperationException("appSettings section not found");
			}
			bool result;
			try
			{
				XmlElement xmlElement = (XmlElement)ConfigHelper.node.SelectSingleNode("//add[@key='" + key + "']");
				if (xmlElement != null)
				{
					xmlElement.SetAttribute("value", value);
				}
				else
				{
					XmlElement xmlElement2 = xmlDocument.CreateElement("add");
					xmlElement2.SetAttribute("key", key);
					xmlElement2.SetAttribute("value", value);
					ConfigHelper.node.AppendChild(xmlElement2);
				}
				ConfigHelper.saveConfigDoc(xmlDocument, ConfigHelper.docName);
				
                string CacheKey = "AppSettings-" + key;
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static string GetValue(string key)
		{
            string CacheKey = "AppSettings-" + key;
			object obj = new object();
			try
			{
				obj = ConfigurationManager.AppSettings[key];
			}
			catch
			{
			}
			return obj.ToString();
		}

		private static void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
		{
			try
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(cfgDocPath, null);
				xmlTextWriter.Formatting = Formatting.Indented;
				cfgDoc.WriteTo(xmlTextWriter);
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
			catch
			{
				throw;
			}
		}

		public static bool removeElement(string elementKey)
		{
			bool result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				ConfigHelper.loadConfigDoc(xmlDocument);
				ConfigHelper.node = xmlDocument.SelectSingleNode("//appSettings");
				if (ConfigHelper.node == null)
				{
					throw new InvalidOperationException("appSettings section not found");
				}
				ConfigHelper.node.RemoveChild(ConfigHelper.node.SelectSingleNode("//add[@key='" + elementKey + "']"));
				ConfigHelper.saveConfigDoc(xmlDocument, ConfigHelper.docName);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool modifyElement(string elementKey)
		{
			bool result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				ConfigHelper.loadConfigDoc(xmlDocument);
				ConfigHelper.node = xmlDocument.SelectSingleNode("//appSettings");
				if (ConfigHelper.node == null)
				{
					throw new InvalidOperationException("appSettings section not found");
				}
				ConfigHelper.node.RemoveChild(ConfigHelper.node.SelectSingleNode("//add[@key='" + elementKey + "']"));
				ConfigHelper.saveConfigDoc(xmlDocument, ConfigHelper.docName);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private static XmlDocument loadConfigDoc(XmlDocument cfgDoc)
		{
			if (Convert.ToInt32(ConfigHelper.ConfigType) == Convert.ToInt32(ConfigFileType.AppConfig))
			{
				ConfigHelper.docName = Assembly.GetEntryAssembly().GetName().Name;
				ConfigHelper.docName += ".exe.config";
			}
			else
			{
				string str = (HttpContext.Current.Request.ApplicationPath != "/") ? HttpContext.Current.Request.ApplicationPath : "";
				ConfigHelper.docName = HttpContext.Current.Server.MapPath(str + "/web.config");
			}
			cfgDoc.Load(ConfigHelper.docName);
			return cfgDoc;
		}
	}
}
