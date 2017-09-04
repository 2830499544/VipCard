using System;
using System.Management;

namespace Chain.Common
{
	public class Computer
	{
		public string CpuID;

		public string MacAddress;

		public string DiskID;

		public string IpAddress;

		public string LoginUserName;

		public string ComputerName;

		public string SystemType;

		public string TotalPhysicalMemory;

		private static Computer _instance;

		public static Computer Instance()
		{
			if (Computer._instance == null)
			{
				Computer._instance = new Computer();
			}
			return Computer._instance;
		}

		public Computer()
		{
			this.CpuID = this.GetCpuID();
			this.MacAddress = this.GetMacAddress();
			this.DiskID = this.GetDiskID();
			this.IpAddress = this.GetIPAddress();
			this.LoginUserName = this.GetUserName();
			this.SystemType = this.GetSystemType();
			this.TotalPhysicalMemory = this.GetTotalPhysicalMemory();
			this.ComputerName = this.GetComputerName();
		}

		private string GetCpuID()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_Processor");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject.Properties["ProcessorId"].Value.ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			return result;
		}

		private string GetMacAddress()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						if ((bool)managementObject["IPEnabled"])
						{
							text = managementObject["MacAddress"].ToString();
							break;
						}
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			return result;
		}

		private string GetIPAddress()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						if ((bool)managementObject["IPEnabled"])
						{
							Array array = (Array)managementObject.Properties["IpAddress"].Value;
							text = array.GetValue(0).ToString();
							break;
						}
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			return result;
		}

		private string GetDiskID()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_DiskDrive");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = (string)managementObject.Properties["Model"].Value;
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			return result;
		}

		private string GetUserName()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["UserName"].ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			return result;
		}

		private string GetSystemType()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["SystemType"].ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			return result;
		}

		private string GetTotalPhysicalMemory()
		{
			string result;
			try
			{
				string text = "";
				ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
				ManagementObjectCollection instances = managementClass.GetInstances();
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ManagementObject managementObject = (ManagementObject)enumerator.Current;
						text = managementObject["TotalPhysicalMemory"].ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "unknow";
			}
			return result;
		}

		private string GetComputerName()
		{
			string result;
			try
			{
				result = Environment.GetEnvironmentVariable("ComputerName");
			}
			catch
			{
				result = "unknow";
			}
			return result;
		}
	}
}
