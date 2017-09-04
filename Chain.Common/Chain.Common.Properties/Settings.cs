using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Chain.Common.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"), CompilerGenerated]
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

		[ApplicationScopedSetting, DefaultSettingValue("http://zlsms.vip5968.net/SmsService.asmx"), SpecialSetting(SpecialSetting.WebServiceUrl), DebuggerNonUserCode]
		public string Chain_Common_WebReference_SmsService
		{
			get
			{
				return (string)this["Chain_Common_WebReference_SmsService"];
			}
		}
	}
}
