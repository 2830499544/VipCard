using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ChainStock.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0"), CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[ApplicationScopedSetting, DefaultSettingValue("http://smsms.vip5968.net/SmsService.asmx"), SpecialSetting(SpecialSetting.WebServiceUrl), DebuggerNonUserCode]
		public string ChainStock_WebReference_SmsService
		{
			get
			{
				return (string)this["ChainStock_WebReference_SmsService"];
			}
		}
	}
}
