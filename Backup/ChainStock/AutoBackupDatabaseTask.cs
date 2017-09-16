using Chain.BLL;
using Chain.Tasks;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

public class AutoBackupDatabaseTask : ITask
{
	public void Execute(XmlNode node)
	{
		if (PubFunction.curParameter.AutoBackupDB)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string LuJing = AppDomain.CurrentDomain.BaseDirectory + "AppData\\AutoDataBase\\";
			string[] FileNames = Directory.GetFiles(LuJing);
			if (FileNames.Length > 0)
			{
				string LastFileName = FileNames[FileNames.Length - 1];
				string LastFileTimeStr = LastFileName.Substring(LastFileName.Length - 18, 14).ToString();
				DateTime LastFileTime = DateTime.ParseExact(LastFileTimeStr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
				int DD = PubFunction.curParameter.AutoBackupDay;
				DateTime AutoBackUpTime = LastFileTime.AddDays((double)DD);
				if (DateTime.Now >= AutoBackUpTime)
				{
					if (Directory.Exists(LuJing))
					{
						if (FileNames.Length > 2)
						{
							string[] array = FileNames;
							for (int i = 0; i < array.Length; i++)
							{
								string item = array[i];
								if (File.Exists(item))
								{
									File.Delete(item);
									break;
								}
							}
						}
						SysParameter par = new SysParameter();
						string databaseName = par.GetDataBaseName();
						string bakUpName = "Aoto" + databaseName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
						string fileName = LuJing + bakUpName;
						if (File.Exists(fileName))
						{
							File.Delete(fileName);
							par.DataBakUp(fileName);
						}
						else
						{
							par.DataBakUp(fileName);
						}
					}
				}
			}
			else
			{
				SysParameter par = new SysParameter();
				string databaseName = par.GetDataBaseName();
				string bakUpName = "Aoto" + databaseName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
				string fileName = LuJing + bakUpName;
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
					par.DataBakUp(fileName);
				}
				else
				{
					par.DataBakUp(fileName);
				}
			}
		}
	}
}
